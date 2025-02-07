using Microsoft.AspNetCore.Mvc;
using VintageGroupCar.Dal;
using VintageGroupCar.Models;

namespace VintageGroupCar.Controllers
{
    public class RaduniController : Controller
    {
        private readonly AppDbContext _dbContext;

        public RaduniController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var raduni = _dbContext.Raduni.ToList();
            return View(raduni);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
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

            return RedirectToAction("Index", "Home");
        }
    }
}
