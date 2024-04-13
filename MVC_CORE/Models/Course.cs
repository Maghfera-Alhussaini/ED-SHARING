using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_CORE.Models
{
    public partial class Course
    {
        public int Id { get; set; }
        public int Number { get; set; }

        public string Title { get; set; }
        public string Des { get; set; }
        public int Rate { get; set; }
        public string image { get; set; }
        public string teacher { get; set; }
        [ForeignKey("topicby")]
        public int topic_id { get; set; }
        public topic Topic { get; set; }
        public ICollection<Video> Video { get; set; }
        public ICollection<Questions> questions { get; set; }
    }
}
