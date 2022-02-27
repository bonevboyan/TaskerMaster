namespace TaskerMaster.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using TaskerMaster.Data.Common.Models;
    using TaskerMaster.Data.Models.Enums;

    using static TaskerMaster.Common.DataConstants.Company;

    public class Company : BaseDeletableModel<string>
    {
        public Company()
            : base()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Teams = new HashSet<Team>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public CompanySize CompanySize { get; set; }

        public CompanyType CompanyType { get; set; }

        public ICollection<Team> Teams { get; set; }
    }
}
