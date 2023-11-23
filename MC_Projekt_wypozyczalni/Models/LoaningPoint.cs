using FluentValidation;

namespace MC_Projekt_wypozyczalni.Models
{
    public class LoaningPoint : IEntity
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
    }

    public class LoaningPointValidator : AbstractValidator<LoaningPoint>
    {
        public LoaningPointValidator()
        {
            RuleFor(x => x.name).Length(4, 50).WithMessage("Name has to be between 4 and 50 characters long.");
        }
    }
}
