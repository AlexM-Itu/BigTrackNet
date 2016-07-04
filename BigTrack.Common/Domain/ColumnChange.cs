namespace BigTrack.Common.Domain
{
	public class ColumnChange
	{
		public long Id { get; set; }
		public long TableChangeId { get; set; }
		public string ColumnName { get; set; }
		public string PriorValue { get; set; }
		public string UpdatedValue { get; set; }
	}
}