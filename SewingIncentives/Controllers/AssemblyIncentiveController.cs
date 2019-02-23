using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SewingIncentives.Models;
using SewingIncentives.Entities;
using System.Data.SqlClient;
namespace SewingIncentives.Controllers
{
    public class AssemblyIncentiveController
    {
        public static bool Insert(AssemblyIncentiveModel model)
        {
            var @PatternNo = new SqlParameter("@PatternNo", model.PatternNo);
            var @ShoeName = new SqlParameter("@ShoeName", model.ShoeName);
            var @Efficiency = new SqlParameter("@Efficiency", model.Efficiency);
            var @AssemblyOutput = new SqlParameter("@AssemblyOutput", model.AssemblyOutput);
            var @Incentive = new SqlParameter("@Incentive", model.Incentive);
            var @Worker = new SqlParameter("@Worker", model.Worker);

            SaovietCheckInEntities db = new SaovietCheckInEntities();
            if (db.ExecuteStoreCommand("EXEC spm_InsertAssemblyIncentive @PatternNo, @ShoeName, @Efficiency, @AssemblyOutput, @Incentive,@Worker", @PatternNo, @ShoeName, @Efficiency, @AssemblyOutput, @Incentive, @Worker) >= 1)
            {
                return true;
            }
            return false;
        }
        public static bool Update(AssemblyIncentiveModel model)
        {
            var @Id = new SqlParameter("@Id", model.Id);
            var @PatternNo = new SqlParameter("@PatternNo", model.PatternNo);
            var @ShoeName = new SqlParameter("@ShoeName", model.ShoeName);
            var @Efficiency = new SqlParameter("@Efficiency", model.Efficiency);
            var @AssemblyOutput = new SqlParameter("@AssemblyOutput", model.AssemblyOutput);
            var @Incentive = new SqlParameter("@Incentive", model.Incentive);

            SaovietCheckInEntities db = new SaovietCheckInEntities();
            if (db.ExecuteStoreCommand("EXEC spm_UpdateAssemblyIncentive @Id, @PatternNo, @ShoeName, @Efficiency, @AssemblyOutput, @Incentive", @Id, @PatternNo, @ShoeName, @Efficiency, @AssemblyOutput, @Incentive) >= 1)
            {
                return true;
            }
            return false;
        }
        public static bool Delete(string patternNo)
        {
            var @PatternNo = new SqlParameter("@PatternNo", patternNo);
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            if (db.ExecuteStoreCommand("EXEC spm_DeleteAssemblyIncentive @PatternNo", @PatternNo) >= 1)
            {
                return true;
            }
            return false;
        }
        public static List<AssemblyIncentiveModel> Select(string PM)
        {
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            var @PatternNo = new SqlParameter("@PatternNo", PM);
            return db.ExecuteStoreQuery<AssemblyIncentiveModel>("EXEC spm_SelectAssemblyIncentive @PatternNo", @PatternNo).ToList();
        }
    }
}
