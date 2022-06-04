using System.ComponentModel.DataAnnotations;

namespace VacationManagement.Models
{
    public class VacationType:EntityBase
    {
        [Display(Name = "اسم الاجازة")]
        [StringLength(200)]
        public string VacationName { get; set; }=string.Empty;
        [Display(Name ="لون الاجازة")]
        [StringLength(7)]
        public string BackGroundColor { get; set; }=string.Empty ;
        [Display(Name ="عدد الايام")]
        public int NumberDays { get; set; }
    }
}
