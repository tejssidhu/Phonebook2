define(['require'], function(require) {
	var loader = {

		init: function() {
			if (typeof requirejs !== 'undefined') {
				var previousHandler = requirejs.onResourceLoad;
				requirejs.onResourceLoad = function(context, map, depArray) {
					if (previousHandler) {
						previousHandler(context, map, depArray);
					}
					loader.initializer(context.defined[map.id], map.id);
				};
			}
		},

		initializer: function(obj) {
			if (!obj) {
				return;
			}

			if (typeof obj === 'function') {
				return;
			}

			if (typeof obj === 'string') {
				return;
			}

			if (typeof obj.init === 'function') {
				obj.init();
			}

		}
	};

	return loader;
});