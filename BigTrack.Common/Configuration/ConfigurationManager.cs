using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BigTrack.Common.Database;
using Newtonsoft.Json;

namespace BigTrack.Common.Configuration
{
	public class ConfigurationManager
	{
		private static readonly object locker = new object();
		private static ConfigurationManager instanse;

		private readonly string configurationFileLocation = System.Configuration.ConfigurationManager.AppSettings["BigTrackConfigurationFileLocation"];
		private const string configurationFilename = "BigTrack.json";
		private BigTrackConfiguration bigTrackConfiguration;

		private ConfigurationManager()
		{
			bigTrackConfiguration = JsonConvert.DeserializeObject<BigTrackConfiguration>(File.ReadAllText(string.Format("{0}/{1}", configurationFileLocation, configurationFilename)));
			foreach (var databaseConfiguration in bigTrackConfiguration.DatabaseConfigurations)
			{
				databaseConfiguration.DialectDriver = (IDialectDriver)Activator.CreateInstanceFrom(databaseConfiguration.DialectDriverAssemblyName, databaseConfiguration.DialectDriverName).Unwrap();
				databaseConfiguration.DialectDriver.DatabaseManager.SetConnectionString(databaseConfiguration.ConnectionString);
			}
		}

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
			return bigTrackConfiguration.DatabaseConfigurations;
		}

		public IDatabaseManager GetDatabaseManagerByDatabaseId(string databaseId)
		{
			return bigTrackConfiguration
				.DatabaseConfigurations
				.FirstOrDefault(conf => conf.Id == databaseId)
				.DialectDriver
				.DatabaseManager;
		}
	}
}
