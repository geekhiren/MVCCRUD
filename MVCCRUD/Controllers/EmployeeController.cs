using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using MVCCRUD.Models;

namespace MVCCRUD.Controllers
{
    public class EmployeeController : Controller
    {

        // GET: Employee
        public ActionResult Index()
        {
            using (Trainee2021Entities db=new Trainee2021Entities())
            {
                var Result = db.Hiren_Employee.Include(x => x.Department).Include(x =>x.Exams).ToList();
                return View(Result);
            }
        }

        public ActionResult Create()
        {
            using (Trainee2021Entities db = new Trainee2021Entities())
            {
                var Result = db.Departments.Select(x => x.id).ToList();
                ViewBag.DepartmentIds = Result;
                return View();
            }
        }
        [HttpPost]
        public ActionResult Create(Hiren_Employee emp)
        {
            if (ModelState.IsValid)
            {
                using (var db = new Trainee2021Entities())
                {
                    db.Hiren_Employee.Add(emp);
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
                var result = db.Departments.Select(x => x.id).ToList();
                ViewBag.DepartmentIds = result;
                var tempdata = db.Hiren_Employee.Where(a => a.id.Equals(id)).FirstOrDefault();
                return View(tempdata);
            }
        }
        [HttpPost]
        public ActionResult Edit(Hiren_Employee emp)
        {
            if (ModelState.IsValid)
            {
                using (var db = new Trainee2021Entities())
                {
                    var empdata = db.Hiren_Employee.Where(a => a.id.Equals(emp.id)).FirstOrDefault();
                    if (empdata != null)
                    {
                        empdata.FirstName = emp.FirstName;
                        empdata.LastName = emp.LastName;
                        empdata.City = emp.City;
                        empdata.MobileNumber = emp.MobileNumber;
                        empdata.Gender = emp.Gender;
                        empdata.DepartmentId = emp.DepartmentId;
                        empdata.Email = emp.Email;
                        empdata.Address = emp.Address;
                        empdata.Age = emp.Age;
                        empdata.Username = emp.Username;
                        empdata.Password = emp.Password;
                        empdata.ConfirmPassword = emp.ConfirmPassword;
                    }
                    db.Entry(empdata).State = EntityState.Modified;
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
                var tempdata = db.Hiren_Employee.Include(x => x.Department).Include(x => x.Exams).ToList().Where(a => a.id.Equals(id)).FirstOrDefault();

                return View(tempdata);
            }
        }
        public ActionResult Delete(int id)
        {
            using (var db = new Trainee2021Entities())
            {
                var temp1 = db.Hiren_Employee.Where(x => x.id == id).FirstOrDefault();
                var tempdata = db.Hiren_Employee.Where(a => a.id.Equals(id)).FirstOrDefault();
                db.Hiren_Employee.Remove(temp1);
                db.SaveChanges();
                return RedirectToAction("index");
            }
        }
    }
}