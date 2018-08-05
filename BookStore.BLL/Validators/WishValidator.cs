namespace BookStore.BLL.Validators
{
    using FluentValidation;

    using BookStore.DAL.Models;

    public class WishValidator : AbstractValidator<Wish>
    {
        public WishValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.BookId)
                .NotEmpty();

            RuleFor(x => x.ClientId)
                .NotEmpty();
        }
    }
}
