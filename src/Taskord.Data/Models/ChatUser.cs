namespace Taskord.Data.Models
{
    public class ChatUser
    {
        public string ChatId { get; set; }

        public Chat Chat { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        //public bool IsRead { get; set; }

        public string LastReadMessageId { get; set; }
    }
}