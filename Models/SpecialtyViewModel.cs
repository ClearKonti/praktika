using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DekanatWeb.Models
{
    public class SpecialtyViewModel
    {
        public int SpecialtyId { get; set; }
        public string назва { get; set; }
        public int FacultyId { get; set; }

        public virtual факультет факультет { get; set; }

    }
}