using MediatR;

namespace TalkoWeb.SharedKernel;

public abstract record BaseDomainEvent : INotification
{
    public Guid EventId { get; private set; } = Guid.NewGuid();
    public DateTimeOffset DateOccurred { get; protected set; } = DateTimeOffset.UtcNow;
}
