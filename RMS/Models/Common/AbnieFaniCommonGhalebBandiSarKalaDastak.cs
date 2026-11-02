using Microsoft.EntityFrameworkCore;
using RMS.Models.Entity;
using System.Data;
using static RMS.Models.Common.AbnieFaniEnum;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Common;

public class AbnieFaniCommonGhalebBandiSarKalaDastak(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;


    public bool GhalebBandiSarKalaDastak(Guid PolVaAbroId, NoeFehrestBaha NoeFBId, int PolNum, string D, string TedadDahaneh, Guid BarAvordId,
    string t, string hMinw1, string hMinw2, string hMinw3, string hMinw4,
    string w1, string w2, string w3, string w4)
    {
        ///////////////

        clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "080201" && x.NoeFBId == NoeFBId);
        //DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

        Guid guFBId080201 = new Guid();
        //if (DtFBUser.Rows.Count == 0)
        if (varFBUser == null)
        {
            clsFB FBSave = new clsFB();
            FBSave.BarAvordId = BarAvordId;
            FBSave.Shomareh = "080201";
            //FBSave.BahayeVahedZarib = 0;
            FBSave.NoeFBId = NoeFBId;
            _context.FBs.Add(FBSave);

            guFBId080201 = FBSave.ID;
        }
        else
            guFBId080201 = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

        var RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId080201).OrderByDescending(x => x.Shomareh).FirstOrDefault();
        long Shomareh080201 = 0;
        if (RizMetreUserLastShomareh != null)
            Shomareh080201 = RizMetreUserLastShomareh.Shomareh + 1;
        else
            Shomareh080201 = 1;

        varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "080202" && x.NoeFBId == NoeFBId);
        //DtFBUser = clsConvert.ToDataTable(varFBUser);

        Guid guFBId080202 = new Guid();
        //if (DtFBUser.Rows.Count == 0)
        if (varFBUser == null)
        {
            clsFB FBSave = new clsFB();
            FBSave.BarAvordId = BarAvordId;
            FBSave.Shomareh = "080202";
            //FBSave.BahayeVahedZarib = 0;
            FBSave.NoeFBId = NoeFBId;
            _context.FBs.Add(FBSave);

            guFBId080202 = FBSave.ID;
        }
        else
            guFBId080202 = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
        RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId080202).OrderByDescending(x => x.Shomareh).FirstOrDefault();
        long Shomareh080202 = 0;
        if (RizMetreUserLastShomareh != null)
            Shomareh080202 = RizMetreUserLastShomareh.Shomareh + 1;
        else
            Shomareh080202 = 1;

        ///////////////
        varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "080203" && x.NoeFBId == NoeFBId);
        //DtFBUser = clsConvert.ToDataTable(varFBUser);

        Guid guFBId080203 = new Guid();
        //if (DtFBUser.Rows.Count == 0)
        if (varFBUser == null)
        {
            clsFB FBSave = new clsFB();
            FBSave.BarAvordId = BarAvordId;
            FBSave.Shomareh = "080203";
            //FBSave.BahayeVahedZarib = 0;
            FBSave.NoeFBId = NoeFBId;
            _context.FBs.Add(FBSave);
            _context.SaveChanges();
            guFBId080203 = FBSave.ID;
        }
        else
            guFBId080203 = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
        ////////////////
        RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId080203).OrderByDescending(x => x.Shomareh).FirstOrDefault();
        long Shomareh080203 = 0;
        if (RizMetreUserLastShomareh != null)
            Shomareh080203 = RizMetreUserLastShomareh.Shomareh + 1;
        else
            Shomareh080203 = 1;

        ///////////////
        varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "080204" && x.NoeFBId == NoeFBId);
        //DtFBUser = clsConvert.ToDataTable(varFBUser);

        Guid guFBId080204 = new Guid();
        if (varFBUser == null)
        //if (DtFBUser.Rows.Count == 0)
        {
            clsFB FBSave = new clsFB();
            FBSave.BarAvordId = BarAvordId;
            FBSave.Shomareh = "080204";
            //FBSave.BahayeVahedZarib = 0;
            FBSave.NoeFBId = NoeFBId;
            _context.FBs.Add(FBSave);
            //_context.SaveChanges();
            guFBId080204 = FBSave.ID;
        }
        else
            guFBId080204 = varFBUser.ID;//Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
        ////////////////
        RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId080204).OrderByDescending(x => x.Shomareh).FirstOrDefault();
        long Shomareh080204 = 0;
        if (RizMetreUserLastShomareh != null)
            Shomareh080204 = RizMetreUserLastShomareh.Shomareh + 1;
        else
            Shomareh080204 = 1;

        varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "080205" && x.NoeFBId == NoeFBId);
        //DtFBUser = clsConvert.ToDataTable(varFBUser);

        Guid guFBId080205 = new Guid();
        //if (DtFBUser.Rows.Count == 0)
        if (varFBUser == null)
        {
            clsFB FBSave = new clsFB();
            FBSave.BarAvordId = BarAvordId;
            FBSave.Shomareh = "080205";
            //FBSave.BahayeVahedZarib = 0;
            FBSave.NoeFBId = NoeFBId;
            _context.FBs.Add(FBSave);

            guFBId080205 = FBSave.ID;
        }
        else
            guFBId080205 = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
        ////////////////
        RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId080205).OrderByDescending(x => x.Shomareh).FirstOrDefault();
        long Shomareh080205 = 0;
        if (RizMetreUserLastShomareh != null)
            Shomareh080205 = RizMetreUserLastShomareh.Shomareh + 1;
        else
            Shomareh080205 = 1;

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

            decimal dx = decimal.Parse(DtAbadDivarBali.Rows[0]["x"].ToString());

            if (hMinw > 0)
            {
                if (hMinw > 0 && hMinw <= 2)
                {
                    decimal res = hMinw / 6 + dx + decimal.Parse("0.35");
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh080201++;
                    RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                    RizMetre.Sharh = " قالب سرکله دیوار بالی  " + i + " تا 2 متر ";
                    RizMetre.Tedad = 1;
                    RizMetre.Tool = null;
                    RizMetre.Arz = Math.Round(res, 2);
                    RizMetre.Ertefa = hMinw;
                    RizMetre.Vazn = null;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId080201;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "500" + PolNum.ToString("D3") + "20";///" قالب سرکله دیوار بالی  " + i +" تا 2 متر ";
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
                        PolDetailType = PolDetailTypeEnum.قالب_سرکله_دیوار_بالی_تا_2_متر
                    };
                    _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);

                }

                else if (hMinw > 2 && hMinw <= 3)
                {
                    decimal res1 = (hMinw - 1) / 3 + dx + decimal.Parse("0.35");

                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh080201++;
                    RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                    RizMetre.Sharh = " قالب سرکله دیوار بالی  " + i + " تا 2 متر ";
                    RizMetre.Tedad = 2;
                    RizMetre.Tool = null;
                    RizMetre.Arz = Math.Round(res1, 2);
                    RizMetre.Ertefa = 2;
                    RizMetre.Vazn = null;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId080201;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "500" + PolNum.ToString("D3") + "20";///قالب سرکله دیوار بالی  " + i +" تا 2 متر 
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
                        PolDetailType = PolDetailTypeEnum.قالب_سرکله_دیوار_بالی_تا_2_متر
                    };
                    _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);

                    //_context.SaveChanges();

                    //RizMetre.Save();

                    RizMetre = new clsRizMetreUsers();
                    decimal res2 = ((hMinw - 2) / 6) + dx + decimal.Parse("0.35");
                    RizMetre.Shomareh = Shomareh080202++;
                    RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                    RizMetre.Sharh = " قالب سرکله دیوار بالی  " + i + " 2 تا 3 متر ";
                    RizMetre.Tedad = 1;
                    RizMetre.Tool = null;
                    RizMetre.Arz = Math.Round(res2, 2);
                    RizMetre.Ertefa = hMinw - 2;
                    RizMetre.Vazn = null;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId080202;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "500" + PolNum.ToString("D3") + "21";/// قالب سرکله دیوار دیوار بالی  " + i +" 2 تا 3 متر
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
                        PolDetailType = PolDetailTypeEnum.قالب_سرکله_دیوار_بالی_2_تا_3_متر
                    };
                    _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                    //_context.SaveChanges();

                    //RizMetre.Save();
                }

                else if (hMinw > 3 && hMinw <= 5)
                {
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    decimal res1 = (hMinw - 1) / 3 + dx + decimal.Parse("0.35");
                    RizMetre.Shomareh = Shomareh080201++;
                    RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                    RizMetre.Sharh = " قالب سرکله دیوار بالی  " + i + " تا 2 متر ";
                    RizMetre.Tedad = 1;
                    RizMetre.Tool = null;
                    RizMetre.Arz = Math.Round(res1, 2);
                    RizMetre.Ertefa = 2;
                    RizMetre.Vazn = null;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId080201;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "500" + PolNum.ToString("D3") + "20";///قالب سرکله دیوار بالی  " + i +" تا 2 متر 
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
                        PolDetailType = PolDetailTypeEnum.قالب_سرکله_دیوار_بالی_تا_2_متر
                    };
                    _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                    //_context.SaveChanges();

                    //RizMetre.Save();

                    decimal res2 = (2 * hMinw - 5) / 6 + dx + decimal.Parse("0.35");
                    RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh080202++;
                    RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                    RizMetre.Sharh = " قالب سرکله دیوار بالی  " + i + " 2 تا 3 متر ";
                    RizMetre.Tedad = 1;
                    RizMetre.Tool = null;
                    RizMetre.Arz = Math.Round(res2, 2);
                    RizMetre.Ertefa = 1;
                    RizMetre.Vazn = null;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId080202;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "500" + PolNum.ToString("D3") + "21";///قالب سرکله دیوار بالی  " + i +" 2 تا 3 متر 
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
                        PolDetailType = PolDetailTypeEnum.قالب_سرکله_دیوار_بالی_2_تا_3_متر
                    };
                    _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                    //_context.SaveChanges();

                    //RizMetre.Save();

                    decimal res3 = (hMinw - 3) / 6 + dx + decimal.Parse("0.35");
                    RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh080203++;
                    RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                    RizMetre.Sharh = " قالب سرکله دیوار بالی  " + i + " 3 تا 5 متر ";
                    RizMetre.Tedad = 1;
                    RizMetre.Tool = null;
                    RizMetre.Arz = Math.Round(res3, 2);
                    RizMetre.Ertefa = hMinw - 3;
                    RizMetre.Vazn = null;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId080203;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "500" + PolNum.ToString("D3") + "22";///قالب سرکله دیوار بالی  " + i +" 3 تا 5 متر
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
                        PolDetailType = PolDetailTypeEnum.قالب_سرکله_دیوار_بالی_3_تا_5_متر
                    };
                    _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                    //_context.SaveChanges();

                    //RizMetre.Save();
                }

                else if (hMinw > 5 && hMinw <= 7)
                {
                    decimal res1 = (hMinw - 1) / 3 + dx + decimal.Parse("0.35");
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh080201++;
                    RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                    RizMetre.Sharh = " قالب سرکله دیوار بالی  " + i + " تا 2 متر ";
                    RizMetre.Tedad = 2;
                    RizMetre.Tool = null;
                    RizMetre.Arz = Math.Round(res1, 2);
                    RizMetre.Ertefa = 2;
                    RizMetre.Vazn = null;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId080201;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "500" + PolNum.ToString("D3") + "20";/// قالب سرکله دیوار بالی  " + i +" تا 2 متر
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
                        PolDetailType = PolDetailTypeEnum.قالب_سرکله_دیوار_بالی_تا_2_متر
                    };
                    _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                    //_context.SaveChanges();

                    //RizMetre.Save();

                    decimal res2 = (2 * hMinw - 5) / 6 + dx + decimal.Parse("0.35");
                    RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh080202++;
                    RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                    RizMetre.Sharh = " قالب سرکله دیوار بالی  " + i + " 2 تا 3 متر ";
                    RizMetre.Tedad = 1;
                    RizMetre.Tool = null;
                    RizMetre.Arz = Math.Round(res2, 2);
                    RizMetre.Ertefa = 1;
                    RizMetre.Vazn = null;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId080202;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "500" + PolNum.ToString("D3") + "21";///قالب سرکله دیوار بالی  " + i +" 2 تا 3 متر
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
                        PolDetailType = PolDetailTypeEnum.قالب_سرکله_دیوار_بالی_2_تا_3_متر
                    };
                    _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                    //_context.SaveChanges();

                    //RizMetre.Save();

                    decimal res3 = (2 * hMinw - 8) / 6 + dx + decimal.Parse("0.35");
                    RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh080203++;
                    RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                    RizMetre.Sharh = " قالب سرکله دیوار دیوار بالی  " + i + " 3 تا 5 متر ";
                    RizMetre.Tedad = 1;
                    RizMetre.Tool = null;
                    RizMetre.Arz = Math.Round(res3, 2);
                    RizMetre.Ertefa = 2;
                    RizMetre.Vazn = null;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId080203;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "500" + PolNum.ToString("D3") + "22";/// قالب سرکله دیوار بالی  " + i +" 3 تا 5 متر 
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
                        PolDetailType = PolDetailTypeEnum.قالب_سرکله_دیوار_بالی_3_تا_5_متر
                    };
                    _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                    //_context.SaveChanges();

                    //RizMetre.Save();

                    decimal res4 = (hMinw - 5) / 6 + dx + decimal.Parse("0.35");
                    RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh080204++;
                    RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                    RizMetre.Sharh = " قالب سرکله دیوار بالی  " + i + " 5 تا 7 متر ";
                    RizMetre.Tedad = 1;
                    RizMetre.Tool = null;
                    RizMetre.Arz = Math.Round(res4, 2);
                    RizMetre.Ertefa = hMinw - 5;
                    RizMetre.Vazn = null;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId080204;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "500" + PolNum.ToString("D3") + "23";/// قالب سرکله دیوار بالی  " + i +" 5 تا 7 متر 
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
                        PolDetailType = PolDetailTypeEnum.قالب_سرکله_دیوار_بالی_5_تا_7_متر
                    };
                    _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                    //_context.SaveChanges();

                    //RizMetre.Save();
                }

                else if (hMinw > 7 && hMinw <= 10)
                {
                    decimal res1 = (hMinw - 1) / 3 + dx + decimal.Parse("0.35");
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh080201++;
                    RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                    RizMetre.Sharh = " قالب سرکله دیوار بالی  " + i + " تا 2 متر ";
                    RizMetre.Tedad = 1;
                    RizMetre.Tool = null;
                    RizMetre.Arz = Math.Round(res1, 2);
                    RizMetre.Ertefa = 2;
                    RizMetre.Vazn = null;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId080201;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "500" + PolNum.ToString("D3") + "20";///قالب سرکله دیوار بالی  " + i +" تا 2 متر
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
                        PolDetailType = PolDetailTypeEnum.قالب_سرکله_دیوار_بالی_تا_2_متر
                    };
                    _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                    //_context.SaveChanges();

                    //RizMetre.Save();

                    decimal res2 = (2 * hMinw - 5) / 6 + dx + decimal.Parse("0.35");
                    RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh080202++;
                    RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                    RizMetre.Sharh = " قالب سرکله دیوار بالی  " + i + " 2 تا 3 متر ";
                    RizMetre.Tedad = 1;
                    RizMetre.Tool = null;
                    RizMetre.Arz = Math.Round(res2, 2);
                    RizMetre.Ertefa = 1;
                    RizMetre.Vazn = null;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId080202;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "500" + PolNum.ToString("D3") + "21";///قالب سرکله دیوار بالی  " + i +" 2 تا 3 متر
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
                        PolDetailType = PolDetailTypeEnum.قالب_سرکله_دیوار_بالی_2_تا_3_متر
                    };
                    _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                    //_context.SaveChanges();

                    //RizMetre.Save();

                    decimal res3 = (2 * hMinw - 8) / 6 + dx + decimal.Parse("0.35");
                    RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh080203++;
                    RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                    RizMetre.Sharh = " قالب سرکله دیوار بالی  " + i + " 3 تا 5 متر ";
                    RizMetre.Tedad = 1;
                    RizMetre.Tool = null;
                    RizMetre.Arz = Math.Round(res3, 2);
                    RizMetre.Ertefa = 2;
                    RizMetre.Vazn = null;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId080203;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "500" + PolNum.ToString("D3") + "22";/// قالب سرکله دیوار بالی  " + i +" 3 تا 5 متر
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
                        PolDetailType = PolDetailTypeEnum.قالب_سرکله_دیوار_بالی_3_تا_5_متر
                    };
                    _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                    //_context.SaveChanges();

                    //RizMetre.Save();

                    decimal res4 = (2 * hMinw - 12) / 6 + dx + decimal.Parse("0.35");
                    RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh080204++;
                    RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                    RizMetre.Sharh = " قالب سرکله دیوار بالی  " + i + " 5 تا 7 متر ";
                    RizMetre.Tedad = 1;
                    RizMetre.Tool = null;
                    RizMetre.Arz = Math.Round(res4, 2);
                    RizMetre.Ertefa = 2;
                    RizMetre.Vazn = null;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId080204;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "500" + PolNum.ToString("D3") + "23";///قالب سرکله دیوار بالی  " + i +" 5 تا 7 متر
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
                        PolDetailType = PolDetailTypeEnum.قالب_سرکله_دیوار_بالی_5_تا_7_متر
                    };
                    _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                    //_context.SaveChanges();

                    //RizMetre.Save();

                    decimal res5 = (hMinw - 7) / 6 + dx + decimal.Parse("0.35");
                    RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh080205++;
                    RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                    RizMetre.Sharh = " قالب سرکله دیوار بالی  " + i + " 7 تا 10 متر ";
                    RizMetre.Tedad = 1;
                    RizMetre.Tool = null;
                    RizMetre.Arz = Math.Round(res5, 2);
                    RizMetre.Ertefa = hMinw - 7;
                    RizMetre.Vazn = null;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId080205;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "500" + PolNum.ToString("D3") + "24";/// قالب سرکله دیوار بالی  " + i +" 7 تا 10 متر
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
                        PolDetailType = PolDetailTypeEnum.قالب_سرکله_دیوار_بالی_7_تا_10_متر
                    };
                    _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                    //_context.SaveChanges();
                    //RizMetre.Save();
                }
            }
        }
        return true;
    }
}
