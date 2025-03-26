using Microsoft.AspNetCore.Identity;

namespace TalkoWeb.Core.Domain.User.Aggregates
{
    public class User : IdentityUser<Guid> { }
}
