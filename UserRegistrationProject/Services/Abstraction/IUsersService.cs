namespace UserRegistrationProject.Services.Abstraction
{
    public interface IUsersService
    {
        Task<(int Id, string errorMessage)> CreateUser(string email, string password, string confirmPassword);
    }
}
