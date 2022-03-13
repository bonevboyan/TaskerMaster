namespace Taskord.Data.Models
{
    public class AdminTeam
    {
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string TeamId { get; set; }

        public Team Team { get; set; }
    }
}
