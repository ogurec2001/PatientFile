//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ПРАКТИКА_MVC_ПРИЛОЖЕНИЕ.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public partial class Родственная_связь
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Родственная_связь()
        {
            this.Связь_пациента_с_представителем = new HashSet<Связь_пациента_с_представителем>();
        }
    
        public int Код_родства__свойства { get; set; }
        public string Название_родственной_связи { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Связь_пациента_с_представителем> Связь_пациента_с_представителем { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }

    }
}
