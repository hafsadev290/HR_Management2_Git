using HR_Management2.Data;
using HR_Management2.Models;
using HR_Management2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management2.Controllers
{
    public class AbsenceController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AbsenceController(ApplicationDbContext _db)
        {
            this._db = _db;
        }
        public IActionResult Index()
        {
            IEnumerable<Absence> obj_list = _db.Absences;
            foreach (var abs_ in obj_list)
            {
                abs_.AbsenceType = _db.AbsenceTypes.FirstOrDefault(k => k.Id == abs_.AbsenceTypeId);
            }
            foreach (var abs_ in obj_list)
            {
                abs_.Employee = _db.Employees.FirstOrDefault(k => k.Id == abs_.EmployeeId);
            }
            return View(obj_list);
        }

        public IActionResult Create()
        {
            AbsenceVM absenceVM = new AbsenceVM()
            {
                Absence = new Absence(),
                AbsenceType = _db.AbsenceTypes.Select(i => new SelectListItem
                {
                    Text = i.AbsenceName,
                    Value = i.Id.ToString()
                }),
                Employee = _db.Employees.Select(i => new SelectListItem
                {
                    Text = i.LastName + " " + i.FirstName,
                    Value = i.Id.ToString()
                }),
            };

            //IEnumerable<SelectListItem> CategoryExpense = _db.ExpenseCategories.Select(i => new SelectListItem
            //{
            //    Text = i.CategoryName,
            //    Value = i.Id.ToString()
            //});
            //ViewBag.CategoryExpense = CategoryExpense;
            return View(absenceVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AbsenceVM absenceVM)
        {
            if (ModelState.IsValid)
            {
                _db.Absences.Add(absenceVM.Absence);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(absenceVM);
        }

        public IActionResult Update(int? id)
        {
            if (id == 0 || id == null)
                return NotFound();

            var absence = _db.Absences.Find(id);
            if (absence == null)
                return NotFound();
            AbsenceVM absenceVM = new AbsenceVM()
            {
                Absence = absence,
                AbsenceType = _db.AbsenceTypes.Select(i => new SelectListItem
                {
                    Text = i.AbsenceName,
                    Value = i.Id.ToString()
                }),
                Employee = _db.Employees.Select(i => new SelectListItem
                {
                    Text = i.LastName + " " + i.FirstName,
                    Value = i.Id.ToString()
                })
            };
            return View(absenceVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(AbsenceVM exp)
        {
            if (ModelState.IsValid)
            {
                _db.Absences.Update(exp.Absence);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(exp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var absence = _db.Absences.Find(id);
            if (absence == null)
                return NotFound();
            _db.Absences.Remove(absence);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
                return NotFound();

            var absence = _db.Absences.Find(id);
            if (absence == null)
                return NotFound();
            return View(absence);
        }
    }
}
