using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Migrations;
using Unicorn.Umbraco.InvoiceManager.Constants;

namespace Unicorn.Umbraco.InvoiceManager.Data.Migrations
{
    public class AddSectionToAdminsMigration : MigrationBase
    {
        private readonly IUmbracoContextFactory _context;
        private readonly IScopeProvider _scopeProvider;
        private readonly IUserService _userService;

        public AddSectionToAdminsMigration(IMigrationContext context, IUmbracoContextFactory umbracoContextFactory, IScopeProvider scopeProvider, IUserService userService)
            : base(context)
        {
            _userService = userService;
            _context = umbracoContextFactory;
            _scopeProvider = scopeProvider;
        }

        protected override void Migrate()
        {
            using (UmbracoContextReference umbracoContextReference = _context.EnsureUmbracoContext())
            {
                using (var scope = _scopeProvider.CreateScope())
                {
                    var adminGroup = _userService.GetUserGroupByAlias("admin");
                    adminGroup.AddAllowedSection(ConstantVariables.SectionAlias);

                    _userService.Save(adminGroup);

                    scope.Complete();
                }
            }
        }
    }
}
