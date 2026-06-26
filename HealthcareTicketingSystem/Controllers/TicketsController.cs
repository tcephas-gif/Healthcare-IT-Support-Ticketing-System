using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthcareTicketingSystem.Models;
using HealthcareTicketingSystem.Data;

public class TicketsController : Controller
{
    private readonly ApplicationDbContext _context;

    public TicketsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(
        string searchString,
        string statusFilter,
        string priorityFilter,
        string categoryFilter)
    {
        var ticketsQuery = _context.Tickets.AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            ticketsQuery = ticketsQuery.Where(t =>
                t.Title.Contains(searchString) ||
                t.Description.Contains(searchString) ||
                t.Department.Contains(searchString) ||
                t.Category.Contains(searchString) ||
                t.AssignedTo.Contains(searchString) ||
                t.ReportedBy.Contains(searchString));
        }

        if (!string.IsNullOrEmpty(statusFilter))
        {
            ticketsQuery = ticketsQuery.Where(t => t.Status == statusFilter);
        }

        if (!string.IsNullOrEmpty(priorityFilter))
        {
            ticketsQuery = ticketsQuery.Where(t => t.Priority == priorityFilter);
        }

        if (!string.IsNullOrEmpty(categoryFilter))
        {
            ticketsQuery = ticketsQuery.Where(t => t.Category == categoryFilter);
        }

        var tickets = await ticketsQuery.ToListAsync();

        var categoryCounts = tickets
            .Where(t => !string.IsNullOrEmpty(t.Category))
            .GroupBy(t => t.Category)
            .Select(g => new
            {
                Category = g.Key,
                Count = g.Count()
            })
            .ToList();

        ViewBag.CategoryLabels = categoryCounts.Select(x => x.Category).ToArray();
        ViewBag.CategoryData = categoryCounts.Select(x => x.Count).ToArray();

        ViewBag.TotalTickets = tickets.Count;
        ViewBag.OpenTickets = tickets.Count(t => t.Status == "Open");
        ViewBag.InProgressTickets = tickets.Count(t => t.Status == "In Progress");
        ViewBag.HighPriorityTickets = tickets.Count(t => t.Priority == "High" || t.Priority == "Critical");

        ViewBag.TopCategory = categoryCounts
            .OrderByDescending(g => g.Count)
            .Select(g => g.Category)
            .FirstOrDefault() ?? "None";

        ViewBag.TopTechnician = tickets
            .Where(t => !string.IsNullOrEmpty(t.AssignedTo))
            .GroupBy(t => t.AssignedTo)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .FirstOrDefault() ?? "None";

        ViewBag.SearchString = searchString;
        ViewBag.StatusFilter = statusFilter;
        ViewBag.PriorityFilter = priorityFilter;
        ViewBag.CategoryFilter = categoryFilter;

        return View(tickets);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var ticket = await _context.Tickets
            .FirstOrDefaultAsync(m => m.TicketId == id);

        if (ticket == null) return NotFound();

        return View(ticket);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("TicketId,Title,Description,Department,Category,Priority,Status,ReportedBy,AssignedTo")] Ticket ticket)
    {
        if (ModelState.IsValid)
        {
            ticket.CreatedDate = DateTime.Now;

            _context.Add(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(ticket);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var ticket = await _context.Tickets.FindAsync(id);

        if (ticket == null) return NotFound();

        return View(ticket);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("TicketId,Title,Description,Department,Category,Priority,Status,ReportedBy,AssignedTo,CreatedDate")] Ticket ticket)
    {
        if (id != ticket.TicketId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(ticket);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(ticket.TicketId)) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(ticket);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var ticket = await _context.Tickets
            .FirstOrDefaultAsync(m => m.TicketId == id);

        if (ticket == null) return NotFound();

        return View(ticket);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var ticket = await _context.Tickets.FindAsync(id);

        if (ticket != null)
        {
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool TicketExists(int id)
    {
        return _context.Tickets.Any(e => e.TicketId == id);
    }
}