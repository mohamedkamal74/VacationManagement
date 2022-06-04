using System.ComponentModel.DataAnnotations;

namespace VacationManagement.Models
{
    public class Department:EntityBase
    {
        [Display(Name ="اسم القسم")]
        [StringLength(150)]
        public string Name { get; set; }=string.Empty;
        [Display(Name = "وصف القسم")]

        public string Description { get; set; }=string.Empty ;
    }
}
