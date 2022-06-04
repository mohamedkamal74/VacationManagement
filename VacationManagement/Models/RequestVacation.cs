using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VacationManagement.Models
{
    public class RequestVacation : EntityBase
    {
        [Display(Name = "الموظف")]
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "تاريخ الاجازة")]
        public DateTime RequestDate { get; set; }
        [Display(Name = "نوع الاجازة")]
        public int VacationTypeId { get; set; }
        [ForeignKey("VacationTypeId")]
        public VacationType? VacationType { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "بداية الاجازة")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "نهاية الاجازة")]
        public DateTime EndtDate { get; set; }
        [Display(Name = "تعليق ")]

        public string? Comment { get; set; }
        [Display(Name = "موافقه")]

        public bool Approved { get; set; }
        [Display(Name = "تاريخ الطلب")]

        public DateTime? DateApproved { get; set; }
        public List<VacationPlan> VacationPlanList { get; set; } = new List<VacationPlan>();

    }
}
