using mvc1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc1
{
    public class SampleData
    {
        public static void Initialize(MobileContext context)
        {
            if (!context.Phones.Any()) //если данные в таблицы Phones отсутствуют
            {
                context.Phones.AddRange(
                    new Phone
                    {
                    Name = "iPhone X",
                    Company = "Apple",
                    Price = 600
                    },
                    new Phone
                    {
                        Name = "Samsung Galaxy Edge",
                        Company = "Samsung",
                        Price = 550
                    },
                    new Phone
                    {
                        Name = "Pixel 3",
                        Company = "Google",
                        Price = 500
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
