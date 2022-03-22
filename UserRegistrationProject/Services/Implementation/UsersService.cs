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
            if (password != confirmPassword)
            {
                return (0, "Both passwords must match");
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
