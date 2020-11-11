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
            var isPageOccupied = _contentBlockerService.isPageBlocked(currentUserEmail, pageId);

            return Json(isPageOccupied);
        }
    }
}