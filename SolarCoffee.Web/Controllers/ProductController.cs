﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Product;
using SolarCoffee.Web.Serialization;

namespace SolarCoffee.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet("/api/product")]
        public ActionResult GetProduct()
        {
             _logger.LogInformation("Getting all products");

            var products =_productService.GetAllProducts();
            var productViewModels = products.Select(ProductMapper.SerialProductModel);
            return Ok(productViewModels);
        }
        [HttpPatch("/api/product/{id}")]
        public ActionResult ArchiveProduct(int id )
        {
            _logger.LogInformation("Archiving product");
            var archiveResult =_productService.ArchiveProduct(id);
            return Ok(archiveResult);
        }
    }
}
