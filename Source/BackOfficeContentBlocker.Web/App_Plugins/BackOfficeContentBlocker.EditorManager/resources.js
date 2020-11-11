(function () {
    'use strict';

    angular
        .module('umbraco.resources')
        .factory('BackOfficeContentBlocker.EditorManager.Resources', backOfficeContentBlockerResources);

    backOfficeContentBlockerResources.$inject = ['$http'];

    var apiRoot = '/umbraco/backoffice/ContentBlocker/BackOfficeContentBlockerApi/';

    function backOfficeContentBlockerResources($http) {

        var service = {
            isPageBlocked: isPageBlocked,
            lockPage: lockPage
        };

        return service;

        function isPageBlocked() {
            return $http.get(apiRoot + 'IsPageBlocked');
        }

        function lockPage() {
            return $http.get(apiRoot + 'LockPage');
        }
    }
})();