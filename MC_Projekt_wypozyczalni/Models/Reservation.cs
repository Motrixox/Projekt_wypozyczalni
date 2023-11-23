using FluentValidation;

namespace MC_Projekt_wypozyczalni.Models
{
    public class Reservation : IEntity
    {
        public int Id { get; set; }
        public DateTime reservationStart { get; set; }
        public DateTime reservationEnd { get; set; }
        public int vehicleID { get; set; }
        public string userName { get; set; }
        public string status { get; set; } = "Reserved";
    }

    public class ReservationValidator : AbstractValidator<Reservation>
    {
        public ReservationValidator()
        {
            RuleFor(x => x.reservationStart)
                .LessThan(x => x.reservationEnd)
                .WithMessage("Data początkowa rezerwacji nie może być późniejsza niż data końcowa.");
        }
    }
}
