using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG6212_POE_CMCS.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PROG6212_POE_CMCS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;  // Add the AppDbContext

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;  // Initialize the AppDbContext
        }

        public async Task<IActionResult> Index()
        {
            // Eagerly load the Lecturer information for each claim
            var claims = await _context.Claims
                                       .Include(c => c.Lecturer)  // Include Lecturer details
                                       .ToListAsync();

            // Pass the list of claims to the view
            return View(claims);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [AllowAnonymous] // Allow unauthenticated access to the Error page
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
      
       
    }
}
