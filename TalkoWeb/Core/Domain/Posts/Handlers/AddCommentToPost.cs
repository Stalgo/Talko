using MediatR;
using TalkoWeb.Core.Domain.Comments.ValueObjects;

namespace TalkoWeb.Core.Domain.Posts.Handlers
{
    public class AddCommentToPost : INotificationHandler<CommentSaved>
    {
        private readonly DatabaseContext _db;

        public AddCommentToPost(DatabaseContext db)
        {
            _db = db;
        }

        public async Task Handle(CommentSaved commentDTO, CancellationToken cancellationToken)
        {
            Post? post = await _db.Posts.FindAsync(commentDTO.PostId);
            if (post is null)
                return;

            post.AddCommentReference(commentDTO.CommentId);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
