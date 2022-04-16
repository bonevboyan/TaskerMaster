namespace Taskord.Services.Users.Models
{
    using System.Collections.Generic;

    public class UserQueryServiceModel
    {
        public int UsersPerPage { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalUsers { get; set; }

        public IEnumerable<UserListServiceModel> Users { get; set; }
    }
}
