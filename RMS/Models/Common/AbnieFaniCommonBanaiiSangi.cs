using RMS.Controllers.Operation.Common;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Entity;
using System;
using System.Data;
using static RMS.Models.Common.AbnieFaniEnum;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Common;

public class AbnieFaniCommonBanaiiSangi(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;
    public async Task<bool> BanaiiSangiKoole(Guid PolVaAbroId, int PolNum, Guid BarAvordId, NoeFehrestBaha NoeFBId, string LAbro, string w1, string w2, string w3, string w4,
    string f, string m, string n, string k, string h, string t, string b1, string b2, string j, string c1, string c2
    , string p1, string p2, string D, string TedadDahaneh, string NahveEjraDal, string hMinw1, string hMinw2, string hMinw3, string hMinw4, int intNoeBanaii,int Year)
    {
        long intShomareh060203 = 0;
        long intShomareh060301 = 0;

        clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "060203" && x.NoeFBId == NoeFBId);

        Guid guFBId060203 = new Guid();
        if (varFBUser == null)
        {
            clsFB FBSave = new clsFB();
            FBSave.BarAvordId = BarAvordId;
            FBSave.Shomareh = "060203";
            //FBSave.BahayeVahedZarib = 0;
            FBSave.NoeFBId = NoeFBId;
            _context.FBs.Add(FBSave);

            guFBId060203 = FBSave.ID;
        }
        else
            guFBId060203 = varFBUser.ID;


        var RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId060203).OrderByDescending(x => x.Shomareh).FirstOrDefault();
        if (RizMetreUserLastShomareh != null)
            intShomareh060203 = RizMetreUserLastShomareh.Shomareh + 1;
        else
            intShomareh060203 = 1;

        decimal db1 = decimal.Parse(b1);
        decimal db2 = decimal.Parse(b2);

        decimal dh = decimal.Parse(h)- decimal.Parse("0.3");
        decimal d2h = 2 * dh;
        int intN = (int)Math.Truncate(d2h);
        decimal dNe = dh-((intN)* decimal.Parse("0.5"));

        decimal dP = (db1 - db2) - ((db1 - db2) * (dh - decimal.Parse("0.5")) / dh);
        
       // int intP = (int)Math.Truncate(dP);
        //decimal dPe = (db1 - db2 - (intN * intP));
        decimal dPe = (db1 - db2 - (intN *dP));
        int intSum = 0;

        for (int i = 1; i <= intN -1; i++)
        {
            intSum += i;
        }

        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = intShomareh060203++;
        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
        RizMetre.Sharh = "بنایی سنگی دیوار کوله";
        RizMetre.Tedad =  2 * intN;
        RizMetre.Tool = decimal.Parse(LAbro);
       // RizMetre.Arz = Math.Round((db1 - (intSum * (intP / intN))), 3);
        RizMetre.Arz = Math.Round(db1 - ((intSum * dP )/ intN), 3);
        RizMetre.Ertefa = decimal.Parse("0.5");
        RizMetre.Vazn = null;
        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = guFBId060203;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "";
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
            PolDetailType = PolDetailTypeEnum.بنایی_سنگی_دیوار_کوله
        };
        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);


        /////////////
        ///درج حمل///
        /////////////
        SaveHamlDto requestSaveHaml = new SaveHamlDto
        {
            BarAvordUserId = BarAvordId,
            Year = Year,
            NoeFBId = NoeFBId,
            ItemFBShomareh = "060203",
            Shomareh = intShomareh060203++,
            MeghdarJoz = dMeghdarJoz,
            LevelNumber = 1
        };

        List<Guid> lstHamlIds = await HamlCommon.SaveHaml(requestSaveHaml, context);
        foreach (var HamlId in lstHamlIds)
        {
            polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
            {
                PolVaAbroBarAvordId = PolVaAbroId,
                RizMetreId = HamlId,
                PolDetailType = PolDetailTypeEnum.حمل_مصالح_مربوط_به_پل,
            };
            _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
        }

        RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = intShomareh060203++;
        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
        RizMetre.Sharh = "بنایی سنگی دیوار کوله";
        RizMetre.Tedad = 2 ;
        RizMetre.Tool = decimal.Parse(LAbro);
       // RizMetre.Arz = Math.Round(((db1 + db2 + dPe) / 2), 3);
        RizMetre.Arz = Math.Round((( db2 + dPe) ), 3);
        RizMetre.Ertefa = Math.Round(dNe, 3);
        RizMetre.Vazn = null;
        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = guFBId060203;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "";
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
            PolDetailType = PolDetailTypeEnum.بنایی_سنگی_دیوار_کوله
        };
        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);

        /////////////
        ///درج حمل///
        /////////////
        requestSaveHaml = new SaveHamlDto
        {
            BarAvordUserId = BarAvordId,
            Year = Year,
            NoeFBId = NoeFBId,
            ItemFBShomareh = "060203",
            Shomareh = intShomareh060203++,
            MeghdarJoz = dMeghdarJoz,
            LevelNumber = 1
        };

        lstHamlIds = await HamlCommon.SaveHaml(requestSaveHaml, context);
        foreach (var HamlId in lstHamlIds)
        {
            polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
            {
                PolVaAbroBarAvordId = PolVaAbroId,
                RizMetreId = HamlId,
                PolDetailType = PolDetailTypeEnum.حمل_مصالح_مربوط_به_پل,
            };
            _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
        }


        ///اضافه بها بنایی در دیوار سنگی
        ///
        varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "060301" && x.NoeFBId == NoeFBId);

        Guid guFBId060301 = new Guid();
        if (varFBUser == null)
        {
            clsFB FBSave = new clsFB();
            FBSave.BarAvordId = BarAvordId;
            FBSave.Shomareh = "060301";
            //FBSave.BahayeVahedZarib = 0;
            FBSave.NoeFBId = NoeFBId;
            _context.FBs.Add(FBSave);

            guFBId060301 = FBSave.ID;
        }
        else
            guFBId060301 = varFBUser.ID;


        RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId060301).OrderByDescending(x => x.Shomareh).FirstOrDefault();
        if (RizMetreUserLastShomareh != null)
            intShomareh060301 = RizMetreUserLastShomareh.Shomareh + 1;
        else
            intShomareh060301 = 1;

        RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = intShomareh060301++;
        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
        RizMetre.Sharh = "اضافه بها بنایی سنگی در دیوار کوله";
        RizMetre.Tedad = 2 * intN;
        RizMetre.Tool = decimal.Parse(LAbro);
        //RizMetre.Arz = Math.Round((db1 - (intSum * (intP / intN))), 3);
        RizMetre.Arz = Math.Round(db1 - ((intSum * dP) / intN), 3);
        RizMetre.Ertefa = decimal.Parse("0.5");
        RizMetre.Vazn = null;
        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = guFBId060301;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "";
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
            PolDetailType = PolDetailTypeEnum.اضافه_بها_بنایی_سنگی_در_دیوارکوله
        };
        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);


        RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = intShomareh060301++;
        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
        RizMetre.Sharh = "اضافه بها بنایی سنگی در دیوار کوله";
        RizMetre.Tedad = 2;
        RizMetre.Tool = decimal.Parse(LAbro);
        RizMetre.Arz = Math.Round(((db1 + db2 + dPe) / 2), 3);
        RizMetre.Ertefa = Math.Round(dNe, 3);
        RizMetre.Vazn = null;
        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = guFBId060301;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "";
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
            PolDetailType = PolDetailTypeEnum.اضافه_بها_بنایی_سنگی_در_دیوارکوله
        };
        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);


        /////////////
        //////////////
        ///060402
        ///070202
        ///////////////////
        //////////////
        ///


        long intShomareh060402 = 0;
        long intShomareh070202 = 0;

        varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "060402" && x.NoeFBId == NoeFBId);

        Guid guFBId060402 = new Guid();
        if (varFBUser == null)
        {
            clsFB FBSave = new clsFB();
            FBSave.BarAvordId = BarAvordId;
            FBSave.Shomareh = "060402";
            //FBSave.BahayeVahedZarib = 0;
            FBSave.NoeFBId = NoeFBId;
            _context.FBs.Add(FBSave);

            guFBId060402 = FBSave.ID;
        }
        else
            guFBId060402 = varFBUser.ID;


        RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId060203).OrderByDescending(x => x.Shomareh).FirstOrDefault();
        if (RizMetreUserLastShomareh != null)
            intShomareh060402 = RizMetreUserLastShomareh.Shomareh + 1;
        else
            intShomareh060402 = 1;

        RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = intShomareh060402++;
        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
        RizMetre.Sharh = "اضافه بها نما سازی با سنگ باد بر در دیوار کوله";
        RizMetre.Tedad = 2;
        RizMetre.Tool = decimal.Parse(LAbro);
        RizMetre.Arz = null;
        RizMetre.Ertefa = dh;
        RizMetre.Vazn = null;
        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = guFBId060402;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "";
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
            PolDetailType = PolDetailTypeEnum.اضافه_بها_نماسازی_باسنگ_بادبر_دیوارکوله
        };
        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);


        ///بندکشی نمای سنگی بادبر
        ///
        varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "070202" && x.NoeFBId == NoeFBId);

        Guid guFBId070202 = new Guid();
        if (varFBUser == null)
        {
            clsFB FBSave = new clsFB();
            FBSave.BarAvordId = BarAvordId;
            FBSave.Shomareh = "070202";
            //FBSave.BahayeVahedZarib = 0;
            FBSave.NoeFBId = NoeFBId;
            _context.FBs.Add(FBSave);

            guFBId070202 = FBSave.ID;
        }
        else
            guFBId070202 = varFBUser.ID;


        RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId060301).OrderByDescending(x => x.Shomareh).FirstOrDefault();
        if (RizMetreUserLastShomareh != null)
            intShomareh070202 = RizMetreUserLastShomareh.Shomareh + 1;
        else
            intShomareh070202 = 1;

        RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = intShomareh070202++;
        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
        RizMetre.Sharh = "بندکشی نمای سنگی بادبر";
        RizMetre.Tedad = 2;
        RizMetre.Tool = decimal.Parse(LAbro);
        RizMetre.Arz = null;
        RizMetre.Ertefa = dh;
        RizMetre.Vazn = null;
        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = guFBId070202;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "";
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
            PolDetailType = PolDetailTypeEnum.بندکشی_نمای_سنگی_بادبر_دیوارکوله
        };
        _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);

        ///////////////
        //////060606
        //////

        if (decimal.Parse(LAbro) > 5)
        {
            decimal dNk = decimal.Parse(LAbro) / 5;
            int intNk = (int)Math.Truncate(dNk);


            long intShomareh060606 = 0;

            varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "060606" && x.NoeFBId == NoeFBId);

            Guid guFBId060606 = new Guid();
            if (varFBUser == null)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "060606";
                //FBSave.BahayeVahedZarib = 0;
                FBSave.NoeFBId = NoeFBId;
                _context.FBs.Add(FBSave);

                guFBId060606 = FBSave.ID;
            }
            else
                guFBId060606 = varFBUser.ID;


            RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId060606).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            if (RizMetreUserLastShomareh != null)
                intShomareh060606 = RizMetreUserLastShomareh.Shomareh + 1;
            else
                intShomareh060606 = 1;

            RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = intShomareh060606++;
            RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
            RizMetre.Sharh = "تعبیه درز انقطاع در بنایی سنگی در دیوار کوله";
            RizMetre.Tedad = 2 * intNk + 4;
            RizMetre.Tool = null;
            //RizMetre.Arz = Math.Round((db1 - (intSum * (intP / intN))), 3);
            RizMetre.Arz = Math.Round(db1 - ((intSum * dP) / intN), 3);
            RizMetre.Ertefa = decimal.Parse("0.5");
            RizMetre.Vazn = null;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId060606;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "";
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
                PolDetailType = PolDetailTypeEnum.تعبیه_درز_انقطاع_در_بنایی_سنگی_دیوارکوله
            };
            _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);



            ///ردیف دوم
            RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = intShomareh060606++;
            RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
            RizMetre.Sharh = "تعبیه درز انقطاع در بنایی سنگی در دیوار کوله";
            RizMetre.Tedad = 2 * intNk + 4;
            RizMetre.Tool = null;
            RizMetre.Arz = Math.Round(((db1 + db2 + dPe) / 2), 3);
            RizMetre.Ertefa = Math.Round(dNe, 3);
            RizMetre.Vazn = null;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId060606;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "";
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
                PolDetailType = PolDetailTypeEnum.تعبیه_درز_انقطاع_در_بنایی_سنگی_دیوارکوله
            };
            _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);


        }
       return true;
    }

    public async Task<bool> BanaiiSangiDastak(Guid PolVaAbroId, int PolNum, Guid BarAvordId, NoeFehrestBaha NoeFBId, string LAbro, string w1, string w2, string w3, string w4,
            string f, string m, string n, string k, string h, string t, string b1, string b2, string j, string c1, string c2
            , string p1, string p2, string D, string TedadDahaneh, string NahveEjraDal, string hMinw1, string hMinw2, string hMinw3, string hMinw4, int intNoeBanaii,
            clsAbadDivarBali AbadDivarBaliForhMinw1, clsAbadDivarBali AbadDivarBaliForhMinw2, clsAbadDivarBali AbadDivarBaliForhMinw3, clsAbadDivarBali AbadDivarBaliForhMinw4,int Year)
    {
        long intShomareh060203 = 0;
        long intShomareh060301 = 0;

        clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "060203" && x.NoeFBId == NoeFBId);

        Guid guFBId060203 = new Guid();
        if (varFBUser == null)
        {
            clsFB FBSave = new clsFB();
            FBSave.BarAvordId = BarAvordId;
            FBSave.Shomareh = "060203";
            //FBSave.BahayeVahedZarib = 0;
            FBSave.NoeFBId = NoeFBId;
            _context.FBs.Add(FBSave);

            guFBId060203 = FBSave.ID;
        }
        else
            guFBId060203 = varFBUser.ID;


        var RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId060203).OrderByDescending(x => x.Shomareh).FirstOrDefault();
        if (RizMetreUserLastShomareh != null)
            intShomareh060203 = RizMetreUserLastShomareh.Shomareh + 1;
        else
            intShomareh060203 = 1;


        for (int ii = 1; ii < 5; ii++)
        {
            decimal hMinw = 0;
            switch (ii)
            {
                case 1: hMinw = decimal.Parse(hMinw1.Trim()); break;
                case 2: hMinw = decimal.Parse(hMinw2.Trim()); break;
                case 3: hMinw = decimal.Parse(hMinw3.Trim()); break;
                case 4: hMinw = decimal.Parse(hMinw4.Trim()); break;
                default:
                    break;
            }

            decimal xMinw = 0;
            switch (ii)
            {
                case 1: xMinw = AbadDivarBaliForhMinw1.x; break;
                case 2: xMinw = AbadDivarBaliForhMinw2.x; break;
                case 3: xMinw = AbadDivarBaliForhMinw3.x; break;
                case 4: xMinw = AbadDivarBaliForhMinw4.x; break;
                default:
                    break;
            }

            decimal LW = 0;
            switch (ii)
            {
                case 1: LW = decimal.Parse(w1.Trim()); break;
                case 2: LW = decimal.Parse(w2.Trim()); break;
                case 3: LW = decimal.Parse(w3.Trim()); break;
                case 4: LW = decimal.Parse(w4.Trim()); break;
                default:
                    break;
            }

            decimal db1 = decimal.Parse(b1);
            decimal db2 = decimal.Parse(b2);

            //decimal dh = decimal.Parse(h);
            decimal d2hMinw = 2 * hMinw;
            int intNb = (int)Math.Truncate(d2hMinw);//تعداد پله 50 سانت ارتفاع
            decimal dPb = decimal.Parse("0.5");      // سانت 50  ارتفاع پله
            decimal dPbe = hMinw - intNb / 2;// decimal.Parse("0.5");      //   ارتفاع پله آخر
            decimal dNb = decimal.Parse("0.5") / 3;   // عرض پله 50 سانتی
            decimal dNbe = (hMinw /3) - (intNb * dNb);   // عرض پله آخر

            int intSum = 0;

            for (int i = 1; i <= intNb - 1; i++)
            {
                intSum += i;
            }

            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = intShomareh060203++;
            RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
            RizMetre.Sharh = "بنایی سنگی دیوار بالی " + ii;
            RizMetre.Tedad = intNb;
            RizMetre.Tool = LW;
            RizMetre.Arz = Math.Round(((hMinw / 3) + xMinw + decimal.Parse("0.35") - (intSum * (dNb / intNb))), 3);
            RizMetre.Ertefa = decimal.Parse("0.5");
            RizMetre.Vazn = null;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId060203;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "";
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
                PolDetailType = PolDetailTypeEnum.بنایی_سنگی_دیوار_بالی
            };
            _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);

            /////////////
            ///درج حمل///
            /////////////
            SaveHamlDto requestSaveHaml = new SaveHamlDto
            {
                BarAvordUserId = BarAvordId,
                Year = Year,
                NoeFBId = NoeFBId,
                ItemFBShomareh = "060203",
                Shomareh = intShomareh060203++,
                MeghdarJoz = dMeghdarJoz,
                LevelNumber = 1
            };

            List<Guid> lstHamlIds = await HamlCommon.SaveHaml(requestSaveHaml, context);
            foreach (var HamlId in lstHamlIds)
            {
                polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                {
                    PolVaAbroBarAvordId = PolVaAbroId,
                    RizMetreId = HamlId,
                    PolDetailType = PolDetailTypeEnum.حمل_مصالح_مربوط_به_پل,
                };
                _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
            }


            RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = intShomareh060203++;
            RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
            RizMetre.Sharh = "بنایی سنگی دیوار بالی " + ii;
            RizMetre.Tedad = 1;
            RizMetre.Tool = LW;
            RizMetre.Arz = Math.Round(( xMinw + decimal.Parse("0.35") + (dNbe)), 3);
            RizMetre.Ertefa = Math.Round(dPbe, 3);
            RizMetre.Vazn = null;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId060203;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "";
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
                PolDetailType = PolDetailTypeEnum.بنایی_سنگی_دیوار_بالی
            };
            _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);

            /////////////
            ///درج حمل///
            /////////////
            requestSaveHaml = new SaveHamlDto
            {
                BarAvordUserId = BarAvordId,
                Year = Year,
                NoeFBId = NoeFBId,
                ItemFBShomareh = "060203",
                Shomareh = intShomareh060203++,
                MeghdarJoz = dMeghdarJoz,
                LevelNumber = 1
            };

            lstHamlIds = await HamlCommon.SaveHaml(requestSaveHaml, context);
            foreach (var HamlId in lstHamlIds)
            {
                polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                {
                    PolVaAbroBarAvordId = PolVaAbroId,
                    RizMetreId = HamlId,
                    PolDetailType = PolDetailTypeEnum.حمل_مصالح_مربوط_به_پل,
                };
                _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
            }

            ///اضافه بها بنایی سنگی در دیوار  بالی
            ///
            varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "060301" && x.NoeFBId == NoeFBId);

            Guid guFBId060301 = new Guid();
            if (varFBUser == null)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "060301";
                //FBSave.BahayeVahedZarib = 0;
                FBSave.NoeFBId = NoeFBId;
                _context.FBs.Add(FBSave);

                guFBId060301 = FBSave.ID;
            }
            else
                guFBId060301 = varFBUser.ID;


            RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId060301).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            if (RizMetreUserLastShomareh != null)
                intShomareh060301 = RizMetreUserLastShomareh.Shomareh + 1;
            else
                intShomareh060301 = 1;

            RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = intShomareh060301++;
            RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
            RizMetre.Sharh = "اضافه بها بنایی سنگی در دیوار بالی " + ii;
            RizMetre.Tedad =  intNb;
            RizMetre.Tool = LW;
            RizMetre.Arz = Math.Round(((hMinw / 3) + xMinw + decimal.Parse("0.35") - (intSum * (dNb / intNb))), 3);
            RizMetre.Ertefa = decimal.Parse("0.5");
            RizMetre.Vazn = null;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId060301;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "";
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
                PolDetailType = PolDetailTypeEnum.اضافه_بها_بنایی_سنگی_در_دیوار_بالی
            };
            _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);


            RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = intShomareh060301++;
            RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
            RizMetre.Sharh = "اضافه بها بنایی سنگی در دیوار بالی " + ii;
            RizMetre.Tedad = 1;
            RizMetre.Tool = LW;
            RizMetre.Arz = Math.Round((xMinw + decimal.Parse("0.35") + (dNbe)), 3);
            RizMetre.Ertefa = Math.Round(dPbe, 3);
            RizMetre.Vazn = null;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId060301;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "";
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
                PolDetailType = PolDetailTypeEnum.اضافه_بها_بنایی_سنگی_در_دیوار_بالی
            };
            _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);


            /////////////
            //////////////
            ///060402
            ///070202
            ///////////////////
            //////////////
            ///


            long intShomareh060402 = 0;
            long intShomareh070202 = 0;

            varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "060402" && x.NoeFBId == NoeFBId);

            Guid guFBId060402 = new Guid();
            if (varFBUser == null)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "060402";
                //FBSave.BahayeVahedZarib = 0;
                FBSave.NoeFBId = NoeFBId;
                _context.FBs.Add(FBSave);

                guFBId060402 = FBSave.ID;
            }
            else
                guFBId060402 = varFBUser.ID;


            RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId060203).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            if (RizMetreUserLastShomareh != null)
                intShomareh060402 = RizMetreUserLastShomareh.Shomareh + 1;
            else
                intShomareh060402 = 1;

            RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = intShomareh060402++;
            RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
            RizMetre.Sharh = "اضافه بها نما سازی با سنگ باد بر در دیوار بالی " + ii;
            RizMetre.Tedad = 1;
            RizMetre.Tool = LW;
            RizMetre.Arz = null;
            RizMetre.Ertefa = hMinw;
            RizMetre.Vazn = null;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId060402;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "";
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
                PolDetailType = PolDetailTypeEnum.اضافه_بها_نماسازی_باسنگ_بادبر_دیواربالی
            };
            _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);


            ///بندکشی نمای سنگی بادبر
            ///
            varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "070202" && x.NoeFBId == NoeFBId);

            Guid guFBId070202 = new Guid();
            if (varFBUser == null)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "070202";
                //FBSave.BahayeVahedZarib = 0;
                FBSave.NoeFBId = NoeFBId;
                _context.FBs.Add(FBSave);

                guFBId070202 = FBSave.ID;
            }
            else
                guFBId070202 = varFBUser.ID;


            RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId060301).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            if (RizMetreUserLastShomareh != null)
                intShomareh070202 = RizMetreUserLastShomareh.Shomareh + 1;
            else
                intShomareh070202 = 1;

            RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = intShomareh070202++;
            RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
            RizMetre.Sharh = "بندکشی نمای سنگی بادبر در دیوار بالی " + ii;
            RizMetre.Tedad = 1;
            RizMetre.Tool = LW;
            RizMetre.Arz = null;
            RizMetre.Ertefa = hMinw;
            RizMetre.Vazn = null;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId070202;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "";
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
                PolDetailType = PolDetailTypeEnum.بندکشی_نمای_سنگی_بادبر_دیواربالی
            };
            _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
        }
        return true;
    }
}
