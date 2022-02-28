namespace TaskerMaster.Web.ViewModels.Companies
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using TaskerMaster.Common;
    using TaskerMaster.Data.Models.Enums;

    public class WorkspaceFormModel
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string CompanySize { get; set; }

        public string CompanyType { get; set; }

        public List<string> CompanySizes { get; } = Enum.GetValues(typeof(CompanySize)).Cast<CompanySize>().Select(x => x.GetAttribute<DisplayAttribute>().Name).ToList();

        public List<string> CompanyTypes { get; } = Enum.GetValues(typeof(CompanyType)).Cast<CompanyType>().Select(x => x.GetAttribute<DisplayAttribute>().Name).ToList();
    }
}
