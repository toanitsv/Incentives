using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SewingCheckIn.Models;
using System.Data.SqlClient;

namespace SewingCheckIn.Controllers
{
    class SourceController
    {
        public static List<SourceModel> Get(string line, DateTime date)
        {
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            var @Line = new SqlParameter("@Line", line);
            var @Date = new SqlParameter("@Date", date);
            return db.ExecuteStoreQuery<SourceModel>("EXEC sp_c_SelectSourceByLineDate @Line, @Date", @Line, @Date).ToList();
        }
    }
}
