using System;
using System.Collections.Generic;

namespace BigTrack.Api.Models
{
	public class ChangeSetDetailResponse
	{
		public List<KeyValuePair<string, string>> PriorValues { get; set; }
		public List<KeyValuePair<string, string>> UpdatedValues { get; set; }
		public string Operation { get; set; } 
		public DateTime ChangeTimestamp { get; set; }
	}
}