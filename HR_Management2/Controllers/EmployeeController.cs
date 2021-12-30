using HR_Management2.Data;
using HR_Management2.Models;
using HR_Management2.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management2.Controllers
{
    //[Authorize(Roles = Helper.Admin)]
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


        public IActionResult ListAbsences_chart(int? id)
        { 
            var months = _db.Absences
                    .Where(x => x.EmployeeId == id)
                    .GroupBy(a => a.StartDate.Month)
                    
                    .Select(a => new ListAbsences
                    {
                        Total = a.Key
                    }).ToList();

            List<int> listMonths=new List<int>();
            foreach (var item in months)
                listMonths.Add(item.Total);

            var durations = _db.Absences
                        .Where(x => x.EmployeeId == id)
                        .Select(abs => new Absence
                        {
                            Id = abs.Id,
                            StartDate = abs.StartDate,
                            Duration = abs.Duration
                        })

                        .GroupBy(s => new { s.StartDate.Year, s.StartDate.Month })
                        .Select(a => new ListAbsences
                        {
                            Total = a.Sum(w => w.Duration)
                        })
                        .ToList();

            List<int> listDurations = new List<int>();
            foreach (var item in durations)
                listDurations.Add(item.Total);

            ViewBag.Months = Newtonsoft.Json.JsonConvert.SerializeObject(listMonths);
            ViewBag.Durations = Newtonsoft.Json.JsonConvert.SerializeObject(listDurations);
            ViewBag.Amount = listMonths.Count;
            return View();
            
        }

    }
}
