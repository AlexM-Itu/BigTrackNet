using BigTrack.Cassandra.Database;
using BigTrack.Common.Database;

namespace BigTrack.Cassandra.Configuration
{
	public class CassandraDialectDriver : IDialectDriver
	{
		private readonly IDatabaseManager cassandraDatabaseManager = new CassandraDatabaseManager();
		public string DatabaseTypeName
		{
			get { return "Cassandra"; }
		}

		public string CatalogTypeName
		{
			get { return "table";  }
		}

		public bool UserTrackingSupported
		{
			get { return false; } // todo elaborate later
		}

		public IDatabaseManager DatabaseManager
		{
			get { return cassandraDatabaseManager;}
		}
	}
}
