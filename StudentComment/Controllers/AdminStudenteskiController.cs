using StudentComment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentComment.Controllers
{
    public class AdminStudenteskiController : Controller
    {
        //
        // GET: /AdminStudent/
        public ActionResult List()
        {
            using (var db = new ApplicationDbContext())
            {
                var model = db.Students.ToList();
                return View(model);
            }

        }

        public ActionResult Add(string Id)
        {


            if (!string.IsNullOrEmpty(Id))
            {
                using (var db = new ApplicationDbContext())
                {
                    var model = db.Students.Find(Id);
                    return View("edit", model);
                }
            }
            return View(new Student());
        }

        [HttpPost]
        public ActionResult Add(Student model)
        {
            using (var db = new ApplicationDbContext())
            {

                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(DateTime.Now.Ticks.ToString() + file.FileName);
                        var path = Path.Combine(Server.MapPath("~/students/"), fileName);
                        file.SaveAs(path);
                        model.Picture = "../students/" + fileName;
                    }
                }
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
            using (var db = new ApplicationDbContext())
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
            return RedirectToAction("List");
        }





        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Student student = db.Students.Find(id);
        //    if (student == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(student);
        //}

        public ActionResult Delete(string Id)
        {
            using (var db = new ApplicationDbContext())
            {
                Student student = db.Students.Find(Id);
                return View(student);
            }
        }






        public ActionResult DeleteConfirmed(string Id)
        {
            using (var db = new ApplicationDbContext())
            {
                try
                {
                    Student student = db.Students.Find(Id);
                    db.Students.Remove(student);
                    db.SaveChanges();
                    return RedirectToAction("List");
                }
                catch (DbUpdateException e)
                {
                    ViewBag.Error = "Silme Sırasında hata Oluştu";
                    return RedirectToAction("List");
                }
            }
        }

    }
}