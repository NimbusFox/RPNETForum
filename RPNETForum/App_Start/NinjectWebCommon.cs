using System.Web.Hosting;
using RPNETForum.Classes;
using RPNETForum.Classes.Forum;
using RPNETForum.Interfaces.DatabaseMethods;
using RPNETForum.Validation;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(RPNETForum.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(RPNETForum.App_Start.NinjectWebCommon), "Stop")]

namespace RPNETForum.App_Start {
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using System.IO;
    using System.Collections.Generic;

    public static class NinjectWebCommon {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop() {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel() {
            var kernel = new StandardKernel();
            try {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            } catch {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel) {
            if (!Directory.Exists(HostingEnvironment.MapPath("~/App_Data"))) {
                Directory.CreateDirectory(HostingEnvironment.MapPath("~/App_Data"));
            }

            if (Settings.DatabaseType == DatabaseTypes.LiteDB) {
                kernel.Bind<ICategoryMethods>().To<DatabaseMethods.LiteDB.CategoryMethods>();
                kernel.Bind<IEmailTemplateMethods>().To<DatabaseMethods.LiteDB.EmailTemplateMethods>();
                kernel.Bind<IForumMethods>().To<DatabaseMethods.LiteDB.ForumMethods>();
                kernel.Bind<IPostMethods>().To<DatabaseMethods.LiteDB.PostMethods>();
                kernel.Bind<IThreadMethods>().To<DatabaseMethods.LiteDB.ThreadMethods>();
                kernel.Bind<IUserMethods>().To<DatabaseMethods.LiteDB.UserMethods>();
            } else if (Settings.DatabaseType == DatabaseTypes.MySql) {
                kernel.Bind<ICategoryMethods>().To<DatabaseMethods.MySql.CategoryMethods>();
                kernel.Bind<IEmailTemplateMethods>().To<DatabaseMethods.MySql.EmailTemplateMethods>();
                kernel.Bind<IForumMethods>().To<DatabaseMethods.MySql.ForumMethods>();
                kernel.Bind<IPostMethods>().To<DatabaseMethods.MySql.PostMethods>();
                kernel.Bind<IThreadMethods>().To<DatabaseMethods.MySql.ThreadMethods>();
                kernel.Bind<IUserMethods>().To<DatabaseMethods.MySql.UserMethods>();
            } else if (Settings.DatabaseType == DatabaseTypes.Sqlite) {
                kernel.Bind<ICategoryMethods>().To<DatabaseMethods.Sqlite.CategoryMethods>();
                kernel.Bind<IEmailTemplateMethods>().To<DatabaseMethods.Sqlite.EmailTemplateMethods>();
                kernel.Bind<IForumMethods>().To<DatabaseMethods.Sqlite.ForumMethods>();
                kernel.Bind<IPostMethods>().To<DatabaseMethods.Sqlite.PostMethods>();
                kernel.Bind<IThreadMethods>().To<DatabaseMethods.Sqlite.ThreadMethods>();
                kernel.Bind<IUserMethods>().To<DatabaseMethods.Sqlite.UserMethods>();
            } else {
                throw new Exception("Invalid DatabaseType");
            }

            if (kernel.Get<IEmailTemplateMethods>().GetTemplateCount() == 0) {
                kernel.Get<IEmailTemplateMethods>().CreateTemplate("Verification", "Hello {username},<br/><br/>Click <a href=\"{url}/Verify/{verification}\">here</a> to verify your account");
            }

            if (kernel.Get<ICategoryMethods>().CountCategories() == 0) {
                kernel.Get<ICategoryMethods>().CreateCategory(1, "First Category", "A demo description");
            }

            if (kernel.Get<IForumMethods>().CountForums() == 0) {
                kernel.Get<IForumMethods>().AddForum(new Forum {
                    Banned = new List<int>(),
                    CategoryID = 1,
                    Creator = 1,
                    Description = "A demo description",
                    Name = "First Forum",
                    Permissions = new ForumPermissions()
                });
            }

            if (kernel.Get<IThreadMethods>().CountThreads() == 0) {
                kernel.Get<IThreadMethods>().AddThread(new Thread {
                    Creator = 1,
                    Forum = 1,
                    Description = "A demo description",
                    Name = "First Thread",
                    Banned = new List<int>(),
                    Permissions = new ForumPermissions()
                });
            }

            if (kernel.Get<IPostMethods>().CountPosts() == 0) {
                kernel.Get<IPostMethods>().AddPost(new Post {
                    Edited = false,
                    EditedAt = new DateTime(),
                    Message = "This the first post of the thread",
                    PostedAt = DateTime.Now,
                    Poster = 1,
                    Thread = 1
                });
            }

            new Users(kernel.Get<IUserMethods>(), kernel.Get<IEmailTemplateMethods>());
        }
    }
}
