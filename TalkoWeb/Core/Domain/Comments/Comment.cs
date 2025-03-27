using System.ComponentModel.DataAnnotations;
using TalkoWeb.SharedKernel;

namespace TalkoWeb.Core.Domain.Comments
{
    public class Comment : BaseEntity
    {
        [Key]
        public Guid CommentId { get; private set; }
        public Guid AuthorId { get; private set; }
        public string CommentContent { get; set; } = string.Empty;

        private Comment() { } // dotnet ef

        public Comment(Guid authorId, string commentContent)
        {
            AuthorId = authorId;
            CommentContent = commentContent;
        }
    }
}
