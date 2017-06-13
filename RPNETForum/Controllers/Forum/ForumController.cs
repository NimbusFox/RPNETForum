using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RPNETForum.Controllers.Forum {
    public class ForumController : Controller {
        // GET: Forum
        public ActionResult Index(int id) {
            return View();
        }
    }
}