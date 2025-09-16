using RMS.Models.Common;
using RMS.Models.Entity;
using System.Data;
using System.Reflection.Metadata;

namespace RMS.Controllers.AmalyateKhaki.Dto
{
    public class AmalyatKhakBardariFuncs(ApplicationDbContext context)
    {
        ApplicationDbContext _context = context;
        public bool SaveRizMetreKhakBardari(Guid BarAvordUserId, Guid KMId, int KMNum, string KMS, string KME,
                decimal Hajm1, decimal Hajm2, decimal Hajm3, decimal Hajm4, decimal Hajm5, decimal Hajm6, decimal Hajm7, decimal HajmKol)
        {
            clsFB? FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordUserId && x.Shomareh == "030102");
            Guid gFBId = new Guid();
            if (FBUser == null)
            {
                clsFB FB = new clsFB();
                FB.BarAvordId = BarAvordUserId;
                FB.Shomareh = "030102";
                FB.BahayeVahedZarib = 0;
                _context.FBs.Add(FB);
                _context.SaveChanges();
                gFBId = FB.ID;
            }
            else
                gFBId = FBUser.ID;

            ////////////////
            long Shomareh = 1;
            clsRizMetreUsers? varDt = _context.RizMetreUserses.OrderByDescending(x=>x.Shomareh).FirstOrDefault(x => x.FBId == gFBId);
            if (varDt != null)
            {
                Shomareh = varDt.Shomareh + 1;
            }


            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
            if (Hajm1 != 0)
            {

                RizMetre.Shomareh = Shomareh;
                RizMetre.Sharh = "لجن برداری مکانیکی";
                RizMetre.Tedad = Hajm1 / HajmKol;
                RizMetre.Tool = 0;
                RizMetre.Arz = 0;
                RizMetre.Ertefa = 0;
                RizMetre.Vazn = HajmKol;
                RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                RizMetre.FBId = gFBId;
                RizMetre.OperationsOfHamlId = 0;
                RizMetre.Type = "400" + KMNum.ToString("D3") + "00";///لجن برداری مکانیکی
                RizMetre.ForItem = "";
                RizMetre.UseItem = "";
                _context.RizMetreUserses.Add(RizMetre);
                _context.SaveChanges();
                //RizMetre.Save();
            }
            /////////////////
            FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordUserId && x.Shomareh == "030103");

            gFBId = new Guid();
            if (FBUser==null)
            {
                clsFB FB = new clsFB();
                FB.BarAvordId = BarAvordUserId;
                FB.Shomareh = "030103";
                FB.BahayeVahedZarib = 0;
                _context.FBs.Add(FB);
                _context.SaveChanges();
                gFBId = FB.ID;
            }
            else
                gFBId = FBUser.ID;

            ////////////////
            if (Hajm2 != 0)
            {
                Shomareh = 1;
                varDt = _context.RizMetreUserses.OrderByDescending(x => x.Shomareh).FirstOrDefault(x => x.FBId == gFBId);
                if (varDt != null)
                {
                    Shomareh = varDt.Shomareh + 1;
                }

                RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = Shomareh++;
                RizMetre.Sharh = "خاکبرداری در زمین خاکی نباتی";
                RizMetre.Tedad = Hajm2 / HajmKol;
                RizMetre.Tool = 0;
                RizMetre.Arz = 0;
                RizMetre.Ertefa = 0;
                RizMetre.Vazn = HajmKol;
                RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                RizMetre.FBId = gFBId;
                RizMetre.OperationsOfHamlId = 0;
                RizMetre.Type = "400" + KMNum.ToString("D3") + "01";///خاکبرداری در زمین خاکی نباتی
                RizMetre.ForItem = "";
                RizMetre.UseItem = "";
                _context.RizMetreUserses.Add(RizMetre);
                _context.SaveChanges();
                //RizMetre.Save();
            }
            if (Hajm3 != 0)
            {
                RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = Shomareh;
                RizMetre.Sharh = "خاکبرداری در زمین خاکی نرم";
                RizMetre.Tedad = Hajm3 / HajmKol;
                RizMetre.Tool = 0;
                RizMetre.Arz = 0;
                RizMetre.Ertefa = 0;
                RizMetre.Vazn = HajmKol;
                RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                RizMetre.FBId = gFBId;
                RizMetre.OperationsOfHamlId = 0;
                RizMetre.Type = "400" + KMNum.ToString("D3") + "02";///خاکبرداری در زمین خاکی نرم
                RizMetre.ForItem = "";
                RizMetre.UseItem = "";
                _context.RizMetreUserses.Add(RizMetre);
                _context.SaveChanges();
                //RizMetre.Save();
            }
            /////////////////
            if (Hajm4 != 0)
            {
                FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordUserId && x.Shomareh == "030104");
                gFBId = new Guid();
                if (FBUser == null)
                {
                    clsFB FB = new clsFB();
                    FB.BarAvordId = BarAvordUserId;
                    FB.Shomareh = "030104";
                    FB.BahayeVahedZarib = 0;
                    _context.FBs.Add(FB);
                    _context.SaveChanges();
                    gFBId = FB.ID;
                    //FBId = Operation_ItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["_ID"].ToString()), Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim(), 0);
                }
                else
                    gFBId = FBUser.ID;

                ////////////////

