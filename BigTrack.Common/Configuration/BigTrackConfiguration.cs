using System.Collections.Generic;
using Newtonsoft.Json;

namespace BigTrack.Common.Configuration
{
	class BigTrackConfiguration
	{
		[JsonProperty("trackableDatabases")]
		public List<DatabaseConfiguration> DatabaseConfigurations { get; set; }
	}
}
