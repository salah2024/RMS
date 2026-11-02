using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RMS.Models.Entity;
using RMS.Models.StoredProceduresData;
using System.Data;
using static RMS.Models.Common.AbnieFaniEnum;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Common;

public class AbnieFaniCommonGhalebBandiDalPishSakhteh(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public bool GhalebBandiDalPishSakhteh(Guid BarAvordId, Guid PolVaAbroId, NoeFehrestBaha NoeFBId, int PolNum, string LAbro, string w1, string w2, string w3, string w4,
    string t, string j, string c1, string D, string TedadDahaneh)
    {
        try
        {
            clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "080701" && x.NoeFBId == NoeFBId);
            //DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

            Guid guFBId = new Guid();
            //if (DtFBUser.Rows.Count == 0)
            if (varFBUser == null)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "080701";
                //FBSave.BahayeVahedZarib = 0;
                FBSave.NoeFBId = NoeFBId;
                _context.FBs.Add(FBSave);
                //_context.SaveChanges();
                guFBId = FBSave.ID;
            }
            else
                guFBId = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
            ////////////////
            //var varDtLastRizMetreUsersesShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).ToList();
            //DataTable DtLastRizMetreUsersesShomareh = clsConvert.ToDataTable(varDtLastRizMetreUsersesShomareh);
            //int Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());

            var RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            long Shomareh = 0;
            if (RizMetreUserLastShomareh != null)
                Shomareh = RizMetreUserLastShomareh.Shomareh + 1;
            else
                Shomareh = 1;


            ////////////////قالب زیر دال پیش ساخته
            //DataTable DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId);
            //int Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = Shomareh;
            RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
            RizMetre.Sharh = "قالب زیر دال پیش ساخته";
            RizMetre.Tedad = decimal.Parse(LAbro.Trim());
            RizMetre.Tool = int.Parse(TedadDahaneh.Trim());
            RizMetre.Arz = int.Parse(D.Trim()) + (2 * decimal.Parse(c1.Trim())) - (2 * decimal.Parse(j.Trim()));
            RizMetre.Ertefa = null;
            RizMetre.Vazn = null;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "300" + PolNum.ToString("D3") + "52";///قالب زیر دال پیش ساخته
            RizMetre.ForItem = "";
            RizMetre.UseItem = "";

            decimal dMeghdarJoz = 0;
            if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                dMeghdarJoz = 0;
            else
                dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

            RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz, 2);

            _context.RizMetreUserses.Add(RizMetre);

            clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
            {
                PolVaAbroBarAvordId = PolVaAbroId,
                RizMetreId = RizMetre.ID,
                PolDetailType = PolDetailTypeEnum.قالب_زیر_دال_پیش_ساخته
            };
            _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
            //_context.SaveChanges();
            //RizMetre.Save();
            ///////
            RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = ++Shomareh;
            RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
            RizMetre.Sharh = "قالب بغل دال پیش ساخته در جهت طولی";
            RizMetre.Tedad = decimal.Parse(LAbro.Trim());
            RizMetre.Tool = 2; //int.Parse(TedadDahaneh.Trim());
            RizMetre.Arz = null;
            RizMetre.Ertefa = decimal.Parse(t.Trim());
            RizMetre.Vazn = null;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "300" + PolNum.ToString("D3") + "52";///قالب بغل دال پیش ساخته در جهت طولی
            RizMetre.ForItem = "";
            RizMetre.UseItem = "";

            dMeghdarJoz = 0;
            if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                dMeghdarJoz = 0;
            else
                dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

            RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz, 2);

            _context.RizMetreUserses.Add(RizMetre);
            polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
            {
                PolVaAbroBarAvordId = PolVaAbroId,
                RizMetreId = RizMetre.ID,
                PolDetailType = PolDetailTypeEnum.قالب_بغل_دال_پیش_ساخته_در_جهت_طولی
            };
            _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
            //_context.SaveChanges();
            //RizMetre.Save();
            /////////////
            RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = ++Shomareh;
            RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
            RizMetre.Sharh = "قالب بغل دال پیش ساخته در جهت عرضی";
            RizMetre.Tedad = 2 * decimal.Parse(LAbro.Trim());// int.Parse(TedadDahaneh.Trim());
            RizMetre.Tool = null; //2 * ;
            RizMetre.Arz = int.Parse(D.Trim()) + (2 * decimal.Parse(c1.Trim())) - (2 * decimal.Parse(j.Trim()));
            RizMetre.Ertefa = decimal.Parse(t.Trim());
            RizMetre.Vazn = null;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "300" + PolNum.ToString("D3") + "52";///قالب بغل دال پیش ساخته در جهت عرضی
            RizMetre.ForItem = "";
            RizMetre.UseItem = "";

            dMeghdarJoz = 0;
            if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                dMeghdarJoz = 0;
            else
                dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

            RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz, 2);

            _context.RizMetreUserses.Add(RizMetre);

            polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
            {
                PolVaAbroBarAvordId = PolVaAbroId,
                RizMetreId = RizMetre.ID,
                PolDetailType = PolDetailTypeEnum.قالب_بغل_دال_پیش_ساخته_در_جهت_عرضی
            };
            _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);


            /////////////
            RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = ++Shomareh;
            RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
            RizMetre.Sharh = "قالب طولی جانپناه";
            RizMetre.Tedad = 4;
            RizMetre.Tool = int.Parse(D.Trim()) + (2 * decimal.Parse(c1.Trim())) - (2 * decimal.Parse(j.Trim()));
            RizMetre.Arz = null;
            RizMetre.Ertefa = decimal.Parse("0.25");
            RizMetre.Vazn = null;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "300" + PolNum.ToString("D3") + "52";///قالب طولی جانپناه
            RizMetre.ForItem = "";
            RizMetre.UseItem = "";

            dMeghdarJoz = 0;
            if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                dMeghdarJoz = 0;
            else
                dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

            RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz, 2);

            _context.RizMetreUserses.Add(RizMetre);

            polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
            {
                PolVaAbroBarAvordId = PolVaAbroId,
                RizMetreId = RizMetre.ID,
                PolDetailType = PolDetailTypeEnum.قالب_طولی_جانپناه
            };
            _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);


            /////////////
            RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = ++Shomareh;
            RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
            RizMetre.Sharh = "قالب عرضی جانپناه";
            RizMetre.Tedad = 4;
            RizMetre.Tool = null;
            RizMetre.Arz = decimal.Parse("0.35");
            RizMetre.Ertefa = decimal.Parse("0.25");
            RizMetre.Vazn = null;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "300" + PolNum.ToString("D3") + "52";///قالب عرضی جانپناه
            RizMetre.ForItem = "";
            RizMetre.UseItem = "";

            dMeghdarJoz = 0;
            if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                dMeghdarJoz = 0;
            else
                dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

            RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz, 2);

            _context.RizMetreUserses.Add(RizMetre);

            polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
            {
                PolVaAbroBarAvordId = PolVaAbroId,
                RizMetreId = RizMetre.ID,
                PolDetailType = PolDetailTypeEnum.قالب_عرضی_جانپناه
            };
            _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
            //_context.SaveChanges();

            //RizMetre.Save();
            //////////

            varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "080702" && x.NoeFBId == NoeFBId);
            //DtFBUser = clsConvert.ToDataTable(varFBUser);

            guFBId = new Guid();
            //if (DtFBUser.Rows.Count == 0)
            if (varFBUser == null)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "080702";
                //FBSave.BahayeVahedZarib = 0;
                FBSave.NoeFBId = NoeFBId;
                _context.FBs.Add(FBSave);
                //_context.SaveChanges();
                guFBId = FBSave.ID;
            }
            else
                guFBId = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
            ////////////////
            //varDtLastRizMetreUsersesShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).ToList();
            //DtLastRizMetreUsersesShomareh = clsConvert.ToDataTable(varDtLastRizMetreUsersesShomareh);
            //Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());

            RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            Shomareh = 0;
            if (RizMetreUserLastShomareh != null)
                Shomareh = RizMetreUserLastShomareh.Shomareh + 1;
            else
                Shomareh = 1;

            ////////////////قالب دال پیش ساخته
            //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId);
            //Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());

            //DataTable DtSumOfToolPolVaAbro = tblPolVaAbroBarAvord.SumOfToolPolVaAbroBarAvordListWithParameter("BarAvordId='" + BarAvordId + "'");

            string strParam = "BarAvordId='" + BarAvordId + "'";
            var PolVaAbroParam = new SqlParameter("@Parameter", strParam);

            var SumOfToolPolVaAbro = _context.Set<SumOfToolPolVaAbroBarAvordDto>()
                .FromSqlRaw("EXEC SumOfToolPolVaAbroBarAvordListWithParameter @Parameter", PolVaAbroParam)
                .ToList();

            DataTable DtSumOfToolPolVaAbro = clsConvert.ToDataTable(SumOfToolPolVaAbro);

            DataRow[] DrSumOfToolPolVaAbro = DtSumOfToolPolVaAbro.Select("ToolAbroWithBie>24");
            for (int i = 0; i < DrSumOfToolPolVaAbro.Length; i++)
            {
                decimal dSumwith24 = 0;
                decimal dToolAbroWithBie = decimal.Parse(DrSumOfToolPolVaAbro[i]["ToolAbroWithBie"].ToString());
                decimal dFloatOfToolAbroWithBie = dToolAbroWithBie - Math.Truncate(dToolAbroWithBie);
                int intOfToolAbroWithBie = int.Parse(Math.Truncate(dToolAbroWithBie).ToString());
                if (dFloatOfToolAbroWithBie != 0)
                {
                    dSumwith24 += dToolAbroWithBie - 24;
                }
                for (int count = 25; count <= intOfToolAbroWithBie; count++)
                {
                    dSumwith24 += (count - 24);
                }
                /////////
                RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = Shomareh;
                RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                RizMetre.Sharh = "قالب دال پیش ساخته";
                RizMetre.Tedad = int.Parse(TedadDahaneh.Trim()) * dSumwith24;
                RizMetre.Tool = decimal.Parse(LAbro.Trim());
                RizMetre.Arz = int.Parse(D.Trim()) + (2 * decimal.Parse(c1.Trim())) - (2 * decimal.Parse(j.Trim()));
                RizMetre.Ertefa = null;
                RizMetre.Vazn = null;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId;
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "300" + PolNum.ToString("D3") + "52";///قالب دال پیش ساخته
                RizMetre.ForItem = "";
                RizMetre.UseItem = "";

                dMeghdarJoz = 0;
                if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                    dMeghdarJoz = 0;
                else
                    dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                    (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz, 2);

                _context.RizMetreUserses.Add(RizMetre);

                polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                {
                    PolVaAbroBarAvordId = PolVaAbroId,
                    RizMetreId = RizMetre.ID,
                    PolDetailType = PolDetailTypeEnum.قالب_دال_پیش_ساخته
                };
                _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);

                //_context.SaveChanges();
                //RizMetre.Save();
                ///////
                RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = ++Shomareh;
                RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                RizMetre.Sharh = "قالب دال پیش ساخته";
                RizMetre.Tedad = int.Parse(TedadDahaneh.Trim()) * dSumwith24;
                RizMetre.Tool = decimal.Parse(LAbro.Trim());
                RizMetre.Arz = null;
                RizMetre.Ertefa = decimal.Parse(t.Trim());
                RizMetre.Vazn = null;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId;
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "300" + PolNum.ToString("D3") + "52";///قالب دال پیش ساخته
                RizMetre.ForItem = "";
                RizMetre.UseItem = "";

                dMeghdarJoz = 0;
                if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                    dMeghdarJoz = 0;
                else
                    dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                    (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz, 2);

                _context.RizMetreUserses.Add(RizMetre);

                polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                {
                    PolVaAbroBarAvordId = PolVaAbroId,
                    RizMetreId = RizMetre.ID,
                    PolDetailType = PolDetailTypeEnum.قالب_دال_پیش_ساخته
                };
                _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);

                //_context.SaveChanges();
                //RizMetre.Save();
                /////////////
                RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = ++Shomareh;
                RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                RizMetre.Sharh = "قالب دال پیش ساخته";
                RizMetre.Tedad = int.Parse(TedadDahaneh.Trim()) * dSumwith24;
                RizMetre.Tool = 2 * decimal.Parse(LAbro.Trim());
                RizMetre.Arz = int.Parse(D.Trim()) + (2 * decimal.Parse(c1.Trim())) - (2 * decimal.Parse(j.Trim()));
                RizMetre.Ertefa = decimal.Parse(t.Trim());
                RizMetre.Vazn = null;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId;
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "300" + PolNum.ToString("D3") + "52";///قالب دال پیش ساخته
                RizMetre.ForItem = "";
                RizMetre.UseItem = "";

                dMeghdarJoz = 0;
                if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                    dMeghdarJoz = 0;
                else
                    dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                    (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz, 2);

                _context.RizMetreUserses.Add(RizMetre);

                polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                {
                    PolVaAbroBarAvordId = PolVaAbroId,
                    RizMetreId = RizMetre.ID,
                    PolDetailType = PolDetailTypeEnum.قالب_دال_پیش_ساخته
                };
                _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);

                //_context.SaveChanges();
                //RizMetre.Save();
            }

            return true;
        }
        catch (System.Exception)
        {
            return false;
        }
    }

}
