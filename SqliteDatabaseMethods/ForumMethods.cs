using System.Collections.Generic;
using RPNETForum.Interfaces.DatabaseMethods;
using RPNETForum.Interfaces.Forum;

namespace RPNETForum.DatabaseMethods.Sqlite {
    public class ForumMethods : BaseMethods, IForumMethods {
        public int CountForums() {
            throw new System.NotImplementedException();
        }

        public int CountForums(int categoryID) {
            throw new System.NotImplementedException();
        }

        public List<IForum> GetForums(int categoryID) {
            throw new System.NotImplementedException();
        }

        public IForum GetForum(int forumID) {
            throw new System.NotImplementedException();
        }

        public void AddForum(IForum forum) {
            throw new System.NotImplementedException();
        }

        public void UpdateForum(IForum forum) {
            throw new System.NotImplementedException();
        }

        public void RemoveForum(int forumID) {
            throw new System.NotImplementedException();
        }
    }
}