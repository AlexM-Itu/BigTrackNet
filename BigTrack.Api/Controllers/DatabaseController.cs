using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BigTrack.Api.Models;
using BigTrack.Common.Configuration;
using BigTrack.Common.Database;
using BigTrack.Common.Domain;

namespace BigTrack.Api.Controllers
{
	[RoutePrefix("database")]
    public class DatabaseController : ApiController
	{
		private readonly ConfigurationManager configurationManager = ConfigurationManager.Instanse;

		[Route("{databaseId}/tables")]
		[HttpGet]
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
		[HttpGet]
		public List<Column> GetTableColumns(string databaseId, string tableId)
		{
			return configurationManager
				.GetDatabaseManagerByDatabaseId(databaseId)
				.GetTableColumns(tableId);
		}

		[Route("{databaseId}/tables/{tableId}/changesets")]
		[HttpPost]
		public List<ChangeSetListItemModel> FinChangesets(string databaseId, string tableId, [FromBody]ChangesetRequest searchOptions)
		{
			return configurationManager
				.GetDatabaseManagerByDatabaseId(databaseId)
				.FindChangesets(tableId, new ChangeSearchOptions
				{
					Offset = searchOptions.Offset,
					Count = searchOptions.Count,
					FromDate = searchOptions.FromDate,
					ToDate = searchOptions.ToDate,
					User = searchOptions.User,
					AffectedColumns = searchOptions.AffectedColumns
				})
				.Select(changeset => new ChangeSetListItemModel
				{
					ChangeId = changeset.Id,
					ChangesetTimestamp = changeset.ChangeTimestamp,
					User = changeset.User,
					Columns = changeset.ColumnChanges.Select(col => col.ColumnName).ToList(),
					Operation = changeset.OperationType.Name,
				})
				.ToList();
		}

		[Route("{databaseId}/changeset/{changesetId}")]
		public ChangeSetDetailResponse GetChangesetDetails(string databaseId, string changesetId)
		{
			var result = configurationManager
				.GetDatabaseManagerByDatabaseId(databaseId)
				.GetChangesetDetails(changesetId);

			if (result == null)
				return null;

			return new ChangeSetDetailResponse
			{
				ChangeTimestamp = result.ChangeTimestamp,
				Operation = result.OperationType.Name,
				PriorValues = result.ColumnChanges.Select(ch=> new KeyValuePair<string, string> (ch.ColumnName, ch.PriorValue)).ToList(),
				UpdatedValues = result.ColumnChanges.Select(ch=> new KeyValuePair<string, string>(ch.ColumnName, ch.UpdatedValue)).ToList()
			};
		}
	}
}
