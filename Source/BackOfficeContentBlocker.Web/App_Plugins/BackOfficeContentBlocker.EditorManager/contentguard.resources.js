(() => {
    'use strict';

    function backOfficeContentBlockerResources($http, umbRequestHelper) {

        //const urlBase = Umbraco.Sys.ServerVariables.workflow.apiBasePath + '/groups';
        const apiRoot = '/umbraco/backoffice/ContentBlocker/BackOfficeContentBlockerApi/';

        const request = (method, url, data) =>
            umbRequestHelper.resourcePromise(
                method === 'DELETE' ? $http.delete(url)
                    : method === 'POST' ? $http.post(url, data)
                        : method === 'PUT' ? $http.put(url, data)
                            : $http.get(url),
                'Something broke'
            );

        const service = {
            isPageBlocked: () => request('GET', apiRoot + 'IsPageBlocked'),
            lockPage: () => request('GET', apiRoot + 'LockPage'),
            removeLock: () => request('GET', apiRoot + 'RemoveLock')
        };

        return service;
    }


    angular.module('umbraco.services')
        .factory('BackOfficeContentBlocker.EditorManager.Resources', ['$http', 'umbRequestHelper', backOfficeContentBlockerResources]);

})();