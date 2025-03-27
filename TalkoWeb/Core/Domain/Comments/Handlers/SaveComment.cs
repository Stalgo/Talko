using FluentValidation;
using FluentValidation.Results;
using MediatR;
using TalkoWeb.Application;
using TalkoWeb.Core.Domain.Comments.ValueObjects;

namespace TalkoWeb.Core.Domain.Comments.Handlers
{
    public class SaveComment : IRequestHandler<SaveCommentDTO, Result>
    {
        private readonly IValidator<SaveCommentDTO> _validator;
        private readonly DatabaseContext _db;

        public SaveComment(IValidator<SaveCommentDTO> valitdator, DatabaseContext database)
        {
            _validator = valitdator;
            _db = database;
        }

        public async Task<Result> Handle(SaveCommentDTO addComment, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(addComment, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Result.Failure(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            Comment comment = new(addComment.authorId, addComment.content);

            await _db.Comments.AddAsync(comment, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
