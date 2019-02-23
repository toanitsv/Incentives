using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SewingIncentives.Models;
using SewingIncentives.Entities;
namespace SewingIncentives.Controllers
{
    class SectionController
    {
        public static List<SectionModel> Select()
        {
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            return db.ExecuteStoreQuery<SectionModel>("EXEC spm_SelectSection").ToList();
        }
    }
}
