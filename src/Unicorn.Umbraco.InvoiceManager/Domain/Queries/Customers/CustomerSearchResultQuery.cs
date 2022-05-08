using Unicorn.Umbraco.InvoiceManager.Interfaces;
using Unicorn.Umbraco.InvoiceManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Umbraco.InvoiceManager.Queries
{
    public class CustomerSearchResultQuery
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

        
        public CustomerSearchResultQuery(int total, int limit, int offset, int page, int pages, ICustomer[] items)
        {
            Pagination = new SearchResultPagination(total, limit, offset, page, pages);
            Items = items;
        }

        public CustomerSearchResultQuery()
        {
        }

        #endregion
    }
}
