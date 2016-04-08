define(['plugins/router', 'durandal/app', 'managers/userManager'], function (router, app, userManager) {
	
	function activate() {
		console.log('shell activate');
	}

	return {
		router: router,
		userIsLoggedOn: userManager.isLoggedOn,
		activiate: activate
	};
});