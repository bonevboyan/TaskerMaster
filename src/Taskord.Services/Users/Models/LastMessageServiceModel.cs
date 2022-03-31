namespace Taskord.Services.Users.Models
{
    using Taskord.Data.Models;

    internal class LastMessageServiceModel
    {
        public User User { get; set; }

        public string ChatId { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
}
