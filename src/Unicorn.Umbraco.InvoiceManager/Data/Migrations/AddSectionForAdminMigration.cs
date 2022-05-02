using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Migrations;
using Umbraco.Cms.Web.Common.UmbracoContext;
using Unicorn.Umbraco.InvoiceManager.Models.Schema;

namespace Unicorn.Umbraco.InvoiceManager.Migrations
{
    public class AddSectionForAdminMigration : MigrationBase
    {
        private readonly IUmbracoContextFactory _context;
        private readonly IScopeProvider _scopeProvider;
        private readonly IUserService _userService;
        public AddSectionForAdminMigration(IUmbracoContextFactory umbracoContextFactory, IScopeProvider scopeProvider, IUserService userService,IMigrationContext context) : base(context)
        {
            _userService = userService;
            _context = umbracoContextFactory;
            _scopeProvider = scopeProvider;
        }
        protected override void Migrate()
        {
            Logger.LogDebug("Running migration {MigrationStep}", "Addsection for admin");
            using (UmbracoContextReference umbracoContextReference = _context.EnsureUmbracoContext())
            {
                using (var scope = _scopeProvider.CreateScope())
                {
                    var adminGroup = _userService.GetUserGroupByAlias("admin");
                    adminGroup.AddAllowedSection("unicornUmbracoInvoiceManager");

                    _userService.Save(adminGroup);

                    scope.Complete();
                }
            }

        }
    }
}
