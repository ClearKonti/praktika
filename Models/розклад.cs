//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DekanatWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class розклад
    {
        [Display(Name = "Номер розкладу")]
        public int CurriculumId { get; set; }

        [StringLength(1)]
        [Display(Name = "Курс")]
        public int курс { get; set; }


        [StringLength(1)]
        [Display(Name = "Семестр")]
        public int семестр { get; set; }

        [StringLength(3)]

        [Display(Name = "Лекційні години")]
        public int лекційні_часи { get; set; }

        [StringLength(3)]

        [Display(Name = "Практичні години")]
        public int практичні_часи { get; set; }

        [StringLength(3)]

        [Display(Name = "Лабораторні години")]
        public int лабораторні_часи { get; set; }


        [StringLength(10)]
        [Display(Name = "Форма захисту")]
        public string форма_захисту { get; set; }

        [StringLength(1)]
        [Display(Name = "Курсова робота")]
        public string курсова_робота { get; set; }

        [StringLength(3)]
        [Display(Name = "Курсові години")]
        public int курсові_часи { get; set; }

        [Display(Name = "Номер спеціальності")]
        public int SpecialtyId { get; set; }

        [Display(Name = "Номер предмету")]
        public int SubjectId { get; set; }
    
        public virtual предмет предмет { get; set; }
        public virtual спеціальність спеціальність { get; set; }
    }
}
