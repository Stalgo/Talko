using TalkoWeb.SharedKernel;

namespace TalkoWeb.Core.Domain.Posts
{
    public class Post : BaseEntity
    {
        public Guid PostId { get; private set; }

        public Guid AuthorId { get; private set; }
        public string PostTitle { get; private set; } = string.Empty;

        public string PostContent { get; set; } = string.Empty;

        public List<Guid> CommentRefereces { get; private set; } = new();

        public Post(Guid id, Guid authorId, string title, string content)
        {
            PostId = id;
            AuthorId = authorId;
            PostTitle = title;
            PostContent = content;
        }

        public void AddCommentReference(Guid commentId)
        {
            CommentRefereces.Add(commentId);
        }
    }
}
