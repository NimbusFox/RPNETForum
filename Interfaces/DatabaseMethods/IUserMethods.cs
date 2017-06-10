using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPNETForum.Interfaces.DatabaseMethods {
    public interface IUserMethods {
        bool UserExists(string name);
        bool UserExists(int id);
    }
}
