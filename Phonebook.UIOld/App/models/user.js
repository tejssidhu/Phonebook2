define(function () {

	function User(userId) {
		this.userId = ko.observable(userId);
	}

	return User;
});