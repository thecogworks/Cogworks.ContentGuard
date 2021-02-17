using Umbraco.Core.Logging;
using Umbraco.Core.Migrations;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace Cogworks.ContentGuard.Core.Migrations
{
    public class AddContentGuardRelationType : MigrationBase
    {
        private IRelationService _relationService;

        public AddContentGuardRelationType(IMigrationContext context, IRelationService relationService) : base(context)
        {
            _relationService = relationService;
        }

        public override void Migrate()
        {
            Logger.Debug<AddContentGuardRelationType>("Running migration {MigrationStep}", "AddContentGuardRelationType");

            var contentGuartRelationType = _relationService.GetRelationTypeByAlias("contentguard");
            if (contentGuartRelationType != null) return;

            // Insert custom relation type to Umbraco DB
            contentGuartRelationType =
                new RelationType("Relate (Lock) the Document with the Owner", "contentguard", true, null, null);

            _relationService.Save(contentGuartRelationType);
        }
    }
}