using Invoice_Manager.Interfaces;
using Invoice_Manager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_Manager.Commands.Invoices
{
    public class EditInvoiceCommand: ICommand
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

        [JsonProperty("customer")]
        public Customer Customer { get; set; }

        [JsonProperty("invoicedata")]
        public InvoiceData InvoiceData { get; set; }
    }
}
