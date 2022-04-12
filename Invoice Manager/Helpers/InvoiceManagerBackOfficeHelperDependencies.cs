using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Services;

namespace Invoice_Manager.Helpers
{
    public class InvoiceManagerBackOfficeHelperDependencies
    {
        #region Properties

        /// <summary>
        /// Gets the reference to the current <see cref="IRuntimeState"/>.
        /// </summary>
        public IRuntimeState RuntimeState { get; }

        /// <summary>
        /// Gets the reference to the current <see cref="IDomainService"/>.
        /// </summary>
        public IDomainService DomainService { get; }

        /// <summary>
        /// Gets the reference to the current <see cref="IContentService"/>.
        /// </summary>
        public IContentService ContentService { get; }

        /// <summary>
        /// Gets the reference to the current <see cref="IMediaService"/>.
        /// </summary>
        public IMediaService MediaService { get; }

        /// <summary>
        /// Gets the reference to the current <see cref="ILocalizedTextService"/>.
        /// </summary>
        public ILocalizedTextService TextService { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified dependencies.
        /// </summary>
        /// <param name="runtimeState"></param>
        /// <param name="domainService"></param>
        /// <param name="contentService"></param>
        /// <param name="mediaService"></param>
        /// <param name="textService"></param>
        public InvoiceManagerBackOfficeHelperDependencies(IRuntimeState runtimeState, IDomainService domainService, IContentService contentService, IMediaService mediaService, ILocalizedTextService textService)
        {
            RuntimeState = runtimeState;
            DomainService = domainService;
            ContentService = contentService;
            MediaService = mediaService;
            TextService = textService;
        }

        #endregion
    }
}
