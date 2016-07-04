using System.Linq;
using System.Web.Http;
using BigTrack.Common.Configuration;
using BigTrack.Ui.Models;

namespace BigTrack.Ui.Controllers
{
	[RoutePrefix("configuration")]
    public class ConfigurationController : ApiController
	{
		private readonly ConfigurationManager configurationManager = ConfigurationManager.Instanse;

		[Route("")]
		public ConfigurationResponse GetConfiguration()
		{
			var configurations = configurationManager.GetDatabaseConfigurations();

			return new ConfigurationResponse
			{
				DatabaseConfigurations = configurations
					.Select(conf => new DatabaseConfigurationModel
					{
						Id = conf.Id,
						Name = conf.Name,
						DatabaseTypeName = conf.DialectDriver.DatabaseTypeName,
						CatalogTypeName = conf.DialectDriver.CatalogTypeName,
						IsUserTrackingSupported = conf.DialectDriver.UserTrackingSupported
					})
					.ToList()
			};
		}
	}
}
