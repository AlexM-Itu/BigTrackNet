angular
	.module("BigTrack")
	.controller("navBarController", ["configurationService", "$scope", "$window", function (configurationService, $scope, $window) {
		var getConfiguration = function() {
			configurationService.getDatabaseConiguration().then(function(response) {
				$scope.databases = response.data.databaseConfigurations;
			});
		};

		$scope.getTables = function(databaseId) {
			if (databaseId)
				configurationService.getTables(databaseId).then(function(response) {
					$scope.databaseTables = response.data;
					$scope.isTableSelectorVisible = true;
				});
		};

		$scope.goToTablePage = function(databaseId, tableId) {
			window.location.hash = "#/changesets/" + databaseId + "/" + tableId;
		};

		getConfiguration();
	}
]);