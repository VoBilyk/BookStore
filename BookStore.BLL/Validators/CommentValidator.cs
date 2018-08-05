namespace BookStore.BLL.Validators
{
    using FluentValidation;

    using BookStore.DAL.Models;

    public class CommentValidator : AbstractValidator<Comment>
    {
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
