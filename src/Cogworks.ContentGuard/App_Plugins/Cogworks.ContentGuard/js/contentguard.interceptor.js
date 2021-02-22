angular.module("umbraco.services").config([

    "$httpProvider",

    function ($httpProvider) {
        // See: https://dev.to/skttl/how-to-customize-searching-in-umbraco-list-views-1knk
        // See: https://skrift.io/issues/changing-backoffice-functionality-without-changing-core-code/

        $httpProvider.interceptors.push(function ($q, $injector) {
            return {
                'response': function(response) {
                    if (response.config.url.includes('/umbraco/backoffice/UmbracoApi/Content/PostSave')) {
                        if (response.status === 200) {
                            var eventsService = $injector.get('eventsService');
                            eventsService.emit('guard.ContentSave', response.data);
                        }
                    }

                    return response || $q.when(response);
                }
            };
        });
    }]);