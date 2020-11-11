angular.module('umbraco').controller('BackOfficeContentBlocker.EditorManager.ContentEditController',
    ['$scope', '$controller','$window',
        function ($scope, $controller, $window) {
        // inherit core content edit controller
        angular.extend(this, $controller('Umbraco.Editors.Content.EditController', { $scope: $scope }));

        $scope.reload = function () {
            $window.location.reload(true);
        };
    }]);