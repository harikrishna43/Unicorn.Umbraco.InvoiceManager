using Umbraco.Cms.Infrastructure.Migrations;
using Microsoft.Extensions.Logging;
using Unicorn.Umbraco.InvoiceManager.Models.Schema;
using Unicorn.Umbraco.InvoiceManager.Models.Dtos;

namespace Unicorn.Umbraco.InvoiceManager.Migrations
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

            if (TableExists(InvoiceSchema.TableName) == false)
            {
                Create.Table<InvoiceSchema>().Do();
            }
            else
            {
                Logger.LogDebug("The database table {DbTable} already exists, skipping", InvoiceSchema.TableName);
            }
        }
    }
}
