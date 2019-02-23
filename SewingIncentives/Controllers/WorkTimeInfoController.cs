using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SewingIncentives.Models;
using SewingIncentives.Entities;
using System.Data.SqlClient;

namespace SewingIncentives.Controllers
{
    public class WorkTimeInfoController
    {
        public static WorkTimeInfoModel Select(string worktimeCode)
        {
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            var @WorkTimeInfoCode = new SqlParameter("@WorkTimeInfoCode", worktimeCode);
            return db.ExecuteStoreQuery<WorkTimeInfoModel>("EXEC spw_SelectWorkTimeInfo @WorkTimeInfoCode",@WorkTimeInfoCode).FirstOrDefault();
        }
    }
}
