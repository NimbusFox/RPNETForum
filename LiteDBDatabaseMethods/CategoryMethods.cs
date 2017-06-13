using System.Collections.Generic;
using System.Linq;
using RPNETForum.Classes.Forum;
using RPNETForum.Interfaces.DatabaseMethods;
using RPNETForum.Interfaces.Forum;

namespace RPNETForum.DatabaseMethods.LiteDB {
    public class CategoryMethods : BaseMethods, ICategoryMethods {
        public List<ICategory> GetCategories() {
            return CountCategories() == 0 ? new List<ICategory>() : _categoryDB.FindAll().ToList<ICategory>();
        }

        public int CountCategories() {
            return _categoryDB.Count();
        }

        public void CreateCategory(int creator, string name, string description) {
            var category = new Category {
                Creator = creator,
                Name = name,
                Description = description
            };

            _categoryDB.Insert(category);
        }
    }
}