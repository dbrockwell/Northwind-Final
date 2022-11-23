using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;
using Microsoft.EntityFrameworkCore;

namespace Northwind.Controllers
{
    public class APIController : Controller
    {
        // this controller depends on the NorthwindRepository
        private NorthwindContext _northwindContext;
        public APIController(NorthwindContext db) => _northwindContext = db;

        [HttpGet, Route("api/product")]
        // returns all products
        public IEnumerable<Product> Get() => _northwindContext.Products.OrderBy(p => p.ProductName);
        [HttpGet, Route("api/product/{id}")]
        // returns specific product
        public Product Get(int id) => _northwindContext.Products.FirstOrDefault(p => p.ProductId == id);
        [HttpGet, Route("api/product/discontinued/{discontinued}")]
        // returns all products where discontinued = true/false
        public IEnumerable<Product> GetDiscontinued(bool discontinued) => _northwindContext.Products.Where(p => p.Discontinued == discontinued).OrderBy(p => p.ProductName);
        [HttpGet, Route("api/category/{CategoryId}/product")]
        // returns all products in a specific category
        public IEnumerable<Product> GetByCategory(int CategoryId) => _northwindContext.Products.Where(p => p.CategoryId == CategoryId).OrderBy(p => p.ProductName);
        [HttpGet, Route("api/category/{CategoryId}/product/discontinued/{discontinued}")]
        // returns all products in a specific category where discontinued = true/false
        public IEnumerable<Product> GetByCategoryDiscontinued(int CategoryId, bool discontinued) => _northwindContext.Products.Where(p => p.CategoryId == CategoryId && p.Discontinued == discontinued).OrderBy(p => p.ProductName);
        [HttpPost, Route("api/addtocart")]
        // adds a row to the cartitem table
        public CartItem Post([FromBody] CartItemJSON cartItem) => _northwindContext.AddToCart(cartItem);

        [HttpGet, Route("api/cartitems/count")]
        public int CartItemCount(){
            var email = User.Identity.Name;
           return  _northwindContext.CartItems.Where(ci => ci.Customer.Email == email).Sum(ci => ci.Quantity);
        }

        // [HttpGet, Route("api/cartitems/products")]
        // public IEnumerable<CartItemDisplayJSON> CartProducts(){
        //     var email = User.Identity.Name;
        //     var Items = _northwindContext.CartItems.Where(ci => ci.Customer.Email == email).Select(c => new CartItemDisplayJSON { c.CartItemId, c.Product.ProductName, c.Quantity}).ToList();

        //    return  Items;
        // }
    }
}