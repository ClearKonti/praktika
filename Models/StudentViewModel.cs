using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DekanatWeb.Models
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }
        public string імя { get; set; }
        public string прізвище { get; set; }
        public string телефон { get; set; }
        public int GroupId { get; set; }
        public Nullable<System.DateTime> дата_народження { get; set; }
        public virtual група група { get; set; }

    }
}