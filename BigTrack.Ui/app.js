var CONFIG = {
	serverName: "."
};

angular.module('BigTrack', ['ngRoute', 'ui.grid', 'isteven-multi-select']);

angular.module('BigTrack').config(['$routeProvider', function($routeProvider) {
	$routeProvider
		.when('/',
		{
			templateUrl: '/views/home.html'
		})
		.when('/changesets',
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