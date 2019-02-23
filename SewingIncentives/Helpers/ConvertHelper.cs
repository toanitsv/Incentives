using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SewingIncentives.Helpers
{
    class ConvertHelper
    {
        public static TimeSpan ConvertTime(string time)
        {
            TimeSpan result = new TimeSpan(7, 15, 0);
            if (String.IsNullOrEmpty(time) == false && time.Length == 5)
            {
                int hour = 0;
                int.TryParse(time.Substring(0, 2), out hour);
                int minute = 0;
                int.TryParse(time.Substring(3, 2), out minute);

                result = new TimeSpan(hour, minute, 0);
            }
            return result;
        }
    }
}
