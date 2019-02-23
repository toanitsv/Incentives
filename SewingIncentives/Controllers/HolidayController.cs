using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SewingIncentives.Models;
using SewingIncentives.Entities;
using System.Data.SqlClient;

namespace SewingIncentives.Controllers
{
    public class HolidayController
    {
        public static List<HolidayModel> Select(int year, int month)
        {
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            var @Year = new SqlParameter("@Year", year);
            var @Month = new SqlParameter("@Month", month);
            return db.ExecuteStoreQuery<HolidayModel>("EXEC spm_SelectHolidayByYearMonth @Year, @Month", @Year, @Month).ToList();
        }
    }
}
