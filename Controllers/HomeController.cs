using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VintageGroupCar.Dal;
using VintageGroupCar.Models;

namespace VintageGroupCar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public IActionResult Create(Raduno model, IFormFile immagineFile)
        {
            if (immagineFile != null && immagineFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    immagineFile.CopyTo(ms);
                    model.Immagine = ms.ToArray();
                }
            }

            _dbContext.Raduni.Add(model);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
