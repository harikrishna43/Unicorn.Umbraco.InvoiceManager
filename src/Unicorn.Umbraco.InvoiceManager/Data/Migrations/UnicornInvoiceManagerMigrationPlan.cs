using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Packaging;
using Unicorn.Umbraco.InvoiceManager.Migrations;

namespace Unicorn.Umbraco.InvoiceManager.Data.Migrations
{
    public class UnicornInvoiceManagerMigrationPlan : PackageMigrationPlan
    {
        public UnicornInvoiceManagerMigrationPlan()
            : base("Unicorm.Umbraco.InvoiceManager", "UnicornInvoiceManager_v1_4")
        { }

        protected override void DefinePlan()
        {
            To<CreateTableMigration>("state-1");
            To<AddSectionToAdminsMigration>("state-2");
        }
    }
}
