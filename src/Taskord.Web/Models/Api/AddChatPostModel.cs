namespace Taskord.Web.Models.Api
{
    using System.ComponentModel.DataAnnotations;
    using static Taskord.Common.DataConstants.Chat;

    public class AddChatPostModel
    {
        [Required]
        public string TeamId { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }
    }
}
