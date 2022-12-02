using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace Northwind.Models
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext(DbContextOptions<NorthwindContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
            SaveChanges();
        }
        public void EditCustomer(Customer customer)
        {
            var customerToUpdate = Customers.FirstOrDefault(c => c.CustomerId == customer.CustomerId);
            customerToUpdate.Address = customer.Address;
            customerToUpdate.City = customer.City;
            customerToUpdate.Region = customer.Region;
            customerToUpdate.PostalCode = customer.PostalCode;
            customerToUpdate.Country = customer.Country;
            customerToUpdate.Phone = customer.Phone;
            customerToUpdate.Fax = customer.Fax;
            SaveChanges();
        }
        public CartItem AddToCart(CartItemJSON cartItemJSON)
        {
            int CustomerId = Customers.FirstOrDefault(c => c.Email == cartItemJSON.email).CustomerId;
            int ProductId = cartItemJSON.id;
            // check for duplicate cart item
            CartItem cartItem = CartItems.FirstOrDefault(ci => ci.ProductId == ProductId && ci.CustomerId == CustomerId);
            if (cartItem == null)
            {
                     // this is a new cart item
                cartItem = new CartItem()
                {
                    CustomerId = CustomerId,
                    ProductId = cartItemJSON.id,
                    Quantity = cartItemJSON.qty
                };
                CartItems.Add(cartItem);
            }
            else
            {
                // for duplicate cart item, simply update the quantity
                cartItem.Quantity += cartItemJSON.qty;
            }
            SaveChanges();
            cartItem.Product = Products.Find(cartItem.ProductId);
            return cartItem;
        }
        public void RemoveFromCart(int id)
        {
            CartItem cartItem = CartItems.FirstOrDefault(ci => ci.CartItemId == id);
            CartItems.Remove(cartItem);
            SaveChanges();
        }
        // public void RemoveAllFromCart(int id)
        // {
        //     var items = CartItems.Where(ci => ci.CustomerId == id).ToList();
        //     CartItems.RemoveRange(items);

        //     SaveChanges();
        // }
        public void AddOrder(Order order, int id)
        {
            // STEP 1: Add to order table
            Orders.Add(order);
            SaveChanges();
            // STEP 2: Add to OrderDetail table
            var items = CartItems.Where(ci => ci.CustomerId == id).ToList();
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach(var item in items){
                OrderDetail od = new OrderDetail();
                od.OrderId = order.OrderId;
                od.ProductId = item.ProductId;
                od.UnitPrice = Products.FirstOrDefault(p => p.ProductId == item.ProductId).UnitPrice;
                od.Quantity = item.Quantity;
                od.Discount = 0;
                orderDetails.Add(od);
            }
            OrderDetails.AddRange(orderDetails);
            // Step 3: Remove from CartItems table
            CartItems.RemoveRange(items);

            SaveChanges();
        }
    }
}
