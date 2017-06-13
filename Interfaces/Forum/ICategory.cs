using System.Collections.Generic;

namespace RPNETForum.Interfaces.Forum {
    public interface ICategory {
        int Id { get; set; }
        int Creator { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}
