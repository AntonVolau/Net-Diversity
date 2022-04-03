using System.Linq;
using Data.Context;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly NorthwindContext _northwindContext;

        public CategoryController(NorthwindContext northwindContext)
        {
            _northwindContext = northwindContext;
        }

        public IActionResult Index()
        {
            var categories = _northwindContext.Categories.ToList();
            return View(categories);
        }
    }
}
