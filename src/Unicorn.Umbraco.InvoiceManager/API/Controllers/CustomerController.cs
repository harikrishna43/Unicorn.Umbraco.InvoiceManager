using Unicorn.Umbraco.InvoiceManager.Commands;
using Unicorn.Umbraco.InvoiceManager.Enums;
using Unicorn.Umbraco.InvoiceManager.Interfaces;
using Unicorn.Umbraco.InvoiceManager.Models.Options;
using Unicorn.Umbraco.InvoiceManager.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Web.BackOffice.Controllers;
using Umbraco.Cms.Web.Common.Attributes;
using J2N.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace Unicorn.Umbraco.InvoiceManager.Controllers
{
    /// <summary>
    /// API controller for managing Customer.
    /// </summary>
    [PluginController("InvoiceManager")]
    public class CustomerController : UmbracoAuthorizedApiController
    {
        private readonly IUmbracoMapper _mapper;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(IUmbracoMapper mapper, ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, ILogger<CustomerController> logger)
        {
            _mapper = mapper;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            _logger = logger;
        }
        [HttpPost]
        public ActionResult AddCustomer([FromBody] JObject m)
        {
            try
            {

                AddCustomerOption model = m.ToObject<AddCustomerOption>();

                var customerIsExists = _queryDispatcher.Send<IsCustomerExistsQuery, bool>(new IsCustomerExistsQuery(){ PhoneNumber=model.Phone});
                if (customerIsExists)
                {
                    return Conflict($"Customer is alraedy exist with {model.Phone} phone number");
                }
                var command = _mapper.Map<AddCustomerCommand>(model);
                var query = new GetCustomerQuery { Limit = 20, Page = 1, Text = model.Phone, CustomerType = (CustomerType)2 };
                var data = _queryDispatcher.Send<GetCustomerQuery, CustomerSearchResultQuery>(query);
                if (data.Items.Count() > 0)
                {
                    return Conflict("Customer already added with this phone number.");
                }
                _commandDispatcher.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Customer has not been added.");
                return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult EditCustomer([FromBody] JObject m)
        {
            try
            {
                EditCustomerOption model = m.ToObject<EditCustomerOption>();
                var command = _mapper.Map<EditCustomerCommand>(model);
                _commandDispatcher.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Customer has not been added.");
                return BadRequest();
            }
        }

        [HttpGet]
        public ActionResult DeleteCustomer(int customerId)
        {
            try
            {
                _commandDispatcher.Send(new DeleteCustomerCommand { CustomerId = customerId });
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Customer has not been added.");
                return BadRequest();
            }
        }

        [HttpGet]
        public ActionResult GetCustomers(int page = 1, int limit = 20, int type = 2, string text = null, int? customerId = null)
        {
            try
            {
                var query = new GetCustomerQuery { Limit = limit, Page = page, Text = text, CustomerType = (CustomerType)type };
                var data = _queryDispatcher.Send<GetCustomerQuery, CustomerSearchResultQuery>(query);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return BadRequest();
            }
        }
        [HttpGet]
        public ActionResult GetAllCustomers()
        {
            try
            {
                var data = _queryDispatcher.Send<GetAllCustomerQuery, ICustomer[]>(new GetAllCustomerQuery());
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return BadRequest();
            }
        }

    }
}
