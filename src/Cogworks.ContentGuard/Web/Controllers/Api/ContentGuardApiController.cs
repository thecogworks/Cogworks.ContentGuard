using System.Web.Http;
using Cogworks.ContentGuard.Core.Services;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;

namespace Cogworks.ContentGuard.Web.Controllers.Api
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
            var pageLockDetails = _contentGuardService.GetPageLockDetails(pageId);
            var isPageLocked = _contentGuardService.IsLocked(pageLockDetails, ownerUsername);
            var currentlyEditingUserName = _contentGuardService.GetPageEditingUser(pageLockDetails);


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

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Unlock(int pageId)
        {
            _contentGuardService.Unlock(pageId);

            return Ok();
        }
    }
}