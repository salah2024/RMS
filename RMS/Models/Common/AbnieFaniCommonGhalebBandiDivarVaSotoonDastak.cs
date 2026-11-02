using RMS.Models.Entity;
using System.Data;
using static RMS.Models.Common.AbnieFaniEnum;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Common;

public class AbnieFaniCommonGhalebBandiDivarVaSotoonDastak(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public bool GhalebBandiDivarVaSotoonDastak(Guid PolVaAbroId, NoeFehrestBaha NoeFBId, int PolNum, string D, string TedadDahaneh, Guid BarAvordId,
        string h, string t, string hMinw1, string hMinw2, string hMinw3, string hMinw4,
        string w1, string w2, string w3, string w4)
    {
        decimal dh = decimal.Parse(h);
        decimal dt = decimal.Parse(t);
        decimal dht = dh + dt;

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
        ////////////////


        var RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId080201).OrderByDescending(x => x.Shomareh).FirstOrDefault();
        long Shomareh080201 = 0;
        if (RizMetreUserLastShomareh != null)
            Shomareh080201 = RizMetreUserLastShomareh.Shomareh + 1;
        else
            Shomareh080201 = 1;

        ///////////////
        varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "080202" && x.NoeFBId == NoeFBId);
        //DtFBUser = clsConvert.ToDataTable(varFBUser);

        Guid guFBId080202 = new Guid();
        if (varFBUser == null)
        //if (DtFBUser.Rows.Count == 0)
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
        ////////////////

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
        //if (DtFBUser.Rows.Count == 0)
        if (varFBUser == null)
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

        ///////////////
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
            //_context.SaveChanges();
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
                        decimal res = Math.Round(((dht + hMinw) / 2), 3);

                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080201++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080201;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_حداکثر_2_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();
                    }

                    else if (dht > 2 && dht <= 3)
                    {
                        decimal res1 = Math.Round(((dht - 2) / 2), 3);
                        decimal res2 = Math.Round((((dht + hMinw) / 2) - res1), 3);

                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080201++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res2;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080201;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_حداکثر_2_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080202++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res1;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080202;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_2_تا_3_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);

                        //_context.SaveChanges();
                        //RizMetre.Save();
                    }

                    else if (dht > 3 && dht <= 5)
                    {
                        decimal res1 = Math.Round(((dht - 3) / 2), 3);
                        decimal res2 = Math.Round((((dht - 2) / 2) - res1), 3);
                        decimal res3 = Math.Round((((dht + hMinw) / 2) - res2), 3);

                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080201++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res3;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080201;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_حداکثر_2_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080202++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res2;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080202;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_2_تا_3_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080203++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res1;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080203;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "12";///قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_3_تا_5_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();
                    }

                    else if (dht > 5 && dht <= 7)
                    {
                        decimal res1 = Math.Round(((dht - 5) / 2), 3);
                        decimal res2 = Math.Round((((dht - 3) / 2) - res1), 3);
                        decimal res3 = Math.Round((((dht - 2) / 2) - res2), 3);
                        decimal res4 = Math.Round((((dht + hMinw) / 2) - res3), 3);

                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080201++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res4;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080201;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_حداکثر_2_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080202++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res3;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080202;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_2_تا_3_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080203++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res2;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080203;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "12";///قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_3_تا_5_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();

                        //RizMetre.Save();
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080204++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res1;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080204;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "13";///قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_5_تا_7_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();

                        //RizMetre.Save();
                    }

                    else if (dht > 7 && dht <= 10)
                    {
                        decimal res1 = Math.Round(((dht - 7) / 2), 3);
                        decimal res2 = Math.Round((((dht - 5) / 2) - res1), 3);
                        decimal res3 = Math.Round((((dht - 3) / 2) - res2), 3);
                        decimal res4 = Math.Round((((dht - 2) / 2) - res3), 3);
                        decimal res5 = Math.Round((((dht + hMinw) / 2) - res4), 3);

                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080201++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res5;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080201;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_حداکثر_2_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080202++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res4;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080202;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_2_تا_3_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080203++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res3;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080203;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "12";///قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_3_تا_5_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080204++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res2;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080204;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "13";///قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_5_تا_7_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080205++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 7 تا 10 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res1;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080205;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "14";///قالب دیوارها و ستون های بتنی بین 7 تا 10 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_7_تا_10_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
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
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080201;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_حداکثر_2_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();
                    }

                    else if (dht > 2 && dht <= 3)
                    {
                        decimal res1 = Math.Round((((dht + hMinw) / 2) - 2), 3);
                        decimal res2 = 2;

                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080201++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res2;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080201;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_حداکثر_2_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080202++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res1;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080202;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_2_تا_3_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();
                    }

                    else if (dht > 3 && dht <= 5)
                    {
                        decimal res1 = Math.Round(((dht - 3) / 2), 3);
                        decimal res2 = Math.Round((((dht + hMinw) / 2) - res1 - 2), 3);
                        decimal res3 = 2;

                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080201++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res3;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080201;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_حداکثر_2_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080202++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res2;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080202;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_2_تا_3_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080203++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res1;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080203;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "12";///قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_3_تا_5_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
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
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res4;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080201;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_حداکثر_2_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080202++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res3;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080202;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_2_تا_3_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080203++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res2;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080203;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "12";///قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_3_تا_5_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080204++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res1;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080204;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "13";///قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_5_تا_7_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
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
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res5;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080201;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_حداکثر_2_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080202++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res4;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080202;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_2_تا_3_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080203++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res3;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080203;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "12";///قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_3_تا_5_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080204++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res2;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080204;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "13";///قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_5_تا_7_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080205++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 7 تا 10 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res1;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080205;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "14";///قالب دیوارها و ستون های بتنی بین 7 تا 10 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_7_تا_10_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
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
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080201;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_حداکثر_2_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();
                    }

                    else if (dht > 2 && dht <= 3)
                    {
                        decimal res1 = 1;
                        decimal res2 = 2;

                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080201++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res2;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080201;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_حداکثر_2_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080202++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = 0;
                        RizMetre.Ertefa = res1;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080202;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_2_تا_3_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();
                    }

                    else if (dht > 3 && dht <= 5)
                    {
                        decimal res1 = ((dht + hMinw) / 2) - 3;
                        decimal res2 = 1;
                        decimal res3 = 2;

                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080201++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res3;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080201;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_حداکثر_2_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080202++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res2;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080202;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_2_تا_3_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080203++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res1;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080203;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "12";///قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_3_تا_5_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
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
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res4;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080201;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_حداکثر_2_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080202++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res3;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080202;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_2_تا_3_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080203++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res2;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080203;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "12";///قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_3_تا_5_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080204++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res1;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080204;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "13";///قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_5_تا_7_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
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
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res5;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080201;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_حداکثر_2_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080202++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res4;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080202;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_2_تا_3_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080203++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res3;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080203;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "12";///قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_3_تا_5_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080204++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res2;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080204;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "13";///قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_5_تا_7_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080205++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 7 تا 10 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res1;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080205;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "14";///قالب دیوارها و ستون های بتنی بین 7 تا 10 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_7_تا_10_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
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
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080201;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_حداکثر_2_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();
                    }

                    else if (dht > 2 && dht <= 3)
                    {
                        decimal res1 = 1;
                        decimal res2 = 2;

                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080201++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res2;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080201;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_حداکثر_2_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080202++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res1;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080202;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_2_تا_3_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();
                    }

                    else if (dht > 3 && dht <= 5)
                    {
                        decimal res1 = 2;
                        decimal res2 = 1;
                        decimal res3 = 2;

                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080201++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res3;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080201;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_حداکثر_2_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080202++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res2;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080202;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_2_تا_3_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);

                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080203++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res1;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080203;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "12";///قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_3_تا_5_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
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
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res4;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080201;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_حداکثر_2_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080202++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res3;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080202;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_2_تا_3_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080203++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res2;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080203;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "12";///قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_3_تا_5_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080204++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res1;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080204;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "13";///قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_5_تا_7_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
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
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res5;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080201;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_حداکثر_2_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080202++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res4;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080202;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_2_تا_3_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080203++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res3;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080203;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "12";///قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_3_تا_5_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080204++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res2;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080204;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "13";///قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_5_تا_7_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080205++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 7 تا 10 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res1;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080205;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "14";///قالب دیوارها و ستون های بتنی بین 7 تا 10 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_7_تا_10_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        ///_context.SaveChanges();
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
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080201;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_حداکثر_2_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();
                    }

                    else if (dht > 2 && dht <= 3)
                    {
                        decimal res1 = 1;
                        decimal res2 = 2;

                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080201++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res2;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080201;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_حداکثر_2_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);


                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080202++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res1;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080202;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_2_تا_3_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();
                    }

                    else if (dht > 3 && dht <= 5)
                    {
                        decimal res1 = 2;
                        decimal res2 = 1;
                        decimal res3 = 2;

                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080201++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res3;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080201;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_حداکثر_2_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080202++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res2;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080202;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_2_تا_3_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080203++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res1;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080203;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "12";///قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_3_تا_5_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
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
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res4;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080201;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_حداکثر_2_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080202++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res3;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080202;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_2_تا_3_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080203++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res2;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080203;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "12";///قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_3_تا_5_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080204++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res1;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080204;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "13";///قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_5_تا_7_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
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
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res5;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080201;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "10";///قالب دیوارها و ستون های بتنی حداکثر 2 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_حداکثر_2_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080202++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res4;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080202;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "11";///قالب دیوارها و ستون های بتنی بین 2 تا 3 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_2_تا_3_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080203++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res3;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080203;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "12";///قالب دیوارها و ستون های بتنی بین 3 تا 5 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_3_تا_5_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080204++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res2;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080204;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "13";///قالب دیوارها و ستون های بتنی بین 5 تا 7 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_5_تا_7_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh080205++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " قالب دیوارها و ستون های بتنی بین 7 تا 10 متر دیوار بالی " + i;
                        RizMetre.Tedad = 2;
                        RizMetre.Tool = LW;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = res1;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId080205;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "500" + PolNum.ToString("D3") + "14";///قالب دیوارها و ستون های بتنی بین 7 تا 10 متر دیوار بالی
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
                            PolDetailType = PolDetailTypeEnum.قالب_دیوارها_ستون_های_بتنی_بین_7_تا_10_متر_دیوار_بالی
                        };
                        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
                        //_context.SaveChanges();
                        //RizMetre.Save();
                    }
                }
            }
        }
        return true;
    }
}
