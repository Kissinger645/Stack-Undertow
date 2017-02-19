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
        public string AnswerText { get; set; }
        public bool Best { get; set; }

        public string Answerer { get; set; } 

        [ForeignKey("Answerer")]
        public virtual ApplicationUser AnswerUser { get; set; }

        public int QId { get; set; } 

        [ForeignKey("QId")]
        public virtual Questions QuestionId { get; set; }
    }
}