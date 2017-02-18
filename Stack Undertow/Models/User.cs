using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Stack_Undertow.Models
{
    public class User
    {
        public int Id { get; set; }
        public int MyPoints { get; set; }
    }
}