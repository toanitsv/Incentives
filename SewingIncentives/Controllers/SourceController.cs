using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SewingIncentives.Models;
using SewingIncentives.Entities;
using System.Data.SqlClient;
namespace SewingIncentives.Controllers
{
    class SourceController
    {
        public static List<SourceModel> Select(string departments, DateTime date)
        {
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            List<SourceModel> sourceList = new List<SourceModel>();
            foreach (string department in departments.Split(','))
            {
                var @Line = new SqlParameter("@Line", department);
                var @Date = new SqlParameter("@Date", date);
                sourceList.AddRange(db.ExecuteStoreQuery<SourceModel>("EXEC sp_c_SelectSourceByLineDate @Line, @Date", @Line, @Date).ToList());
            }
            return sourceList;
        }

        public static List<SourceModel> Select(string department, int year, int month)
        {
            SaovietCheckInEntities db = new SaovietCheckInEntities();

            var @Line = new SqlParameter("@Line", department);
            var @Year = new SqlParameter("@Year", year);
            var @Month = new SqlParameter("@Month", month);
            return db.ExecuteStoreQuery<SourceModel>("EXEC sp_c_SelectSourceByLineYearMonth  @Line, @Year, @Month", @Line, @Year, @Month).ToList();
        }
    }
}
