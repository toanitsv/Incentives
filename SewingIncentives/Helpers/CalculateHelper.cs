using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SewingIncentives.Models;
namespace SewingIncentives.Helpers
{
    class CalculateHelper
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

        //public static double CalculateWHRS(string timeIn, string timeOut)
        //{           
        //    TimeSpan tsTimeIn = new TimeSpan(7,15,0);
        //    TimeSpan tsTimeOut = new TimeSpan(7,15,0);
        //    if (string.IsNullOrEmpty(timeIn) == false)
        //    {
        //        tsTimeIn = ConvertHelper.ConvertTime(timeIn);                
        //    }
        //    tsTimeIn = tsTimeIn.Add(new TimeSpan(0, -2, 0));
        //    if (string.IsNullOrEmpty(timeOut) == false)
        //    {
        //        tsTimeOut = ConvertHelper.ConvertTime(timeOut);
        //    }            

        //    double wHrs = 0;
        //    wHrs = (tsTimeOut - tsTimeIn).TotalHours;
        //    wHrs = Math.Truncate(wHrs);
        //    if (tsTimeIn < new TimeSpan(12, 15, 0) && tsTimeOut >= new TimeSpan(13, 15, 0))
        //    {
        //        wHrs -= 1;
        //    }
        //    if (tsTimeIn < new TimeSpan(18, 15, 0) && tsTimeOut >= new TimeSpan(18, 45, 0))
        //    {
        //        wHrs -= 0.5;
        //    }
        //    if (tsTimeOut.Minutes > 43 || tsTimeOut.Minutes < 13)
        //    {
        //        wHrs += 0.5;
        //    }

        //    return wHrs;            
        //}
        public static double CalculateWHRS(string timeIn, string timeOut)
        {
            TimeSpan tsTimeIn = new TimeSpan(7, 30, 0);
            TimeSpan tsTimeOut = new TimeSpan(7, 30, 0);
            if (string.IsNullOrEmpty(timeIn) == false)
            {
                tsTimeIn = ConvertHelper.ConvertTime(timeIn);
            }
            if (string.IsNullOrEmpty(timeOut) == false)
            {
                tsTimeOut = ConvertHelper.ConvertTime(timeOut);
            }

            double wHrs = 0;
            wHrs = (tsTimeOut - tsTimeIn).TotalHours;

            if (tsTimeIn < new TimeSpan(12, 15, 0) && tsTimeOut >= new TimeSpan(13, 15, 0))
            {
                wHrs -= 1;
            }
            if (tsTimeIn < new TimeSpan(18, 15, 0) && tsTimeOut >= new TimeSpan(18, 45, 0))
            {
                wHrs -= 0.5;
            }

            double ExceedHour = wHrs - Math.Truncate(wHrs);
            wHrs = Math.Truncate(wHrs);
            if (15.0 / 60.0 <= ExceedHour && ExceedHour < 29.0 / 60.0)
            {
                wHrs += 0.25;
            }
            if (30.0 / 60.0 <= ExceedHour && ExceedHour < 44.0 / 60.0)
            {
                wHrs += 0.5;
            }
            if (45.0 / 60.0 <= ExceedHour && ExceedHour < 59.0 / 60.0)
            {
                wHrs += 0.75;
            }

            return wHrs;
        }

    }
}
