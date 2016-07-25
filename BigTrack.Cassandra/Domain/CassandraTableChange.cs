using System;

namespace BigTrack.Cassandra.Domain
{
	public class CassandraTableChange
	{
		public Guid Id { get; set; }
		public string ChangeId { get; set; }
		public string TableId { get; set; }
		public string TableName { get; set; }
		public DateTime ChangeTimestamp { get; set; }
		public string User { get; set; }
		public string ColumnId { get; set; } 
		public string ColumnName { get; set; } 
		public string PriorValue { get; set; }
		public string UpdatedValue { get; set; }
		public byte Operation { get; set; }
	}
}