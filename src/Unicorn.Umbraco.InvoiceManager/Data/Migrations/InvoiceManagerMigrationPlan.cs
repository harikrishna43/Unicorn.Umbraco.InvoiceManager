using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Infrastructure.Migrations;

namespace Unicorn.Umbraco.InvoiceManager.Data.Migrations
{
    public class InvoiceManagerMigrationPlan : MigrationPlan
    {
        public InvoiceManagerMigrationPlan()
            : base("Unicorn.Umbraco.InvoiceManager")
        {
            From(string.Empty).To<AddSectionToAdminsMigration>("AddedSectionForAdmins-InvoiceManager");
        }
    }
}
