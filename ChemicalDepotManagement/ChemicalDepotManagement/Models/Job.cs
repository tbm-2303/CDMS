using System;
using System.ComponentModel.DataAnnotations;

namespace ChemicalDepotManagement.Models
{
    public class Job
    {
        [Key]
        public int JobId { get; set; }

        [Required]
        public string JobType { get; set; }  // Delivery or Dispatch

        [Required]
        public string Status { get; set; }  // Pending or Confirmed

        [Required]
        public DateTime JobDate { get; set; }

        [Required]
        public int ChemicalId { get; set; }  // Foreign key
    }
}
