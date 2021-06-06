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
    
    public partial class Электронная_амбулаторная_карта
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Электронная_амбулаторная_карта()
        {
            this.Адрес_пациента = new HashSet<Адрес_пациента>();
            this.Осмотр_пациента = new HashSet<Осмотр_пациента>();
            this.Связь_пациента_с_представителем = new HashSet<Связь_пациента_с_представителем>();
        }
    
        public int Номер_амбулаторной_карты { get; set; }
        public Nullable<int> Код_учреждения_здравоохранения { get; set; }
        public string Фамилия { get; set; }
        public string Имя { get; set; }
        public string Отчество { get; set; }
        public Nullable<System.DateTime> Дата_рождения { get; set; }
        public string Пол { get; set; }
        public string Номер_полиса_ОМС { get; set; }
        public string Номер_СНИЛС { get; set; }
        public string Серия_и_номер_паспорта { get; set; }
        public string Серия_и_номер_свидетельства_о_рождении_пациента { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Адрес_пациента> Адрес_пациента { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Осмотр_пациента> Осмотр_пациента { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Связь_пациента_с_представителем> Связь_пациента_с_представителем { get; set; }
        public virtual Учреждение_здравоохранения Учреждение_здравоохранения { get; set; }
    }
}
