using System;
using System.Collections.Generic;
using System.Text;

namespace SolarCoffee.Services.Inventory
{
    public interface IInventoryService
    {
        public List<Data.Models.ProductInventory> GetCurrentInventory();
        public ServiceResponse<Data.Models.ProductInventory> UpdateUnitsAvailable(int id, int adjustements);
        public Data.Models.ProductInventory GetByIdProduct(int id);
       
        public List<Data.Models.ProductInventorySnapshot> GetSnapshotHistory();
    }
}
