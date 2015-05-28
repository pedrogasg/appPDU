module.exports = function (grunt) {
    var paths = {
        root: 'wwwroot',
        js: 'wwwroot/scripts',
        css: 'wwwroot/styles'
    };
    grunt.loadNpmTasks('grunt-contrib-concat');
    //grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-bower');
    grunt.loadNpmTasks('grunt-angular-templates');
    grunt.initConfig({
        bower: {
            dev: {
                dest: paths.root,
                js_dest: paths.js,
                css_dest: paths.css
            }
        },
        concat: {
            files: {
                src: ['scripts/**/**.js'],
                dest: paths.js + '/app.js'
            }
        },
        ngtemplates: {
            app: {
                src: ['scripts/**/**.html'],
                dest: paths.js + '/templates.js',
                options: {
                    module:'appPDU',
                    htmlmin: { collapseWhitespace: true, collapseBooleanAttributes: true }
                }
            }
        }
    })
    grunt.registerTask('default', ['bower:dev', 'concat','ngtemplates']);
};