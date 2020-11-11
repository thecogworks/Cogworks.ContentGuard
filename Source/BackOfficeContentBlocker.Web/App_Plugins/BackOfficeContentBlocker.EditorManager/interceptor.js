angular.module('umbraco.services').config([
    '$httpProvider',
    function ($httpProvider) {

        $httpProvider.interceptors.push(['$q', '$injector', function ($q, $injector) {

            var resources = $injector.get('BackOfficeContentBlocker.EditorManager.Resources');
            var email = "";
            var pageId = 0;

            return {

                'response': function (response) {

                    if (response.config.url.indexOf("/umbraco/backoffice/UmbracoApi/Authentication/GetCurrentUser") === 0) {
                        email = response.data.email;
                    }

                    if (response.config.url.indexOf("umbraco/backoffice/UmbracoApi/Content/GetById") === 0) {
                        pageId = response.data.id;
                    }

                    return response;
                },

                'request': function (request) {

                    if (request.url.indexOf("views/content/edit.html") === 0) {

                        if (pageId !== 0 & email !== "" & resources.backOfficeContentBlockerResources.isPageBlocked(email, pageId) === 1) {
                            request.url = '/App_Plugins/BackOfficeContentBlocker.EditorManager/editor-manager.html';
                        }
                    }

                    return request || $q.when(request);
                }
            };
        }]);
    }]);