using Cogworks.ContentGuard.Core.Migrations;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Composing;

using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Migrations;
using Umbraco.Cms.Infrastructure.Migrations.Upgrade;
using Umbraco.Cms.Infrastructure.Scoping;

namespace Cogworks.ContentGuard.Core.Components;



internal class ContentGuardComponent : IComponent
{

    private readonly IScopeProvider _scopeProvider;
    private readonly IScopeAccessor _scopeAccessor;
    private readonly IMigrationBuilder _migrationBuilder;
    private readonly IKeyValueService _keyValueService;
    private readonly ILogger _logger;
    private readonly ILoggerFactory _loggerFactory;


    public ContentGuardComponent(IScopeProvider scopeProvider, IMigrationBuilder migrationBuilder,
        IKeyValueService keyValueService, ILogger<ContentGuardComponent> logger, IScopeAccessor scopeAccessor, ILoggerFactory loggerFactory)
    {
        _scopeProvider = scopeProvider;
        _migrationBuilder = migrationBuilder;
        _keyValueService = keyValueService;
        _logger = logger;
        _scopeAccessor = scopeAccessor;
        _loggerFactory = loggerFactory;
    }

    public void Initialize()
    {
        // Create a migration plan for a specific project/feature
        // We can then track that latest migration state/step for this project/feature
        var migrationPlan = new MigrationPlan("ContentGuard");

        // This is the steps we need to take
        // Each step in the migration adds a unique value
        migrationPlan.From(string.Empty)
            .To<AddContentGuardRelationType>("contentguard-init");

        // Go and upgrade our site (Will check if it needs to do the work or not)
        // Based on the current/latest step
        var upgrader = new Upgrader(migrationPlan);
        var executor = new MigrationPlanExecutor(_scopeProvider, _scopeAccessor, _loggerFactory, _migrationBuilder);
        try {
            upgrader.Execute(executor, _scopeProvider, _keyValueService);
        } catch (Exception ex) {
            _logger.LogDebug($"no connections string, please perform the umbraco installation!");
        }
        
    }

    public void Terminate()
    {
    }
}




