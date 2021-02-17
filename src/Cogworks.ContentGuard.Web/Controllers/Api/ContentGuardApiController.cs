using System.Web.Http;
using Cogworks.ContentGuard.Core.Services;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;

namespace BackOfficeContentBlocker.Web.Controllers.Api
{
    [PluginController("ContentGuard")]
    public class ContentGuardApiController : UmbracoAuthorizedJsonController
    {
        private readonly IContentGuardService _contentGuardService;

        public ContentGuardApiController(IContentGuardService contentGuardService)
        {
            _contentGuardService = contentGuardService;
        }

        [HttpGet]
        public IHttpActionResult IsLocked(int pageId, string ownerUsername)
        {
            var isPageLocked = _contentGuardService.IsLocked(pageId, ownerUsername);
            var currentlyEditingUserName = _contentGuardService.PageDetails(pageId);

            return Json(new
            {
                isPageLocked,
                currentlyEditingUserName
            });
        }

        [HttpGet]
        public IHttpActionResult Lock(int pageId, string ownerUsername)
        {
            _contentGuardService.Lock(pageId, ownerUsername);

            return Json("true");
        }

        [HttpGet]
        public IHttpActionResult Unlock(int pageId)
        {
            _contentGuardService.Unlock(pageId);

            return Json("true");
        }
    }
}