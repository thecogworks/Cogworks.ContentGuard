(function () {
    "use strict";

    angular.module("umbraco")
        .controller("Cogworks.Guard.Controller",
            function ($scope, editorState, contentGuardService) {
                var vm = this;

                vm.buttonState = "init";
                vm.clickCommand = clickCommand;
                vm.notification = "";

                function clickCommand() {
                    vm.buttonState = "busy";
                    var pageId = editorState.current.id;

                    contentGuardService.unlockPage(pageId)
                        .then(function () {
                            vm.buttonState = "success";
                            vm.notification = "Page successfully unlocked. You will be redirected to root page in a moment.";

                            setTimeout(function () {
                                window.location.href = "/umbraco#/content";
                            }, 2500);
                        }, function (error) {
                                vm.buttonState = "error";
                                vm.notification = "There was a problem unlocking this page. Please try again.";

                                console.log("Unlocking page failed", error);
                        });
                }
            });
})();