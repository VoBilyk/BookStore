namespace BookStore.BLL.Validators
{
    using FluentValidation;

    using BookStore.DAL.Models;

    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(x => x.SecondName)
                .NotEmpty()
                .MinimumLength(3);
        }
    }
}
