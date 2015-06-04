/// <reference path="views/shared/_layout.cshtml" />
module.exports = function (grunt) {
    var paths = {
        root: 'wwwroot',
        js: 'wwwroot/scripts',
        css: 'wwwroot/styles',
        fonts: 'wwwroot/fonts'
    };
    grunt.loadNpmTasks('grunt-contrib-concat');
    //grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-bower');
    grunt.loadNpmTasks('grunt-angular-templates');
    grunt.loadNpmTasks('grunt-include-source');
    grunt.initConfig({
        bower: {
            dev: {
                dest: paths.root,
                js_dest: paths.js,
                css_dest: paths.css,
                fonts_dest: paths.fonts,
                options: {
                    keepExpandedHierarchy: false
                }
            }
        },
        concat: {
            files: {
                src: ['scripts/**/*.js'],
                dest: paths.js + '/app.js'
            }
        },
        ngtemplates: {
            app: {
                src: ['scripts/**/**.html'],
                dest: paths.js + '/app-templates.js',
                options: {
                    module: 'appPDU',
                    htmlmin: { collapseWhitespace: true, collapseBooleanAttributes: true }
                }
            }
        },
        includeSource: {
            layout: {
                files: {
                    'Views/Shared/_Layout.cshtml': 'Views/Shared/_Layout.cshtml'
                }
            },
            options: {
                basePath: paths.js,
                baseUrl: '~/scripts/',
            }
        }
    })
    grunt.registerTask('default', ['bower:dev', 'concat', 'ngtemplates'/*, 'includeSource'*/]);
};