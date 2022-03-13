namespace Taskord.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    using static Taskord.Common.DataConstants.User;

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Teams = new HashSet<Team>();
            this.Cards = new HashSet<Card>();
            this.Chats = new HashSet<Chat>();
            this.Messages = new HashSet<Message>();
        }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; }

        [Required]
        public int YearOfBirth { get; set; }

        [Required]
        public string ImagePath { get; set; }

        [MaxLength(StatusMaxLength)]
        public string StatusDescription { get; set; }

        public ICollection<Team> Teams { get; set; }

        public ICollection<AdminTeam> AdminTeams { get; set; }

        public ICollection<Card> Cards { get; set; }

        public ICollection<Message> Messages { get; set; }

        public ICollection<Chat> Chats { get; set; }

        public ICollection<ApplicationUser> Connections { get; set; }
    }
}
