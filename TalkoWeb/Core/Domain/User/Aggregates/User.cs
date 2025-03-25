using Microsoft.AspNetCore.Identity;
using TalkoWeb.SharedKernel;

namespace TalkoWeb.Core.Domain.User.Aggregates
{
    public class User : BaseEntity
    {
        private readonly IdentityUser<Guid> _identityUser;
        public string FullName { get; set; }

        public string UserName
        {
            get { return _identityUser.UserName; }
            set { _identityUser.UserName = value; }
        }

        public string Email
        {
            get { return _identityUser.Email; }
            set { _identityUser.Email = value; }
        }

        public Guid Id
        {
            get { return _identityUser.Id; }
            set { _identityUser.Id = value; }
        }

        // Add custom properties or methods specific to your domain
        public string CustomProperty { get; set; }

        // Constructor to initialize the wrapped IdentityUser
        public User(IdentityUser<Guid> identityUser)
        {
            _identityUser = identityUser;
        }

        // You can also add methods to handle domain-specific logic if needed
        public void UpdateProfile(string fullName, string email)
        {
            FullName = fullName;
            Email = email;
            _identityUser.Email = email; // Update IdentityUser email
        }
    }
}
