namespace VacationManagement.Models
{
    public class SpGetReportVacationPlans
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int VacationBalance { get; set; }
        public int Totalvacations { get; set; }
        public int Total { get; set; }
    }
}
