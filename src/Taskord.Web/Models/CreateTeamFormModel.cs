namespace Taskord.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using Taskord.Services.Users.Models;
    using static Taskord.Common.DataConstants.Team;

    public class CreateTeamFormModel
    {
        [Required]
        [Display(Name = "Image URL")]
        public string ImagePath { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [StringLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public IEnumerable<UserListServiceModel> UserIds { get; set; }

        public IEnumerable<bool> SelectedUserIds { get; set; }
    }
}
