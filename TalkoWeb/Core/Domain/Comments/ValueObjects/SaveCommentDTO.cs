using MediatR;
using TalkoWeb.Application;

namespace TalkoWeb.Core.Domain.Comments.ValueObjects
{
    public record SaveCommentDTO(Guid authorId, string content) : IRequest<Result>;
}
