using System.Linq;
using Data.Configuration;
using Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly NorthwindContext _northwindContext;
        private readonly DatabaseConfiguration _configuration;

        public ProductController(NorthwindContext northwindContext,
            IOptionsMonitor<DatabaseConfiguration> configuration)
        {
            _northwindContext = northwindContext;
            _configuration = configuration.CurrentValue;
        }

        public IActionResult Index()
        {
            var topProducts = _configuration.TopProducts == 0
                ? _northwindContext.Products.Count()
                : _configuration.TopProducts;
            var products = _northwindContext.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Take(topProducts)
                .ToList();
            return View(products);
        }
    }
}
