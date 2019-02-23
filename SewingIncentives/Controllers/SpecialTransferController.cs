using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SewingIncentives.Entities;
using SewingIncentives.Models;
using System.Data.SqlClient;
namespace SewingIncentives.Controllers
{
    public class SpecialTransferController
    {
        public static List<SpecialTransferModel> Select(string workerId)
        {
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            var @WorkerId = new SqlParameter("@WorkerId", workerId);
            return db.ExecuteStoreQuery<SpecialTransferModel>("EXEC spm_SelectSpecialTransferByWorkerId @WorkerId", @WorkerId).ToList();
        }

        public static List<SpecialTransferModel> Select(string lineId, DateTime date)
        {
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            var @DestinationLineId = new SqlParameter("@DestinationLineId", lineId);
            var @Date = new SqlParameter("@Date", date);
            return db.ExecuteStoreQuery<SpecialTransferModel>("EXEC spm_SelectWorkerLeaveByLineIdDate @DestinationLineId, @Date", @DestinationLineId, @Date).ToList();
        }

        //public static List<WorkerLeaveModel> Select(int year, int month)
        //{
        //    SaovietCheckInEntities db = new SaovietCheckInEntities();
        //    var @Year = new SqlParameter("@Year", year);
        //    var @Month = new SqlParameter("@Month", month);
        //    return db.ExecuteStoreQuery<WorkerLeaveModel>("EXEC spm_SelectWorkerLeaveByYearMonth @Year, @Month", @Year, @Month).ToList();
        //}

        public static bool Insert(List<SpecialTransferModel> modelList)
        {
            bool result = true;
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            foreach (SpecialTransferModel model in modelList)
            {
                var @WorkerId = new SqlParameter("@WorkerId", model.WorkerId);
                var @TransferType = new SqlParameter("@TransferType", model.TransferType);
                var @DestinationLineId = new SqlParameter("@DestinationLineId", model.DestinationLineId);
                var @Date = new SqlParameter("@Date", model.Date);
                var @Time = new SqlParameter("@Time", model.Time);
                if (db.ExecuteStoreCommand("EXEC spm_InsertSpecialTransfer @WorkerId, @TransferType, @DestinationLineId, @Date, @Time", @WorkerId, @TransferType, @DestinationLineId, @Date, @Time) <= 0)
                {
                    result = false;
                }                
            }
            return result;
        }

        //public static bool Update(WorkerLeaveModel model)
        //{
        //    SaovietCheckInEntities db = new SaovietCheckInEntities();
        //    var @WorkerLeaveId = new SqlParameter("@WorkerLeaveId", model.WorkerLeaveId);
        //    var @WorkerId = new SqlParameter("@WorkerId", model.WorkerId);
        //    var @StartDate = new SqlParameter("@StartDate", model.StartDate);
        //    var @EndDate = new SqlParameter("@EndDate", model.EndDate);
        //    var @LeaveType = new SqlParameter("@LeaveType", model.LeaveType);
        //    if (db.ExecuteStoreCommand("EXEC spm_UpdateWorkerLeave_1 @WorkerLeaveId, @WorkerId, @StartDate, @EndDate, @LeaveType", @WorkerLeaveId, @WorkerId, @StartDate, @EndDate, @LeaveType) > 0)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        public static bool Delete(int id)
        {
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            var @SpecialTransferId = new SqlParameter("@SpecialTransferId", id);
            if (db.ExecuteStoreCommand("EXEC spm_DeleteSpecialTransfer @SpecialTransferId", @SpecialTransferId) > 0)
            {
                return true;
            }
            return false;
        }
    }
}
