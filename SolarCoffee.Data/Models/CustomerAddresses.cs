using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SolarCoffee.Data.Models
{
    public class CustomerAddresses
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        [MaxLength(100)]
        public String AddressLine1 { get; set; }

        [MaxLength(100)]
        public String AddressLine2 { get; set; }

        [MaxLength(100)] 
        public String City { get; set; }
        [MaxLength(100)]
        public String State { get; set; }
        [MaxLength(5)]
        public String PostalCode { get; set; }
        [MaxLength(50)]
        public String Country { get; set; }

    }
}
