using System.ComponentModel.DataAnnotations;

namespace Northwind.Models
{
    public class CartItemDisplayJSON
    {
        public int id { get; set; }
        public string name { get; set; }
        public int qty { get; set; }
        public decimal price { get; set; }
    }
}