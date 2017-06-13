using System.Collections.Generic;
using RPNETForum.Interfaces.Forum;

namespace RPNETForum.Classes.Forum {
    public class ForumPermission : IForumPermission {
        public List<int> U { get; set; }
        public List<int> G { get; set; }
    }

    public class Forum : IForum {
        public int Id { get; set; }
        public int CategoryID { get; set; }
        public int Creator { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IForumPermissions Permissions { get; set; }
        public List<int> Banned { get; set; }
    }

    public class ForumPermissions : IForumPermissions {
        public IForumPermission CanRead { get; set; }
        public IForumPermission CanWrite { get; set; }
        public IForumPermission CanEdit { get; set; }
        public IForumPermission CanDelete { get; set; }
        public IForumPermission CanOEdit { get; set; }
        public IForumPermission CanODelete { get; set; }
        public IForumPermission CanBan { get; set; }
    }
}