using Unicorn.Umbraco.InvoiceManager.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorn.Umbraco.InvoiceManager.Models;

namespace Unicorn.Umbraco.InvoiceManager.Commands.Invoices
{
    public class CreateInvoiceCommand: ICommand
    {
        [JsonProperty("invoiceid")]
        public int InvoiceId { get; set; }

        [JsonProperty("customerId")]
        public int CustomerId { get; set; }

        [JsonProperty("paymentstatus")]
        public int PaymentStatus { get; set; }

        [JsonProperty("invoicenote")]
        public string InvoiceNote { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("invoicedate")]
        public DateTime InvoiceDate { get; set; }

        [JsonProperty("duedate")]
        public DateTime DueDate { get; set; }

        public List<InvoiceData> InvoiceData { get; set; }
    }

   
}
