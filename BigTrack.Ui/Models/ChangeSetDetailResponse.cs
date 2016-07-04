using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace BigTrack.Ui.Models
{
	public class ChangeSetDetailResponse
	{
		public Dictionary<string, string> PriorValues { get; set; }
		public Dictionary<string, string> UpdatedValues { get; set; }
		public string Operation { get; set; } //todo change to enum
		public DateTime ChangeTimestamp { get; set; }
	}
}