                Shomareh = 1;
                varDt = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();
                DtLastRizMetreUsersShomareh = clsConvert.ToDataTable(varDt);
                if (DtLastRizMetreUsersShomareh.Rows.Count != 0)
                {
                    Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["_Shomareh"].ToString().Trim()) + 1;
                }


                //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = Shomareh;
                RizMetre.Sharh = "خاکبرداری در زمین دج";
                RizMetre.Tedad = Hajm4 / HajmKol;
                RizMetre.Tool = 0;
                RizMetre.Arz = 0;
                RizMetre.Ertefa = 0;
                RizMetre.Vazn = HajmKol;
                RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                RizMetre.FBId = FBId;
                RizMetre.OperationsOfHamlId = 0;
                RizMetre.Type = "400" + KMNum.ToString("D3") + "03";///خاکبرداری در زمین دج
                RizMetre.ForItem = "";
                RizMetre.UseItem = "";
                _context.RizMetreUserses.Add(RizMetre);
                _context.SaveChanges();
                //RizMetre.Save();
            }
            /////////////////
            if (Hajm5 != 0)
            {

                varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordUserId && x.Shomareh == "030105").ToList();
                DtFBUser = clsConvert.ToDataTable(varFBUser);
                //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='030105'");
                //FBId = 0;
                //if (DtFBUser.Rows.Count == 0)
                //    FBId = Operation_ItemsFB.SaveFB(BarAvordID, "030105", 0);
                //else
                //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

                FBId = new Guid();
                if (DtFBUser.Rows.Count == 0)
                {
                    clsFB FB = new clsFB();
                    FB.BarAvordId = BarAvordUserId;
                    FB.Shomareh = "030105";
                    FB.BahayeVahedZarib = 0;
                    _context.FBs.Add(FB);
                    _context.SaveChanges();
                    FBId = FB.ID;
                }
                else
                    FBId = Guid.Parse(DtFBUser.Rows[0]["_ID"].ToString());

                ////////////////
                Shomareh = 1;
                varDt = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();
                DtLastRizMetreUsersShomareh = clsConvert.ToDataTable(varDt);
                if (DtLastRizMetreUsersShomareh.Rows.Count != 0)
                {
                    Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["_Shomareh"].ToString().Trim()) + 1;
                }


                //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = Shomareh;
                RizMetre.Sharh = "خاکبرداری در زمین سنگی";
                RizMetre.Tedad = Hajm5 / HajmKol;
                RizMetre.Tool = 0;
                RizMetre.Arz = 0;
                RizMetre.Ertefa = 0;
                RizMetre.Vazn = HajmKol;
                RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                RizMetre.FBId = FBId;
                RizMetre.OperationsOfHamlId = 0;
                RizMetre.Type = "400" + KMNum.ToString("D3") + "04";///خاکبرداری در زمین سنگی
                RizMetre.ForItem = "";
                RizMetre.UseItem = "";
                _context.RizMetreUserses.Add(RizMetre);
                _context.SaveChanges();
                //RizMetre.Save();
            }
            ////////////////
            if (Hajm6 != 0)
            {

                varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordUserId && x.Shomareh == "030201").ToList();
                DtFBUser = clsConvert.ToDataTable(varFBUser);
                //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='030201'");
                //FBId = 0;
                //if (DtFBUser.Rows.Count == 0)
                //    FBId = Operation_ItemsFB.SaveFB(BarAvordID, "030201", 0);
                //else
                //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

                FBId = new Guid();
                if (DtFBUser.Rows.Count == 0)
                {
                    clsFB FB = new clsFB();
                    FB.BarAvordId = BarAvordUserId;
                    FB.Shomareh = "030201";
                    FB.BahayeVahedZarib = 0;
                    _context.FBs.Add(FB);
                    _context.SaveChanges();
                    FBId = FB.ID;
                }
                else
                    FBId = Guid.Parse(DtFBUser.Rows[0]["_ID"].ToString());

                ////////////////
                Shomareh = 1;
                varDt = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();
                DtLastRizMetreUsersShomareh = clsConvert.ToDataTable(varDt);
                if (DtLastRizMetreUsersShomareh.Rows.Count != 0)
                {
                    Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["_Shomareh"].ToString().Trim()) + 1;
                }
                //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = Shomareh;
                RizMetre.Sharh = "خاکبرداری با مواد سوزا";
                RizMetre.Tedad = Hajm6 / HajmKol;
                RizMetre.Tool = 0;
                RizMetre.Arz = 0;
                RizMetre.Ertefa = 0;
                RizMetre.Vazn = HajmKol;
                RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                RizMetre.FBId = FBId;
                RizMetre.OperationsOfHamlId = 0;
                RizMetre.Type = "400" + KMNum.ToString("D3") + "05";///خاکبرداری با مواد سوزا
                RizMetre.ForItem = "";
                RizMetre.UseItem = "";
                _context.RizMetreUserses.Add(RizMetre);
                _context.SaveChanges();
                //RizMetre.Save();
            }
            ////////////////
            if (Hajm7 != 0)
            {

                varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordUserId && x.Shomareh == "030202").ToList();
                DtFBUser = clsConvert.ToDataTable(varFBUser);
                //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='030202'");
                //FBId = 0;
                //if (DtFBUser.Rows.Count == 0)
                //    FBId = Operation_ItemsFB.SaveFB(BarAvordID, "030202", 0);
                //else
                //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

                FBId = new Guid();
                if (DtFBUser.Rows.Count == 0)
                {
                    clsFB FB = new clsFB();
                    FB.BarAvordId = BarAvordUserId;
                    FB.Shomareh = "030202";
                    FB.BahayeVahedZarib = 0;
                    _context.FBs.Add(FB);
                    _context.SaveChanges();
                    FBId = FB.ID;
                }
                else
                    FBId = Guid.Parse(DtFBUser.Rows[0]["_ID"].ToString());

                ////////////////
                Shomareh = 1;
                varDt = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();
                DtLastRizMetreUsersShomareh = clsConvert.ToDataTable(varDt);
                if (DtLastRizMetreUsersShomareh.Rows.Count != 0)
                {
                    Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["_Shomareh"].ToString().Trim()) + 1;
                }

                //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = Shomareh;
                RizMetre.Sharh = "خاکبرداری با مواد منبسط شونده";
                RizMetre.Tedad = Hajm7 / HajmKol;
                RizMetre.Tool = 0;
                RizMetre.Arz = 0;
                RizMetre.Ertefa = 0;
                RizMetre.Vazn = HajmKol;
                RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                RizMetre.FBId = FBId;
                RizMetre.OperationsOfHamlId = 0;
                RizMetre.Type = "400" + KMNum.ToString("D3") + "06";///خاکبرداری با مواد منبسط شونده
                RizMetre.ForItem = "";
                RizMetre.UseItem = "";
                _context.RizMetreUserses.Add(RizMetre);
                _context.SaveChanges();
                //RizMetre.Save();
            }
            return true;
        }


        public bool SaveRizMetreEzafeBahaKhakBardari(Guid BarAvordUserId, Guid KMId, int KMNum, string KMS, string KME,
    decimal Hajm1, decimal Hajm2, decimal Hajm3, decimal Hajm4, decimal Hajm5, decimal Hajm6, decimal Hajm7
    , decimal HajmKol, bool ckFaseleHaml1, bool ckFaseleHaml2, bool ckFaseleHaml3, bool ckFaseleHaml4, bool ckFaseleHaml5, bool ckFaseleHaml6, bool ckFaseleHaml7
    , bool ckPakhsh2, bool ckPakhsh3, bool ckPakhsh4, bool ckPakhsh5, bool ckPakhsh6, bool ckPakhsh7
    , bool ckUseNatel6, bool ckUseNatel7, bool ckUseHydrolicPic6)
        {
            DataTable DtFBUser = new DataTable();
            //clsOperation_ItemsFB Operation_ItemsFB = new clsOperation_ItemsFB();
            //clsRizMetreUsers RizMetre = new clsRizMetreUsers();
            DataTable DtLastRizMetreUsersShomareh = new DataTable();
            Guid FBId = new Guid();
            int Shomareh = 1;

            //int FBId = 0;
            //int Shomareh = 0;
            if (ckFaseleHaml1)
            {
                if (Hajm1 != 0)
                {
                    var varFBUser1 = _context.FBs.Where(x => x.BarAvordId == BarAvordUserId && x.Shomareh == "030601").ToList();
                    DtFBUser = clsConvert.ToDataTable(varFBUser1);

                    //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='030601'");

                    FBId = new Guid();
                    if (DtFBUser.Rows.Count == 0)
                    {
                        clsFB FB = new clsFB();
                        FB.BarAvordId = BarAvordUserId;
                        FB.Shomareh = "030601";
                        FB.BahayeVahedZarib = 0;
                        _context.FBs.Add(FB);
                        _context.SaveChanges();
                        FBId = FB.ID;
                    }
                    else
                        FBId = Guid.Parse(DtFBUser.Rows[0]["_ID"].ToString());

                    //FBId = 0;
                    //if (DtFBUser.Rows.Count == 0)
                    //    FBId = Operation_ItemsFB.SaveFB(BarAvordID, "030601", 0);
                    //else
                    //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

                    ////////////////
                    Shomareh = 1;
                    var varDt1 = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();
                    DtLastRizMetreUsersShomareh = clsConvert.ToDataTable(varDt1);
                    if (DtLastRizMetreUsersShomareh.Rows.Count != 0)
                    {
                        Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["_Shomareh"].ToString().Trim()) + 1;
                    }

                    //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                    //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh;
                    RizMetre.Sharh = "اضافه بها لجن برداری چنانچه فاصله حمل بيـش از20 متر و حداکثر50 متر باشد";
                    RizMetre.Tedad = Hajm1 / HajmKol;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = HajmKol;
                    RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 0;
                    RizMetre.Type = "400" + KMNum.ToString("D3") + "10";///اضافه بها لجن برداری چنانچه فاصله حمل بيـش از20 متر و حداکثر50 متر باشد
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();

                    //RizMetre.Save();
                }
            }
            var varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordUserId && x.Shomareh == "030602").ToList();
            DtFBUser = clsConvert.ToDataTable(varFBUser);

            //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='030602'");
            //FBId = 0;
            //if (DtFBUser.Rows.Count == 0)
            //    FBId = Operation_ItemsFB.SaveFB(BarAvordID, "030602", 0);
            //else
            //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

            FBId = new Guid();
            if (DtFBUser.Rows.Count == 0)
            {
                clsFB FB = new clsFB();
                FB.BarAvordId = BarAvordUserId;
                FB.Shomareh = "030602";
                FB.BahayeVahedZarib = 0;
                _context.FBs.Add(FB);
                _context.SaveChanges();
                FBId = FB.ID;
            }
            else
                FBId = Guid.Parse(DtFBUser.Rows[0]["_ID"].ToString());

            Shomareh = 1;
            var varDt = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();
            DtLastRizMetreUsersShomareh = clsConvert.ToDataTable(varDt);
            if (DtLastRizMetreUsersShomareh.Rows.Count != 0)
            {
                Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["_Shomareh"].ToString().Trim()) + 1;
            }
            //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
            //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            if (ckFaseleHaml2)
            {
                ////////////////
                if (Hajm2 != 0)
                {
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh++;
                    RizMetre.Sharh = "اضافه بها خاکبرداری در زمین نباتی چنانچه فاصله حمل بيـش از20 متر و حداکثر50 متر باشد";
                    RizMetre.Tedad = Hajm2 / HajmKol;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = HajmKol;
                    RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 0;
                    RizMetre.Type = "400" + KMNum.ToString("D3") + "11";///اضافه بها خاکبرداری در زمین نباتی چنانچه فاصله حمل بيـش از20 متر و حداکثر50 متر باشد
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();

                    //RizMetre.Save();
                }
            }
            if (ckFaseleHaml3)
            {
                if (Hajm3 != 0)
                {
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh++;
                    RizMetre.Sharh = "اضافه بها خاکبرداری در زمین نرم چنانچه فاصله حمل بيـش از20 متر و حداکثر50 متر باشد";
                    RizMetre.Tedad = Hajm3 / HajmKol;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = HajmKol;
                    RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 0;
                    RizMetre.Type = "400" + KMNum.ToString("D3") + "12";///اضافه بها خاکبرداری در زمین نرم چنانچه فاصله حمل بيـش از20 متر و حداکثر50 متر باشد
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();

                    //RizMetre.Save();
                }
            }
            if (ckFaseleHaml4)
            {
                if (Hajm4 != 0)
                {
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh++;
                    RizMetre.Sharh = "اضافه بها خاکبرداری در زمین دج چنانچه فاصله حمل بيـش از20 متر و حداکثر50 متر باشد";
                    RizMetre.Tedad = Hajm4 / HajmKol;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = HajmKol;
                    RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 0;
                    RizMetre.Type = "400" + KMNum.ToString("D3") + "13";///اضافه بها خاکبرداری در زمین دج چنانچه فاصله حمل بيـش از20 متر و حداکثر50 متر باشد
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();

                    //RizMetre.Save();
                }
            }
            if (ckFaseleHaml5)
            {
                if (Hajm5 != 0)
                {

                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh++;
                    RizMetre.Sharh = "اضافه بها خاکبرداری در زمین سنگی چنانچه فاصله حمل بيـش از20 متر و حداکثر50 متر باشد";
                    RizMetre.Tedad = Hajm5 / HajmKol;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = HajmKol;
                    RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 0;
                    RizMetre.Type = "400" + KMNum.ToString("D3") + "14";///اضافه بها خاکبرداری در زمین سنگی چنانچه فاصله حمل بيـش از20 متر و حداکثر50 متر باشد
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();

                    //RizMetre.Save();
                }
            }
            if (ckFaseleHaml6)
            {
                if (Hajm6 != 0)
                {
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh++;
                    RizMetre.Sharh = "اضافه بها خاکبرداری با مواد سوزا چنانچه فاصله حمل بيـش از20 متر و حداکثر50 متر باشد";
                    RizMetre.Tedad = Hajm6 / HajmKol;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = HajmKol;
                    RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 0;
                    RizMetre.Type = "400" + KMNum.ToString("D3") + "15";///اضافه بها خاکبرداری با مواد سوزا چنانچه فاصله حمل بيـش از20 متر و حداکثر50 متر باشد
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();

                    //RizMetre.Save();
                }
            }
            if (ckFaseleHaml7)
            {
                if (Hajm7 != 0)
                {
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh;
                    RizMetre.Sharh = "اضافه بها خاکبرداری با مواد منبسط شونده چنانچه فاصله حمل بيـش از20 متر و حداکثر50 متر باشد";
                    RizMetre.Tedad = Hajm7 / HajmKol;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = HajmKol;
                    RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 0;
                    RizMetre.Type = "400" + KMNum.ToString("D3") + "16";///اضافه بها خاکبرداری با مواد منبسط شونده چنانچه فاصله حمل بيـش از20 متر و حداکثر50 متر باشد
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();

                    //RizMetre.Save();
                }
            }

            if (ckPakhsh2)
            {
                if (Hajm2 != 0)
                {
                    varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordUserId && x.Shomareh == "031203").ToList();
                    DtFBUser = clsConvert.ToDataTable(varFBUser);

                    //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='031203'");
                    //FBId = 0;
                    //if (DtFBUser.Rows.Count == 0)
                    //    FBId = Operation_ItemsFB.SaveFB(BarAvordID, "031203", 0);
                    //else
                    //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

                    FBId = new Guid();
                    if (DtFBUser.Rows.Count == 0)
                    {
                        clsFB FB = new clsFB();
                        FB.BarAvordId = BarAvordUserId;
                        FB.Shomareh = "031203";
                        FB.BahayeVahedZarib = 0;
                        _context.FBs.Add(FB);
                        _context.SaveChanges();
                        FBId = FB.ID;
                    }
                    else
                        FBId = Guid.Parse(DtFBUser.Rows[0]["_ID"].ToString());

                    ////////////////
                    Shomareh = 1;
                    varDt = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();
                    DtLastRizMetreUsersShomareh = clsConvert.ToDataTable(varDt);
                    if (DtLastRizMetreUsersShomareh.Rows.Count != 0)
                    {
                        Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["_Shomareh"].ToString().Trim()) + 1;
                    }

                    //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                    //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh;
                    RizMetre.Sharh = "اضافه بها پخـش خاکهاي نباتي ريسه شده در زمین خاک نباتی";
                    RizMetre.Tedad = Hajm2 / HajmKol;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = HajmKol;
                    RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 0;
                    RizMetre.Type = "400" + KMNum.ToString("D3") + "20";///اضافه بها پخـش خاکهاي نباتي ريسه شده در زمین خاک نباتی
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();

                    //RizMetre.Save();
                }
            }

            varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordUserId && x.Shomareh == "031204").ToList();
            DtFBUser = clsConvert.ToDataTable(varFBUser);

            //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='031204'");
            //FBId = 0;
            //if (DtFBUser.Rows.Count == 0)
            //    FBId = Operation_ItemsFB.SaveFB(BarAvordID, "031204", 0);
            //else
            //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
            FBId = new Guid();
            if (DtFBUser.Rows.Count == 0)
            {
                clsFB FB = new clsFB();
                FB.BarAvordId = BarAvordUserId;
                FB.Shomareh = "031204";
                FB.BahayeVahedZarib = 0;
                _context.FBs.Add(FB);
                _context.SaveChanges();
                FBId = FB.ID;
            }
            else
                FBId = Guid.Parse(DtFBUser.Rows[0]["_ID"].ToString());

            //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
            //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            Shomareh = 1;
            varDt = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();
            DtLastRizMetreUsersShomareh = clsConvert.ToDataTable(varDt);
            if (DtLastRizMetreUsersShomareh.Rows.Count != 0)
            {
                Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["_Shomareh"].ToString().Trim()) + 1;
            }
            if (ckPakhsh3)
            {
                if (Hajm3 != 0)
                {
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh++;
                    RizMetre.Sharh = "اضافه بها پخـش مصالح حاصل از خاک برداري در زمین خاک نرم";
                    RizMetre.Tedad = Hajm3 / HajmKol;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = HajmKol;
                    RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 0;
                    RizMetre.Type = "400" + KMNum.ToString("D3") + "21";///اضافه بها پخـش مصالح حاصل از خاک برداري در زمین خاک نرم
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();

                    //RizMetre.Save();
                }
            }

            if (ckPakhsh4)
            {
                if (Hajm4 != 0)
                {

                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh++;
                    RizMetre.Sharh = "اضافه بها پخـش مصالح حاصل از خاک برداري در زمین خاک دج";
                    RizMetre.Tedad = Hajm4 / HajmKol;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = HajmKol;
                    RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 0;
                    RizMetre.Type = "400" + KMNum.ToString("D3") + "22";///اضافه بها پخـش مصالح حاصل از خاک برداري در زمین خاک دج
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();

                    //RizMetre.Save();
                }
            }

            if (ckPakhsh5)
            {
                if (Hajm5 != 0)
                {
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh++;
                    RizMetre.Sharh = "اضافه بها پخـش مصالح حاصل از خاک برداري در زمین خاک سنگی";
                    RizMetre.Tedad = Hajm5 / HajmKol;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = HajmKol;
                    RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 0;
                    RizMetre.Type = "400" + KMNum.ToString("D3") + "23";///اضافه بها پخـش مصالح حاصل از خاک برداري در زمین خاک سنگی
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();

                    //RizMetre.Save();
                }
            }

            if (ckPakhsh6)
            {
                if (Hajm6 != 0)
                {
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh++;
                    RizMetre.Sharh = "اضافه بها پخـش مصالح حاصل از خاک برداري با مواد سوزا";
                    RizMetre.Tedad = Hajm6 / HajmKol;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = HajmKol;
                    RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 0;
                    RizMetre.Type = "400" + KMNum.ToString("D3") + "24";///اضافه بها پخـش مصالح حاصل از خاک برداري با مواد سوزا
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();

                    //RizMetre.Save();
                }
            }

            if (ckPakhsh7)
            {
                if (Hajm7 != 0)
                {
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh;
                    RizMetre.Sharh = "اضافه بها پخـش مصالح حاصل از خاک برداري با مواد منبسط شونده";
                    RizMetre.Tedad = Hajm7 / HajmKol;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = HajmKol;
                    RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 0;
                    RizMetre.Type = "400" + KMNum.ToString("D3") + "25";///اضافه بها پخـش مصالح حاصل از خاک برداري با مواد منبسط شونده
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();

                    //RizMetre.Save();
                }
            }

            varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordUserId && x.Shomareh == "030204").ToList();
            DtFBUser = clsConvert.ToDataTable(varFBUser);
            //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='030204'");
            //FBId = 0;
            //if (DtFBUser.Rows.Count == 0)
            //    FBId = Operation_ItemsFB.SaveFB(BarAvordID, "030204", 0);
            //else
            //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
            FBId = new Guid();
            if (DtFBUser.Rows.Count == 0)
            {
                clsFB FB = new clsFB();
                FB.BarAvordId = BarAvordUserId;
                FB.Shomareh = "030204";
                FB.BahayeVahedZarib = 0;
                _context.FBs.Add(FB);
                _context.SaveChanges();
                FBId = FB.ID;
            }
            else
                FBId = Guid.Parse(DtFBUser.Rows[0]["_ID"].ToString());

            //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
            //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            Shomareh = 1;
            varDt = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();
            DtLastRizMetreUsersShomareh = clsConvert.ToDataTable(varDt);
            if (DtLastRizMetreUsersShomareh.Rows.Count != 0)
            {
                Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["_Shomareh"].ToString().Trim()) + 1;
            }
            if (ckUseNatel6)
            {
                ////////////////
                if (Hajm6 != 0)
                {
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh++;
                    RizMetre.Sharh = "اضافه بها سیستم نانل به جای چاشنی الکتریکی با مواد سوزا";
                    RizMetre.Tedad = Hajm6 / HajmKol;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = HajmKol;
                    RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 0;
                    RizMetre.Type = "400" + KMNum.ToString("D3") + "30";///اضافه بها سیستم نانل به جای چاشنی الکتریکی با مواد سوزا
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();

                    //RizMetre.Save();
                }
            }

            if (ckUseNatel7)
            {
                ////////////////
                if (Hajm7 != 0)
                {
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh;
                    RizMetre.Sharh = "اضافه بها سیستم نانل به جای چاشنی الکتریکی با مواد منبسط شونده";
                    RizMetre.Tedad = Hajm7 / HajmKol;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = HajmKol;
                    RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 0;
                    RizMetre.Type = "400" + KMNum.ToString("D3") + "31";///اضافه بها سیستم نانل به جای چاشنی الکتریکی با مواد منبسط شونده
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();

                    //RizMetre.Save();
                }
            }
            ///////////
            ///////
            ////////
            varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordUserId && x.Shomareh == "030201").ToList();
            DtFBUser = clsConvert.ToDataTable(varFBUser);
            //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='030201'");
            //FBId = 0;
            //if (DtFBUser.Rows.Count == 0)
            //    FBId = Operation_ItemsFB.SaveFB(BarAvordID, "030201", 0);
            //else
            //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

            FBId = new Guid();
            if (DtFBUser.Rows.Count == 0)
            {
                clsFB FB = new clsFB();
                FB.BarAvordId = BarAvordUserId;
                FB.Shomareh = "030201";
                FB.BahayeVahedZarib = 0;
                _context.FBs.Add(FB);
                _context.SaveChanges();
                FBId = FB.ID;
            }
            else
                FBId = Guid.Parse(DtFBUser.Rows[0]["_ID"].ToString());

            //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
            //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            Shomareh = 1;
            varDt = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();
            DtLastRizMetreUsersShomareh = clsConvert.ToDataTable(varDt);
            if (DtLastRizMetreUsersShomareh.Rows.Count != 0)
            {
                Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["_Shomareh"].ToString().Trim()) + 1;
            }
            if (ckUseHydrolicPic6)
            {
                ////////////////
                if (Hajm6 != 0)
                {
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh++;
                    RizMetre.Sharh = "استفاده از چکش هیدرولیک و GSI بزرگتر از 60";
                    RizMetre.Tedad = Hajm6 / HajmKol;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = HajmKol;
                    RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 0;
                    RizMetre.Type = "400" + KMNum.ToString("D3") + "40";///استفاده از چکش هیدرولیک و GSI بزرگتر از 60
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();
                    //RizMetre.Save();
                }
            }

            return true;
        }


        public bool SaveRizMetreGharzeh(Guid BarAvordUserID)
        {
            DataTable Dt = tblAmalyateKhakiInfoForBarAvord.ListWithParameter("_BaravordUserId='" + BarAvordUserID + "' and _type=4", 4);

            if (Dt.Rows.Count != 0)
            {
                decimal X = decimal.Parse(Dt.Rows[0]["_FromKM"].ToString().Trim());
                decimal Y = decimal.Parse(Dt.Rows[0]["_ToKM"].ToString().Trim());
                long KMId = long.Parse(Dt.Rows[0]["_ID"].ToString().Trim());
                int KMNum = int.Parse(Dt.Rows[0]["_KMNum"].ToString().Trim());
                string strKMNum = KMNum.ToString("D3");

                clsRizMetreUsers.Delete("substring(clsRizMetreUserss.Type,1,3)='440' and substring(clsRizMetreUserss.Type,4,3)='" + strKMNum + "'  and clsFBs_.BarAvordId=" + BarAvordUserID
                + " and clsFBs.shomareh in('030103','030104','030105','030602')");

                Guid AmalyateKhakiInfoForBarAvordId = Guid.Parse(Dt.Rows[0]["_ID"].ToString().Trim());
                var varMGharzehBarAvordDetails = _context.AmalyateKhakiInfoForBarAvordDetails1.Where(x => x.AmalyateKhakiInfoForBarAvordId == AmalyateKhakiInfoForBarAvordId).ToList();
                DataTable DtMGharzehBarAvordDetails = clsConvert.ToDataTable(varMGharzehBarAvordDetails);
                //DataTable DtMGharzehBarAvordDetails = clsAmalyateKhakiInfoForBarAvordDetails.ListWithParameter("AmalyateKhakiInfoForBarAvordId=" + Dt.Rows[0]["Id"].ToString().Trim());

                //string str = "";
                List<Guid> lstIds = new List<Guid>();
                if (DtMGharzehBarAvordDetails.Rows.Count != 0)
                {
                    //str += "AmalyateKhakiInfoForBarAvordDetailsId in(";
                    for (int i = 0; i < DtMGharzehBarAvordDetails.Rows.Count; i++)
                    {
                        lstIds.Add(Guid.Parse(DtMGharzehBarAvordDetails.Rows[i]["_ID"].ToString()));
                        //if ((i + 1) != DtMGharzehBarAvordDetails.Rows.Count)
                        //    str += DtMGharzehBarAvordDetails.Rows[i]["Id"].ToString() + ",";
                        //else
                        //    str += DtMGharzehBarAvordDetails.Rows[i]["Id"].ToString();
                    }
                    // str += ")";
                }

                var varMGharzehBarAvordDetailsMore = _context.AmalyateKhakiInfoForBarAvordDetailsMores.Where(x => lstIds.Contains(x.AmalyateKhakiInfoForBarAvordDetailsId)).ToList();
                DataTable DtMGharzehBarAvordDetailsMore = clsConvert.ToDataTable(varMGharzehBarAvordDetailsMore);
                //DataTable DtMGharzehBarAvordDetailsMore = clsAmalyateKhakiInfoForBarAvordDetails.ListWithParameterMore(str);

                var varMGharzehBarAvordDetailsEzafeBahas = _context.AmalyateKhakiInfoForBarAvordDetailsEzafeBahas.Where(x => lstIds.Contains(x.AmalyateKhakiInfoForBarAvordDetailsId)).ToList();
                DataTable DtMGharzehBarAvordDetailsEzafeBaha = clsConvert.ToDataTable(varMGharzehBarAvordDetailsEzafeBahas);
                //DataTable DtMGharzehBarAvordDetailsEzafeBaha = clsAmalyateKhakiInfoForBarAvordDetails.ListWithParameterEzafeBaha(str);

                decimal dGhDetail1 = 0;
                decimal dGhDarsad2 = 0;
                decimal dGhDarsad3 = 0;
                decimal dGhDarsad4 = 0;
                for (int i = 0; i < DtMGharzehBarAvordDetailsMore.Rows.Count; i++)
                {
                    if (DtMGharzehBarAvordDetailsMore.Rows[i]["_Name"].ToString().Trim() == "GhDetail1")
                        dGhDetail1 = decimal.Parse(DtMGharzehBarAvordDetailsMore.Rows[i]["_value"].ToString().Trim());
                    else if (DtMGharzehBarAvordDetailsMore.Rows[i]["_Name"].ToString().Trim() == "GhDarsad2")
                        dGhDarsad2 = decimal.Parse(DtMGharzehBarAvordDetailsMore.Rows[i]["_value"].ToString().Trim());
                    else if (DtMGharzehBarAvordDetailsMore.Rows[i]["_Name"].ToString().Trim() == "GhDarsad3")
                        dGhDarsad3 = decimal.Parse(DtMGharzehBarAvordDetailsMore.Rows[i]["_value"].ToString().Trim());
                }

                ////////////
                bool bGhFaseleHaml1 = false;
                bool bGhFaseleHaml2 = false;
                bool bGhFaseleHaml3 = false;
                bool bGhFaseleHaml4 = false;
                for (int i = 0; i < DtMGharzehBarAvordDetailsEzafeBaha.Rows.Count; i++)
                {
                    if (DtMGharzehBarAvordDetailsEzafeBaha.Rows[i]["_Name"].ToString().Trim() == "GhFaseleHaml1")
                        bGhFaseleHaml1 = bool.Parse(DtMGharzehBarAvordDetailsEzafeBaha.Rows[i]["_value"].ToString().Trim());
                    else if (DtMGharzehBarAvordDetailsEzafeBaha.Rows[i]["_Name"].ToString().Trim() == "GhFaseleHaml2")
                        bGhFaseleHaml2 = bool.Parse(DtMGharzehBarAvordDetailsEzafeBaha.Rows[i]["_value"].ToString().Trim());
                    else if (DtMGharzehBarAvordDetailsEzafeBaha.Rows[i]["_Name"].ToString().Trim() == "GhFaseleHaml3")
                        bGhFaseleHaml3 = bool.Parse(DtMGharzehBarAvordDetailsEzafeBaha.Rows[i]["_value"].ToString().Trim());
                    else if (DtMGharzehBarAvordDetailsEzafeBaha.Rows[i]["_Name"].ToString().Trim() == "GhFaseleHaml4")
                        bGhFaseleHaml4 = bool.Parse(DtMGharzehBarAvordDetailsEzafeBaha.Rows[i]["_value"].ToString().Trim());
                }

                string strParam1 = "_BarAvordUserId='" + BarAvordUserID + "' and tblAmalyateKhakiInfoForBarAvords.type=1 and (tblAmalyateKhakiInfoForBarAvordDetailsMores.Name ='Haml') and (tblAmalyateKhakiInfoForBarAvordDetails.Type in (5,6,7))";
                DataTable DtAmalyateKhakiAllInfo = tblAmalyateKhakiInfoForBarAvord.AmalyateKhakiGetAllInfoForBarAvord(strParam1);
                decimal dValue1 = decimal.Parse(DtAmalyateKhakiAllInfo.Rows[0]["_sumDetailsMoreValue"].ToString() == "" ? "0" : DtAmalyateKhakiAllInfo.Rows[0]["_sumDetailsMoreValue"].ToString());

                strParam1 = "_BarAvordUserId='" + BarAvordUserID + "' and tblAmalyateKhakiInfoForBarAvords.type=2 and (tblAmalyateKhakiInfoForBarAvordDetailsMores.Name ='Haml') and (tblAmalyateKhakiInfoForBarAvordDetails.Type in (2))";
                DtAmalyateKhakiAllInfo = tblAmalyateKhakiInfoForBarAvord.AmalyateKhakiGetAllInfoForBarAvord(strParam1);
                decimal dValue2 = decimal.Parse(DtAmalyateKhakiAllInfo.Rows[0]["_sumDetailsMoreValue"].ToString() == "" ? "0" : DtAmalyateKhakiAllInfo.Rows[0]["_sumDetailsMoreValue"].ToString());

                strParam1 = "_BarAvordUserId='" + BarAvordUserID + "' and tblAmalyateKhakiInfoForBarAvords.type=3 and (tblAmalyateKhakiInfoForBarAvordMores.Name ='HajmBetweenTo100') ";
                DtAmalyateKhakiAllInfo = tblAmalyateKhakiInfoForBarAvord.AmalyateKhakiGetAllInfoForBarAvordWithOutDetails(strParam1);
                decimal dValue3 = decimal.Parse(DtAmalyateKhakiAllInfo.Rows[0]["_sumMoreValue"].ToString() == "" ? "0" : DtAmalyateKhakiAllInfo.Rows[0]["_sumMoreValue"].ToString());

                decimal dGharzehKhBSangi = dValue3 - (dValue1 + dValue2);

                strParam1 = "_BarAvordUserId='" + BarAvordUserID + "' and tblAmalyateKhakiInfoForBarAvords.type=1 and (tblAmalyateKhakiInfoForBarAvordDetailsMores.Name ='Haml') and (tblAmalyateKhakiInfoForBarAvordDetails.Type in (3,4))";
                DtAmalyateKhakiAllInfo = tblAmalyateKhakiInfoForBarAvord.AmalyateKhakiGetAllInfoForBarAvord(strParam1);
                dValue1 = decimal.Parse(DtAmalyateKhakiAllInfo.Rows[0]["_sumDetailsMoreValue"].ToString() == "" ? "0" : DtAmalyateKhakiAllInfo.Rows[0]["_sumDetailsMoreValue"].ToString());

                strParam1 = "BarAvordUserId='" + BarAvordUserID + "' and tblAmalyateKhakiInfoForBarAvords.type=2 and (tblAmalyateKhakiInfoForBarAvordDetailsMores.Name ='Haml') and (tblAmalyateKhakiInfoForBarAvordDetails.Type in (1))";
                DtAmalyateKhakiAllInfo = tblAmalyateKhakiInfoForBarAvord.AmalyateKhakiGetAllInfoForBarAvord(strParam1);
                dValue2 = decimal.Parse(DtAmalyateKhakiAllInfo.Rows[0]["_sumDetailsMoreValue"].ToString() == "" ? "0" : DtAmalyateKhakiAllInfo.Rows[0]["_sumDetailsMoreValue"].ToString());

                strParam1 = "BarAvordUserId='" + BarAvordUserID + "' and tblAmalyateKhakiInfoForBarAvords.type=3 and (tblAmalyateKhakiInfoForBarAvordMores.Name in('HajmBetween0To30','HajmBetween30To100')) ";
                DtAmalyateKhakiAllInfo = tblAmalyateKhakiInfoForBarAvord.AmalyateKhakiGetAllInfoForBarAvordWithOutDetails(strParam1);
                dValue3 = decimal.Parse(DtAmalyateKhakiAllInfo.Rows[0]["_sumMoreValue"].ToString() == "" ? "0" : DtAmalyateKhakiAllInfo.Rows[0]["_sumMoreValue"].ToString());

                decimal dGharzehKhBNarmSakht = dValue3 - (dValue1 + dValue2);

                decimal sumAll = dGharzehKhBSangi + dGharzehKhBNarmSakht + dGhDetail1;
                decimal dGhDarsad1 = dGhDetail1 / sumAll * 100;
                dGhDarsad4 = 100 - (dGhDarsad1 + dGhDarsad2 + dGhDarsad3);

                decimal dGhDetail2 = dGhDarsad2 * sumAll / 100;
                decimal dGhDetail3 = dGhDarsad3 * sumAll / 100;
                decimal dGhDetail4 = dGhDarsad4 * sumAll / 100;

                //clsOperation_ItemsFB Operation_ItemsFBFH = new clsOperation_ItemsFB();
                var varFBUserFH = _context.FBs.Where(x => x.BarAvordId == BarAvordUserID && x.Shomareh == "030602").ToList();
                DataTable DtFBUserFH = clsConvert.ToDataTable(varFBUserFH);
                //DataTable DtFBUserFH = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='030602'");
                DataTable DtLastRizMetreUsersShomarehFH = new DataTable();
                //int FBIdFH = 0;
                int ShomarehFH = 0;
                //if (DtFBUserFH.Rows.Count == 0)
                //    FBIdFH = Operation_ItemsFBFH.SaveFB(BarAvordID, "030602", 0);
                //else
                //    FBIdFH = int.Parse(DtFBUserFH.Rows[0]["Id"].ToString());
                Guid FBIdFH = new Guid();
                if (DtFBUserFH.Rows.Count == 0)
                {
                    clsFB FB = new clsFB();
                    FB.BarAvordId = BarAvordUserID;
                    FB.Shomareh = "030602";
                    FB.BahayeVahedZarib = 0;
                    _context.FBs.Add(FB);
                    _context.SaveChanges();
                    FBIdFH = FB.ID;
                }
                else
                    FBIdFH = Guid.Parse(DtFBUserFH.Rows[0]["_ID"].ToString());

                ShomarehFH = 1;
                var varLastRizMetreUsersShomarehFH = _context.RizMetreUserses.Where(x => x.FBId == FBIdFH).ToList();
                DtLastRizMetreUsersShomarehFH = clsConvert.ToDataTable(varLastRizMetreUsersShomarehFH);
                if (DtLastRizMetreUsersShomarehFH.Rows.Count != 0)
                {
                    ShomarehFH = int.Parse(DtLastRizMetreUsersShomarehFH.Rows[0]["_Shomareh"].ToString().Trim()) + 1;
                }

                //DtLastRizMetreUsersShomarehFH = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBIdFH);
                //ShomarehFH = int.Parse(DtLastRizMetreUsersShomarehFH.Rows[0]["lastShomareh"].ToString().Trim());

                //clsOperation_ItemsFB Operation_ItemsFB = new clsOperation_ItemsFB();
                //clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                var varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordUserID && x.Shomareh == "030103").ToList();
                DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

                //DataTable DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='030103'");
                //DataTable DtLastRizMetreUsersShomareh = new DataTable();
                //int FBId = 0;
                //int Shomareh = 0;
                //if (DtFBUser.Rows.Count == 0)
                //    FBId = Operation_ItemsFB.SaveFB(BarAvordID, "030103", 0);
                //else
                //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

                Guid FBId = new Guid();
                if (DtFBUser.Rows.Count == 0)
                {
                    clsFB FB = new clsFB();
                    FB.BarAvordId = BarAvordUserID;
                    FB.Shomareh = "030103";
                    FB.BahayeVahedZarib = 0;
                    _context.FBs.Add(FB);
                    _context.SaveChanges();
                    FBId = FB.ID;
                }
                else
                    FBId = Guid.Parse(DtFBUser.Rows[0]["_ID"].ToString());

                int Shomareh = 1;
                var varLastRizMetreUsersShomareh = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();
                DataTable DtLastRizMetreUsersShomareh = clsConvert.ToDataTable(varLastRizMetreUsersShomareh);
                if (DtLastRizMetreUsersShomareh.Rows.Count != 0)
                {
                    Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["_Shomareh"].ToString().Trim()) + 1;
                }

                //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());

                if (dGhDetail1 != 0)
                {
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh++;
                    RizMetre.Sharh = "خاکبرداری نباتی در زمین محل قرضه";
                    RizMetre.Tedad = 1;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = dGhDetail1;
                    RizMetre.Des = "مختصات x: " + X + " ، Y: " + Y;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 0;
                    RizMetre.Type = "440" + KMNum.ToString("D3") + "00";///خاکبرداری نباتی در زمین محل قرضه
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();
                    //RizMetre.Save();
                    if (bGhFaseleHaml1)
                    {
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = ShomarehFH++;
                        RizMetre.Sharh = "اضافه بها حمل خاکبرداری نباتی در زمین محل قرضه به آیتم 030103 ";
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = 0;
                        RizMetre.Ertefa = 0;
                        RizMetre.Vazn = dGhDetail1;
                        RizMetre.Des = "مختصات x: " + X + " ، Y: " + Y;
                        RizMetre.FBId = FBId;
                        RizMetre.OperationsOfHamlId = 0;
                        RizMetre.Type = "440" + KMNum.ToString("D3") + "10";///اضافه بها حمل خاکبرداری نباتی در زمین محل قرضه به آیتم 030103
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();

                        //RizMetre.Save();
                    }
                }
                if (dGhDetail2 != 0)
                {
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh;
                    RizMetre.Sharh = "خاکبرداری در زمین نرم";
                    RizMetre.Tedad = 1;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = dGhDetail2;
                    RizMetre.Des = "مختصات x: " + X + " ، Y: " + Y;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 0;
                    RizMetre.Type = "440" + KMNum.ToString("D3") + "01";///خاکبرداری در زمین نرم
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();

                    //RizMetre.Save();
                    if (bGhFaseleHaml2)
                    {
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = ShomarehFH++;
                        RizMetre.Sharh = "اضافه بها حمل خاکبرداری در زمین نرم به آیتم 030103 ";
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = 0;
                        RizMetre.Ertefa = 0;
                        RizMetre.Vazn = dGhDetail2;
                        RizMetre.Des = "مختصات x: " + X + " ، Y: " + Y;
                        RizMetre.FBId = FBId;
                        RizMetre.OperationsOfHamlId = 0;
                        RizMetre.Type = "440" + KMNum.ToString("D3") + "10";///اضافه بها حمل خاکبرداری در زمین نرم به آیتم 030103
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();

                        //RizMetre.Save();
                    }
                }

                if (dGhDetail3 != 0)
                {
                    varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordUserID && x.Shomareh == "030104").ToList();
                    DtFBUser = clsConvert.ToDataTable(varFBUser);

                    //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='030104'");
                    //DtLastRizMetreUsersShomareh = new DataTable();
                    //FBId = 0;
                    //Shomareh = 0;
                    //if (DtFBUser.Rows.Count == 0)
                    //    FBId = Operation_ItemsFB.SaveFB(BarAvordID, "030104", 0);
                    //else
                    //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

                    FBId = new Guid();
                    if (DtFBUser.Rows.Count == 0)
                    {
                        clsFB FB = new clsFB();
                        FB.BarAvordId = BarAvordUserID;
                        FB.Shomareh = "030104";
                        FB.BahayeVahedZarib = 0;
                        _context.FBs.Add(FB);
                        _context.SaveChanges();
                        FBId = FB.ID;
                    }
                    else
                        FBId = Guid.Parse(DtFBUser.Rows[0]["_ID"].ToString());

                    Shomareh = 1;
                    varLastRizMetreUsersShomareh = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();
                    DtLastRizMetreUsersShomareh = clsConvert.ToDataTable(varLastRizMetreUsersShomareh);
                    if (DtLastRizMetreUsersShomareh.Rows.Count != 0)
                    {
                        Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["_Shomareh"].ToString().Trim()) + 1;
                    }

                    //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                    //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());

                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh;
                    RizMetre.Sharh = "خاکبرداری در زمین دج";
                    RizMetre.Tedad = 1;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = dGhDetail3;
                    RizMetre.Des = "مختصات x: " + X + " ، Y: " + Y;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 0;
                    RizMetre.Type = "440" + KMNum.ToString("D3") + "02";///خاکبرداری در زمین دج
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();

                    //RizMetre.Save();

                    if (bGhFaseleHaml3)
                    {
                        RizMetre.Shomareh = ShomarehFH++;
                        RizMetre.Sharh = "اضافه بها حمل خاکبرداری در زمین دج به آیتم 030104 ";
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = 0;
                        RizMetre.Ertefa = 0;
                        RizMetre.Vazn = dGhDetail3;
                        RizMetre.Des = "مختصات x: " + X + " ، Y: " + Y;
                        RizMetre.FBId = FBId;
                        RizMetre.OperationsOfHamlId = 0;
                        RizMetre.Type = "440" + KMNum.ToString("D3") + "10";///اضافه بها حمل خاکبرداری در زمین دج به آیتم 030104
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();

                        //RizMetre.Save();
                    }

                }

                if (dGhDetail4 != 0)
                {
                    varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordUserID && x.Shomareh == "03015").ToList();
                    DtFBUser = clsConvert.ToDataTable(varFBUser);
                    //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='030105'");
                    //DtLastRizMetreUsersShomareh = new DataTable();
                    //FBId = 0;
                    //Shomareh = 0;
                    //if (DtFBUser.Rows.Count == 0)
                    //    FBId = Operation_ItemsFB.SaveFB(BarAvordID, "030105", 0);
                    //else
                    //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
                    FBId = new Guid();
                    if (DtFBUser.Rows.Count == 0)
                    {
                        clsFB FB = new clsFB();
                        FB.BarAvordId = BarAvordUserID;
                        FB.Shomareh = "030105";
                        FB.BahayeVahedZarib = 0;
                        _context.FBs.Add(FB);
                        _context.SaveChanges();
                        FBId = FB.ID;
                    }
                    else
                        FBId = Guid.Parse(DtFBUser.Rows[0]["_ID"].ToString());

                    Shomareh = 1;
                    varLastRizMetreUsersShomareh = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();
                    DtLastRizMetreUsersShomareh = clsConvert.ToDataTable(varLastRizMetreUsersShomareh);
                    if (DtLastRizMetreUsersShomareh.Rows.Count != 0)
                    {
                        Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["_Shomareh"].ToString().Trim()) + 1;
                    }

                    //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                    //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());

                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh;
                    RizMetre.Sharh = "خاکبرداری در زمین سنگی";
                    RizMetre.Tedad = 1;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = dGhDetail4;
                    RizMetre.Des = "مختصات x: " + X + " ، Y: " + Y;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 0;
                    RizMetre.Type = "440" + KMNum.ToString("D3") + "03";///خاکبرداری در زمین سنگی
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();

                    //RizMetre.Save();

                    if (bGhFaseleHaml4)
                    {
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = ShomarehFH++;
                        RizMetre.Sharh = "اضافه بها حمل خاکبرداری در زمین نرم به آیتم 030105 ";
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = 0;
                        RizMetre.Ertefa = 0;
                        RizMetre.Vazn = dGhDetail4;
                        RizMetre.Des = "مختصات x: " + X + " ، Y: " + Y;
                        RizMetre.FBId = FBId;
                        RizMetre.OperationsOfHamlId = 0;
                        RizMetre.Type = "440" + KMNum.ToString("D3") + "10";///اضافه بها حمل خاکبرداری در زمین سنگی به آیتم 030105
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();
                    }
                }
                return true;
            }
            else
                return false;
        }
    }



}
