angular
	.module("BigTrack")
	.controller("changesetsController", [
		"configurationService", "$scope", function(configurationService, $scope) {

			$scope.changesetGridColumnsDefinition = [
				{ field: 'changesetTimestamp', displayName: 'Timestamp', width: "*" },
				{ field: 'operation', displayName: 'Operation', width: "20%" },
				{ field: 'user', displayName: 'User', width: "20%" },
				{ field: 'columns', displayName: 'Columns', width: "*" }
			];

			$scope.changesets = [
				{
					changesetTimestamp: Date(),
					operation: 'Update',
					user: "Alex",
					columns: 'A, b, c, d'
				}, {
					changesetTimestamp: Date(),
					operation: 'Update',
					user: "Alex",
					columns: 'A, b, c, d'
				}, {
					changesetTimestamp: Date(),
					operation: 'Update',
					user: "Alex",
					columns: 'A, b, c, d'
				}, {
					changesetTimestamp: Date(),
					operation: 'Update',
					user: "Alex",
					columns: 'A, b, c, d'
				}, {
					changesetTimestamp: Date(),
					operation: 'Update',
					user: "Alex",
					columns: 'A, b, c, d'
				}, {
					changesetTimestamp: Date(),
					operation: 'Update',
					user: "Alex",
					columns: 'A, b, c, d'
				}, {
					changesetTimestamp: Date(),
					operation: 'Update',
					user: "Alex",
					columns: 'A, b, c, d'
				}, {
					changesetTimestamp: Date(),
					operation: 'Update',
					user: "Alex",
					columns: 'A, b, c, d'
				}
			];
		}
	]);
