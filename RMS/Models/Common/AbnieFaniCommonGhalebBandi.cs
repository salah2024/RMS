using Microsoft.EntityFrameworkCore;
using RMS.Controllers.Operation.Common;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Entity;
using System.Data;
using static RMS.Models.Common.AbnieFaniEnum;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Common;

public class AbnieFaniCommonGhalebBandi(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public bool GhalebBandi(Guid BarAvordId, NoeFehrestBaha NoeFBId, Guid PolVaAbroId, int PolNum, string LAbro, string w1, string w2, string w3, string w4,
                            string f, string m, string n, string k, string h, string D, string TedadDahaneh, string LPayeMoarab, string LKooleMoarab, string b1,
                            string b2, string p1, string p2, int intNoeBanaii, int Year)
    {
        clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "080101" && x.NoeFBId == NoeFBId);
        //DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

        //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='080101'");
        //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
        //int FBId = 0;
        //if (DtFBUser.Rows.Count == 0)
        //    FBId = OperationItemsFB.SaveFB(BarAvordID, "080101", 0);
        //else
        //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

        Guid guFBId = new Guid();
        //if (DtFBUser.Rows.Count == 0)
        if (varFBUser == null)
        {
            clsFB FBSave = new clsFB();
            FBSave.BarAvordId = BarAvordId;
            FBSave.Shomareh = "080101";
            //FBSave.BahayeVahedZarib = 0;
            FBSave.NoeFBId = NoeFBId;
            _context.FBs.Add(FBSave);
            //_context.SaveChanges();
            guFBId = FBSave.ID;
        }
        else
            guFBId = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
        ////////////////
        clsRizMetreUsers? RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
        long Shomareh = 0;
        if (RizMetreUser != null)
            Shomareh = RizMetreUser.Shomareh + 1;
        else
            Shomareh = 1;

        //DataTable DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId);
        //int Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = Shomareh;
        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
        RizMetre.Sharh = "قالب بندی طولی پی کوله";
        RizMetre.Tedad = 4;
        RizMetre.Tool = decimal.Parse(LAbro.Trim());
        RizMetre.Arz = null;
        RizMetre.Ertefa = decimal.Parse(m.Trim());
        RizMetre.Vazn = null;
        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = guFBId;
        ////***
        //////****
        ///بایستی دستی پر نشود
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "00";///قالب بندی طولی پی کوله
        RizMetre.ForItem = "";
        RizMetre.UseItem = "";

        decimal dMeghdarJoz = 0;
        if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
            dMeghdarJoz = 0;
        else
            dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
            (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

        RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz, 2);

        _context.RizMetreUserses.Add(RizMetre);

        clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
        {
            PolVaAbroBarAvordId = PolVaAbroId,
            RizMetreId = RizMetre.ID,
            PolDetailType = PolDetailTypeEnum.قالب_بندی_طولی_پی_کوله,
        };
        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);

        //RizMetre.Save();
        /////////////////
        int countWIsZero = 0;
        countWIsZero = decimal.Parse(w1.Trim()) == 0 ? countWIsZero++ : countWIsZero;
        countWIsZero = decimal.Parse(w2.Trim()) == 0 ? countWIsZero++ : countWIsZero;
        countWIsZero = decimal.Parse(w3.Trim()) == 0 ? countWIsZero++ : countWIsZero;
        countWIsZero = decimal.Parse(w4.Trim()) == 0 ? countWIsZero++ : countWIsZero;
        ////////////////
        int tedad = 4 - (countWIsZero);
        if (tedad != 0)
        {
            RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = ++Shomareh;
            RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
            RizMetre.Sharh = "قالب بندی عرضی پی کوله (سرکله)";
            RizMetre.Tedad = tedad;
            RizMetre.Tool = null;
            RizMetre.Arz = decimal.Parse(f.Trim());
            RizMetre.Ertefa = decimal.Parse(m.Trim());
            RizMetre.Vazn = null;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId;
            ///************
            ///
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "300" + PolNum.ToString("D3") + "01";
            RizMetre.ForItem = "";
            RizMetre.UseItem = "";

            dMeghdarJoz = 0;
            if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                dMeghdarJoz = 0;
            else
                dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

            RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz, 2);

            _context.RizMetreUserses.Add(RizMetre);

            polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
            {
                PolVaAbroBarAvordId = PolVaAbroId,
                RizMetreId = RizMetre.ID,
                PolDetailType = PolDetailTypeEnum.قالب_بندی_عرضی_پی_کوله_سرکله,
            };
            _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);

            //RizMetre.Save();
        }
        ////////////////
        tedad = 2 * (int.Parse(TedadDahaneh) - 1);
        if (tedad != 0)
        {
            RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = ++Shomareh;
            RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
            RizMetre.Sharh = "قالب بندی طولی پی پایه میانی";
            RizMetre.Tedad = tedad;
            RizMetre.Tool = decimal.Parse(LAbro.Trim());
            RizMetre.Arz = null;
            RizMetre.Ertefa = decimal.Parse(n.Trim());
            RizMetre.Vazn = null;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId;
            //*********************
            ///
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "300" + PolNum.ToString("D3") + "02";
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
                PolDetailType = PolDetailTypeEnum.قالب_بندی_طولی_پی_پایه_میانی,
            };
            _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);

            //RizMetre.Save();
        }
        ////////////////
        tedad = 2 * (int.Parse(TedadDahaneh) - 1) - countWIsZero;
        if (tedad != 0)
        {
            RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = ++Shomareh;
            RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
            RizMetre.Sharh = "قالب بندی عرضی پی پایه میانی (سرکله)";
            RizMetre.Tedad = tedad;
            RizMetre.Tool = null;
            RizMetre.Arz = decimal.Parse(k.Trim()); ;
            RizMetre.Ertefa = decimal.Parse(n.Trim());
            RizMetre.Vazn = null;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId;
            //////////********************
            ///////////
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "300" + PolNum.ToString("D3") + "03";
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
            //RizMetre.Save();
        }
        ////////////////
        ///
        if (intNoeBanaii == 1)
        {
            string[,] arrItemsForInsert = { { "2", "3", "5", "7", "10" },
                                      { "080201", "080202", "080203", "080204", "080205" } };
            int count = 0;
            decimal a = 0;
            do
            {
                string strCurrentShomareh = arrItemsForInsert[1, count];
                clsFB? varFBUser1 = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strCurrentShomareh && x.NoeFBId == NoeFBId);
                //DtFBUser = clsConvert.ToDataTable(varFBUser1);

                //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='" + arrItemsForInsert[1, count] + "'");
                guFBId = new Guid();
                //if (DtFBUser.Rows.Count == 0)
                if (varFBUser1 == null)
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
                //FBId = OperationItemsFB.SaveFB(BarAvordID, arrItemsForInsert[1, count], 0);
                else
                    guFBId = varFBUser1.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                a = decimal.Parse(h.Trim()) - decimal.Parse("0.3") - int.Parse(arrItemsForInsert[0, count].ToString());
                decimal hForInsert = 0;
                if (a >= 0)
                {
                    if (count == 0)
                        hForInsert = int.Parse(arrItemsForInsert[0, count]);
                    else
                        hForInsert = int.Parse(arrItemsForInsert[0, count]) - int.Parse(arrItemsForInsert[0, count - 1]);
                }
                else
                {
                    if (count == 0)
                        hForInsert = decimal.Parse(h) - decimal.Parse("0.3");
                    else
                        hForInsert = decimal.Parse(h) - decimal.Parse("0.3") - int.Parse(arrItemsForInsert[0, count - 1]);
                }
                ////////////////
                clsRizMetreUsers? RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                Shomareh = 0;
                if (RizMetreUserLastShomareh != null)
                    Shomareh = RizMetreUserLastShomareh.Shomareh + 1;
                else
                    Shomareh = 1;

                //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId);
                RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = Shomareh;//int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                RizMetre.Sharh = "قالب بندی طولی دیوار کوله طرف داخل";
                RizMetre.Tedad = 2;
                RizMetre.Tool = decimal.Parse(LAbro.Trim());
                RizMetre.Arz = null;
                RizMetre.Ertefa = hForInsert;
                RizMetre.Vazn = null;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId;
                ///////*********************
                ////////
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "300" + PolNum.ToString("D3") + "04"; ///قالب بندی طولی دیوار کوله طرف داخل
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
                    PolDetailType = PolDetailTypeEnum.قالب_بندی_طولی_دیوار_کوله_طرف_داخل
                };
                _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                //_context.SaveChanges();
                //RizMetre.Save();
                count++;
            } while (a >= 0);
            count = 0;
            do
            {
                string strCurrentShomareh = arrItemsForInsert[1, count];
                clsFB? varFBUser1 = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strCurrentShomareh && x.NoeFBId == NoeFBId);
                //DtFBUser = clsConvert.ToDataTable(varFBUser1);

                //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='" + arrItemsForInsert[1, count] + "'");
                guFBId = new Guid();
                if (varFBUser1 == null)
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
                    guFBId = varFBUser1.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                decimal hPayePrimForInsert = 0;
                ////////////////
                a = decimal.Parse(LPayeMoarab.Trim()) / 100 - int.Parse(arrItemsForInsert[0, count].ToString());
                if (a >= 0)
                {
                    if (count == 0)
                        hPayePrimForInsert = int.Parse(arrItemsForInsert[0, count]);
                    else
                        hPayePrimForInsert = int.Parse(arrItemsForInsert[0, count]) - int.Parse(arrItemsForInsert[0, count - 1]);
                }
                else
                {
                    if (count == 0)
                        hPayePrimForInsert = decimal.Parse(LPayeMoarab) / 100;
                    else
                        hPayePrimForInsert = decimal.Parse(LPayeMoarab) / 100 - int.Parse(arrItemsForInsert[0, count - 1]);
                }

                clsRizMetreUsers? RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                long lastShomareh = 0;
                if (RizMetreUserLastShomareh != null)
                    lastShomareh = RizMetreUserLastShomareh.Shomareh + 1;
                else
                    lastShomareh = 1;

                //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId);
                RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = lastShomareh;// int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                RizMetre.Sharh = "قالب طولی دیوار کوله طرف خارج";
                RizMetre.Tedad = 2;
                RizMetre.Tool = decimal.Parse(LAbro.Trim());
                RizMetre.Arz = null;
                RizMetre.Ertefa = hPayePrimForInsert;
                RizMetre.Vazn = null;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId;
                //////***************
                ///////
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "300" + PolNum.ToString("D3") + "05";
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
                    PolDetailType = PolDetailTypeEnum.قالب_طولی_دیوار_کوله_طرف_خارج
                };
                _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                //_context.SaveChanges();
                //RizMetre.Save();
                count++;
            } while (a >= 0);
            ////////////////
            count = 0;
            do
            {
                string strCurrentShomareh = arrItemsForInsert[1, count];
                clsFB? varFBUser1 = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strCurrentShomareh && x.NoeFBId == NoeFBId);
                //DtFBUser = clsConvert.ToDataTable(varFBUser1);

                //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='" + arrItemsForInsert[1, count] + "'");
                guFBId = new Guid();
                if (varFBUser1 == null)
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
                    guFBId = varFBUser1.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                decimal hKoolePrimForInsert = 0;
                tedad = 2 * (int.Parse(TedadDahaneh) - 1);
                if (tedad != 0)
                {
                    a = decimal.Parse(LKooleMoarab.Trim()) / 100 - int.Parse(arrItemsForInsert[0, count].ToString());
                    if (a >= 0)
                    {
                        if (count == 0)
                            hKoolePrimForInsert = int.Parse(arrItemsForInsert[0, count]);
                        else
                            hKoolePrimForInsert = int.Parse(arrItemsForInsert[0, count]) - int.Parse(arrItemsForInsert[0, count - 1]);
                    }
                    else
                    {
                        if (count == 0)
                            hKoolePrimForInsert = decimal.Parse(LKooleMoarab) / 100;
                        else
                            hKoolePrimForInsert = decimal.Parse(LKooleMoarab) / 100 - int.Parse(arrItemsForInsert[0, count - 1]);
                    }

                    RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = ++Shomareh;
                    RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                    RizMetre.Sharh = "قالب بندی طولی دیوار پایه میانی";
                    RizMetre.Tedad = tedad;
                    RizMetre.Tool = decimal.Parse(LAbro.Trim());
                    RizMetre.Arz = null;
                    RizMetre.Ertefa = hKoolePrimForInsert;
                    RizMetre.Vazn = null;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId;
                    //////*****************
                    ///////////
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "300" + PolNum.ToString("D3") + "06";
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
                        PolDetailType = PolDetailTypeEnum.قالب_بندی_طولی_دیوار_پایه_میانی
                    };
                    _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                    //_context.SaveChanges();
                    //RizMetre.Save();
                }
                count++;
            } while (a >= 0);
            /////////////////
            count = 0;
            do
            {
                string strCurrentShomareh = arrItemsForInsert[1, count];
                var varFBUser1 = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strCurrentShomareh && x.NoeFBId == NoeFBId);
                //DtFBUser = clsConvert.ToDataTable(varFBUser1);

                guFBId = new Guid();
                //if (DtFBUser.Rows.Count == 0)
                if (varFBUser1==null)
                {
                    clsFB FBSave = new clsFB();
                    FBSave.BarAvordId = BarAvordId;
                    FBSave.Shomareh = strCurrentShomareh;
                    //FBSave.BahayeVahedZarib = 0;
                    FBSave.NoeFBId = NoeFBId;
                    _context.FBs.Add(FBSave);
                    guFBId = FBSave.ID;

                }
                else
                    guFBId = varFBUser1.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());


                decimal db1 = decimal.Parse(b1);
                decimal db2 = decimal.Parse(b2);
                decimal dp1 = decimal.Parse(p1);
                decimal dp2 = decimal.Parse(p2);

                //کوله های خارجی
                if (db1 != 0)
                {

                    decimal hArzKoole = Math.Round(((db1 + db2) / 2), 2);
                    //tedad = 2 * (int.Parse(TedadDahaneh) - 1);
                    //if (tedad != 0)
                    //{
                    //a = decimal.Parse(LKooleMoarab.Trim()) / 100 - int.Parse(arrItemsForInsert[0, count].ToString());
                    //if (a >= 0)
                    //{
                    //    if (count == 0)
                    //        hKoolePrimForInsert = int.Parse(arrItemsForInsert[0, count]);
                    //    else
                    //        hKoolePrimForInsert = int.Parse(arrItemsForInsert[0, count]) - int.Parse(arrItemsForInsert[0, count - 1]);
                    //}
                    //else
                    //{
                    //    if (count == 0)
                    //        hKoolePrimForInsert = decimal.Parse(LKooleMoarab) / 100;
                    //    else
                    //        hKoolePrimForInsert = decimal.Parse(LKooleMoarab) / 100 - int.Parse(arrItemsForInsert[0, count - 1]);
                    //}

                    RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = ++Shomareh;
                    RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                    RizMetre.Sharh = "قالب بندی عرضی دیوار کوله پایه خارجی";
                    RizMetre.Tedad = 4;//یک پایه طرف راست و یک پایه طرف چپ
                    RizMetre.Tool = null;
                    RizMetre.Arz = hArzKoole;
                    RizMetre.Ertefa = decimal.Parse(h);
                    RizMetre.Vazn = null;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId;
                    //////*****************
                    ///////////
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "300" + PolNum.ToString("D3") + "07";
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
                        PolDetailType = PolDetailTypeEnum.قالب_بندی_عرضی_دیوار_کوله_پایه_خارجی
                    };
                    _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                }

                //کوله های میانی
                if (dp1 != 0)
                {
                    decimal hArzKoole = Math.Round(((dp1 + dp2) / 2), 2);

                    RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = ++Shomareh;
                    RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                    RizMetre.Sharh = "قالب بندی عرضی دیوار کوله پایه میانی";
                    RizMetre.Tedad = 2;
                    RizMetre.Tool = null;
                    RizMetre.Arz = hArzKoole;
                    RizMetre.Ertefa = decimal.Parse(h);
                    RizMetre.Vazn = null;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId;
                    //////*****************
                    ///////////
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "300" + PolNum.ToString("D3") + "08";
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
                        PolDetailType = PolDetailTypeEnum.قالب_بندی_عرضی_دیوار_کوله_پایه_میانی
                    };
                    _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                }
                //}
                count++;
            } while (a >= 0);
        }
        /////////////////
        return true;
    }

}
