namespace Taskord.Test.Services
{
    using Taskord.Data.Models;
    using Xunit;

    public class UserServiceTests : BaseServiceTests
    {
        private readonly string userId = "userId";
        private readonly string username = "username";
        private readonly string email = "email";
        private readonly string image = "ImagePath";

        public UserServiceTests()
            : base()
        {
            this.data.Users.Add(new User
            {
                Id = userId,
                UserName = username,
                Email = email,
                ImagePath = image
            });

            this.data.SaveChanges();
        }

        [Fact]
        public void GetUserProfileShouldReturnCorrectData()
        {
            var userProfile = this.userService.GetUserProfile(userId);

            Assert.Equal(userId, userProfile.Id);
            Assert.Equal(username, userProfile.Username);
            Assert.Equal(email, userProfile.Email);
            Assert.Equal(image, userProfile.ImagePath);
        }
    }
}
