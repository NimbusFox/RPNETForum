using System.Collections.Generic;

namespace RPNETForum.Interfaces.Forum {
    public interface IForumPermission {
        List<int> U { get; set; }
        List<int> G { get; set; }
    }

    public interface IForum {
        int Id { get; set; }
        int Creator { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        IForumPermissions Permissions { get; set; }
        List<int> Banned { get; set; }
    }

    public interface IForumPermissions {
        IForumPermission CanRead { get; set; }
        IForumPermission CanWrite { get; set; }
        IForumPermission CanEdit { get; set; }
        IForumPermission CanDelete { get; set; }
        IForumPermission CanOEdit { get; set; }
        IForumPermission CanODelete { get; set; }
        IForumPermission CanBan { get; set; }
    }
}