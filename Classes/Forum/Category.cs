using System.Collections.Generic;
using RPNETForum.Interfaces.Forum;

namespace RPNETForum.Classes.Forum {
    public class Category : ICategory {
        public int Id { get; set; }
        public int Creator { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}