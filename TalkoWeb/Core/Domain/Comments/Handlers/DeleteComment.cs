using System.Reflection.Metadata.Ecma335;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TalkoWeb.Application;
using TalkoWeb.Core.Domain.Comments.ValueObjects;

namespace TalkoWeb.Core.Domain.Comments.Handlers
{
    public class DeleteComment : IRequestHandler<DeleteCommentDTO, Result>
    {
        private readonly DatabaseContext _db;
        private List<string> _errors;

        public DeleteComment(DatabaseContext database)
        {
            _db = database;
            _errors = new();
        }

        public async Task<Result> Handle(DeleteCommentDTO deleteComment, CancellationToken cancellationToken)
        {
            Comment? comment = await _db.Comments.SingleOrDefaultAsync(i => i.CommentId == deleteComment.commentId && i.AuthorId == deleteComment.authorId, cancellationToken);

            if (comment is null)
            {
                return Result.Failure(_errors.Append("Could not delete comment"));
            }
            return Result.Success();
        }
    }
}
