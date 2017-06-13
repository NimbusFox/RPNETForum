using System;
using RPNETForum.Interfaces.Forum;

namespace RPNETForum.Classes.Forum {
    public class Post : IPost {
        public int Id { get; set; }
        public int Thread { get; set; }
        public int Poster { get; set; }
        public DateTime PostedAt { get; set; }
        public string Message { get; set; }
        public bool Edited { get; set; }
        public DateTime EditedAt { get; set; }
    }
}