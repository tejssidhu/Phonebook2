define(['durandal/system', 'plugins/router'], function (system, router) {
	var vm = {};

	vm.activate = function (name) {
		system.log("** activate for contacts");
	};

	return vm;
});