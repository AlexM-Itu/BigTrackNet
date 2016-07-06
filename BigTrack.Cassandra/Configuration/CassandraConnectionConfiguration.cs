using Newtonsoft.Json;

namespace BigTrack.Cassandra.Configuration
{
	public class CassandraConnectionConfiguration
	{
		[JsonProperty("contactPoint")]
		public string ContactPoint { get; set; }

		[JsonProperty("keyspace")]
		public string Keyspace { get; set; }
	}
}
