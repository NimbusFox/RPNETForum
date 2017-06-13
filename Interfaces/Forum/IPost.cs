using System;

namespace RPNETForum.Interfaces.Forum {
    public interface IPost {
        int Id { get; set; }
        int Thread { get; set; }
        int Poster { get; set; }
        DateTime PostedAt { get; set; }
        string Message { get; set; }
        bool Edited { get; set; }
        DateTime EditedAt { get; set; }
    }
}