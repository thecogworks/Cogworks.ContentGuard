angular.module("umbraco.services").config([

    "$httpProvider",

    function ($httpProvider) {
        // See: https://dev.to/skttl/how-to-customize-searching-in-umbraco-list-views-1knk
        // See: https://skrift.io/issues/changing-backoffice-functionality-without-changing-core-code/
        $httpProvider.interceptors.push(function ($q, $injector, overlayService) {
            return {
                'request': function (request) {
                    var editUrlRegex = /^\/umbraco\/backoffice\/UmbracoApi\/Content\/GetById\?id=(\d*)$/i;

                    if (editUrlRegex.test(request.url)) {
                        var pageId = editUrlRegex.exec(request.url)[1];
                        var userService = $injector.get("userService");
                        var contentGuardService = $injector.get("contentGuardService");

                        userService.getCurrentUser().then(function (user) {
                            console.log(user);

                            contentGuardService.isPageLocked(pageId, user.name)
                                .then(function (isLocked) {

                                    console.log(isLocked);

                                    // 1. If page is not LOCKED = enter and LOCK it for the current user (comment)
                                    // 2. If page is LOCKED = display the notification message and let user decide what to do
                                    // 3. Option 1 after point 2: Leave = redirect to the main /content url = don't touch this page
                                    // 4. Option 2 after point 2: Takeover = UNLOCK the page and LOCK for the user who triggered the action
                                    if (!isLocked) {
                                        contentGuardService.lockPage(pageId, user.name);
                                    }
                                    else {
                                        var overlay = {
                                            title: "🛡️ Content Guard - This page is locked",
                                            confirmMessage: "{{username}} is currently editing this page. Do you want to take over?",
                                            content: "If you'll take over, the changes not saved by {{username}} might got lost.",
                                            disableBackdropClick: true,
                                            closeButtonLabelKey: "contentGuard_closeLabel",
                                            submitButtonLabelKey: "contentGuard_submitLabel",
                                            close: function () {
                                                overlayService.close();
                                                // + Redirect to main Content area = leave the page?
                                                window.location.href = "/umbraco/#/content";
                                            },
                                            submit: function () {
                                                // UNLOCK + redirect? set up lock?
                                                contentGuardService.unlockPage(pageId)
                                                    .then(function (result) {
                                                        window.location.reload();
                                                    })
                                            }
                                        };
                                        overlayService.confirm(overlay);
                                    }

                                });
                        });
                    }

                    return request || $q.when(request);
                }
            };
        });
    }]);