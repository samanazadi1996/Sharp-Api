using System.ComponentModel.DataAnnotations;

namespace Common
{
    public enum GenderType
    {
        [Display(Name = "نامشخص")]
        Unknow,
        [Display(Name = "مرد")]
        Mele,
        [Display(Name = "زن")]
        Femele
    }
}
