using Unicorn.Umbraco.InvoiceManager.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Umbraco.InvoiceManager.Models
{
    public class CustomerSearchResult
    {
        #region Properties

        /// <summary>
        /// Gets pagination information about the collection.
        /// </summary>
        [JsonProperty("pagination")]
        public SearchResultPagination Pagination { get; set; }

        /// <summary>
        /// Gets an array representing the items of the collection.
        /// </summary>
        [JsonProperty("items")]
        public ICustomer[] Items { get; set; }

        #endregion

        #region Constructors
        public CustomerSearchResult(int total, int limit, int offset, int page, int pages, ICustomer[] items)
        {
            Pagination = new SearchResultPagination(total, limit, offset, page, pages);
            Items = items;
        }

        #endregion
    }
}
