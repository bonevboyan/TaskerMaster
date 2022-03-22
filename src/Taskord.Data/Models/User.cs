namespace Taskord.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    using static Taskord.Common.DataConstants.User;

    public class User : IdentityUser
    {
        public User()
        {
            this.UserTeams = new HashSet<UserTeam>();
            this.Cards = new HashSet<Card>();
            this.Chats = new HashSet<Chat>();
            this.Messages = new HashSet<Message>();
            this.Friends = new HashSet<User>();
        }

        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; }

        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; }

        public int YearOfBirth { get; set; }

        public string ImagePath { get; set; }

        [MaxLength(StatusMaxLength)]
        public string StatusDescription { get; set; }

        public ICollection<UserTeam> UserTeams { get; set; }

        public ICollection<Card> Cards { get; set; }

        public ICollection<Message> Messages { get; set; }

        public ICollection<Chat> Chats { get; set; }

        public ICollection<User> Friends { get; set; }
    }
}
