using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SewingIncentives.Models;
using SewingIncentives.Entities;
using System.Data.SqlClient;

namespace SewingIncentives.Controllers
{
    public class WorkerShiftController
    {
        public static WorkerShiftModel Select(string workerId, int year, int month)
        {
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            var @WorkerId = new SqlParameter("@WorkerId", workerId);
            var @Year = new SqlParameter("@Year", year);
            var @Month = new SqlParameter("@Month", month);
            return db.ExecuteStoreQuery<WorkerShiftModel>("EXEC spw_SelectWorkerShift @WorkerId, @Year, @Month", @WorkerId, @Year, @Month).FirstOrDefault();
        }
    }
}
