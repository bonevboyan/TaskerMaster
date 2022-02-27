namespace TaskerMaster.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum CompanyType
    {
        [Display(Name = "Corporation")]
        Corporation = 0,
        [Display(Name = "Limited Liability Company")]
        LimitedLiabilityCompany = 1,
        [Display(Name = "Partnerships")]
        Partnerships = 2,
        [Display(Name = "Joint Venture")]
        JointVenture = 3,
        [Display(Name = "Nonprofit")]
        Nonprofit = 4,
    }
}
