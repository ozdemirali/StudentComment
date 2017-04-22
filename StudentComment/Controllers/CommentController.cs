using StudentComment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentComment.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        //
        // GET: /Comment/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult List(string val)
        {
            if (string.IsNullOrEmpty(val))
            {
                return View("Index");
            }
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var model = db.Students.Where(x => (x.Number == val) || (x.Name.Contains(val)) || (x.SurName.Contains(val))).ToList();
                    if (model.Count == 0)
                    {
                        ViewBag.Error = "Ardağınız Kişi Bulunamadı";
                        return View("index");
                    }
                    else
                    {
                        return View(model);
                    }

                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Sistemde Bir Hata Oluştu";
                return View("index");
            }
        }

        public ActionResult AddComment(string id)
        {
            try
            {

                using (var db=new ApplicationDbContext())
                {
                    var model = db.Comments.Where(x => x.Students.Id == id).ToList();
                    return View(model);
                }
            }
            catch (Exception)
            {
                return View(); 
            }
           
        }

        public ActionResult Add(string id)
        {
            try
            {
                using (var db=new ApplicationDbContext())
                {
                    db.Configuration.LazyLoadingEnabled = true;
                    var model = db.Students.Include("Comments").Where(x=>x.Id==id).FirstOrDefault();
                    return View(model);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        [HttpPost]
        public ActionResult Add(string studentId,string explanation,string status)
        {

            using (var db=new ApplicationDbContext())
            {

                Comment data = new Comment();
                data.StudentId = studentId;
                data.Explanation = explanation;
                data.Status = status;
                var userData = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
                data.TeacherName = userData.Name+" "+userData.SurName;
                data.TeacherPicture = userData.Picture;
                db.Comments.Add(data);
                db.SaveChanges();
            }
            return Json("Ok");
        }
	}
}