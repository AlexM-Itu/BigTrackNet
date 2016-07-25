var CONFIG = {
	serverName: "http://localhost:16261/"
};

angular.module('BigTrack', ['ngRoute', 'ui.grid', 'ui.grid.selection', 'isteven-multi-select']);

angular.module('BigTrack').config(['$routeProvider', function($routeProvider) {
	$routeProvider
		.when('/',
		{
			templateUrl: '/views/home.html'
		})
		.when('/changesets/:databaseId/:tableId',
		{
			controller: 'changesetsController',
			templateUrl: '/views/changesets.html'
		})
		//.when('/detail',
		//{
		//	controller: 'carDetailController',
		//	templateUrl: '/views/detail.html'
		//})
		//.when('/favourites',
		//{
		//	controller: 'favouritesController',
		//	templateUrl: '/views/favourites.html'
		//})
		.otherwise({ redirectTo: '/' });
}]);