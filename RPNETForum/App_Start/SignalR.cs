using Microsoft.Owin;
using Owin;
using RPNETForum;

namespace RPNETForum {
    public class Startup {
        public void Configuration(IAppBuilder app) {
            app.MapSignalR();
        }
    }
}