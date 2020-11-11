angular.module('umbraco.services').config([
    '$httpProvider',
    function ($httpProvider) {

        $httpProvider.interceptors.push(['$q', function ($q) {

            var name = "";
            var email = "";
            var id = 0;

            return {

                'response': function (response) {

                    console.log("response.config.url: " + response.config.url);

                    if (response.config.url.indexOf("/umbraco/backoffice/UmbracoApi/Authentication/GetCurrentUser") === 0) {
                        name = response.data.name;
                        email = response.data.email;
                    }

                    if (response.config.url.indexOf("umbraco/backoffice/UmbracoApi/Content/GetById") === 0) {
                        id = response.data.id;
                    }

                    return response;
                },

                'request': function (request) {

                    console.log("request.url: " + request.url);

                    // Redirect any requests to built in content edit to our custom view
                    if (request.url.indexOf("views/content/edit.html") === 0) {
                        console.log("name: " + name);
                        console.log("email: " + email);
                        console.log("id: " + id);

                        request.url = '/App_Plugins/BackOfficeContentBlocker.EditorManager/editor-manager.html';
                    }

                    if (request.url.indexOf("umbraco/backoffice/UmbracoApi/Content/GetById") === 0) {
                        id = request.data.id;
                    }

                    if (id !== 0) {
                        request.url = '/App_Plugins/BackOfficeContentBlocker.EditorManager/editor-manager.html';
                    }

                    return request || $q.when(request);
                }
            };
        }]);
    }]);

//angular.module('umbraco.services').config([
//    '$httpProvider',
//    function ($httpProvider) {

//        $httpProvider.interceptors.push(function ($q) {
//            return {
//                'response': function (response) {

//                    console.log("response: " + response);

//                    return response;
//                }
//            };
//        });

//    }]);