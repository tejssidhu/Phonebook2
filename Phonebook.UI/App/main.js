requirejs.config({
	paths: {
		'text': '../Scripts/text',
		'durandal': '../Scripts/durandal',
		'plugins': '../Scripts/durandal/plugins',
		'transitions': '../Scripts/durandal/transitions',
		'jquery': '../Scripts/jquery-1.9.1',
		'knockout': '../Scripts/knockout-2.3.0'
	}
});

define('jquery', function() { return jQuery; });
define('knockout', ko);

define(['durandal/system', 'durandal/app', 'durandal/viewLocator', 'loader'], function (system, app, viewLocator, loader) {
	system.debug(true);

	app.title = 'Phonebook App - Durandal';

	app.configurePlugins({
		router: true,
		dialog: true,
		widget: true,
		observable: true
	});

	app.start().then(function () {
		viewLocator.useConvention();
		loader.init();
		system.log("app started");

		require(['managers/routeManager'], function (routeManager) {
			routeManager.initRoutes();
		});
	});
});