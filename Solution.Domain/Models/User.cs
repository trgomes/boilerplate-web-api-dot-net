using Dapper.Contrib.Extensions;
using Solution.Domain.Enums;
using Solution.Infra.CrossCuting.Helpers;

namespace Solution.Domain.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public EUserProfile Profile { get; private set; }
        public bool Active { get; private set; }

        protected User() { }

        public User(string name, string email, string password, EUserProfile profile = EUserProfile.User, bool active = true)
        {
            Name = name;
            Email = email;
            Password = !SecurePasswordHasher.IsHashSupported(password) ? SecurePasswordHasher.Hash(password) : password;
            Profile = profile;
            Active = active;
        }

        public User(int id, string name, string email, string password, EUserProfile profile = EUserProfile.User, bool active = true)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = !SecurePasswordHasher.IsHashSupported(password) ? SecurePasswordHasher.Hash(password) : password;
            Profile = profile;
            Active = active;
        }

        public bool Authenticate(string email, string password)
        {
            if (Email == email && SecurePasswordHasher.Verify(password, Password))
                return true;

            return false;
        }

        public void Activate() => Active = true;
        public void Deactivate() => Active = false;

    }
}
