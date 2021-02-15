angular.module('umbraco').controller('BackOfficeContentBlocker.EditorManager.ContentEditController',
    ['$scope', '$controller','$window',
        function ($scope, $controller, $window) {
        // inherit core content edit controller
            angular.extend(this, $controller('Umbraco.Editors.Content.EditController', { $scope: $scope }));

            // this needs to be the email address of the user who is editing the page
            var userCurrentlyEditing = "";

        $scope.reload = function () {
            $window.location.reload(true);
        };
    }]);