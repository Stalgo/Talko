namespace TalkoWeb.SharedKernel;

public abstract class BaseEntity
{
    public List<BaseDomainEvent> events = new();
}