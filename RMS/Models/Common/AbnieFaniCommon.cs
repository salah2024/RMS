using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RMS.Models.Entity;
using RMS.Models.StoredProceduresData;

namespace RMS.Models.Common
{
    public class AbnieFaniCommon(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;
        public bool GhalebBandi(Guid BarAvordId, Guid PolVaAbroId, int PolNum, string LAbro, string w1, string w2, string w3, string w4,
            string f, string m, string n, string k, string h, string D, string TedadDahaneh, string LPayeMoarab, string LKooleMoarab)
        {
            var varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "080101").ToList();
            DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

            //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='080101'");
            //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
            //int FBId = 0;
            //if (DtFBUser.Rows.Count == 0)
            //    FBId = OperationItemsFB.SaveFB(BarAvordID, "080101", 0);
            //else
            //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

            Guid guFBId = new Guid();
            if (DtFBUser.Rows.Count == 0)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "080101";
                FBSave.BahayeVahedZarib = 0;
                _context.FBs.Add(FBSave);
                _context.SaveChanges();
                guFBId = FBSave.ID;
            }
            else
                guFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
            ////////////////
            clsRizMetreUsers RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            long Shomareh = 0;
            if (RizMetreUser != null)
                Shomareh = RizMetreUser.Shomareh + 1;
            else
                Shomareh = 1;

