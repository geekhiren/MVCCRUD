using MVCCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace MVCCRUD.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        public ActionResult Index()
        {
            using (Trainee2021Entities db = new Trainee2021Entities())
            {
                var Result = db.Departments.ToList();
                return View(Result);
            }
        }

        public ActionResult Create()
        {
            using (Trainee2021Entities db = new Trainee2021Entities())
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Create(Department dep)
        {
            if (ModelState.IsValid)
            {
                using (var db = new Trainee2021Entities())
                {
                    db.Departments.Add(dep);
                    db.SaveChanges();
                    return RedirectToAction("index");
                }
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            using (var db = new Trainee2021Entities())
            {
                var tempdata = db.Departments.Where(a => a.id.Equals(id)).FirstOrDefault();
                return View(tempdata);
            }
        }
        [HttpPost]
        public ActionResult Edit(Department dep)
        {
            if (ModelState.IsValid)
            {
                using (var db = new Trainee2021Entities())
                {
                    var depdata = db.Departments.Where(a => a.id.Equals(dep.id)).FirstOrDefault();
                    if (depdata != null)
                    {
                        depdata.Name = dep.Name;
                        depdata.Description = dep.Description;
                    }
                    db.Entry(depdata).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("index");
                }
            }
            return View();
        }
        public ActionResult Details(int id)
        {
            using (var db = new Trainee2021Entities())
            {
                var tempdata = db.Departments.ToList().Where(a => a.id.Equals(id)).FirstOrDefault();

                return View(tempdata);
            }
        }
        public ActionResult Delete(int id)
        {
            using (var db = new Trainee2021Entities())
            {
                var temp1 = db.Departments.Where(x => x.id == id).FirstOrDefault();
                var tempdata = db.Departments.Where(a => a.id.Equals(id)).FirstOrDefault();
                db.Departments.Remove(temp1);
                db.SaveChanges();
                return RedirectToAction("index");
            }
        }
    }
}