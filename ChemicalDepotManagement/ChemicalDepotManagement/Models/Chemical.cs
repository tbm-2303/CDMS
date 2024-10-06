using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChemicalDepotManagement.Models
{
    public class Chemical
    {
        public int Id { get; set; } // Primary Key
        public string ChemicalType { get; set; } // Type of Chemical
        public int Quantity { get; set; } // Quantity of Chemical
        public int JobId { get; set; } // Foreign Key to Job
        public Job Job { get; set; } // Navigation property
    }
}
