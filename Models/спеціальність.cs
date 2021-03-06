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

    public partial class спеціальність
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public спеціальність()
        {
            this.група = new HashSet<група>();
            this.розклад = new HashSet<розклад>();
        }

        [StringLength(3)]
        [Display(Name = "Номер спеціальності")]
        public int SpecialtyId { get; set; }

        [StringLength(50)]
        [Display(Name = "Назва спеціальності")]
        public string назва { get; set; }

        [Display(Name = "Номер факультету")]
        public int FacultyId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<група> група { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<розклад> розклад { get; set; }
        public virtual факультет факультет { get; set; }
    }
}
