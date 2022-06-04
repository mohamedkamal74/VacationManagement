using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VacationManagement.Models
{
    public class VacationPlan:EntityBase
    {
        [DataType(DataType.Date)]
        [Display(Name = "تاريخ الاجازة")]
        [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}")]
        public DateTime? VacationDate { get; set; }
        [Display(Name = "طلب الاجازة")]
        public int ReaquestVacationId { get; set; }
        [ForeignKey("ReaquestVacationId")]
        public RequestVacation? RequestVacation { get; set; }
    }
}
