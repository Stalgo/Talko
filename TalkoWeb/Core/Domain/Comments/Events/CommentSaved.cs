using TalkoWeb.SharedKernel;

namespace TalkoWeb.Core.Domain.Comments.ValueObjects
{
    public record CommentSaved(Guid CommentId, Guid PostId) : BaseDomainEvent;
}
