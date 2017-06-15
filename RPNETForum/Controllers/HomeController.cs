using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Hosting;
using System.Web.Mvc;
using RPNETForum.Classes.Models;
using RPNETForum.Extensions.Users;
using RPNETForum.Interfaces.DatabaseMethods;
using RPNETForum.Interfaces.Forum;

namespace RPNETForum.Controllers {
    public class HomeController : Controller {
        private IUserMethods _userMethods;
        private ICategoryMethods _categoryMethods;
        private IForumMethods _forumMethods;

        public HomeController(IUserMethods userMethods, ICategoryMethods categoryMethods, IForumMethods forumMethods) {
            _userMethods = userMethods;
            _categoryMethods = categoryMethods;
            _forumMethods = forumMethods;
        }

        public ActionResult Index() {
            var catergories = _categoryMethods.GetCategories();
            var forums = new Dictionary<int, List<IForum>>();

            foreach (var catergory in catergories) {
                forums.Add(catergory.Id, _forumMethods.GetForums(catergory.Id));
            }

            return View(new Tuple<List<ICategory>, Dictionary<int, List<IForum>>>(catergories, forums));
        }

        public ActionResult Verify(string token) {
            return _userMethods.Verify(token) ? View("Redirect", new RedirectModel {
                Message = "You will now be redirected to the login page.",
                RedirectSeconds = 3,
                RedirectTo = Settings.Url + "/Login",
                Title = "Verification Successful"
            }) : View("Redirect", new RedirectModel {
                Message = "If you followed a link please check if it's from an official source",
                RedirectSeconds = 3,
                RedirectTo = Settings.Url,
                Title = "Invalid verification"
            });
        }

        public ActionResult Logout() {
            RPNETForum.Users.Logout();

            return View("Redirect", new RedirectModel {
                Message = "You have been successfully logged out",
                RedirectSeconds = 3,
                RedirectTo = string.IsNullOrWhiteSpace(UserSession.PreviousURL) || string.IsNullOrWhiteSpace(UserSession.PreviousURL) ? Settings.Url : UserSession.PreviousURL,
                Title = "Logged out"
            });
        }

        public ActionResult Avatar(int id) {
            if (!Directory.Exists(HostingEnvironment.MapPath("~/App_Data/ImgCache"))) {
                Directory.CreateDirectory(HostingEnvironment.MapPath("~/App_Data/ImgCache"));
            }

            if (!_userMethods.UserExists(id)) {
                return HttpNotFound();
            }

            var user = _userMethods.GetUserByID(id);

            if (!System.IO.File.Exists(HostingEnvironment.MapPath("~/App_Data/ImgCache/" + user.Id))) {
                var webClient = new WebClient();

                webClient.DownloadFile(user.GetProfilePic(), HostingEnvironment.MapPath("~/App_Data/ImgCache/" + user.Id));
            }

            var mime =
                System.Web.MimeMapping.GetMimeMapping(HostingEnvironment.MapPath("~/App_Data/ImgCache/" + user.Id));

            if (mime == "application/octet-stream") {
                mime = "image/png";
            }

            Response.ContentType = mime;

            return File(HostingEnvironment.MapPath("~/App_Data/ImgCache/" + user.Id), mime);
        }
    }
}