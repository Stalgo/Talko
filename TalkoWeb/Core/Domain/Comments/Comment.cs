using System.ComponentModel.DataAnnotations;
using TalkoWeb.Core.Domain.Comments.ValueObjects;
using TalkoWeb.SharedKernel;

namespace TalkoWeb.Core.Domain.Comments
{
    public class Comment : BaseEntity
    {
        [Key]
        public Guid CommentId { get; private set; }
        public Guid AuthorId { get; private set; }
        public Guid PostId { get; private set; }
        public string CommentContent { get; set; } = string.Empty;
        private CommentStatus _status { get; set; }
        public CommentStatus Status
        {
            get => _status;
            set
            {
                if (value is CommentStatus.visible)
                {
                    Events.Add(new CommentSaved(CommentId, PostId));
                }
            }
        }

        private Comment() { } // dotnet ef

        public Comment(Guid authorId, Guid postId, string commentContent)
        {
            AuthorId = authorId;
            CommentContent = commentContent;
            PostId = postId;
        }
    }
}
