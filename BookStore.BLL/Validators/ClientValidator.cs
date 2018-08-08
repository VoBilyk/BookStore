namespace BookStore.BLL.Validators
{
    using FluentValidation;

    using BookStore.DAL.Models;

    /// <summary>
    /// Validator for client model
    /// </summary>
    public class ClientValidator : AbstractValidator<Client>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientValidator"/> class.
        /// </summary>
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
