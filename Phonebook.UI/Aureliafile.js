var aurelia = require("aurelia-cli");

aurelia.command("bundle", {
    js: {
        "appbundle": {
            modules: [
                //"app", "main", "about/**", "movies/**", "resources/**",
                "aurelia-bootstrapper",
                "aurelia-framework",
                "aurelia-router",
                "aurelia-http-client",
                "aurelia-validation"
            ],
            options: {
                inject: true,
                minify: true
            }
        }
    },
    template: {
        "appbundle": {
            pattern: ['movies/*.html', 'about/*.html'],
            options: {
                inject: true
            }
        }
    }
});