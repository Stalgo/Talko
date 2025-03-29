using MediatR;
using TalkoWeb.Application;

namespace TalkoWeb.Core.Domain.Comments.Handlers
{
    public record DeleteCommentDTO(Guid authorId, Guid commentId) : IRequest<Result>;
}
