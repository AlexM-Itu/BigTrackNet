using System;
using System.Collections.Generic;
using BigTrack.Common.Domain;

namespace BigTrack.Cassandra.Domain
{
	public class CassandraTableChange
	{
		public Guid Id { get; set; }
		public HashSet<ColumnChange> ColumnChanges{ get; set; }
		public string User { get; set; }
		public Operations Operation { get; set; }
		public string TableName { get; set; }
		public DateTime Timestamp { get; set; }
	}
}