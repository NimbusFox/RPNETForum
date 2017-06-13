namespace RPNETForum.Interfaces.Users {
    public interface IVerification {
        string VerificationToken { get; set; }
        int UID { get; set; }
    }
}