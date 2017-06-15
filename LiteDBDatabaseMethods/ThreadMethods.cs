using System.Collections.Generic;
using System.Linq;
using RPNETForum.Classes.Forum;
using RPNETForum.Interfaces.DatabaseMethods;
using RPNETForum.Interfaces.Forum;

namespace RPNETForum.DatabaseMethods.LiteDB {
    public class ThreadMethods : BaseMethods, IThreadMethods {
        public List<IThread> GetThreads(int forumID) {
            return _threadDB.Count() == 0 ? new List<IThread>() : _threadDB.Find(x => x.Forum == forumID).ToList<IThread>();
        }

        public IThread GetThread(int threadID) {
            return _threadDB.FindOne(x => x.Id == threadID);
        }

        public void AddThread(IThread thread) {
            _threadDB.Insert((Thread) thread);
        }

        public void RemoveThread(int threadID) {
            if (ThreadExists(threadID)) {
                _threadDB.Delete(x => x.Id == threadID);
            }
        }

        public void UpdateThread(IThread thread) {
            _threadDB.Update((Thread) thread);
        }

        public bool ThreadExists(int id) {
            return _threadDB.Exists(x => x.Id == id);
        }

        public int CountThreads() {
            return _threadDB.Count();
        }

        public int CountThreads(int forumID) {
            return _threadDB.Count(x => x.Forum == forumID);
        }
    }
}