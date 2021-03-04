(function () {
    "use strict";

    angular.module("umbraco")
        .controller("Cogworks.Guard.Controller",
            function ($scope, $rootScope, editorState, contentGuardService, overlayService, userService, eventsService) {

                var unsubscribeAppTabChange = eventsService.on("app.tabChange",
                    function(event, args) {
                        tryBlockContent();
                        event.preventDefault();
                    });

                var unsubscribeGuardContentSave = eventsService.on("guard.ContentSave",
                    function (event, args) {
                        if (args && args.isElement) {
                            return;
                        }

                        tryBlockContent(args.id);
                        event.preventDefault();
                    });

                $scope.$on("$destroy",
                    function() {
                        unsubscribeAppTabChange();
                        unsubscribeGuardContentSave();
                    });

                var vm = this;

                vm.buttonState = "init";
                vm.clickCommand = clickCommand;
                vm.notification = "";

                tryBlockContent();

                function clickCommand() {
                    vm.buttonState = "busy";

                    contentGuardService.unlockPage(editorState.current.id)
                        .then(function () {
                            vm.buttonState = "success";
                            vm.notification = "Page successfully unlocked. You will be redirected to root page in a moment.";

                            setTimeout(function () {
                                window.location.replace("/umbraco");
                            }, 2500);
                        }, function (error) {
                            vm.buttonState = "error";
                            vm.notification = "There was a problem unlocking this page. Please try again.";

                            console.log("Unlocking page failed", error);
                        });
                }

                function tryBlockContent(pageId = undefined) {
                    var currentTab = editorState.current.apps.find(x => x.active === true);
                    var tabAlias = currentTab.alias;

                    if (!(tabAlias === "umbContent" || tabAlias === "umbInfo")) {
                        return;
                    }

                    var createUrlRegex = /.*\/umbraco.*\/content.*\/edit\/.*create=true.*$/i;

                    if (pageId === undefined && createUrlRegex.test(window.location.href)) {
                        return;
                    }

                    userService.getCurrentUser().then(function (user) {
                        if (pageId === undefined) {
                            pageId = editorState.current.id;
                        }

                        if (pageId === undefined || pageId === 0 || pageId === -1) {
                            return;
                        }

                        contentGuardService.isPageLocked(pageId, user.name).then(function (data) {

                            // 1. If page is not LOCKED = enter and LOCK it for the current user (comment)
                            // 2. If page is LOCKED = display the notification message and let user decide what to do
                            // 3. Option 1 after point 2: Leave = redirect to the main /content url = don't touch this page
                            // 4. Option 2 after point 2: Takeover = UNLOCK the page and LOCK for the user who triggered the action

                            if (!data.isPageLocked) {
                                contentGuardService.lockPage(pageId, user.name);
                                return;
                            }

                            var overlay = {
                                title: "🛡️ Content Guard - This page is locked",
                                confirmMessage: data.currentlyEditingUserName + " is currently editing this page. Do you want to take over?",
                                content: "If you take over, any unsaved changes made by " + data.currentlyEditingUserName + " may be lost.",
                                disableBackdropClick: true,
                                closeButtonLabelKey: "contentGuard_closeLabel",
                                submitButtonLabelKey: "contentGuard_submitLabel",
                                close: function () {
                                    overlayService.close();
                                    // + Redirect to main Content area = leave the page?
                                    window.location.replace("/umbraco");
                                },
                                submit: function () {
                                    // UNLOCK + redirect? set up lock?
                                    contentGuardService.unlockPage(pageId)
                                        .then(function () {
                                            window.location.reload();
                                        });
                                }
                            };

                            overlayService.confirm(overlay);
                        });
                    });
                }
            });
})();