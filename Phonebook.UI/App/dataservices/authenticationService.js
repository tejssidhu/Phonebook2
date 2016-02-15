define(['durandal/events'], function (events) {
	
	//TODO: add ajax stuff
	var service = {
		logon: logon,
		logOff: logOff,
		unAuthorised: unAuthorised
	}

	function unAuthorised() {
		service.trigger('authenticationService:unAuthorised');
	}

	function logon(username, password) {
		var ajaxOptions = {
			url: 'http://localhost/api/api/logon/logon',
			type: 'POST',
			headers: {
				"Authorization": "Basic " + btoa(username + ":" + password)
			}
		};

		//data: options

		return $.ajax(ajaxOptions)
			.done(logonSuccess)
			.fail(logonFail);

		logonSuccess();
	}

	function getAuthHash(user, pass) {
		var tok = user + ':' + pass;
		var hash = Base64.encode(tok);
		return "Basic " + hash;
	}

	function logonSuccess(data) {
		service.trigger('authenticationService:logonSuccess', data);
	}

	function logonFail(jqXHR) {
		var error = jqXHR.responseText ? JSON.parse(jqXHR.responseText) : jqXHR.statusText;
		service.trigger('authenticationService:logonFail', error);
	}

	function logOff() {
		logoffComplete();
	}

	function logoffComplete(data) {
		service.trigger('authenticationService:logoffSuccess', data);
	}

	

	events.includeIn(service);

	return service;

});