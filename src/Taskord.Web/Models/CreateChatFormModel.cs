namespace Taskord.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using Taskord.Services.Users.Models;
    using static Taskord.Common.DataConstants.Chat;

    public class CreateChatFormModel
    {
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        public IEnumerable<UserListServiceModel> UserIds { get; set; }
    }
}
