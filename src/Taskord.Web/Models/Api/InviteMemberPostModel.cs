namespace Taskord.Web.Models.Api
{
    using System.ComponentModel.DataAnnotations;

    public class InviteMemberPostModel
    {

        [Required]
        public string UserId { get; set; }

        [Required]
        public string TeamId { get; set; }
    }
}
