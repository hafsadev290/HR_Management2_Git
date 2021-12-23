using HR_Management2.Data;
using HR_Management2.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management2.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public EmployeeController(ApplicationDbContext _db)
        {
            this._db = _db;
        }
        public IActionResult Index()
        {
            IEnumerable<Employee> obj_list = _db.Employees;
            return View(obj_list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee emp)
        {
            if (ModelState.IsValid)
            {
                _db.Employees.Add(emp);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emp);
        }

        public IActionResult Update(int? id)
        {
            if (id == 0 || id == null)
                return NotFound();

            var employee = _db.Employees.Find(id);
            if (employee == null)
                return NotFound();
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _db.Employees.Update(employee);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        public IActionResult ListAbsences(int? id)
        {
            
            var absences = _db.Absences.Where(x => x.EmployeeId == id).ToList().Select(abs => new Absence
            {
                Id = abs.Id,
                AbsenceType = new AbsenceType
                {
                    Id = abs.AbsenceTypeId,
                    AbsenceName = _db.AbsenceTypes.Where(x => x.Id == abs.AbsenceTypeId).Select(x => x.AbsenceName).FirstOrDefault()
                },
                Duration = abs.Duration,
                StartDate = abs.StartDate,
                Employee = new Employee
                {
                    LastName = _db.Employees.Where(x => x.Id == abs.EmployeeId).Select(x => x.LastName).FirstOrDefault()
                },
                EmployeeId = abs.EmployeeId,
                AbsenceTypeId = abs.AbsenceTypeId
            }).ToList();
            //IEnumerable<Absence> absences = _db.Absences.Where(x => x.EmployeeId == id);
            return View(absences);
        }

    }
}
