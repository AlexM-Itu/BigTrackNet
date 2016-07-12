using System;
using System.Collections.Generic;
using System.Linq;
using BigTrack.Cassandra.Configuration;
using BigTrack.Cassandra.Domain;
using BigTrack.Common.Database;
using BigTrack.Common.Domain;
using Cassandra;
using Newtonsoft.Json;
using ColumnChange = BigTrack.Cassandra.Domain.ColumnChange;

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
					.Execute("SELECT \"name\" FROM \"TableNames\"")
					.Select(row => new Table
					{
						Id = row.GetValue<string>("name"),
						Name = row.GetValue<string>("name")
					})
					.ToList();
			}
		}
		
		public List<string> GetTableColumns(string tableId)
		{
			using (var session = GetSession())
			{
				var query = session.Prepare("select \"columnName\" from \"TableColumns\" where \"tableId\" = ?"); // todo replace*

				return session
					.Execute(query.Bind(tableId))
					.Select(row => row.GetValue<string>("columnName"))
					.ToList();
			}
		}

		public List<TableChange> FindChangesets(string tableId, ChangeSearchOptions searchOptions)
		{
			using (var session = GetSession())
			{
				var query = "select  * from TableChanges where tableName = tableId ";
				if (searchOptions.FromDate.HasValue)
					query += "and timestamp >= " + searchOptions.FromDate;

				if (searchOptions.ToDate.HasValue)
					query += "and timestamp <= " + searchOptions.ToDate;

				if (searchOptions.User != null)
					query += " and dbUser = " + searchOptions.User;
				throw new NotImplementedException();
				//if (searchOptions.AffectedColumns != null)
				//{
				//	foreach (var column in searchOptions.AffectedColumns)
				//	{
				//		query += "and "
				//	}
				//}

				//return session
				//	.Execute(query)
					//.Select(row=> new TableChange
					//{
					//	//Id = row.GetValue<string>("id"),
					//	//ChangeTimestamp = row.GetValue<DateTime>("timestamp"),
					//	//User = row.GetValue<string>("dbUser"),
					//	//Table = new Table
					//	//{
					//	//	Id = row.GetValue<string>("tableName"),
					//	//	Name = row.GetValue<string>("tableName"),
					//	//},
					//	//TableId = row.GetValue<string>("tableName"),,
					//	//OperationType = new OperationType
					//	//{
					//	//	Name = row.GetValue<string>("operation"),
					//	//},
					//	//ColumnChanges = row.GetValue<cas>()


					//})
					//.Select(row=> row.GetValue<CassandraTableChange>())
			}

		}

		public TableChange GetChangesetDetails(string changesetId)
		{
			using (var session = GetSession())
			{
				session.UserDefinedTypes.Define(UdtMap.For<ColumnChange>("ColumnChange")); // todo Cassandra lowercases the name
				var query = session.Prepare("SELECT * FROM \"TableChanges\" where \"id\"=?");
					
				var result = session.Execute(query.Bind(changesetId)).FirstOrDefault();

				if (result == null)
					return null;

				return new TableChange
				{
					Id = result.GetValue<string>("id"),
					Table = new Table
					{
						Id = result.GetValue<string>("tableName"),
						Name = result.GetValue<string>("tableName"),
					},
					TableId = result.GetValue<string>("tableName"),
					User = result.GetValue<string>("dbUser"),
					OperationType = new OperationType
					{
						Name = result.GetValue<string>("operation"),
					},
					ChangeTimestamp = result.GetValue<DateTime>("timestamp"),
					ColumnChanges = result
						.GetValue<HashSet<ColumnChange>>("columnChanges")
						.Select(change=> new Common.Domain.ColumnChange
						{
							ColumnName = change.ColumnsName,
							PriorValue = change.PriorValue,
							UpdatedValue = change.UpdatedValue
						})
						.ToList()
				};
			}
		}
	}
}
