using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG6212_POE_CMCS.Models;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using ApplicationDocument = PROG6212_POE_CMCS.Models.Document;  // Alias your custom Document
using iTextDocument = iText.Layout.Document;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "Lecturer")]
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
            if (ModelState.IsValid)
            {
                // Check if the lecturer already exists
                var lecturer = _context.Lecturers.FirstOrDefault(l => l.Name == claim.Lecturer.Name);

                if (lecturer == null)
                {
                    // Create a new lecturer if they don't exist
                    lecturer = new Lecturer
                    {
                        Name = claim.Lecturer.Name,
                        HourlyRate = claim.Lecturer.HourlyRate
                    };
                    _context.Lecturers.Add(lecturer);
                    await _context.SaveChangesAsync(); // Save lecturer to get LecturerID
                }

                // Link the claim to the lecturer
                claim.LecturerID = lecturer.LecturerID;

                // Calculate the final payment (assuming it's client-side validated)
                claim.FinalPayment = claim.HoursWorked * lecturer.HourlyRate;

                // Handle document upload
                if (supportingDocument != null && supportingDocument.Length > 0)
                {
                    var uploadPath = Path.Combine(_configuration["UploadSettings:UploadPath"], supportingDocument.FileName);
                    using (var stream = new FileStream(uploadPath, FileMode.Create))
                    {
                        await supportingDocument.CopyToAsync(stream);
                    }
                    claim.DocumentPath = uploadPath; // Store document path in the claim
                }

                // Set additional claim fields
                claim.Status = ClaimStatus.Pending; // Default status
                claim.SubmissionDate = DateTime.Now; // Set submission date

                // Save the claim to the database
                _context.Claims.Add(claim);
                await _context.SaveChangesAsync();

                // Redirect to the list of claims (Index action)
                return RedirectToAction(nameof(Index));
            }

            // If model state is not valid, return the view with validation errors
            return View(claim);
        }
        [Authorize(Roles = "Admin, ProgramCoordinator")]
        public IActionResult Approve()
        {
            var claims = _context.Claims.Include(c => c.Lecturer).ToList(); // Include Lecturer details
            return View(claims);
        }

        // POST: Approve Claim
        [HttpPost]
        public async Task<IActionResult> ApproveClaim(int claimId)
        {
            var claim = await _context.Claims.Include(c => c.Lecturer).FirstOrDefaultAsync(c => c.ClaimId == claimId);

            if (claim != null)
            {
                // Validate the claim before approval
                if (claim.HoursWorked <= 0 || claim.Lecturer.HourlyRate <= 0)
                {
                    TempData["ErrorMessage"] = "Invalid claim: Hours worked and hourly rate must be positive.";
                    return RedirectToAction(nameof(Approve));
                }

                // Example of additional business rule: Max hours per week
                if (claim.HoursWorked > 80)
                {
                    TempData["ErrorMessage"] = "Invalid claim: Hours worked exceed the maximum allowed per week.";
                    return RedirectToAction(nameof(Approve));
                }

                // Approve the claim
                claim.Status = ClaimStatus.Approved;
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Claim {claim.ClaimId} approved on {DateTime.Now}."); // Log approval
            }

            return RedirectToAction(nameof(Approve));
        }

        // POST: Reject Claim
        [HttpPost]
        public async Task<IActionResult> RejectClaim(int claimId)
        {
            var claim = await _context.Claims.Include(c => c.Lecturer).FirstOrDefaultAsync(c => c.ClaimId == claimId);

            if (claim != null)
            {
                // Reject the claim
                claim.Status = ClaimStatus.Rejected;
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Claim {claim.ClaimId} rejected on {DateTime.Now}."); // Log rejection
            }

            return RedirectToAction(nameof(Approve));
        }
        [Authorize(Roles = "Admin, ProgramCoordinator")]
        // GET: Submitted Claims
        public IActionResult SubmittedClaims()
        {
            var claims = _context.Claims.Include(c => c.Lecturer).ToList();
            return View(claims);
        }

        // GET: Claim List
        public IActionResult Index()
        {
            var claims = _context.Claims.Include(c => c.Lecturer).ToList();
            return View(claims);
        }
        [Authorize(Roles = "Admin, ProgramCoordinator")]
        public IActionResult HRView()
        {
            var approvedClaims = _context.Claims
                .Include(c => c.Lecturer)
                .Where(c => c.Status == ClaimStatus.Approved)
                .ToList();

            return View(approvedClaims); // Pass the list of approved claims to the view
        }


        
        }
    }

