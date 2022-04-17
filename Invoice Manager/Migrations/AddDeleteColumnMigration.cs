using Invoice_Manager.Models.Dtos;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Hosting;
using Umbraco.Cms.Infrastructure.Migrations;

namespace Invoice_Manager.Migrations
{
    public class AddDeleteColumnMigration: MigrationBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public AddDeleteColumnMigration(IMigrationContext context, IHostingEnvironment hostingEnvironment) : base(context)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        protected override void Migrate()
        {

            try
            {
                // Add the "DestinationQuery" column to the database table
                if (!ColumnExists(CustomerDto.TableName, nameof(CustomerDto.IsDeleted)))
                {
                    Create
                        .Column(nameof(CustomerDto.IsDeleted))
                        .OnTable(CustomerDto.TableName)
                        .AsBoolean()
                        .WithDefaultValue(false)
                        .Do();
                }

                // Add the "DestinationFragment" column to the database table
                if (!ColumnExists(InvoiceDto.TableName, nameof(InvoiceDto.IsDeleted)))
                {
                    Create
                        .Column(nameof(InvoiceDto.IsDeleted))
                        .OnTable(InvoiceDto.TableName)
                        .AsBoolean()
                        .WithDefaultValue(false)
                        .Do();
                }

            }
            catch (Exception ex)
            {

                throw new Exception("Failed migration for Unicorn.Umbraco.InvouceManager. See the Umbraco log for more information.", ex);

            }

        }
    }
}
