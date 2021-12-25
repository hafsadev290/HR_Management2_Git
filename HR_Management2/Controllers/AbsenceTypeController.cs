using HR_Management2.Data;
using HR_Management2.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management2.Controllers
{
    public class AbsenceTypeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AbsenceTypeController(ApplicationDbContext _db)
        {
            this._db = _db;
        }
        public IActionResult Index()
        {
            IEnumerable<AbsenceType> obj_list = _db.AbsenceTypes;
            return View(obj_list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AbsenceType absType)
        {
            if (ModelState.IsValid)
            {
                _db.AbsenceTypes.Add(absType);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(absType);
        }

        public IActionResult Update(int? id)
        {
            if (id == 0 || id == null)
                return NotFound();

            var obj = _db.AbsenceTypes.Find(id);
            if (obj == null)
                return NotFound();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(AbsenceType absType)
        {
            if (ModelState.IsValid)
            {
                _db.AbsenceTypes.Update(absType);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(absType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.AbsenceTypes.Find(id);
            if (obj == null)
                return NotFound();
            _db.AbsenceTypes.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
                return NotFound();

            var obj = _db.AbsenceTypes.Find(id);
            if (obj == null)
                return NotFound();
            return View(obj);
        }
    }
}
