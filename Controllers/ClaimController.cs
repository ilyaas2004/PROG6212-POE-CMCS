using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG6212_POE_CMCS.Models;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace PROG6212_POE_CMCS.Controllers
{
    public class ClaimController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ClaimController> _logger;
        private readonly IConfiguration _configuration;

        // Constructor: Initialize the context, logger, and configuration
        public ClaimController(AppDbContext context, ILogger<ClaimController> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        // GET: /Claim/Create
        public IActionResult Create()
        {
            ViewBag.Lecturers = _context.Lecturers.ToList(); // Fetch lecturers to populate dropdown
            return View();
        }

        // POST: /Claim/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Claim claim, IFormFile supportingDocument)
        {
            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                // Check if a document was uploaded
                if (supportingDocument != null && supportingDocument.Length > 0)
                {
                    try
                    {
                        // Specify the upload path (use configuration for flexibility)
                        var uploadPath = Path.Combine(
                            _configuration["UploadSettings:UploadPath"] ?? string.Empty,
                            supportingDocument.FileName
                        );

                        // Save the uploaded file
                        using (var stream = new FileStream(uploadPath, FileMode.Create))
                        {
                            await supportingDocument.CopyToAsync(stream);
                        }

                        // Set the document path in the claim
                        claim.DocumentPath = uploadPath;

                        // Set initial status and submission date using the enum
                        claim.Status = ClaimStatus.Pending; // Directly use the enum
                        claim.SubmissionDate = DateTime.Now; // Set submission date
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error occurred while uploading the document.");
                        ModelState.AddModelError("", "An error occurred while processing your request. Please try again.");
                    }
                }
                else
                {
                    ModelState.AddModelError("supportingDocument", "A supporting document is required.");
                }

                // If everything is valid, add the claim to the context
                if (ModelState.IsValid)
                {
                    _context.Claims.Add(claim);
                    await _context.SaveChangesAsync(); // Save changes to the database
                    return RedirectToAction(nameof(Index)); // Redirect to the Index action
                }
            }

            // If model state is invalid, fetch lecturers again for the view
            ViewBag.Lecturers = _context.Lecturers.ToList();
            return View(claim); // Return to the Create view with the current claim data
        }

        // POST: /Claim/ApproveClaim
        [HttpPost]
        public async Task<IActionResult> ApproveClaim(int claimId)
        {
            var claim = await _context.Claims.FindAsync(claimId); // Find the claim by ID
            if (claim != null)
            {
                claim.Status = ClaimStatus.Approved; // Set status to Approved using enum
                await _context.SaveChangesAsync(); // Save changes to the database
                _logger.LogInformation($"Claim {claim.ClaimId} approved by manager on {DateTime.Now}."); // Log approval
            }

            return RedirectToAction("Approve"); // Redirect to the Approve action
        }

        // POST: /Claim/RejectClaim
        [HttpPost]
        public async Task<IActionResult> RejectClaim(int claimId)
        {
            var claim = await _context.Claims.FindAsync(claimId); // Find the claim by ID
            if (claim != null)
            {
                claim.Status = ClaimStatus.Rejected; // Set status to Rejected using enum
                await _context.SaveChangesAsync(); // Save changes to the database
                _logger.LogInformation($"Claim {claim.ClaimId} rejected by manager on {DateTime.Now}."); // Log rejection
            }

            return RedirectToAction("Approve"); // Redirect to the Approve action
        }


        public IActionResult SubmittedClaims()
        {
            var claims = _context.Claims.Include(c => c.Lecturer).ToList(); // or however you retrieve your claims
            return View(claims);
        }
        public IActionResult Index()
        {
            var claims = _context.Claims.Include(c => c.Lecturer).ToList(); // Include Lecturer details
            return View(claims); // Ensure you're returning the list of claims to the view
        }
        public IActionResult Approve()
        {
            var claims = _context.Claims.Include(c => c.Lecturer).ToList();
            return View(claims); // Ensure you're returning the list of claims to the view
        }

    }
}