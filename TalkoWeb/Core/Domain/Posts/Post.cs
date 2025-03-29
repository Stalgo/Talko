using System.ComponentModel.DataAnnotations;
using TalkoWeb.SharedKernel;

namespace TalkoWeb.Core.Domain.Posts
{
    public class Post : BaseEntity
    {
        [Key]
        public Guid PostId { get; private set; }

        public Guid AuthorId { get; private set; }
        public string PostTitle { get; private set; } = string.Empty;

        public string PostContent { get; set; } = string.Empty;

        private List<Guid> _commentReferences = new();
        public IReadOnlyList<Guid> CommentsRefernces // to prevent exposing object reference, so that lists content cant be modified externaly
        {
            get { return _commentReferences; }
        }

        private Post() { } //Dotnet ef

        public Post(Guid id, Guid authorId, string title, string content)
        {
            PostId = id;
            AuthorId = authorId;
            PostTitle = title;
            PostContent = content;
        }

        public void AddCommentReference(Guid commentId)
        {
            if (!_commentReferences.Contains(commentId))
                _commentReferences.Add(commentId);
        }
    }
}
