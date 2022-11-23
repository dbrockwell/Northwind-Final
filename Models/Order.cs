using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Northwind.Models
{
    public class Order
    {
        public int OrderId {get; set;}
        public int CustomerId {get; set;}
        public int EmployeeId {get; set;}
        //public DateTime OrderDate {get; set;}
        //public DateTime RequiredDate {get; set;}
        //public DateTime ShippedDate {get; set;}
        [Required]
        public int ShipVia {get; set;}
        public double Freight {get; set;}
        [Required]
        public string ShipName {get; set;}
        [Required]
        public string ShipAddress {get; set;}
        [Required]
        public string ShipCity {get; set;}
        [Required]
        public string ShipRegion {get; set;}
        [Required]
        public string ShipPostalCode {get; set;}
        [Required]
        public string ShipCountry {get; set;}
    }
        
}