using NPoco;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace BackOfficeContentBlocker.Core.Models.Schemas
{
    [TableName("BackOfficeContentBlocker")]
    [PrimaryKey("Id", AutoIncrement = true)]
    [ExplicitColumns]
    public class BackOfficeContentBlockerSchema
    {
        [PrimaryKeyColumn(AutoIncrement = true, IdentitySeed = 1)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("UserEmail")] public string UserEmail { get; set; }

        [Column("PageId")] public int PageId { get; set; }

        [Column("TimeStamp")] public string TimeStamp { get; set; }
    }
}
