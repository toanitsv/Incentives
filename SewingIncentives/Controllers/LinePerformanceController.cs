using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SewingIncentives.Models;
using System.Data.SqlClient;
using SewingIncentives.Entities;
namespace SewingIncentives.Controllers
{
    class LinePerformanceController
    {
        public static LinePerformanceModel Select(string lineId, DateTime date)
        {
            var @LineId = new SqlParameter("@LineId", lineId);
            var @Date = new SqlParameter("@Date", date);
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            return db.ExecuteStoreQuery<LinePerformanceModel>("EXEC spm_SelectLinePerformanceByLineIdDate @LineId, @Date", @LineId, @Date).FirstOrDefault();
        }

        public static bool Insert(LinePerformanceModel model)
        {
            var @LineId = new SqlParameter("@LineId", model.LineId);
            var @Date = new SqlParameter("@Date", model.Date);
            var @Output = new SqlParameter("@Output", model.Output);
            var @PatternNo = new SqlParameter("@PatternNo", model.PatternNo);
            var @ArticleNo = new SqlParameter("@ArticleNo", model.ArticleNo);
            var @IncentiveGradeA = new SqlParameter("@IncentiveGradeA", model.IncentiveGradeA);
            var @ReportedWorkHour = new SqlParameter("@ReportedWorkHour", model.ReportedWorkHour);
            var @IncentiveGradeASmall = new SqlParameter("@IncentiveGradeASmall", model.IncentiveGradeASmall);
            var @ExcessIncentive = new SqlParameter("@ExcessIncentive", model.ExcessIncentive);
            var @QuotaTarget = new SqlParameter("@QuotaTarget", model.QuotaTarget);
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            if (db.ExecuteStoreCommand(@"EXEC spm_InsertLinePerformance_4 @LineId, @Date, @Output, @PatternNo, @ArticleNo, @IncentiveGradeA, @ReportedWorkHour, @IncentiveGradeASmall, @ExcessIncentive, @QuotaTarget",
                                                                          @LineId, @Date, @Output, @PatternNo, @ArticleNo, @IncentiveGradeA, @ReportedWorkHour, @IncentiveGradeASmall, @ExcessIncentive, @QuotaTarget) >= 1)
            {
                return true;
            }
            return false;
        }

        public static List<LinePerformanceModel> SelectBySection(string sectionId, int month, int year)
        {
            var @SectionId = new SqlParameter("@SectionId", sectionId);
            var @Month = new SqlParameter("@Month", month);
            var @Year = new SqlParameter("@Year", year);
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            return db.ExecuteStoreQuery<LinePerformanceModel>("EXEC spm_SelectLinePerformanceBySectionIdMonthYear @SectionId, @Month, @Year", @SectionId, @Month, @Year).ToList();
        }

        public static List<LinePerformanceModel> Select(string lineId, int month, int year)
        {
            var @LineId = new SqlParameter("@LineId", lineId);
            var @Month = new SqlParameter("@Month", month);
            var @Year = new SqlParameter("@Year", year);
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            return db.ExecuteStoreQuery<LinePerformanceModel>("EXEC spm_SelectLinePerformanceByLineIdMonthYear @LineId, @Month, @Year", @LineId, @Month, @Year).ToList();
        }
    }
}
