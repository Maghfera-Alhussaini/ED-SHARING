using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_CORE.Models
{
    public class student_videos
    {

        public int Id { get; set; }
        public string title { get; set; }
        public string Duration { get; set; }
        public string Source { get; set; }
        public string image { get; set; }
        public string confirm { get; set; }
        public int Rate { get; set; }
        public int? Num { get; set; }
        public int? CourseId { get; set; }
    }
}
