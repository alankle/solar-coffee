using SolarCoffee.Data.Models;
using SolarCoffee.Web.Serialization;
using SolarCoffee.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolarCoffee.Web.Controllers
{
    /// <summary>
/// handles mapping order data models to and from related view Models 
/// </summary>
    public static class OrderMapper
    {

        public static SalesOrder SerializeInvoiceToOrder(invoiceModel invoice)
        {
            var salesOrderItems = invoice.LineItems
                .Select(item => new SalesOrderItem
                {
                    Id = item.Id,
                    Quantity = item.Quantity,
                    Product = ProductMapper.SerialProductModel(item.Product)
                }).ToList();


            return new SalesOrder
            {
                SalesOrderItems = salesOrderItems,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
        }
        /// <summary>
        /// Maps a collections od SalesOrders (data) to OrdersMOdels (ViewMod)
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public static List<OrderModel> SerialOrderToViewModels(IEnumerable<SalesOrder> orders)
        {
            return (List<OrderModel>)orders.Select(order => new OrderModel {
            Id =order.Id,
            CreatedOn = order.CreatedOn,
            UpdatedOn = order.UpdatedOn,
            SalesOrderItems = serializeOrderItems(order.SalesOrderItems),
            Customer = CustomerMapper.SerializeCustomer(order.Customer),
            IsPaid = order.IsPaid
            
            });
        }

   /// <summary>
   ///  Maps a collection of salesOrderItem (data) to salesorderItemModels (view models)
   /// </summary>
   /// <param name="orderItems"></param>
   /// <returns></returns>
        private static List<SalesOrderItemModel> serializeOrderItems(IEnumerable<SalesOrderItem> orderItems)
        {
            return orderItems.Select(item => new SalesOrderItemModel
            {
                Id = item.Id,
                Quantity = item.Quantity,
                Product = ProductMapper.SerialProductModel(item.Product)
            }).ToList();
        }

    }
}