(() => {

    function contentGuardService($http, umbRequestHelper) {

        var apiRoot = '/umbraco/backoffice/ContentGuard/ContentGuardApi/';

        var request = (method, url, data) =>
            umbRequestHelper.resourcePromise(
                method === 'DELETE' ? $http.delete(url)
                    : method === 'POST' ? $http.post(url, data)
                        : method === 'PUT' ? $http.put(url, data)
                            : $http.get(url),
                'Something broke'
            );

        const service = {
            isPageLocked: (pageId) => request('GET', apiRoot + 'IsLocked?pageId=' + pageId),
            lockPage: (pageId, ownerUsername) => request('GET', apiRoot + 'Lock?pageId=' + pageId + '&ownerUsername=' + ownerUsername),
            unlockPage: (pageId) => request('GET', apiRoot + 'Unlock?pageId=' + pageId)
        };

        return service;
    }

    angular.module("umbraco.services")
        .factory("contentGuardService", contentGuardService);

})();