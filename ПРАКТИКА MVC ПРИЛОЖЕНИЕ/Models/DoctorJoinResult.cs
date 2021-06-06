using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ПРАКТИКА_MVC_ПРИЛОЖЕНИЕ.Models
{
    public class DoctorJoinResult
    {
        public IEnumerable<Врач> doc { get; set; }
        public IEnumerable<Идентификатор_врача> docId { get; set; }
        public DoctorJoinResult()
        {
            this.Идентификатор_врача = new HashSet<Идентификатор_врача>();
        }

        public int Код_врача { get; set; }
        public int Код_учреждения_здравоохранения { get; set; }
        public string Рабочий_телефон { get; set; }
        public string Фамилия_врача { get; set; }
        public string Имя_врача { get; set; }
        public string Отчество_врача { get; set; }
        public int Код_специальности { get; set; }
        public string Категория { get; set; }
        public virtual ICollection<Идентификатор_врача> Идентификатор_врача { get; set; }

    }
}