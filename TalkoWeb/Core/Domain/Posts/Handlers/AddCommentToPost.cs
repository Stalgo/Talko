using MediatR;
using TalkoWeb.Core.Domain.Comments.ValueObjects;

namespace TalkoWeb.Core.Domain.Posts.Handlers
{
    public class AddCommentToPost : INotificationHandler<CommentSaved>
    {
        private readonly DatabaseContext _db;
        private readonly ILogger<AddCommentToPost> _logger;
        public AddCommentToPost(DatabaseContext db, ILogger<AddCommentToPost> logger)
        {
            _logger = logger;
            _db = db;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1725")]
        // commentdto rather than  notfication 
        public async Task Handle(CommentSaved commentDTO, CancellationToken cancellationToken)
        {
            Post? post = await _db.Posts.FindAsync(new object?[] { commentDTO.PostId, cancellationToken }, cancellationToken: cancellationToken);
            if (post is not null)
            {
                post.AddCommentReference(commentDTO.CommentId);
                int affectedRows = await _db.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("{affectedRows} records updated", affectedRows);

            }
        }
    }
}
