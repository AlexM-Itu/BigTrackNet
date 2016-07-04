using System.Security.AccessControl;

namespace BigTrack.Cassandra.Domain
{
	public class ColumnChange
	{
		public string ColumnsName { get; set; } // wierd
		public string PriorValue { get; set; }
		public string UpdatedValue { get; set; }
	}
}
