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
    using System.ComponentModel.DataAnnotations;

    public partial class Логин_и_пароль
    {
        public int Код { get; set; }
        [Required(ErrorMessage ="Заполните поле!")]
        public string Логин { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Заполните поле!")]
        public string Пароль { get; set; }
        public string ErrorMsg { get; set; }
    }
}
