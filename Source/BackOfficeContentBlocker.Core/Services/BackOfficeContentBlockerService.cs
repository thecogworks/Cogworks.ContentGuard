namespace BackOfficeContentBlocker.Core.Services
{
    public interface IBackOfficeContentBlockerService
    {
        bool IsPageBlocked(string currentUserEmail, int pageId);
        bool LockPage(string currentUserEmail, int pageId);
    }

    public class BackOfficeContentBlockerService : IBackOfficeContentBlockerService
    {
        public bool IsPageBlocked(string currentUserEmail, int pageId)
        {
            return true;
        }

        public bool LockPage(string currentUserEmail, int pageId)
        {
            return true;
        }
    }
}