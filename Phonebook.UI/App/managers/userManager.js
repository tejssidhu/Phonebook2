define(['durandal/events', 'models/user', 'dataservices/authenticationService'],
	function (events, User, authenticationService) {
		var isLoggedOn,
			user,
			manager = {
				init: init,
				loggedOnEvent: 'userManager:loggedOn',
				loggedOffEvent: 'userManager:loggedOff',
				unAuthorisedEvent: 'userManager:unAuthorised',
			};

		console.log("before userManager started");

		function init() {
			console.log("userManager init started");
			var mss = window.mss || {};
			var userId = (mss.userDetails && mss.userDetails.userId) ? mss.userDetails.userId() : '';

			manager.userDetails = user = new User(userId)
			manager.isLoggedOn = isLoggedOn = ko.observable(mss.loggedOn() || false);

			events.includeIn(manager);

			authenticationService.on('authenticationService:logonSuccess', loggedOnHandler);
			authenticationService.on('authenticationService:logoffSuccess', loggedOffHandler);
			authenticationService.on('authenticationService:unAuthorised', unAuthorisedHandler);

			if (isLoggedOn()) {
				$(document.docmentElement).addClass('logged-on');
			}
		}

		function clearUserInfo() {
			user.UserId('');
			mss.user.userId('');
		}

		function unAuthorisedHandler() {
			clearUserInfo();
			isLoggedOn(false);
			manager.trigger(manager.unAuthorisedEvent);
			$('#applicationHost').hide();
			$(document.docmentElement).removeClass('logged-on');
		}

		function loggedOffHandler(data) {
			clearUserInfo();
			if (data && data.forceRefresh) {
				document.location.reload(true);
			} else {
				isLoggedOn(false);
				manager.trigger(manager.loggedOffEvent);
				$('#applicationHost').hide();
				$(document.docmentElement).removeClass('logged-on');
			};
		}

		function loggedOnHandler(data) {
			mss.userDetails.userId(data.Id);
			user.userId(data.Id);
			isLoggedOn(true);
			manager.trigger(manager.loggedOnEvent);
			$(document.docmentElement).addClass('logged-on');
		}

		return manager;
	});