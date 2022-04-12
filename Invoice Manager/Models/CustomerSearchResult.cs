﻿using Invoice_Manager.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_Manager.Models
{
    public class CustomerSearchResult
    {
        #region Properties

        /// <summary>
        /// Gets pagination information about the collection.
        /// </summary>
        [JsonProperty("pagination")]
        public CustomerSearchResultPagination Pagination { get; }

        /// <summary>
        /// Gets an array representing the items of the collection.
        /// </summary>
        [JsonProperty("items")]
        public ICustomer[] Items { get; }

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
        public CustomerSearchResult(int total, int limit, int offset, int page, int pages, ICustomer[] items)
        {
            Pagination = new CustomerSearchResultPagination(total, limit, offset, page, pages);
            Items = items;
        }

        #endregion
    }
}