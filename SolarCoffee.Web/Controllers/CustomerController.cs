﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Customer;
using SolarCoffee.Web.Serialization;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpPost("/api/customer")]
        public ActionResult CreateCustomer([FromBody] CustomerModel customer)
        {
            _logger.LogInformation("Creating a new customer");
            customer.CreatedOn = DateTime.UtcNow;
            customer.UpdatedOn = DateTime.UtcNow;

            var customerData = CustomerMapper.SerializeCustomer(customer);
            var newCustomer = _customerService.CreateCustomer(customerData);
            return Ok(newCustomer);

        }
        [HttpGet("/api/customer")]
        public ActionResult GetCustomer([FromBody] CustomerModel customer)
        {
            _logger.LogInformation("Getting customer");

            var customers = _customerService.GetAllCustomers();

            var customerModels = customers.Select(customer => new CustomerModel {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PrimaryAddress = CustomerMapper.MapCustomerAddress(customer.PrimaryAddresses),
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn            
            }
            )
               .OrderByDescending(customer => customer.CreatedOn)
               .ToList();

            return Ok(customerModels);            

        }

        [HttpDelete("/api/customer{id}")]
        public ActionResult DeleteCustomer(int id)
        {
            _logger.LogInformation("Delete Customer");
            var response = _customerService.DeleteCustomer(id);

            return Ok(response);
        }

    }
}
