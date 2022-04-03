namespace Taskord.Web.Models.Api
{
    using Taskord.Data.Models.Enums;

    public class ManageMemberRolesPostModel
    {
        public string TeamId { get; set; }

        public string UserId { get; set; }

        public TeamRole Role { get; set; }
    }
}
