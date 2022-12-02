using System;
using System.ComponentModel.DataAnnotations.Schema;
// using System.Data.Entity.Core;
using System.ComponentModel.DataAnnotations;
namespace Northwind.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailsId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
         [Column(TypeName = "decimal(18,4)")]
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(5,3)")]
        public decimal Discount { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
