using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Migrations;

namespace Cogworks.ContentGuard.Core.Migrations;

internal class AddContentGuardRelationType : MigrationBase
{
    private readonly IRelationService _relationService;
    private readonly ILogger _logger;



    public AddContentGuardRelationType(IMigrationContext context, IRelationService relationService, ILogger<AddContentGuardRelationType> logger) : base(context)
    {
        _relationService = relationService;
        _logger = logger;
    }

    protected override void Migrate()
    {
        _logger.LogDebug("Running migration {MigrationStep}", "AddContentGuardRelationType");
        var contentGuardRelationType = _relationService.GetRelationTypeByAlias("contentguard");

        if (contentGuardRelationType != null)
        {
            return;
        }

        // Insert custom relation type to Umbraco DB

        // TODO: this ctor call must be different, maybe?
        contentGuardRelationType =
            new RelationType("Relate (Lock) the Document with the Owner", "contentguard", true, null, null, true);

        _relationService.Save(contentGuardRelationType);

    }
}

