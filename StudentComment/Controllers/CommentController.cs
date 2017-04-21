using StudentComment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentComment.Controllers
{
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

        public ActionResult AddComment()
        {
            return View();
        }
	}
}