using Microsoft.AspNetCore.Mvc;

public class LecturerController : Controller
{
    private readonly AppDbContext _context;

    public LecturerController(AppDbContext context)
    {
        _context = context;
    }

    // View the list of lecturers
    public IActionResult Index()
    {
        var lecturers = _context.Lecturers.ToList(); // Fetch lecturers from the database
        return View(lecturers); // Pass the list of lecturers to the view
    }

    // Update Lecturer (POST)
    [HttpPost]
    public async Task<IActionResult> UpdateLecturer(int lecturerId, string name, decimal hourlyRate)
    {
        var lecturer = await _context.Lecturers.FindAsync(lecturerId);
        if (lecturer != null)
        {
            // Update lecturer information
            lecturer.Name = name;
            lecturer.HourlyRate = hourlyRate;

            // Save changes to the database
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        return Json(new { success = false });
    }
}
