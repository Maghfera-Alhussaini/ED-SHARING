using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_CORE.Models
{
    public class topic
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public ICollection<Course> Courses { get; set; }


    }
}
