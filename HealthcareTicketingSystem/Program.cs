using HealthcareTicketingSystem.Data;
using HealthcareTicketingSystem.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Connect the app to SQL Server locally and SQLite on Railway/cloud
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
    else
    {
        options.UseSqlite("Data Source=healthcaretickets.db");
    }
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Create database and add clean demo tickets if the database is empty
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (app.Environment.IsDevelopment())
    {
        context.Database.Migrate();
    }
    else
    {
        context.Database.EnsureCreated();
    }

    if (!context.Tickets.Any())
    {
        context.Tickets.AddRange(
            new Ticket
            {
                Title = "MRI Scanner Offline",
                Description = "Radiology reported that the MRI scanner workstation is offline.",
                Department = "Radiology",
                Category = "Medical Device",
                Priority = "Critical",
                Status = "Open",
                ReportedBy = "Emily Davis, RN",
                AssignedTo = "Emily Rodriguez",
                CreatedDate = DateTime.Now
            },
            new Ticket
            {
                Title = "EHR Login Failure",
                Description = "Provider cannot log into the electronic health record system.",
                Department = "Health Information Management",
                Category = "EHR System",
                Priority = "High",
                Status = "In Progress",
                ReportedBy = "Sarah Patel",
                AssignedTo = "David Chen",
                CreatedDate = DateTime.Now.AddDays(-1)
            },
            new Ticket
            {
                Title = "Password Reset Request",
                Description = "Nursing staff member needs password reset after failed login attempts.",
                Department = "Nursing",
                Category = "Password Reset",
                Priority = "Medium",
                Status = "Resolved",
                ReportedBy = "Emily Davis, RN",
                AssignedTo = "Michael Carter",
                CreatedDate = DateTime.Now.AddDays(-2)
            },
            new Ticket
            {
                Title = "Label Printer Offline",
                Description = "Laboratory label printer is not responding.",
                Department = "Laboratory",
                Category = "Printer",
                Priority = "Low",
                Status = "Closed",
                ReportedBy = "Mark Thompson",
                AssignedTo = "Sarah Johnson",
                CreatedDate = DateTime.Now.AddDays(-4)
            },
            new Ticket
            {
                Title = "Medication Order Delay",
                Description = "Pharmacy reported delay with medication order processing.",
                Department = "Pharmacy",
                Category = "EHR System",
                Priority = "High",
                Status = "Open",
                ReportedBy = "Olivia Brown",
                AssignedTo = "David Chen",
                CreatedDate = DateTime.Now.AddDays(-1)
            },
            new Ticket
            {
                Title = "Network Connectivity Issue",
                Description = "Emergency Department workstation has intermittent network connectivity.",
                Department = "Emergency Department",
                Category = "Network",
                Priority = "Critical",
                Status = "In Progress",
                ReportedBy = "Dr. Lisa Adams",
                AssignedTo = "Emily Rodriguez",
                CreatedDate = DateTime.Now.AddDays(-3)
            }
        );

        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();