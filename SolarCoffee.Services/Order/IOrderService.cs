using System;
using System.Collections.Generic;
using System.Text;

namespace SolarCoffee.Services.Order
{
    public interface IOrderService
    {
        public List<Data.Models.SalesOrder> GetOrders();
        public ServiceResponse<bool> GenerateInvoiceForOrder(Data.Models.SalesOrder order);
        public ServiceResponse<bool> MarkFulfilled(int id);
    }
}
