using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SewingIncentives.Models;
using SewingIncentives.Entities;
using System.Data.SqlClient;

namespace SewingIncentives.Controllers
{
    public class CieControlIncentiveController
    {
        public static bool Insert(CieControlIncentiveModel model)
        {
            var @WorkHrsBefore = new SqlParameter("@WorkHrsBefore", model.WorkHrsBefore);
            var @WorkHrsAfter = new SqlParameter("@WorkHrsAfter", model.WorkHrsAfter);
            var @IncentiveBefore = new SqlParameter("@IncentiveBefore", model.IncentiveBefore);
            var @IncentiveAfter = new SqlParameter("@IncentiveAfter", model.IncentiveAfter);

            var @LineId = new SqlParameter("@LineId", model.LineId);
            var @RequestTime = new SqlParameter("@RequestTime", model.RequestTime);
            var @Reason = new SqlParameter("@Reason", model.Reason);

            SaovietCheckInEntities db = new SaovietCheckInEntities();
            if (db.ExecuteStoreCommand("EXEC spm_InsertCieControlIncentive @WorkHrsBefore, @WorkHrsAfter, @IncentiveBefore, @IncentiveAfter, @LineId, @RequestTime, @Reason", @WorkHrsBefore, @WorkHrsAfter, @IncentiveBefore, @IncentiveAfter, @LineId, @RequestTime, @Reason) >= 1)
            {
                return true;
            }
            return false;
        }
        public static bool Update(string lineId, DateTime requestTime, DateTime acceptedTime)
        {
            var @LineId = new SqlParameter("@LineId", lineId);
            var @RequestTime = new SqlParameter("@RequestTime", requestTime);
            var @AcceptedTime = new SqlParameter("@AcceptedTime", acceptedTime);

            SaovietCheckInEntities db = new SaovietCheckInEntities();
            if (db.ExecuteStoreCommand("EXEC spm_UpdateCieControlIncentive @LineId, @RequestTime,@AcceptedTime", @LineId, @RequestTime, @AcceptedTime) >= 1)
            {
                return true;
            }
            return false;
        }

        
        public static List<CieControlIncentiveModel> Select(string lineId, int month, int year)
        {
            var @LineId = new SqlParameter("@LineId", lineId);
            var @Month = new SqlParameter("@Month", month);
            var @Year = new SqlParameter("@Year", year);
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            return db.ExecuteStoreQuery<CieControlIncentiveModel>("EXEC spm_SelectCieControlIncentiveByLineIdDate @LineId, @Month, @Year", @LineId, @Month, @Year).ToList();
        }
        public static CieControlIncentiveModel Select(string lineId, DateTime requestTime)
        {
            var @LineId = new SqlParameter("@LineId", lineId);
            var @RequestTime = new SqlParameter("@RequestTime", requestTime);
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            return db.ExecuteStoreQuery<CieControlIncentiveModel>("EXEC spm_SelectCieControlIncentive @LineId,@RequestTime", @LineId, @RequestTime).FirstOrDefault();
        }
    }
}
