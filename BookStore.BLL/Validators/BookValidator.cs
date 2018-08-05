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
                .NotEmpty();

            RuleFor(x => x.Author)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Genre)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Price)
                .NotEmpty();
        }
    }
}
