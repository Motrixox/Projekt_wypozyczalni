using System.ComponentModel.DataAnnotations;

namespace MC_Projekt_wypozyczalni.Models
{
    public class Vehicle : IEntity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string name { get; set; }
        [Required]
        [StringLength(100)]
        public string color { get; set; }
        public VehicleType type { get; set; }
        [Required]
        [Range(0, 1000000)]
        public double range { get; set; }
        public LoaningPoint loaningPoint { get; set; }
    }
}
