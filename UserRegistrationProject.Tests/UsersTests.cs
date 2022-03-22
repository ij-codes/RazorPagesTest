using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using UserRegistrationProject.Config;
using UserRegistrationProject.DAL.Models;
using UserRegistrationProject.DAL.Repositories.Abstractions;
using UserRegistrationProject.Helpers.Implementation;
using UserRegistrationProject.Services.Implementation;

namespace UserRegistrationProject.Tests
{
    public class UsersTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task UserRegistration_WithAlreadyExistingEmail_ShouldFail()
        {
            //arrange
            var mock = new Mock<IUsersRepository>();
            mock.Setup(x => x.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(new User
            {
                Id = 1
            });
            var repository = mock.Object;
            var usersService = new UsersService(repository, new PasswordHasher(Options.Create(new HashingOptions())));
            //act
            var result = await usersService.CreateUser("test@test.test", "Password@123", "Password@123");

            //assert
            Assert.AreEqual(result.errorMessage, "User with the same email address already exists.");
            Assert.IsTrue(result.Id < 1, "User id should be 0 when there's an error");
        }

        [Test]
        public async Task UserRegistration_WithInvalidEmail_ShouldFail()
        {
            //arrange
            var mock = new Mock<IUsersRepository>();
            mock.Setup(x => x.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(new User
            {
                Id = 1
            });
            var repository = mock.Object;
            var usersService = new UsersService(repository, new PasswordHasher(Options.Create(new HashingOptions())));

            //act
            var result = await usersService.CreateUser("invalidEmail", "password", "password");

            //assert
            Assert.AreEqual(result.errorMessage, "Invalid email address");
            Assert.IsTrue(result.Id < 1, "User id should be 0 when there's an error");
        }

        [Test]
        public async Task UserRegistration_WithWeakPassword_ShouldFail()
        {
            //arrange
            var mock = new Mock<IUsersRepository>();
            mock.Setup(x => x.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(new User
            {
                Id = 1
            });
            var repository = mock.Object;
            var usersService = new UsersService(repository, new PasswordHasher(Options.Create(new HashingOptions())));

            //act
            var result = await usersService.CreateUser("test@mail.com", "test", "test");

            //assert
            Assert.AreEqual(result.errorMessage, "Your password must contain at least 1 uppercase letter, 1 lowercase letter and a special character");
            Assert.IsTrue(result.Id < 1, "User id should be 0 when there's an error");
        }

        [Test]
        public async Task UserRegistration_WithNonMatchingPasswords_ShouldFail()
        {
            //arrange
            var mock = new Mock<IUsersRepository>();
            mock.Setup(x => x.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(new User
            {
                Id = 1
            });
            var repository = mock.Object;
            var usersService = new UsersService(repository, new PasswordHasher(Options.Create(new HashingOptions())));

            //act
            var result = await usersService.CreateUser("test@mail.com", "Test123!", "Test123@");

            //assert
            Assert.AreEqual(result.errorMessage, "Both passwords must match");
            Assert.IsTrue(result.Id < 1, "User id should be 0 when there's an error");
        }

        [Test]
        public async Task UserRegistration_WithValidData_ShoulReturnId()
        {
            //arrange
            var mock = new Mock<IUsersRepository>();
            mock.Setup(x => x.GetUserByEmail(It.IsAny<string>())).ReturnsAsync((User)null) ;
            mock.Setup(x => x.CreateUser(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new User
            {
                Id = 2
            });
            var repository = mock.Object;
            var usersService = new UsersService(repository, new PasswordHasher(Options.Create(new HashingOptions())));

            //act
            var result = await usersService.CreateUser("test@mail.com", "Test123!", "Test123!");

            //assert
            Assert.AreEqual(result.errorMessage, string.Empty);
            Assert.IsTrue(result.Id > 0, "User id should be bigger than 0 when successfully creating user.");
        }
    }
}