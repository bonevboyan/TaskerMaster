namespace TaskerMaster.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum CompanySize
    {
        [Display(Name = "Small Business")]
        SmallBusiness = 0,
        [Display(Name = "Medium Business")]
        MediumBusiness = 1,
        [Display(Name = "Large Enterprise")]
        LargeEnterprise = 2,
    }
}
