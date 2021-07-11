using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DekanatWeb.Models
{
    public class ScheduleViewModel
    {

        public int CurriculumId { get; set; }
        public int курс { get; set; }
        public int семестр { get; set; }
        public int лекційні_часи { get; set; }
        public int практичні_часи { get; set; }
        public int лабораторні_часи { get; set; }
        public string форма_захисту { get; set; }
        public string курсова_робота { get; set; }
        public int курсові_часи { get; set; }
        public int SpecialtyId { get; set; }
        public int SubjectId { get; set; }

        public virtual предмет предмет { get; set; }
        public virtual спеціальність спеціальність { get; set; }
    }
}