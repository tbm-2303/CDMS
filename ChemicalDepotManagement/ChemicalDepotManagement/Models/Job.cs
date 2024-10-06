namespace ChemicalDepotManagement.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Description { get; set; } // Job details
        public string Status { get; set; } // e.g., "Pending", "Confirmed"

        // Navigation property
        public ICollection<Chemical> Chemicals { get; set; } = new List<Chemical>();
    }

}
