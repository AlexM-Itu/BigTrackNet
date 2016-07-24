angular
	.module("BigTrack")
	.controller("navBarController", ["configurationService", "$scope", function($scope) {
		var getConfiguration = function() {
			configurationService.getDatabaseConiguration().then(function(response) {
				$scope.databases = response.data.databaseConfigurations;
			});
		};

		$scope.getTables = function(databaseId) {
			configurationService.getTables(databaseId);
		};

		//getConfiguration();
	}
]);