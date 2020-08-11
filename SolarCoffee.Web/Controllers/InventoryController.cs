using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Inventory;
using SolarCoffee.Web.Serialization;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {

        private readonly ILogger<InventoryController> _logger;
        private readonly IInventoryService _inventoryService;

        public InventoryController(ILogger<InventoryController> logger, IInventoryService inventoryService)
        {
            _logger = logger;
            _inventoryService = inventoryService;
        }

        [HttpGet("/api/inventory")]
        public ActionResult GetCurrentInventory()
        {
            _logger.LogInformation("Getting all inventory...");
            var inventory = _inventoryService.GetCurrentInventory()
                .Select(pi => new ProductInventoryModel
                {
                    Id = pi.Id,
                    Product = ProductMapper.SerialProductModel(pi.Product),
                    IdealQuantity = pi.IdealQuantity,
                    QuantityOnHand = pi.QuantityOnHand
                }
                )
                .OrderBy(inv => inv.Product.Name)
                .ToList();

            return Ok(inventory);
                 
        }
        [HttpPatch("/api/inventory")]
        public ActionResult UpdateInventory([FromBody]ShipmentModel shipment)
        {
            _logger.LogInformation("Updating inventory" +
                $"for {shipment.ProductId } - " + 
                $" Adjustement: {shipment.Adjustment} "
                );

            var id = shipment.ProductId;
            var adjustement = shipment.Adjustment;
            var inventory = _inventoryService.UpdateUnitsAvailable(id, adjustement);

            return Ok(inventory);
        }

    }
}
