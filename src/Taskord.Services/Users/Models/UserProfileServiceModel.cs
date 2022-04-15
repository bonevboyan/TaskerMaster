namespace Taskord.Services.Users.Models
{
    using Taskord.Services.Posts.Models;

    public class UserProfileServiceModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string ImagePath { get; set; }

        public int FriendCount { get; set; }

        public int TeamCount { get; set; }

        public int PostsCount { get; set; }
    }
}
