using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Stack_Undertow.Models
{
    public class Point
    {
        public int Id { get; set; }
        public int Points { get; set; }
        public string Reason { get; set; }
        public DateTime Created { get; set; }

        public string PointId { get; set; } //ApplicationUserId

        [ForeignKey("PointId")]
        public virtual ApplicationUser PointUser { get; set; }
    }
}