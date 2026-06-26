using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthcareTicketingSystem.Data;

namespace HealthcareTicketingSystem.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var tickets = await _context.Tickets.ToListAsync();

            // Dashboard Cards
            ViewBag.TotalTickets = tickets.Count;
            ViewBag.OpenTickets = tickets.Count(t => t.Status == "Open");
            ViewBag.InProgressTickets = tickets.Count(t => t.Status == "In Progress");
            ViewBag.HighPriorityTickets = tickets.Count(t => t.Priority == "High" || t.Priority == "Critical");

            // Tickets by Category Chart
            var categoryCounts = tickets
                .GroupBy(t => t.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    Count = g.Count()
                })
                .ToList();

            ViewBag.CategoryLabels = categoryCounts.Select(x => x.Category).ToArray();
            ViewBag.CategoryData = categoryCounts.Select(x => x.Count).ToArray();

            // Tickets by Priority Chart
            var priorityCounts = tickets
                .GroupBy(t => t.Priority)
                .Select(g => new
                {
                    Priority = g.Key,
                    Count = g.Count()
                })
                .ToList();

            ViewBag.PriorityLabels = priorityCounts.Select(x => x.Priority).ToArray();
            ViewBag.PriorityData = priorityCounts.Select(x => x.Count).ToArray();

            // Ticket Status Chart
            var statusCounts = tickets
                .GroupBy(t => t.Status)
                .Select(g => new
                {
                    Status = g.Key,
                    Count = g.Count()
                })
                .ToList();

            ViewBag.StatusLabels = statusCounts.Select(x => x.Status).ToArray();
            ViewBag.StatusData = statusCounts.Select(x => x.Count).ToArray();

            ViewBag.RecentTickets = tickets
    .OrderByDescending(t => t.CreatedDate)
    .Take(5)
    .ToList();
            return View();
        }
    }
}