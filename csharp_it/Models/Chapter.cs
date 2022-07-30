﻿using System.Collections.Generic;

namespace csharp_it.Models
{
    public class Chapter
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int SecondsNum { get; set; }

        public List<Lesson> Lessons { get; set; }

        public Chapter()
        {
            SecondsNum = 0;

            Lessons = new List<Lesson>();
        }
    }
}

