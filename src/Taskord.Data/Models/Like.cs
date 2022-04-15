namespace Taskord.Data.Models
{
    using Taskord.Data.Common;

    public class Like : BaseModel
    {
        public Like()
            :base()
        {

        }

        public User User { get; set; }

        public string UserId { get; set; }

        public Post Post { get; set; }

        public string PostId { get; set; }
    }
}
