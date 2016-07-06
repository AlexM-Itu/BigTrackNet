using System;
using System.Collections.Generic;
using System.Linq;
using BigTrack.Cassandra.Configuration;
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
			}

		}

		public TableChange GetChangesetDetails(string changesetId)
		{
			throw new NotImplementedException();
		}
	}
}
