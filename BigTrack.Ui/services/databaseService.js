angular
	.module("BigTrack")
	.service("databaseService", ["$http", function($http) {
	this.getTableColumns = function(databaseId, tableId) {
		return $http.get(CONFIG.serverName + "database/" + databaseId + "/tables/" + tableId + "/columns");
	};
}]);