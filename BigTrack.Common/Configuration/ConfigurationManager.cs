using System.Collections.Generic;

namespace BigTrack.Common.Configuration
{
	public class ConfigurationManager
	{
		private ConfigurationManager() { }
		private static readonly object locker = new object();
		private static ConfigurationManager instanse;

		public static ConfigurationManager Instanse
		{
			get
			{
				if (instanse == null)
					lock (locker)
					{
						if (instanse == null)
							instanse = new ConfigurationManager();
					}

				return instanse;
			}
		}

		public List<DatabaseConfiguration> GetDatabaseConfigurations()
		{
			throw new System.NotImplementedException();
		}
	}
}
