using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SewingIncentives.Entities;
using System.Data.SqlClient;
namespace SewingIncentives.Controllers
{
    class OtherController
    {
        public static bool ChangeCardId(string workerId, string cardId)
        {
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            var @WorkerId = new SqlParameter("@WorkerId", workerId);
            var @CardId = new SqlParameter("@CardId", cardId);

            if (db.ExecuteStoreCommand("spm_UpdateCardIdByWorkerId @WorkerId, @CardId", @WorkerId, @CardId) > 0)
            {
                return true;
            }
            return false;
        }

        public static bool ChangeWorkerId(string workerId, string newWorkerId)
        {
            SaovietCheckInEntities db = new SaovietCheckInEntities();
            var @WorkerId = new SqlParameter("@WorkerId", workerId);
            var @NewWorkerId = new SqlParameter("@NewWorkerId", newWorkerId);

            if (db.ExecuteStoreCommand("spm_UpdateWorkerId @WorkerId, @NewWorkerId", @WorkerId, @NewWorkerId) > 0)
            {
                return true;
            }
            return false;
        }
    }
}
