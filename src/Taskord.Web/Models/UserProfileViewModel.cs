namespace Taskord.Web.Models
{
    using Taskord.Data.Models.Enums;
    using Taskord.Services.Posts.Models;
    using Taskord.Services.Relationships.Models;
    using Taskord.Services.Users.Models;

    public class UserProfileViewModel
    {
        public UserProfileServiceModel User { get; set; }

        public PostServiceModel Post { get; set; }

        public RelationshipServiceModel Relationship { get; set; }

        public bool IsOwn { get; set; }
    }
}
