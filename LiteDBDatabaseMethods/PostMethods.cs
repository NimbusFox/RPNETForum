using System.Collections.Generic;
using System.Linq;
using RPNETForum.Classes.Forum;
using RPNETForum.Interfaces.DatabaseMethods;
using RPNETForum.Interfaces.Forum;

namespace RPNETForum.DatabaseMethods.LiteDB {
    public class PostMethods : BaseMethods, IPostMethods {
        public List<IPost> GetPosts(int threadID) {
            return _postDB.Count() == 0 ? new List<IPost>() : _postDB.Find(x => x.Thread == threadID).ToList<IPost>();
        }

        public IPost GetPost(int postID) {
            return _postDB.FindOne(x => x.Id == postID);
        }

        public bool PostExists(int postID) {
            return _postDB.Exists(x => x.Id == postID);
        }

        public void AddPost(IPost post) {
            _postDB.Insert((Post) post);
        }

        public void UpdatePost(IPost post) {
            _postDB.Update((Post) post);
        }

        public void RemovePost(int postID) {
            _postDB.Delete(x => x.Id == postID);
        }

        public int CountPosts(int threadID) {
            return _postDB.Count(x => x.Thread == threadID);
        }

        public int CountPosts() {
            return _postDB.Count();
        }
    }
}