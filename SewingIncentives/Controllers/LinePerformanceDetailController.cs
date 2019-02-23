using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SewingIncentives.Models;
using System.Data.SqlClient;
using SewingIncentives.Entities;
namespace SewingIncentives.Controllers
{
    class LinePerformanceDetailController
    {
        public static List<LinePerformanceDetailModel> Select(string lineId, DateTime date)
        {
            var @LineId = new SqlParameter("@LineId", lineId);
            var @Date = new SqlParameter("@Date", date);
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            return db.ExecuteStoreQuery<LinePerformanceDetailModel>("EXEC spm_SelectLinePerformanceDetailByLineIdDate @LineId, @Date", @LineId, @Date).ToList();
        }

        public static List<LinePerformanceDetailModel> SelectByPersonal(string line, int year, int month)
        {
            var @Line = new SqlParameter("@Line", line);
            var @Year = new SqlParameter("@Year", year);
            var @Month = new SqlParameter("@Month", month);
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            db.CommandTimeout = 180;
            return db.ExecuteStoreQuery<LinePerformanceDetailModel>("EXEC spm_SelectLinePerformanceDetailByYearMonthByPersonalByLine @Year, @Month, @Line", @Year, @Month, @Line).ToList();
        }

        public static LinePerformanceDetailModel SelectTop1(string cardId, DateTime date)
        {
            var @CardId = new SqlParameter("@CardId", cardId);
            var @Date = new SqlParameter("@Date", date);
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            return db.ExecuteStoreQuery<LinePerformanceDetailModel>("EXEC spm_SelectLinePerformanceDetailByCardIdDate @CardId, @Date", @CardId, @Date).FirstOrDefault();
        }

        public static List<LinePerformanceDetailModel> SelectBySection(string sectionId, int month, int year)
        {
            var @SectionId = new SqlParameter("@SectionId", sectionId);
            var @Month = new SqlParameter("@Month", month);
            var @Year = new SqlParameter("@Year", year);
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            return db.ExecuteStoreQuery<LinePerformanceDetailModel>("EXEC spm_SelectLinePerformanceDetailBySectionIdMonthYear @SectionId, @Month, @Year", @SectionId, @Month, @Year).ToList();
        }

        public static List<LinePerformanceDetailModel> SelectByWorkerLogin(string lineId, DateTime date)
        {
            var @LineId = new SqlParameter("@LineId", lineId);
            var @CreatedTime = new SqlParameter("@CreatedTime", date);
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            return db.ExecuteStoreQuery<LinePerformanceDetailModel>("EXEC spm_SelectLinePerformanceDetailByWorkerLoginByLineIdCreatedTime @LineId, @CreatedTime", @LineId, @CreatedTime).ToList();
        }

        public static List<LinePerformanceDetailModel> SelectBySection(string sectionId, DateTime date)
        {
            var @SectionId = new SqlParameter("@SectionId", sectionId);
            var @Date = new SqlParameter("@Date", date);
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            return db.ExecuteStoreQuery<LinePerformanceDetailModel>("EXEC spm_SelectLinePerformanceDetailByDateLineBySectionId @Date, @SectionId", @Date, @SectionId).ToList();
        }        

        public static bool Insert(LinePerformanceDetailModel model)
        {
            var @LineId = new SqlParameter("@LineId", model.LineId);
            var @Date = new SqlParameter("@Date", model.Date);
            var @CardId = new SqlParameter("@CardId", model.CardId);
            var @WorkerId = new SqlParameter("@WorkerId", model.WorkerId);
            var @OthersPosition = new SqlParameter("@OthersPosition", model.OthersPosition);
            var @TimeOut = new SqlParameter("@TimeOut", model.TimeOut);
            var @IncentiveGrade = new SqlParameter("@IncentiveGrade", model.IncentiveGrade);
            var @Incentive = new SqlParameter("@Incentive", model.Incentive);
            var @SpecsIncentive = new SqlParameter("@SpecsIncentive", model.SpecsIncentive);
            var @ExcessIncentive = new SqlParameter("@ExcessIncentive", model.ExcessIncentive);
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            if (db.ExecuteStoreCommand("EXEC spm_InsertLinePerformanceDetail_1 @LineId,@Date,@CardId,@WorkerId,@OthersPosition,@TimeOut,@IncentiveGrade,@Incentive,@SpecsIncentive,@ExcessIncentive", @LineId, @Date, @CardId, @WorkerId, @OthersPosition, @TimeOut, @IncentiveGrade, @Incentive, @SpecsIncentive, @ExcessIncentive) >= 1)
            {
                return true;
            }
            return false;
        }

        public static bool Transfer(string lineId, DateTime date, string cardId, string workerId, string lineIdNew)
        {
            var @LineId = new SqlParameter("@LineId", lineId);
            var @Date = new SqlParameter("@Date", date);
            var @CardId = new SqlParameter("@CardId", cardId);
            var @WorkerId = new SqlParameter("@WorkerId", workerId);
            var @LineIdNew = new SqlParameter("@LineIdNew", lineIdNew);
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            if (db.ExecuteStoreCommand("EXEC spm_TransferLineId @LineId,@Date,@CardId,@WorkerId,@LineIdNew", @LineId, @Date, @CardId, @WorkerId, @LineIdNew) >= 1)
            {
                return true;
            }
            return false;
        }
    }
}
