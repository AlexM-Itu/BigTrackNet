using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BigTrack.Common.Configuration;
using BigTrack.Ui.Models;

namespace BigTrack.Ui.Controllers
{
	[RoutePrefix("database")]
    public class DatabaseController : ApiController
	{
		private readonly ConfigurationManager configurationManager = ConfigurationManager.Instanse;

		[Route("{databaseId}/tables")]
		public List<DatabaseTableListItemModel> GetTableList(string databaseId)
		{
			return configurationManager
				.GetDatabaseManagerByDatabaseId(databaseId)
				.GetDatabaseTables()
				.Select(table => new DatabaseTableListItemModel
				{
					Id = table.Id,
					Name = table.Name
				})
				.ToList();
		}

		[Route("{databaseId}/tables/{tableId}/columns")]
		public List<string> GetTableColumns(string databaseId, string tableId)
		{
			return configurationManager
				.GetDatabaseManagerByDatabaseId(databaseId)
				.GetTableColumns(tableId);
		}
	}
}
