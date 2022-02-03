using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartialViews.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public string Name { get; set; }
       public Company Manufacturer { get; set; }
        public decimal Price { get; set; }

    }
}
