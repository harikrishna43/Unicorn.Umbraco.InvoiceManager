using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_Manager.Interfaces
{
    public interface IInvoice
    {
        [JsonProperty("invoiceid")]
        public int InvoiceId { get; set; }

        [JsonProperty("customerId")]
        public int CustomerId { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("invoicedate")]
        public DateTime InvoiceDate { get; set; }

        [JsonProperty("duedate")]
        public DateTime DueDate { get; set; }

        [JsonProperty("note")]
        public string InvoiceNote { get; set; }

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

        [JsonProperty("isdeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("totalamount")]
        public decimal TotalAmount { get; }

        [JsonProperty("totaltax")]
        public decimal TotalTax { get; }

        [JsonProperty("invoicenumber")]
        public string InvoiceNumber { get; }

        [JsonProperty("customer")]
        public ICustomer Customer { get; }

        [JsonProperty("invoicedata")]
        public IInvoiceData InvoiceData { get; }
    }
}
