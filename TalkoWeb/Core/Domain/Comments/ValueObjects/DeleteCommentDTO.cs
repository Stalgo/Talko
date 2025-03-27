using MediatR;
using TalkoWeb.Application;

namespace TalkoWeb.Core.Domain.Comments.ValueObjects
{
    public record DeleteCommentDTO(Guid authorId, Guid commentId) : IRequest<Result>;
}
