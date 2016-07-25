using System;
using System.Collections.Generic;

namespace BigTrack.Api.Models
{
	public class ChangeSetListItemModel
	{
		public string ChangeId { get; set; }
		public DateTime ChangesetTimestamp { get; set; }
		public string User { get; set; }
		public List<String> Columns { get; set; }
		public string Operation { get; set; } // toto change to enum
	}
}