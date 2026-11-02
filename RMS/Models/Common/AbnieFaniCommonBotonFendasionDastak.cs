using RMS.Controllers.Operation.Common;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Entity;
using System.Data;
using static RMS.Models.Common.AbnieFaniEnum;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Common;

public class AbnieFaniCommonBotonFendasionDastak(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public async Task<bool> BotonFendasionDastak(Guid PolVaAbroId, NoeFehrestBaha NoeFBId, int PolNum, string D, string TedadDahaneh, Guid BarAvordId,
                    string h, string t, string hMinw1, string hMinw2, string hMinw3, string hMinw4,
                    string w1, string w2, string w3, string w4, int intNoeBanaii, int Year)
    {

        decimal dh = decimal.Parse(h);
        decimal dht = decimal.Parse(h) + decimal.Parse(t);
        if (dht <= decimal.Parse("0.5"))
            dht += decimal.Parse("0.5");


        ///////////////
        clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "120104" && x.NoeFBId == NoeFBId);
        //DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

        Guid guFBId120104 = new Guid();
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
            guFBId120104 = FBSave.ID;
        }
        else
            guFBId120104 = varFBUser.ID;// Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
        ////////////////
        var RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId120104).OrderByDescending(x => x.Shomareh).FirstOrDefault();
        long Shomareh120104 = 0;
        if (RizMetreUserLastShomareh != null)
            Shomareh120104 = RizMetreUserLastShomareh.Shomareh + 1;
        else
            Shomareh120104 = 1;

        ///////////////
        varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "120302" && x.NoeFBId == NoeFBId);
        //DtFBUser = clsConvert.ToDataTable(varFBUser);

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
        RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId120302).OrderByDescending(x => x.Shomareh).FirstOrDefault();
        long Shomareh120302 = 0;
        if (RizMetreUserLastShomareh != null)
            Shomareh120302 = RizMetreUserLastShomareh.Shomareh + 1;
        else
            Shomareh120302 = 1;
        ///////////////
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
            guFBId120303 = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
        ////////////////
        RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId120303).OrderByDescending(x => x.Shomareh).FirstOrDefault();
        long Shomareh120303 = 0;
        if (RizMetreUserLastShomareh != null)
            Shomareh120303 = RizMetreUserLastShomareh.Shomareh + 1;
        else
            Shomareh120303 = 1;

        decimal rndh = Math.Round(decimal.Parse(h));
        clsAbadDivarBali? varAbadDivarBali = _context.AbadDivarBalis.Where(x => x.h == rndh).FirstOrDefault();
        decimal dfStart = varAbadDivarBali != null ? varAbadDivarBali.f : 0;
        decimal dmStart = varAbadDivarBali != null ? varAbadDivarBali.m : 0;

        decimal dxStart = varAbadDivarBali != null ? varAbadDivarBali.x : 0;


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
            clsAbadDivarBali? varAbadDivarBali1 = _context.AbadDivarBalis.Where(x => x.h == rndhMinw).FirstOrDefault();


            if (hMinw > 0)
            {
                decimal dxEnd = varAbadDivarBali1 != null ? varAbadDivarBali1.x : 0;

                decimal dfEnd = varAbadDivarBali1 != null ? varAbadDivarBali1.f : 0;
                decimal dmEnd = varAbadDivarBali1 != null ? varAbadDivarBali1.m : 0;

                //decimal hEnd = Math.Round((hMinw / 6 + dx + decimal.Parse("0.35")), 2);
                //decimal hStart = Math.Round((decimal.Parse(h) / 6 + dx + decimal.Parse("0.35")), 2);


                clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = Shomareh120104++;
                RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                RizMetre.Sharh = " بتن پی دیوار بالی  " + i;
                RizMetre.Tedad = 1;
                RizMetre.Tool = dw;
                RizMetre.Arz = Math.Round(((dfStart + dfEnd) / 2), 3); //res; //Math.Round(((df + dfh) / 2), 2);
                RizMetre.Ertefa = dmStart; //hMinw;
                RizMetre.Vazn = null;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId120104;
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "500" + PolNum.ToString("D3") + "30";///" بتن پی دیوار بالی ";
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
                    PolDetailType = PolDetailTypeEnum.بتن_پی_دیوار_بالی
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
                    ItemFBShomareh = "120104",
                    Shomareh = Shomareh120104++,
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


                /////////////////////////
                //بتن شناژ دیوار بالی////
                /////////////////////////
                RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = Shomareh120104++;
                RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                RizMetre.Sharh = " بتن شناژ دیوار بالی  " + i;
                RizMetre.Tedad = 1;
                RizMetre.Tool = dw;
                RizMetre.Arz = decimal.Parse("0.35");
                RizMetre.Ertefa = decimal.Parse("0.2");
                RizMetre.Vazn = null;
                RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                RizMetre.FBId = guFBId120104;
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "500" + PolNum.ToString("D3") + "32";///" بتن شناژ دیوار بالی ";
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
                    PolDetailType = PolDetailTypeEnum.بتن_شناژ_دیوار_بالی
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
                    Shomareh = Shomareh120104++,
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

                //_context.SaveChanges();
                //RizMetre.Save();

                if (intNoeBanaii == 1)
                {
                    RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh120104++;
                    RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                    RizMetre.Sharh = " بتن دیوار بالی  " + i;
                    RizMetre.Tedad = 1;
                    RizMetre.Tool = dw;
                    RizMetre.Arz = Math.Round(((dh + hMinw) / 12 + (dxStart + dxEnd) / 2 + decimal.Parse("0.35")), 3);
                    RizMetre.Ertefa = Math.Round(((dh + hMinw) / 2), 3);
                    RizMetre.Vazn = null;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId120104;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "500" + PolNum.ToString("D3") + "30";///" بتن دیوار بالی ";
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
                        PolDetailType = PolDetailTypeEnum.بتن_دیوار_بالی
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
                        Shomareh = Shomareh120104++,
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


                    if (dh <= 5)
                    {
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh120302++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " اضافه بها بتن ریزی (دیوار بالی " + i + " ) تا ارتفاع 5 متر ";
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = dw;
                        RizMetre.Arz = Math.Round(((dh + hMinw) / 12 + (dxStart + dxEnd) / 2 + decimal.Parse("0.35")), 3); //res;
                        RizMetre.Ertefa = Math.Round(((dh + hMinw) / 2), 3);// hMinw;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId120302;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";

                        decimal dMeghdarJoz1 = 0;
                        if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                            dMeghdarJoz1 = 0;
                        else
                            dMeghdarJoz1 += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                            (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                        RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz1, 3);

                        _context.RizMetreUserses.Add(RizMetre);

                        polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                        {
                            PolVaAbroBarAvordId = PolVaAbroId,
                            RizMetreId = RizMetre.ID,
                            PolDetailType = PolDetailTypeEnum.اضافه_بها_بتن_ریزی_دیوار_بالی_تا_ارتفاع_5_متر
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
                            ItemFBShomareh = "120302",
                            Shomareh = Shomareh120302++,
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
                    else if (hMinw <= 5 && dh > 5)
                    {
                        ///اضافه بها
                        ///120302
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh120302++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " اضافه بها بتن ریزی (دیوار بالی " + i + " ) تا ارتفاع 5 متر ";
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = Math.Round((((dh - 5) / (dh - hMinw)) * dw), 3); //dw;
                        RizMetre.Arz = Math.Round(((dh / 6) + (dxStart / 2) + decimal.Parse("0.65083")), 3); // Math.Round(((((2 * hMinw) - 5) / 6) + dxEnd + decimal.Parse("0.35")), 2);
                        RizMetre.Ertefa = 5;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId120302;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";

                        decimal dMeghdarJoz1 = 0;
                        if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                            dMeghdarJoz1 = 0;
                        else
                            dMeghdarJoz1 += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                            (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                        RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz1, 3);

                        _context.RizMetreUserses.Add(RizMetre);

                        polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                        {
                            PolVaAbroBarAvordId = PolVaAbroId,
                            RizMetreId = RizMetre.ID,
                            PolDetailType = PolDetailTypeEnum.اضافه_بها_بتن_ریزی_دیوار_بالی_تا_ارتفاع_5_متر
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
                            ItemFBShomareh = "120302",
                            Shomareh = Shomareh120302++,
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


                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh120302++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " اضافه بها بتن ریزی (دیوار بالی " + i + " ) تا ارتفاع 5 متر ";
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = Math.Round((dw - (((dh - 5) / (dh - hMinw)) * dw)), 3);
                        RizMetre.Arz = Math.Round(((hMinw / 12) + (dxEnd / 2) + decimal.Parse("1.0675")), 3); // Math.Round(((((2 * hMinw) - 5) / 6) + dxEnd + decimal.Parse("0.35")), 2);
                        RizMetre.Ertefa = Math.Round((((5 + hMinw) / 2)), 3);
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId120302;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";

                        dMeghdarJoz1 = 0;
                        if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                            dMeghdarJoz1 = 0;
                        else
                            dMeghdarJoz1 += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                            (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                        RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz1, 3);

                        _context.RizMetreUserses.Add(RizMetre);

                        polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                        {
                            PolVaAbroBarAvordId = PolVaAbroId,
                            RizMetreId = RizMetre.ID,
                            PolDetailType = PolDetailTypeEnum.اضافه_بها_بتن_ریزی_دیوار_بالی_تا_ارتفاع_5_متر
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
                            ItemFBShomareh = "120302",
                            Shomareh = Shomareh120302++,
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

                        //120303
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh120303++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " اضافه بها بتن ریزی (دیوار بالی " + i + " ) برای ارتفاع از 5 تا 10  متر ";
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = Math.Round((((dh - 5) / (dh - hMinw)) * dw), 3);
                        RizMetre.Arz = Math.Round((((dh) / 12) + (dxStart / 2) + decimal.Parse("0.23333")), 3);// Math.Round((((hMinw - 5) / 6) + dxEnd + decimal.Parse("0.35")), 2);
                        RizMetre.Ertefa = Math.Round(((dh - 5) / 2), 3);// hMinw - 5;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId120303;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";

                        decimal dMeghdarJoz2 = 0;
                        if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                            dMeghdarJoz2 = 0;
                        else
                            dMeghdarJoz2 += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                            (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                        RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz2, 3);

                        _context.RizMetreUserses.Add(RizMetre);

                        polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                        {
                            PolVaAbroBarAvordId = PolVaAbroId,
                            RizMetreId = RizMetre.ID,
                            PolDetailType = PolDetailTypeEnum.اضافه_بها_بتن_ریزی_دیوار_بالی_برای_ارتفاع_از_5_تا_10_متر
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
                            ItemFBShomareh = "120303",
                            Shomareh = Shomareh120303++,
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

                    else if (hMinw > 5 && dh > 5)
                    {

                        ///اضافه بها
                        ///120302
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh120302++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " اضافه بها بتن ریزی (دیوار بالی " + i + " ) تا ارتفاع 5 متر ";
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = dw;
                        RizMetre.Arz = Math.Round((((dh + hMinw) / 6) + ((dxStart + dxEnd) / 2) - decimal.Parse("0.48333")), 3);
                        RizMetre.Ertefa = 5;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId120302;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";

                        decimal dMeghdarJoz1 = 0;
                        if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                            dMeghdarJoz1 = 0;
                        else
                            dMeghdarJoz1 += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                            (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                        RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz1, 3);

                        _context.RizMetreUserses.Add(RizMetre);

                        polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                        {
                            PolVaAbroBarAvordId = PolVaAbroId,
                            RizMetreId = RizMetre.ID,
                            PolDetailType = PolDetailTypeEnum.اضافه_بها_بتن_ریزی_دیوار_بالی_تا_ارتفاع_5_متر
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
                            ItemFBShomareh = "120302",
                            Shomareh = Shomareh120302++,
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

                        //120303
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh120303++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = " اضافه بها بتن ریزی (دیوار بالی " + i + " ) برای ارتفاع از 5 تا 10  متر ";
                        RizMetre.Tedad = 1;
                        RizMetre.Tool = dw;
                        RizMetre.Arz = Math.Round((((dh + hMinw) / 12) + ((dxStart + dxEnd) / 2) - decimal.Parse("0.48333")), 3);
                        RizMetre.Ertefa = Math.Round((((dh + hMinw) / 2) - 5), 3);
                        RizMetre.Vazn = null;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId120303;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";

                        decimal dMeghdarJoz2 = 0;
                        if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                            dMeghdarJoz2 = 0;
                        else
                            dMeghdarJoz2 += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                            (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                        RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz2, 3);

                        _context.RizMetreUserses.Add(RizMetre);

                        polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                        {
                            PolVaAbroBarAvordId = PolVaAbroId,
                            RizMetreId = RizMetre.ID,
                            PolDetailType = PolDetailTypeEnum.اضافه_بها_بتن_ریزی_دیوار_بالی_برای_ارتفاع_از_5_تا_10_متر
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
                            ItemFBShomareh = "120303",
                            Shomareh = Shomareh120303++,
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
                }

            }
        }
        return true;
    }
}
