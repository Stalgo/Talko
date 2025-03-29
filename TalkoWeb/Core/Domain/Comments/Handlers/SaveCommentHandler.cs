using MediatR;
using TalkoWeb.Application;

namespace TalkoWeb.Core.Domain.Comments.Handlers
{
    public record SaveCommentHandler(Guid AuthorId, Guid PostId, string Content) : IRequest<Result>;
}
