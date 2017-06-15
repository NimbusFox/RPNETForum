using System.Collections.Generic;
using RPNETForum.Interfaces.Forum;

namespace RPNETForum.Interfaces.DatabaseMethods {
    public interface IThreadMethods {
        List<IThread> GetThreads(int forumID);
        IThread GetThread(int threadID);
        void AddThread(IThread thread);
        void RemoveThread(int threadID);
        void UpdateThread(IThread thread);
        bool ThreadExists(int id);
        int CountThreads();
        int CountThreads(int forumID);
    }
}