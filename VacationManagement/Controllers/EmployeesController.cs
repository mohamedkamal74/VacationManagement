using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VacationManagement.Data;
using VacationManagement.Models;

namespace VacationManagement.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly VacationDbContext _context;

        public EmployeesController(VacationDbContext context)
        {
            _context = context;
        }
        public IActionResult Employees()
        {
            return View(_context.Employees.Include(d=>d.Department).OrderBy(x=>x.Name).ToList());
        }

        public IActionResult Create()
        {
            ViewBag.departments=_context.Departments.OrderBy(d=>d.Name).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee model)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Employees));
            }
            ViewBag.departments = _context.Departments.OrderBy(d => d.Name).ToList();
            return View();
        }

        public IActionResult Edit(int? Id)
        {
            ViewBag.departments = _context.Departments.OrderBy(d => d.Name).ToList();
            return View(_context.Employees.Include(d=>d.Department).FirstOrDefault(e=>e.Id==Id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee model)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Update(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Employees));
            }
            ViewBag.departments = _context.Departments.OrderBy(d => d.Name).ToList();
            return View();
        }

        public IActionResult Delete(int? Id)
        {
          return View(_context.Employees.Include(d => d.Department).FirstOrDefault(e => e.Id == Id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Employee model)
        {
            if (ModelState !=null)
            {
                _context.Employees.Remove(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Employees));
            }
            return View();
        }
    }
}
