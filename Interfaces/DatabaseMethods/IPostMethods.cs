using System.Collections.Generic;
using RPNETForum.Interfaces.Forum;

namespace RPNETForum.Interfaces.DatabaseMethods {
    public interface IPostMethods {
        List<IPost> GetPosts(int threadID);
        IPost GetPost(int postID);
        bool PostExists(int postID);
        void AddPost(IPost post);
        void UpdatePost(IPost post);
        void RemovePost(int postID);
        int CountPosts(int threadID);
        int CountPosts();
    }
}