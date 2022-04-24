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
        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("unitprice")]
        public decimal UnitPrice { get; set; }

        [JsonProperty("gst")]
        public decimal GST { get; set; }

        [JsonProperty("totalamount")]
        public decimal TotalAmount { get; }

        [JsonProperty("taxableamount")]
        public decimal TaxableAmount { get;}

        [JsonProperty("totaltax")]
        public decimal TotalTax { get; }

    }
}
