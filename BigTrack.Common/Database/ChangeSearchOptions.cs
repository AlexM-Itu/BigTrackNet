using System;
using System.Collections.Generic;

namespace BigTrack.Common.Database
{
	public class ChangeSearchOptions
	{
		public int Offset { get; set; }
		public int Count { get; set; }
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }
		public string User { get; set; }
		public List<string> AffectedColumns { get; set; }
	}
}