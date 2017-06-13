using System.Collections.Generic;
using RPNETForum.Interfaces.Forum;

namespace RPNETForum.Classes.Forum {
    public class Category : ICategory {
        public int Creator { get; set; }
        public string Name { get; set; }
        public List<int> Forums { get; set; }
        public string Description { get; set; }
    }
}