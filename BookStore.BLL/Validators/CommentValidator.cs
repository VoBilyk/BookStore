using FluentValidation;
using BookStore.DAL.Models;

namespace BookStore.BLL.Validators
{
    public class CommentValidator : AbstractValidator<Comment>
    {
        public CommentValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Book)
                .NotNull();

            RuleFor(x => x.Client)
                .NotNull();
        }
    }
}
