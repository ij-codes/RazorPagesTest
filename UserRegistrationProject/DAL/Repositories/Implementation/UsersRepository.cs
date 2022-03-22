using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using UserRegistrationProject.Config;
using UserRegistrationProject.DAL.Models;
using UserRegistrationProject.DAL.Repositories.Abstractions;

namespace UserRegistrationProject.DAL.Repositories.Implementation
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ILogger<UsersRepository> _logger;
        private readonly SqlServerConfig _sqlServerConfig;

        public UsersRepository(ILogger<UsersRepository> logger, IOptions<SqlServerConfig> sqlServerConfig)
        {
            _logger = logger;
            _sqlServerConfig = sqlServerConfig.Value;
        }

        public async Task<User> CreateUser(string email, string password)
        {
            string queryString =
            $@"INSERT INTO Users (Email, Password) 
               VALUES('{email.Trim().ToLowerInvariant()}', '{password}');
               SELECT SCOPE_IDENTITY()
               ";

            var id = 0;
            using SqlConnection connection = new(_sqlServerConfig.ConnectionString);
            SqlCommand command = new(queryString, connection);
            try
            {
                connection.Open();
                var result = await command.ExecuteScalarAsync();
                id = result == null ? 0 : Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId(2), ex, "Error creating user");
                return null;
            }
            finally
            {
                connection.Close();
            }

            return new User()
            {
                Email = email,
                Id = id,
                Password = password
            };
        }

        public async Task<User> GetUserByEmail(string email)
        {
            User existingUser = null;

            string queryString =
            $@"SELECT Id, Email
              FROM Users
              WHERE email = '{email.Trim().ToLowerInvariant()}'";

            using SqlConnection connection = new(_sqlServerConfig.ConnectionString);
            SqlCommand command = new(queryString, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = await command.ExecuteReaderAsync();

                await reader.ReadAsync();
                var id = reader.GetInt32(0);
                var emailAddress = reader.GetString(1);
                reader.Close();
                if (id > 0)
                {
                    existingUser = new User
                    {
                        Id = id,
                        Email = emailAddress
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId(1), ex, "Error occured while querying the database");
            }
            finally
            {
                connection.Close(); //even though the using should dispose of the connection, I prefer to close it before that happens
            }

            return existingUser;
        }
    }
}
