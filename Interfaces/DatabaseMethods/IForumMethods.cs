using System.Collections.Generic;
using RPNETForum.Interfaces.Forum;

namespace RPNETForum.Interfaces.DatabaseMethods {
    public interface IForumMethods {
        int CountForums();
        int CountForums(int categoryID);
        List<IForum> GetForums(int categoryID);
        IForum GetForum(int forumID);
        void AddForum(IForum forum);
        void UpdateForum(IForum forum);
        void RemoveForum(int forumID);
    }
}