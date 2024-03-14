using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1Test.Models.EntityFramework;

namespace WebApplication1Test.Controllers
{

 

    public class ProductController : Controller
    {
        private readonly NewTestingContext _context;

    

        public ProductController(NewTestingContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Product.ToList();
            return View(products);
        }
    }
}