using System;
using System.Globalization;
using System.Linq;
using BackOfficeContentBlocker.Core.Models.Schemas;
using Umbraco.Core.Scoping;

namespace BackOfficeContentBlocker.Core.Services
{
    public interface IBackOfficeContentBlockerService
    {
        bool IsPageBlocked(string currentUserEmail, int pageId);
        bool LockPage(string currentUserEmail, int pageId);
    }

    public class BackOfficeContentBlockerService : IBackOfficeContentBlockerService
    {
        private readonly IScopeProvider _scopeProvider;
        public BackOfficeContentBlockerService(IScopeProvider scopeProvider)
        {
            _scopeProvider = scopeProvider;
        }

        public bool IsPageBlocked(string currentUserEmail, int pageId)
        {
            bool isPageBlocked;

            using (var scope = _scopeProvider.CreateScope())
            {
                isPageBlocked = scope.Database
                    .Fetch<BackOfficeContentBlockerSchema>()
                    .Where(x=> x.PageId == pageId && x.UserEmail != currentUserEmail)
                    .ToList()
                    .Any();

                scope.Complete();
            };

            return isPageBlocked;
        }

        public bool LockPage(string currentUserEmail, int pageId)
        {
            var entry = new BackOfficeContentBlockerSchema
            {
                UserEmail = currentUserEmail,
                PageId =  pageId,
                TimeStamp = DateTime.Now.ToString(CultureInfo.InvariantCulture)
            };

            RemoveLock(currentUserEmail);

            using (var scope = _scopeProvider.CreateScope())
            {
                scope.Database.Insert(entry);
                scope.Complete();
            }

            return true;
        }

        public bool RemoveLock(string currentUserEmail)
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                var blockedContentData = scope.Database
                    .Fetch<BackOfficeContentBlockerSchema>()
                    .Where(x => x.UserEmail != currentUserEmail)
                    .ToList();

                if (blockedContentData.Any())
                {
                    scope.Database.Delete(blockedContentData);
                }

                scope.Complete();
            };

            return true;
        }
    }
}