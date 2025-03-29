using FluentValidation;
using TalkoWeb.Core.Domain.Comments.Handlers;

namespace TalkoWeb.Core.Domain.Comments.Validators
{
    public class AddCommentValidator : AbstractValidator<SaveCommentHandler>
    {
        public AddCommentValidator()
        {
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required.").Length(1, 500).WithMessage("Content must be between 1 and 500 characters.");

            RuleFor(x => x.AuthorId).NotEmpty().WithMessage("AuthorId is required.").Must(id => id != Guid.Empty).WithMessage("AuthorId must be a valid GUID.");
        }
    }
}
