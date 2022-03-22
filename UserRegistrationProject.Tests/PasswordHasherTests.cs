using Microsoft.Extensions.Options;
using NUnit.Framework;
using System.Threading.Tasks;
using UserRegistrationProject.Config;
using UserRegistrationProject.Helpers.Implementation;

namespace UserRegistrationProject.Tests
{
    public class PasswordHasherTests
    {
        [Test]
        public async Task HashingPassword_MultipleTimes_ShouldReturnSameValue()
        {
            //arrange
            var password = "test";
            var hasher = new PasswordHasher(Options.Create(new HashingOptions()));

            //act
            var hashedPassword1 = hasher.Hash(password);
            var hashedPassword2 = hasher.Hash(password);

            var check1 = hasher.Check(hashedPassword1, password);
            var check2 = hasher.Check(hashedPassword2, password);

            //assert
            Assert.AreNotEqual(hashedPassword1, hashedPassword2);
            Assert.IsTrue(check1);
            Assert.IsTrue(check2);
        }
    }
}