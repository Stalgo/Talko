using MediatR;
using Microsoft.EntityFrameworkCore;
using TalkoWeb.Application;

namespace TalkoWeb.Core.Domain.Comments.Handlers
{
    public class DeleteComment : IRequestHandler<DeleteCommentDTO, Result>
    {
        private readonly DatabaseContext _db;

        public DeleteComment(DatabaseContext database)
        {
            _db = database;
        }

        public async Task<Result> Handle(DeleteCommentDTO deleteComment, CancellationToken cancellationToken)
        {
            Comment? comment = await _db.Comments.SingleOrDefaultAsync(i => i.CommentId == deleteComment.commentId && i.AuthorId == deleteComment.authorId, cancellationToken);

            if (comment is null)
            {
                return Result.Fail("Could not delete comment");
            }
            return Result.Success();
        }
    }
}
