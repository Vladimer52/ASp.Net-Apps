using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartialViews.Services
{
    public class SimpleTimeService : ITimeService
    {
        public string GetTime()
        {
            return System.DateTime.Now.ToString("hh:mm:ss");
        }
    }
}
