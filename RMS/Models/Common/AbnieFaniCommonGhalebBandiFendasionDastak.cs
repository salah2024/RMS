using RMS.Models.Entity;
using System.Data;
using static RMS.Models.Common.AbnieFaniEnum;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Common;

public class AbnieFaniCommonGhalebBandiFendasionDastak(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public bool GhalebBandiFendasionDastak(Guid PolVaAbroId, NoeFehrestBaha NoeFBId, int PolNum, string D, string TedadDahaneh, Guid BarAvordId,
    string LW1j, string LW1p, string LB1W1, string LW2j, string LW2p, string LB1W2,
    string LW3j, string LW3p, string LB1W3, string LW4j, string LW4p, string LB1W4, string h)
    {
        //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
        //clsRizMetreUsersses RizMetre = new clsRizMetreUsersses();
        ///////////////
        clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "080101" && x.NoeFBId == NoeFBId);
        //DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

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

        var RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
        long Shomareh = 0;
        if (RizMetreUserLastShomareh != null)
            Shomareh = RizMetreUserLastShomareh.Shomareh + 1;
        else
            Shomareh = 1;

        ///////////////
        decimal dh = decimal.Parse(h);
        decimal rndh = Math.Round(dh);

        clsAbadDivarBali? varAbadDivarBali = _context.AbadDivarBalis.FirstOrDefault(x => x.h == rndh);
        //DataTable DtAbadDivarBali = clsConvert.ToDataTable(varAbadDivarBali);
        //DataTable DtAbadDivarBali = clsAbadeKoole.AbadDivarBaliListWithParametr("h=" + rndh.ToString());

        decimal m = 0;
        if (varAbadDivarBali!=null)
        //if (DtAbadDivarBali.Rows.Count != 0)
        {
            m = varAbadDivarBali.m; //decimal.Parse(DtAbadDivarBali.Rows[0]["m"].ToString());
        }
        for (int i = 1; i < 5; i++)
        {
            decimal dLWj = 0;
            decimal dLWp = 0;
            decimal dLB1W = 0;
            //decimal dLB2W = 0;
            switch (i)
            {
                case 1:
                    {
                        dLWj = decimal.Parse(LW1j.Trim() == "" ? "0" : LW1j.Trim());
                        dLWp = decimal.Parse(LW1p.Trim() == "" ? "0" : LW1p.Trim());
                        dLB1W = decimal.Parse(LB1W1.Trim() == "" ? "0" : LB1W1.Trim());
                        //dLB2W = decimal.Parse(LB2W1.Trim() == "" ? "0" : LB2W1.Trim());
                        break;
                    }
                case 2:
                    {
                        dLWj = decimal.Parse(LW2j.Trim() == "" ? "0" : LW2j.Trim());
                        dLWp = decimal.Parse(LW2p.Trim() == "" ? "0" : LW2p.Trim());
                        dLB1W = decimal.Parse(LB1W2.Trim() == "" ? "0" : LB1W2.Trim());
                        //dLB2W = decimal.Parse(LB2W2.Trim() == "" ? "0" : LB2W2.Trim());
                        break;
                    }
                case 3:
                    {
                        dLWj = decimal.Parse(LW3j.Trim() == "" ? "0" : LW3j.Trim());
                        dLWp = decimal.Parse(LW3p.Trim() == "" ? "0" : LW3p.Trim());
                        dLB1W = decimal.Parse(LB1W3.Trim() == "" ? "0" : LB1W3.Trim());
                        //dLB2W = decimal.Parse(LB2W3.Trim() == "" ? "0" : LB2W3.Trim());
                        break;
                    }
                case 4:
                    {
                        dLWj = decimal.Parse(LW4j.Trim() == "" ? "0" : LW4j.Trim());
                        dLWp = decimal.Parse(LW4p.Trim() == "" ? "0" : LW4p.Trim());
                        dLB1W = decimal.Parse(LB1W4.Trim() == "" ? "0" : LB1W4.Trim());
                        //dLB2W = decimal.Parse(LB2W4.Trim() == "" ? "0" : LB2W4.Trim());
                        break;
                    }
                default:
                    break;
            }

            if (dLWj != 0)
            {
                clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = Shomareh++;
                RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                RizMetre.Sharh = " قالب طولی پی دیوار بالی " + i + "(جلو)";
                RizMetre.Tedad = 1;
                RizMetre.Tool = dLWj;
                RizMetre.Arz = null;
                RizMetre.Ertefa = m;
                RizMetre.Vazn = null;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId;
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "500" + PolNum.ToString("D3") + "00";///قالب بندی طولی پی دیوار بالی جلو
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
                    PolDetailType = PolDetailTypeEnum.قالب_بندی_طولی_پی_دیوار_بالی_جلو
                };
                _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                //_context.SaveChanges();
                //RizMetre.Save();
            }
            if (dLWp != 0)
            {
                clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = Shomareh++;
                RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                RizMetre.Sharh = " قالب طولی پی دیوار بالی " + i + "(پشت)";
                RizMetre.Tedad = 1;
                RizMetre.Tool = dLWp;
                RizMetre.Arz = null;
                RizMetre.Ertefa = m;
                RizMetre.Vazn = null;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId;
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "500" + PolNum.ToString("D3") + "01";///قالب بندی طولی پی دیوار بالی پشت
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
                    PolDetailType = PolDetailTypeEnum.قالب_بندی_طولی_پی_دیوار_بالی_پشت
                };
                _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                //_context.SaveChanges();
                //RizMetre.Save();
            }
            if (dLB1W != 0)
            {
                clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = Shomareh++;
                RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                RizMetre.Sharh = " قالب عرضی پی دیوار بالی (سرکله) " + i + "(طرف یک)";
                RizMetre.Tedad = 1;
                RizMetre.Tool = null;
                RizMetre.Arz = dLB1W;
                RizMetre.Ertefa = m;
                RizMetre.Vazn = null;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId;
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "500" + PolNum.ToString("D3") + "02";///قالب بندی عرضی پی دیوار بالی طرف 1
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
                    PolDetailType = PolDetailTypeEnum.قالب_بندی_عرضی_پی_دیوار_بالی_طرف_1
                };
                _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                //_context.SaveChanges();
                //RizMetre.Save();
            }
            //if (dLB2W != 0)
            //{
            //    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
            //    RizMetre.Shomareh = Shomareh++;
            //    RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
            //    RizMetre.Sharh = " قالب عرضی پی دیوار بالی (سرکله) " + i + "(طرف دو)";
            //    RizMetre.Tedad = 1;
            //    RizMetre.Tool = null;
            //    RizMetre.Arz = dLB2W;
            //    RizMetre.Ertefa = m;
            //    RizMetre.Vazn = null;
            //    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            //    RizMetre.FBId = guFBId;
            //    RizMetre.OperationsOfHamlId = 1;
            //    RizMetre.Type = "500" + PolNum.ToString("D3") + "03";///قالب بندی عرضی پی دیوار بالی طرف 2
            //    RizMetre.ForItem = "";
            //    RizMetre.UseItem = "";

            //    decimal dMeghdarJoz = 0;
            //    if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
            //        dMeghdarJoz = 0;
            //    else
            //        dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
            //        (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

            //    RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz,3);

            //    _context.RizMetreUserses.Add(RizMetre);

            //    clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
            //    {
            //        PolVaAbroBarAvordId = PolVaAbroId,
            //        RizMetreId = RizMetre.ID,
            //        PolDetailType = PolDetailTypeEnum.قالب_بندی_عرضی_پی_دیوار بالی_طرف_2
            //    };
            //    _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
            //    //_context.SaveChanges();
            //    //RizMetre.Save();
            //}
        }
        return true;
    }

}
