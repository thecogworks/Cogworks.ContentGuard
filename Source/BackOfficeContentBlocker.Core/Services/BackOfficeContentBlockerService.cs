namespace BackOfficeContentBlocker.Core.Services
{
    public interface IBackOfficeContentBlockerService
    {
        bool IsPageOccupied(string currentUserEmail, int pageId);
    }

    public class BackOfficeContentBlockerService : IBackOfficeContentBlockerService
    {
        public bool IsPageOccupied(string currentUserEmail, int pageId)
        {
            return true;
        }
    }
}