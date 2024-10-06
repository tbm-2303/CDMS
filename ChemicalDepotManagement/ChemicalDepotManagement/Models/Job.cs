using ChemicalDepotManagement.Models.ChemicalDepotManagement.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ChemicalDepotManagement.Models
{
    public class Job
    {
        public int Id { get; set; } // Primary Key
        public string JobDescription { get; set; } // Description of the Job
        public ICollection<Chemical> Chemicals { get; set; } // Collection of Chemicals related to this Job

        public Job()
        {
            Chemicals = new List<Chemical>(); // Initialize the collection
        }
    }
}
