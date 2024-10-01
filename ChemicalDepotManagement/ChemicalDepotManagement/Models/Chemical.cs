using System.ComponentModel.DataAnnotations;

namespace ChemicalDepotManagement.Models
{
    public class Chemical
    {
        [Key]
        public int ChemicalId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Class { get; set; }  // A, B, or C

        [Required]
        public double Quantity { get; set; }  // In kilo-units
    }
}
