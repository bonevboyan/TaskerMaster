namespace Taskord.Web.Models.Api
{
    using System.ComponentModel.DataAnnotations;
    using Taskord.Data.Models.Enums;

    public class ManageMemberRolesPostModel
    {
        [Required]
        public string TeamId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public TeamRole Role { get; set; }
    }
}
