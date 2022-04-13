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
            this.Cards = new HashSet<Card>();
            this.ChatUsers = new HashSet<ChatUser>();
            this.Messages = new HashSet<Message>();
            this.Friendships = new HashSet<Friendship>();
            this.Chats = new HashSet<Chat>();
        }

        public string ImagePath { get; set; }

        public ICollection<UserTeam> UserTeams { get; set; }

        public ICollection<Card> Cards { get; set; }

        public ICollection<Message> Messages { get; set; }

        public ICollection<ChatUser> ChatUsers { get; set; }

        public ICollection<Chat> Chats { get; set; }

        public ICollection<Friendship> Friendships { get; set; }
    }
}
