using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Umbraco.InvoiceManager.Interfaces
{
    public interface IInvoiceData
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("note")]
        public string InvoiceNote { get; set; }

        [JsonProperty("invoiceid")]
        public int InvoiceId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("unitprice")]
        public decimal UnitPrice { get; set; }

        [JsonProperty("gst")]
        public decimal GST { get; set; }

        [JsonProperty("datecreated")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("datemodified")]
        public DateTime DateModified { get; set; }

        public decimal TaxableAmount { get; }
        public decimal TotalTax { get; }
        public decimal TotalAmount { get; }

    }
}
