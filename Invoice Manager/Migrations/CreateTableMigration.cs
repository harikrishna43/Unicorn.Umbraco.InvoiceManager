using Umbraco.Cms.Infrastructure.Migrations;
using Microsoft.Extensions.Logging;
using Invoice_Manager.Models.Schema;

namespace Invoice_Manager.Migrations
{
    public class CreateTableMigration : MigrationBase
    {
        public CreateTableMigration(IMigrationContext context) : base(context)
        {
        }
        protected override void Migrate()
        {
            Logger.LogDebug("Running migration {MigrationStep}", "AddCommentsTable");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (TableExists(CustomerSchema.TableName) == false)
            {
                Create.Table<CustomerSchema>().Do();
            }
            else
            {
                Logger.LogDebug("The database table {DbTable} already exists, skipping", CustomerSchema.TableName);
            }

            if (TableExists(InvoiceDto.TableName) == false)
            {
                Create.Table<InvoiceDto>().Do();
            }
            else
            {
                Logger.LogDebug("The database table {DbTable} already exists, skipping", InvoiceDto.TableName);
            }
        }
    }
}
