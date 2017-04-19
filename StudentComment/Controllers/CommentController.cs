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

        public ActionResult List()
        {
            return View();
        }
	}
}