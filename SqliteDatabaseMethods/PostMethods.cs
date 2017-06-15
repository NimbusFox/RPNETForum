using System.Collections.Generic;
using RPNETForum.Interfaces.DatabaseMethods;
using RPNETForum.Interfaces.Forum;

namespace RPNETForum.DatabaseMethods.Sqlite {
    public class PostMethods : BaseMethods, IPostMethods {
        public List<IPost> GetPosts(int threadID) {
            throw new System.NotImplementedException();
        }

        public IPost GetPost(int postID) {
            throw new System.NotImplementedException();
        }

        public bool PostExists(int postID) {
            throw new System.NotImplementedException();
        }

        public void AddPost(IPost post) {
            throw new System.NotImplementedException();
        }

        public void UpdatePost(IPost post) {
            throw new System.NotImplementedException();
        }

        public void RemovePost(int postID) {
            throw new System.NotImplementedException();
        }

        public int CountPosts(int threadID) {
            throw new System.NotImplementedException();
        }

        public int CountPosts() {
            throw new System.NotImplementedException();
        }
    }
}