angular
	.module("BigTrack")
	.service("databaseService", ["$http", function($http) {
	this.getTableColumns = function(databaseId, tableId) {
		return $http.get(CONFIG.serverName + "database/" + databaseId + "/tables/" + tableId + "/columns");
	};
	this.getChangesets = function(databaseId, tableId, fromDate, toDate, affectedcolumns, dbUser, offset, count) {
		return $http.post(CONFIG.serverName + "database/" + databaseId + "/tables/" + tableId + "/changesets", {
			offset: offset,
			count: count,
			fromDate: fromDate,
			toDate: toDate,
			user: dbUser,
			affectedColumns: affectedcolumns
		});
	};
	this.getChangeset = function(databaseId, changesetId) {
		return $http.get(CONFIG.serverName + "database/" + databaseId + "/changeset/" + changesetId);
	};
}]);