namespace ChemicalDepotManagement.Models
{
    public class Chemical
    {
        public int Id { get; set; }
        public string Name { get; set; } // Name of the chemical
        public string Class { get; set; } // Chemical type A, B or C
        public int Quantity { get; set; } // Quantity of the chemical

        // Foreign key for Job
        public int JobId { get; set; }

        // Navigation property
        public Job Job { get; set; }


        // Foreign key for Warehouse
        public int WarehouseId { get; set; }

        // Navigation property
        public Warehouse Warehouse { get; set; } // The warehouse where the chemical is stored
    }

}
