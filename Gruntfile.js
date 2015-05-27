module.exports = function (grunt) {
    var paths = {
        bower: "./bower_components/",
        lib: "./" + project.webroot + "/js/"
    };
    	grunt.LoadNpmTasks("grunt-contrib-concat")
    	grunt.LoadNpmTasks("grunt-contrib-uglify")	
    	
    //	grunt.initConfig({
    //		concat:{
    //			files:[
    //				
    //			]
    //		}	
    //	});
};