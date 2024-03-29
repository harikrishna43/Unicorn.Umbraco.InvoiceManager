﻿using Unicorn.Umbraco.InvoiceManager.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Umbraco.InvoiceManager.Models.Options
{
    public class EditInvoiceOption
    {
        [JsonProperty("invoiceid")]
        public int InvoiceId { get; set; }

        [JsonProperty("customerId")]
        public int CustomerId { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("invoicedate")]
        public DateTime InvoiceDate { get; set; }

        [JsonProperty("paymentstatus")]
        public int PaymentStatus { get; set; }


        [JsonProperty("duedate")]
        public DateTime DueDate { get; set; }

        [JsonProperty("invoicenote")]
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
        public List<InvoiceData> InvoiceData { get; set; }

    }
}
