using RPNETForum.Interfaces.Users;

namespace RPNETForum.Classes.Users {
    public class Verification : IVerification {
        public string VerificationToken { get; set; }
        public int UID { get; set; }
    }
}