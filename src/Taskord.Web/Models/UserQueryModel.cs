namespace Taskord.Web.Models
{
    public class UserQueryModel
    {

        public const int UsersPerPage = 10;

        public string SearchTerm { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalCars { get; set; }

        public IEnumerable<CarServiceModel> Users { get; set; }
    }
}
