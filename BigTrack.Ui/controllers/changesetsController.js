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

		$scope.tableColumns = [
			{
				id: 1,
				name: 'Col1'
			}, {
				id: 2,
				name: 'Col2'
			}, {
				id: 3,
				name: 'Col3'
			}, {
				id: 4,
				name: 'Col4'
			}, {
				id: 5,
				name: 'Col5'
			}, {
				id: 6,
				name: 'Col6'
			}, {
				id: 7,
				name: 'Col7'
			}, {
				id: 8,
				name: 'Col8'
			}, {
				id: 9,
				name: 'Col9'
			}, {
				id: 10,
				name: 'Col10'
			}, {
				id: 11,
				name: 'Col11'
			}, {
				id: 12,
				name: 'Col12'
			}
		];

		$scope.sampleChengesetDetail = {
			priorValues: [
				{ key: 'col1', value: 'val1' },
				{
					key: 'col2',
					value: 'val2'
				},
				{ key: 'col3', value: 'val3' },
				{ key: 'col4', value: 'val4' },
				{ key: 'col4', value: 'val4' },
				{ key: 'col4', value: 'val4' },
				{ key: 'col4', value: 'val4' },
				{ key: 'col4', value: 'val4' },
				{ key: 'col4', value: 'val4' },
				{ key: 'col4', value: 'val4' },
				{ key: 'col4', value: 'val4' }
			],
			updatedValues: [
				{ key: 'col1', value: 'new val1' },
				{
					key: 'col2',
					value: 'new val2'
				},
				{ key: 'col3', value: 'new val3' },
				{ key: 'col4', value: 'val4' },
				{ key: 'col4', value: 'val4' },
				{ key: 'col4', value: 'val4' },
				{ key: 'col4', value: 'val4' },
				{ key: 'col4', value: 'val4' },
				{ key: 'col4', value: 'val4' },
				{ key: 'col4', value: 'val4' },
				{ key: 'col4', value: 'val4' }
			],
			operation: "Insert",
			changeTimestamp: Date()
		};
	}
	]);
