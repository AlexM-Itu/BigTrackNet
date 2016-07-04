using System.Collections.Generic;
using BigTrack.Common.Domain;

namespace BigTrack.Common.Database
{
	public interface IDatabaseManager
	{
		void SetConnectionString(string connectionString);
		List<Table> GetDatabaseTables();
		List<string> GetTableColumns(string tableId);
		List<TableChange> FindChangesets(string tableId, ChangeSearchOptions searchOptions);
		TableChange GetChangesetDetails(string changesetId);
	}
}