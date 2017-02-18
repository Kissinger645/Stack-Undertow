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

        public string PointName { get; set; } //ApplicationUserId

        [ForeignKey("PointName")]
        public virtual ApplicationUser PointId { get; set; }
    }
}