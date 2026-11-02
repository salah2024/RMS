using RMS.Models.Entity;
using System.Data;
using static RMS.Models.Common.AbnieFaniEnum;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Common;

public class AbnieFaniCommonGhalebBandiChenaj(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public bool GhalebBandiChenaj(Guid BarAvordId, NoeFehrestBaha NoeFBId, Guid PolVaAbroId, int PolNum, string LAbro, string w1, string w2, string w3, string w4,
        string f, string m, string n, string k, string h, string t, string b2, string c2, string p2, string D,
            string TedadDahaneh)
    {
        /////////////////
        int countWIsZero = 0;
        countWIsZero = decimal.Parse(w1.Trim()) == 0 ? countWIsZero++ : countWIsZero;
        countWIsZero = decimal.Parse(w2.Trim()) == 0 ? countWIsZero++ : countWIsZero;
        countWIsZero = decimal.Parse(w3.Trim()) == 0 ? countWIsZero++ : countWIsZero;
        countWIsZero = decimal.Parse(w4.Trim()) == 0 ? countWIsZero++ : countWIsZero;
        ////////////////
        string[,] arrItemsForInsert = { { "2", "3", "5", "7", "10" },
                                      { "080201", "080202", "080203", "080204", "080205" } };
        int intLength = arrItemsForInsert.Length / 2;
        for (int count = 0; count < intLength; count++)
        {
            bool blnFirstCon = false;
            if (count == 0)
                blnFirstCon = decimal.Parse(h.Trim()) + decimal.Parse(t.Trim()) <= decimal.Parse(arrItemsForInsert[0, count]);
            else
                blnFirstCon = decimal.Parse(h.Trim()) + decimal.Parse(t.Trim()) <= decimal.Parse(arrItemsForInsert[0, count])
                    && decimal.Parse(h.Trim()) - decimal.Parse("0.3") > decimal.Parse(arrItemsForInsert[0, count - 1]);

            bool blnSecondCon = decimal.Parse(h.Trim()) - decimal.Parse("0.3") <= decimal.Parse(arrItemsForInsert[0, count])
                && decimal.Parse(h.Trim()) + decimal.Parse(t.Trim()) > decimal.Parse(arrItemsForInsert[0, count]);

            //bool blnThirdCon = decimal.Parse(h.Trim()) + decimal.Parse(t.Trim()) > decimal.Parse(arrItemsForInsert[0, count]);

            //////
            //////0<H+t<=2
            //////
            if (blnFirstCon)
            {
                string strCurrentShomareh = arrItemsForInsert[1, count];
                clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strCurrentShomareh && x.NoeFBId == NoeFBId);

                Guid guFBId = new Guid();
                //if (DtFBUser.Rows.Count == 0)
                if (varFBUser == null)
                {
                    clsFB FBSave = new clsFB();
                    FBSave.BarAvordId = BarAvordId;
                    FBSave.Shomareh = strCurrentShomareh;
                    //FBSave.BahayeVahedZarib = 0;
                    FBSave.NoeFBId = NoeFBId;
                    _context.FBs.Add(FBSave);
                    //_context.SaveChanges();
                    guFBId = FBSave.ID;

                    //FBId = OperationItemsFB.SaveFB(BarAvordID, arrItemsForInsert[1, count], 0);
                }
                else
                    guFBId = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                ////////////////قالب بندی طولی شناژ کوله
                clsRizMetreUsers? RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                long Shomareh = 0;
                if (RizMetreUserLastShomareh != null)
                    Shomareh = RizMetreUserLastShomareh.Shomareh + 1;
                else
                    Shomareh = 1;
                //DataTable DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId);
                //int Shomareh = lastShomareh;// int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = Shomareh;
                RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                RizMetre.Sharh = "قالب بندی طولی شناژ کوله";
                RizMetre.Tedad = 4;
                RizMetre.Tool = decimal.Parse(LAbro.Trim());
                RizMetre.Arz = null;
                RizMetre.Ertefa = decimal.Parse(t.Trim()) + decimal.Parse("0.3");
                RizMetre.Vazn = null;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId;
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "300" + PolNum.ToString("D3") + "10"; ///قالب بندی طولی شناژ کوله
                RizMetre.ForItem = "";
                RizMetre.UseItem = "";

                decimal dMeghdarJoz = 0;
                if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                    dMeghdarJoz = 0;
                else
                    dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                    (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz, 3);

                _context.RizMetreUserses.Add(RizMetre);

                clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                {
                    PolVaAbroBarAvordId = PolVaAbroId,
                    RizMetreId = RizMetre.ID,
                    PolDetailType = PolDetailTypeEnum.قالب_بندی_طولی_شناژ_کوله
                };
                _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                //_context.SaveChanges();
                //RizMetre.Save();
                /////////////قالب بندی عرضی شناژ کوله
                int tedad = 4 - (countWIsZero);
                if (tedad != 0)
                {
                    RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = ++Shomareh;
                    RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                    RizMetre.Sharh = "قالب بندی عرضی شناژ کوله";
                    RizMetre.Tedad = tedad;
                    RizMetre.Tool = null;
                    RizMetre.Arz = decimal.Parse(b2.Trim());
                    RizMetre.Ertefa = decimal.Parse("0.3");
                    RizMetre.Vazn = null;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "300" + PolNum.ToString("D3") + "11"; ///قالب بندی عرضی شناژ کوله
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

                    polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                    {
                        PolVaAbroBarAvordId = PolVaAbroId,
                        RizMetreId = RizMetre.ID,
                        PolDetailType = PolDetailTypeEnum.قالب_بندی_عرضی_شناژ_کوله
                    };
                    _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                    //_context.SaveChanges();
                    //RizMetre.Save();
                    ////////////////////
                    RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = ++Shomareh;
                    RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                    RizMetre.Sharh = "قالب بندی عرضی شناژ کوله";
                    RizMetre.Tedad = tedad;
                    RizMetre.Tool = null;
                    RizMetre.Arz = decimal.Parse(c2.Trim());
                    RizMetre.Ertefa = decimal.Parse(t.Trim());
                    RizMetre.Vazn = null;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "300" + PolNum.ToString("D3") + "11";///قالب بندی عرضی شناژ کوله
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

                    polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                    {
                        PolVaAbroBarAvordId = PolVaAbroId,
                        RizMetreId = RizMetre.ID,
                        PolDetailType = PolDetailTypeEnum.قالب_بندی_عرضی_شناژ_کوله
                    };
                    _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                    //_context.SaveChanges();
                    //RizMetre.Save();
                }

                /////////////قالب بندی طولی شناژ پایه میانی
                tedad = 2 * (int.Parse(TedadDahaneh) - 1);
                if (tedad != 0)
                {
                    RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = ++Shomareh;
                    RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                    RizMetre.Sharh = "قالب بندی طولی شناژ پایه میانی";
                    RizMetre.Tedad = tedad;
                    RizMetre.Tool = decimal.Parse(LAbro.Trim());
                    RizMetre.Arz = null;
                    RizMetre.Ertefa = decimal.Parse("0.3");
                    RizMetre.Vazn = null;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "300" + PolNum.ToString("D3") + "12";///قالب بندی طولی شناژ پایه میانی
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    //RizMetre.Save();
                    dMeghdarJoz = 0;
                    if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                        dMeghdarJoz = 0;
                    else
                        dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                        (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                    RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz, 3);

                    _context.RizMetreUserses.Add(RizMetre);

                    polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                    {
                        PolVaAbroBarAvordId = PolVaAbroId,
                        RizMetreId = RizMetre.ID,
                        PolDetailType = PolDetailTypeEnum.قالب_بندی_طولی_شناژ_پایه_میانی
                    };
                    _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);


                    //_context.SaveChanges();
                    /////////////قالب بندی عرضی شناژ پایه میانی
                    tedad = 4 - (countWIsZero);
                    if (tedad != 0)
                    {
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = ++Shomareh;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = "قالب بندی عرضی شناژ پایه میانی";
                        RizMetre.Tedad = tedad;
                        RizMetre.Tool = null;
                        RizMetre.Arz = decimal.Parse(p2.Trim());
                        RizMetre.Ertefa = decimal.Parse("0.3");
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "13";///قالب بندی عرضی شناژ پایه میانی
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";

                        dMeghdarJoz = 0;
                        if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                            dMeghdarJoz = 0;
                        else
                            dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                            (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                        RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz, 3);

                        //RizMetre.Save();
                        _context.RizMetreUserses.Add(RizMetre);

                        polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                        {
                            PolVaAbroBarAvordId = PolVaAbroId,
                            RizMetreId = RizMetre.ID,
                            PolDetailType = PolDetailTypeEnum.قالب_بندی_عرضی_شناژ_پایه_میانی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                    }
                }
            }
            //////
            //////
            //////
            else if (blnSecondCon)
            {
                string strCurrentShomareh = arrItemsForInsert[1, count];
                clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strCurrentShomareh && x.NoeFBId == NoeFBId);

                Guid guFBId = new Guid();
                //if (DtFBUser.Rows.Count == 0)
                if (varFBUser == null)
                {
                    clsFB FBSave = new clsFB();
                    FBSave.BarAvordId = BarAvordId;
                    FBSave.Shomareh = strCurrentShomareh;
                    //FBSave.BahayeVahedZarib = 0;
                    FBSave.NoeFBId = NoeFBId;
                    _context.FBs.Add(FBSave);
                    //_context.SaveChanges();
                    guFBId = FBSave.ID;
                }
                else
                    guFBId = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                ////////////////قالب بندی طولی شناژ کوله

                clsRizMetreUsers? RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                long Shomareh = 0;
                if (RizMetreUserLastShomareh != null)
                    Shomareh = RizMetreUserLastShomareh.Shomareh + 1;
                else
                    Shomareh = 1;

                //int Shomareh = lastShomareh;// int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = Shomareh;
                RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                RizMetre.Sharh = "قالب بندی طولی شناژ کوله";
                RizMetre.Tedad = 4;
                RizMetre.Tool = decimal.Parse(LAbro.Trim());
                RizMetre.Arz = null;
                RizMetre.Ertefa = decimal.Parse(arrItemsForInsert[0, count]) - (decimal.Parse(h.Trim()) - decimal.Parse("0.3"));
                RizMetre.Vazn = null;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId;
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "300" + PolNum.ToString("D3") + "10";///قالب بندی طولی شناژ کوله
                RizMetre.ForItem = "";
                RizMetre.UseItem = "";

                decimal dMeghdarJoz = 0;
                if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                    dMeghdarJoz = 0;
                else
                    dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                    (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz, 3);

                _context.RizMetreUserses.Add(RizMetre);

                clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                {
                    PolVaAbroBarAvordId = PolVaAbroId,
                    RizMetreId = RizMetre.ID,
                    PolDetailType = PolDetailTypeEnum.قالب_بندی_طولی_شناژ_کوله
                };
                _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);

                string strCurrentShomareh1 = arrItemsForInsert[1, count + 1];
                clsFB? varFBUser1 = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strCurrentShomareh1 && x.NoeFBId == NoeFBId);
                //DtFBUser = clsConvert.ToDataTable(varFBUser1);
                guFBId = new Guid();
                //if (DtFBUser.Rows.Count == 0)
                if (varFBUser1 == null)
                {
                    clsFB FBSave = new clsFB();
                    FBSave.BarAvordId = BarAvordId;
                    FBSave.Shomareh = strCurrentShomareh1;
                    //FBSave.BahayeVahedZarib = 0;
                    FBSave.NoeFBId = NoeFBId;
                    _context.FBs.Add(FBSave);
                    //_context.SaveChanges();
                    guFBId = FBSave.ID;
                }
                else
                    guFBId = varFBUser1.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                ////////////////
                RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                long lastShomareh = 0;
                if (RizMetreUserLastShomareh != null)
                    lastShomareh = RizMetreUserLastShomareh.Shomareh + 1;
                else
                    lastShomareh = 1;

                //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId);
                //Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = lastShomareh;
                RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                RizMetre.Sharh = "قالب بندی طولی شناژ کوله";
                RizMetre.Tedad = 4;
                RizMetre.Tool = decimal.Parse(LAbro.Trim());
                RizMetre.Arz = null;
                RizMetre.Ertefa = (decimal.Parse("0.3") + decimal.Parse(t.Trim())) - (decimal.Parse(arrItemsForInsert[0, count]) - (decimal.Parse(h.Trim()) - decimal.Parse("0.3")));
                RizMetre.Vazn = null;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId;
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "300" + PolNum.ToString("D3") + "10";///قالب بندی طولی شناژ کوله
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

                polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                {
                    PolVaAbroBarAvordId = PolVaAbroId,
                    RizMetreId = RizMetre.ID,
                    PolDetailType = PolDetailTypeEnum.قالب_بندی_طولی_شناژ_کوله
                };
                _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);

                //_context.SaveChanges();
                //RizMetre.Save();

                //////////////قالب بندی عرضی شناژ کوله
                ///////
                ///////h-0.3<2<h
                ///////
                if (decimal.Parse(h.Trim()) > decimal.Parse(arrItemsForInsert[0, count])
                    && decimal.Parse(h.Trim()) - decimal.Parse("0.3") < decimal.Parse(arrItemsForInsert[0, count]))
                {
                    int tedad = 4 - countWIsZero;
                    if (tedad != 0)
                    {
                        strCurrentShomareh = arrItemsForInsert[1, count];
                        varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strCurrentShomareh && x.NoeFBId == NoeFBId);
                        //DtFBUser = clsConvert.ToDataTable(varFBUser);

                        guFBId = new Guid();
                        //if (DtFBUser.Rows.Count == 0)
                        if (varFBUser == null)
                        {
                            clsFB FBSave = new clsFB();
                            FBSave.BarAvordId = BarAvordId;
                            FBSave.Shomareh = strCurrentShomareh;
                            //FBSave.BahayeVahedZarib = 0;
                            FBSave.NoeFBId = NoeFBId;
                            _context.FBs.Add(FBSave);
                            //_context.SaveChanges();
                            guFBId = FBSave.ID;
                        }
                        else
                            guFBId = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                        ////////////////
                        RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                        lastShomareh = 0;
                        if (RizMetreUserLastShomareh != null)
                            lastShomareh = RizMetreUserLastShomareh.Shomareh + 1;
                        else
                            lastShomareh = 1;

                        //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId);
                        //Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = lastShomareh;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = "قالب بندی عرضی شناژ کوله";
                        RizMetre.Tedad = tedad;
                        RizMetre.Tool = null;
                        RizMetre.Arz = decimal.Parse(b2.Trim());
                        RizMetre.Ertefa = decimal.Parse(arrItemsForInsert[0, count]) - (decimal.Parse(h.Trim()) - decimal.Parse("0.3"));
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "11";///قالب بندی عرضی شناژ کوله
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

                        polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                        {
                            PolVaAbroBarAvordId = PolVaAbroId,
                            RizMetreId = RizMetre.ID,
                            PolDetailType = PolDetailTypeEnum.قالب_بندی_عرضی_شناژ_کوله
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();
                        ///////////////قسمت دوم

                        strCurrentShomareh1 = arrItemsForInsert[1, count + 1];
                        varFBUser1 = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strCurrentShomareh1 && x.NoeFBId == NoeFBId);
                        //DtFBUser = clsConvert.ToDataTable(varFBUser1);
                        guFBId = new Guid();
                        //if (DtFBUser.Rows.Count == 0)
                        if (varFBUser1 == null)
                        {
                            clsFB FBSave = new clsFB();
                            FBSave.BarAvordId = BarAvordId;
                            FBSave.Shomareh = strCurrentShomareh1;
                            //FBSave.BahayeVahedZarib = 0;
                            FBSave.NoeFBId = NoeFBId;
                            _context.FBs.Add(FBSave);
                            //_context.SaveChanges();
                            guFBId = FBSave.ID;
                        }
                        else
                            guFBId = varFBUser1.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                        ////////////////
                        RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                        lastShomareh = 0;
                        if (RizMetreUserLastShomareh != null)
                            lastShomareh = RizMetreUserLastShomareh.Shomareh + 1;
                        else
                            lastShomareh = 1;

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = lastShomareh;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = "قالب بندی عرضی شناژ کوله";
                        RizMetre.Tedad = tedad;
                        RizMetre.Tool = null;
                        RizMetre.Arz = decimal.Parse(b2.Trim());
                        RizMetre.Ertefa = decimal.Parse(h.Trim()) - decimal.Parse(arrItemsForInsert[0, count]);
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "11";///قالب بندی عرضی شناژ کوله
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
                        polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                        {
                            PolVaAbroBarAvordId = PolVaAbroId,
                            RizMetreId = RizMetre.ID,
                            PolDetailType = PolDetailTypeEnum.قالب_بندی_عرضی_شناژ_کوله
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //RizMetre.Save();
                        ////////
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = ++lastShomareh;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = "قالب بندی عرضی شناژ کوله";
                        RizMetre.Tedad = tedad;
                        RizMetre.Tool = null;
                        RizMetre.Arz = decimal.Parse(c2.Trim());
                        RizMetre.Ertefa = decimal.Parse(t.Trim());
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "11";///قالب بندی عرضی شناژ کوله
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
                        polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                        {
                            PolVaAbroBarAvordId = PolVaAbroId,
                            RizMetreId = RizMetre.ID,
                            PolDetailType = PolDetailTypeEnum.قالب_بندی_عرضی_شناژ_کوله
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);

                        //_context.SaveChanges();
                        //RizMetre.Save();
                    }

                    /////////////قالب بندی طولی شناژ پایه میانی
                    tedad = 2 * (int.Parse(TedadDahaneh) - 1);
                    if (tedad != 0)
                    {
                        strCurrentShomareh = arrItemsForInsert[1, count];
                        varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strCurrentShomareh && x.NoeFBId == NoeFBId);
                        //DtFBUser = clsConvert.ToDataTable(varFBUser);

                        guFBId = new Guid();
                        //if (DtFBUser.Rows.Count == 0)
                        if (varFBUser == null)
                        {
                            clsFB FBSave = new clsFB();
                            FBSave.BarAvordId = BarAvordId;
                            FBSave.Shomareh = strCurrentShomareh;
                            //FBSave.BahayeVahedZarib = 0;
                            FBSave.NoeFBId = NoeFBId;
                            _context.FBs.Add(FBSave);
                            //_context.SaveChanges();
                            guFBId = FBSave.ID;
                        }
                        else
                            guFBId = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
                        ////////////////
                        RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                        lastShomareh = 0;
                        if (RizMetreUserLastShomareh != null)
                            lastShomareh = RizMetreUserLastShomareh.Shomareh + 1;
                        else
                            lastShomareh = 1;

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = lastShomareh;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = "قالب بندی طولی شناژ پایه میانی";
                        RizMetre.Tedad = tedad;
                        RizMetre.Tool = decimal.Parse(LAbro.Trim());
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = decimal.Parse(arrItemsForInsert[0, count]) - (decimal.Parse(h.Trim()) - decimal.Parse("0.3"));
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "12";///قالب بندی طولی شناژ پایه میانی
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

                        polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                        {
                            PolVaAbroBarAvordId = PolVaAbroId,
                            RizMetreId = RizMetre.ID,
                            PolDetailType = PolDetailTypeEnum.قالب_بندی_طولی_شناژ_پایه_میانی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);

                        //_context.SaveChanges();
                        //RizMetre.Save();
                        //////////قالب بندی عرضی شناژ پایه میانی
                        int tedad2 = 4 - countWIsZero;
                        if (tedad2 != 0)
                        {
                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = ++lastShomareh;
                            RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                            RizMetre.Sharh = "قالب بندی عرضی شناژ پایه میانی";
                            RizMetre.Tedad = tedad2;
                            RizMetre.Tool = null;
                            RizMetre.Arz = decimal.Parse(p2.Trim());
                            RizMetre.Ertefa = decimal.Parse(arrItemsForInsert[0, count]) - (decimal.Parse(h.Trim()) - decimal.Parse("0.3"));
                            RizMetre.Vazn = null;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "300" + PolNum.ToString("D3") + "13";///قالب بندی عرضی شناژ پایه میانی
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

                            polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                            {
                                PolVaAbroBarAvordId = PolVaAbroId,
                                RizMetreId = RizMetre.ID,
                                PolDetailType = PolDetailTypeEnum.قالب_بندی_عرضی_شناژ_پایه_میانی
                            };
                            _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                            //_context.SaveChanges();
                            //RizMetre.Save();
                        }
                        ////////
                        /////////قسمت دوم

                        strCurrentShomareh = arrItemsForInsert[1, count + 1];
                        varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strCurrentShomareh && x.NoeFBId == NoeFBId);
                        //DtFBUser = clsConvert.ToDataTable(varFBUser);

                        guFBId = new Guid();
                        //if (DtFBUser.Rows.Count == 0)
                        if (varFBUser == null)
                        {
                            clsFB FBSave = new clsFB();
                            FBSave.BarAvordId = BarAvordId;
                            FBSave.Shomareh = strCurrentShomareh;
                            //FBSave.BahayeVahedZarib = 0;
                            FBSave.NoeFBId = NoeFBId;
                            _context.FBs.Add(FBSave);
                            //_context.SaveChanges();
                            guFBId = FBSave.ID;
                        }
                        else
                            guFBId = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
                        ////////////////
                        RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                        lastShomareh = 0;
                        if (RizMetreUserLastShomareh != null)
                            lastShomareh = RizMetreUserLastShomareh.Shomareh + 1;
                        else
                            lastShomareh = 1;

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = lastShomareh;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = "قالب بندی طولی شناژ پایه میانی";
                        RizMetre.Tedad = tedad;
                        RizMetre.Tool = decimal.Parse(LAbro.Trim());
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = decimal.Parse("0.3") - (decimal.Parse(arrItemsForInsert[0, count]) - (decimal.Parse(h.Trim()) - decimal.Parse("0.3")));
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "12";///قالب بندی طولی شناژ پایه میانی
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

                        polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                        {
                            PolVaAbroBarAvordId = PolVaAbroId,
                            RizMetreId = RizMetre.ID,
                            PolDetailType = PolDetailTypeEnum.قالب_بندی_طولی_شناژ_پایه_میانی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();
                        ///////////////قالب بندی عرضی شناژ پایه میانی
                        tedad2 = 4 - countWIsZero;
                        if (tedad2 != 0)
                        {
                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = ++lastShomareh;
                            RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                            RizMetre.Sharh = "قالب بندی عرضی شناژ پایه میانی";
                            RizMetre.Tedad = tedad2;
                            RizMetre.Tool = null;
                            RizMetre.Arz = decimal.Parse(p2.Trim());
                            RizMetre.Ertefa = decimal.Parse("0.3") - (decimal.Parse(arrItemsForInsert[0, count]) - (decimal.Parse(h.Trim()) - decimal.Parse("0.3")));
                            RizMetre.Vazn = null;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "300" + PolNum.ToString("D3") + "13";///قالب بندی عرضی شناژ پایه میانی
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

                            polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                            {
                                PolVaAbroBarAvordId = PolVaAbroId,
                                RizMetreId = RizMetre.ID,
                                PolDetailType = PolDetailTypeEnum.قالب_بندی_عرضی_شناژ_پایه_میانی
                            };
                            _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);

                            //_context.SaveChanges();
                            //RizMetre.Save();
                        }
                    }
                }
                ///////
                ///////h<=2<h+t
                ///////
                else if (decimal.Parse(h.Trim()) <= decimal.Parse(arrItemsForInsert[0, count])
                    && decimal.Parse(h.Trim()) + decimal.Parse(t.Trim()) > decimal.Parse(arrItemsForInsert[0, count]))
                {
                    int tedad = 4 - countWIsZero;
                    if (tedad != 0)
                    {
                        strCurrentShomareh = arrItemsForInsert[1, count];
                        varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strCurrentShomareh && x.NoeFBId == NoeFBId);
                        //DtFBUser = clsConvert.ToDataTable(varFBUser);

                        guFBId = new Guid();
                        //if (DtFBUser.Rows.Count == 0)
                        if (varFBUser == null)
                        {
                            clsFB FBSave = new clsFB();
                            FBSave.BarAvordId = BarAvordId;
                            FBSave.Shomareh = strCurrentShomareh;
                            //FBSave.BahayeVahedZarib = 0;
                            FBSave.NoeFBId = NoeFBId;
                            _context.FBs.Add(FBSave);
                            //_context.SaveChanges();
                            guFBId = FBSave.ID;
                        }
                        else
                            guFBId = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
                        ////////////////

                        RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                        lastShomareh = 0;
                        if (RizMetreUserLastShomareh != null)
                            lastShomareh = RizMetreUserLastShomareh.Shomareh + 1;
                        else
                            lastShomareh = 1;

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = lastShomareh;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = "قالب بندی عرضی شناژ کوله";
                        RizMetre.Tedad = tedad;
                        RizMetre.Tool = null;
                        RizMetre.Arz = decimal.Parse(b2.Trim());
                        RizMetre.Ertefa = decimal.Parse("0.3");
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "11";///قالب بندی عرضی شناژ کوله
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

                        polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                        {
                            PolVaAbroBarAvordId = PolVaAbroId,
                            RizMetreId = RizMetre.ID,
                            PolDetailType = PolDetailTypeEnum.قالب_بندی_عرضی_شناژ_کوله
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();
                        ///////////////قسمت دوم

                        strCurrentShomareh = arrItemsForInsert[1, count + 1];
                        varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strCurrentShomareh && x.NoeFBId == NoeFBId);
                        //DtFBUser = clsConvert.ToDataTable(varFBUser);

                        guFBId = new Guid();
                        //if (DtFBUser.Rows.Count == 0)
                        if (varFBUser == null)
                        {
                            clsFB FBSave = new clsFB();
                            FBSave.BarAvordId = BarAvordId;
                            FBSave.Shomareh = strCurrentShomareh;
                            //FBSave.BahayeVahedZarib = 0;
                            FBSave.NoeFBId = NoeFBId;
                            _context.FBs.Add(FBSave);
                            //_context.SaveChanges();
                            guFBId = FBSave.ID;
                        }
                        else
                            guFBId = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
                        ////////////////
                        RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                        lastShomareh = 0;
                        if (RizMetreUserLastShomareh != null)
                            lastShomareh = RizMetreUserLastShomareh.Shomareh + 1;
                        else
                            lastShomareh = 1;

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = lastShomareh;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = "قالب بندی عرضی شناژ کوله";
                        RizMetre.Tedad = tedad;
                        RizMetre.Tool = null;
                        RizMetre.Arz = decimal.Parse(c2.Trim());
                        RizMetre.Ertefa = decimal.Parse(arrItemsForInsert[0, count]) - decimal.Parse(h.Trim());
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "11";///قالب بندی عرضی شناژ کوله
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

                        polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                        {
                            PolVaAbroBarAvordId = PolVaAbroId,
                            RizMetreId = RizMetre.ID,
                            PolDetailType = PolDetailTypeEnum.قالب_بندی_عرضی_شناژ_کوله
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();
                        ////////
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = ++lastShomareh;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = "قالب بندی عرضی شناژ کوله";
                        RizMetre.Tedad = tedad;
                        RizMetre.Tool = null;
                        RizMetre.Arz = decimal.Parse(c2.Trim());
                        RizMetre.Ertefa = decimal.Parse(t.Trim()) - (decimal.Parse(arrItemsForInsert[0, count]) - decimal.Parse(h.Trim()));
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "11";///قالب بندی عرضی شناژ کوله
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

                        polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                        {
                            PolVaAbroBarAvordId = PolVaAbroId,
                            RizMetreId = RizMetre.ID,
                            PolDetailType = PolDetailTypeEnum.قالب_بندی_عرضی_شناژ_کوله
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();
                    }

                    ////////////قالب بندی طولی شناژ پایه میانی
                    tedad = 2 * (int.Parse(TedadDahaneh) - 1);
                    if (tedad != 0)
                    {
                        strCurrentShomareh = arrItemsForInsert[1, count];
                        varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strCurrentShomareh && x.NoeFBId == NoeFBId);
                        //DtFBUser = clsConvert.ToDataTable(varFBUser);

                        guFBId = new Guid();
                        //if (DtFBUser.Rows.Count == 0)
                        if (varFBUser == null)
                        {
                            clsFB FBSave = new clsFB();
                            FBSave.BarAvordId = BarAvordId;
                            FBSave.Shomareh = strCurrentShomareh;
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
                        lastShomareh = 0;
                        if (RizMetreUserLastShomareh != null)
                            lastShomareh = RizMetreUserLastShomareh.Shomareh + 1;
                        else
                            lastShomareh = 1;

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = lastShomareh;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = "قالب بندی طولی شناژ پایه میانی";
                        RizMetre.Tedad = tedad;
                        RizMetre.Tool = decimal.Parse(LAbro.Trim());
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = decimal.Parse("0.3");
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "12";///قالب بندی طولی شناژ پایه میانی
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

                        polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                        {
                            PolVaAbroBarAvordId = PolVaAbroId,
                            RizMetreId = RizMetre.ID,
                            PolDetailType = PolDetailTypeEnum.قالب_بندی_طولی_شناژ_پایه_میانی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();
                        //////قالب بندی عرضی شناژ پایه میانی
                        int tedad2 = 4 - countWIsZero;
                        if (tedad2 != 0)
                        {
                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = ++lastShomareh;
                            RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                            RizMetre.Sharh = "قالب بندی عرضی شناژ پایه میانی";
                            RizMetre.Tedad = tedad;
                            RizMetre.Tool = null;
                            RizMetre.Arz = decimal.Parse(p2.Trim());
                            RizMetre.Ertefa = decimal.Parse("0.3");
                            RizMetre.Vazn = null;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "300" + PolNum.ToString("D3") + "13";///قالب بندی عرضی شناژ پایه میانی
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

                            polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                            {
                                PolVaAbroBarAvordId = PolVaAbroId,
                                RizMetreId = RizMetre.ID,
                                PolDetailType = PolDetailTypeEnum.قالب_بندی_عرضی_شناژ_پایه_میانی
                            };
                            _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                            //_context.SaveChanges();
                            //RizMetre.Save();
                        }
                    }
                }
            }
        }
        ////////////////
        return true;
    }

}
