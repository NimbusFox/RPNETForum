using System.Collections.Generic;
using RPNETForum.Interfaces.DatabaseMethods;
using RPNETForum.Interfaces.Forum;

namespace RPNETForum.DatabaseMethods.Sqlite {
    public class CategoryMethods : BaseMethods, ICategoryMethods {
        public List<ICategory> GetCategories() {
            throw new System.NotImplementedException();
        }

        public int CountCategories() {
            throw new System.NotImplementedException();
        }

        public void CreateCategory(int creator, string name, string description) {
            throw new System.NotImplementedException();
        }
    }
}