namespace Taskord.Data.Models
{
    using System.Collections.Generic;
    using Taskord.Data.Common;

    public class Post : BaseModel
    {
        public Post()
            :base()
        {
        }

        public string UserId { get; set; }

        public User User { get; set; }

        public string Content { get; set; }
    }
}
