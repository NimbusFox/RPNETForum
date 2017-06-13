using RPNETForum.Interfaces.Users;

namespace RPNETForum.Classes.Users {
    public class Session : ISession {
        public string SessionToken { get; set; }
        public int UID { get; set; }
    }
}