using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StudentComment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;

namespace StudentComment.Controllers
{
    //[Authorize(Roles="Admin")]
    public class AdminTeacherController : Controller
    {
         public AdminTeacherController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

         public AdminTeacherController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }
        //
        // GET: /AdminTeacher/
        public ActionResult Index()
        {

            using (var user =new ApplicationDbContext())
            {
                var data = user.Users.Select(x => new TeacherViewModel()
                {
                    Id=x.Id,
                    UserName=x.UserName,
                    Name=x.Name,
                    SurName=x.SurName,
                    Picture=x.Picture,

                }).ToList();
                return View(data);

            }
        }

        //
        // GET: /AdminTeacher/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /AdminTeacher/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AdminTeacher/Create
        [HttpPost]
        public ActionResult Create(TeacherViewModel model,HttpPostedFileBase file)
        {
            try
            {
                // TODO: Add insert logic here
                if (file!=null && file.ContentLength>0)
                {
                    string[] dizi = file.FileName.Split('.');
                    var fileName = Path.GetFileName(DateTime.Now.Ticks.ToString() + "." + dizi[1]);
                    var path = Path.Combine(Server.MapPath("~/Images/Teachers/"),fileName);
                    file.SaveAs(path);
                    model.Picture="../Images/Teachers/"+fileName;
                }


                var user = new ApplicationUser() { UserName = model.UserName,Name=model.Name, SurName = model.SurName,Picture=model.Picture};
                var result =  UserManager.Create(user, model.NewPassword);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "User");
                    return RedirectToAction("Index");
                }
               
            }
            catch
            {

            }
            return View(model);

        }

        //
        // GET: /AdminTeacher/Edit/5
        public ActionResult Edit(string id)
        {
            using (var db=new ApplicationDbContext())
            {
                var user = db.Users.Find(id);
                var teacherModel = new TeacherViewModel();
                teacherModel.Id = user.Id;
                teacherModel.UserName = user.UserName;
                teacherModel.Name = user.Name;
                teacherModel.SurName = user.SurName;
                return View(teacherModel);

            }

        }

        //
        // POST: /AdminTeacher/Edit/5
        [HttpPost]
        public ActionResult Edit(TeacherViewModel model)
        {
            
            try
            {
                // TODO: Add update logic here
                using (var db=new ApplicationDbContext())
                {
                    var origin = db.Users.Find(model.Id);
                    origin.Name = model.Name;
                    origin.SurName = model.SurName;
                    db.Users.Attach(origin);
                    db.Entry(origin).State = EntityState.Modified;
                    db.SaveChanges();
                    if (!string.IsNullOrEmpty(model.OldPassword) && !string.IsNullOrEmpty(model.NewPassword))
                    {
                         UserManager.ChangePassword(model.Id, model.OldPassword, model.NewPassword);
                    }
                   
                    return RedirectToAction("Index");
                }

                
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /AdminTeacher/Delete/5
        public ActionResult Delete(string id)
        {
            using (var db=new ApplicationDbContext())
            {
                var user = db.Users.Find(id);
                var teacherModel = new TeacherViewModel();
                teacherModel.Id = user.Id;
                teacherModel.UserName = user.UserName;
                teacherModel.Name = user.Name;
                teacherModel.SurName = user.SurName;
                return View(teacherModel);
            }
        }

        //
        // POST: /AdminTeacher/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string Id)
        {
            try
            {
                // TODO: Add delete logic here
                using (var db=new ApplicationDbContext())
                {
                    var user = db.Users.Find(Id);
                    db.Users.Remove(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
