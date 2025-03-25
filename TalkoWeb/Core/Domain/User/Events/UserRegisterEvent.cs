using TalkoWeb.SharedKernel;

namespace TalkoWeb.Core.Domain.User.Events;

public record UserRegisterEvent(Guid UserId, string UserName, string Email, DateTime CreatedAt) : BaseDomainEvent;
