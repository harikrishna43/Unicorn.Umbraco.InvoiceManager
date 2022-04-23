using System;
using Umbraco.Cms.Core.Semver;

namespace Invoice_Manager
{

    /// <summary>
    /// Static class with various information and constants about the package.
    /// </summary>
    public static class InvoiceManagerPackage {

        /// <summary>
        /// Gets the alias of the package.
        /// </summary>
        public const string Alias = "Unicorn.Umbraco.InvoiceManager";

        /// <summary>
        /// Gets the friendly name of the package.
        /// </summary>
        public const string Name = "Unicorn Invoice Manager";

        /// <summary>
        /// Gets the version of the package.
        /// </summary>
        public static readonly Version Version = typeof(InvoiceManagerPackage).Assembly.GetName().Version;

        
        /// <summary>
        /// Gets the URL of the GitHub repository for this package.
        /// </summary>
        public const string GitHubUrl = "https://github.com/harikrishna43/Unicorn.Umbraco.InvoiceManager";

        /// <summary>
        /// Gets the URL of the issue tracker for this package.
        /// </summary>
        public const string IssuesUrl = "https://github.com/harikrishna43/Unicorn.Umbraco.InvoiceManager/issues";
        
        /// <summary>
        /// Gets the URL of the documentation for this package.
        /// </summary>
        public const string DocumentationUrl = "https://github.com/harikrishna43/Unicorn.Umbraco.InvoiceManager";

    }

}