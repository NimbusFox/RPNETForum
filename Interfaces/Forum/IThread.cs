using System;
using System.Collections.Generic;

namespace RPNETForum.Interfaces.Forum {
    public interface IThread {
        int Id { get; set; }
        int Forum { get; set; }
        int Creator { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        IForumPermissions Permissions { get; set; }
        List<int> Banned { get; set; }
    }
}