using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPNETForum.Interfaces.DatabaseMethods;

namespace RPNETForum.DatabaseMethods.MySql {
    public class UserMethods : IUserMethods {
        public bool UserExists(string name) {
            return true;
        }

        public bool UserExists(int id) {
            return true;
        }
    }
}
