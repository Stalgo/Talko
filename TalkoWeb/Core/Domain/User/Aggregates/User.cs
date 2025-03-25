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

        // Add parameterless constructor for EF
        public User()
        {
            _identityUser = new IdentityUser<Guid>(); // Initialize the IdentityUser
        }

        // Constructor to initialize the wrapped IdentityUser
        public User(IdentityUser<Guid> identityUser)
        {
            _identityUser = identityUser;
        }

        public void UpdateProfile(string fullName, string email)
        {
            FullName = fullName;
            Email = email;
            _identityUser.Email = email; // Update IdentityUser email
        }
    }
}
