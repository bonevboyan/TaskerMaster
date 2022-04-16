namespace Taskord.Services.Posts.Models
{
    using Taskord.Services.Users.Models;

    public class PostServiceModel
    {
        public UserListServiceModel User { get; set; }

        public string Id { get; set; }

        public string Content { get; set; }

        public string DateTime { get; set; }
    }
}
