using RMS.Models.Entity;
using System.Data;
using static RMS.Models.Common.AbnieFaniEnum;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Common;

public class AbnieFaniCommonGhalebBandiDalDarja(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public bool GhalebBandiDalDarja(Guid BarAvordId, NoeFehrestBaha NoeFBId, Guid PolVaAbroId, int PolNum, string LAbro, string w1, string w2, string w3, string w4,
        string t, string j, string c1, string D, string TedadDahaneh)
    {
        try
        {
            int countWIsZero = 0;
            countWIsZero = decimal.Parse(w1.Trim()) == 0 ? countWIsZero++ : countWIsZero;
            countWIsZero = decimal.Parse(w2.Trim()) == 0 ? countWIsZero++ : countWIsZero;
            countWIsZero = decimal.Parse(w3.Trim()) == 0 ? countWIsZero++ : countWIsZero;
            countWIsZero = decimal.Parse(w4.Trim()) == 0 ? countWIsZero++ : countWIsZero;

            DataTable DtFBUser = null;

            // clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
            Guid guFBId = new Guid();
            DataTable DtLastRizMetreUsersesShomareh = null;
            long Shomareh = 0;
            //clsRizMetreUsersses RizMetre = new clsRizMetreUsersses();
            decimal dMeghdarJoz = 0;
            if (int.Parse(D.Trim()) <= 5)
            {
                clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "080301" && x.NoeFBId == NoeFBId);
                //DtFBUser = clsConvert.ToDataTable(varFBUser);

                guFBId = new Guid();
                //if (DtFBUser.Rows.Count == 0)
                if (varFBUser == null)
                {
                    clsFB FBSave = new clsFB();
                    FBSave.BarAvordId = BarAvordId;
                    FBSave.Shomareh = "080301";
                    //FBSave.BahayeVahedZarib = 0;
                    FBSave.NoeFBId = NoeFBId;
                    _context.FBs.Add(FBSave);
                    //_context.SaveChanges();
                    guFBId = FBSave.ID;
                }
                else
                    guFBId = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
                ////////////////
                var RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                if (RizMetreUserLastShomareh != null)
                    Shomareh = RizMetreUserLastShomareh.Shomareh + 1;
                else
                    Shomareh = 1;


                ////////////////قالب زیر دال
                //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId);
                //Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = Shomareh;
                RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                RizMetre.Sharh = "قالب زیر دال";
                RizMetre.Tedad = int.Parse(TedadDahaneh.Trim());
                RizMetre.Tool = decimal.Parse(LAbro.Trim());
                RizMetre.Arz = int.Parse(D.Trim());
                RizMetre.Ertefa = null;
                RizMetre.Vazn = null;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId;
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "300" + PolNum.ToString("D3") + "20";///قالب دال
                RizMetre.ForItem = "";
                RizMetre.UseItem = "";

                dMeghdarJoz = 0;
                if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                    dMeghdarJoz = 0;
                else
                    dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                    (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz,3);

                _context.RizMetreUserses.Add(RizMetre);

                clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre2 = new clsPolVaAbroBarAvordRizMetre
                {
                    PolVaAbroBarAvordId = PolVaAbroId,
                    RizMetreId = RizMetre.ID,
                    PolDetailType = PolDetailTypeEnum.قالب_زیر_دال
                };
                _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre2);
                //_context.SaveChanges();
                //RizMetre.Save();
                /////////////قالب نمای دال
                int tedad = int.Parse(TedadDahaneh) * (2 - (countWIsZero / 2));
                if (tedad != 0)
                {
                    RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = ++Shomareh;
                    RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                    RizMetre.Sharh = "قالب نمای دال";
                    RizMetre.Tedad = tedad;
                    RizMetre.Tool = null;
                    RizMetre.Arz = int.Parse(D.Trim()) + 2 * (decimal.Parse(c1)) - 2 * (decimal.Parse(j));
                    RizMetre.Ertefa = decimal.Parse(t.Trim());
                    RizMetre.Vazn = null;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "300" + PolNum.ToString("D3") + "20";///قالب دال
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";

                    dMeghdarJoz = 0;
                    if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                        dMeghdarJoz = 0;
                    else
                        dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                        (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                    RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz,3);

                    _context.RizMetreUserses.Add(RizMetre);

                    polVaAbroBarAvordRizMetre2 = new clsPolVaAbroBarAvordRizMetre
                    {
                        PolVaAbroBarAvordId = PolVaAbroId,
                        RizMetreId = RizMetre.ID,
                        PolDetailType = PolDetailTypeEnum.قالب_نمای_دال
                    };
                    _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre2);
                    //_context.SaveChanges();
                    //RizMetre.Save();
                }

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

                RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz, 3);

                _context.RizMetreUserses.Add(RizMetre);

                clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre3 = new clsPolVaAbroBarAvordRizMetre
                {
                    PolVaAbroBarAvordId = PolVaAbroId,
                    RizMetreId = RizMetre.ID,
                    PolDetailType = PolDetailTypeEnum.قالب_طولی_جانپناه
                };
                _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre3);


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

                RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz, 3);

                _context.RizMetreUserses.Add(RizMetre);

                clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre4 = new clsPolVaAbroBarAvordRizMetre
                {
                    PolVaAbroBarAvordId = PolVaAbroId,
                    RizMetreId = RizMetre.ID,
                    PolDetailType = PolDetailTypeEnum.قالب_عرضی_جانپناه
                };
                _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre4);
            }
            else if (int.Parse(D.Trim()) > 5 && int.Parse(D.Trim()) <= 10)
            {

                clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "080302" && x.NoeFBId == NoeFBId);
                //DtFBUser = clsConvert.ToDataTable(varFBUser);

                guFBId = new Guid();
                //if (DtFBUser.Rows.Count == 0)
                if (varFBUser == null)
                {
                    clsFB FBSave = new clsFB();
                    FBSave.BarAvordId = BarAvordId;
                    FBSave.Shomareh = "080302";
                    //FBSave.BahayeVahedZarib = 0;
                    FBSave.NoeFBId = NoeFBId;
                    _context.FBs.Add(FBSave);
                    //_context.SaveChanges();
                    guFBId = FBSave.ID;
                }
                else
                    guFBId = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
                ////////////////

                var RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                Shomareh = 0;
                if (RizMetreUserLastShomareh != null)
                    Shomareh = RizMetreUserLastShomareh.Shomareh + 1;
                else
                    Shomareh = 1;


                //////////////////قالب زیر دال
                clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = Shomareh;
                RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                RizMetre.Sharh = "قالب زیر دال";
                RizMetre.Tedad = int.Parse(TedadDahaneh.Trim());
                RizMetre.Tool = decimal.Parse(LAbro.Trim());
                RizMetre.Arz = int.Parse(D.Trim());
                RizMetre.Ertefa = null;
                RizMetre.Vazn = null;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId;
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "300" + PolNum.ToString("D3") + "20";///قالب زیر دال
                RizMetre.ForItem = "";
                RizMetre.UseItem = "";

                dMeghdarJoz = 0;
                if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                    dMeghdarJoz = 0;
                else
                    dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                    (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz,3);

                _context.RizMetreUserses.Add(RizMetre);

                clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre1 = new clsPolVaAbroBarAvordRizMetre
                {
                    PolVaAbroBarAvordId = PolVaAbroId,
                    RizMetreId = RizMetre.ID,
                    PolDetailType = PolDetailTypeEnum.قالب_زیر_دال
                };
                _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre1);
                //_context.SaveChanges();
                //RizMetre.Save();
                /////////////قالب نمای دال
                int tedad = int.Parse(TedadDahaneh) * (2 - (countWIsZero / 2));
                if (tedad != 0)
                {
                    RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = ++Shomareh;
                    RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                    RizMetre.Sharh = "قالب نمای دال";
                    RizMetre.Tedad = tedad;
                    RizMetre.Tool = null;
                    RizMetre.Arz = int.Parse(D.Trim()) + 2 * (decimal.Parse(c1)) - 2 * (decimal.Parse(j));
                    RizMetre.Ertefa = null;
                    RizMetre.Vazn = null;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "300" + PolNum.ToString("D3") + "21";///قالب نمای دال
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";

                    dMeghdarJoz = 0;
                    if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                        dMeghdarJoz = 0;
                    else
                        dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                        (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                    RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz,3);

                    _context.RizMetreUserses.Add(RizMetre);

                    polVaAbroBarAvordRizMetre1 = new clsPolVaAbroBarAvordRizMetre
                    {
                        PolVaAbroBarAvordId = PolVaAbroId,
                        RizMetreId = RizMetre.ID,
                        PolDetailType = PolDetailTypeEnum.قالب_نمای_دال
                    };
                    _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre1);

                    //_context.SaveChanges();
                    //RizMetre.Save();
                }

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

                RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz, 3);

                _context.RizMetreUserses.Add(RizMetre);

                clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre3 = new clsPolVaAbroBarAvordRizMetre
                {
                    PolVaAbroBarAvordId = PolVaAbroId,
                    RizMetreId = RizMetre.ID,
                    PolDetailType = PolDetailTypeEnum.قالب_طولی_جانپناه
                };
                _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre3);


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

                RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz, 3);

                _context.RizMetreUserses.Add(RizMetre);

                clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre4 = new clsPolVaAbroBarAvordRizMetre
                {
                    PolVaAbroBarAvordId = PolVaAbroId,
                    RizMetreId = RizMetre.ID,
                    PolDetailType = PolDetailTypeEnum.قالب_عرضی_جانپناه
                };
                _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre4);
            }


            clsFB? varFBUser1 = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "080501" && x.NoeFBId == NoeFBId);
            //DtFBUser = clsConvert.ToDataTable(varFBUser1);

            guFBId = new Guid();
            //if (DtFBUser.Rows.Count == 0)
            if (varFBUser1 == null)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "080501";
                //FBSave.BahayeVahedZarib = 0;
                FBSave.NoeFBId = NoeFBId;
                _context.FBs.Add(FBSave);
                //_context.SaveChanges();
                guFBId = FBSave.ID;
            }
            else
                guFBId = varFBUser1.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
            ////////////////

            var RizMetreUserLastShomareh1 = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            Shomareh = 0;
            if (RizMetreUserLastShomareh1 != null)
                Shomareh = RizMetreUserLastShomareh1.Shomareh + 1;
            else
                Shomareh = 1;

            //////////////////قالب درز انبساط

            clsRizMetreUsers RizMetre1 = new clsRizMetreUsers();
            RizMetre1.Shomareh = Shomareh;
            RizMetre1.ShomarehNew = RizMetre1.Shomareh.ToString();
            RizMetre1.Sharh = "قالب درز انبساط";
            RizMetre1.Tedad = int.Parse(TedadDahaneh.Trim()) + 1;
            RizMetre1.Tool = decimal.Parse(LAbro.Trim()) * 10;
            RizMetre1.Arz = decimal.Parse(j.Trim()) * 10;
            RizMetre1.Ertefa = decimal.Parse(t.Trim()) * 10;
            RizMetre1.Vazn = null;
            RizMetre1.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre1.FBId = guFBId;
            RizMetre1.OperationsOfHamlId = 1;
            RizMetre1.Type = "300" + PolNum.ToString("D3") + "22";///قالب درز انبساط
            RizMetre1.ForItem = "";
            RizMetre1.UseItem = "";

            dMeghdarJoz = 0;
            if (RizMetre1.Tedad == null && RizMetre1.Tool == null && RizMetre1.Arz == null && RizMetre1.Ertefa == null && RizMetre1.Vazn == null)
                dMeghdarJoz = 0;
            else
                dMeghdarJoz += (RizMetre1.Tedad == null ? 1 : RizMetre1.Tedad.Value) * (RizMetre1.Tool == null ? 1 : RizMetre1.Tool.Value) *
                (RizMetre1.Arz == null ? 1 : RizMetre1.Arz.Value) * (RizMetre1.Ertefa == null ? 1 : RizMetre1.Ertefa.Value) * (RizMetre1.Vazn == null ? 1 : RizMetre1.Vazn.Value);

            RizMetre1.MeghdarJoz = dMeghdarJoz;

            _context.RizMetreUserses.Add(RizMetre1);

            clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
            {
                PolVaAbroBarAvordId = PolVaAbroId,
                RizMetreId = RizMetre1.ID,
                PolDetailType = PolDetailTypeEnum.قالب_درز_انبساط
            };
            _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
            //_context.SaveChanges();
            //RizMetre.Save();

            return true;
        }
        catch (System.Exception)
        {
            return false;
        }
    }

}
