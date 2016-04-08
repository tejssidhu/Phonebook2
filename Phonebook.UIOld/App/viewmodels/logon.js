define(['dataservices/authenticationService'],
	function (authenticationService) {
		var inProgress = ko.observable(false),
			errorMessage = ko.observable();

		authenticationService.on('authenticationService:logonSuccess', logonSuccess);
		authenticationService.on('authenticationService:logonFail', logonFail);

		function submit(formElement) {
			var str;
			if (!formElement.UserName.value) {
				errorMessage("UserName and Password are required");
			}  else if (!formElement.Password.value) {
				errorMessage("UserName and Password are required");
			} else {
				//str = $(formElement).serialize();
				errorMessage('');

				inProgress(true);
				authenticationService.logon(formElement.UserName.value, formElement.Password.value);
			}
		}

		function logonSuccess() {
			inProgress(false);
		}

		function logonFail(error) {
			inProgress(false);

			var errorMsg = error.Message ? error.Message : error;

			errorMessage(errorMsg);
		}

		return {
			errorMessage: errorMessage,
			inProgress: inProgress,
			submit: submit
		};
	});