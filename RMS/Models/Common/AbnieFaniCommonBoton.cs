using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.Operation.Common;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Entity;
using RMS.Models.StoredProceduresData;
using System.Data;
using static RMS.Models.Common.AbnieFaniEnum;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Common;

public class AbnieFaniCommonBoton(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;


    public async Task<bool> Boton(Guid PolVaAbroId, int PolNum, Guid BarAvordId, NoeFehrestBaha NoeFBId, string LAbro, string w1, string w2, string w3, string w4,
        string f, string m, string n, string k, string h, string t, string b1, string b2, string j, string c1, string c2
        , string p1, string p2, string D, string TedadDahaneh, string NahveEjraDal, string hMinw1, string hMinw2, string hMinw3, string hMinw4, int intNoeBanaii, int Year)
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

        clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "120302" && x.NoeFBId == NoeFBId);
        //DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

        Guid guFBId120302 = new Guid();
        //if (DtFBUser.Rows.Count == 0)
        if (varFBUser == null)
        {
            clsFB FBSave = new clsFB();
            FBSave.BarAvordId = BarAvordId;
            FBSave.Shomareh = "120302";
            //FBSave.BahayeVahedZarib = 0;
            FBSave.NoeFBId = NoeFBId;
            _context.FBs.Add(FBSave);
            //_context.SaveChanges();
            guFBId120302 = FBSave.ID;
        }
        else
            guFBId120302 = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
        ////////////////
        var RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId120302).OrderByDescending(x => x.Shomareh).FirstOrDefault();
        if (RizMetreUserLastShomareh != null)
            intShomareh120302 = RizMetreUserLastShomareh.Shomareh + 1;
        else
            intShomareh120302 = 1;

        DataTable DtLastRizMetreUsersesShomareh = new DataTable();

        varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "120303" && x.NoeFBId == NoeFBId);
        //DtFBUser = clsConvert.ToDataTable(varFBUser);

        Guid guFBId120303 = new Guid();
        //if (DtFBUser.Rows.Count == 0)
        if (varFBUser == null)
        {
            clsFB FBSave = new clsFB();
            FBSave.BarAvordId = BarAvordId;
            FBSave.Shomareh = "120303";
            //FBSave.BahayeVahedZarib = 0;
            FBSave.NoeFBId = NoeFBId;
            _context.FBs.Add(FBSave);
            //_context.SaveChanges();
            guFBId120303 = FBSave.ID;
        }
        else
            guFBId120303 = varFBUser.ID;//Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
        ////////////////
        RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId120303).OrderByDescending(x => x.Shomareh).FirstOrDefault();
        if (RizMetreUserLastShomareh != null)
            intShomareh120303 = RizMetreUserLastShomareh.Shomareh + 1;
        else
            intShomareh120303 = 1;

        //////////

        varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "120304" && x.NoeFBId == NoeFBId);
        //DtFBUser = clsConvert.ToDataTable(varFBUser);

        Guid guFBId120304 = new Guid();
        //if (DtFBUser.Rows.Count == 0)
        if (varFBUser == null)
        {
            clsFB FBSave = new clsFB();
            FBSave.BarAvordId = BarAvordId;
            FBSave.Shomareh = "120304";
            //FBSave.BahayeVahedZarib = 0;
            FBSave.NoeFBId = NoeFBId;
            _context.FBs.Add(FBSave);
            //_context.SaveChanges();
            guFBId120304 = FBSave.ID;
        }
        else
            guFBId120304 = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
        ////////////////

        RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId120304).OrderByDescending(x => x.Shomareh).FirstOrDefault();
        if (RizMetreUserLastShomareh != null)
            intShomareh120304 = RizMetreUserLastShomareh.Shomareh + 1;
        else
            intShomareh120304 = 1;


        varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "120305" && x.NoeFBId == NoeFBId);
        //DtFBUser = clsConvert.ToDataTable(varFBUser);

        Guid guFBId120305 = new Guid();
        //if (DtFBUser.Rows.Count == 0)
        if (varFBUser == null)
        {
            clsFB FBSave = new clsFB();
            FBSave.BarAvordId = BarAvordId;
            FBSave.Shomareh = "120305";
            //FBSave.BahayeVahedZarib = 0;
            FBSave.NoeFBId = NoeFBId;
            _context.FBs.Add(FBSave);
            guFBId120305 = FBSave.ID;
        }
        else
            guFBId120305 = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
        ////////////////
        RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId120305).OrderByDescending(x => x.Shomareh).FirstOrDefault();
        if (RizMetreUserLastShomareh != null)
            intShomareh120305 = RizMetreUserLastShomareh.Shomareh + 1;
        else
            intShomareh120305 = 1;

        varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "120310" && x.NoeFBId == NoeFBId);
        //DtFBUser = clsConvert.ToDataTable(varFBUser);

        Guid guFBId120310 = new Guid();
        //if (DtFBUser.Rows.Count == 0)
        if (varFBUser == null)
        {
            clsFB FBSave = new clsFB();
            FBSave.BarAvordId = BarAvordId;
            FBSave.Shomareh = "120310";
            //FBSave.BahayeVahedZarib = 0;
            FBSave.NoeFBId = NoeFBId;
            _context.FBs.Add(FBSave);
            guFBId120310 = FBSave.ID;
        }
        else
            guFBId120310 = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
        ////////////////
        RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId120310).OrderByDescending(x => x.Shomareh).FirstOrDefault();
        if (RizMetreUserLastShomareh != null)
            intShomareh120310 = RizMetreUserLastShomareh.Shomareh + 1;
        else
            intShomareh120310 = 1;

        string strParam = "PolVaAbroId='" + PolVaAbroId + "'";
        var QuesForAbnieFaniValues = new SqlParameter("@Parameter", strParam);

        var QuesForAbnieFani = _context.Set<QuesForAbnieFaniValuesDto>()
            .FromSqlRaw("EXEC QuesForAbnieFaniValuesListWithParameter @Parameter", QuesForAbnieFaniValues)
            .ToList();

        DataTable DtQuesForAbnieFaniValues = clsConvert.ToDataTable(QuesForAbnieFani);

        string strShomareh = "120101"; //DtQuesForAbnieFaniValues.Rows[0]["Shomareh"].ToString().Trim();
        varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strShomareh && x.NoeFBId == NoeFBId);
        //DtFBUser = clsConvert.ToDataTable(varFBUser);
        Guid guFBId = new Guid();
        //if (DtFBUser.Rows.Count == 0)
        if (varFBUser == null)
        {
            clsFB FBSave = new clsFB();
            FBSave.BarAvordId = BarAvordId;
            FBSave.Shomareh = strShomareh;
            //FBSave.BahayeVahedZarib = 0;
            FBSave.NoeFBId = NoeFBId;
            _context.FBs.Add(FBSave);
            guFBId = FBSave.ID;
        }
        else
            guFBId = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
        ////////////////

        RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
        long Shomareh = 0;
        if (RizMetreUserLastShomareh != null)
            Shomareh = RizMetreUserLastShomareh.Shomareh + 1;
        else
            Shomareh = 1;

        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = Shomareh;
        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
        RizMetre.Sharh = "بتن مگر پی کوله";
        RizMetre.Tedad = 2;
        RizMetre.Tool = decimal.Parse(LAbro.Trim()) + decimal.Parse("10") / 100;// decimal.Parse(LAbro.Trim()) + decimal.Parse(DtQuesForAbnieFaniValues.Rows[0]["Value"].ToString().Trim()) / 100;
        RizMetre.Arz = decimal.Parse(f.Trim()) + decimal.Parse("10") / 100;//decimal.Parse(f.Trim()) + decimal.Parse(DtQuesForAbnieFaniValues.Rows[0]["Value"].ToString().Trim()) / 100;
        RizMetre.Ertefa = decimal.Parse("0.1");
        RizMetre.Vazn = null;
        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = guFBId;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "30";///بتن مگر پی کوله
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
            PolDetailType = PolDetailTypeEnum.بتن_مگر_پی_کوله
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
            ItemFBShomareh = "120101",
            Shomareh = ++Shomareh,
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

        _context.SaveChanges();


        //////////بتن مگر پی پایه میانی
        int tedad = int.Parse(TedadDahaneh) - 1;
        if (tedad != 0)
        {
            RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = ++Shomareh;
            RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
            RizMetre.Sharh = "بتن مگر پی پایه میانی";
            RizMetre.Tedad = tedad;
            RizMetre.Tool = decimal.Parse(LAbro.Trim()) + decimal.Parse("10") / 100;//decimal.Parse(LAbro.Trim()) + decimal.Parse(DtQuesForAbnieFaniValues.Rows[0]["Value"].ToString().Trim()) / 100;
            RizMetre.Arz = decimal.Parse(k.Trim()) + decimal.Parse("10") / 100;//decimal.Parse(k.Trim()) + decimal.Parse(DtQuesForAbnieFaniValues.Rows[0]["Value"].ToString().Trim()) / 100;
            RizMetre.Ertefa = decimal.Parse("0.1");
            RizMetre.Vazn = null;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "300" + PolNum.ToString("D3") + "31";///بتن مگر پی پایه میانی
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
                PolDetailType = PolDetailTypeEnum.بتن_مگر_پی_پایه_میانی
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
                ItemFBShomareh = "120101",
                Shomareh = ++Shomareh,
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
        }


        decimal dht = decimal.Parse(h) + decimal.Parse(t);
        if (dht <= decimal.Parse("0.5"))
            dht += decimal.Parse("0.5");
        decimal rndh = Math.Round(dht);
        clsAbadDivarBali? varAbadDivarBali = _context.AbadDivarBalis.Where(x => x.h == rndh).FirstOrDefault();

        decimal dmh = varAbadDivarBali != null ? varAbadDivarBali.m : 0; //decimal.Parse(DtAbadDivarBalih.Rows[0]["m"].ToString());
        decimal dfh = varAbadDivarBali != null ? varAbadDivarBali.f : 0; //decimal.Parse(DtAbadDivarBalih.Rows[0]["f"].ToString());

        decimal d2dahm = decimal.Parse("0.2");

        ////////////
        ///
        if (decimal.Parse(w1.Trim()) != 0)
        {
            decimal dhMinw1 = decimal.Parse(hMinw1);
            if (dhMinw1 <= decimal.Parse("0.5"))
                dhMinw1 += decimal.Parse("0.5");
            decimal rndhMinw = Math.Round(dhMinw1);
            clsAbadDivarBali? varAbadDivarBali1 = _context.AbadDivarBalis.Where(x => x.h == rndhMinw).FirstOrDefault();
            decimal df = varAbadDivarBali1 != null ? varAbadDivarBali1.f : 0;


            RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = Shomareh;
            RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
            RizMetre.Sharh = "بتن مگر پی دیوار بالی 1";
            RizMetre.Tedad = 1;
            RizMetre.Tool = decimal.Parse(w1.Trim()) + decimal.Parse("0.1");
            RizMetre.Arz = Math.Round((((df + dfh) / 2) + d2dahm), 2);
            RizMetre.Ertefa = decimal.Parse("0.1");
            RizMetre.Vazn = null;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "300" + PolNum.ToString("D3") + "30";///بتن مگر دیوار بالی
            RizMetre.ForItem = "";
            RizMetre.UseItem = "";

            decimal dMeghdarJoz1 = 0;
            if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                dMeghdarJoz1 = 0;
            else
                dMeghdarJoz1 += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

            RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz1, 2);

            _context.RizMetreUserses.Add(RizMetre);

            clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre1 = new clsPolVaAbroBarAvordRizMetre
            {
                PolVaAbroBarAvordId = PolVaAbroId,
                RizMetreId = RizMetre.ID,
                PolDetailType = PolDetailTypeEnum.بتن_مگر_پی_دیوار_بالی
            };
            _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre1);

            /////////////
            ///درج حمل///
            /////////////
            requestSaveHaml = new SaveHamlDto
            {
                BarAvordUserId = BarAvordId,
                Year = Year,
                NoeFBId = NoeFBId,
                ItemFBShomareh = "120101",
                Shomareh = ++Shomareh,
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
        }
        ////////////
        ///
        if (decimal.Parse(w2.Trim()) != 0)
        {
            decimal dhMinw2 = decimal.Parse(hMinw2);
            if (dhMinw2 <= decimal.Parse("0.5"))
                dhMinw2 += decimal.Parse("0.5");
            decimal rndhMinw = Math.Round(dhMinw2);
            clsAbadDivarBali? varAbadDivarBali2 = _context.AbadDivarBalis.Where(x => x.h == rndhMinw).FirstOrDefault();
            decimal df = varAbadDivarBali2 != null ? varAbadDivarBali2.f : 0;

            RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = Shomareh;
            RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
            RizMetre.Sharh = "بتن مگر پی دیوار بالی 2";
            RizMetre.Tedad = 1;
            RizMetre.Tool = decimal.Parse(w2.Trim()) + decimal.Parse("0.1");
            RizMetre.Arz = Math.Round((((df + dfh) / 2) + d2dahm), 2);
            RizMetre.Ertefa = decimal.Parse("0.1");
            RizMetre.Vazn = null;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "300" + PolNum.ToString("D3") + "30";///بتن مگر دیوار بالی
            RizMetre.ForItem = "";
            RizMetre.UseItem = "";

            decimal dMeghdarJoz2 = 0;
            if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                dMeghdarJoz2 = 0;
            else
                dMeghdarJoz2 += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

            RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz2, 2);

            _context.RizMetreUserses.Add(RizMetre);

            clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre2 = new clsPolVaAbroBarAvordRizMetre
            {
                PolVaAbroBarAvordId = PolVaAbroId,
                RizMetreId = RizMetre.ID,
                PolDetailType = PolDetailTypeEnum.بتن_مگر_پی_دیوار_بالی
            };
            _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre2);

            /////////////
            ///درج حمل///
            /////////////
            requestSaveHaml = new SaveHamlDto
            {
                BarAvordUserId = BarAvordId,
                Year = Year,
                NoeFBId = NoeFBId,
                ItemFBShomareh = "120101",
                Shomareh = ++Shomareh,
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
        }
        ////////////
        ///
        if (decimal.Parse(w3.Trim()) != 0)
        {
            decimal dhMinw3 = decimal.Parse(hMinw3);
            if (dhMinw3 <= decimal.Parse("0.5"))
                dhMinw3 += decimal.Parse("0.5");
            decimal rndhMinw = Math.Round(dhMinw3);
            clsAbadDivarBali? varAbadDivarBali3 = _context.AbadDivarBalis.Where(x => x.h == rndhMinw).FirstOrDefault();
            decimal df = varAbadDivarBali3 != null ? varAbadDivarBali3.f : 0;


            RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = Shomareh;
            RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
            RizMetre.Sharh = "بتن مگر پی دیوار بالی 3";
            RizMetre.Tedad = 1;
            RizMetre.Tool = decimal.Parse(w3.Trim()) + decimal.Parse("0.1");
            RizMetre.Arz = Math.Round((((df + dfh) / 2) + d2dahm), 2);
            RizMetre.Ertefa = decimal.Parse("0.1");
            RizMetre.Vazn = null;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "300" + PolNum.ToString("D3") + "30";///بتن مگر دیوار بالی
            RizMetre.ForItem = "";
            RizMetre.UseItem = "";

            decimal dMeghdarJoz3 = 0;
            if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                dMeghdarJoz3 = 0;
            else
                dMeghdarJoz3 += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

            RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz3, 2);

            _context.RizMetreUserses.Add(RizMetre);

            clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre3 = new clsPolVaAbroBarAvordRizMetre
            {
                PolVaAbroBarAvordId = PolVaAbroId,
                RizMetreId = RizMetre.ID,
                PolDetailType = PolDetailTypeEnum.بتن_مگر_پی_دیوار_بالی
            };
            _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre3);

            /////////////
            ///درج حمل///
            /////////////
            requestSaveHaml = new SaveHamlDto
            {
                BarAvordUserId = BarAvordId,
                Year = Year,
                NoeFBId = NoeFBId,
                ItemFBShomareh = "120101",
                Shomareh = ++Shomareh,
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
        }
        ////////////
        ///
        if (decimal.Parse(w4.Trim()) != 0)
        {

            decimal dhMinw4 = decimal.Parse(hMinw4);
            if (dhMinw4 <= decimal.Parse("0.5"))
                dhMinw4 += decimal.Parse("0.5");
            decimal rndhMinw = Math.Round(dhMinw4);
            clsAbadDivarBali? varAbadDivarBali4 = _context.AbadDivarBalis.Where(x => x.h == rndhMinw).FirstOrDefault();
            decimal df = varAbadDivarBali4 != null ? varAbadDivarBali4.f : 0;

            RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = Shomareh;
            RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
            RizMetre.Sharh = "بتن مگر پی دیوار بالی 4";
            RizMetre.Tedad = 1;
            RizMetre.Tool = decimal.Parse(w4.Trim()) + decimal.Parse("0.1");
            RizMetre.Arz = Math.Round((((df + dfh) / 2) + d2dahm), 2);
            RizMetre.Ertefa = decimal.Parse("0.1");
            RizMetre.Vazn = null;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "300" + PolNum.ToString("D3") + "30";///بتن مگر دیوار بالی
            RizMetre.ForItem = "";
            RizMetre.UseItem = "";

            decimal dMeghdarJoz4 = 0;
            if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                dMeghdarJoz4 = 0;
            else
                dMeghdarJoz4 += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

            RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz4, 2);

            _context.RizMetreUserses.Add(RizMetre);

            clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre4 = new clsPolVaAbroBarAvordRizMetre
            {
                PolVaAbroBarAvordId = PolVaAbroId,
                RizMetreId = RizMetre.ID,
                PolDetailType = PolDetailTypeEnum.بتن_مگر_پی_دیوار_بالی
            };
            _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre4);

            /////////////
            ///درج حمل///
            /////////////
            requestSaveHaml = new SaveHamlDto
            {
                BarAvordUserId = BarAvordId,
                Year = Year,
                NoeFBId = NoeFBId,
                ItemFBShomareh = "120101",
                Shomareh = ++Shomareh,
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
        }
        /////////////////
        ///120104
        //////////////
        varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "120104" && x.NoeFBId == NoeFBId);
        //DtFBUser = clsConvert.ToDataTable(varFBUser);

        guFBId = new Guid();
        //if (DtFBUser.Rows.Count == 0)
        if (varFBUser == null)
        {
            clsFB FBSave = new clsFB();
            FBSave.BarAvordId = BarAvordId;
            FBSave.Shomareh = "120104";
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
        Shomareh = 0;
        if (RizMetreUserLastShomareh != null)
            Shomareh = RizMetreUserLastShomareh.Shomareh + 1;
        else
            Shomareh = 1;


        RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = Shomareh;
        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
        RizMetre.Sharh = "بتن پی کوله";
        RizMetre.Tedad = 2;
        RizMetre.Tool = decimal.Parse(LAbro.Trim());
        RizMetre.Arz = decimal.Parse(f.Trim());
        RizMetre.Ertefa = decimal.Parse(m.Trim());
        RizMetre.Vazn = null;
        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = guFBId;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "32";///بتن پی کوله
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
            PolDetailType = PolDetailTypeEnum.بتن_پی_کوله
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
            ItemFBShomareh = "120104",
            Shomareh = ++Shomareh,
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
        //////////بتن پی پایه میانی
        tedad = int.Parse(TedadDahaneh) - 1;
        if (tedad != 0)
        {
            RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = ++Shomareh;
            RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
            RizMetre.Sharh = "بتن پی پایه میانی";
            RizMetre.Tedad = tedad;
            RizMetre.Tool = decimal.Parse(LAbro.Trim());
            RizMetre.Arz = decimal.Parse(k.Trim());
            RizMetre.Ertefa = decimal.Parse(n.Trim());
            RizMetre.Vazn = null;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "300" + PolNum.ToString("D3") + "33";///بتن پی پایه میانی 
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
                PolDetailType = PolDetailTypeEnum.بتن_پی_پایه_میانی
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
                ItemFBShomareh = "120104",
                Shomareh = ++Shomareh,
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

        }

        if (intNoeBanaii == 1)
        {
            ////////////////بتن دیوار کوله
            RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = ++Shomareh;
            RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
            RizMetre.Sharh = "بتن دیوار کوله";
            RizMetre.Tedad = 2;
            RizMetre.Tool = decimal.Parse(LAbro.Trim());
            RizMetre.Arz = Math.Round(((decimal.Parse(b1.Trim()) + decimal.Parse(b2.Trim())) / 2), 2);
            RizMetre.Ertefa = decimal.Parse(h.Trim()) - decimal.Parse("0.3");
            RizMetre.Vazn = null;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "300" + PolNum.ToString("D3") + "34";///بتن دیوار کوله
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
                PolDetailType = PolDetailTypeEnum.بتن_دیوار_کوله
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
                ItemFBShomareh = "120104",
                Shomareh = ++Shomareh,
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

            ////////////////اضافه بها بتن دیوار کوله
            if (decimal.Parse(h.Trim()) - decimal.Parse("0.3") <= 5)
            {
                string strShomareh120302 = abnieFaniEzafeBahaCommon.BotonDivarKooleEzafeBahaLow5(PolVaAbroId, PolNum, TedadDahaneh, D, intShomareh120302, LAbro, b1, b2, h, guFBId120302);
                intShomareh120302 = long.Parse(strShomareh120302);
            }
            else if (decimal.Parse(h.Trim()) - decimal.Parse("0.3") > 5 && decimal.Parse(h.Trim()) - decimal.Parse("0.3") <= 10)
            {
                strShomareh = abnieFaniEzafeBahaCommon.BotonDivarKooleEzafeBahaHigh5(PolVaAbroId, PolNum, TedadDahaneh, D, intShomareh120302, intShomareh120303, LAbro, b1, b2, h, guFBId120302, guFBId120303);
                intShomareh120302 = long.Parse(strShomareh.Split(",")[0]);
                intShomareh120303 = long.Parse(strShomareh.Split(",")[1]);
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
                    RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                    RizMetre.Sharh = "بتن مخروط پایه میانی (کامل)";
                    RizMetre.Tedad = (int.Parse(TedadDahaneh) - 1) * tedad;
                    RizMetre.Tool = Math.Round(((decimal.Parse(p1.Trim()) * decimal.Parse(p1.Trim())) / 4), 3);
                    RizMetre.Arz = Math.Round(decimal.Parse(Math.PI.ToString()), 3);
                    RizMetre.Ertefa = decimal.Parse(h.Trim()) + x;
                    RizMetre.Vazn = null;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "300" + PolNum.ToString("D3") + "35";///بتن مخروط پایه میانی (کامل)
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
                        PolDetailType = PolDetailTypeEnum.بتن_مخروط_پایه_میانی_کامل
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
                        ItemFBShomareh = "120104",
                        Shomareh = ++Shomareh,
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

                    ////////////
                    if (x != 0)
                    {
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = ++Shomareh;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = "کسر بتن مخروط پایه میانی(بالایی)";
                        RizMetre.Tedad = (int.Parse(TedadDahaneh) - 1) * tedad;
                        RizMetre.Tool = Math.Round(((decimal.Parse(p2.Trim()) * decimal.Parse(p2.Trim())) / 4), 3);
                        RizMetre.Arz = Math.Round((decimal.Parse(Math.PI.ToString())), 3);
                        RizMetre.Ertefa = (-1) * x;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "36";///کسر بتن مخروط پایه میانی(بالایی)
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
                            PolDetailType = PolDetailTypeEnum.کسر_بتن_مخروط_پایه_میانی_بالایی
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
                            ItemFBShomareh = "120104",
                            Shomareh = ++Shomareh,
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
                RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                RizMetre.Sharh = "بتن دیوار پایه میانی";
                RizMetre.Tedad = (int.Parse(TedadDahaneh) - 1);
                RizMetre.Tool = decimal.Parse(LAbro.Trim());
                RizMetre.Arz = (decimal.Parse(p1.Trim()) + decimal.Parse(p2.Trim())) / 2;
                RizMetre.Ertefa = decimal.Parse(h.Trim()) - decimal.Parse("0.3");
                RizMetre.Vazn = null;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId;
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "300" + PolNum.ToString("D3") + "37";///بتن دیوار پایه میانی
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
                    PolDetailType = PolDetailTypeEnum.بتن_دیوار_پایه_میانی
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
                    ItemFBShomareh = "120104",
                    Shomareh = ++Shomareh,
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
        }
        ////////////////////

        varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "120106" && x.NoeFBId == NoeFBId);
        //DtFBUser = clsConvert.ToDataTable(varFBUser);

        guFBId = new Guid();
        //if (DtFBUser.Rows.Count == 0)
        if (varFBUser == null)
        {
            clsFB FBSave = new clsFB();
            FBSave.BarAvordId = BarAvordId;
            FBSave.Shomareh = "120106";
            //FBSave.BahayeVahedZarib = 0;
            FBSave.NoeFBId = NoeFBId;
            _context.FBs.Add(FBSave);
            guFBId = FBSave.ID;
        }
        else
            guFBId = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
        ////////////////

        RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
        Shomareh = 0;
        if (RizMetreUserLastShomareh != null)
            Shomareh = RizMetreUserLastShomareh.Shomareh + 1;
        else
            Shomareh = 1;

        ////////////////بتن شناژ کوله
        RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = Shomareh;
        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
        RizMetre.Sharh = "بتن شناژ کوله";
        RizMetre.Tedad = 2;
        RizMetre.Tool = decimal.Parse(LAbro.Trim());
        RizMetre.Arz = decimal.Parse(b2.Trim());
        RizMetre.Ertefa = decimal.Parse("0.3");
        RizMetre.Vazn = null;
        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = guFBId;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "38";///بتن شناژ کوله
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
            PolDetailType = PolDetailTypeEnum.بتن_شناژ_کوله
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
            ItemFBShomareh = "120106",
            Shomareh = ++Shomareh,
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

        //////////بتن شناژ کوله
        RizMetre = new clsRizMetreUsers();
        RizMetre.Shomareh = ++Shomareh;
        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
        RizMetre.Sharh = "بتن شناژ کوله";
        RizMetre.Tedad = 2;
        RizMetre.Tool = decimal.Parse(LAbro.Trim());
        RizMetre.Arz = decimal.Parse(c1.Trim());
        RizMetre.Ertefa = decimal.Parse(t);
        RizMetre.Vazn = null;
        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
        RizMetre.FBId = guFBId;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "38";///بتن شناژ کوله
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
            PolDetailType = PolDetailTypeEnum.بتن_شناژ_کوله
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
            ItemFBShomareh = "120106",
            Shomareh = ++Shomareh,
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
            intShomareh120302 = long.Parse(strShomareh.Split(",")[0]);
            intShomareh120303 = long.Parse(strShomareh.Split(",")[1]);
        }
        else if (decimal.Parse(h.Trim()) < 5 && decimal.Parse(h.Trim()) + decimal.Parse(t.Trim()) > 5)
        {
            strShomareh = abnieFaniEzafeBahaCommon.BotonShenajKooleEzafeBahaState4(PolVaAbroId, PolNum, TedadDahaneh, D, intShomareh120302, intShomareh120303, LAbro, b1, b2, h, guFBId120302, guFBId120303, c1, t);
            intShomareh120302 = long.Parse(strShomareh.Split(",")[0]);
            intShomareh120303 = long.Parse(strShomareh.Split(",")[1]);
        }

        string strShomareh120310 = abnieFaniEzafeBahaCommon.BotonShenajKooleEzafeBahaState1(PolVaAbroId, PolNum, TedadDahaneh, D, intShomareh120310, LAbro, b1, b2, h, guFBId120310, c1, t);
        intShomareh120310 = long.Parse(strShomareh120310);

        //////////بتن شناژ پایه میانی
        tedad = (int.Parse(TedadDahaneh.Trim())) - 1;
        if (tedad != 0)
        {
            RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = ++Shomareh;
            RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
            RizMetre.Sharh = "بتن شناژ پایه میانی";
            RizMetre.Tedad = tedad;
            RizMetre.Tool = decimal.Parse(LAbro.Trim());
            RizMetre.Arz = decimal.Parse(p2.Trim());
            RizMetre.Ertefa = decimal.Parse("0.3");
            RizMetre.Vazn = null;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "300" + PolNum.ToString("D3") + "39";///بتن شناژ پایه میانی
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
                PolDetailType = PolDetailTypeEnum.بتن_شناژ_پایه_میانی
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
                ItemFBShomareh = "120106",
                Shomareh = ++Shomareh,
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
                RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                RizMetre.Sharh = "بتن شناژ مخروطی پایه میانی";
                RizMetre.Tedad = (int.Parse(TedadDahaneh.Trim()) - 1) * tedad;
                RizMetre.Tool = Math.Round(((decimal.Parse(p2.Trim()) * decimal.Parse(p2.Trim())) / 4), 3);
                RizMetre.Arz = Math.Round(decimal.Parse(Math.PI.ToString()), 3);
                RizMetre.Ertefa = decimal.Parse("0.3");
                RizMetre.Vazn = null;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId;
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "300" + PolNum.ToString("D3") + "40";///بتن شناژ مخروطی پایه میانی
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
                    PolDetailType = PolDetailTypeEnum.بتن_شناژ_مخروطی_پایه_میانی
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
                    ItemFBShomareh = "120106",
                    Shomareh = ++Shomareh,
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
            RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
            RizMetre.Sharh = "بتن دال";
            RizMetre.Tedad = int.Parse(TedadDahaneh.Trim());
            RizMetre.Tool = decimal.Parse(LAbro.Trim());
            RizMetre.Arz = decimal.Parse(D) + 2 * (decimal.Parse(c1.Trim())) - 2 * (decimal.Parse(j.Trim()));
            RizMetre.Ertefa = decimal.Parse(t.Trim());
            RizMetre.Vazn = null;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "300" + PolNum.ToString("D3") + "41";///بتن دال
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
                PolDetailType = PolDetailTypeEnum.بتن_دال
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
                ItemFBShomareh = "120106",
                Shomareh = ++Shomareh,
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
            varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "130101" && x.NoeFBId == NoeFBId);
            //DtFBUser = clsConvert.ToDataTable(varFBUser);

            guFBId = new Guid();
            //if (DtFBUser.Rows.Count == 0)
            if (varFBUser == null)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "130101";
                //FBSave.BahayeVahedZarib = 0;
                FBSave.NoeFBId = NoeFBId;
                _context.FBs.Add(FBSave);

                guFBId = FBSave.ID;
            }
            else
                guFBId = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
            ////////////////


            RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            Shomareh = 0;
            if (RizMetreUserLastShomareh != null)
                Shomareh = RizMetreUserLastShomareh.Shomareh + 1;
            else
                Shomareh = 1;


            //////////////////بتن دال پیش ساخته

            RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = Shomareh;
            RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
            RizMetre.Sharh = "بتن دال پیش ساخته";
            RizMetre.Tedad = int.Parse(TedadDahaneh.Trim());
            RizMetre.Tool = decimal.Parse(LAbro.Trim());
            RizMetre.Arz = decimal.Parse(D) + 2 * (decimal.Parse(c1.Trim())) - 2 * (decimal.Parse(j.Trim()));
            RizMetre.Ertefa = decimal.Parse(t.Trim());
            RizMetre.Vazn = null;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "300" + PolNum.ToString("D3") + "51";///بتن دال پیش ساخته
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
                PolDetailType = PolDetailTypeEnum.بتن_دال_پیش_ساخته
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
                ItemFBShomareh = "130101",
                Shomareh = ++Shomareh,
                MeghdarJoz = dMeghdarJoz,
                LevelNumber = 1
            };

            lstHamlIds =await HamlCommon.SaveHaml(requestSaveHaml, context);
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

            ///////////////
            varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "130401" && x.NoeFBId == NoeFBId);
            //DtFBUser = clsConvert.ToDataTable(varFBUser);

            guFBId = new Guid();
            //if (DtFBUser.Rows.Count == 0)
            if (varFBUser==null)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BarAvordId;
                FBSave.Shomareh = "130401";
                //FBSave.BahayeVahedZarib = 0;
                FBSave.NoeFBId = NoeFBId;
                _context.FBs.Add(FBSave);

                guFBId = FBSave.ID;
            }
            else
                guFBId = varFBUser.ID;
            ////////////////


            RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            Shomareh = 0;
            if (RizMetreUserLastShomareh != null)
                Shomareh = RizMetreUserLastShomareh.Shomareh + 1;
            else
                Shomareh = 1;

            RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = Shomareh;
            RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
            RizMetre.Sharh = "بتن دال پیش ساخته";
            RizMetre.Tedad = int.Parse(TedadDahaneh.Trim());
            RizMetre.Tool = decimal.Parse(LAbro.Trim());
            RizMetre.Arz = decimal.Parse(D) + 2 * (decimal.Parse(c1.Trim())) - 2 * (decimal.Parse(j.Trim()));
            RizMetre.Ertefa = decimal.Parse(t.Trim());
            RizMetre.Vazn = null;
            RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
            RizMetre.FBId = guFBId;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "300" + PolNum.ToString("D3") + "51";///بتن دال پیش ساخته
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
                PolDetailType = PolDetailTypeEnum.بتن_دال_پیش_ساخته
            };
            _context.PolVaAbroBarAvordRizMetres.Add(polVaAbroBarAvordRizMetre);
            //_context.SaveChanges();
            //RizMetre.Save();
        }
        return true;
    }

}
