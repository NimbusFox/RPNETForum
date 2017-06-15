using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RPNETForum.Classes.Users;
using RPNETForum.Interfaces.DatabaseMethods;

namespace RPNETForum.Controllers.Forum {
    public class ThreadController : Controller {
        private IThreadMethods _threadMethods;
        private IPostMethods _postMethods;
        private IUserMethods _userMethods;

        public ThreadController(IThreadMethods threadMethods, IPostMethods postMethods, IUserMethods userMethods) {
            _threadMethods = threadMethods;
            _postMethods = postMethods;
            _userMethods = userMethods;
        }

        public ActionResult Index(int id) {
            var thread = _threadMethods.GetThread(id);

            var posts = _postMethods.GetPosts(thread.Id);

            var users = new List<User>();

            foreach (var post in posts) {
                if (!users.Any(x => x.Id == post.Poster)) {
                    if (_userMethods.UserExists(post.Poster)) {
                        users.Add((User) _userMethods.GetUserByID(post.Poster));
                    }
                }
            }

            return View(new Tuple<Interfaces.Forum.IThread, List<Interfaces.Forum.IPost>, List<User>>(thread, posts, users));
        }
    }
}