            //DataTable DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId);
            //int Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = Shomareh;
            RizMetre.Sharh = "قالب بندی طولی پی کوله";
            RizMetre.Tedad = 4;
            RizMetre.Tool = decimal.Parse(LAbro.Trim());
            RizMetre.Arz = 0;
            RizMetre.Ertefa = decimal.Parse(m.Trim());
            RizMetre.Vazn = 0;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId;
            ////***
            //////****
            ///بایستی دستی پر نشود
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "300" + PolNum.ToString("D3") + "00";///قالب بندی طولی پی کوله
            RizMetre.ForItem = "";
            RizMetre.UseItem = "";
            _context.RizMetreUserses.Add(RizMetre);
            _context.SaveChanges();
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
                RizMetre.Sharh = "قالب بندی عرضی پی کوله (سرکله)";
                RizMetre.Tedad = tedad;
                RizMetre.Tool = 0;
                RizMetre.Arz = decimal.Parse(f.Trim());
                RizMetre.Ertefa = decimal.Parse(m.Trim());
                RizMetre.Vazn = 0;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId;
                ///************
                ///
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "300" + PolNum.ToString("D3") + "01";
                RizMetre.ForItem = "";
                RizMetre.UseItem = "";
                _context.RizMetreUserses.Add(RizMetre);
                _context.SaveChanges();
                //RizMetre.Save();
            }
            ////////////////
            tedad = 2 * (int.Parse(TedadDahaneh) - 1);
            if (tedad != 0)
            {
                RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = ++Shomareh;
                RizMetre.Sharh = "قالب بندی طولی پی پایه میانی";
                RizMetre.Tedad = tedad;
                RizMetre.Tool = decimal.Parse(LAbro.Trim());
                RizMetre.Arz = 0;
                RizMetre.Ertefa = decimal.Parse(n.Trim());
                RizMetre.Vazn = 0;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId;
                //*********************
                ///
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "300" + PolNum.ToString("D3") + "02";
                RizMetre.ForItem = "";
                RizMetre.UseItem = "";
                _context.RizMetreUserses.Add(RizMetre);
                _context.SaveChanges();
                //RizMetre.Save();
            }
            ////////////////
            tedad = 2 * (int.Parse(TedadDahaneh) - 1) - countWIsZero;
            if (tedad != 0)
            {
                RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = ++Shomareh;
                RizMetre.Sharh = "قالب بندی عرضی پی پایه میانی (سرکله)";
                RizMetre.Tedad = tedad;
                RizMetre.Tool = 0;
                RizMetre.Arz = decimal.Parse(k.Trim()); ;
                RizMetre.Ertefa = decimal.Parse(n.Trim());
                RizMetre.Vazn = 0;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId;
                //////////********************
                ///////////
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "300" + PolNum.ToString("D3") + "03";
                RizMetre.ForItem = "";
                RizMetre.UseItem = "";
                _context.RizMetreUserses.Add(RizMetre);
                _context.SaveChanges();
                //RizMetre.Save();
            }
            ////////////////
            string[,] arrItemsForInsert = { { "2", "3", "5", "7", "10" },
                                          { "080201", "080202", "080203", "080204", "080205" } };
            int count = 0;
            decimal a = 0;
            do
            {
                string strCurrentShomareh = arrItemsForInsert[1, count];
                var varFBUser1 = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strCurrentShomareh).ToList();
                DtFBUser = clsConvert.ToDataTable(varFBUser1);

                //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='" + arrItemsForInsert[1, count] + "'");
                guFBId = new Guid();
                if (DtFBUser.Rows.Count == 0)
                {
                    clsFB FBSave = new clsFB();
                    FBSave.BarAvordId = BarAvordId;
                    FBSave.Shomareh = strCurrentShomareh;
                    FBSave.BahayeVahedZarib = 0;
                    _context.FBs.Add(FBSave);
                    _context.SaveChanges();
                    guFBId = FBSave.ID;
                }
                //FBId = OperationItemsFB.SaveFB(BarAvordID, arrItemsForInsert[1, count], 0);
                else
                    guFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

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
                clsRizMetreUsers RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                Shomareh = 0;
                if (RizMetreUserLastShomareh != null)
                    Shomareh = RizMetreUserLastShomareh.Shomareh + 1;
                else
                    Shomareh = 1;

                //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId);
                RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = Shomareh;//int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                RizMetre.Sharh = "قالب بندی طولی دیوار کوله طرف داخل";
                RizMetre.Tedad = 2;
                RizMetre.Tool = decimal.Parse(LAbro.Trim());
                RizMetre.Arz = 0;
                RizMetre.Ertefa = hForInsert;
                RizMetre.Vazn = 0;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId;
                ///////*********************
                ////////
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "300" + PolNum.ToString("D3") + "04"; ///قالب بندی طولی دیوار کوله طرف داخل
                RizMetre.ForItem = "";
                RizMetre.UseItem = "";
                _context.RizMetreUserses.Add(RizMetre);
                _context.SaveChanges();
                //RizMetre.Save();
                count++;
            } while (a >= 0);
            count = 0;
            do
            {
                string strCurrentShomareh = arrItemsForInsert[1, count];
                var varFBUser1 = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strCurrentShomareh).ToList();
                DtFBUser = clsConvert.ToDataTable(varFBUser1);

                //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='" + arrItemsForInsert[1, count] + "'");
                guFBId = new Guid();
                if (DtFBUser.Rows.Count == 0)
                {
                    clsFB FBSave = new clsFB();
                    FBSave.BarAvordId = BarAvordId;
                    FBSave.Shomareh = strCurrentShomareh;
                    FBSave.BahayeVahedZarib = 0;
                    _context.FBs.Add(FBSave);
                    _context.SaveChanges();
                    guFBId = FBSave.ID;

                    //FBId = OperationItemsFB.SaveFB(BarAvordID, arrItemsForInsert[1, count], 0);
                }
                else
                    guFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

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

                clsRizMetreUsers RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                long lastShomareh = 0;
                if (RizMetreUserLastShomareh != null)
                    lastShomareh = RizMetreUserLastShomareh.Shomareh + 1;
                else
                    lastShomareh = 1;

                //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId);
                RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = lastShomareh;// int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                RizMetre.Sharh = "قالب طولی دیوار کوله طرف خارج";
                RizMetre.Tedad = 2;
                RizMetre.Tool = decimal.Parse(LAbro.Trim());
                RizMetre.Arz = 0;
                RizMetre.Ertefa = hPayePrimForInsert;
                RizMetre.Vazn = 0;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId;
                //////***************
                ///////
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "300" + PolNum.ToString("D3") + "05";
                RizMetre.ForItem = "";
                RizMetre.UseItem = "";
                _context.RizMetreUserses.Add(RizMetre);
                _context.SaveChanges();
                //RizMetre.Save();
                count++;
            } while (a >= 0);
            ////////////////
            count = 0;
            do
            {
                string strCurrentShomareh = arrItemsForInsert[1, count];
                var varFBUser1 = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strCurrentShomareh).ToList();
                DtFBUser = clsConvert.ToDataTable(varFBUser1);

                //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='" + arrItemsForInsert[1, count] + "'");
                guFBId = new Guid();
                if (DtFBUser.Rows.Count == 0)
                {
                    clsFB FBSave = new clsFB();
                    FBSave.BarAvordId = BarAvordId;
                    FBSave.Shomareh = strCurrentShomareh;
                    FBSave.BahayeVahedZarib = 0;
                    _context.FBs.Add(FBSave);
                    _context.SaveChanges();
                    guFBId = FBSave.ID;

                    //FBId = OperationItemsFB.SaveFB(BarAvordID, arrItemsForInsert[1, count], 0);
                }
                else
                    guFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

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
                    RizMetre.Sharh = "قالب بندی طولی دیوار پایه میانی";
                    RizMetre.Tedad = tedad;
                    RizMetre.Tool = decimal.Parse(LAbro.Trim());
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = hKoolePrimForInsert;
                    RizMetre.Vazn = 0;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId;
                    //////*****************
                    ///////////
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "300" + PolNum.ToString("D3") + "06";
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();
                    //RizMetre.Save();
                }
                count++;
            } while (a >= 0);
            /////////////////
            return true;
        }

        public bool GhalebBandiChenaj(Guid BarAvordId, Guid PolVaAbroId, int PolNum, string LAbro, string w1, string w2, string w3, string w4,
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
                    var varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strCurrentShomareh).ToList();
                    DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

                    //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='" + arrItemsForInsert[1, count] + "'");
                    //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
                    Guid guFBId = new Guid();
                    if (DtFBUser.Rows.Count == 0)
                    {
                        clsFB FBSave = new clsFB();
                        FBSave.BarAvordId = BarAvordId;
                        FBSave.Shomareh = strCurrentShomareh;
                        FBSave.BahayeVahedZarib = 0;
                        _context.FBs.Add(FBSave);
                        _context.SaveChanges();
                        guFBId = FBSave.ID;

                        //FBId = OperationItemsFB.SaveFB(BarAvordID, arrItemsForInsert[1, count], 0);
                    }
                    else
                        guFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                    ////////////////قالب بندی طولی شناژ کوله
                    clsRizMetreUsers RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                    long Shomareh = 0;
                    if (RizMetreUserLastShomareh != null)
                        Shomareh = RizMetreUserLastShomareh.Shomareh + 1;
                    else
                        Shomareh = 1;
                    //DataTable DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId);
                    //int Shomareh = lastShomareh;// int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh;
                    RizMetre.Sharh = "قالب بندی طولی شناژ کوله";
                    RizMetre.Tedad = 4;
                    RizMetre.Tool = decimal.Parse(LAbro.Trim());
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = decimal.Parse(t.Trim()) + decimal.Parse("0.3");
                    RizMetre.Vazn = 0;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "300" + PolNum.ToString("D3") + "10"; ///قالب بندی طولی شناژ کوله
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();
                    //RizMetre.Save();
                    /////////////قالب بندی عرضی شناژ کوله
                    int tedad = 4 - (countWIsZero);
                    if (tedad != 0)
                    {
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = ++Shomareh;
                        RizMetre.Sharh = "قالب بندی عرضی شناژ کوله";
                        RizMetre.Tedad = tedad;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = decimal.Parse(b2.Trim());
                        RizMetre.Ertefa = decimal.Parse("0.3");
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "11"; ///قالب بندی عرضی شناژ کوله
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();
                        ////////////////////
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = ++Shomareh;
                        RizMetre.Sharh = "قالب بندی عرضی شناژ کوله";
                        RizMetre.Tedad = tedad;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = decimal.Parse(c2.Trim());
                        RizMetre.Ertefa = decimal.Parse(t.Trim());
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "11";///قالب بندی عرضی شناژ کوله
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();
                    }

                    /////////////قالب بندی طولی شناژ پایه میانی
                    tedad = 2 * (int.Parse(TedadDahaneh) - 1);
                    if (tedad != 0)
                    {
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = ++Shomareh;
                        RizMetre.Sharh = "قالب بندی طولی شناژ پایه میانی";
                        RizMetre.Tedad = tedad;
                        RizMetre.Tool = decimal.Parse(LAbro.Trim());
                        RizMetre.Arz = 0;
                        RizMetre.Ertefa = decimal.Parse("0.3");
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "12";///قالب بندی طولی شناژ پایه میانی
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        //RizMetre.Save();
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        /////////////قالب بندی عرضی شناژ پایه میانی
                        tedad = 4 - (countWIsZero);
                        if (tedad != 0)
                        {
                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = ++Shomareh;
                            RizMetre.Sharh = "قالب بندی عرضی شناژ پایه میانی";
                            RizMetre.Tedad = tedad;
                            RizMetre.Tool = 0;
                            RizMetre.Arz = decimal.Parse(p2.Trim());
                            RizMetre.Ertefa = decimal.Parse("0.3");
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "300" + PolNum.ToString("D3") + "13";///قالب بندی عرضی شناژ پایه میانی
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            //RizMetre.Save();
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                        }
                    }
                }
                //////
                //////
                //////
                else if (blnSecondCon)
                {
                    string strCurrentShomareh = arrItemsForInsert[1, count];
                    var varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strCurrentShomareh).ToList();
                    DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

                    //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='" + arrItemsForInsert[1, count] + "'");
                    //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
                    //int FBId = 0;
                    //if (DtFBUser.Rows.Count == 0)
                    //    FBId = OperationItemsFB.SaveFB(BarAvordID, arrItemsForInsert[1, count], 0);
                    //else
                    //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

                    Guid guFBId = new Guid();
                    if (DtFBUser.Rows.Count == 0)
                    {
                        clsFB FBSave = new clsFB();
                        FBSave.BarAvordId = BarAvordId;
                        FBSave.Shomareh = strCurrentShomareh;
                        FBSave.BahayeVahedZarib = 0;
                        _context.FBs.Add(FBSave);
                        _context.SaveChanges();
                        guFBId = FBSave.ID;
                    }
                    else
                        guFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                    ////////////////قالب بندی طولی شناژ کوله
                    //DataTable DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId);
                    //int Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                    //clsRizMetreUsersses RizMetre = new clsRizMetreUsersses();

                    clsRizMetreUsers RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                    long Shomareh = 0;
                    if (RizMetreUserLastShomareh != null)
                        Shomareh = RizMetreUserLastShomareh.Shomareh + 1;
                    else
                        Shomareh = 1;

                    //int Shomareh = lastShomareh;// int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh;
                    RizMetre.Sharh = "قالب بندی طولی شناژ کوله";
                    RizMetre.Tedad = 4;
                    RizMetre.Tool = decimal.Parse(LAbro.Trim());
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = decimal.Parse(arrItemsForInsert[0, count]) - (decimal.Parse(h.Trim()) - decimal.Parse("0.3"));
                    RizMetre.Vazn = 0;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "300" + PolNum.ToString("D3") + "10";///قالب بندی طولی شناژ کوله
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();
                    //RizMetre.Save();
                    /////////////ثبت قسمت دوم
                    //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='" + arrItemsForInsert[1, count + 1] + "'");
                    //FBId = 0;
                    //if (DtFBUser.Rows.Count == 0)
                    //    FBId = OperationItemsFB.SaveFB(BarAvordID, arrItemsForInsert[1, count + 1], 0);
                    //else
                    //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
                    string strCurrentShomareh1 = arrItemsForInsert[1, count + 1];
                    var varFBUser1 = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strCurrentShomareh1).ToList();
                    DtFBUser = clsConvert.ToDataTable(varFBUser1);
                    guFBId = new Guid();
                    if (DtFBUser.Rows.Count == 0)
                    {
                        clsFB FBSave = new clsFB();
                        FBSave.BarAvordId = BarAvordId;
                        FBSave.Shomareh = strCurrentShomareh1;
                        FBSave.BahayeVahedZarib = 0;
                        _context.FBs.Add(FBSave);
                        _context.SaveChanges();
                        guFBId = FBSave.ID;
                    }
                    else
                        guFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

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
                    RizMetre.Sharh = "قالب بندی طولی شناژ کوله";
                    RizMetre.Tedad = 4;
                    RizMetre.Tool = decimal.Parse(LAbro.Trim());
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = (decimal.Parse("0.3") + decimal.Parse(t.Trim())) - (decimal.Parse(arrItemsForInsert[0, count]) - (decimal.Parse(h.Trim()) - decimal.Parse("0.3")));
                    RizMetre.Vazn = 0;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "300" + PolNum.ToString("D3") + "10";///قالب بندی طولی شناژ کوله
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();
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
                            varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strCurrentShomareh).ToList();
                            DtFBUser = clsConvert.ToDataTable(varFBUser);

                            guFBId = new Guid();
                            if (DtFBUser.Rows.Count == 0)
                            {
                                clsFB FBSave = new clsFB();
                                FBSave.BarAvordId = BarAvordId;
                                FBSave.Shomareh = strCurrentShomareh;
                                FBSave.BahayeVahedZarib = 0;
                                _context.FBs.Add(FBSave);
                                _context.SaveChanges();
                                guFBId = FBSave.ID;
                            }
                            else
                                guFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                            //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='" + arrItemsForInsert[1, count] + "'");
                            //FBId = 0;
                            //if (DtFBUser.Rows.Count == 0)
                            //    FBId = OperationItemsFB.SaveFB(BarAvordID, arrItemsForInsert[1, count], 0);
                            //else
                            //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
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
                            RizMetre.Sharh = "قالب بندی عرضی شناژ کوله";
                            RizMetre.Tedad = tedad;
                            RizMetre.Tool = 0;
                            RizMetre.Arz = decimal.Parse(b2.Trim());
                            RizMetre.Ertefa = decimal.Parse(arrItemsForInsert[0, count]) - (decimal.Parse(h.Trim()) - decimal.Parse("0.3"));
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "300" + PolNum.ToString("D3") + "11";///قالب بندی عرضی شناژ کوله
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                            ///////////////قسمت دوم

                            strCurrentShomareh1 = arrItemsForInsert[1, count + 1];
                            varFBUser1 = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strCurrentShomareh1).ToList();
                            DtFBUser = clsConvert.ToDataTable(varFBUser1);
                            guFBId = new Guid();
                            if (DtFBUser.Rows.Count == 0)
                            {
                                clsFB FBSave = new clsFB();
                                FBSave.BarAvordId = BarAvordId;
                                FBSave.Shomareh = strCurrentShomareh1;
                                FBSave.BahayeVahedZarib = 0;
                                _context.FBs.Add(FBSave);
                                _context.SaveChanges();
                                guFBId = FBSave.ID;
                            }
                            else
                                guFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                            ////////////////
                            RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                            lastShomareh = 0;
                            if (RizMetreUserLastShomareh != null)
                                lastShomareh = RizMetreUserLastShomareh.Shomareh + 1;
                            else
                                lastShomareh = 1;


                            //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='" + arrItemsForInsert[1, count + 1] + "'");
                            //FBId = 0;
                            //if (DtFBUser.Rows.Count == 0)
                            //    FBId = OperationItemsFB.SaveFB(BarAvordID, arrItemsForInsert[1, count + 1], 0);
                            //else
                            //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
                            ////////////////
                            //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId);
                            //Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = lastShomareh;
                            RizMetre.Sharh = "قالب بندی عرضی شناژ کوله";
                            RizMetre.Tedad = tedad;
                            RizMetre.Tool = 0;
                            RizMetre.Arz = decimal.Parse(b2.Trim());
                            RizMetre.Ertefa = decimal.Parse(h.Trim()) - decimal.Parse(arrItemsForInsert[0, count]);
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "300" + PolNum.ToString("D3") + "11";///قالب بندی عرضی شناژ کوله
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            //RizMetre.Save();
                            ////////
                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = ++lastShomareh;
                            RizMetre.Sharh = "قالب بندی عرضی شناژ کوله";
                            RizMetre.Tedad = tedad;
                            RizMetre.Tool = 0;
                            RizMetre.Arz = decimal.Parse(c2.Trim());
                            RizMetre.Ertefa = decimal.Parse(t.Trim());
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "300" + PolNum.ToString("D3") + "11";///قالب بندی عرضی شناژ کوله
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }

                        /////////////قالب بندی طولی شناژ پایه میانی
                        tedad = 2 * (int.Parse(TedadDahaneh) - 1);
                        if (tedad != 0)
                        {
                            strCurrentShomareh = arrItemsForInsert[1, count];
                            varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strCurrentShomareh).ToList();
                            DtFBUser = clsConvert.ToDataTable(varFBUser);

                            guFBId = new Guid();
                            if (DtFBUser.Rows.Count == 0)
                            {
                                clsFB FBSave = new clsFB();
                                FBSave.BarAvordId = BarAvordId;
                                FBSave.Shomareh = strCurrentShomareh;
                                FBSave.BahayeVahedZarib = 0;
                                _context.FBs.Add(FBSave);
                                _context.SaveChanges();
                                guFBId = FBSave.ID;
                            }
                            else
                                guFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
                            ////////////////
                            RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                            lastShomareh = 0;
                            if (RizMetreUserLastShomareh != null)
                                lastShomareh = RizMetreUserLastShomareh.Shomareh + 1;
                            else
                                lastShomareh = 1;

                            //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='" + arrItemsForInsert[1, count] + "'");
                            //FBId = 0;
                            //if (DtFBUser.Rows.Count == 0)
                            //    FBId = OperationItemsFB.SaveFB(BarAvordID, arrItemsForInsert[1, count], 0);
                            //else
                            //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
                            ////////////////
                            //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId);
                            //Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = lastShomareh;
                            RizMetre.Sharh = "قالب بندی طولی شناژ پایه میانی";
                            RizMetre.Tedad = tedad;
                            RizMetre.Tool = decimal.Parse(LAbro.Trim());
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = decimal.Parse(arrItemsForInsert[0, count]) - (decimal.Parse(h.Trim()) - decimal.Parse("0.3"));
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "300" + PolNum.ToString("D3") + "12";///قالب بندی طولی شناژ پایه میانی
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                            //////////قالب بندی عرضی شناژ پایه میانی
                            int tedad2 = 4 - countWIsZero;
                            if (tedad2 != 0)
                            {
                                RizMetre = new clsRizMetreUsers();
                                RizMetre.Shomareh = ++lastShomareh;
                                RizMetre.Sharh = "قالب بندی عرضی شناژ پایه میانی";
                                RizMetre.Tedad = tedad2;
                                RizMetre.Tool = 0;
                                RizMetre.Arz = decimal.Parse(p2.Trim());
                                RizMetre.Ertefa = decimal.Parse(arrItemsForInsert[0, count]) - (decimal.Parse(h.Trim()) - decimal.Parse("0.3"));
                                RizMetre.Vazn = 0;
                                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                                RizMetre.FBId = guFBId;
                                RizMetre.OperationsOfHamlId = 1;
                                RizMetre.Type = "300" + PolNum.ToString("D3") + "13";///قالب بندی عرضی شناژ پایه میانی
                                RizMetre.ForItem = "";
                                RizMetre.UseItem = "";
                                _context.RizMetreUserses.Add(RizMetre);
                                _context.SaveChanges();
                                //RizMetre.Save();
                            }
                            ////////
                            /////////قسمت دوم

                            strCurrentShomareh = arrItemsForInsert[1, count + 1];
                            varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strCurrentShomareh).ToList();
                            DtFBUser = clsConvert.ToDataTable(varFBUser);

                            guFBId = new Guid();
                            if (DtFBUser.Rows.Count == 0)
                            {
                                clsFB FBSave = new clsFB();
                                FBSave.BarAvordId = BarAvordId;
                                FBSave.Shomareh = strCurrentShomareh;
                                FBSave.BahayeVahedZarib = 0;
                                _context.FBs.Add(FBSave);
                                _context.SaveChanges();
                                guFBId = FBSave.ID;
                            }
                            else
                                guFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
                            ////////////////
                            RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                            lastShomareh = 0;
                            if (RizMetreUserLastShomareh != null)
                                lastShomareh = RizMetreUserLastShomareh.Shomareh + 1;
                            else
                                lastShomareh = 1;

                            //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='" + arrItemsForInsert[1, count + 1] + "'");
                            //FBId = 0;
                            //if (DtFBUser.Rows.Count == 0)
                            //    FBId = OperationItemsFB.SaveFB(BarAvordID, arrItemsForInsert[1, count + 1], 0);
                            //else
                            //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
                            ////////////////قالب بندی طولی شناژ پایه میانی
                            //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId);
                            //Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = lastShomareh;
                            RizMetre.Sharh = "قالب بندی طولی شناژ پایه میانی";
                            RizMetre.Tedad = tedad;
                            RizMetre.Tool = decimal.Parse(LAbro.Trim());
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = decimal.Parse("0.3") - (decimal.Parse(arrItemsForInsert[0, count]) - (decimal.Parse(h.Trim()) - decimal.Parse("0.3")));
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "300" + PolNum.ToString("D3") + "12";///قالب بندی طولی شناژ پایه میانی
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                            ///////////////قالب بندی عرضی شناژ پایه میانی
                            tedad2 = 4 - countWIsZero;
                            if (tedad2 != 0)
                            {
                                RizMetre = new clsRizMetreUsers();
                                RizMetre.Shomareh = ++lastShomareh;
                                RizMetre.Sharh = "قالب بندی عرضی شناژ پایه میانی";
                                RizMetre.Tedad = tedad2;
                                RizMetre.Tool = 0;
                                RizMetre.Arz = decimal.Parse(p2.Trim());
                                RizMetre.Ertefa = decimal.Parse("0.3") - (decimal.Parse(arrItemsForInsert[0, count]) - (decimal.Parse(h.Trim()) - decimal.Parse("0.3")));
                                RizMetre.Vazn = 0;
                                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                                RizMetre.FBId = guFBId;
                                RizMetre.OperationsOfHamlId = 1;
                                RizMetre.Type = "300" + PolNum.ToString("D3") + "13";///قالب بندی عرضی شناژ پایه میانی
                                RizMetre.ForItem = "";
                                RizMetre.UseItem = "";
                                _context.RizMetreUserses.Add(RizMetre);
                                _context.SaveChanges();
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
                            varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strCurrentShomareh).ToList();
                            DtFBUser = clsConvert.ToDataTable(varFBUser);

                            guFBId = new Guid();
                            if (DtFBUser.Rows.Count == 0)
                            {
                                clsFB FBSave = new clsFB();
                                FBSave.BarAvordId = BarAvordId;
                                FBSave.Shomareh = strCurrentShomareh;
                                FBSave.BahayeVahedZarib = 0;
                                _context.FBs.Add(FBSave);
                                _context.SaveChanges();
                                guFBId = FBSave.ID;
                            }
                            else
                                guFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
                            ////////////////

                            RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                            lastShomareh = 0;
                            if (RizMetreUserLastShomareh != null)
                                lastShomareh = RizMetreUserLastShomareh.Shomareh + 1;
                            else
                                lastShomareh = 1;

                            //varDtLastRizMetreUsersesShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).ToList();
                            //DtLastRizMetreUsersesShomareh = clsConvert.ToDataTable(varDtLastRizMetreUsersesShomareh);
                            //Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());

                            //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='" + arrItemsForInsert[1, count] + "'");
                            //FBId = 0;
                            //if (DtFBUser.Rows.Count == 0)
                            //    FBId = OperationItemsFB.SaveFB(BarAvordID, arrItemsForInsert[1, count], 0);
                            //else
                            //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
                            ////////////////
                            //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId);
                            //Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = lastShomareh;
                            RizMetre.Sharh = "قالب بندی عرضی شناژ کوله";
                            RizMetre.Tedad = tedad;
                            RizMetre.Tool = 0;
                            RizMetre.Arz = decimal.Parse(b2.Trim());
                            RizMetre.Ertefa = decimal.Parse("0.3");
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "300" + PolNum.ToString("D3") + "11";///قالب بندی عرضی شناژ کوله
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                            ///////////////قسمت دوم

                            strCurrentShomareh = arrItemsForInsert[1, count + 1];
                            varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strCurrentShomareh).ToList();
                            DtFBUser = clsConvert.ToDataTable(varFBUser);

                            guFBId = new Guid();
                            if (DtFBUser.Rows.Count == 0)
                            {
                                clsFB FBSave = new clsFB();
                                FBSave.BarAvordId = BarAvordId;
                                FBSave.Shomareh = strCurrentShomareh;
                                FBSave.BahayeVahedZarib = 0;
                                _context.FBs.Add(FBSave);
                                _context.SaveChanges();
                                guFBId = FBSave.ID;
                            }
                            else
                                guFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
                            ////////////////
                            RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                            lastShomareh = 0;
                            if (RizMetreUserLastShomareh != null)
                                lastShomareh = RizMetreUserLastShomareh.Shomareh + 1;
                            else
                                lastShomareh = 1;

                            //varDtLastRizMetreUsersesShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).ToList();
                            //DtLastRizMetreUsersesShomareh = clsConvert.ToDataTable(varDtLastRizMetreUsersesShomareh);
                            //Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());

                            //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='" + arrItemsForInsert[1, count + 1] + "'");
                            //FBId = 0;
                            //if (DtFBUser.Rows.Count == 0)
                            //    FBId = OperationItemsFB.SaveFB(BarAvordID, arrItemsForInsert[1, count + 1], 0);
                            //else
                            //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
                            //////////////////
                            //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId);
                            //Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = lastShomareh;
                            RizMetre.Sharh = "قالب بندی عرضی شناژ کوله";
                            RizMetre.Tedad = tedad;
                            RizMetre.Tool = 0;
                            RizMetre.Arz = decimal.Parse(c2.Trim());
                            RizMetre.Ertefa = decimal.Parse(arrItemsForInsert[0, count]) - decimal.Parse(h.Trim());
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "300" + PolNum.ToString("D3") + "11";///قالب بندی عرضی شناژ کوله
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                            ////////
                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = ++lastShomareh;
                            RizMetre.Sharh = "قالب بندی عرضی شناژ کوله";
                            RizMetre.Tedad = tedad;
                            RizMetre.Tool = 0;
                            RizMetre.Arz = decimal.Parse(c2.Trim());
                            RizMetre.Ertefa = decimal.Parse(t.Trim()) - (decimal.Parse(arrItemsForInsert[0, count]) - decimal.Parse(h.Trim()));
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "300" + PolNum.ToString("D3") + "11";///قالب بندی عرضی شناژ کوله
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }

                        ////////////قالب بندی طولی شناژ پایه میانی
                        tedad = 2 * (int.Parse(TedadDahaneh) - 1);
                        if (tedad != 0)
                        {
                            strCurrentShomareh = arrItemsForInsert[1, count];
                            varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strCurrentShomareh).ToList();
                            DtFBUser = clsConvert.ToDataTable(varFBUser);

                            guFBId = new Guid();
                            if (DtFBUser.Rows.Count == 0)
                            {
                                clsFB FBSave = new clsFB();
                                FBSave.BarAvordId = BarAvordId;
                                FBSave.Shomareh = strCurrentShomareh;
                                FBSave.BahayeVahedZarib = 0;
                                _context.FBs.Add(FBSave);
                                _context.SaveChanges();
                                guFBId = FBSave.ID;
                            }
                            else
                                guFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
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

                            //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='" + arrItemsForInsert[1, count] + "'");
                            //FBId = 0;
                            //if (DtFBUser.Rows.Count == 0)
                            //    FBId = OperationItemsFB.SaveFB(BarAvordID, arrItemsForInsert[1, count], 0);
                            //else
                            //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
                            //////////////////
                            //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId);
                            //Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = lastShomareh;
                            RizMetre.Sharh = "قالب بندی طولی شناژ پایه میانی";
                            RizMetre.Tedad = tedad;
                            RizMetre.Tool = decimal.Parse(LAbro.Trim());
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = decimal.Parse("0.3");
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "300" + PolNum.ToString("D3") + "12";///قالب بندی طولی شناژ پایه میانی
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                            //////قالب بندی عرضی شناژ پایه میانی
                            int tedad2 = 4 - countWIsZero;
                            if (tedad2 != 0)
                            {
                                RizMetre = new clsRizMetreUsers();
                                RizMetre.Shomareh = ++lastShomareh;
                                RizMetre.Sharh = "قالب بندی عرضی شناژ پایه میانی";
                                RizMetre.Tedad = tedad;
                                RizMetre.Tool = 0;
                                RizMetre.Arz = decimal.Parse(p2.Trim());
                                RizMetre.Ertefa = decimal.Parse("0.3");
                                RizMetre.Vazn = 0;
                                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                                RizMetre.FBId = guFBId;
                                RizMetre.OperationsOfHamlId = 1;
                                RizMetre.Type = "300" + PolNum.ToString("D3") + "13";///قالب بندی عرضی شناژ پایه میانی
                                RizMetre.ForItem = "";
                                RizMetre.UseItem = "";
                                _context.RizMetreUserses.Add(RizMetre);
                                _context.SaveChanges();
                                //RizMetre.Save();
                            }
                        }
                    }
                }
            }
            ////////////////
            return true;
        }
        public bool GhalebBandiDalDarja(Guid BarAvordId, Guid PolVaAbroId, int PolNum, string LAbro, string w1, string w2, string w3, string w4,
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

                if (int.Parse(D.Trim()) <= 5)
                {
                    var varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "080301").ToList();
                    DtFBUser = clsConvert.ToDataTable(varFBUser);

                    guFBId = new Guid();
                    if (DtFBUser.Rows.Count == 0)
                    {
                        clsFB FBSave = new clsFB();
                        FBSave.BarAvordId = BarAvordId;
                        FBSave.Shomareh = "080301";
                        FBSave.BahayeVahedZarib = 0;
                        _context.FBs.Add(FBSave);
                        _context.SaveChanges();
                        guFBId = FBSave.ID;
                    }
                    else
                        guFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
                    ////////////////
                    var RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                    if (RizMetreUserLastShomareh != null)
                        Shomareh = RizMetreUserLastShomareh.Shomareh + 1;
                    else
                        Shomareh = 1;

                    //var varDtLastRizMetreUsersesShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).ToList();
                    //DtLastRizMetreUsersesShomareh = clsConvert.ToDataTable(varDtLastRizMetreUsersesShomareh);
                    //Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                    //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='080301'");
                    //FBId = 0;
                    //if (DtFBUser.Rows.Count == 0)
                    //    FBId = OperationItemsFB.SaveFB(BarAvordID, "080301", 0);
                    //else
                    //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

                    ////////////////قالب زیر دال
                    //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId);
                    //Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh;
                    RizMetre.Sharh = "قالب زیر دال";
                    RizMetre.Tedad = int.Parse(TedadDahaneh.Trim());
                    RizMetre.Tool = decimal.Parse(LAbro.Trim());
                    RizMetre.Arz = int.Parse(D.Trim());
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = 0;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "300" + PolNum.ToString("D3") + "20";///قالب دال
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();
                    //RizMetre.Save();
                    /////////////قالب نمای دال
                    int tedad = int.Parse(TedadDahaneh) * (2 - (countWIsZero / 2));
                    if (tedad != 0)
                    {
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = ++Shomareh;
                        RizMetre.Sharh = "قالب نمای دال";
                        RizMetre.Tedad = tedad;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = int.Parse(D.Trim()) + 2 * (decimal.Parse(c1)) - 2 * (decimal.Parse(j));
                        RizMetre.Ertefa = decimal.Parse(t.Trim());
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "20";///قالب دال
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();
                    }
                }
                else if (int.Parse(D.Trim()) > 5 && int.Parse(D.Trim()) <= 10)
                {

                    var varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "080302").ToList();
                    DtFBUser = clsConvert.ToDataTable(varFBUser);

                    guFBId = new Guid();
                    if (DtFBUser.Rows.Count == 0)
                    {
                        clsFB FBSave = new clsFB();
                        FBSave.BarAvordId = BarAvordId;
                        FBSave.Shomareh = "080302";
                        FBSave.BahayeVahedZarib = 0;
                        _context.FBs.Add(FBSave);
                        _context.SaveChanges();
                        guFBId = FBSave.ID;
                    }
                    else
                        guFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
                    ////////////////
                    //var varDtLastRizMetreUsersesShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).ToList();
                    //DtLastRizMetreUsersesShomareh = clsConvert.ToDataTable(varDtLastRizMetreUsersesShomareh);
                    //Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());

                    var RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                    Shomareh = 0;
                    if (RizMetreUserLastShomareh != null)
                        Shomareh = RizMetreUserLastShomareh.Shomareh + 1;
                    else
                        Shomareh = 1;

                    //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='080302'");
                    //FBId = 0;
                    //if (DtFBUser.Rows.Count == 0)
                    //    FBId = OperationItemsFB.SaveFB(BarAvordID, "080302", 0);
                    //else
                    //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

                    //////////////////قالب زیر دال
                    //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId);
                    //Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh;
                    RizMetre.Sharh = "قالب زیر دال";
                    RizMetre.Tedad = int.Parse(TedadDahaneh.Trim());
                    RizMetre.Tool = decimal.Parse(LAbro.Trim());
                    RizMetre.Arz = int.Parse(D.Trim());
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = 0;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "300" + PolNum.ToString("D3") + "20";///قالب زیر دال
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();
                    //RizMetre.Save();
                    /////////////قالب نمای دال
                    int tedad = int.Parse(TedadDahaneh) * (2 - (countWIsZero / 2));
                    if (tedad != 0)
                    {
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = ++Shomareh;
                        RizMetre.Sharh = "قالب نمای دال";
                        RizMetre.Tedad = tedad;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = int.Parse(D.Trim()) + 2 * (decimal.Parse(c1)) - 2 * (decimal.Parse(j));
                        RizMetre.Ertefa = 0;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "21";///قالب نمای دال
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();
                    }
                }


                var varFBUser1 = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "080501").ToList();
                DtFBUser = clsConvert.ToDataTable(varFBUser1);

                guFBId = new Guid();
                if (DtFBUser.Rows.Count == 0)
                {
                    clsFB FBSave = new clsFB();
                    FBSave.BarAvordId = BarAvordId;
                    FBSave.Shomareh = "080501";
                    FBSave.BahayeVahedZarib = 0;
                    _context.FBs.Add(FBSave);
                    _context.SaveChanges();
                    guFBId = FBSave.ID;
                }
                else
                    guFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
                ////////////////
                //var varDtLastRizMetreUsersesShomareh1 = _context.RizMetreUserses.Where(x => x.FBId == guFBId1).ToList();
                //DtLastRizMetreUsersesShomareh = clsConvert.ToDataTable(varDtLastRizMetreUsersesShomareh1);
                //Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='080501'");
                //FBId = 0;
                //if (DtFBUser.Rows.Count == 0)
                //    FBId = OperationItemsFB.SaveFB(BarAvordID, "080501", 0);
                //else
                //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

                var RizMetreUserLastShomareh1 = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                Shomareh = 0;
                if (RizMetreUserLastShomareh1 != null)
                    Shomareh = RizMetreUserLastShomareh1.Shomareh + 1;
                else
                    Shomareh = 1;

                //////////////////قالب درز انبساط
                //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId);
                //Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                clsRizMetreUsers RizMetre1 = new clsRizMetreUsers();
                RizMetre1.Shomareh = Shomareh;
                RizMetre1.Sharh = "قالب درز انبساط";
                RizMetre1.Tedad = int.Parse(TedadDahaneh.Trim()) + 1;
                RizMetre1.Tool = decimal.Parse(LAbro.Trim()) * 10;
                RizMetre1.Arz = decimal.Parse(j.Trim()) * 10;
                RizMetre1.Ertefa = decimal.Parse(t.Trim()) * 10;
                RizMetre1.Vazn = 0;
                RizMetre1.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre1.FBId = guFBId;
                RizMetre1.OperationsOfHamlId = 1;
                RizMetre1.Type = "300" + PolNum.ToString("D3") + "22";///قالب درز انبساط
                RizMetre1.ForItem = "";
                RizMetre1.UseItem = "";
                _context.RizMetreUserses.Add(RizMetre1);
                _context.SaveChanges();
                //RizMetre.Save();

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public bool GhalebBandiDalPishSakhteh(Guid BarAvordId, Guid PolVaAbroId, int PolNum, string LAbro, string w1, string w2, string w3, string w4,
            string t, string j, string c1, string D, string TedadDahaneh)
        {
            try
            {
                var varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "080701").ToList();
                DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

                Guid guFBId = new Guid();
                if (DtFBUser.Rows.Count == 0)
                {
                    clsFB FBSave = new clsFB();
                    FBSave.BarAvordId = BarAvordId;
                    FBSave.Shomareh = "080701";
                    FBSave.BahayeVahedZarib = 0;
                    _context.FBs.Add(FBSave);
                    _context.SaveChanges();
                    guFBId = FBSave.ID;
                }
                else
                    guFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
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

                //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='080701'");

                //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
                //int FBId = 0;
                //if (DtFBUser.Rows.Count == 0)
                //    FBId = OperationItemsFB.SaveFB(BarAvordID, "080701", 0);
                //else
                //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

                //clsRizMetreUsersses RizMetre = new clsRizMetreUsersses();
                ////////////////قالب دال پیش ساخته
                //DataTable DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId);
                //int Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = Shomareh;
                RizMetre.Sharh = "قالب دال پیش ساخته";
                RizMetre.Tedad = int.Parse(TedadDahaneh.Trim());
                RizMetre.Tool = decimal.Parse(LAbro.Trim());
                RizMetre.Arz = int.Parse(D.Trim()) + (2 * decimal.Parse(c1.Trim())) - (2 * decimal.Parse(j.Trim()));
                RizMetre.Ertefa = 0;
                RizMetre.Vazn = 0;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId;
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "300" + PolNum.ToString("D3") + "52";///قالب دال پیش ساخته
                RizMetre.ForItem = "";
                RizMetre.UseItem = "";
                _context.RizMetreUserses.Add(RizMetre);
                _context.SaveChanges();
                //RizMetre.Save();
                ///////
                RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = ++Shomareh;
                RizMetre.Sharh = "قالب دال پیش ساخته";
                RizMetre.Tedad = int.Parse(TedadDahaneh.Trim());
                RizMetre.Tool = decimal.Parse(LAbro.Trim());
                RizMetre.Arz = 0;
                RizMetre.Ertefa = decimal.Parse(t.Trim());
                RizMetre.Vazn = 0;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId;
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "300" + PolNum.ToString("D3") + "52";///قالب دال پیش ساخته
                RizMetre.ForItem = "";
                RizMetre.UseItem = "";
                _context.RizMetreUserses.Add(RizMetre);
                _context.SaveChanges();
                //RizMetre.Save();
                /////////////
                RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = ++Shomareh;
                RizMetre.Sharh = "قالب دال پیش ساخته";
                RizMetre.Tedad = int.Parse(TedadDahaneh.Trim());
                RizMetre.Tool = 2 * decimal.Parse(LAbro.Trim());
                RizMetre.Arz = int.Parse(D.Trim()) + (2 * decimal.Parse(c1.Trim())) - (2 * decimal.Parse(j.Trim()));
                RizMetre.Ertefa = decimal.Parse(t.Trim());
                RizMetre.Vazn = 0;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId;
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "300" + PolNum.ToString("D3") + "52";///قالب دال پیش ساخته
                RizMetre.ForItem = "";
                RizMetre.UseItem = "";
                _context.RizMetreUserses.Add(RizMetre);
                _context.SaveChanges();

                //RizMetre.Save();
                //////////

                varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "080702").ToList();
                DtFBUser = clsConvert.ToDataTable(varFBUser);

                guFBId = new Guid();
                if (DtFBUser.Rows.Count == 0)
                {
                    clsFB FBSave = new clsFB();
                    FBSave.BarAvordId = BarAvordId;
                    FBSave.Shomareh = "080702";
                    FBSave.BahayeVahedZarib = 0;
                    _context.FBs.Add(FBSave);
                    _context.SaveChanges();
                    guFBId = FBSave.ID;
                }
                else
                    guFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
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

                //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='080702'");

                //FBId = 0;
                //if (DtFBUser.Rows.Count == 0)
                //    FBId = OperationItemsFB.SaveFB(BarAvordID, "080701", 0);
                //else
                //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

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
                    RizMetre.Sharh = "قالب دال پیش ساخته";
                    RizMetre.Tedad = int.Parse(TedadDahaneh.Trim()) * dSumwith24;
                    RizMetre.Tool = decimal.Parse(LAbro.Trim());
                    RizMetre.Arz = int.Parse(D.Trim()) + (2 * decimal.Parse(c1.Trim())) - (2 * decimal.Parse(j.Trim()));
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = 0;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "300" + PolNum.ToString("D3") + "52";///قالب دال پیش ساخته
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();
                    //RizMetre.Save();
                    ///////
                    RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = ++Shomareh;
                    RizMetre.Sharh = "قالب دال پیش ساخته";
                    RizMetre.Tedad = int.Parse(TedadDahaneh.Trim()) * dSumwith24;
                    RizMetre.Tool = decimal.Parse(LAbro.Trim());
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = decimal.Parse(t.Trim());
                    RizMetre.Vazn = 0;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "300" + PolNum.ToString("D3") + "52";///قالب دال پیش ساخته
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();
                    //RizMetre.Save();
                    /////////////
                    RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = ++Shomareh;
                    RizMetre.Sharh = "قالب دال پیش ساخته";
                    RizMetre.Tedad = int.Parse(TedadDahaneh.Trim()) * dSumwith24;
                    RizMetre.Tool = 2 * decimal.Parse(LAbro.Trim());
                    RizMetre.Arz = int.Parse(D.Trim()) + (2 * decimal.Parse(c1.Trim())) - (2 * decimal.Parse(j.Trim()));
                    RizMetre.Ertefa = decimal.Parse(t.Trim());
                    RizMetre.Vazn = 0;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "300" + PolNum.ToString("D3") + "52";///قالب دال پیش ساخته
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();
                    //RizMetre.Save();
                }

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public bool Boton(Guid PolVaAbroId, int PolNum, Guid BarAvordId, string LAbro, string w1, string w2, string w3, string w4,
        string f, string m, string n, string k, string h, string t, string b1, string b2, string j, string c1, string c2
            , string p1, string p2, string D, string TedadDahaneh, string NahveEjraDal)
        {
            AbnieFaniEzafeBahaCommon abnieFaniEzafeBahaCommon = new AbnieFaniEzafeBahaCommon(_context);
            /////////////////
            int countWIsZero = 0;
            long intShomareh120302 = 0;
            long intShomareh120303 = 0;
            long intShomareh120304 = 0;
            long intShomareh120305 = 0;
            long intShomareh120310 = 0;

            countWIsZero = decimal.Parse(w1.Trim()) == 0 ? countWIsZero++ : countWIsZero;
            countWIsZero = decimal.Parse(w2.Trim()) == 0 ? countWIsZero++ : countWIsZero;
            countWIsZero = decimal.Parse(w3.Trim()) == 0 ? countWIsZero++ : countWIsZero;
            countWIsZero = decimal.Parse(w4.Trim()) == 0 ? countWIsZero++ : countWIsZero;

            var varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "120302").ToList();
            DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

            Guid guFBId120302 = new Guid();
            if (DtFBUser.Rows.Count == 0)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "120302";
                FBSave.BahayeVahedZarib = 0;
                _context.FBs.Add(FBSave);
                _context.SaveChanges();
                guFBId120302 = FBSave.ID;
            }
            else
                guFBId120302 = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
            ////////////////
            var RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId120302).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            if (RizMetreUserLastShomareh != null)
                intShomareh120302 = RizMetreUserLastShomareh.Shomareh + 1;
            else
                intShomareh120302 = 1;

            DataTable DtLastRizMetreUsersesShomareh = new DataTable();
            //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
            /////////////محاسبه اضافه بهای بتن دیوار کوله
            //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='120302'");
            //int FBId120302 = 0;
            //if (DtFBUser.Rows.Count == 0)
            //    FBId120302 = OperationItemsFB.SaveFB(BarAvordID, "120302", 0);
            //else
            //    FBId120302 = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
            //DataTable DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId120302);
            //intShomareh120302 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            //////////

            varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "120303").ToList();
            DtFBUser = clsConvert.ToDataTable(varFBUser);

            Guid guFBId120303 = new Guid();
            if (DtFBUser.Rows.Count == 0)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "120303";
                FBSave.BahayeVahedZarib = 0;
                _context.FBs.Add(FBSave);
                _context.SaveChanges();
                guFBId120303 = FBSave.ID;
            }
            else
                guFBId120303 = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
            ////////////////
            //varDtLastRizMetreUsersesShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId120303).ToList();
            //DtLastRizMetreUsersesShomareh = clsConvert.ToDataTable(varDtLastRizMetreUsersesShomareh);
            //intShomareh120303 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());

            RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId120303).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            if (RizMetreUserLastShomareh != null)
                intShomareh120303 = RizMetreUserLastShomareh.Shomareh + 1;
            else
                intShomareh120303 = 1;

            //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='120303'");
            //int FBId120303 = 0;
            //if (DtFBUser.Rows.Count == 0)
            //    FBId120303 = OperationItemsFB.SaveFB(BarAvordID, "120303", 0);
            //else
            //    FBId120303 = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
            //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId120303);
            //intShomareh120303 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            //////////

            varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "120304").ToList();
            DtFBUser = clsConvert.ToDataTable(varFBUser);

            Guid guFBId120304 = new Guid();
            if (DtFBUser.Rows.Count == 0)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "120304";
                FBSave.BahayeVahedZarib = 0;
                _context.FBs.Add(FBSave);
                _context.SaveChanges();
                guFBId120304 = FBSave.ID;
            }
            else
                guFBId120304 = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
            ////////////////
            //varDtLastRizMetreUsersesShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId120304).ToList();
            //DtLastRizMetreUsersesShomareh = clsConvert.ToDataTable(varDtLastRizMetreUsersesShomareh);
            //intShomareh120304 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());

            RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId120304).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            if (RizMetreUserLastShomareh != null)
                intShomareh120304 = RizMetreUserLastShomareh.Shomareh + 1;
            else
                intShomareh120304 = 1;

            //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='120304'");
            //int FBId120304 = 0;
            //if (DtFBUser.Rows.Count == 0)
            //    FBId120304 = OperationItemsFB.SaveFB(BarAvordID, "120304", 0);
            //else
            //    FBId120304 = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
            //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId120304);
            //intShomareh120304 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            //////////

            varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "120305").ToList();
            DtFBUser = clsConvert.ToDataTable(varFBUser);

            Guid guFBId120305 = new Guid();
            if (DtFBUser.Rows.Count == 0)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "120305";
                FBSave.BahayeVahedZarib = 0;
                _context.FBs.Add(FBSave);
                _context.SaveChanges();
                guFBId120305 = FBSave.ID;
            }
            else
                guFBId120305 = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
            ////////////////
            RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId120305).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            if (RizMetreUserLastShomareh != null)
                intShomareh120305 = RizMetreUserLastShomareh.Shomareh + 1;
            else
                intShomareh120305 = 1;

            //varDtLastRizMetreUsersesShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId120305).ToList();
            //DtLastRizMetreUsersesShomareh = clsConvert.ToDataTable(varDtLastRizMetreUsersesShomareh);
            //intShomareh120305 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='120305'");
            //int FBId120305 = 0;
            //if (DtFBUser.Rows.Count == 0)
            //    FBId120305 = OperationItemsFB.SaveFB(BarAvordID, "120305", 0);
            //else
            //    FBId120305 = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
            //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId120305);
            //intShomareh120305 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            ////////////////

            varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "120310").ToList();
            DtFBUser = clsConvert.ToDataTable(varFBUser);

            Guid guFBId120310 = new Guid();
            if (DtFBUser.Rows.Count == 0)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "120310";
                FBSave.BahayeVahedZarib = 0;
                _context.FBs.Add(FBSave);
                _context.SaveChanges();
                guFBId120310 = FBSave.ID;
            }
            else
                guFBId120310 = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
            ////////////////
            RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId120310).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            if (RizMetreUserLastShomareh != null)
                intShomareh120310 = RizMetreUserLastShomareh.Shomareh + 1;
            else
                intShomareh120310 = 1;

            //varDtLastRizMetreUsersesShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId120310).ToList();
            //DtLastRizMetreUsersesShomareh = clsConvert.ToDataTable(varDtLastRizMetreUsersesShomareh);
            //intShomareh120310 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='120310'");
            //int FBId120310 = 0;
            //if (DtFBUser.Rows.Count == 0)
            //    FBId120310 = OperationItemsFB.SaveFB(BarAvordID, "120310", 0);
            //else
            //    FBId120310 = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
            //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId120310);
            //intShomareh120310 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            ////////////////
            //DataTable DtQuesForAbnieFaniValues = tblQuesForAbnieFani.ListWithParameterQuesForAbnieFaniValues("PolVaAbroId='" + PolVaAbroId + "'");

            string strParam = "PolVaAbroId='" + PolVaAbroId + "'";
            var QuesForAbnieFaniValues = new SqlParameter("@Parameter", strParam);

            var QuesForAbnieFani = _context.Set<QuesForAbnieFaniValuesDto>()
                .FromSqlRaw("EXEC QuesForAbnieFaniValuesListWithParameter @Parameter", QuesForAbnieFaniValues)
                .ToList();

            DataTable DtQuesForAbnieFaniValues = clsConvert.ToDataTable(QuesForAbnieFani);

            //DataTable DtQuesForAbnieFaniValues = clsAbnieFaniQueries.ListWithParameterQuesForAbnieFaniValues("PolVaAbroId=" + PolVaAbroId);


            string strShomareh = DtQuesForAbnieFaniValues.Rows[0]["Shomareh"].ToString().Trim();
            varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strShomareh).ToList();
            DtFBUser = clsConvert.ToDataTable(varFBUser);
            Guid guFBId = new Guid();
            if (DtFBUser.Rows.Count == 0)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = strShomareh;
                FBSave.BahayeVahedZarib = 0;
                _context.FBs.Add(FBSave);
                _context.SaveChanges();
                guFBId = FBSave.ID;
            }
            else
                guFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
            ////////////////

            RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            long Shomareh = 0;
            if (RizMetreUserLastShomareh != null)
                Shomareh = RizMetreUserLastShomareh.Shomareh + 1;
            else
                Shomareh = 1;

            //varDtLastRizMetreUsersesShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).ToList();
            //DtLastRizMetreUsersesShomareh = clsConvert.ToDataTable(varDtLastRizMetreUsersesShomareh);
            //int Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='" + DtQuesForAbnieFaniValues.Rows[0]["Shomareh"].ToString().Trim() + "'");
            //int FBId = 0;
            //if (DtFBUser.Rows.Count == 0)
            //    FBId = OperationItemsFB.SaveFB(BarAvordID, DtQuesForAbnieFaniValues.Rows[0]["Shomareh"].ToString().Trim(), 0);
            //else
            //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
            ////////////////بتن مگر پی کوله
            //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId);
            //int Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = Shomareh;
            RizMetre.Sharh = "بتن مگر پی کوله";
            RizMetre.Tedad = 2;
            RizMetre.Tool = decimal.Parse(LAbro.Trim()) + decimal.Parse(DtQuesForAbnieFaniValues.Rows[0]["Value"].ToString().Trim()) / 100;
            RizMetre.Arz = decimal.Parse(f.Trim()) + decimal.Parse(DtQuesForAbnieFaniValues.Rows[0]["Value"].ToString().Trim()) / 100;
            RizMetre.Ertefa = decimal.Parse("0.1");
            RizMetre.Vazn = 0;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "300" + PolNum.ToString("D3") + "30";///بتن مگر پی کوله
            RizMetre.ForItem = "";
            RizMetre.UseItem = "";
            _context.RizMetreUserses.Add(RizMetre);
            _context.SaveChanges();
            //RizMetre.Save();
            //////////بتن مگر پی پایه میانی
            int tedad = int.Parse(TedadDahaneh) - 1;
            if (tedad != 0)
            {
                RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = ++Shomareh;
                RizMetre.Sharh = "بتن مگر پی پایه میانی";
                RizMetre.Tedad = tedad;
                RizMetre.Tool = decimal.Parse(LAbro.Trim()) + decimal.Parse(DtQuesForAbnieFaniValues.Rows[0]["Value"].ToString().Trim()) / 100;
                RizMetre.Arz = decimal.Parse(k.Trim()) + decimal.Parse(DtQuesForAbnieFaniValues.Rows[0]["Value"].ToString().Trim()) / 100;
                RizMetre.Ertefa = decimal.Parse("0.1");
                RizMetre.Vazn = 0;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId;
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "300" + PolNum.ToString("D3") + "31";///بتن مگر پی پایه میانی
                RizMetre.ForItem = "";
                RizMetre.UseItem = "";
                _context.RizMetreUserses.Add(RizMetre);
                _context.SaveChanges();
                //RizMetre.Save();
            }
            /////////////////
            varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "120104").ToList();
            DtFBUser = clsConvert.ToDataTable(varFBUser);

            guFBId = new Guid();
            if (DtFBUser.Rows.Count == 0)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "120104";
                FBSave.BahayeVahedZarib = 0;
                _context.FBs.Add(FBSave);
                _context.SaveChanges();
                guFBId = FBSave.ID;
            }
            else
                guFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
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

            //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='120104'");
            //FBId = 0;
            //if (DtFBUser.Rows.Count == 0)
            //    FBId = OperationItemsFB.SaveFB(BarAvordID, "120104", 0);
            //else
            //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

            //////////////////بتن پی کوله
            //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId);
            //Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = Shomareh;
            RizMetre.Sharh = "بتن پی کوله";
            RizMetre.Tedad = 2;
            RizMetre.Tool = decimal.Parse(LAbro.Trim());
            RizMetre.Arz = decimal.Parse(f.Trim());
            RizMetre.Ertefa = decimal.Parse(m.Trim());
            RizMetre.Vazn = 0;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "300" + PolNum.ToString("D3") + "32";///بتن پی کوله
            RizMetre.ForItem = "";
            RizMetre.UseItem = "";
            _context.RizMetreUserses.Add(RizMetre);
            _context.SaveChanges();
            //RizMetre.Save();
            //////////بتن پی پایه میانی
            tedad = int.Parse(TedadDahaneh) - 1;
            if (tedad != 0)
            {
                RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = ++Shomareh;
                RizMetre.Sharh = "بتن پی پایه میانی";
                RizMetre.Tedad = tedad;
                RizMetre.Tool = decimal.Parse(LAbro.Trim());
                RizMetre.Arz = decimal.Parse(k.Trim());
                RizMetre.Ertefa = decimal.Parse(n.Trim());
                RizMetre.Vazn = 0;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId;
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "300" + PolNum.ToString("D3") + "33";///بتن پی پایه میانی 
                RizMetre.ForItem = "";
                RizMetre.UseItem = "";
                _context.RizMetreUserses.Add(RizMetre);
                _context.SaveChanges();
                //RizMetre.Save();
            }
            ////////////////بتن دیوار کوله
            RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = ++Shomareh;
            RizMetre.Sharh = "بتن دیوار کوله";
            RizMetre.Tedad = 2;
            RizMetre.Tool = decimal.Parse(LAbro.Trim());
            RizMetre.Arz = (decimal.Parse(b1.Trim()) + decimal.Parse(b2.Trim())) / 2;
            RizMetre.Ertefa = decimal.Parse(h.Trim()) - decimal.Parse("0.3");
            RizMetre.Vazn = 0;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "300" + PolNum.ToString("D3") + "34";///بتن دیوار کوله
            RizMetre.ForItem = "";
            RizMetre.UseItem = "";
            _context.RizMetreUserses.Add(RizMetre);
            _context.SaveChanges();
            //RizMetre.Save();
            ////////////////اضافه بها بتن دیوار کوله
            if (decimal.Parse(h.Trim()) - decimal.Parse("0.3") <= 5)
            {
                string strShomareh120302 = abnieFaniEzafeBahaCommon.BotonDivarKooleEzafeBahaLow5(PolVaAbroId, PolNum, TedadDahaneh, D, intShomareh120302, LAbro, b1, b2, h, guFBId120302);
                intShomareh120302 = long.Parse(strShomareh120302);
            }
            else if (decimal.Parse(h.Trim()) - decimal.Parse("0.3") > 5 && decimal.Parse(h.Trim()) - decimal.Parse("0.3") <= 10)
            {
                strShomareh = abnieFaniEzafeBahaCommon.BotonDivarKooleEzafeBahaHigh5(PolVaAbroId, PolNum, TedadDahaneh, D, intShomareh120302, intShomareh120303, LAbro, b1, b2, h, guFBId120302, guFBId120303);
                intShomareh120302 = long.Parse(strShomareh.Split("")[0]);
                intShomareh120303 = long.Parse(strShomareh.Split("")[1]);
            }

            ////////////////بتن مخروط پایه میانی (کامل)
            tedad = 4 - (countWIsZero);
            if (tedad != 0)
            {
                if (int.Parse(TedadDahaneh) - 1 != 0)
                {
                    decimal x = 0;
                    if (decimal.Parse(p2.Trim()) != decimal.Parse(p1.Trim()))
                        x = ((decimal.Parse(h.Trim()) - decimal.Parse("0.3")) * decimal.Parse(p2.Trim())) / (decimal.Parse(p1.Trim()) - decimal.Parse(p2.Trim()));
                    RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = ++Shomareh;
                    RizMetre.Sharh = "بتن مخروط پایه میانی (کامل)";
                    RizMetre.Tedad = (int.Parse(TedadDahaneh) - 1) * tedad;
                    RizMetre.Tool = (decimal.Parse(p1.Trim()) * decimal.Parse(p1.Trim())) / 4;
                    RizMetre.Arz = decimal.Parse(Math.PI.ToString());
                    RizMetre.Ertefa = decimal.Parse(h.Trim()) + x;
                    RizMetre.Vazn = 0;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "300" + PolNum.ToString("D3") + "35";///بتن مخروط پایه میانی (کامل)
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();
                    //RizMetre.Save();
                    ////////////
                    if (x != 0)
                    {
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = ++Shomareh;
                        RizMetre.Sharh = "کسر بتن مخروط پایه میانی(بالایی)";
                        RizMetre.Tedad = (int.Parse(TedadDahaneh) - 1) * tedad;
                        RizMetre.Tool = (decimal.Parse(p2.Trim()) * decimal.Parse(p2.Trim())) / 4;
                        RizMetre.Arz = decimal.Parse(Math.PI.ToString());
                        RizMetre.Ertefa = (-1) * x;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "36";///کسر بتن مخروط پایه میانی(بالایی)
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();
                    }

                    ////////////اضافه بهای بتن مخروطی پایه میانی
                    if (decimal.Parse(h.Trim()) - decimal.Parse("0.3") <= 5)
                    {
                        string strShomareh120302 = abnieFaniEzafeBahaCommon.BotonDivarMakhrootiEzafeBahaLow5(PolVaAbroId, PolNum, D, intShomareh120302, LAbro, b1, b2, h, guFBId120302, p1, p2, x, TedadDahaneh, tedad);
                        intShomareh120302 = int.Parse(strShomareh120302);
                    }
                    else if (decimal.Parse(h.Trim()) - decimal.Parse("0.3") > 5 && decimal.Parse(h.Trim()) - decimal.Parse("0.3") <= 10)
                    {
                        strShomareh = abnieFaniEzafeBahaCommon.BotonDivarMakhrootiEzafeBahaHigh5(PolVaAbroId, PolNum, D, intShomareh120302, intShomareh120303, LAbro, b1, b2, h, guFBId120302, guFBId120303, p1, p2, x, TedadDahaneh, tedad);
                        intShomareh120302 = int.Parse(strShomareh.Split("")[0]);
                        intShomareh120303 = int.Parse(strShomareh.Split("")[1]);
                    }
                }
            }

            if (int.Parse(TedadDahaneh) - 1 != 0)
            {
                RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = ++Shomareh;
                RizMetre.Sharh = "بتن دیوار پایه میانی";
                RizMetre.Tedad = (int.Parse(TedadDahaneh) - 1);
                RizMetre.Tool = decimal.Parse(LAbro.Trim());
                RizMetre.Arz = (decimal.Parse(p1.Trim()) + decimal.Parse(p2.Trim())) / 2;
                RizMetre.Ertefa = decimal.Parse(h.Trim()) - decimal.Parse("0.3");
                RizMetre.Vazn = 0;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId;
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "300" + PolNum.ToString("D3") + "37";///بتن دیوار پایه میانی
                RizMetre.ForItem = "";
                RizMetre.UseItem = "";
                _context.RizMetreUserses.Add(RizMetre);

                //RizMetre.Save();

                ////////////////اضافه بها بتن دیوار پایه میانی
                if (decimal.Parse(h.Trim()) - decimal.Parse("0.3") <= 5)
                {
                    string strShomareh120302 = abnieFaniEzafeBahaCommon.BotonDivarPayeMianiEzafeBahaLow5(PolVaAbroId, PolNum, D, intShomareh120302, LAbro, b1, b2, h, guFBId120302
                        , p1, p1, TedadDahaneh);
                    intShomareh120302 = int.Parse(strShomareh120302);
                }
                else if (decimal.Parse(h.Trim()) - decimal.Parse("0.3") > 5 && decimal.Parse(h.Trim()) - decimal.Parse("0.3") <= 10)
                {
                    strShomareh = abnieFaniEzafeBahaCommon.BotonDivarPayeMianiEzafeBahaHigh5(PolVaAbroId, PolNum, D, intShomareh120302, intShomareh120303, LAbro, b1, b2, h, guFBId120302, guFBId120303
                        , p1, p1, TedadDahaneh);
                    intShomareh120302 = int.Parse(strShomareh.Split("")[0]);
                    intShomareh120303 = int.Parse(strShomareh.Split("")[1]);
                }
            }
            ////////////////////
            //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='120106'");
            //FBId = 0;
            //if (DtFBUser.Rows.Count == 0)
            //    FBId = OperationItemsFB.SaveFB(BarAvordID, "120106", 0);
            //else
            //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

            //varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "120106").ToList();
            //DtFBUser = clsConvert.ToDataTable(varFBUser);

            varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "120106").ToList();
            DtFBUser = clsConvert.ToDataTable(varFBUser);

            guFBId = new Guid();
            if (DtFBUser.Rows.Count == 0)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "120106";
                FBSave.BahayeVahedZarib = 0;
                _context.FBs.Add(FBSave);
                _context.SaveChanges();
                guFBId = FBSave.ID;
            }
            else
                guFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
            ////////////////
            //var varDtLastRizMetreUsersesShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).ToList();
            //DtLastRizMetreUsersesShomareh = clsConvert.ToDataTable(varDtLastRizMetreUsersesShomareh);
            //Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());

            RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            Shomareh = 0;
            if (RizMetreUserLastShomareh != null)
                Shomareh = RizMetreUserLastShomareh.Shomareh + 1;
            else
                Shomareh = 1;

            ////////////////بتن شناژ کوله
            //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId);
            //Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = Shomareh;
            RizMetre.Sharh = "بتن شناژ کوله";
            RizMetre.Tedad = 2;
            RizMetre.Tool = decimal.Parse(LAbro.Trim());
            RizMetre.Arz = decimal.Parse(b2.Trim());
            RizMetre.Ertefa = decimal.Parse("0.3");
            RizMetre.Vazn = 0;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "300" + PolNum.ToString("D3") + "38";///بتن شناژ کوله
            RizMetre.ForItem = "";
            RizMetre.UseItem = "";
            _context.RizMetreUserses.Add(RizMetre);
            _context.SaveChanges();
            //RizMetre.Save();
            //////////بتن شناژ کوله
            RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = ++Shomareh;
            RizMetre.Sharh = "بتن شناژ کوله";
            RizMetre.Tedad = 2;
            RizMetre.Tool = decimal.Parse(LAbro.Trim());
            RizMetre.Arz = decimal.Parse(c1.Trim());
            RizMetre.Ertefa = decimal.Parse(t);
            RizMetre.Vazn = 0;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "300" + PolNum.ToString("D3") + "38";///بتن شناژ کوله
            RizMetre.ForItem = "";
            RizMetre.UseItem = "";
            _context.RizMetreUserses.Add(RizMetre);
            _context.SaveChanges();
            //RizMetre.Save();
            ////////////////اضافه بها بتن شناژ کوله
            if (decimal.Parse(h.Trim()) + decimal.Parse(t.Trim()) <= 5)
            {
                string strShomareh120302 = abnieFaniEzafeBahaCommon.BotonShenajKooleEzafeBahaState1(PolVaAbroId, PolNum, TedadDahaneh, D, intShomareh120302, LAbro, b1, b2, h, guFBId120302, c1, t);
                intShomareh120302 = long.Parse(strShomareh120302);
            }
            else if (decimal.Parse(h.Trim()) - decimal.Parse("0.3") >= 5 && decimal.Parse(h.Trim()) + decimal.Parse(t.Trim()) <= 10)
            {
                string strShomareh120303 = abnieFaniEzafeBahaCommon.BotonShenajKooleEzafeBahaState2(PolVaAbroId, PolNum, TedadDahaneh, D, intShomareh120303, LAbro, b1, b2, h, guFBId120303, c1, t);
                intShomareh120303 = long.Parse(strShomareh120303);
            }
            else if (decimal.Parse(h.Trim()) - decimal.Parse("0.3") < 5 && decimal.Parse(h.Trim()) > 5)
            {
                strShomareh = abnieFaniEzafeBahaCommon.BotonShenajKooleEzafeBahaState3(PolVaAbroId, PolNum, TedadDahaneh, D, intShomareh120302, intShomareh120303, LAbro, b1, b2, h, guFBId120302, guFBId120303, c1, t);
                intShomareh120302 = long.Parse(strShomareh.Split("")[0]);
                intShomareh120303 = long.Parse(strShomareh.Split("")[1]);
            }
            else if (decimal.Parse(h.Trim()) < 5 && decimal.Parse(h.Trim()) + decimal.Parse(t.Trim()) > 5)
            {
                strShomareh = abnieFaniEzafeBahaCommon.BotonShenajKooleEzafeBahaState4(PolVaAbroId, PolNum, TedadDahaneh, D, intShomareh120302, intShomareh120303, LAbro, b1, b2, h, guFBId120302, guFBId120303, c1, t);
                intShomareh120302 = long.Parse(strShomareh.Split("")[0]);
                intShomareh120303 = long.Parse(strShomareh.Split("")[1]);
            }

            string strShomareh120310 = abnieFaniEzafeBahaCommon.BotonShenajKooleEzafeBahaState1(PolVaAbroId, PolNum, TedadDahaneh, D, intShomareh120310, LAbro, b1, b2, h, guFBId120310, c1, t);
            intShomareh120310 = long.Parse(strShomareh120310);

            //////////بتن شناژ پایه میانی
            tedad = (int.Parse(TedadDahaneh.Trim())) - 1;
            if (tedad != 0)
            {
                RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = ++Shomareh;
                RizMetre.Sharh = "بتن شناژ پایه میانی";
                RizMetre.Tedad = tedad;
                RizMetre.Tool = decimal.Parse(LAbro.Trim());
                RizMetre.Arz = decimal.Parse(p2.Trim());
                RizMetre.Ertefa = decimal.Parse("0.3");
                RizMetre.Vazn = 0;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId;
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "300" + PolNum.ToString("D3") + "39";///بتن شناژ پایه میانی
                RizMetre.ForItem = "";
                RizMetre.UseItem = "";
                _context.RizMetreUserses.Add(RizMetre);
                _context.SaveChanges();
                //RizMetre.Save();

                ////////////////اضافه بها بتن شناژ پایه میانی
                if (decimal.Parse(h.Trim()) <= 5)
                {
                    string strShomareh120302 = abnieFaniEzafeBahaCommon.BotonShenajPayeMianiEzafeBahaState1(PolVaAbroId, PolNum, TedadDahaneh, D, intShomareh120302, LAbro, b1, b2, h, guFBId120302, p2, tedad);
                    intShomareh120302 = long.Parse(strShomareh120302);
                }
                else if (decimal.Parse(h.Trim()) > 5 && decimal.Parse(h.Trim()) - decimal.Parse("0.3") < 5)
                {
                    strShomareh = abnieFaniEzafeBahaCommon.BotonShenajPayeMianiEzafeBahaState2(PolVaAbroId, PolNum, TedadDahaneh, D, intShomareh120302, intShomareh120303, LAbro, b1, b2, h, guFBId120302, guFBId120303, p2, tedad);
                    intShomareh120302 = long.Parse(strShomareh.Split("")[0]);
                    intShomareh120303 = long.Parse(strShomareh.Split("")[1]);
                }
                else if (decimal.Parse(h.Trim()) - decimal.Parse("0.3") > 5 && decimal.Parse(h.Trim()) <= 10)
                {
                    string Shomareh120303 = abnieFaniEzafeBahaCommon.BotonShenajPayeMianiEzafeBahaState3(PolVaAbroId, PolNum, TedadDahaneh, D, intShomareh120303, LAbro, b1, b2, h, guFBId120303, p2, tedad);
                    intShomareh120303 = long.Parse(Shomareh120303);
                }

                strShomareh120310 = abnieFaniEzafeBahaCommon.BotonShenajPayeMianiEzafeBahaState1(PolVaAbroId, PolNum, TedadDahaneh, D, intShomareh120310, LAbro, b1, b2, h, guFBId120310, p2, tedad);
                intShomareh120310 = long.Parse(strShomareh120310);

            }
            //////////بتن شناژ مخروطی پایه میانی
            tedad = 4 - countWIsZero;
            if (tedad != 0)
            {
                if (int.Parse(TedadDahaneh.Trim()) - 1 != 0)
                {
                    RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = ++Shomareh;
                    RizMetre.Sharh = "بتن شناژ مخروطی پایه میانی";
                    RizMetre.Tedad = (int.Parse(TedadDahaneh.Trim()) - 1) * tedad;
                    RizMetre.Tool = (decimal.Parse(p2.Trim()) * decimal.Parse(p2.Trim())) / 4;
                    RizMetre.Arz = decimal.Parse(Math.PI.ToString());
                    RizMetre.Ertefa = decimal.Parse("0.3");
                    RizMetre.Vazn = 0;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "300" + PolNum.ToString("D3") + "40";///بتن شناژ مخروطی پایه میانی
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();
                    //RizMetre.Save();

                    ////////////اضافه بهای بتن شناژ مخروطی پایه میانی
                    if (decimal.Parse(h.Trim()) <= 5)
                    {
                        string strShomareh120302 = abnieFaniEzafeBahaCommon.BotonShenajMakhrootiEzafeBahaState1(PolVaAbroId, PolNum, D, intShomareh120302, LAbro, b1, b2, h, guFBId120302, p1, TedadDahaneh, tedad);
                        intShomareh120302 = int.Parse(strShomareh120302);
                    }
                    else if (decimal.Parse(h.Trim()) > 5 && decimal.Parse(h.Trim()) - decimal.Parse("0.3") < 5)
                    {
                        strShomareh = abnieFaniEzafeBahaCommon.BotonShenajMakhrootiEzafeBahaState2(PolVaAbroId, PolNum, D, intShomareh120302, intShomareh120303, LAbro, b1, b2, h, guFBId120302, guFBId120303, p1, TedadDahaneh, tedad);
                        intShomareh120302 = int.Parse(strShomareh.Split("")[0]);
                        intShomareh120303 = int.Parse(strShomareh.Split("")[1]);
                    }
                    else if (decimal.Parse(h.Trim()) - decimal.Parse("0.3") > 5 && decimal.Parse(h.Trim()) <= 10)
                    {
                        string Shomareh120303 = abnieFaniEzafeBahaCommon.BotonShenajMakhrootiEzafeBahaState3(PolVaAbroId, PolNum, D, intShomareh120303, LAbro, b1, b2, h, guFBId120303, p1, TedadDahaneh, tedad);
                        intShomareh120303 = int.Parse(Shomareh120303);
                    }

                    strShomareh120310 = abnieFaniEzafeBahaCommon.BotonShenajMakhrootiEzafeBahaState1(PolVaAbroId, PolNum, D, intShomareh120310, LAbro, b1, b2, h, guFBId120310, p1, TedadDahaneh, tedad);
                    intShomareh120310 = int.Parse(strShomareh120310);
                }
            }


            //////////بتن دال درجا
            if (NahveEjraDal.Trim() == "1")
            {
                RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = ++Shomareh;
                RizMetre.Sharh = "بتن دال";
                RizMetre.Tedad = int.Parse(TedadDahaneh.Trim());
                RizMetre.Tool = decimal.Parse(LAbro.Trim());
                RizMetre.Arz = decimal.Parse(D) + 2 * (decimal.Parse(c1.Trim())) - 2 * (decimal.Parse(j.Trim()));
                RizMetre.Ertefa = decimal.Parse(t.Trim());
                RizMetre.Vazn = 0;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId;
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "300" + PolNum.ToString("D3") + "41";///بتن دال
                RizMetre.ForItem = "";
                RizMetre.UseItem = "";
                _context.RizMetreUserses.Add(RizMetre);
                _context.SaveChanges();
                //RizMetre.Save();
                ////////////اضافه بهای بتن دال
                if (decimal.Parse(h.Trim()) <= 5)
                {
                    string strShomareh120302 = abnieFaniEzafeBahaCommon.BotonDalEzafeBahaState1(PolVaAbroId, PolNum, intShomareh120302, LAbro, b1, b2, h, guFBId120302, c1, j, D, t, TedadDahaneh);
                    intShomareh120302 = int.Parse(strShomareh120302);

                    string strShomareh120304 = abnieFaniEzafeBahaCommon.BotonDalEzafeBahaState1(PolVaAbroId, PolNum, intShomareh120304, LAbro, b1, b2, h, guFBId120304, c1, j, D, t, TedadDahaneh);
                    intShomareh120304 = int.Parse(strShomareh120304);

                }
                else if (decimal.Parse(h.Trim()) > 5 && decimal.Parse(h.Trim()) - decimal.Parse("0.3") < 5)
                {
                    strShomareh = abnieFaniEzafeBahaCommon.BotonDalEzafeBahaState2(PolVaAbroId, PolNum, intShomareh120302, intShomareh120303, LAbro, b1, b2, h, guFBId120302, guFBId120303, c1, j, D, t, TedadDahaneh);
                    intShomareh120302 = int.Parse(strShomareh.Split("")[0]);
                    intShomareh120303 = int.Parse(strShomareh.Split("")[1]);

                    strShomareh = abnieFaniEzafeBahaCommon.BotonDalEzafeBahaState2(PolVaAbroId, PolNum, intShomareh120304, intShomareh120305, LAbro, b1, b2, h, guFBId120304, guFBId120305, c1, j, D, t, TedadDahaneh);
                    intShomareh120304 = int.Parse(strShomareh.Split("")[0]);
                    intShomareh120305 = int.Parse(strShomareh.Split("")[1]);

                }
                else if (decimal.Parse(h.Trim()) - decimal.Parse("0.3") > 5 && decimal.Parse(h.Trim()) <= 10)
                {
                    string Shomareh120303 = abnieFaniEzafeBahaCommon.BotonDalEzafeBahaState3(PolVaAbroId, PolNum, intShomareh120303, LAbro, b1, b2, h, guFBId120303, c1, j, D, t, TedadDahaneh);
                    intShomareh120303 = int.Parse(Shomareh120303);

                    string Shomareh120305 = abnieFaniEzafeBahaCommon.BotonDalEzafeBahaState3(PolVaAbroId, PolNum, intShomareh120305, LAbro, b1, b2, h, guFBId120305, c1, j, D, t, TedadDahaneh);
                    intShomareh120305 = int.Parse(Shomareh120305);
                }

                /////120310
                strShomareh120310 = abnieFaniEzafeBahaCommon.BotonDalEzafeBahaState1(PolVaAbroId, PolNum, intShomareh120310, LAbro, b1, b2, h, guFBId120310, c1, j, D, t, TedadDahaneh);
                intShomareh120310 = int.Parse(strShomareh120310);
            }
            else if (NahveEjraDal.Trim() == "2")
            {
                varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "130101").ToList();
                DtFBUser = clsConvert.ToDataTable(varFBUser);

                guFBId = new Guid();
                if (DtFBUser.Rows.Count == 0)
                {
                    clsFB FBSave = new clsFB();
                    FBSave.BarAvordId = BarAvordId;
                    FBSave.Shomareh = "130101";
                    FBSave.BahayeVahedZarib = 0;
                    _context.FBs.Add(FBSave);
                    _context.SaveChanges();
                    guFBId = FBSave.ID;
                }
                else
                    guFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
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

                //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='130101'");
                //FBId = 0;
                //if (DtFBUser.Rows.Count == 0)
                //    FBId = OperationItemsFB.SaveFB(BarAvordID, "130101", 0);
                //else
                //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

                //////////////////بتن دال پیش ساخته
                //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId);
                //Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = Shomareh;
                RizMetre.Sharh = "بتن دال پیش ساخته";
                RizMetre.Tedad = int.Parse(TedadDahaneh.Trim());
                RizMetre.Tool = decimal.Parse(LAbro.Trim());
                RizMetre.Arz = decimal.Parse(D) + 2 * (decimal.Parse(c1.Trim())) - 2 * (decimal.Parse(j.Trim()));
                RizMetre.Ertefa = decimal.Parse(t.Trim());
                RizMetre.Vazn = 0;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId;
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "300" + PolNum.ToString("D3") + "51";///بتن دال پیش ساخته
                RizMetre.ForItem = "";
                RizMetre.UseItem = "";
                _context.RizMetreUserses.Add(RizMetre);
                _context.SaveChanges();
                //RizMetre.Save();
                ///////////////
                varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "130401").ToList();
                DtFBUser = clsConvert.ToDataTable(varFBUser);

                guFBId = new Guid();
                if (DtFBUser.Rows.Count == 0)
                {
                    clsFB FBSave = new clsFB();
                    FBSave.BarAvordId = BarAvordId;
                    FBSave.Shomareh = "130401";
                    FBSave.BahayeVahedZarib = 0;
                    _context.FBs.Add(FBSave);
                    _context.SaveChanges();
                    guFBId = FBSave.ID;
                }
                else
                    guFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
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

                //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='130401'");
                //FBId = 0;
                //if (DtFBUser.Rows.Count == 0)
                //    FBId = OperationItemsFB.SaveFB(BarAvordID, "130401", 0);
                //else
                //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

                //////////////////بتن دال پیش ساخته
                //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId);
                //Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());

                RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = Shomareh;
                RizMetre.Sharh = "بتن دال پیش ساخته";
                RizMetre.Tedad = int.Parse(TedadDahaneh.Trim());
                RizMetre.Tool = decimal.Parse(LAbro.Trim());
                RizMetre.Arz = decimal.Parse(D) + 2 * (decimal.Parse(c1.Trim())) - 2 * (decimal.Parse(j.Trim()));
                RizMetre.Ertefa = decimal.Parse(t.Trim());
                RizMetre.Vazn = 0;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId;
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "300" + PolNum.ToString("D3") + "51";///بتن دال پیش ساخته
                RizMetre.ForItem = "";
                RizMetre.UseItem = "";
                _context.RizMetreUserses.Add(RizMetre);
                _context.SaveChanges();
                //RizMetre.Save();
            }
            return true;
        }

        public bool Armator(Guid PolVaAbroId, int PolNum, string D, string TedadDahaneh, Guid BarAvordId, string LAbro, string Hs)
        {

            long lngHs = long.Parse(Hs);
            var varAbroDaliDetails = (from tblAbroDaliDetails in _context.AbroDaliDetailses
                                      join tblAbadeKoole in _context.AbadeKooles
                                      on tblAbroDaliDetails.AbadeKooleId equals tblAbadeKoole.Id
                                      join tblHadAksarErtefaKoole in _context.HadAksarErtefaKooles
                                      on tblAbadeKoole.HadAksarErtefaKooleId equals tblHadAksarErtefaKoole.Id
                                      select new
                                      {
                                          tblAbroDaliDetails.Id,
                                          tblAbroDaliDetails.AbadeKooleId,
                                          tblAbroDaliDetails.Pos,
                                          tblAbroDaliDetails.Ghotr,
                                          tblAbroDaliDetails.Tedad,
                                          tblAbroDaliDetails.Fasele,
                                          tblAbroDaliDetails.Tool,
                                          tblAbroDaliDetails.VazMilgard1M,
                                          tblAbroDaliDetails.VazMilgardSE,
                                          tblHadAksarErtefaKoole.TedadDahaneh,
                                          tblHadAksarErtefaKoole.DahaneAbro,
                                          tblAbadeKoole.Hs,
                                          AbadeKooleID = tblAbadeKoole.Id
                                      }).Where(x => x.AbadeKooleID == lngHs).ToList();
            DataTable DtAbroDaliDetails = clsConvert.ToDataTable(varAbroDaliDetails);
            //DataTable DtAbroDaliDetails = clsAbnieFaniQueries.ListWithParameterAbroDaliDetails("tblAbadeKoole.Id=" + Hs);

            string[,] GhotrVazn ={{"8","10","12","14","16","18","20","22","25","28","32"},
                                  {"0.345","0.617","0.888","1.21","1.58","2","2.47","2.98"
                                      ,"3.85","4.83","6.31"}};

            var varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "090204").ToList();
            DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

            Guid guFBId090204 = new Guid();
            if (DtFBUser.Rows.Count == 0)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "090204";
                FBSave.BahayeVahedZarib = 0;
                _context.FBs.Add(FBSave);
                _context.SaveChanges();
                guFBId090204 = FBSave.ID;
            }
            else
                guFBId090204 = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
            ////////////////
            //var varDtLastRizMetreUsersesShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId090204).ToList();
            //DataTable DtLastRizMetreUsersesShomareh = clsConvert.ToDataTable(varDtLastRizMetreUsersesShomareh);
            //int Shomareh090204 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());

            var RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId090204).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            long Shomareh090204 = 0;
            if (RizMetreUserLastShomareh != null)
                Shomareh090204 = RizMetreUserLastShomareh.Shomareh + 1;
            else
                Shomareh090204 = 1;

            //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
            //clsRizMetreUsersses RizMetre = new clsRizMetreUsersses();
            /////////////////
            //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='090204'");
            //int FBId090204 = 0;
            //if (DtFBUser.Rows.Count == 0)
            //    FBId090204 = OperationItemsFB.SaveFB(BarAvordID, "090204", 0);
            //else
            //    FBId090204 = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
            //DataTable DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId090204);
            //int Shomareh090204 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            ///////////////
            //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='090205'");
            //int FBId090205 = 0;
            //if (DtFBUser.Rows.Count == 0)
            //    FBId090205 = OperationItemsFB.SaveFB(BarAvordID, "090205", 0);
            //else
            //    FBId090205 = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
            //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId090205);
            //int Shomareh090205 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());

            varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "090205").ToList();
            DtFBUser = clsConvert.ToDataTable(varFBUser);

            Guid guFBId090205 = new Guid();
            if (DtFBUser.Rows.Count == 0)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "090205";
                FBSave.BahayeVahedZarib = 0;
                _context.FBs.Add(FBSave);
                _context.SaveChanges();
                guFBId090205 = FBSave.ID;
            }
            else
                guFBId090205 = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
            ////////////////
            //varDtLastRizMetreUsersesShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId090205).ToList();
            //DtLastRizMetreUsersesShomareh = clsConvert.ToDataTable(varDtLastRizMetreUsersesShomareh);
            //int Shomareh090205 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());

            RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId090205).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            long Shomareh090205 = 0;
            if (RizMetreUserLastShomareh != null)
                Shomareh090205 = RizMetreUserLastShomareh.Shomareh + 1;
            else
                Shomareh090205 = 1;

            ///////////////
            varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "090206").ToList();
            DtFBUser = clsConvert.ToDataTable(varFBUser);

            Guid guFBId090206 = new Guid();
            if (DtFBUser.Rows.Count == 0)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "090206";
                FBSave.BahayeVahedZarib = 0;
                _context.FBs.Add(FBSave);
                _context.SaveChanges();
                guFBId090206 = FBSave.ID;
            }
            else
                guFBId090206 = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
            ////////////////
            //varDtLastRizMetreUsersesShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId090206).ToList();
            //DtLastRizMetreUsersesShomareh = clsConvert.ToDataTable(varDtLastRizMetreUsersesShomareh);
            //int Shomareh090206 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());

            RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId090206).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            long Shomareh090206 = 0;
            if (RizMetreUserLastShomareh != null)
                Shomareh090206 = RizMetreUserLastShomareh.Shomareh + 1;
            else
                Shomareh090206 = 1;

            //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='090206'");
            //int FBId090206 = 0;
            //if (DtFBUser.Rows.Count == 0)
            //    FBId090206 = OperationItemsFB.SaveFB(BarAvordID, "090206", 0);
            //else
            //    FBId090206 = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
            //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId090206);
            //int Shomareh090206 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            ///////////////

            for (int i = 0; i < DtAbroDaliDetails.Rows.Count; i++)
            {
                int intGhotr = int.Parse(DtAbroDaliDetails.Rows[i]["Ghotr"].ToString().Trim());
                decimal dTedad = decimal.Parse(DtAbroDaliDetails.Rows[i]["Tedad"].ToString().Trim());
                int intPos = int.Parse(DtAbroDaliDetails.Rows[i]["Pos"].ToString().Trim());
                decimal dVaz = 0;
                for (int jj = 0; jj < GhotrVazn.Length / 2; jj++)
                {
                    if (int.Parse(GhotrVazn[0, jj]) == intGhotr)
                        dVaz = decimal.Parse(GhotrVazn[1, jj]);
                }

                if (intGhotr == 10)
                {
                    if (intPos == 3 || intPos == 5 || intPos == 6 || intPos == 8)
                    {
                        decimal dLAbro = decimal.Parse(LAbro.Trim()) - decimal.Parse("0.1") + decimal.Parse("0.024") * intGhotr;
                        if (dLAbro >= 12 && dLAbro < 24)
                        {
                            decimal dLAbroPrim = dLAbro + decimal.Parse("0.056") * intGhotr;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh090204++;
                            RizMetre.Sharh = "pos " + intPos.ToString();
                            RizMetre.Tedad = dTedad;
                            RizMetre.Tool = Math.Round(dLAbroPrim, 2);
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = 0;
                            RizMetre.Vazn = dVaz;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId090204;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "300" + PolNum.ToString("D3") + "50";///آرماتور
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }
                        else if (dLAbro >= 24 && dLAbro < 36)
                        {
                            decimal dLAbroPrim = dLAbro + 2 * decimal.Parse("0.056") * intGhotr;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh090204++;
                            RizMetre.Sharh = "pos " + intPos.ToString();
                            RizMetre.Tedad = dTedad;
                            RizMetre.Tool = Math.Round(dLAbroPrim, 2); ;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = 0;
                            RizMetre.Vazn = dVaz;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId090204;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "300" + PolNum.ToString("D3") + "50";///آرماتور
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }
                        else if (dLAbro >= 36 && dLAbro < 48)
                        {
                            decimal dLAbroPrim = dLAbro + 3 * decimal.Parse("0.056") * intGhotr;
                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh090204++;
                            RizMetre.Sharh = "pos " + intPos.ToString();
                            RizMetre.Tedad = dTedad;
                            RizMetre.Tool = Math.Round(dLAbroPrim, 2);
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = 0;
                            RizMetre.Vazn = dVaz;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId090204;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "300" + PolNum.ToString("D3") + "50";///آرماتور
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }
                        else
                        {
                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh090204++;
                            RizMetre.Sharh = "pos " + intPos.ToString();
                            RizMetre.Tedad = dTedad;
                            RizMetre.Tool = Math.Round(dLAbro, 2);
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = 0;
                            RizMetre.Vazn = dVaz;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId090204;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "300" + PolNum.ToString("D3") + "50";///آرماتور
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }
                    }
                    else if (intPos == 1 || intPos == 2 || intPos == 4 || intPos == 7
                             || intPos == 9 || intPos == 10)
                    {
                        decimal dFasele = decimal.Parse(DtAbroDaliDetails.Rows[i]["Fasele"].ToString().Trim());
                        decimal dTool = decimal.Parse(DtAbroDaliDetails.Rows[i]["Tool"].ToString().Trim());
                        decimal dTedadNew = Math.Round((decimal.Parse(LAbro.Trim()) - decimal.Parse("0.1")) / (dFasele / 100) + 1);

                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh090204++;
                        RizMetre.Sharh = "pos " + intPos.ToString();
                        RizMetre.Tedad = dTedadNew;
                        RizMetre.Tool = dTool;
                        RizMetre.Arz = 0;
                        RizMetre.Ertefa = 0;
                        RizMetre.Vazn = dVaz;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId090204;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "50";///آرماتور
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();
                    }
                }
                else if (intGhotr <= 18 && 10 < intGhotr)
                {
                    if (intPos == 3 || intPos == 5 || intPos == 6 || intPos == 8)
                    {
                        decimal dLAbro = decimal.Parse(LAbro.Trim()) - decimal.Parse("0.1") + decimal.Parse("0.024") * intGhotr;
                        if (dLAbro >= 12 && dLAbro < 24)
                        {
                            decimal dLAbroPrim = dLAbro + decimal.Parse("0.056") * intGhotr;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh090205++;
                            RizMetre.Sharh = "pos " + intPos.ToString();
                            RizMetre.Tedad = dTedad;
                            RizMetre.Tool = Math.Round(dLAbroPrim, 2);
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = 0;
                            RizMetre.Vazn = dVaz;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId090205;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "300" + PolNum.ToString("D3") + "50";///آرماتور
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }
                        else if (dLAbro >= 24 && dLAbro < 36)
                        {
                            decimal dLAbroPrim = dLAbro + 2 * decimal.Parse("0.056") * intGhotr;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh090205++;
                            RizMetre.Sharh = "pos " + intPos.ToString();
                            RizMetre.Tedad = dTedad;
                            RizMetre.Tool = Math.Round(dLAbroPrim, 2);
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = 0;
                            RizMetre.Vazn = dVaz;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId090205;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "300" + PolNum.ToString("D3") + "50";///آرماتور
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            //RizMetre.Save();
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                        }
                        else if (dLAbro >= 36 && dLAbro < 48)
                        {
                            decimal dLAbroPrim = dLAbro + 3 * decimal.Parse("0.056") * intGhotr;
                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh090205++;
                            RizMetre.Sharh = "pos " + intPos.ToString();
                            RizMetre.Tedad = dTedad;
                            RizMetre.Tool = Math.Round(dLAbroPrim, 2);
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = 0;
                            RizMetre.Vazn = dVaz;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId090205;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "300" + PolNum.ToString("D3") + "50";///آرماتور
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }
                        else
                        {
                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh090205++;
                            RizMetre.Sharh = "pos " + intPos.ToString();
                            RizMetre.Tedad = dTedad;
                            RizMetre.Tool = Math.Round(dLAbro, 2);
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = 0;
                            RizMetre.Vazn = dVaz;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId090205;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "300" + PolNum.ToString("D3") + "50";///آرماتور
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }
                    }
                    else if (intPos == 1 || intPos == 2 || intPos == 4 || intPos == 7
                             || intPos == 9 || intPos == 10)
                    {
                        decimal dFasele = decimal.Parse(DtAbroDaliDetails.Rows[i]["Fasele"].ToString().Trim());
                        decimal dTool = decimal.Parse(DtAbroDaliDetails.Rows[i]["Tool"].ToString().Trim());
                        decimal dTedadNew = Math.Round((decimal.Parse(LAbro.Trim()) - decimal.Parse("0.1")) / (dFasele / 100) + 1);

                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh090205++;
                        RizMetre.Sharh = "pos " + intPos.ToString();
                        RizMetre.Tedad = dTedadNew;
                        RizMetre.Tool = dTool;
                        RizMetre.Arz = 0;
                        RizMetre.Ertefa = 0;
                        RizMetre.Vazn = dVaz;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId090205;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "50";///آرماتور
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();
                    }
                }
                else if (intGhotr > 18)
                {
                    if (intPos == 3 || intPos == 5 || intPos == 6 || intPos == 8)
                    {
                        decimal dZaribLAbroPrim = decimal.Parse("0.056");
                        if (intGhotr > 20)
                            dZaribLAbroPrim = decimal.Parse("0.07");

                        decimal dLAbro = decimal.Parse(LAbro.Trim()) - decimal.Parse("0.1") + decimal.Parse("0.024") * intGhotr;
                        if (dLAbro >= 12 && dLAbro < 24)
                        {
                            decimal dLAbroPrim = dLAbro + dZaribLAbroPrim * intGhotr;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh090206++;
                            RizMetre.Sharh = "pos " + intPos.ToString();
                            RizMetre.Tedad = dTedad;
                            RizMetre.Tool = Math.Round(dLAbroPrim, 2);
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = 0;
                            RizMetre.Vazn = dVaz;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId090206;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "300" + PolNum.ToString("D3") + "50";///آرماتور
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }
                        else if (dLAbro >= 24 && dLAbro < 36)
                        {
                            decimal dLAbroPrim = dLAbro + 2 * dZaribLAbroPrim * intGhotr;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh090206++;
                            RizMetre.Sharh = "pos " + intPos.ToString();
                            RizMetre.Tedad = dTedad;
                            RizMetre.Tool = Math.Round(dLAbroPrim, 2);
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = 0;
                            RizMetre.Vazn = dVaz;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId090206;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "300" + PolNum.ToString("D3") + "50";///آرماتور
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }
                        else if (dLAbro >= 36 && dLAbro < 48)
                        {
                            decimal dLAbroPrim = dLAbro + 3 * dZaribLAbroPrim * intGhotr;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh090206++;
                            RizMetre.Sharh = "pos " + intPos.ToString();
                            RizMetre.Tedad = dTedad;
                            RizMetre.Tool = Math.Round(dLAbroPrim, 2);
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = 0;
                            RizMetre.Vazn = dVaz;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId090206;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "300" + PolNum.ToString("D3") + "50";///آرماتور
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }
                        else
                        {
                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh090206++;
                            RizMetre.Sharh = "pos " + intPos.ToString();
                            RizMetre.Tedad = dTedad;
                            RizMetre.Tool = Math.Round(dLAbro, 2);
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = 0;
                            RizMetre.Vazn = dVaz;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId090206;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "300" + PolNum.ToString("D3") + "50";///آرماتور
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }
                    }
                    else if (intPos == 1 || intPos == 2 || intPos == 4 || intPos == 7
                             || intPos == 9 || intPos == 10)
                    {
                        decimal dFasele = decimal.Parse(DtAbroDaliDetails.Rows[i]["Fasele"].ToString().Trim());
                        decimal dTool = decimal.Parse(DtAbroDaliDetails.Rows[i]["Tool"].ToString().Trim());
                        decimal dTedadNew = Math.Round((decimal.Parse(LAbro.Trim()) - decimal.Parse("0.1")) / (dFasele / 100) + 1);

                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh090206++;
                        RizMetre.Sharh = "pos " + intPos.ToString();
                        RizMetre.Tedad = dTedadNew;
                        RizMetre.Tool = dTool;
                        RizMetre.Arz = 0;
                        RizMetre.Ertefa = 0;
                        RizMetre.Vazn = dVaz;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId090206;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "50";///آرماتور
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();
                    }
                }
            }
            return true;
        }

        public bool GhalebBandiFendasionDastak(Guid PolVaAbroId, int PolNum, string D, string TedadDahaneh, Guid BarAvordId,
            string LW1j, string LW1p, string LB1W1, string LB2W1, string LW2j, string LW2p, string LB1W2, string LB2W2,
            string LW3j, string LW3p, string LB1W3, string LB2W3, string LW4j, string LW4p, string LB1W4, string LB2W4, string h)
        {
            //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
            //clsRizMetreUsersses RizMetre = new clsRizMetreUsersses();
            ///////////////
            var varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "080101").ToList();
            DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

            Guid guFBId = new Guid();
            if (DtFBUser.Rows.Count == 0)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "080101";
                FBSave.BahayeVahedZarib = 0;
                _context.FBs.Add(FBSave);
                _context.SaveChanges();
                guFBId = FBSave.ID;
            }
            else
                guFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
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

            //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='080101'");
            //int FBId = 0;
            //if (DtFBUser.Rows.Count == 0)
            //    FBId = OperationItemsFB.SaveFB(BarAvordID, "080101", 0);
            //else
            //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
            //DataTable DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId);
            //int Shomareh = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            ///////////////
            decimal dh = decimal.Parse(h);
            decimal rndh = Math.Round(dh);

            var varAbadDivarBali = _context.AbadDivarBalis.Where(x => x.h == rndh).ToList();
            DataTable DtAbadDivarBali = clsConvert.ToDataTable(varAbadDivarBali);
            //DataTable DtAbadDivarBali = clsAbadeKoole.AbadDivarBaliListWithParametr("h=" + rndh.ToString());

            decimal m = 0;
            if (DtAbadDivarBali.Rows.Count != 0)
            {
                m = decimal.Parse(DtAbadDivarBali.Rows[0]["m"].ToString());
            }
            for (int i = 1; i < 5; i++)
            {
                decimal dLWj = 0;
                decimal dLWp = 0;
                decimal dLB1W = 0;
                decimal dLB2W = 0;
                switch (i)
                {
                    case 1:
                        {
                            dLWj = decimal.Parse(LW1j.Trim() == "" ? "0" : LW1j.Trim());
                            dLWp = decimal.Parse(LW1p.Trim() == "" ? "0" : LW1p.Trim());
                            dLB1W = decimal.Parse(LB1W1.Trim() == "" ? "0" : LB1W1.Trim());
                            dLB2W = decimal.Parse(LB2W1.Trim() == "" ? "0" : LB2W1.Trim());
                            break;
                        }
                    case 2:
                        {
                            dLWj = decimal.Parse(LW2j.Trim() == "" ? "0" : LW2j.Trim());
                            dLWp = decimal.Parse(LW2p.Trim() == "" ? "0" : LW2p.Trim());
                            dLB1W = decimal.Parse(LB1W2.Trim() == "" ? "0" : LB1W2.Trim());
                            dLB2W = decimal.Parse(LB2W2.Trim() == "" ? "0" : LB2W2.Trim());
                            break;
                        }
                    case 3:
                        {
                            dLWj = decimal.Parse(LW3j.Trim() == "" ? "0" : LW3j.Trim());
                            dLWp = decimal.Parse(LW3p.Trim() == "" ? "0" : LW3p.Trim());
                            dLB1W = decimal.Parse(LB1W3.Trim() == "" ? "0" : LB1W3.Trim());
                            dLB2W = decimal.Parse(LB2W3.Trim() == "" ? "0" : LB2W3.Trim());
                            break;
                        }
                    case 4:
                        {
                            dLWj = decimal.Parse(LW4j.Trim() == "" ? "0" : LW4j.Trim());
                            dLWp = decimal.Parse(LW4p.Trim() == "" ? "0" : LW4p.Trim());
                            dLB1W = decimal.Parse(LB1W4.Trim() == "" ? "0" : LB1W4.Trim());
                            dLB2W = decimal.Parse(LB2W4.Trim() == "" ? "0" : LB2W4.Trim());
                            break;
                        }
                    default:
                        break;
                }

                if (dLWj != 0)
                {
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh++;
                    RizMetre.Sharh = " قالب طولی پی دستک " + i + "(جلو)";
                    RizMetre.Tedad = 1;
                    RizMetre.Tool = dLWj;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = m;
                    RizMetre.Vazn = 0;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "500" + PolNum.ToString("D3") + "00";///قالب بندی طولی پی دستک جلو
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();
                    //RizMetre.Save();
                }
                if (dLWp != 0)
                {
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh++;
                    RizMetre.Sharh = " قالب طولی پی دستک " + i + "(پشت)";
                    RizMetre.Tedad = 1;
                    RizMetre.Tool = dLWp;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = m;
                    RizMetre.Vazn = 0;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "500" + PolNum.ToString("D3") + "01";///قالب بندی طولی پی دستک پشت
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();
                    //RizMetre.Save();
                }
                if (dLB1W != 0)
                {
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh++;
                    RizMetre.Sharh = " قالب عرضی پی دستک (سرکله) " + i + "(طرف یک)";
                    RizMetre.Tedad = 1;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = dLB1W;
                    RizMetre.Ertefa = m;
                    RizMetre.Vazn = 0;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "500" + PolNum.ToString("D3") + "02";///قالب بندی عرضی پی دستک طرف 1
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();
                    //RizMetre.Save();
                }
                if (dLB2W != 0)
                {
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh++;
                    RizMetre.Sharh = " قالب عرضی پی دستک (سرکله) " + i + "(طرف دو)";
                    RizMetre.Tedad = 1;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = dLB2W;
                    RizMetre.Ertefa = m;
                    RizMetre.Vazn = 0;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "500" + PolNum.ToString("D3") + "03";///قالب بندی عرضی پی دستک طرف 2
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();
                    //RizMetre.Save();
                }
            }
            return true;
        }

        public bool GhalebBandiDivarVaSotoonDastak(Guid PolVaAbroId, int PolNum, string D, string TedadDahaneh, Guid BarAvordId,
                string h, string t, string hMinw1, string hMinw2, string hMinw3, string hMinw4,
                string w1, string w2, string w3, string w4)
        {
            decimal dh = decimal.Parse(h);
            decimal dt = decimal.Parse(t);
            decimal dht = dh + dt;

            //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
            //clsRizMetreUsersses RizMetre = new clsRizMetreUsersses();
            ///////////////
            var varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "080201").ToList();
            DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

            Guid guFBId080201 = new Guid();
            if (DtFBUser.Rows.Count == 0)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "080201";
                FBSave.BahayeVahedZarib = 0;
                _context.FBs.Add(FBSave);
                _context.SaveChanges();
                guFBId080201 = FBSave.ID;
            }
            else
                guFBId080201 = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
            ////////////////
            //var varDtLastRizMetreUsersesShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId080201).ToList();
            //DataTable DtLastRizMetreUsersesShomareh = clsConvert.ToDataTable(varDtLastRizMetreUsersesShomareh);
            //int Shomareh080201 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());

            var RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId080201).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            long Shomareh080201 = 0;
            if (RizMetreUserLastShomareh != null)
                Shomareh080201 = RizMetreUserLastShomareh.Shomareh + 1;
            else
                Shomareh080201 = 1;

            //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='080201'");
            //int FBId080201 = 0;
            //if (DtFBUser.Rows.Count == 0)
            //    FBId080201 = OperationItemsFB.SaveFB(BarAvordID, "080201", 0);
            //else
            //    FBId080201 = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
            //DataTable DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId080201);
            //int Shomareh080201 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            ///////////////
            varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "080202").ToList();
            DtFBUser = clsConvert.ToDataTable(varFBUser);

            Guid guFBId080202 = new Guid();
            if (DtFBUser.Rows.Count == 0)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "080202";
                FBSave.BahayeVahedZarib = 0;
                _context.FBs.Add(FBSave);
                _context.SaveChanges();
                guFBId080202 = FBSave.ID;
            }
            else
                guFBId080202 = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
            ////////////////
            //varDtLastRizMetreUsersesShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId080202).ToList();
            //DtLastRizMetreUsersesShomareh = clsConvert.ToDataTable(varDtLastRizMetreUsersesShomareh);
            //int Shomareh080202 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());

            RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId080202).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            long Shomareh080202 = 0;
            if (RizMetreUserLastShomareh != null)
                Shomareh080202 = RizMetreUserLastShomareh.Shomareh + 1;
            else
                Shomareh080202 = 1;

            //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='080202'");
            //int FBId080202 = 0;
            //if (DtFBUser.Rows.Count == 0)
            //    FBId080202 = OperationItemsFB.SaveFB(BarAvordID, "080202", 0);
            //else
            //    FBId080202 = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
            //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId080202);
            //int Shomareh080202 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            ///////////////

            varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "080203").ToList();
            DtFBUser = clsConvert.ToDataTable(varFBUser);

            Guid guFBId080203 = new Guid();
            if (DtFBUser.Rows.Count == 0)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "080203";
                FBSave.BahayeVahedZarib = 0;
                _context.FBs.Add(FBSave);
                _context.SaveChanges();
                guFBId080203 = FBSave.ID;
            }
            else
                guFBId080203 = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
            ////////////////
            //varDtLastRizMetreUsersesShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId080203).ToList();
            //DtLastRizMetreUsersesShomareh = clsConvert.ToDataTable(varDtLastRizMetreUsersesShomareh);
            //int Shomareh080203 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId080203).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            long Shomareh080203 = 0;
            if (RizMetreUserLastShomareh != null)
                Shomareh080203 = RizMetreUserLastShomareh.Shomareh + 1;
            else
                Shomareh080203 = 1;

            //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='080203'");
            //int FBId080203 = 0;
            //if (DtFBUser.Rows.Count == 0)
            //    FBId080203 = OperationItemsFB.SaveFB(BarAvordID, "080203", 0);
            //else
            //    FBId080203 = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
            //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId080203);
            //int Shomareh080203 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            ///////////////
            varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "080204").ToList();
            DtFBUser = clsConvert.ToDataTable(varFBUser);

            Guid guFBId080204 = new Guid();
            if (DtFBUser.Rows.Count == 0)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "080204";
                FBSave.BahayeVahedZarib = 0;
                _context.FBs.Add(FBSave);
                _context.SaveChanges();
                guFBId080204 = FBSave.ID;
            }
            else
                guFBId080204 = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
            ////////////////
            //varDtLastRizMetreUsersesShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId080204).ToList();
            //DtLastRizMetreUsersesShomareh = clsConvert.ToDataTable(varDtLastRizMetreUsersesShomareh);
            //int Shomareh080204 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId080204).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            long Shomareh080204 = 0;
            if (RizMetreUserLastShomareh != null)
                Shomareh080204 = RizMetreUserLastShomareh.Shomareh + 1;
            else
                Shomareh080204 = 1;

            //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='080204'");
            //int FBId080204 = 0;
            //if (DtFBUser.Rows.Count == 0)
            //    FBId080204 = OperationItemsFB.SaveFB(BarAvordID, "080204", 0);
            //else
            //    FBId080204 = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
            //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId080204);
            //int Shomareh080204 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            ///////////////
            varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "080205").ToList();
            DtFBUser = clsConvert.ToDataTable(varFBUser);

            Guid guFBId080205 = new Guid();
            if (DtFBUser.Rows.Count == 0)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "080205";
                FBSave.BahayeVahedZarib = 0;
                _context.FBs.Add(FBSave);
                _context.SaveChanges();
                guFBId080205 = FBSave.ID;
            }
            else
                guFBId080205 = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
            ////////////////
            //varDtLastRizMetreUsersesShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId080205).ToList();
            //DtLastRizMetreUsersesShomareh = clsConvert.ToDataTable(varDtLastRizMetreUsersesShomareh);
            //int Shomareh080205 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId080205).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            long Shomareh080205 = 0;
            if (RizMetreUserLastShomareh != null)
                Shomareh080205 = RizMetreUserLastShomareh.Shomareh + 1;
            else
                Shomareh080205 = 1;

            //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='080205'");
            //int FBId080205 = 0;
            //if (DtFBUser.Rows.Count == 0)
            //    FBId080205 = OperationItemsFB.SaveFB(BarAvordID, "080205", 0);
            //else
            //    FBId080205 = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
            //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId080205);
            //int Shomareh080205 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            ///////////////

            for (int i = 1; i < 5; i++)
            {
                decimal hMinw = 0;
                switch (i)
                {
                    case 1: hMinw = decimal.Parse(hMinw1.Trim()); break;
                    case 2: hMinw = decimal.Parse(hMinw2.Trim()); break;
                    case 3: hMinw = decimal.Parse(hMinw3.Trim()); break;
                    case 4: hMinw = decimal.Parse(hMinw4.Trim()); break;
                    default:
                        break;
                }

                decimal LW = 0;
                switch (i)
                {
                    case 1: LW = decimal.Parse(w1.Trim()); break;
                    case 2: LW = decimal.Parse(w2.Trim()); break;
                    case 3: LW = decimal.Parse(w3.Trim()); break;
                    case 4: LW = decimal.Parse(w4.Trim()); break;
                    default:
                        break;
                }

                if (hMinw > 0 && LW > 0)
                {
                    ////////////////
                    //////////////
                    /////////////
                    ////////////////
                    if (hMinw > 0 && hMinw <= 2)
                    {
                        if (dht > 0 && dht <= 2)
                        {
                            decimal res = (dht + hMinw) / 2;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080201++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080201;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }

                        else if (dht > 2 && dht <= 3)
                        {
                            decimal res1 = (dht - 2) / 2;
                            decimal res2 = ((dht + hMinw) / 2) - res1;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080201++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res2;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080201;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080202++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res1;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080202;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }

                        else if (dht > 3 && dht <= 5)
                        {
                            decimal res1 = (dht - 3) / 2;
                            decimal res2 = ((dht - 2) / 2) - res1;
                            decimal res3 = ((dht + hMinw) / 2) - res2;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080201++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res3;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080201;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080202++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res2;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080202;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080203++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res1;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080203;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "12";///قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }

                        else if (dht > 5 && dht <= 7)
                        {
                            decimal res1 = (dht - 5) / 2;
                            decimal res2 = ((dht - 3) / 2) - res1;
                            decimal res3 = ((dht - 2) / 2) - res2;
                            decimal res4 = ((dht + hMinw) / 2) - res3;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080201++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res4;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080201;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080202++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res3;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080202;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080203++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res2;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080203;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "12";///قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();

                            //RizMetre.Save();
                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080204++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res1;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080204;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "13";///قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();

                            //RizMetre.Save();
                        }

                        else if (dht > 7 && dht <= 10)
                        {
                            decimal res1 = (dht - 7) / 2;
                            decimal res2 = ((dht - 5) / 2) - res1;
                            decimal res3 = ((dht - 3) / 2) - res2;
                            decimal res4 = ((dht - 2) / 2) - res3;
                            decimal res5 = ((dht + hMinw) / 2) - res4;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080201++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res5;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080201;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080202++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res4;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080202;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080203++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res3;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080203;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "12";///قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080204++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res2;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080204;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "13";///قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080205++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 7 تا 10 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res1;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080205;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "14";///قالب دیوارها و ستون های بتنی بین 7 تا 10 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }
                    }

                    else if (hMinw > 2 && hMinw <= 3)
                    {
                        if (dht > 0 && dht <= 2)
                        {
                            decimal res = 2;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080201++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080201;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }

                        else if (dht > 2 && dht <= 3)
                        {
                            decimal res1 = ((dht + hMinw) / 2) - 2;
                            decimal res2 = 2;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080201++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res2;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080201;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080202++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res1;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080202;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }

                        else if (dht > 3 && dht <= 5)
                        {
                            decimal res1 = (dht - 3) / 2;
                            decimal res2 = ((dht + hMinw) / 2) - res1 - 2;
                            decimal res3 = 2;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080201++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res3;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080201;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080202++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res2;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080202;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080203++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res1;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080203;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "12";///قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }

                        else if (dht > 5 && dht <= 7)
                        {
                            decimal res1 = (dht - 5) / 2;
                            decimal res2 = ((dht - 3) / 2) - res1;
                            decimal res3 = ((dht + hMinw) / 2) - res2 - 2;
                            decimal res4 = 2;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080201++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res4;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080201;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080202++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res3;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080202;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080203++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res2;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080203;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "12";///قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080204++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res1;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080204;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "13";///قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            //RizMetre.Save();
                        }

                        else if (dht > 7 && dht <= 10)
                        {
                            decimal res1 = (dht - 7) / 2;
                            decimal res2 = ((dht - 5) / 2) - res1;
                            decimal res3 = ((dht - 3) / 2) - res2;
                            decimal res4 = ((dht + hMinw) / 2) - res3 - 2;
                            decimal res5 = 2;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080201++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res5;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080201;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080202++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res4;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080202;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080203++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res3;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080203;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "12";///قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080204++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res2;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080204;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "13";///قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080205++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 7 تا 10 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res1;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080205;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "14";///قالب دیوارها و ستون های بتنی بین 7 تا 10 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            //RizMetre.Save();
                        }
                    }
                    else if (hMinw > 3 && hMinw <= 5)
                    {
                        if (dht > 0 && dht <= 2)
                        {
                            decimal res = 2;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080201++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080201;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }

                        else if (dht > 2 && dht <= 3)
                        {
                            decimal res1 = 1;
                            decimal res2 = 2;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080201++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res2;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080201;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080202++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res1;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080202;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }

                        else if (dht > 3 && dht <= 5)
                        {
                            decimal res1 = ((dht + hMinw) / 2) - 3;
                            decimal res2 = 1;
                            decimal res3 = 2;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080201++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res3;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080201;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080202++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res2;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080202;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080203++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res1;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080203;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "12";///قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }

                        else if (dht > 5 && dht <= 7)
                        {
                            decimal res1 = (dht - 5) / 2;
                            decimal res2 = ((dht + hMinw) / 2) - res1 - 3;
                            decimal res3 = 1;
                            decimal res4 = 2;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080201++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res4;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080201;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080202++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res3;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080202;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080203++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res2;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080203;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "12";///قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080204++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res1;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080204;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "13";///قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }

                        else if (dht > 7 && dht <= 10)
                        {
                            decimal res1 = (dht - 7) / 2;
                            decimal res2 = ((dht - 5) / 2) - res1;
                            decimal res3 = ((dht + hMinw) / 2) - res2 - 3;
                            decimal res4 = 1;
                            decimal res5 = 2;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080201++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res5;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080201;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080202++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res4;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080202;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080203++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res3;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080203;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "12";///قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080204++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res2;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080204;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "13";///قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080205++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 7 تا 10 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res1;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080205;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "14";///قالب دیوارها و ستون های بتنی بین 7 تا 10 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }
                    }

                    else if (hMinw > 5 && hMinw <= 7)
                    {
                        if (dht > 0 && dht <= 2)
                        {
                            decimal res = 2;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080201++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080201;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }

                        else if (dht > 2 && dht <= 3)
                        {
                            decimal res1 = 1;
                            decimal res2 = 2;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080201++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res2;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080201;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080202++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res1;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080202;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }

                        else if (dht > 3 && dht <= 5)
                        {
                            decimal res1 = 2;
                            decimal res2 = 1;
                            decimal res3 = 2;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080201++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res3;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080201;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080202++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res2;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080202;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080203++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res1;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080203;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "12";///قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }

                        else if (dht > 5 && dht <= 7)
                        {
                            decimal res1 = ((dht + hMinw) / 2) - 5;
                            decimal res2 = 2;
                            decimal res3 = 1;
                            decimal res4 = 2;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080201++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res4;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080201;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080202++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res3;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080202;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080203++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res2;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080203;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "12";///قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080204++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res1;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080204;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "13";///قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }

                        else if (dht > 7 && dht <= 10)
                        {
                            decimal res1 = (dht - 7) / 2;
                            decimal res2 = ((dht + hMinw) / 2) - 5 - res1;
                            decimal res3 = 2;
                            decimal res4 = 1;
                            decimal res5 = 2;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080201++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res5;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080201;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080202++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res4;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080202;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080203++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res3;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080203;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "12";///قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            //RizMetre.Save();
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080204++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res2;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080204;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "13";///قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080205++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 7 تا 10 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res1;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080205;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "14";///قالب دیوارها و ستون های بتنی بین 7 تا 10 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }
                    }

                    else if (hMinw > 7 && hMinw <= 10)
                    {
                        if (dht > 0 && dht <= 2)
                        {
                            decimal res = 2;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080201++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080201;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }

                        else if (dht > 2 && dht <= 3)
                        {
                            decimal res1 = 1;
                            decimal res2 = 2;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080201++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res2;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080201;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080202++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res1;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080202;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }

                        else if (dht > 3 && dht <= 5)
                        {
                            decimal res1 = 2;
                            decimal res2 = 1;
                            decimal res3 = 2;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080201++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res3;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080201;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080202++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res2;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080202;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080203++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res1;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080203;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "12";///قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }

                        else if (dht > 5 && dht <= 7)
                        {
                            decimal res1 = 2;
                            decimal res2 = 2;
                            decimal res3 = 1;
                            decimal res4 = 2;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080201++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res4;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080201;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080202++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res3;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080202;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080203++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res2;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080203;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "12";///قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080204++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res1;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080204;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "13";///قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }

                        else if (dht > 7 && dht <= 10)
                        {
                            decimal res1 = ((dht + hMinw) / 2) - 7;
                            decimal res2 = 2;
                            decimal res3 = 2;
                            decimal res4 = 1;
                            decimal res5 = 2;

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080201++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res5;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080201;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080202++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res4;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080202;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080203++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res3;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080203;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "12";///قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080204++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res2;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080204;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "13";///قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();

                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh080205++;
                            RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 7 تا 10 متر دستک " + i;
                            RizMetre.Tedad = 2;
                            RizMetre.Tool = LW;
                            RizMetre.Arz = 0;
                            RizMetre.Ertefa = res1;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                            RizMetre.FBId = guFBId080205;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "500" + PolNum.ToString("D3") + "14";///قالب دیوارها و ستون های بتنی بین 7 تا 10 متر دستک
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            _context.SaveChanges();
                            //RizMetre.Save();
                        }
                    }
                }
            }
            return true;
        }

        public bool GhalebBandiSarKalaDastak(Guid PolVaAbroId, int PolNum, string D, string TedadDahaneh, Guid BarAvordId,
        string t, string hMinw1, string hMinw2, string hMinw3, string hMinw4,
        string w1, string w2, string w3, string w4)
        {
            //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
            //clsRizMetreUsersses RizMetre = new clsRizMetreUsersses();
            ///////////////

            var varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "080201").ToList();
            DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

            Guid guFBId080201 = new Guid();
            if (DtFBUser.Rows.Count == 0)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "080201";
                FBSave.BahayeVahedZarib = 0;
                _context.FBs.Add(FBSave);
                _context.SaveChanges();
                guFBId080201 = FBSave.ID;
            }
            else
                guFBId080201 = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
            ////////////////
            //var varDtLastRizMetreUsersesShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId080201).ToList();
            //DataTable DtLastRizMetreUsersesShomareh = clsConvert.ToDataTable(varDtLastRizMetreUsersesShomareh);
            //int Shomareh080201 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());

            var RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId080201).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            long Shomareh080201 = 0;
            if (RizMetreUserLastShomareh != null)
                Shomareh080201 = RizMetreUserLastShomareh.Shomareh + 1;
            else
                Shomareh080201 = 1;

            //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='080201'");
            //int FBId080201 = 0;
            //if (DtFBUser.Rows.Count == 0)
            //    FBId080201 = OperationItemsFB.SaveFB(BarAvordID, "080201", 0);
            //else
            //    FBId080201 = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
            //DataTable DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId080201);
            //int Shomareh080201 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            ///////////////
            varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "080202").ToList();
            DtFBUser = clsConvert.ToDataTable(varFBUser);

            Guid guFBId080202 = new Guid();
            if (DtFBUser.Rows.Count == 0)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "080202";
                FBSave.BahayeVahedZarib = 0;
                _context.FBs.Add(FBSave);
                _context.SaveChanges();
                guFBId080202 = FBSave.ID;
            }
            else
                guFBId080202 = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
            ////////////////
            //varDtLastRizMetreUsersesShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId080202).ToList();
            //DtLastRizMetreUsersesShomareh = clsConvert.ToDataTable(varDtLastRizMetreUsersesShomareh);
            //int Shomareh080202 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId080202).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            long Shomareh080202 = 0;
            if (RizMetreUserLastShomareh != null)
                Shomareh080202 = RizMetreUserLastShomareh.Shomareh + 1;
            else
                Shomareh080202 = 1;

            //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='080202'");
            //int FBId080202 = 0;
            //if (DtFBUser.Rows.Count == 0)
            //    FBId080202 = OperationItemsFB.SaveFB(BarAvordID, "080202", 0);
            //else
            //    FBId080202 = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
            //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId080202);
            //int Shomareh080202 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            ///////////////
            varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "080203").ToList();
            DtFBUser = clsConvert.ToDataTable(varFBUser);

            Guid guFBId080203 = new Guid();
            if (DtFBUser.Rows.Count == 0)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "080203";
                FBSave.BahayeVahedZarib = 0;
                _context.FBs.Add(FBSave);
                _context.SaveChanges();
                guFBId080203 = FBSave.ID;
            }
            else
                guFBId080203 = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
            ////////////////
            //varDtLastRizMetreUsersesShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId080203).ToList();
            //DtLastRizMetreUsersesShomareh = clsConvert.ToDataTable(varDtLastRizMetreUsersesShomareh);
            //int Shomareh080203 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId080203).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            long Shomareh080203 = 0;
            if (RizMetreUserLastShomareh != null)
                Shomareh080203 = RizMetreUserLastShomareh.Shomareh + 1;
            else
                Shomareh080203 = 1;

            //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='080203'");
            //int FBId080203 = 0;
            //if (DtFBUser.Rows.Count == 0)
            //    FBId080203 = OperationItemsFB.SaveFB(BarAvordID, "080203", 0);
            //else
            //    FBId080203 = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
            //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId080203);
            //int Shomareh080203 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            ///////////////
            varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "080204").ToList();
            DtFBUser = clsConvert.ToDataTable(varFBUser);

            Guid guFBId080204 = new Guid();
            if (DtFBUser.Rows.Count == 0)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "080204";
                FBSave.BahayeVahedZarib = 0;
                _context.FBs.Add(FBSave);
                _context.SaveChanges();
                guFBId080204 = FBSave.ID;
            }
            else
                guFBId080204 = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
            ////////////////
            //varDtLastRizMetreUsersesShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId080204).ToList();
            //DtLastRizMetreUsersesShomareh = clsConvert.ToDataTable(varDtLastRizMetreUsersesShomareh);
            //int Shomareh080204 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId080204).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            long Shomareh080204 = 0;
            if (RizMetreUserLastShomareh != null)
                Shomareh080204 = RizMetreUserLastShomareh.Shomareh + 1;
            else
                Shomareh080204 = 1;

            //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='080204'");
            //int FBId080204 = 0;
            //if (DtFBUser.Rows.Count == 0)
            //    FBId080204 = OperationItemsFB.SaveFB(BarAvordID, "080204", 0);
            //else
            //    FBId080204 = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
            //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId080204);
            //int Shomareh080204 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            ///////////////
            varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "080205").ToList();
            DtFBUser = clsConvert.ToDataTable(varFBUser);

            Guid guFBId080205 = new Guid();
            if (DtFBUser.Rows.Count == 0)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "080205";
                FBSave.BahayeVahedZarib = 0;
                _context.FBs.Add(FBSave);
                _context.SaveChanges();
                guFBId080205 = FBSave.ID;
            }
            else
                guFBId080205 = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
            ////////////////
            //varDtLastRizMetreUsersesShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId080205).ToList();
            //DtLastRizMetreUsersesShomareh = clsConvert.ToDataTable(varDtLastRizMetreUsersesShomareh);
            //int Shomareh080205 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId080205).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            long Shomareh080205 = 0;
            if (RizMetreUserLastShomareh != null)
                Shomareh080205 = RizMetreUserLastShomareh.Shomareh + 1;
            else
                Shomareh080205 = 1;

            //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='080205'");
            //int FBId080205 = 0;
            //if (DtFBUser.Rows.Count == 0)
            //    FBId080205 = OperationItemsFB.SaveFB(BarAvordID, "080205", 0);
            //else
            //    FBId080205 = int.Parse(DtFBUser.Rows[0]["Id"].ToString());
            //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId080205);
            //int Shomareh080205 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            ///////////////
            for (int i = 1; i < 5; i++)
            {
                decimal hMinw = 0;
                switch (i)
                {
                    case 1: hMinw = decimal.Parse(hMinw1.Trim()); break;
                    case 2: hMinw = decimal.Parse(hMinw2.Trim()); break;
                    case 3: hMinw = decimal.Parse(hMinw3.Trim()); break;
                    case 4: hMinw = decimal.Parse(hMinw4.Trim()); break;
                    default:
                        break;
                }

                ///////////////
                decimal rndhMinw = Math.Round(hMinw);

                var varAbadDivarBali = _context.AbadDivarBalis.Where(x => x.h == rndhMinw).ToList();
                DataTable DtAbadDivarBali = clsConvert.ToDataTable(varAbadDivarBali);
                //DataTable DtAbadDivarBali = clsAbadeKoole.AbadDivarBaliListWithParametr("h=" + rndhMinw.ToString());
                decimal dx = decimal.Parse(DtAbadDivarBali.Rows[0]["x"].ToString());

                if (hMinw > 0)
                {
                    if (hMinw > 0 && hMinw <= 2)
                    {
                        decimal res = hMinw / 6 + dx + decimal.Parse("0.35");
                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080201++;
                        RizMetre.Sharh = " قالب سرکله دیوار دستک  " + i + " تا 2 متر ";
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = res;
                        RizMetre.Ertefa = hMinw;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080201;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "20";///" قالب سرکله دیوار دستک  " + i +" تا 2 متر ";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();

                        //RizMetre.Save();
                    }

                    else if (hMinw > 2 && hMinw <= 3)
                    {
                        decimal res1 = (hMinw - 1) / 3 + dx + decimal.Parse("0.35");

                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080201++;
                        RizMetre.Sharh = " قالب سرکله دیوار دستک  " + i + " تا 2 متر ";
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = res1;
                        RizMetre.Ertefa = 2;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080201;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "20";///قالب سرکله دیوار دستک  " + i +" تا 2 متر 
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();

                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        decimal res2 = ((hMinw - 2) / 6) + dx + decimal.Parse("0.35");
                        RizMetre.Shomareh = Shomareh080202++;
                        RizMetre.Sharh = " قالب سرکله دیوار دستک  " + i + " 2 تا 3 متر ";
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = res2;
                        RizMetre.Ertefa = hMinw - 2;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080202;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "21";/// قالب سرکله دیوار دستک  " + i +" 2 تا 3 متر
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();

                        //RizMetre.Save();
                    }

                    else if (hMinw > 3 && hMinw <= 5)
                    {
                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        decimal res1 = (hMinw - 1) / 3 + dx + decimal.Parse("0.35");
                        RizMetre.Shomareh = Shomareh080201++;
                        RizMetre.Sharh = " قالب سرکله دیوار دستک  " + i + " تا 2 متر ";
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = res1;
                        RizMetre.Ertefa = 2;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080201;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "20";///قالب سرکله دیوار دستک  " + i +" تا 2 متر 
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();

                        //RizMetre.Save();

                        decimal res2 = (2 * hMinw - 5) / 6 + dx + decimal.Parse("0.35");
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080202++;
                        RizMetre.Sharh = " قالب سرکله دیوار دستک  " + i + " 2 تا 3 متر ";
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = res2;
                        RizMetre.Ertefa = 1;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080202;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "21";///قالب سرکله دیوار دستک  " + i +" 2 تا 3 متر 
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();

                        //RizMetre.Save();

                        decimal res3 = (hMinw - 3) / 6 + dx + decimal.Parse("0.35");
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080203++;
                        RizMetre.Sharh = " قالب سرکله دیوار دستک  " + i + " 3 تا 5 متر ";
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = res3;
                        RizMetre.Ertefa = hMinw - 3;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080203;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "22";///قالب سرکله دیوار دستک  " + i +" 3 تا 5 متر
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();

                        //RizMetre.Save();
                    }

                    else if (hMinw > 5 && hMinw <= 7)
                    {
                        decimal res1 = (hMinw - 1) / 3 + dx + decimal.Parse("0.35");
                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080201++;
                        RizMetre.Sharh = " قالب سرکله دیوار دستک  " + i + " تا 2 متر ";
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = res1;
                        RizMetre.Ertefa = 2;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080201;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "20";/// قالب سرکله دیوار دستک  " + i +" تا 2 متر
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();

                        //RizMetre.Save();

                        decimal res2 = (2 * hMinw - 5) / 6 + dx + decimal.Parse("0.35");
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080202++;
                        RizMetre.Sharh = " قالب سرکله دیوار دستک  " + i + " 2 تا 3 متر ";
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = res2;
                        RizMetre.Ertefa = 1;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080202;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "21";///قالب سرکله دیوار دستک  " + i +" 2 تا 3 متر
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();

                        //RizMetre.Save();

                        decimal res3 = (2 * hMinw - 8) / 6 + dx + decimal.Parse("0.35");
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080203++;
                        RizMetre.Sharh = " قالب سرکله دیوار دستک  " + i + " 3 تا 5 متر ";
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = res3;
                        RizMetre.Ertefa = 2;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080203;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "22";/// قالب سرکله دیوار دستک  " + i +" 3 تا 5 متر 
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();

                        //RizMetre.Save();

                        decimal res4 = (hMinw - 5) / 6 + dx + decimal.Parse("0.35");
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080204++;
                        RizMetre.Sharh = " قالب سرکله دیوار دستک  " + i + " 5 تا 7 متر ";
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = res4;
                        RizMetre.Ertefa = hMinw - 5;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080204;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "23";/// قالب سرکله دیوار دستک  " + i +" 5 تا 7 متر 
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();

                        //RizMetre.Save();
                    }

                    else if (hMinw > 7 && hMinw <= 10)
                    {
                        decimal res1 = (hMinw - 1) / 3 + dx + decimal.Parse("0.35");
                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080201++;
                        RizMetre.Sharh = " قالب سرکله دیوار دستک  " + i + " تا 2 متر ";
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = res1;
                        RizMetre.Ertefa = 2;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080201;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "20";///قالب سرکله دیوار دستک  " + i +" تا 2 متر
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();

                        //RizMetre.Save();

                        decimal res2 = (2 * hMinw - 5) / 6 + dx + decimal.Parse("0.35");
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080202++;
                        RizMetre.Sharh = " قالب سرکله دیوار دستک  " + i + " 2 تا 3 متر ";
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = res2;
                        RizMetre.Ertefa = 1;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080202;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "21";///قالب سرکله دیوار دستک  " + i +" 2 تا 3 متر
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();

                        //RizMetre.Save();

                        decimal res3 = (2 * hMinw - 8) / 6 + dx + decimal.Parse("0.35");
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080203++;
                        RizMetre.Sharh = " قالب سرکله دیوار دستک  " + i + " 3 تا 5 متر ";
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = res3;
                        RizMetre.Ertefa = 2;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080203;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "22";/// قالب سرکله دیوار دستک  " + i +" 3 تا 5 متر
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();

                        //RizMetre.Save();

                        decimal res4 = (2 * hMinw - 12) / 6 + dx + decimal.Parse("0.35");
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080204++;
                        RizMetre.Sharh = " قالب سرکله دیوار دستک  " + i + " 5 تا 7 متر ";
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = res4;
                        RizMetre.Ertefa = 2;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080204;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "23";///قالب سرکله دیوار دستک  " + i +" 5 تا 7 متر
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();

                        //RizMetre.Save();

                        decimal res5 = (hMinw - 7) / 6 + dx + decimal.Parse("0.35");
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080205++;
                        RizMetre.Sharh = " قالب سرکله دیوار دستک  " + i + " 7 تا 10 متر ";
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = res5;
                        RizMetre.Ertefa = hMinw - 7;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080205;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "24";/// قالب سرکله دیوار دستک  " + i +" 7 تا 10 متر
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();
                    }
                }
            }
            return true;
        }

        public bool BotonFendasionDastak(Guid PolVaAbroId, int PolNum, string D, string TedadDahaneh, Guid BarAvordId,
                            string h, string t, string hMinw1, string hMinw2, string hMinw3, string hMinw4,
                            string w1, string w2, string w3, string w4)
        {
            decimal dht = decimal.Parse(h) + decimal.Parse(t);
            if (dht <= decimal.Parse("0.5"))
                dht += decimal.Parse("0.5");
            decimal rndh = Math.Round(dht);
            var varAbadDivarBali = _context.AbadDivarBalis.Where(x => x.h == rndh).ToList();
            DataTable DtAbadDivarBalih = clsConvert.ToDataTable(varAbadDivarBali);
            //DataTable DtAbadDivarBalih = clsAbadeKoole.AbadDivarBaliListWithParametr("h=" + rndh.ToString());
            decimal dmh = decimal.Parse(DtAbadDivarBalih.Rows[0]["m"].ToString());
            decimal dfh = decimal.Parse(DtAbadDivarBalih.Rows[0]["f"].ToString());


            //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
            //clsRizMetreUsersses RizMetre = new clsRizMetreUsersses();
            ///////////////
            var varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "120104").ToList();
            DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

            Guid guFBId120104 = new Guid();
            if (DtFBUser.Rows.Count == 0)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "120104";
                FBSave.BahayeVahedZarib = 0;
                _context.FBs.Add(FBSave);
                _context.SaveChanges();
                guFBId120104 = FBSave.ID;
            }
            else
                guFBId120104 = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
            ////////////////
            //var varDtLastRizMetreUsersesShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId120104).ToList();
            //DataTable DtLastRizMetreUsersesShomareh = clsConvert.ToDataTable(varDtLastRizMetreUsersesShomareh);
            //int Shomareh120104 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            var RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId120104).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            long Shomareh120104 = 0;
            if (RizMetreUserLastShomareh != null)
                Shomareh120104 = RizMetreUserLastShomareh.Shomareh + 1;
            else
                Shomareh120104 = 1;


            //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='120104'");
            //int FBId120104 = 0;
            //if (DtFBUser.Rows.Count == 0)
            //    FBId120104 = OperationItemsFB.SaveFB(BarAvordID, "120104", 0);
            //else
            //    FBId120104 = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

            //DataTable DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId120104);
            //int Shomareh120104 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());

            ///////////////
            varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "120302").ToList();
            DtFBUser = clsConvert.ToDataTable(varFBUser);

            Guid guFBId120302 = new Guid();
            if (DtFBUser.Rows.Count == 0)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "120302";
                FBSave.BahayeVahedZarib = 0;
                _context.FBs.Add(FBSave);
                _context.SaveChanges();
                guFBId120302 = FBSave.ID;
            }
            else
                guFBId120302 = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
            ////////////////
            //varDtLastRizMetreUsersesShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId120302).ToList();
            //DtLastRizMetreUsersesShomareh = clsConvert.ToDataTable(varDtLastRizMetreUsersesShomareh);
            //int Shomareh120302 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId120302).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            long Shomareh120302 = 0;
            if (RizMetreUserLastShomareh != null)
                Shomareh120302 = RizMetreUserLastShomareh.Shomareh + 1;
            else
                Shomareh120302 = 1;

            //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='120302'");
            //int FBId120302 = 0;
            //if (DtFBUser.Rows.Count == 0)
            //    FBId120302 = OperationItemsFB.SaveFB(BarAvordID, "120302", 0);
            //else
            //    FBId120302 = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

            //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId120302);
            //int Shomareh120302 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());

            ///////////////
            varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == "120303").ToList();
            DtFBUser = clsConvert.ToDataTable(varFBUser);

            Guid guFBId120303 = new Guid();
            if (DtFBUser.Rows.Count == 0)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "120303";
                FBSave.BahayeVahedZarib = 0;
                _context.FBs.Add(FBSave);
                _context.SaveChanges();
                guFBId120303 = FBSave.ID;
            }
            else
                guFBId120303 = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
            ////////////////
            //varDtLastRizMetreUsersesShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId120303).ToList();
            //DtLastRizMetreUsersesShomareh = clsConvert.ToDataTable(varDtLastRizMetreUsersesShomareh);
            //int Shomareh120303 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId120303).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            long Shomareh120303 = 0;
            if (RizMetreUserLastShomareh != null)
                Shomareh120303 = RizMetreUserLastShomareh.Shomareh + 1;
            else
                Shomareh120303 = 1;

            //DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='120303'");
            //int FBId120303 = 0;
            //if (DtFBUser.Rows.Count == 0)
            //    FBId120303 = OperationItemsFB.SaveFB(BarAvordID, "120303", 0);
            //else
            //    FBId120303 = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

            //DtLastRizMetreUsersesShomareh = clsRizMetreUsersses.GetLastRizMetreUsersesShomareh("FBId=" + FBId120303);
            //int Shomareh120303 = int.Parse(DtLastRizMetreUsersesShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            ///////////////

            for (int i = 1; i < 5; i++)
            {
                decimal hMinw = 0;
                switch (i)
                {
                    case 1: hMinw = decimal.Parse(hMinw1.Trim()); break;
                    case 2: hMinw = decimal.Parse(hMinw2.Trim()); break;
                    case 3: hMinw = decimal.Parse(hMinw3.Trim()); break;
                    case 4: hMinw = decimal.Parse(hMinw4.Trim()); break;
                    default:
                        break;
                }

                decimal dw = 0;
                switch (i)
                {
                    case 1: dw = decimal.Parse(w1.Trim()); break;
                    case 2: dw = decimal.Parse(w2.Trim()); break;
                    case 3: dw = decimal.Parse(w3.Trim()); break;
                    case 4: dw = decimal.Parse(w4.Trim()); break;
                    default:
                        break;
                }

                ///////////////
                if (hMinw <= decimal.Parse("0.5"))
                    hMinw += decimal.Parse("0.5");
                decimal rndhMinw = Math.Round(hMinw);
                var varAbadDivarBali1 = _context.AbadDivarBalis.Where(x => x.h == rndhMinw).ToList();
                DataTable DtAbadDivarBali = clsConvert.ToDataTable(varAbadDivarBali1);

                //DataTable DtAbadDivarBali = clsAbadeKoole.AbadDivarBaliListWithParametr("h=" + rndhMinw.ToString());
                //decimal dm = decimal.Parse(DtAbadDivarBali.Rows[0]["m"].ToString());
                decimal df = decimal.Parse(DtAbadDivarBali.Rows[0]["f"].ToString());

                if (hMinw > 0)
                {
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh120104++;
                    RizMetre.Sharh = " بتن پی دستک  " + i;
                    RizMetre.Tedad = 1;
                    RizMetre.Tool = dw;
                    RizMetre.Arz = (df + dfh) / 2;
                    RizMetre.Ertefa = dmh;
                    RizMetre.Vazn = 0;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId120104;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "500" + PolNum.ToString("D3") + "30";///" بتن پی دستک ";
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    _context.SaveChanges();
                    //RizMetre.Save();


                    dht = decimal.Parse(h) + decimal.Parse(t);

                    decimal dhMinw20cm = hMinw - decimal.Parse("0.2");
                    decimal dht20cm = dht - decimal.Parse("0.2");
                    if (dhMinw20cm <= 5 && dht20cm <= 5)
                    {
                        decimal dxh = decimal.Parse(DtAbadDivarBalih.Rows[0]["x"].ToString());
                        decimal dxhMin = decimal.Parse(DtAbadDivarBali.Rows[0]["x"].ToString());
                        decimal dxMiddle = (dxh + dxhMin) / 2;

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh120104++;
                        RizMetre.Sharh = " بتن دیوار دستک  " + i;
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = dw;
                        RizMetre.Arz = decimal.Parse("0.4");
                        RizMetre.Ertefa = decimal.Parse("0.2");
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId120104;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "31";///" بتن دیوار دستک ";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();
                        /////
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh120302++;
                        RizMetre.Sharh = " بتن دیوار دستک  " + i;
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = dw;
                        RizMetre.Arz = decimal.Parse("0.4");
                        RizMetre.Ertefa = decimal.Parse("0.2");
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId120104;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "31";///" بتن دیوار دستک ";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh120104++;
                        RizMetre.Sharh = " بتن دیوار دستک  " + i;
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = dw;
                        RizMetre.Arz = dxMiddle / 2 + decimal.Parse("0.35");
                        RizMetre.Ertefa = dxMiddle / 2;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId120104;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "31";///" بتن دیوار دستک ";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();
                        /////
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh120302++;
                        RizMetre.Sharh = " بتن دیوار دستک  " + i;
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = dw;
                        RizMetre.Arz = dxMiddle / 2 + decimal.Parse("0.35");
                        RizMetre.Ertefa = dxMiddle / 2;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId120104;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "31";///" بتن دیوار دستک ";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh120104++;
                        RizMetre.Sharh = " بتن دیوار دستک  " + i;
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = dw;
                        RizMetre.Arz = dxMiddle + decimal.Parse("0.35") + ((dht + hMinw) / 6);
                        RizMetre.Ertefa = ((dht + hMinw) / 2) - decimal.Parse("0.2") - (dxMiddle / 2);
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId120104;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "31";///" بتن دیوار دستک ";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();
                        /////
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh120302++;
                        RizMetre.Sharh = " بتن دیوار دستک  " + i;
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = dw;
                        RizMetre.Arz = dxMiddle + decimal.Parse("0.35") + ((dht + hMinw) / 6);
                        RizMetre.Ertefa = ((dht + hMinw) / 2) - decimal.Parse("0.2") - (dxMiddle / 2);
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId120104;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "31";///" بتن دیوار دستک ";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();
                    }
                    else if (dhMinw20cm <= 5 && (dht20cm > 5 && dht20cm <= 10))
                    {
                        decimal dxh = decimal.Parse(DtAbadDivarBalih.Rows[0]["x"].ToString());
                        decimal dxhMin = decimal.Parse(DtAbadDivarBali.Rows[0]["x"].ToString());
                        decimal dxMiddle = (dxh + dxhMin) / 2;

                        decimal Lx = ((dht - 5) / (dht - hMinw)) * dw;
                        //decimal arzx5Meter = (((dht / 3 - hMinw / 3) + dxh - dxhMin) / dw) + hMinw / 3 
                        //    + dxhMin + decimal.Parse("0.35");

                        //decimal LBalaX5Metere = arzx5Meter - (5 / 3);

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh120104++;
                        RizMetre.Sharh = " بتن دیوار دستک  " + i;
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = dw;
                        RizMetre.Arz = decimal.Parse("0.4");
                        RizMetre.Ertefa = decimal.Parse("0.2");
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId120104;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "31";///" بتن دیوار دستک ";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();
                        /////
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh120302++;
                        RizMetre.Sharh = " بتن دیوار دستک  " + i;
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = dw;
                        RizMetre.Arz = decimal.Parse("0.4");
                        RizMetre.Ertefa = decimal.Parse("0.2");
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId120104;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "31";///" بتن دیوار دستک ";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh120104++;
                        RizMetre.Sharh = " بتن دیوار دستک  " + i;
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = dw;
                        RizMetre.Arz = dxMiddle / 2 + decimal.Parse("0.35");
                        RizMetre.Ertefa = dxMiddle / 2;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId120104;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "31";///" بتن دیوار دستک ";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();
                        /////
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh120302++;
                        RizMetre.Sharh = " بتن دیوار دستک  " + i;
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = dw;
                        RizMetre.Arz = dxMiddle / 2 + decimal.Parse("0.35");
                        RizMetre.Ertefa = dxMiddle / 2;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId120104;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "31";///" بتن دیوار دستک ";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh120104++;
                        RizMetre.Sharh = " بتن دیوار دستک  " + i;
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = dw - Lx;
                        RizMetre.Arz = (dxhMin + decimal.Parse("0.6")) / 2 + decimal.Parse("0.35") + ((5 + hMinw) / 6);
                        RizMetre.Ertefa = ((5 + hMinw) / 2) - decimal.Parse("0.2") - ((dxhMin + decimal.Parse("0.6")) / 2);
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId120104;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "31";///" بتن دیوار دستک ";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();
                        /////
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh120302++;
                        RizMetre.Sharh = " بتن دیوار دستک  " + i;
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = dw - Lx;
                        RizMetre.Arz = (dxhMin + decimal.Parse("0.6")) / 2 + decimal.Parse("0.35") + ((5 + hMinw) / 6);
                        RizMetre.Ertefa = ((5 + hMinw) / 2) - decimal.Parse("0.2") - ((dxhMin + decimal.Parse("0.6")) / 2);
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId120104;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "31";///" بتن دیوار دستک ";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();


                        //////////
                        ////////بخش بالای 5 متر
                        /////////
                        decimal y = ((((dht + 5) / 6) + decimal.Parse("0.35") + ((dxh + decimal.Parse("0.6")) / 2)) *
                                  (((dht + 5) / 2) - decimal.Parse("0.2") - ((dxh + decimal.Parse("0.6")) / 2))) /
                                  (decimal.Parse("0.35") + (dxh + decimal.Parse("0.6")));

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh120104++;
                        RizMetre.Sharh = " بتن دیوار دستک  " + i;
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = Lx;
                        RizMetre.Arz = (y + decimal.Parse("0.35") + (dxh + decimal.Parse("0.6"))) / 2;
                        RizMetre.Ertefa = ((dht + 5) / 2) - decimal.Parse("5.2") - ((dxh + decimal.Parse("0.6")) / 2);
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId120104;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "31";///" بتن دیوار دستک ";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();
                        /////
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh120303++;
                        RizMetre.Sharh = " بتن دیوار دستک  " + i;
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = Lx;
                        RizMetre.Arz = (y + decimal.Parse("0.35") + (dxh + decimal.Parse("0.6"))) / 2;
                        RizMetre.Ertefa = ((dht + 5) / 2) - decimal.Parse("5.2") - ((dxh + decimal.Parse("0.6")) / 2);
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId120104;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "31";///" بتن دیوار دستک ";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();

                        ///////////
                        /////////////

                        decimal ArzMiddleDar5 = ((decimal.Parse("0.95") + 5 / 6) + ((2 * dht - 5) / 6) + decimal.Parse("0.35") + dxh) / 2;
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh120104++;
                        RizMetre.Sharh = " بتن دیوار دستک  " + i;
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = Lx;
                        RizMetre.Arz = ArzMiddleDar5;
                        RizMetre.Ertefa = 5;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId120104;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "31";///" بتن دیوار دستک ";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();
                        /////
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh120302++;
                        RizMetre.Sharh = " بتن دیوار دستک  " + i;
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = Lx;
                        RizMetre.Arz = ArzMiddleDar5;
                        RizMetre.Ertefa = 5;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId120104;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "31";///" بتن دیوار دستک ";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();

                    }
                    else if ((dhMinw20cm > 5 && dhMinw20cm <= 10) && (dht20cm > 5 && dht20cm <= 10))
                    {
                        decimal dxh = decimal.Parse(DtAbadDivarBalih.Rows[0]["x"].ToString());
                        decimal dxhMin = decimal.Parse(DtAbadDivarBali.Rows[0]["x"].ToString());
                        decimal dxMiddle = (dxh + dxhMin) / 2;
                        decimal y = ((((dht + hMinw) / 6) + decimal.Parse("0.35") + dxMiddle) *
                                    (((dht + hMinw) / 2) - decimal.Parse("0.2") - (dxMiddle / 2))) /
                                    (decimal.Parse("0.35") + dxMiddle);

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh120104++;
                        RizMetre.Sharh = " بتن دیوار دستک  " + i;
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = dw;
                        RizMetre.Arz = decimal.Parse("0.4");
                        RizMetre.Ertefa = decimal.Parse("0.2");
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId120104;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "31";///" بتن دیوار دستک ";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();
                        /////
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh120303++;
                        RizMetre.Sharh = " بتن دیوار دستک  " + i;
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = dw;
                        RizMetre.Arz = dxMiddle / 2 + decimal.Parse("0.35");
                        RizMetre.Ertefa = dxMiddle / 2;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId120104;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "31";///" بتن دیوار دستک ";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh120104++;
                        RizMetre.Sharh = " بتن دیوار دستک  " + i;
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = dw;
                        RizMetre.Arz = dxMiddle / 2 + decimal.Parse("0.35");
                        RizMetre.Ertefa = dxMiddle / 2;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId120104;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "31";///" بتن دیوار دستک ";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();
                        /////
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh120303++;
                        RizMetre.Sharh = " بتن دیوار دستک  " + i;
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = dw;
                        RizMetre.Arz = dxMiddle / 2 + decimal.Parse("0.35");
                        RizMetre.Ertefa = dxMiddle / 2;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId120104;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "31";///" بتن دیوار دستک ";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh120104++;
                        RizMetre.Sharh = " بتن دیوار دستک  " + i;
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = dw;
                        RizMetre.Arz = (y + decimal.Parse("0.35") + dxMiddle) / 2;
                        RizMetre.Ertefa = ((dht + hMinw) / 2) - decimal.Parse("5.2") - (dxMiddle / 2);
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId120104;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "31";///" بتن دیوار دستک ";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();
                        /////
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh120303++;
                        RizMetre.Sharh = " بتن دیوار دستک  " + i;
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = dw;
                        RizMetre.Arz = (y + decimal.Parse("0.35") + dxMiddle) / 2;
                        RizMetre.Ertefa = ((dht + hMinw) / 2) - decimal.Parse("5.2") - (dxMiddle / 2);
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId120104;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "31";///" بتن دیوار دستک ";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();

                        ///////////
                        /////////////

                        decimal ArzMiddleDar5 = (y + ((dht + hMinw) / 6) + decimal.Parse("0.35") + dxMiddle) / 2;
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh120104++;
                        RizMetre.Sharh = " بتن دیوار دستک  " + i;
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = dw;
                        RizMetre.Arz = ArzMiddleDar5;
                        RizMetre.Ertefa = 5;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId120104;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "31";///" بتن دیوار دستک ";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();
                        /////
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh120302++;
                        RizMetre.Sharh = " بتن دیوار دستک  " + i;
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = dw;
                        RizMetre.Arz = ArzMiddleDar5;
                        RizMetre.Ertefa = 5;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId120104;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "31";///" بتن دیوار دستک ";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        _context.SaveChanges();
                        //RizMetre.Save();
                    }
                }
            }
            return true;
        }
    }
}
