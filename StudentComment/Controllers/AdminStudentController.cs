using StudentComment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentComment.Controllers
{
    public class AdminStudentController : Controller
    {
        //
        // GET: /AdminStudent/
        public ActionResult List()
        {
            return View();
        }

        public ActionResult Add(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                using (var db = new ApplicationDbContext())
                {
                    var model = db.Students.Find(Id);
                    return View("edit",model);
                }
            }
            return View(new Student());
        }

        [HttpPost]
        public ActionResult Add(Student model)
        {
            using (var db=new ApplicationDbContext())
            {
                try
                {
                    db.Students.Add(model);
                    db.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    ViewBag.Error = "Tc Kimlik Hatsı";
                    return View(model);
                }
              

                
            }

            return View(new Student());
        }
        
        [HttpPost]
        public ActionResult Edit(Student model)
        {
            using (var db=new ApplicationDbContext())
            {
                try
                {
                    var origin = db.Students.Find(model.Id);

                    origin.Name = model.Name;
                    origin.SurName = model.SurName;
                    origin.Department = model.Department;
                    origin.Class = model.Class;
                    origin.Number = model.Number;
                    db.Students.Attach(origin);
                    db.Entry(origin).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    ViewBag.Error = "Tc Kimlik Hatsı";
                    return View(model);
                }
              

                
            }

            return View(new Student());
        }
	}
}