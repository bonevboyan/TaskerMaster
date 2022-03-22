namespace Taskord.Web.Models
{
    using Taskord.Services.Users.Models;

    public class UserQueryModel
    {

        public const int UsersPerPage = 10;

        public string SearchTerm { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalUsers { get; set; }

        public IEnumerable<UserListServiceModel> Users { get; set; }
    }
}
