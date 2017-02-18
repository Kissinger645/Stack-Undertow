using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Stack_Undertow.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public DateTime Created { get; set; }        

        public string OwnerId { get; set; } 

        [ForeignKey("OwnerId")]
        public virtual ApplicationUser Owner { get; set; }

        public string QuestionId { get; set; } 

        [ForeignKey("QuestionId")]
        public virtual Questions Question { get; set; }
    }
}