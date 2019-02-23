using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SewingIncentives.Entities;
using SewingIncentives.Models;
using System.Data.SqlClient;
namespace SewingIncentives.Controllers
{
    class LineController
    {
        public static List<LineModel> Select()
        {
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            return db.ExecuteStoreQuery<LineModel>("EXEC spm_SelectLine").ToList();
        }

        public static List<LineModel> SelectAll()
        {
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            return db.ExecuteStoreQuery<LineModel>("EXEC spm_SelectAllLine").ToList();
        }

        public static List<LineModel> Select(string sectionId)
        {
            var @SectionId = new SqlParameter("@SectionId", sectionId);
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            return db.ExecuteStoreQuery<LineModel>("EXEC spm_SelectLineBySectionId @SectionId", @SectionId).ToList();
        }
    }
}
