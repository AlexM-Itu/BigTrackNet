using System.Collections.Generic;

namespace BigTrack.Common.Configuration
{
	public class ConfigurationManager
	{
		public static ConfigurationManager Instanse { get; set; }

		public List<DatabaseConfiguration> GetDatabaseConfigurations()
		{
			throw new System.NotImplementedException();
		}
	}
}
