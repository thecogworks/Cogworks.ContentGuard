(() => {

    angular
        .module('umbraco')
        .controller(
            'Cogworks.ContentGuard.Controller',
            function ($scope, editorState, contentGuardService, overlayService, userService, eventsService, localizationService) {
                var vm = this;

                vm.buttonState = 'init';
                vm.unlockButtonClick = unlockButtonClick;
                vm.notification = '';

                var unsubscribeAppTabChange = eventsService.on('app.tabChange', function (event) {
                    tryBlockContent();
                    event.preventDefault();
                });

                var unsubscribeGuardContentSave = eventsService.on('guard.ContentSave', function (event, args) {
                    if (args && args.isElement) {
                        return;
                    }

                    tryBlockContent(args.id);
                    event.preventDefault();
                });

                $scope.$on('$destroy', function () {
                    unsubscribeAppTabChange();
                    unsubscribeGuardContentSave();
                });

                tryBlockContent();

                function unlockCurrentPage() {
                    var pageId = editorState.current.id;
                    contentGuardService.unlockPage(pageId).then(
                        function () {
                            vm.buttonState = 'success';
                            vm.notification = 'Page successfully unlocked. You will be redirected to root page in a moment.';

                            setTimeout(function () {
                                window.location.replace('/umbraco');
                            }, 2500);
                        },
                        function (error) {
                            vm.buttonState = 'error';
                            vm.notification = 'There was a problem unlocking this page. Please try again.';

                            console.log('Unlocking page failed', error);
                        }
                    );
                }

                function unlockButtonClick() {
                    vm.buttonState = 'busy';

                    var currentVersion = editorState.current.variants.find((v) => v.active === true);
                    var isDirty = currentVersion && currentVersion.isDirty === true;

                    if (isDirty) {
                        localizationService
                            .localizeMany(['prompt_unsavedChanges', 'prompt_unsavedChangesWarning', 'prompt_discardChanges', 'prompt_stay'])
                            .then(function (values) {
                                var overlay = {
                                    view: 'default',
                                    title: values[0],
                                    content: values[1],
                                    disableBackdropClick: true,
                                    disableEscKey: true,
                                    submitButtonLabel: values[2],
                                    closeButtonLabel: values[3],
                                    submit: function () {
                                        overlayService.close();
                                        unlockCurrentPage();
                                    },
                                    close: function () {
                                        overlayService.close();
                                        vm.buttonState = 'init';
                                        vm.notification = '';
                                    },
                                };

                                overlayService.open(overlay);
                            });
                    } else {
                        unlockCurrentPage();
                    }
                }

                function tryBlockContent(pageId = undefined) {
                    var currentTab = editorState.current.apps.find((x) => x.active === true);
                    var tabAlias = currentTab.alias;

                    if (!(tabAlias === 'umbContent' || tabAlias === 'umbInfo')) {
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
                            if (!data.isPageLocked) {
                                contentGuardService.lockPage(pageId, user.name);
                                return;
                            }

                            var overlay = {
                                title: '🛡️ Content Guard - This page is locked',
                                confirmMessage: data.currentlyEditingUserName + ' is currently editing this page. Do you want to take over?',
                                content: 'If you take over, any unsaved changes made by ' + data.currentlyEditingUserName + ' may be lost.',
                                disableBackdropClick: true,
                                closeButtonLabelKey: 'contentGuard_closeLabel',
                                submitButtonLabelKey: 'contentGuard_submitLabel',
                                close: function () {
                                    overlayService.close();
                                    window.location.replace('/umbraco');
                                },
                                submit: function () {
                                    contentGuardService.unlockPage(pageId).then(function () {
                                        overlayService.close();
                                        contentGuardService.lockPage(pageId, user.name);
                                    });
                                },
                            };

                            overlayService.confirm(overlay);
                        });
                    });
                }
            }
        );

})();
