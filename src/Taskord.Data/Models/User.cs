namespace Taskord.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public User()
        {
            this.UserTeams = new HashSet<UserTeam>();
            this.ChatUsers = new HashSet<ChatUser>();
            this.Messages = new HashSet<Message>();
            this.Relationships = new HashSet<Relationship>();
            this.Chats = new HashSet<Chat>();
            this.Posts = new HashSet<Post>();
        }

        public string ImagePath { get; set; }

        public ICollection<UserTeam> UserTeams { get; set; }

        public ICollection<Message> Messages { get; set; }

        public ICollection<ChatUser> ChatUsers { get; set; }

        public ICollection<Chat> Chats { get; set; }

        public ICollection<Relationship> Relationships { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
