using FluentValidation.Results;
using Solution.Domain.Enums;
using Solution.Domain.Validations;
using Solution.Infra.CrossCuting.Helpers;
using System;

namespace Solution.Domain.Models
{
    public class User
    {
        public Guid Id { get; private set; }
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

        public User(Guid id, string name, string email, string password, EUserProfile profile = EUserProfile.User, bool active = true)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = !SecurePasswordHasher.IsHashSupported(password) ? SecurePasswordHasher.Hash(password) : password;
            Profile = profile;
            Active = active;
        }

        public ValidationResult Validate()
        {
            var validationResult = new UserValidation().Validate(this);
            return validationResult;
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
