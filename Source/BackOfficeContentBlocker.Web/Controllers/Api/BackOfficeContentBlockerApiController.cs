using System.Web.Http;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;

namespace BackOfficeContentBlocker.Web.Controllers.Api
{
    [PluginController("ContentBlocker")]
    public class BackOfficeContentBlockerApiController : UmbracoAuthorizedJsonController
    {
        [HttpGet]
        public IHttpActionResult IndexEvents()
        {
            return Json("");
        }
    }
}