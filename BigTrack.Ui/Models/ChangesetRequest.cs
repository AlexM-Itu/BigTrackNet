using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Web.UI.WebControls;

namespace BigTrack.Ui.Models
{
	public class ChangesetRequest
	{
		public int Offset { get; set; }
		public int Count { get; set; }
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }
		public string User { get; set; }
		public List<string> AffectedColumns { get; set; }
	}
}