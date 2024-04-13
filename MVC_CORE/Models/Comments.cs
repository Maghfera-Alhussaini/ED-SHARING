using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MVC_CORE.Models
{
    public partial class Comments
    {
        [Required]
        public int Id { get; set; }
               
        public string Contents { get; set; }
        public string username { get; set; }

        [ForeignKey("videoby")]
        public int? VideoId { get; set; }
      

        public Video video { get; set; }

      

       
      
    }
}
