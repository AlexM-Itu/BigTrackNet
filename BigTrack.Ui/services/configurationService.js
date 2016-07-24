angular
	.module("BigTrack")
	.service("configurationService", ["$http", function($http) {
		this.getDatabaseConiguration = function () {
			return $http.get(CONFIG.serverName + "/configuration");
		};

		this.getTables = function(databaseId) {
			return $http.get(CONFIG.serverName + "/database/" + databaseId + "/tables");
		}
	}
]);