using MVCCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace MVCCRUD.Controllers
{
    public class ExamController : Controller
    {
        // GET: Exam
        public ActionResult Index()
        {
            using (Trainee2021Entities db = new Trainee2021Entities())
            {
                var Result = db.Exams.Include(x => x.Hiren_Employee).ToList();
                return View(Result);
            }
        }

        public ActionResult Create()
        {
            using (Trainee2021Entities db = new Trainee2021Entities())
            {
                var Result = db.Hiren_Employee.Select(x => x.id).ToList();
                ViewBag.EmployeeIds = Result;
                return View();
            }
        }
        [HttpPost]
        public ActionResult Create(Exam exam)
        {
            if (ModelState.IsValid)
            {
                using (var db = new Trainee2021Entities())
                {
                    db.Exams.Add(exam);
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
                 var Result = db.Hiren_Employee.Select(x => x.id).ToList();
                ViewBag.EmployeeIds = Result;
                var tempdata = db.Exams.Where(a => a.id.Equals(id)).FirstOrDefault();
                return View(tempdata);
            }
        }
        [HttpPost]
        public ActionResult Edit(Exam exam)
        {
            if (ModelState.IsValid)
            {
                using (var db = new Trainee2021Entities())
                {
                    var examdata = db.Exams.Where(a => a.id.Equals(exam.id)).FirstOrDefault();
                    if (examdata != null)
                    {
                        examdata.Hiren_Employeeid = exam.Hiren_Employeeid;
                        examdata.Title = exam.Title;
                        examdata.Description = exam.Description;
                        examdata.Marks = exam.Marks;
                        examdata.Exam_date = exam.Exam_date;
                    }
                    db.Entry(examdata).State = EntityState.Modified;
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
                var tempdata = db.Exams.Include(x => x.Hiren_Employee).ToList().Where(a => a.id.Equals(id)).FirstOrDefault();

                return View(tempdata);
            }
        }
        public ActionResult Delete(int id)
        {
            using (var db = new Trainee2021Entities())
            {
                var temp1 = db.Exams.Where(x => x.id == id).FirstOrDefault();
                var tempdata = db.Exams.Where(a => a.id.Equals(id)).FirstOrDefault();
                db.Exams.Remove(temp1);
                db.SaveChanges();
                return RedirectToAction("index");
            }
        }
    }
}