using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SolarCoffee.Web.ViewModels
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        [MaxLength(32)] public string FirstName { get; set; }
        [MaxLength(32)] public string LastName { get; set; }
        public CustomerAddressModel PrimaryAddress { get; set; }
    }

    public class CustomerAddressModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        
        public String AddressLine1 { get; set; }        
        public String AddressLine2 { get; set; }        
        public String City { get; set; }       
        public String State { get; set; }       
        public String PostalCode { get; set; }        
        public String Country { get; set; }
    }
}

