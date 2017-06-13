using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPNETForum.Interfaces.Users {
    public interface IUser {
        int Id { get; set; }
        string Username { get; set; }
        string DisplayName { get; set; }
        string Salt { get; set; }
        string Password { get; set; }
        string Email { get; set; }
        bool Verified { get; set; }
        bool HasGravatar { get; set; }
        string ProfilePic { get; set; }
        DateTime LastLogin { get; set; }
        DateTime RegistrationDate { get; set; }
    }
}
