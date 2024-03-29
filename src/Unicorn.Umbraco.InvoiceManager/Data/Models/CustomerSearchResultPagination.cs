﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Umbraco.InvoiceManager.Models
{
    public class CustomerSearchResultPagination
    {
        #region Properties

        /// <summary>
        /// Gets the total amount of items across all pages.
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; }

        /// <summary>
        /// Gets the maximum amount of items per page.
        /// </summary>
        [JsonProperty("limit")]
        public int Limit { get; }

        /// <summary>
        /// Gets the offset.
        /// </summary>
        [JsonProperty("offset")]
        public int Offset { get; }

        /// <summary>
        /// Gets the current page.
        /// </summary>
        [JsonProperty("page")]
        public int Page { get; }

        /// <summary>
        /// Gets the total amout of pages.
        /// </summary>
        [JsonProperty("pages")]
        public int Pages { get; }

        /// <summary>
        /// Gets the index of the first item on the page.
        /// </summary>
        [JsonProperty("from")]
        public int From { get; }

        /// <summary>
        /// Gets the index of the last item on the page.
        /// </summary>
        [JsonProperty("to")]
        public int To { get; }

        #endregion

        #region Constructors
        public CustomerSearchResultPagination(int total, int limit, int offset, int page, int pages)
        {
            Total = total;
            Limit = limit;
            Offset = offset;
            Page = page;
            Pages = pages;
            From = offset + 1;
            To = Math.Min(offset + limit, total);
        }

        #endregion
    }
}
