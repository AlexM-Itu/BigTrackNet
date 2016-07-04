using System;
using System.Collections.Generic;

namespace BigTrack.Ui.Models
{
	public class ChangeSetListItemModel
	{
		public DateTime ChangesetTimestamp { get; set; }
		public string User { get; set; }
		public List<String> Columns { get; set; }
		public string Operation { get; set; } // toto change to enum
	}
}