namespace ChemicalDepotManagement.Models
{
    namespace ChemicalDepotManagement.Models
    {
        public class Ticket
        {
            public int Id { get; set; } // Primary Key
            public string TicketNumber { get; set; } // Ticket Number
            public int JobId { get; set; } // Foreign Key to Job
            public Job Job { get; set; } // Navigation property
        }


    }
}
