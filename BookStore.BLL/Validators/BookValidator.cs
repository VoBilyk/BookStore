namespace BookStore.BLL.Validators
{
    using FluentValidation;

    using BookStore.DAL.Models;

    /// <summary>
    /// Validator for book model
    /// </summary>
    public class BookValidator : AbstractValidator<Book>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookValidator"/> class.
        /// </summary>
        public BookValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotNull()
                .MinimumLength(2);

            RuleFor(x => x.Author)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(x => x.Genre)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(x => x.Price)
                .NotEmpty();
        }
    }
}
