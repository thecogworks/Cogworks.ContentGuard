(function () {
    'use strict';

    angular
        .module('umbraco.resources')
        .factory('BackOfficeContentBlocker.EditorManager.Resources', backOfficeContentBlockerResources);

    backOfficeContentBlockerResources.$inject = ['$http'];

    function backOfficeContentBlockerResources($http) {

        var service = {
            isPageBlocked: isPageBlocked
        };

        return service;

        function isPageBlocked() {
            return $http.get(API_ROOT + 'IndexEvents');
        }
    }
})();