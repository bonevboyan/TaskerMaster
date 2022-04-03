namespace Taskord.Services.Users.Models
{
    using Taskord.Data.Models.Enums;

    public class UserManageRolesServiceModel : UserListServiceModel
    {
        public TeamRole Role { get; set; }
    }
}
