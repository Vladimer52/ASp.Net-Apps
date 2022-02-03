using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Введите имя пользователя")]
        public string Name { get; set; }

        [Range(1, 100, ErrorMessage ="Возраст должен быть в промежутке от 1 до 100")]
        [Required(ErrorMessage ="Укажите возраст пользователя")]
        public int Age { get; set; }
    }
}
