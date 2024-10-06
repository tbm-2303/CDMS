namespace ChemicalDepotManagement.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        public int Capacity { get; set; } // Kilo-Units
        public List<Chemical> Chemicals { get; set; }
    }

}
