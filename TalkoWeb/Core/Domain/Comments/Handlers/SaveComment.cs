using FluentValidation;
using FluentValidation.Results;
using MediatR;
using TalkoWeb.Application;
using TalkoWeb.Core.Domain.Comments.ValueObjects;

namespace TalkoWeb.Core.Domain.Comments.Handlers
{
    public class SaveComment : IRequestHandler<SaveCommentHandler, Result>
    {
        private readonly IValidator<SaveCommentHandler> _validator;
        private readonly DatabaseContext _db;

        public SaveComment(IValidator<SaveCommentHandler> valitdator, DatabaseContext database)
        {
            _validator = valitdator;
            _db = database;
        }

        public async Task<Result> Handle(SaveCommentHandler addComment, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(addComment, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Result.Failure(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            Comment comment = new(addComment.AuthorId, addComment.PostId, addComment.Content);
            comment.Status = CommentStatus.visible;

            await _db.Comments.AddAsync(comment, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
