using System.Collections.Generic;
using RPNETForum.Interfaces.DatabaseMethods;
using RPNETForum.Interfaces.Forum;

namespace RPNETForum.DatabaseMethods.Sqlite {
    public class ThreadMethods : BaseMethods, IThreadMethods {
        public List<IThread> GetThreads(int forumID) {
            throw new System.NotImplementedException();
        }

        public IThread GetThread(int threadID) {
            throw new System.NotImplementedException();
        }

        public void AddThread(IThread thread) {
            throw new System.NotImplementedException();
        }

        public void RemoveThread(int threadID) {
            throw new System.NotImplementedException();
        }

        public void UpdateThread(IThread thread) {
            throw new System.NotImplementedException();
        }

        public bool ThreadExists(int id) {
            throw new System.NotImplementedException();
        }

        public int CountThreads() {
            throw new System.NotImplementedException();
        }

        public int CountThreads(int forumID) {
            throw new System.NotImplementedException();
        }
    }
}