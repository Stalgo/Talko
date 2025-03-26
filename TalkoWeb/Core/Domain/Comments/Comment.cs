using TalkoWeb.SharedKernel;

namespace TalkoWeb.Core.Domain.Comments
{
    public class Comment : BaseEntity
    {
        private Guid CommentId { get; set; }
        private string CommentContent { get; set; } = string.Empty;

        public Comment() { }
    }
}
