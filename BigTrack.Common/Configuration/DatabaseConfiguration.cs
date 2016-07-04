using BigTrack.Common.Database;
using Newtonsoft.Json;

namespace BigTrack.Common.Configuration
{
	public class DatabaseConfiguration
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("connectionStrin")]
		public string ConnectionString { get; set; }

		[JsonProperty("dialectDriverAssemblyName")]
		public string DialectDriverAssemblyName { get; set; }

		[JsonProperty("dialectDriverName")]
		public string DialectDriverName { get; set; }

		public IDialectDriver DialectDriver { get; set; }
	}
}