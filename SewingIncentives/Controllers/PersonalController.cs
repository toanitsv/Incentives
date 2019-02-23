using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SewingIncentives.Models;
using SewingIncentives.Entities;
using System.Data.SqlClient;
namespace SewingIncentives.Controllers
{
    class PersonalController
    {
        public static List<PersonalModel> Select()
        {
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            return db.ExecuteStoreQuery<PersonalModel>("EXEC sp_c_SelectPersonal").ToList();
        }

        public static List<PersonalModel> Select(string departments)
        {
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            List<PersonalModel> personalList = new List<PersonalModel>();
            foreach (string department in departments.Split(','))
            {
                var @Department = new SqlParameter("@Department", department);
                personalList.AddRange(db.ExecuteStoreQuery<PersonalModel>("EXEC sp_c_SelectPersonalByDepartment @Department", @Department).ToList());
            }
            return personalList;
        }
    }
}
