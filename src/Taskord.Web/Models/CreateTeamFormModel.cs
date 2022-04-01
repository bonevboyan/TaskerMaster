namespace Taskord.Web.Models
{
    using System.ComponentModel.DataAnnotations;
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
    }
}
