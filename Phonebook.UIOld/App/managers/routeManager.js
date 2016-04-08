define(['durandal/app', 'plugins/router', 'managers/userManager'],
	function (app, router, userManager) {
		var redirectRoute, navigateBack, currentShellName;

		userManager.on(userManager.loggedOnEvent, loggedOnHandler);
		userManager.on(userManager.loggedOffEvent, loggedOffHandler);
		userManager.on(userManager.unAuthorisedEvent, unAuthorizedHandler);

		console.log("after userManager.on started");

		function loggedOffHandler() {
			redirectRoute = ''
			router.navigate('#logon');
		}

		function unAuthorizedHandler() {
			router.navigate('#logon');
		}

		//TODO: read up on router.navigate
		function loggedOnHandler() {
			if (redirectRoute) {
				router.navigate(redirectRoute, { replace: true, trigger: true });
			} else {
				router.navigate('#/', { replace: true, trigger: true });
			}
		}

		function initRoutes() {
			console.log("init routes started");
			var routes = [
				{ route: '', title: 'Home', moduleId: 'viewmodels/home', nav: true },
				{ route: 'logon', title: 'Logon', moduleId: 'viewmodels/logon', nav: true }
			];

			router.map(routes);

			router.activate();

			//TODO: read up on router:navigation:complete
			router.on('router:navigation:complete', function (instance, instruction) {
				console.log('routerManager router.on');
				setShell(instruction);

				$('#applicationHost').show();
			});

			//TODO: read up on guardRoute and instance, instruction
			router.guardRoute = function (instance, instruction) {
				setShell(instruction);

				if (instruction.fragment === 'logon') {
					return !userManager.isLoggedOn() || '#/';
				} else {
					return userManager.isLoggedOn() ? setRedirectRoute(instruction, true) : setRedirectRoute(instruction);
				}
			};
		}

		navigateBack = router.navigateBack;
		router.navigateBack = function () {
			navigateBack();
			router.explicitNavigation = true;
		};

		function setRedirectRoute(instruction, allowRoute) {
			console.log('routeManager setRedirectRoute');

			setShell(instruction);

			redirectRoute = '#/' + instruction.fragment;
			if (instruction.queryString) {
				redirectRoute = redirectRoute + '?' + instruction.queryString;
			}
			return allowRoute ? true : '#/logon';
		}

		function setShell(instruction) {
			var shellName = instruction.config.shellName || '';
			if (currentShellName !== shellName) {
				currentShellName = shellName;
				if (shellName.length > 0)
					app.setRoot('viewmodels/' + shellName + 'Shell');
				else
					app.setRoot('viewmodels/shell');
			}
		}

		return {
			initRoutes: initRoutes,
			setShell: setShell
		};
	});