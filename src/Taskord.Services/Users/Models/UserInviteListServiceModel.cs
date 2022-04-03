namespace Taskord.Services.Users.Models
{
    using Taskord.Data.Models.Enums;

    public class UserInviteListServiceModel : UserListServiceModel
    {
        public RelationshipState State { get; set; }
    }
}
