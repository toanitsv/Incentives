using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SewingIncentives.Entities;
using SewingIncentives.Models;
using System.Data.SqlClient;
namespace SewingIncentives.Controllers
{
    public class WorkerLeaveController
    {
        public static List<WorkerLeaveModel> Select(string workerId)
        {
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            var @WorkerId = new SqlParameter("@WorkerId", workerId);
            return db.ExecuteStoreQuery<WorkerLeaveModel>("EXEC spm_SelectWorkerLeaveByWorkerId @WorkerId", @WorkerId).ToList();
        }

        public static List<WorkerLeaveModel> Select(int year, int month)
        {
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            var @Year = new SqlParameter("@Year", year);
            var @Month = new SqlParameter("@Month", month);
            return db.ExecuteStoreQuery<WorkerLeaveModel>("EXEC spm_SelectWorkerLeaveByYearMonth @Year, @Month", @Year, @Month).ToList();
        }

        public static List<WorkerLeaveModel> Select(int year, int leaveType, string line)
        {
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            var @Year = new SqlParameter("@Year", year);
            var @LeaveType = new SqlParameter("@LeaveType", leaveType);
            var @Line = new SqlParameter("@Line", line);
            return db.ExecuteStoreQuery<WorkerLeaveModel>("EXEC spm_SelectWorkerLeaveByYearLeaveTypeByPersonalByLine @Year,@LeaveType,@Line", @Year, @LeaveType, @Line).ToList();
        }

        public static bool Insert(WorkerLeaveModel model)
        {
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            var @WorkerId = new SqlParameter("@WorkerId", model.WorkerId);
            var @StartDate = new SqlParameter("@StartDate", model.StartDate);
            var @EndDate = new SqlParameter("@EndDate", model.EndDate);
            var @LeaveType = new SqlParameter("@LeaveType", model.LeaveType);
            if (db.ExecuteStoreCommand("EXEC spm_InsertWorkerLeave_1 @WorkerId, @StartDate, @EndDate, @LeaveType", @WorkerId, @StartDate, @EndDate, @LeaveType) > 0)
            {
                return true;
            }
            return false;
        }

        public static bool Update(WorkerLeaveModel model)
        {
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            var @WorkerLeaveId = new SqlParameter("@WorkerLeaveId", model.WorkerLeaveId);
            var @WorkerId = new SqlParameter("@WorkerId", model.WorkerId);
            var @StartDate = new SqlParameter("@StartDate", model.StartDate);
            var @EndDate = new SqlParameter("@EndDate", model.EndDate);
            var @LeaveType = new SqlParameter("@LeaveType", model.LeaveType);
            if (db.ExecuteStoreCommand("EXEC spm_UpdateWorkerLeave_1 @WorkerLeaveId, @WorkerId, @StartDate, @EndDate, @LeaveType", @WorkerLeaveId, @WorkerId, @StartDate, @EndDate, @LeaveType) > 0)
            {
                return true;
            }
            return false;
        }

        public static bool Delete(int workerLeaveId)
        {
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            var @WorkerLeaveId = new SqlParameter("@WorkerLeaveId", workerLeaveId);            
            if (db.ExecuteStoreCommand("EXEC spm_DeleteWorkerLeave @WorkerLeaveId", @WorkerLeaveId) > 0)
            {
                return true;
            }
            return false;
        }
    }
}
