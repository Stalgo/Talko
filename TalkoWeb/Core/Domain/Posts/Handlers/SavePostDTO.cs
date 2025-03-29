using MediatR;
using TalkoWeb.Application;

namespace TalkoWeb.Core.Domain.Posts.Handlers
{
    public record SavePostDTO(Guid AutorId, string Title, string Content) : IRequest<Result>;
}
