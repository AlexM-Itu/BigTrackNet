using System;

namespace BigTrack.Cassandra.Domain
{
	public class CassandraTableChange
	{
		public Guid Id { get; set; }
		public Guid ChangeId { get; set; }
		public Guid TableId { get; set; }
		public string TableName { get; set; }
		public DateTime ChangeTimestamp { get; set; }
		public string User { get; set; }
		public Guid ColumnId { get; set; } 
		public string ColumnName { get; set; } 
		public string PriorValue { get; set; }
		public string UpdatedValue { get; set; }
		public byte Operation { get; set; }
	}
}