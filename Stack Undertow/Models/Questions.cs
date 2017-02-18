using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Stack_Undertow.Models
{
    public class Questions
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Question { get; set; }
        public DateTime Created { get; set; }
        public string Poster { get; set; }

        public string QuestionerId { get; set; } //ApplicationUserId

        [ForeignKey("QuestionerId")]
        public virtual ApplicationUser QuestId { get; set; }
    }
}