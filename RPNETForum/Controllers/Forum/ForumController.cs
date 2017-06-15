using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RPNETForum.Interfaces.DatabaseMethods;
using RPNETForum.Interfaces.Forum;

namespace RPNETForum.Controllers.Forum {
    public class ForumController : Controller {
        private IForumMethods _forumMethods;
        private IThreadMethods _threadMethods;

        public ForumController(IForumMethods forumMethods, IThreadMethods threadMethods) {
            _forumMethods = forumMethods;
            _threadMethods = threadMethods;
        }


        public ActionResult Index(int id) {
            var forum = _forumMethods.GetForum(id);

            var threads = _threadMethods.GetThreads(forum.Id);

            return View(new Tuple<IForum, List<IThread>>(forum, threads));
        }
    }
}