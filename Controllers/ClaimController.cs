using Microsoft.AspNetCore.Mvc;

namespace PROG6212_POE_CMCS.Controllers
{
    public class ClaimController : Controller
    {
        // Show the claim submission form
        public IActionResult Create()
        {
            return View();
        }

        // Show the claims approval view
        public IActionResult Approve()
        {
            return View();
        }

        // Placeholder for the list of claims
        public IActionResult Index()
        {
            return View();
        }
    }
}
