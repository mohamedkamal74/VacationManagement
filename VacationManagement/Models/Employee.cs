using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VacationManagement.Models
{
    public class Employee:EntityBase
    {
        [Display(Name ="اسم الموظف")]
        [StringLength(150)]
        public string Name { get; set; }=string.Empty;
        [Display(Name = "الاجازات المستحقة")]
        [Range(1,31)]
        public int VacationBalance { get; set; }
        [Display(Name ="القسم")]
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }
    }
}
