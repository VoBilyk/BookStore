namespace BookStore.BLL.Validators
{
    using FluentValidation;

    using BookStore.DAL.Models;

    public class BookValidator : AbstractValidator<Book>
    {
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
