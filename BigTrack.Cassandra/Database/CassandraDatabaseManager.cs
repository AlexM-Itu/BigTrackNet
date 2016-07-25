using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BigTrack.Cassandra.Configuration;
using BigTrack.Cassandra.Domain;
using BigTrack.Common.Database;
using BigTrack.Common.Domain;
using Cassandra;
using Newtonsoft.Json;

namespace BigTrack.Cassandra.Database
{
	public class CassandraDatabaseManager : IDatabaseManager
	{
		private Cluster cluster;
		private CassandraConnectionConfiguration connectionConfiguration;

		public void SetConnectionString(string connectionString)
		{
			connectionConfiguration = JsonConvert.DeserializeObject<CassandraConnectionConfiguration>(connectionString);
			cluster = Cluster.Builder().AddContactPoint(connectionConfiguration.ContactPoint).Build();
		}

		private ISession GetSession()
		{
			return cluster.Connect(connectionConfiguration.Keyspace);
		}

		public List<Table> GetDatabaseTables()
		{
			using (var session = GetSession())
			{
				return session
					.Execute("SELECT \"id\", \"name\" FROM \"TableNames\"")
					.Select(row => new Table
					{
						Id = row.GetValue<Guid>("id").ToString(),
						Name = row.GetValue<string>("name")
					})
					.ToList();
			}
		}
		
		public List<Column> GetTableColumns(string tableId)
		{
			using (var session = GetSession())
			{
				var query = session.Prepare("select \"id\", \"columnname\" from \"TableColumns\" where \"tableid\" = ? allow filtering"); 

				return session
					.Execute(query.Bind(tableId))
					.Select(row => new Column
					{
						Id = row.GetValue<Guid>("id").ToString(),
						Name = row.GetValue<string>("columnname")
					})
					.ToList();
			}
		}

		public List<TableChange> FindChangesets(string tableId, ChangeSearchOptions searchOptions)
		{
			using (var session = GetSession())
			{
				var query = string.Format("SELECT \"changeid\"  FROM \"TableChanges\" where \"tableid\" ='{0}'", tableId);
				if (searchOptions.FromDate.HasValue)
					query += string.Format(" and \"changetimestamp\" >= '{0}'", searchOptions.FromDate);

				if (searchOptions.ToDate.HasValue)
					query +=  string.Format(" and \"changetimestamp\" <= '{0}'", searchOptions.ToDate);

				if (searchOptions.User != null)
					query += string.Format(" and \"dbuser\" = +'{0}'", searchOptions.User);
				
				if (searchOptions.AffectedColumns != null && searchOptions.AffectedColumns.Any())
				{
					query += " and (";
					bool isFirst = true;
					foreach (var column in searchOptions.AffectedColumns)
					{
						query += string.Format(" {0} \"columnid\" = '{1}'", (!isFirst ? "or" : string.Empty), column);
						isFirst = false;
					}

					query += ")";
				}

				query += " allow filtering";

				return session.Execute(query)
					.Select(row => row.GetValue<string>("changeid").ToString())
					.Distinct()
					.Select(GetChangesetDetails)
					.ToList();
			}

		}

		public TableChange GetChangesetDetails(string changesetId)
		{
			using (var session = GetSession())
			{
				var query = session.Prepare("SELECT \"id\", \"changeid\", \"tableid\", \"tablename\", \"changetimestamp\", \"dbuser\", \"columnid\", \"columnname\", \"priorvalue\", \"updatedvalue\", \"operation\"  FROM \"TableChanges\" where \"changeid\"=? allow filtering");
					
				var result = session.Execute(query.Bind(changesetId)).ToList();

				return ConvertCassandraTableChangesToCommonTableChange(result.Select(MapCassandraTableChange).ToList());
			}
		}

		private TableChange ConvertCassandraTableChangesToCommonTableChange(List<CassandraTableChange> cassandraTableChanges)
		{
			if (!cassandraTableChanges.Any())
				return null;

			return new TableChange
			{
				Id = cassandraTableChanges.First().ChangeId,
				TableId = cassandraTableChanges.First().TableId,
				Table = new Table
				{
					Id = cassandraTableChanges.First().TableId,
					Name = cassandraTableChanges.First().TableName
				},
				ChangeTimestamp = cassandraTableChanges.First().ChangeTimestamp,
				OperationTypeId = cassandraTableChanges.First().Operation,
				OperationType = new OperationType
				{
					Id = cassandraTableChanges.First().Operation,
					Name = MapCassandraOperationTypeIdToName(cassandraTableChanges.First().Operation),
				},
				User = cassandraTableChanges.First().User,
				ColumnChanges = cassandraTableChanges
					.Select(change=> new ColumnChange
					{
						ColumnName = change.ColumnName,
						PriorValue = change.PriorValue,
						UpdatedValue = change.UpdatedValue
					})
					.ToList()
			};
		}

		private string MapCassandraOperationTypeIdToName(byte operation)
		{
			switch (operation)
			{
				case 1:
					return "Insert";
				case  2:
					return "Update";
				case 3:
					return "Delete";

				default:
					throw new InvalidEnumArgumentException(string.Format("Database operation {0} is unknown!", operation));
			}
		}

		private CassandraTableChange MapCassandraTableChange(Row cassandraTableChangeRow)
		{
			return new CassandraTableChange
			{
				Id = cassandraTableChangeRow.GetValue<Guid>("id"),
				ChangeId = cassandraTableChangeRow.GetValue<string>("changeid"),
				TableId = cassandraTableChangeRow.GetValue<string>("tableid"),
				TableName = cassandraTableChangeRow.GetValue<string>("tablename"),
				ChangeTimestamp = cassandraTableChangeRow.GetValue<DateTime>("changetimestamp"),
				User = cassandraTableChangeRow.GetValue<string>("dbuser"),
				ColumnId = cassandraTableChangeRow.GetValue<string>("columnid"),
				ColumnName = cassandraTableChangeRow.GetValue<string>("columnname"),
				PriorValue = cassandraTableChangeRow.GetValue<string>("priorvalue"),
				UpdatedValue = cassandraTableChangeRow.GetValue<string>("updatedvalue"),
				Operation = (byte)cassandraTableChangeRow.GetValue<sbyte>("operation")
			};
		}
	}
}
