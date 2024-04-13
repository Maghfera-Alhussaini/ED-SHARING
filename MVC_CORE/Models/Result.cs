using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_CORE.Models
{
    public class Result
    {
        public int Id { get; set; }

        public string studen_name { get; set; }

        public string exam_result { get; set; }

        [ForeignKey("courseby")]

        public int? CourseId { get; set; }

        public Course Course { get; set; }
    }
}
