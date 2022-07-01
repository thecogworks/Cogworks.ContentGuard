using Cogworks.ContentGuard.Core.Services;
using Cogworks.ContentGuard.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Web.BackOffice.Controllers;
using Umbraco.Cms.Web.BackOffice.Filters;
using Umbraco.Cms.Web.Common.Attributes;

namespace Cogworks.ContentGuard.Web.Controllers.Api;



[PluginController("ContentGuard")]
public class ContentGuardApiController : UmbracoAuthorizedJsonController
{
    private readonly IContentGuardService _contentGuardService;

    public ContentGuardApiController(IContentGuardService contentGuardService)
    {
        _contentGuardService = contentGuardService;
    }
    
    
    [HttpGet]
    [JsonCamelCaseFormatter]
    public LockInformation IsLocked(int pageId, string ownerUsername)
    {
        var pageLockDetails = _contentGuardService.GetPageLockDetails(pageId);
        var isPageLocked = _contentGuardService.IsLocked(pageLockDetails, ownerUsername);
        var currentlyEditingUserName = _contentGuardService.GetPageEditingUser(pageLockDetails);

        return new LockInformation()
        {
            CurrentlyEditingUserName = currentlyEditingUserName,
            IsPageLocked = isPageLocked
        };
    }

    [HttpGet]
    public IActionResult Lock(int pageId, string ownerUsername)
    {
        _contentGuardService.Lock(pageId, ownerUsername);

        return Ok();
    }

    [HttpGet]
    public IActionResult Unlock(int pageId)
    {
        _contentGuardService.Unlock(pageId);

        return Ok();
    }
}

