using BigTrack.Common.Database;

namespace BigTrack.Common.Configuration
{
	internal class DatabaseConfiguration
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string ConnectionString { get; set; }
		public string DialectDriverName { get; set; }
		public IDialectDriver DialectDriver { get; set; }
	}
}