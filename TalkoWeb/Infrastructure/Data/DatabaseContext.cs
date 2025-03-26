using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TalkoWeb.Core.Domain.Posts;
using TalkoWeb.Core.Domain.User.Aggregates;
using TalkoWeb.SharedKernel;

public class DatabaseContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
{
    public DbSet<Post> Posts { get; set; } = default!;

    private readonly IMediator? _mediator;

    public DatabaseContext(DbContextOptions<DatabaseContext> options, IMediator? mediator = null)
        : base(options)
    {
        _mediator = mediator ?? throw new NullReferenceException("mediator got null ref");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        if (_mediator is null)
            return result;
        var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>().Select(e => e.Entity).Where(e => e.Events.Any()).ToArray();
        foreach (var entity in entitiesWithEvents)
        {
            var events = entity.Events.ToArray();
            entity.Events.Clear();
            foreach (var domainEvent in events)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }
        }
        return result;
    }

    public override int SaveChanges() => SaveChangesAsync().GetAwaiter().GetResult();
}
