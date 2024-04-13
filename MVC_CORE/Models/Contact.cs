using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_CORE.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string user_name { get; set; }

        public string email { get; set; }
        public string subject { get; set; }
        public string message { get; set; }

    }
}
