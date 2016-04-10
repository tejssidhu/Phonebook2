﻿var gulp = require('gulp');
var exec = require('child_process').exec;
var uglify = require('gulp-uglify');

gulp.task("bundle", function (callback) {
    exec("aurelia bundle", function (err) {
        callback(err);
    });
});

gulp.task("uglify", ["bundle"], function () {
    return gulp.src("wwwroot/appbundle.js")
            .pipe(uglify())
            .pipe(gulp.dest("wwwroot/"));
});

gulp.task("default", ["uglify"], function () {

});
