using Umbraco.Cms.Infrastructure.Migrations;
using Microsoft.Extensions.Logging;
using Unicorn.Umbraco.InvoiceManager.Models.Schema;
using Unicorn.Umbraco.InvoiceManager.Models.Dtos;
using Unicorn.Umbraco.InvoiceManager.Data.Models.Schema;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core;
using Unicorn.Umbraco.InvoiceManager.Constants;
using Umbraco.Cms.Core.Web;

namespace Unicorn.Umbraco.InvoiceManager.Migrations
{
    public class CreateTableMigration : MigrationBase
    {
        private readonly IUmbracoContextFactory _context;
        private readonly IScopeProvider _scopeProvider;
        private readonly IUserService _userService;

        public CreateTableMigration(IMigrationContext context, IUmbracoContextFactory umbracoContextFactory, IScopeProvider scopeProvider, IUserService userService)
            : base(context)
        {
            _userService = userService;
            _context = umbracoContextFactory;
            _scopeProvider = scopeProvider;
        }
        protected override void Migrate()
        {
            Logger.LogDebug("Running migration {MigrationStep}", "Add Invoice Manager Tables");

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

            if (TableExists(InvoiceItemsSchema.TableName) == false)
            {
                Create.Table<InvoiceItemsSchema>().Do();
            }
            else
            {
                Logger.LogDebug("The database table {DbTable} already exists, skipping", InvoiceItemsSchema.TableName);
            }
        }
    }
}
