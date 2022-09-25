using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VacationManagement.Data;
using VacationManagement.Models;

namespace VacationManagement.Controllers
{
    public class VacationPlansController : Controller
    {
        private readonly VacationDbContext _context;

        public VacationPlansController(VacationDbContext context)
        {
            _context = context;
        }
        public IActionResult VacationPlans()
        {
            return View(_context.RequestVacations.Include(e => e.Employee)
                .Include(v => v.VacationType).OrderByDescending(d => d.RequestDate).ToList());
        }

        public IActionResult GetVacationTypes()
        {
            return Json(_context.VacationTypes.OrderBy(v => v.Id).ToList());
        }

        public IActionResult GetCountVacationEmployee(int Id)
        {
            return Json(_context.VacationPlans.Where(x => x.ReaquestVacationId.Equals(Id)).Count());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VacationPlan model, int[] dayofweekcheckbox)
        {
            if (ModelState.IsValid)
            {
                var result = _context.VacationPlans.Where(x => x.RequestVacation.EmployeeId == model.RequestVacation.EmployeeId
                  && x.RequestVacation.StartDate >= model.RequestVacation.StartDate
                  && x.RequestVacation.EndtDate <= model.RequestVacation.EndtDate).FirstOrDefault();
                if (result != null)
                {
                    ViewBag.message = "هذه الاجازة موجودة مسبقا !";
                    return View(model);
                }
                for (DateTime date = model.RequestVacation.StartDate;
                    date <= model.RequestVacation.EndtDate; date = date.AddDays(1))
                {
                    if (Array.IndexOf(dayofweekcheckbox, (int)date.DayOfWeek) != -1)
                    {
                        model.Id = 0;
                        model.VacationDate = date;
                        model.RequestVacation.RequestDate = DateTime.Now;
                        _context.VacationPlans.Add(model);
                        _context.SaveChanges();
                    }
                }
                return RedirectToAction(nameof(VacationPlans));
            }
            return View(model);

        }

        public IActionResult Edit(int? Id)
        {
            ViewBag.Employees = _context.Employees.OrderBy(x => x.Name).ToList();
            ViewBag.VacationTypes = _context.VacationTypes.OrderBy(x => x.VacationName).ToList();
            return View(_context.RequestVacations.Include(x => x.Employee)
                .Include(x => x.VacationType).Include(x => x.VacationPlanList).FirstOrDefault(x => x.Id == Id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RequestVacation model)
        {
            if (ModelState.IsValid)
            {
                if (model.Approved)
                    model.DateApproved = DateTime.Now;
                _context.RequestVacations.Update(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(VacationPlans));
            }
            ViewBag.Employees = _context.Employees.OrderBy(x => x.Name).ToList();
            ViewBag.VacationTypes = _context.VacationTypes.OrderBy(x => x.VacationName).ToList();
            return View(model);
        }
        public IActionResult Delete(int? Id)
        {
            return View(_context.RequestVacations.Include(e => e.Employee)
               .Include(v => v.VacationType).Include(x => x.VacationPlanList).FirstOrDefault(x => x.Id == Id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(RequestVacation model)
        {
            if (model != null)
            {
                _context.RequestVacations.Remove(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(VacationPlans));
            }
            return View(model);
        }

        public IActionResult ViewReportVacationPlan()
        {
            ViewBag.Employees = _context.Employees.OrderBy(x => x.Name).ToList();
            return View();
        }

        public IActionResult ViewReportVacationPlan2()
        {
            ViewBag.Employees = _context.Employees.OrderBy(x => x.Name).ToList();
            return View();
        }

        public IActionResult GetReportVacationPlan(int EmployeeId, DateTime FromDate, DateTime ToDate)
        {
            string Id = "";
            if (EmployeeId != 0 && EmployeeId.ToString() != "")
                Id = $"and Employees.Id={EmployeeId}";

            #region
            //      var sqlQuery = _context.SqlDataTable($@"SELECT distinct dbo.Employees.Id,   
            //               dbo.Employees.Name, dbo.Employees.VacationBalance,
            //               SUM(dbo.VacationTypes.NumberDays) as Totalvacations,
            //dbo.Employees.VacationBalance - SUM(dbo.VacationTypes.NumberDays) as Total
            //               FROM  dbo.Employees INNER JOIN
            //          dbo.RequestVacations ON dbo.Employees.Id = dbo.RequestVacations.EmployeeId INNER JOIN
            //          dbo.VacationPlans ON dbo.RequestVacations.Id = dbo.VacationPlans.ReaquestVacationId INNER JOIN
            //          dbo.VacationTypes ON dbo.RequestVacations.VacationTypeId = dbo.VacationTypes.Id
            // where VacationPlans.VacationDate between
            //           '" +FromDate.ToString("yyyy-MM-dd")+"' and '"+ToDate.ToString("yyyy-MM-dd") + "' "+
            //         " and RequestVacations.Approved = 'True'"+
            //         $"{Id} Group By dbo.Employees.Id, dbo.Employees.Name, dbo.Employees.VacationBalance");
            #endregion

            string sqlQuery =$@"SELECT distinct dbo.Employees.Id,   
                     dbo.Employees.Name, dbo.Employees.VacationBalance,
                     SUM(dbo.VacationTypes.NumberDays) as Totalvacations,
					 dbo.Employees.VacationBalance - SUM(dbo.VacationTypes.NumberDays) as Total
                     FROM  dbo.Employees INNER JOIN
                dbo.RequestVacations ON dbo.Employees.Id = dbo.RequestVacations.EmployeeId INNER JOIN
                dbo.VacationPlans ON dbo.RequestVacations.Id = dbo.VacationPlans.ReaquestVacationId INNER JOIN
                dbo.VacationTypes ON dbo.RequestVacations.VacationTypeId = dbo.VacationTypes.Id
			    where VacationPlans.VacationDate between
                 '" + FromDate.ToString("yyyy-MM-dd") + "' and '" + ToDate.ToString("yyyy-MM-dd") + "' " +
              " and RequestVacations.Approved = 'True'" +
              $"{Id} Group By dbo.Employees.Id, dbo.Employees.Name, dbo.Employees.VacationBalance";

            var spGetData = _context.SpGetReportVacationPlans.FromSqlRaw(sqlQuery).ToList();

            ViewBag.Employees = _context.Employees.OrderBy(x => x.Name).ToList();
            return View("ViewReportVacationPlan2", spGetData);
        }
    }
}
