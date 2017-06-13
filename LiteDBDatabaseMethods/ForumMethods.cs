using System.Collections.Generic;
using System.Linq;
using RPNETForum.Classes.Forum;
using RPNETForum.Interfaces.DatabaseMethods;
using RPNETForum.Interfaces.Forum;

namespace RPNETForum.DatabaseMethods.LiteDB {
    public class ForumMethods : BaseMethods, IForumMethods {
        public int CountForums() {
            return _forumDB.Count();
        }

        public int CountForums(int categoryID) {
            return _forumDB.Count(x => x.CategoryID == categoryID);
        }

        public List<IForum> GetForums(int categoryID) {
            return _forumDB.Find(x => x.CategoryID == categoryID).ToList<IForum>();
        }

        public IForum GetForum(int forumID) {
            return _forumDB.FindOne(x => x.Id == forumID);
        }

        public void AddForum(IForum forum) {
            _forumDB.Insert((Forum)forum);
        }

        public void UpdateForum(IForum forum) {
            _forumDB.Update((Forum)forum);
        }

        public void RemoveForum(int forumID) {
            _forumDB.Delete(x => x.Id == forumID);
        }
    }
}