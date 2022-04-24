using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Dashboards;
using Umbraco.Cms.Core.Models.ContentEditing;
using Umbraco.Cms.Core.Models.Membership;
using Umbraco.Cms.Core.Models;
using Umbraco.Extensions;

namespace Unicorn.Umbraco.InvoiceManager.Helpers
{
    public class InvoiceManagerBackOfficeHelper
    {
        #region Properties

        /// <summary>
        /// gets a reference to the dependencies of this instance.
        /// </summary>
        protected InvoiceManagerBackOfficeHelperDependencies Dependencies { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="dependencies"/>.
        /// </summary>
        /// <param name="dependencies">An instance of <see cref="InvoiceManagerBackOfficeHelperDependencies"/>.</param>
        public InvoiceManagerBackOfficeHelper(InvoiceManagerBackOfficeHelperDependencies dependencies)
        {
            Dependencies = dependencies;
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Returns the localized value for the key with the specified <paramref name="alias"/> within the <c>redirects</c> area.
        /// </summary>
        /// <param name="alias">The alias of the key.</param>
        /// <returns>The localized value.</returns>
        public string Localize(string alias)
        {
            return Localize(alias, "customer");
        }

        /// <summary>
        /// Returns the localized value for the key with the specified <paramref name="alias"/> and <paramref name="area"/>.
        /// </summary>
        /// <param name="alias">The alias of the key.</param>
        /// <param name="area">The area in which the key is located.</param>
        /// <returns>The localized value.</returns>
        public string Localize(string alias, string area)
        {
            return Dependencies.TextService.Localize(area, alias);
        }

        /// <summary>
        /// Returns the access rules for <see cref="RedirectsDashboard"/>.
        /// </summary>
        /// <returns>An array of <see cref="IAccessRule"/>.</returns>
        public virtual IAccessRule[] GetDashboardAccessRules()
        {
            return Array.Empty<IAccessRule>();
        }

        /// <summary>
        /// Returns a cache buster value based both on Umbraco's own cache buster as well as the current version of
        /// this package. This ensures a new cache buster value when either the ClientDependency version is bumped or
        /// the package is updated.
        /// </summary>
        /// <returns>The cache buster value.</returns>
        public virtual string GetCacheBuster()
        {
            string version1 = Dependencies.RuntimeState.SemanticVersion.ToSemanticString();
            string version2 = GetInformationVersion();
            return $"{version1}.{Dependencies.RuntimeState.Level}.{version2}".GenerateHash();
        }

        /// <summary>
        /// Gets the information version of the package.
        /// </summary>
        /// <returns>The information version.</returns>
        public string GetInformationVersion()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;

            return version;
        }

        /// <summary>
        /// Returns a dictonary with server variables for this pacakge, available through <c>Umbraco.Sys.ServerVariables.skybrud.redirects</c> in the backoffice.
        /// </summary>
        /// <returns>An instance of <see cref="Dictionary{TKey,TValue}"/>.</returns>
        public virtual Dictionary<string, object> GetServerVariables()
        {

            // Determine the API base URL
            string customerbaseUrl = "/umbraco/backoffice/InvoiceManager/Customer/";
            string invoicebaseUrl = "/umbraco/backoffice/InvoiceManager/Invoice/";

            // Append the "redirects" dictionary to "skybrud"
            return new Dictionary<string, object> {
                {"customerbaseUrl", customerbaseUrl },
                {"invoicebaseUrl", invoicebaseUrl },
                {"cacheBuster", GetCacheBuster()},
                {"version", GetInformationVersion()}
            };

        }
        #endregion

    }
}
