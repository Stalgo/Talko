using TalkoWeb.SharedKernel;

namespace TalkoWeb.Core.Domain.Posts.Aggregates
{
    public class Post : BaseEntity
    {
        public Guid PostID { get; set; }

        public Guid Owner { get; set; }
        public string PostTitle { get; set; } = string.Empty;

        public string PostContent { get; set; } = string.Empty;

        public int Votes { get; set; } = 0; // Updated from user voted function
    }
}
