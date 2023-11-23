namespace MC_Projekt_wypozyczalni.Models
{
    public class VehicleViewModel
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public VehicleType type { get; set; }
        public double range { get; set; }
        public LoaningPoint loaningPoint { get; set; }
    }
}
