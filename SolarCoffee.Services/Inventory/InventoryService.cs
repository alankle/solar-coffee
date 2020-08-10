using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolarCoffee.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly SolarDbContext _db;
        private readonly ILogger<InventoryService> _logger;

        public InventoryService(SolarDbContext db, ILogger<InventoryService> logger)
        {
            _db = db;
            _logger = logger;
        }
        private void CreateSnapShot(ProductInventory inventory)
        {
            var snapshot = new ProductInventorySnapshot {
                SnapshotTime = DateTime.UtcNow,
                Product = inventory.Product,
                QuantityOnHand = inventory.QuantityOnHand
            };
            _db.Add(snapshot);
            //_db.SaveChanges();
        }
        
        public ProductInventory GetByIdProduct(int id)
        {
            return _db.ProductInventories
                .Include(pi => pi.Product)
                .FirstOrDefault(pi => pi.Product.Id == id);
                
        }

        public List<ProductInventory> GetCurrentInventory()
        {
            return _db.ProductInventories
                .Include(pi => pi.Product)
                .Where(pi => !pi.Product.IsArchived)
                .ToList();
        }
        /// <summary>
        /// return snapshot history for the previou 6 hours
        /// </summary>
        /// <returns></returns>
        public List<ProductInventorySnapshot> GetSnapshotHistory()
        {

            var earliest = DateTime.UtcNow - TimeSpan.FromHours(6);

            return _db.ProductInventorySnapshots
                .Include(snap => snap.Product)
                .Where(snap => snap.SnapshotTime > earliest && !snap.Product.IsArchived)
                .ToList();
        }
        /// <summary>
        /// Updates number of units available of the provided product id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="adjustements">number of units added / removed from inventory</param>
        /// <returns></returns>
        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustements)
        {
            try
            {
                var inventory = _db.ProductInventories
                    .Include(inv => inv.Product)
                    .First(inv => inv.Product.Id == id);

                inventory.QuantityOnHand += adjustements;

                try
                {
                    CreateSnapShot(inventory);
                }
                catch (Exception)
                {

                    _logger.LogError("Create Snpshot error");

                }

                _db.SaveChanges();

                return new ServiceResponse<ProductInventory> {
                    Data = inventory,
                    IsSuccess = true,
                    Message = $"product {id} quantity adjusted",
                    Time = DateTime.UtcNow
                };
                    
            }
            catch (Exception)
            {

                return new ServiceResponse<ProductInventory>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = $"Error with productuct {id} quantity adjusted",
                    Time = DateTime.UtcNow
                };
            }
        }
    }
}
