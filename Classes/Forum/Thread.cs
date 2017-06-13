using System.Collections.Generic;
using RPNETForum.Interfaces.Forum;

namespace RPNETForum.Classes.Forum {
    public class Thread : IThread {
        public int Id { get; set; }
        public int Forum { get; set; }
        public int Creator { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IForumPermissions Permissions { get; set; }
        public List<int> Banned { get; set; }
    }
}