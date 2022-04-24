using Unicorn.Umbraco.InvoiceManager.Models.Dtos;
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

namespace Unicorn.Umbraco.InvoiceManager.Migrations
{
    public class AddPhoneColumnMigration: MigrationBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public AddPhoneColumnMigration(IMigrationContext context, IHostingEnvironment hostingEnvironment) : base(context)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        protected override void Migrate()
        {

            try
            {
                if (!ColumnExists(CustomerDto.TableName, nameof(CustomerDto.Phone)))
                {
                    Create
                        .Column(nameof(CustomerDto.Phone))
                        .OnTable(CustomerDto.TableName)
                        .AsString()
                        .WithDefaultValue(String.Empty)
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
