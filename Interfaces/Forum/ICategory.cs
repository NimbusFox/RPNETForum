using System.Collections.Generic;

namespace RPNETForum.Interfaces.Forum {
    public interface ICategory {
        int Creator { get; set; }
        string Name { get; set; }
        List<int> Forums { get; set; }
        string Description { get; set; }
    }
}
