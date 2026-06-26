using System;
using System.ComponentModel.DataAnnotations;

namespace HealthcareTicketingSystem.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }

        public string Title { get; set; } = "";

        public string Description { get; set; } = "";


        public string Department { get; set; } = "";
        public string Category { get; set; } = "";

        public string Priority { get; set; } = "";

        public string Status { get; set; } = "Open";

        public string ReportedBy { get; set; } = "";

        [Display(Name = "Assigned To")]
        public string AssignedTo { get; set; } = "";

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}