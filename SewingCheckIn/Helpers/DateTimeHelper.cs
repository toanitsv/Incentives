using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SewingCheckIn.Helpers
{
    class DateTimeHelper
    {
        public static double CalculateMonth(DateTime dt1, DateTime dt2)
        {
            double result = 0;
            result = (dt2.Year - dt1.Year) * 12 + (dt2.Month - dt1.Month);
            if (dt2.Day < dt1.Day)
            {
                result = result - 0.5;
            }
            else if (dt2.Day == dt1.Day)
            {
                result = result + 0;
            }
            else if (dt2.Day > dt1.Day)
            {
                result = result + 0.5;
            }
            return result;
        }
    }
}
