using UserRegistrationProject.DAL.Models;

namespace UserRegistrationProject.DAL.Repositories.Abstractions
{
    public interface IUsersRepository
    {
        Task<User> GetUserByEmail(string email);
        Task<User> CreateUser(string email, string password);
    }
}
