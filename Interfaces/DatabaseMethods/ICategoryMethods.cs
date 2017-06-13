using System.Collections.Generic;
using RPNETForum.Interfaces.Forum;

namespace RPNETForum.Interfaces.DatabaseMethods {
    public interface ICategoryMethods {
        List<ICategory> GetCategories();
        int CountCategories();
        void CreateCategory(int creator, string name, string description);
    }
}