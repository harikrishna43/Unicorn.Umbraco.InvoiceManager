using Unicorn.Umbraco.InvoiceManager.Interfaces;
using Unicorn.Umbraco.InvoiceManager.Models.Options;
using Unicorn.Umbraco.InvoiceManager.Queries.Invoices;
using Microsoft.AspNetCore.Routing.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Hosting;
using Umbraco.Cms.Core.Mapping;
using Superpower.Model;

namespace Unicorn.Umbraco.InvoiceManager.Queries.Invoice.QueryHandler
{
    public class DownloadInvoiceHandler : IQueryHandler<DownloadInvoiceQuery, InvoiceData>
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IUmbracoMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;
        public DownloadInvoiceHandler(IInvoiceService invoiceService, IHostingEnvironment hostingEnvironment, IUmbracoMapper mapper)
        {
            _invoiceService = invoiceService;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }
        public InvoiceData Handle(DownloadInvoiceQuery query)
        {
            var data = _invoiceService.GetInvoiceById(query.InvoiceId);
            return new InvoiceData { Invoice = data, InvoiceHtmlString = GetHtmlString(data) };
        }

        private string GetHtmlString(IInvoice data)
        {
            string dir = _hostingEnvironment.MapPathContentRoot("~/App_Plugins/Unicorn.Umbraco.InvoiceManager/InvoiceTemplate");
            var file = dir + "/invoice.html";
            string htmlTemplate = System.IO.File.ReadAllText(file);
            htmlTemplate = htmlTemplate.Replace("{{invoicenumber}}", data.InvoiceNumber);
            htmlTemplate = htmlTemplate.Replace("{{name}}", data.Customer.Name);
            htmlTemplate = htmlTemplate.Replace("{{address}}", data.Customer.Address);
            htmlTemplate = htmlTemplate.Replace("{{phone}}", data.Customer.Phone);
            htmlTemplate = htmlTemplate.Replace("{{invoicedate}}", data.InvoiceDate.ToString("MMMM dd, yyyy"));
            htmlTemplate = htmlTemplate.Replace("{{duedate}}", data.DueDate.ToString("MMMM dd, yyyy"));
            //<tr> <td class="service">{{note}}</td> <td class="desc">{{description}}</td> <td class="unit">${{unitprice}}</td> <td class="qty">{{qty}}</td> <td class="total">${{total}}</td></tr>
            string item = String.Empty;
            foreach (var invoiceItem in data.InvoiceData)
            {
                item += "<tr> " +
                    $"<td class=\"service\">{invoiceItem.InvoiceNote}</td> " +
                    $"<td class=\"desc\">{invoiceItem.Description}</td> " +
                    $"<td class=\"qty\">{invoiceItem.Quantity}</td> " +
                    $"<td class=\"unit\">${invoiceItem.UnitPrice.ToString("0.00")}</td> " +
                    $"<td class=\"total\">${invoiceItem.TaxableAmount.ToString("0.00")}</td>" +
                    "</tr>";
            }
            htmlTemplate = htmlTemplate.Replace("#items#", item);
            htmlTemplate = htmlTemplate.Replace("{{total}}", data.TaxableAmount.ToString("0.00"));
            htmlTemplate = htmlTemplate.Replace("{{totaltax}}", data.TotalTax.ToString("0.00"));
            htmlTemplate = htmlTemplate.Replace("{{totalamount}}", data.TotalAmount.ToString("0.00"));
            return htmlTemplate;
        }
    }
}
