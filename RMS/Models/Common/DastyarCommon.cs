using Microsoft.EntityFrameworkCore;
using System.Data;

namespace RMS.Models.Common
{
    public class  DastyarCommon(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;

        public bool CheckLakeGiriIsAddingRizMetreUsers1(string ItemShomareh, string ItemsHasCondition_ConditionContextId, Guid BarAvordUserId, Guid RizMetreId)
        {
            long lngItemsHasCondition_ConditionContextId = long.Parse(ItemsHasCondition_ConditionContextId);
            bool blnCheck = false;
            var varItemsAddingToFB = _context.ItemsAddingToFBs.Where(x => x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId).ToList();
            DataTable DtItemsAddingToFB = clsConvert.ToDataTable(varItemsAddingToFB);

            for (int i = 0; i < DtItemsAddingToFB.Rows.Count; i++)
            {
                string strAddedItems = DtItemsAddingToFB.Rows[i]["AddedItems"].ToString().Trim()+DtItemsAddingToFB.Rows[i]["CharacterPlus"].ToString().Trim();
                var varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordUserId && x.Shomareh == strAddedItems).ToList();
                DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

                if (DtFBUser.Rows.Count != 0)
                {
                    Guid FBId = Guid.Parse(DtFBUser.Rows[0]["Id"].ToString());
                    DataTable DtRizMetreUsers = new DataTable();
                    Guid newGu = new Guid();
                    if (RizMetreId == newGu)
                    {
                        var varRizMetreUsers = (from RUsers in _context.RizMetreUserses
                                                join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                select new
                                                {
                                                    Shomareh = RUsers.Shomareh,
                                                    Sharh = RUsers.Sharh,
                                                    Tedad = RUsers.Tedad,
                                                    Tool = RUsers.Tool,
                                                    Arz = RUsers.Arz,
                                                    Ertefa = RUsers.Ertefa,
                                                    Vazn = RUsers.Vazn,
                                                    Des = RUsers.Des,
                                                    ForItem = RUsers.ForItem,
                                                    Type = RUsers.Type,
                                                    UseItem = RUsers.UseItem,
                                                    BarAvordUserId = fb.BarAvordId,
                                                    FBId = RUsers.FBId
                                                }).Where(x => x.FBId == FBId && x.ForItem == ItemShomareh).ToList();
                        DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);

                        if (DtRizMetreUsers.Rows.Count != 0)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        var varRizMetreCurrent = (from RUsers in _context.RizMetreUserses
                                                  join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                  select new
                                                  {
                                                      Shomareh = RUsers.Shomareh,
                                                      Sharh = RUsers.Sharh,
                                                      Tedad = RUsers.Tedad,
                                                      Tool = RUsers.Tool,
                                                      Arz = RUsers.Arz,
                                                      Ertefa = RUsers.Ertefa,
                                                      Vazn = RUsers.Vazn,
                                                      Des = RUsers.Des,
                                                      ForItem = RUsers.ForItem,
                                                      Type = RUsers.Type,
                                                      UseItem = RUsers.UseItem,
                                                      BarAvordUserId = fb.BarAvordId,
                                                      FBId = RUsers.FBId,
                                                      RizMetreId = RUsers.ID
                                                  }).Where(x => x.RizMetreId == RizMetreId).ToList();
                        DataTable DtRizMetreCurrent = clsConvert.ToDataTable(varRizMetreCurrent);


                        int Shomareh = int.Parse(DtRizMetreCurrent.Rows[0]["Shomareh"].ToString().Trim());
                        var varRizMetreUsers = (from RUsers in _context.RizMetreUserses
                                                join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                select new
                                                {
                                                    Shomareh = RUsers.Shomareh,
                                                    Sharh = RUsers.Sharh,
                                                    Tedad = RUsers.Tedad,
                                                    Tool = RUsers.Tool,
                                                    Arz = RUsers.Arz,
                                                    Ertefa = RUsers.Ertefa,
                                                    Vazn = RUsers.Vazn,
                                                    Des = RUsers.Des,
                                                    ForItem = RUsers.ForItem,
                                                    Type = RUsers.Type,
                                                    UseItem = RUsers.UseItem,
                                                    BarAvordUserId = fb.BarAvordId,
                                                    FBId = RUsers.FBId,
                                                    RizMetreId = RUsers.ID
                                                }).Where(x => x.FBId == FBId && x.ForItem == ItemShomareh && x.Shomareh == Shomareh).ToList();
                        DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);

                        if (DtRizMetreUsers.Rows.Count != 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return blnCheck;
        }

        public static int GetRelType(string RelName)
        {
            switch (RelName)
            {
                case "AndodGhir":
                    return 1;
                case "Filer":
                    return 2;
                case "BardashtMasaleh":
                    return 3;
                default:
                    return 0;
            }
        }
    }
}
