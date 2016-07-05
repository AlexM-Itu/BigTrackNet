using System;
using System.Collections.Generic;
using System.Linq;
using BigTrack.Cassandra.Domain;
using BigTrack.Common.Database;
using BigTrack.Common.Domain;
using Cassandra;
using Cassandra.Mapping;

namespace BigTrack.Cassandra.Database
{
	public class CassandraDatabaseManager : IDatabaseManager
	{
		private Cluster cluster;
		private string connectionString;

		public void SetConnectionString(string connectionString)
		{
			this.connectionString = connectionString;
			cluster = Cluster.Builder().AddContactPoint(connectionString).Build();
		}

		private ISession GetSession()
		{
			return cluster.Connect(connectionString);
		}

		public List<Table> GetDatabaseTables()
		{
			using (var session = GetSession())
			{
				return session
					.Execute("SELECT name FROM TableNames")
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
				var query = session.Prepare("select  * from TableChanges where tableName = ?"); // todo replace*

				return session
					.Execute(query.Bind(tableId))
					.Select(row => row.GetValue<string>("name"))
					.ToList();
			}
		}

		public List<TableChange> FindChangesets(string tableId, ChangeSearchOptions searchOptions)
		{
			throw new System.NotImplementedException();
		}

		public TableChange GetChangesetDetails(string changesetId)
		{
			throw new System.NotImplementedException();
		}
	}
}
