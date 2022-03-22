using System.Net.Mail;
using System.Text.RegularExpressions;
using UserRegistrationProject.DAL.Repositories.Abstractions;
using UserRegistrationProject.Helpers.Abstractions;
using UserRegistrationProject.Services.Abstraction;

namespace UserRegistrationProject.Services.Implementation
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UsersService(IUsersRepository usersRepository, IPasswordHasher passwordHasher)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<(int Id, string errorMessage)> CreateUser(string email, string password, string confirmPassword)
        {
            try
            {
                var emailAddress = new MailAddress(email).Address;
            }
            catch (FormatException)
            {
                return (0, "Invalid email address");
            }

            if (password != confirmPassword)
            {
                return (0, "Both passwords must match");
            }

            var isPasswordValid = Regex.IsMatch(password, "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$");
            if (!isPasswordValid)
            {
                return (0, "Your password must contain at least 1 uppercase letter, 1 lowercase letter and a special character");
            }

            var existingUser = await _usersRepository.GetUserByEmail(email);
            if (existingUser != null)
            {
                return (0, "User with the same email address already exists.");
            }

            var hashedPassword = _passwordHasher.Hash(password);

            var user = await _usersRepository.CreateUser(email, hashedPassword);
            if (user == null)
            {
                return (0, "Error creating user. Please try again later.");
            }
            return (user.Id, string.Empty);
        }
    }
}
