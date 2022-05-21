using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorn.Umbraco.InvoiceManager.Interfaces;

namespace Unicorn.Umbraco.InvoiceManager.Interfaces
{
    public interface IInvoice
    {
        [JsonProperty("invoiceid")]
        public int InvoiceId { get; set; }

        [JsonProperty("customerId")]
        public int CustomerId { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("paymentstatus")]
        public int PaymentStatus { get; set; }

        [JsonProperty("invoicenote")]
        public string InvoiceNote { get; set; }

        [JsonProperty("invoicedate")]
        public DateTime InvoiceDate { get; set; }

        [JsonProperty("duedate")]
        public DateTime DueDate { get; set; }

        [JsonProperty("totalamount")]
        public decimal TotalAmount { get; }

        [JsonProperty("taxableamount")]
        public decimal TaxableAmount { get; }

        [JsonProperty("totaltax")]
        public decimal TotalTax { get; }

        [JsonProperty("datecreated")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("datemodified")]
        public DateTime DateModified { get; set; }

        [JsonProperty("isdeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("invoicenumber")]
        public string InvoiceNumber { get; }

        [JsonProperty("customer")]
        public ICustomer Customer { get; }

        [JsonProperty("invoicedata")]
        public List<IInvoiceData> InvoiceData { get; set; }
    }
}
