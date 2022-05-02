using Unicorn.Umbraco.InvoiceManager.Interfaces;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Unicorn.Umbraco.InvoiceManager.Services;

namespace Unicorn.Umbraco.InvoiceManager.Services{

    /// <summary>
    /// Class representing the dependencies for the <see cref="InvoiceServiceDependencies"/> class.
    /// </summary>
    public class InvoiceServiceDependencies {

        #region Properties

        /// <summary>
        /// Gets the reference to the current <see cref="ILogger{InvoiceServices}"/>.
        /// </summary>
        internal ILogger<InvoiceServices> Logger { get; }

        /// <summary>
        /// Gets the reference to the current <see cref="IScopeProvider"/>.
        /// </summary>
        public IScopeProvider ScopeProvider { get; }
        
        /// <summary>
        /// Gets the reference to the current <see cref="IDomainService"/>.
        /// </summary>
        public IDomainService Domains { get; }
        
        /// <summary>
        /// Gets the reference to the current <see cref="IContentService"/>.
        /// </summary>
        public IContentService ContentService { get; }
        
        /// <summary>
        /// Gets the reference to the current <see cref="IUmbracoContextAccessor"/>.
        /// </summary>
        public IUmbracoContextAccessor UmbracoContextAccessor { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified dependencies.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="scopeProvider"></param>
        /// <param name="domains"></param>
        /// <param name="contentService"></param>
        /// <param name="umbracoContextAccessor"></param>
        public InvoiceServiceDependencies(ILogger<InvoiceServices> logger, IScopeProvider scopeProvider, IDomainService domains, IContentService contentService, IUmbracoContextAccessor umbracoContextAccessor) {
            Logger = logger;
            ScopeProvider = scopeProvider;
            Domains = domains;
            ContentService = contentService;
            UmbracoContextAccessor = umbracoContextAccessor;
        }

        #endregion

    }

}