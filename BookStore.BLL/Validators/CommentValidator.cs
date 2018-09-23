namespace BookStore.BLL.Validators
{
    using FluentValidation;

    using BookStore.DAL.Models;

    /// <summary>
    /// Validator for comment model
    /// </summary>
    public class CommentValidator : AbstractValidator<Comment>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentValidator"/> class.
        /// </summary>
        public CommentValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Text)
                .NotNull()
                .MinimumLength(5);

            RuleFor(x => x.Book)
                .NotNull();

            RuleFor(x => x.Client)
                .NotNull();
        }
    }
}
