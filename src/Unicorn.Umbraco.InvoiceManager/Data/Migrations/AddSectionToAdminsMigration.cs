using System.Linq;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Migrations;
using Unicorn.Umbraco.InvoiceManager.Constants;
using UmbConstants = Umbraco.Cms.Core.Constants;

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
            var userGroup = _userService.GetUserGroupByAlias(UmbConstants.Security.AdminGroupAlias);

            if (userGroup != null && !userGroup.AllowedSections.Contains(ConstantVariables.SectionAlias))
            {
                userGroup.AddAllowedSection(ConstantVariables.SectionAlias);

                _userService.Save(userGroup);
            }
        }
    }
}
