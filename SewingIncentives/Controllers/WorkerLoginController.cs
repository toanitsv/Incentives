using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SewingIncentives.Models;
using SewingIncentives.Entities;
using System.Data.SqlClient;
namespace SewingIncentives.Controllers
{
    class WorkerLoginController
    {

        public static List<WorkerLoginModel> Select(DateTime createdTime)
        {            
            var @CreatedTime = new SqlParameter("@CreatedTime", createdTime);
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            return db.ExecuteStoreQuery<WorkerLoginModel>("EXEC spm_SelectWorkerLoginByCreatedTime @CreatedTime", @CreatedTime).ToList();
        }

        public static List<WorkerLoginModel> Select(string lineId, DateTime createdTime)
        {
            var @LineId = new SqlParameter("@LineId", lineId);
            var @CreatedTime = new SqlParameter("@CreatedTime", createdTime);
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            return db.ExecuteStoreQuery<WorkerLoginModel>("EXEC spm_SelectWorkerLoginByLineIdCreatedTime @LineId, @CreatedTime", @LineId, @CreatedTime).ToList();
        }

        public static List<WorkerLoginModel> Select(int year, int month)
        {
            //var @LineId = new SqlParameter("@LineId", lineId);
            var @Year = new SqlParameter("@Year", year);
            var @Month = new SqlParameter("@Month", month);
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            return db.ExecuteStoreQuery<WorkerLoginModel>("EXEC spm_SelectWorkerLoginByYearMonth @Year, @Month", @Year, @Month).ToList();
        }

        public static WorkerLoginModel SelectTop1(string cardId, DateTime createdTime)
        {
            var @CardId = new SqlParameter("@CardId", cardId);
            var @CreatedTime = new SqlParameter("@CreatedTime", createdTime);
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            return db.ExecuteStoreQuery<WorkerLoginModel>("EXEC spm_SelectWorkerLoginByCardIdDate @CardId, @CreatedTime", @CardId, @CreatedTime).First();
        }

        public static List<WorkerLoginModel> SelectByLine(string sectionId, DateTime createdTime)
        {
            var @CreatedTime = new SqlParameter("@CreatedTime", createdTime);
            var @SectionId = new SqlParameter("@SectionId", sectionId);
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            return db.ExecuteStoreQuery<WorkerLoginModel>("EXEC spm_SelectWorkerLoginByCreatedTimeLineBySectionId @CreatedTime, @SectionId", @CreatedTime, @SectionId).ToList();
        }

        public static bool Insert(WorkerLoginModel model)
        {
            var @CardId = new SqlParameter("@CardId", model.CardId);
            var @WorkerId = new SqlParameter("@WorkerId", model.WorkerId);            
            var @LineId = new SqlParameter("@LineId", model.LineId);           
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            if (db.ExecuteStoreCommand("EXEC spm_InsertWorkerLogin @CardId,@WorkerId,@LineId", @CardId, @WorkerId, @LineId) >= 1)
            {
                return true;
            }
            return false;
        }
    }
}
