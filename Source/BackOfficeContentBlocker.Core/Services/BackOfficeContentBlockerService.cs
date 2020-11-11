namespace BackOfficeContentBlocker.Core.Services
{
    public interface IBackOfficeContentBlockerService
    {
        bool isPageBlocked(string currentUserEmail, int pageId);
    }

    public class BackOfficeContentBlockerService : IBackOfficeContentBlockerService
    {
        public bool isPageBlocked(string currentUserEmail, int pageId)
        {
            return true;
        }
    }
}