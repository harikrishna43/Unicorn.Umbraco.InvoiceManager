﻿using Unicorn.Umbraco.InvoiceManager.Commands;
using Unicorn.Umbraco.InvoiceManager.Commands.Invoices;
using Unicorn.Umbraco.InvoiceManager.Enums;
using Unicorn.Umbraco.InvoiceManager.Interfaces;
using Unicorn.Umbraco.InvoiceManager.Models.Options;
using Unicorn.Umbraco.InvoiceManager.Queries;
using Unicorn.Umbraco.InvoiceManager.Queries.Invoices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Web.BackOffice.Controllers;
using Umbraco.Cms.Web.Common.Attributes;
using PuppeteerSharp.Media;
using PuppeteerSharp;

namespace Unicorn.Umbraco.InvoiceManager.Controllers
{
    /// <summary>
    /// API controller for managing redirects.
    /// </summary>
    [PluginController("InvoiceManager")]
    public class InvoiceController: UmbracoAuthorizedApiController
    {
        private readonly IUmbracoMapper _mapper;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ILogger<InvoiceController> _logger;
        
        public InvoiceController(IUmbracoMapper mapper, ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, ILogger<InvoiceController> logger)
        {
            _mapper = mapper;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            _logger = logger;
        }
        [HttpPost]
        public ActionResult CreateInvoice([FromBody] JObject m)
        {
            try
            {
                CreateInvoiceOption model = m.ToObject<CreateInvoiceOption>();
                model.CustomerId = model.Customer.CustomerId;
                var command = _mapper.Map<CreateInvoiceCommand>(model);
                _commandDispatcher.Send(command);
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,"Invoice has not been created.");
                return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult EditInvoice([FromBody] JObject m)
        {
            try
            {
                EditInvoiceOption model = m.ToObject<EditInvoiceOption>();
                model.CustomerId = model.Customer.CustomerId;
                var command = _mapper.Map<EditInvoiceCommand>(model);
                _commandDispatcher.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Invoice has not been created.");
                return BadRequest();
            }
        }

        [HttpGet]
        public ActionResult DeleteInvoice(int invoiceId)
        {
            try
            {
                _commandDispatcher.Send(new DeleteInvoiceCommand { InvoiceId=invoiceId});
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Invoice has not been added.");
                return BadRequest();
            }
        }

        [HttpGet]
        public ActionResult GetInvoices(int page = 1, int limit = 20, int type =2, string text = null, int? customerId = null)
        {
            try
            {
                var query = new GetInvoiceQuery { Limit = limit, Page = page, Text = text, Status = (InvoiceStatus)type };
                var data=_queryDispatcher.Send<GetInvoiceQuery, InvoiceSearchResultQuery>(query);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Download(int invoiceId)
        {
            var data = _queryDispatcher.Send<DownloadInvoiceQuery,InvoiceData>(new DownloadInvoiceQuery { InvoiceId=invoiceId});

            await new BrowserFetcher().DownloadAsync().ConfigureAwait(false);
            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true
            }).ConfigureAwait(false);
            await using var page = await browser.NewPageAsync().ConfigureAwait(false);
            await page.EmulateMediaTypeAsync(MediaType.Print);
            await page.SetContentAsync(data.InvoiceHtmlString);
            var result = await page.GetContentAsync();
            var pdfContent = await page.PdfStreamAsync(new PdfOptions
            {
                Format = PaperFormat.A4,
                PrintBackground = true
            });
            var fileName = $"{data.Invoice.InvoiceNumber}_{data.Invoice.Customer.CustomerId}.pdf";
            HttpContext.Response.Headers.Add("x-filename", fileName);
            HttpContext.Response.Headers.Add("content-type", "application/pdf");
            return File(pdfContent, "application/pdf", fileName);
            //return File(pdfContent, "application/pdf", $"{data.Invoice.InvoiceNumber}_{data.Invoice.Customer.CustomerId}.pdf");
        }
    }
}