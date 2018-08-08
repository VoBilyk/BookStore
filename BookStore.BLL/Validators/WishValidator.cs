namespace BookStore.BLL.Validators
{
    using FluentValidation;

    using BookStore.DAL.Models;

    /// <summary>
    /// Validator for wish model
    /// </summary>
    public class WishValidator : AbstractValidator<Wish>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WishValidator"/> class.
        /// </summary>
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
