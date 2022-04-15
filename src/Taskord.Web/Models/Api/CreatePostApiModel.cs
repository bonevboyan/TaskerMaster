namespace Taskord.Web.Models.Api
{
    using System.ComponentModel.DataAnnotations;

    using static Taskord.Common.DataConstants.Post;

    public class CreatePostApiModel
    {
        [Required]
        [StringLength(ContentMaxLength, MinimumLength = ContentMinLength)]
        public string Content { get; set; }
    }
}
