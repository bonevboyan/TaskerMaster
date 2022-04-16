namespace Taskord.Data.Models
{
    using System.Collections.Generic;
    using Taskord.Data.Common;

    public class Post : BaseModel
    {
        public Post()
            :base()
        {
            this.Likes = new HashSet<Like>();
        }

        public string UserId { get; set; }

        public User User { get; set; }

        public string Content { get; set; }

        public ICollection<Like> Likes { get; set; }
    }
}
