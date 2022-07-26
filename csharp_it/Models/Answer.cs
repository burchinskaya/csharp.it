﻿namespace csharp_it.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public virtual Question? Question { get; set; }
        public string Text { get; set; }
        public bool RightAnswer { get; set; }
    }
}

