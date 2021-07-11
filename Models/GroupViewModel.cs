using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DekanatWeb.Models
{
    public class GroupViewModel
    {
        public int GroupId { get; set; }
        public string назва { get; set; }
        public int SpecialtyId { get; set; }

        public virtual спеціальність спеціальність { get; set; }

    }
}