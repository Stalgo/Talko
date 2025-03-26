using MediatR;
using TalkoWeb.Application;

namespace TalkoWeb.Core.Domain.Comments.ValueObjects
{
    public record AddComment(Guid authorId, string content) : IRequest<Result>;
}
