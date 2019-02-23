using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SewingCheckIn.Models;
using System.Data.SqlClient;
namespace SewingCheckIn.Controllers
{
    class PersonalController
    {
        public static List<PersonalModel> Get(string department)
        {
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            var @Department = new SqlParameter("@Department", department);
            return db.ExecuteStoreQuery<PersonalModel>("EXEC sp_c_SelectPersonalByDepartment @Department", @Department).ToList();
        }
    }
}
