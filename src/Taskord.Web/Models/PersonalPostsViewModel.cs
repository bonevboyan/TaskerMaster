namespace Taskord.Web.Models
{
    using Taskord.Services.Posts.Models;
    using Taskord.Services.Users.Models;

    public class PersonalPostsViewModel
    {
        public bool IsOwn { get; set; }

        public IEnumerable<PostServiceModel> Posts { get; set; }

        public UserListServiceModel User { get; set; }
    }
}
