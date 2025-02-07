using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VintageGroupCar.Dal;
using VintageGroupCar.Models;

namespace VintageGroupCar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

            _logger = logger;
        }
     
        public IActionResult Index()
        {
            var raduni = _dbContext.Raduni.ToList(); // Recupera i raduni dal DB

            // Se non ci sono raduni, restituisci una lista vuota invece di null
            return View(raduni ?? new List<Raduno>());
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
            if (!ModelState.IsValid)
            {
                return View(model); // Ritorna la vista con il modello per mostrare gli errori di validazione
            }

            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la creazione del raduno.");
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }
}
