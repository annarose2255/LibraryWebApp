using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCommon
{
    public class DayHours
    {
        public string HourOfOperation { get; set; }
        public DateTime Date { get; set; }
        public bool IsOpen { get; set; }
        public string DayName => Date.DayOfWeek.ToString();
    }
}
