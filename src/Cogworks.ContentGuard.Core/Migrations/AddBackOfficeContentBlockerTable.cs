using BackOfficeContentBlocker.Core.Models.Schemas;
using Umbraco.Core.Logging;
using Umbraco.Core.Migrations;

namespace BackOfficeContentBlocker.Core.Migrations
{
    public class AddBackOfficeContentBlockerTable : MigrationBase
    {
        public AddBackOfficeContentBlockerTable(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug<AddBackOfficeContentBlockerTable>("Running migration {MigrationStep}", "AddCommentsTable");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (TableExists("BackOfficeContentBlocker") == false)
            {
                Create.Table<BackOfficeContentBlockerSchema>().Do();
            }
            else
            {
                Logger.Debug<AddBackOfficeContentBlockerTable>("The database table {DbTable} already exists, skipping",
                    "BlogComments");
            }
        }
    }
}