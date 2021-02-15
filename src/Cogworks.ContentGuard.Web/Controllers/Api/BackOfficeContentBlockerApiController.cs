using System.Web.Http;
using BackOfficeContentBlocker.Core.Services;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;

namespace BackOfficeContentBlocker.Web.Controllers.Api
{
    [PluginController("ContentBlocker")]
    public class BackOfficeContentBlockerApiController : UmbracoAuthorizedJsonController
    {
        private readonly IBackOfficeContentBlockerService _contentBlockerService;

        public BackOfficeContentBlockerApiController(IBackOfficeContentBlockerService contentBlockerService)
        {
            _contentBlockerService = contentBlockerService;
        }

        [HttpGet]
        public IHttpActionResult IsPageBlocked(string currentUserEmail, int pageId)
        {
            var isPageOccupied = _contentBlockerService.IsPageBlocked(currentUserEmail, pageId);

            return Json(isPageOccupied);
        }

        [HttpGet]
        public IHttpActionResult LockPage(string currentUserEmail, int pageId)
        {
            var pageLocked = _contentBlockerService.LockPage(currentUserEmail, pageId);

            return Json(pageLocked);
        }

        [HttpGet]
        public IHttpActionResult RemoveLock(string currentUserEmail)
        {
            var lockRemoved = _contentBlockerService.RemoveLock(currentUserEmail);

            return Json(lockRemoved);
        }
    }
}