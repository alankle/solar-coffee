using SolarCoffee.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarCoffee.Web.Serialization
{
    public class ProductMapper
    {
       /// <summary>
       /// DataMOdel to Product MOdel
       /// </summary>
       /// <param name="product"></param>
       /// <returns></returns>
        public static productModel SerialProductModel(Data.Models.Product product)
        {
            return new productModel
            {
                    Id = product.Id,
                    CreatedOn = product.CreatedOn,
                    UpdatedOn = product.UpdatedOn, 
                    Name = product.Name ,
                    Description = product.Description,
                    Price = product.Price, 
                    IsTaxable = product.IsTaxable,
                    IsArchived = product.IsArchived
                };
        }

        /// <summary>
        /// Product MOdel to  DataMOdel 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static Data.Models.Product SerialProductModel(productModel product)
        {
            return new Data.Models.Product
            {
                Id = product.Id,
                CreatedOn = product.CreatedOn,
                UpdatedOn = product.UpdatedOn,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IsTaxable = product.IsTaxable,
                IsArchived = product.IsArchived
            };
        }

    }
}
