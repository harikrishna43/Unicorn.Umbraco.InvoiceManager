using Invoice_Manager.Interfaces;
using Invoice_Manager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_Manager.Queries
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

        /// <summary>
        /// Initializes a new instance based on the specified parameters.
        /// </summary>
        /// <param name="total">The total amount of redirects matched.</param>
        /// <param name="limit">The maximum amount of redirects to be returned per page.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="page">The page returned.</param>
        /// <param name="pages">The total amount of pages.</param>
        /// <param name="items">An array of the items making up the page.</param>
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
