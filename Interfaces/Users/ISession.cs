namespace RPNETForum.Interfaces.Users {
    public interface ISession {
        string SessionToken { get; set; }
        int UID { get; set; }
    }
}