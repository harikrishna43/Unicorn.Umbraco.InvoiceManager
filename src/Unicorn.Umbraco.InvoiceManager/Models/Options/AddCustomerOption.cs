using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Umbraco.InvoiceManager.Models.Options
{
    public class AddCustomerOption
    {
        [JsonProperty("id")]
        int Id { get; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("customertype")]
        public int CustomerType { get; set; }

        [JsonProperty("gstnumber")]
        public string GSTNumber { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("zipcode")]
        public string ZipCode { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }
    }
}
