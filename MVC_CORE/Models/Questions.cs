using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_CORE.Models
{
    public class Questions
    {

        public int Id { get; set; }
        public string Questions_text { get; set; }
        public string Questions_ans1 { get; set; }
        public string Questions_ans2 { get; set; }
        public string Questions_ans3 { get; set; }

        public string Questions_anstrue { get; set; }

        [ForeignKey("courseFK")]

        public int? CourseId { get; set; }

        public Course Course { get; set; }

    }
}
