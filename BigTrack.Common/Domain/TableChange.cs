using System;
using System.Collections.Generic;

namespace BigTrack.Common.Domain
{
	public class TableChange
	{
		public long Id { get; set; }
		public int TableId { get; set; }
		public DateTime ChangeTimestamp { get; set; }
		public string User { get; set; }
		public byte OperationTypeId { get; set; }
		public OperationType OperationType { get; set; }
		public List<ColumnChange> ColumnChanges { get; set; }
		public Table Table { get; set; }
	}
}