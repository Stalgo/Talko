using FluentValidation;
using TalkoWeb.Core.Domain.Comments.ValueObjects;

namespace TalkoWeb.Core.Domain.Comments.Validators
{
    public class AddCommentValidator : AbstractValidator<SaveCommentDTO>
    {
        public AddCommentValidator()
        {
            RuleFor(x => x.content).NotEmpty().WithMessage("Content is required.").Length(1, 500).WithMessage("Content must be between 1 and 500 characters.");

            RuleFor(x => x.authorId).NotEmpty().WithMessage("AuthorId is required.").Must(id => id != Guid.Empty).WithMessage("AuthorId must be a valid GUID.");
        }
    }
}
