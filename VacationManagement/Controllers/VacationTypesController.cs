using Microsoft.AspNetCore.Mvc;
using VacationManagement.Data;
using VacationManagement.Models;

namespace VacationManagement.Controllers
{
    public class VacationTypesController : Controller
    {
        private readonly VacationDbContext _context;

        public VacationTypesController(VacationDbContext context)
        {
            _context = context;
        }
        public IActionResult VacationTypes()
        {
            return View(_context.VacationTypes.OrderBy(v=>v.VacationName).ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VacationType model)
        {
            if (ModelState.IsValid)
            if (ModelState.IsValid)
            {
                var result = _context.VacationTypes.FirstOrDefault(v => v.VacationName.Contains(model.VacationName.Trim()));
                if(result == null)
                {
                    _context.VacationTypes.Add(model);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(VacationTypes));

                }
                ViewBag.ErrorMsg = false;
               
            }
            return View(model);
        }

        public IActionResult Edit(int? Id)
        {
            return View(_context.VacationTypes.FirstOrDefault(v=>v.Id==Id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(VacationType model)
        {
            if (ModelState.IsValid)
            {
                _context.VacationTypes.Update(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(VacationTypes));
            }
            return View(model);
        }

        public IActionResult Delete(int? Id)
        {
            return View(_context.VacationTypes.FirstOrDefault(v => v.Id == Id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(VacationType model)
        {
            if (ModelState != null)
            {
                _context.VacationTypes.Remove(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(VacationTypes));
            }
            return View(model);
        }
    }
}
