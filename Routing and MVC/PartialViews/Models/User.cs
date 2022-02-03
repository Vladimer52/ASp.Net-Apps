using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartialViews.Models
{
    public class User
    {
        public int Id { get; set; }
        [BindRequired]//обязателоьное поле
        public string Name { get; set; }
        public int Age { get; set; }
        [BindNever]//явным образом не задается
        public bool HasRight { get; set; }
    }
}
