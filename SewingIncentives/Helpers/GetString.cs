using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SewingIncentives.Helpers
{
    public class GetString
    {
        public static string GetPatternNoFromFilePath(string fileName)
        {
            string result = "";
            string reserve = "";
            for (int i = fileName.Length - 1; i >= 0; i--)
            {
                if (fileName[i].ToString() == @"\")
                {
                    break;
                }
                reserve += fileName[i];
            }
            string[] tempResultArr = reserve.Split(' ');
            string tempResult = tempResultArr[tempResultArr.Count() - 1].ToString();
            for (int i = tempResult.Length - 1; i >= 0; i--)
            {
                result += tempResult[i].ToString();
            }
            return result;
        }
        public static string GetShoeNameFromFilePath(string fileName)
        {
            string result = "";
            string reserve = "";
            for (int i = fileName.Length - 1; i >= 0; i--)
            {
                if (fileName[i].ToString() == @"\")
                {
                    break;
                }
                reserve += fileName[i];
            }
            string[] tempResultArr = reserve.Split(' ');
            string tempResult = "";
            for (int i = 0; i < tempResultArr.Count() - 1; i++)
            {
                tempResult += tempResultArr[i].ToString() + " ";
            }
            string temResult1 = tempResult.Trim().Split('.')[1].ToString();
            for (int i = temResult1.Length - 1; i >= 0; i--)
            {
                result += temResult1[i].ToString();
            }
            return result;
        }
    }
}
