using System.Collections.Generic;

namespace BigTrack.Api.Models
{
	public class ConfigurationResponse
	{
		public List<DatabaseConfigurationModel> DatabaseConfigurations { get; set; }
	}
}