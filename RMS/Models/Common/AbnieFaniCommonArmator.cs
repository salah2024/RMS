using RMS.Controllers.Operation.Common;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Entity;
using System.Data;
using static RMS.Models.Common.AbnieFaniEnum;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Common;

public class AbnieFaniCommonArmator(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public async Task<bool> Armator(Guid PolVaAbroId, int PolNum, string D, string TedadDahaneh, Guid BarAvordId, NoeFehrestBaha NoeFBId, string LAbro, string Hs,int Year)
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
        //DataTable DtAbroDaliDetails = clsConvert.ToDataTable(varAbroDaliDetails);

        string[,] GhotrVazn ={{"8","10","12","14","16","18","20","22","25","28","32"},
                              {"0.345","0.617","0.888","1.21","1.58","2","2.47","2.98"
                                  ,"3.85","4.83","6.31"}};

        clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "090204" && x.NoeFBId == NoeFBId);
        //DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

        Guid guFBId090204 = new Guid();
        if (varFBUser == null)
        //if (DtFBUser.Rows.Count == 0)
        {
            clsFB FBSave = new clsFB();
            FBSave.BarAvordId = BarAvordId;
            FBSave.Shomareh = "090204";
            //FBSave.BahayeVahedZarib = 0;
            FBSave.NoeFBId = NoeFBId;
            _context.FBs.Add(FBSave);
            //_context.SaveChanges();
            guFBId090204 = FBSave.ID;
        }
        else
            guFBId090204 = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
        ////////////////

        var RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId090204).OrderByDescending(x => x.Shomareh).FirstOrDefault();
        long Shomareh090204 = 0;
        if (RizMetreUserLastShomareh != null)
            Shomareh090204 = RizMetreUserLastShomareh.Shomareh + 1;
        else
            Shomareh090204 = 1;

        varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "090205" && x.NoeFBId == NoeFBId);
        //DtFBUser = clsConvert.ToDataTable(varFBUser);

        Guid guFBId090205 = new Guid();
        //if (DtFBUser.Rows.Count == 0)
        if (varFBUser == null)
        {
            clsFB FBSave = new clsFB();
            FBSave.BarAvordId = BarAvordId;
            FBSave.Shomareh = "090205";
            //FBSave.BahayeVahedZarib = 0;
            FBSave.NoeFBId = NoeFBId;
            _context.FBs.Add(FBSave);
            //_context.SaveChanges();
            guFBId090205 = FBSave.ID;
        }
        else
            guFBId090205 = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
        ////////////////

        RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId090205).OrderByDescending(x => x.Shomareh).FirstOrDefault();
        long Shomareh090205 = 0;
        if (RizMetreUserLastShomareh != null)
            Shomareh090205 = RizMetreUserLastShomareh.Shomareh + 1;
        else
            Shomareh090205 = 1;

        ///////////////
        varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == "090206" && x.NoeFBId == NoeFBId);
        //DtFBUser = clsConvert.ToDataTable(varFBUser);

        Guid guFBId090206 = new Guid();
        //if (DtFBUser.Rows.Count == 0)
        if (varFBUser == null)
        {
            clsFB FBSave = new clsFB();
            FBSave.BarAvordId = BarAvordId;
            FBSave.Shomareh = "090206";
            //FBSave.BahayeVahedZarib = 0;
            FBSave.NoeFBId = NoeFBId;
            _context.FBs.Add(FBSave);
            //_context.SaveChanges();
            guFBId090206 = FBSave.ID;
        }
        else
            guFBId090206 = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
        ////////////////

        RizMetreUserLastShomareh = _context.RizMetreUserses.Where(x => x.FBId == guFBId090206).OrderByDescending(x => x.Shomareh).FirstOrDefault();
        long Shomareh090206 = 0;
        if (RizMetreUserLastShomareh != null)
            Shomareh090206 = RizMetreUserLastShomareh.Shomareh + 1;
        else
            Shomareh090206 = 1;


        //for (int i = 0; i < DtAbroDaliDetails.Rows.Count; i++)
        foreach (var AbroDaliDetails in varAbroDaliDetails)
        {
            int intGhotr = AbroDaliDetails.Ghotr;// int.Parse(DtAbroDaliDetails.Rows[i]["Ghotr"].ToString().Trim());
            decimal dFasele = AbroDaliDetails.Fasele;
            decimal dTedad = AbroDaliDetails.Tedad;// decimal.Parse(DtAbroDaliDetails.Rows[i]["Tedad"].ToString().Trim());
            int intPos = AbroDaliDetails.Pos;// int.Parse(DtAbroDaliDetails.Rows[i]["Pos"].ToString().Trim());
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
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = "pos " + intPos.ToString() + " =  φ " + intGhotr + " @ " + dFasele.ToString("0")+" cm ";
                        RizMetre.Tedad = dTedad;
                        RizMetre.Tool = Math.Round(dLAbroPrim, 3);
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = null;
                        RizMetre.Vazn = dVaz;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId090204;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "50";///آرماتور
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";

                        decimal dMeghdarJoz = 0;
                        if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                            dMeghdarJoz = 0;
                        else
                            dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                            (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                        RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz,3);

                        _context.RizMetreUserses.Add(RizMetre);

                        clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                        {
                            PolVaAbroBarAvordId = PolVaAbroId,
                            RizMetreId = RizMetre.ID,
                            PolDetailType = PolDetailTypeEnum.pos
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
                            ItemFBShomareh = "090204",
                            Shomareh = Shomareh090204++,
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

                        //_context.SaveChanges();
                    }
                    else if (dLAbro >= 24 && dLAbro < 36)
                    {
                        decimal dLAbroPrim = dLAbro + 2 * decimal.Parse("0.056") * intGhotr;

                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh090204++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = "pos " + intPos.ToString() + " =  φ " + intGhotr + " @ " + dFasele.ToString("0") + " cm ";
                        RizMetre.Tedad = dTedad;
                        RizMetre.Tool = Math.Round(dLAbroPrim, 3);
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = null;
                        RizMetre.Vazn = dVaz;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId090204;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "50";///آرماتور
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";

                        decimal dMeghdarJoz = 0;
                        if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                            dMeghdarJoz = 0;
                        else
                            dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                            (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                        RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz,3);

                        _context.RizMetreUserses.Add(RizMetre);

                        clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                        {
                            PolVaAbroBarAvordId = PolVaAbroId,
                            RizMetreId = RizMetre.ID,
                            PolDetailType = PolDetailTypeEnum.pos
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
                            ItemFBShomareh = "090204",
                            Shomareh = Shomareh090204++,
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

                    }
                    else if (dLAbro >= 36 && dLAbro < 48)
                    {
                        decimal dLAbroPrim = dLAbro + 3 * decimal.Parse("0.056") * intGhotr;
                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh090204++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = "pos " + intPos.ToString() + " =  φ " + intGhotr + " @ " + dFasele.ToString("0") + " cm ";
                        RizMetre.Tedad = dTedad;
                        RizMetre.Tool = Math.Round(dLAbroPrim, 3);
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = null;
                        RizMetre.Vazn = dVaz;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId090204;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "50";///آرماتور
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";

                        decimal dMeghdarJoz = 0;
                        if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                            dMeghdarJoz = 0;
                        else
                            dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                            (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                        RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz,3);

                        _context.RizMetreUserses.Add(RizMetre);

                        clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                        {
                            PolVaAbroBarAvordId = PolVaAbroId,
                            RizMetreId = RizMetre.ID,
                            PolDetailType = PolDetailTypeEnum.pos
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
                            ItemFBShomareh = "090204",
                            Shomareh = Shomareh090204++,
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
                    }
                    else
                    {
                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh090204++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = "pos " + intPos.ToString() + " =  φ " + intGhotr + " @ " + dFasele.ToString("0") + " cm ";
                        RizMetre.Tedad = dTedad;
                        RizMetre.Tool = Math.Round(dLAbro, 3);
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = null;
                        RizMetre.Vazn = dVaz;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId090204;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "50";///آرماتور
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";

                        decimal dMeghdarJoz = 0;
                        if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                            dMeghdarJoz = 0;
                        else
                            dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                            (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                        RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz,3);

                        _context.RizMetreUserses.Add(RizMetre);

                        clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                        {
                            PolVaAbroBarAvordId = PolVaAbroId,
                            RizMetreId = RizMetre.ID,
                            PolDetailType = PolDetailTypeEnum.pos
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
                            ItemFBShomareh = "090204",
                            Shomareh = Shomareh090204++,
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
                    }
                }
                else if (intPos == 1 || intPos == 2 || intPos == 4 || intPos == 7
                         || intPos == 9 || intPos == 10)
                {
                    dFasele = AbroDaliDetails.Fasele;// decimal.Parse(DtAbroDaliDetails.Rows[i]["Fasele"].ToString().Trim());
                    decimal dTool = AbroDaliDetails.Tool;// decimal.Parse(DtAbroDaliDetails.Rows[i]["Tool"].ToString().Trim());
                    decimal dTedadNew = Math.Round((decimal.Parse(LAbro.Trim()) - decimal.Parse("0.1")) / (dFasele / 100) + 1);

                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh090204++;
                    RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                    RizMetre.Sharh = "pos " + intPos.ToString() + " =  φ " + intGhotr + " @ " + dFasele.ToString("0") + " cm ";
                    RizMetre.Tedad = dTedadNew;
                    RizMetre.Tool = dTool;
                    RizMetre.Arz = null;
                    RizMetre.Ertefa = null;
                    RizMetre.Vazn = dVaz;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId090204;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "300" + PolNum.ToString("D3") + "50";///آرماتور
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";

                    decimal dMeghdarJoz = 0;
                    if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                        dMeghdarJoz = 0;
                    else
                        dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                        (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                    RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz,3);

                    _context.RizMetreUserses.Add(RizMetre);

                    clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                    {
                        PolVaAbroBarAvordId = PolVaAbroId,
                        RizMetreId = RizMetre.ID,
                        PolDetailType = PolDetailTypeEnum.pos
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
                        ItemFBShomareh = "090204",
                        Shomareh = Shomareh090204++,
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
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = "pos " + intPos.ToString() + " =  φ " + intGhotr + " @ " + dFasele.ToString("0") + " cm ";
                        RizMetre.Tedad = dTedad;
                        RizMetre.Tool = Math.Round(dLAbroPrim,3);
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = null;
                        RizMetre.Vazn = dVaz;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId090205;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "50";///آرماتور
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";

                        decimal dMeghdarJoz = 0;
                        if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                            dMeghdarJoz = 0;
                        else
                            dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                            (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                        RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz,3);

                        _context.RizMetreUserses.Add(RizMetre);

                        clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                        {
                            PolVaAbroBarAvordId = PolVaAbroId,
                            RizMetreId = RizMetre.ID,
                            PolDetailType = PolDetailTypeEnum.pos
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
                            ItemFBShomareh = "090205",
                            Shomareh = Shomareh090205++,
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
                    }
                    else if (dLAbro >= 24 && dLAbro < 36)
                    {
                        decimal dLAbroPrim = dLAbro + 2 * decimal.Parse("0.056") * intGhotr;

                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh090205++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = "pos " + intPos.ToString() + " =  φ " + intGhotr + " @ " + dFasele.ToString("0") + " cm ";
                        RizMetre.Tedad = dTedad;
                        RizMetre.Tool = Math.Round(dLAbroPrim, 3);
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = null;
                        RizMetre.Vazn = dVaz;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId090205;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "50";///آرماتور
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        decimal dMeghdarJoz = 0;
                        if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                            dMeghdarJoz = 0;
                        else
                            dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                            (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                        RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz,3);

                        _context.RizMetreUserses.Add(RizMetre);

                        clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                        {
                            PolVaAbroBarAvordId = PolVaAbroId,
                            RizMetreId = RizMetre.ID,
                            PolDetailType = PolDetailTypeEnum.pos
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
                            ItemFBShomareh = "090205",
                            Shomareh = Shomareh090205++,
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
                    }
                    else if (dLAbro >= 36 && dLAbro < 48)
                    {
                        decimal dLAbroPrim = dLAbro + 3 * decimal.Parse("0.056") * intGhotr;
                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh090205++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = "pos " + intPos.ToString() + " =  φ " + intGhotr + " @ " + dFasele.ToString("0") + " cm ";
                        RizMetre.Tedad = dTedad;
                        RizMetre.Tool = Math.Round(dLAbroPrim, 3);
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = null;
                        RizMetre.Vazn = dVaz;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId090205;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "50";///آرماتور
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";

                        decimal dMeghdarJoz = 0;
                        if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                            dMeghdarJoz = 0;
                        else
                            dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                            (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                        RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz,3);

                        _context.RizMetreUserses.Add(RizMetre);

                        clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                        {
                            PolVaAbroBarAvordId = PolVaAbroId,
                            RizMetreId = RizMetre.ID,
                            PolDetailType = PolDetailTypeEnum.pos
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
                            ItemFBShomareh = "090205",
                            Shomareh = Shomareh090205++,
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
                    }
                    else
                    {
                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh090205++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = "pos " + intPos.ToString() + " =  φ " + intGhotr + " @ " + dFasele.ToString("0") + " cm ";
                        RizMetre.Tedad = dTedad;
                        RizMetre.Tool = Math.Round(dLAbro, 3);
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = null;
                        RizMetre.Vazn = dVaz;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId090205;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "50";///آرماتور
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        decimal dMeghdarJoz = 0;
                        if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                            dMeghdarJoz = 0;
                        else
                            dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                            (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                        RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz,3);

                        _context.RizMetreUserses.Add(RizMetre);

                        clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                        {
                            PolVaAbroBarAvordId = PolVaAbroId,
                            RizMetreId = RizMetre.ID,
                            PolDetailType = PolDetailTypeEnum.pos
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
                            ItemFBShomareh = "090205",
                            Shomareh = Shomareh090205++,
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
                    }
                }
                else if (intPos == 1 || intPos == 2 || intPos == 4 || intPos == 7
                         || intPos == 9 || intPos == 10)
                {
                    dFasele = AbroDaliDetails.Fasele;// decimal.Parse(DtAbroDaliDetails.Rows[i]["Fasele"].ToString().Trim());
                    decimal dTool = AbroDaliDetails.Tool;// decimal.Parse(DtAbroDaliDetails.Rows[i]["Tool"].ToString().Trim());
                    decimal dTedadNew = Math.Round((decimal.Parse(LAbro.Trim()) - decimal.Parse("0.1")) / (dFasele / 100) + 1);

                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh090205++;
                    RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                    RizMetre.Sharh = "pos " + intPos.ToString() + " =  φ " + intGhotr + " @ " + dFasele.ToString("0") + " cm ";
                    RizMetre.Tedad = dTedadNew;
                    RizMetre.Tool = dTool;
                    RizMetre.Arz = null;
                    RizMetre.Ertefa = null;
                    RizMetre.Vazn = dVaz;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId090205;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "300" + PolNum.ToString("D3") + "50";///آرماتور
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";

                    decimal dMeghdarJoz = 0;
                    if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                        dMeghdarJoz = 0;
                    else
                        dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                        (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                    RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz,3);

                    _context.RizMetreUserses.Add(RizMetre);

                    clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                    {
                        PolVaAbroBarAvordId = PolVaAbroId,
                        RizMetreId = RizMetre.ID,
                        PolDetailType = PolDetailTypeEnum.pos
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
                        ItemFBShomareh = "090205",
                        Shomareh = Shomareh090205++,
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
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = "pos " + intPos.ToString() + " =  φ " + intGhotr + " @ " + dFasele.ToString("0") + " cm ";
                        RizMetre.Tedad = dTedad;
                        RizMetre.Tool = Math.Round(dLAbroPrim, 3);
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = null;
                        RizMetre.Vazn = dVaz;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId090206;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "50";///آرماتور
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";

                        decimal dMeghdarJoz = 0;
                        if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                            dMeghdarJoz = 0;
                        else
                            dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                            (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                        RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz,3);

                        _context.RizMetreUserses.Add(RizMetre);

                        clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                        {
                            PolVaAbroBarAvordId = PolVaAbroId,
                            RizMetreId = RizMetre.ID,
                            PolDetailType = PolDetailTypeEnum.pos
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
                            ItemFBShomareh = "090206",
                            Shomareh = Shomareh090206++,
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
                    }
                    else if (dLAbro >= 24 && dLAbro < 36)
                    {
                        decimal dLAbroPrim = dLAbro + 2 * dZaribLAbroPrim * intGhotr;

                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh090206++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = "pos " + intPos.ToString() + " =  φ " + intGhotr + " @ " + dFasele.ToString("0") + " cm ";
                        RizMetre.Tedad = dTedad;
                        RizMetre.Tool = Math.Round(dLAbroPrim, 3);
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = null;
                        RizMetre.Vazn = dVaz;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId090206;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "50";///آرماتور
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";

                        decimal dMeghdarJoz = 0;
                        if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                            dMeghdarJoz = 0;
                        else
                            dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                            (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                        RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz,3);

                        _context.RizMetreUserses.Add(RizMetre);

                        clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                        {
                            PolVaAbroBarAvordId = PolVaAbroId,
                            RizMetreId = RizMetre.ID,
                            PolDetailType = PolDetailTypeEnum.pos
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
                            ItemFBShomareh = "090206",
                            Shomareh = Shomareh090206++,
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
                    }
                    else if (dLAbro >= 36 && dLAbro < 48)
                    {
                        decimal dLAbroPrim = dLAbro + 3 * dZaribLAbroPrim * intGhotr;

                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh090206++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = "pos " + intPos.ToString() + " =  φ " + intGhotr + " @ " + dFasele.ToString("0") + " cm ";
                        RizMetre.Tedad = dTedad;
                        RizMetre.Tool = Math.Round(dLAbroPrim, 3);
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = null;
                        RizMetre.Vazn = dVaz;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId090206;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "50";///آرماتور
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";

                        decimal dMeghdarJoz = 0;
                        if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                            dMeghdarJoz = 0;
                        else
                            dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                            (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                        RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz,3);

                        _context.RizMetreUserses.Add(RizMetre);

                        clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                        {
                            PolVaAbroBarAvordId = PolVaAbroId,
                            RizMetreId = RizMetre.ID,
                            PolDetailType = PolDetailTypeEnum.pos
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
                            ItemFBShomareh = "090206",
                            Shomareh = Shomareh090206++,
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
                    }
                    else
                    {
                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh090206++;
                        RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                        RizMetre.Sharh = "pos " + intPos.ToString();
                        RizMetre.Tedad = dTedad;
                        RizMetre.Tool = Math.Round(dLAbro, 3);
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = null;
                        RizMetre.Vazn = dVaz;
                        RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                        RizMetre.FBId = guFBId090206;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "300" + PolNum.ToString("D3") + "50";///آرماتور
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";

                        decimal dMeghdarJoz = 0;
                        if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                            dMeghdarJoz = 0;
                        else
                            dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                            (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                        RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz,3);

                        _context.RizMetreUserses.Add(RizMetre);

                        clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                        {
                            PolVaAbroBarAvordId = PolVaAbroId,
                            RizMetreId = RizMetre.ID,
                            PolDetailType = PolDetailTypeEnum.pos
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
                            ItemFBShomareh = "090206",
                            Shomareh = Shomareh090206++,
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
                    }
                }
                else if (intPos == 1 || intPos == 2 || intPos == 4 || intPos == 7
                         || intPos == 9 || intPos == 10)
                {
                    dFasele = AbroDaliDetails.Fasele;// decimal.Parse(DtAbroDaliDetails.Rows[i]["Fasele"].ToString().Trim());
                    decimal dTool = AbroDaliDetails.Tool;// decimal.Parse(DtAbroDaliDetails.Rows[i]["Tool"].ToString().Trim());
                    decimal dTedadNew = Math.Round((decimal.Parse(LAbro.Trim()) - decimal.Parse("0.1")) / (dFasele / 100) + 1);

                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh090206++;
                    RizMetre.ShomarehNew = RizMetre.Shomareh.ToString();
                    RizMetre.Sharh = "pos " + intPos.ToString();
                    RizMetre.Tedad = dTedadNew;
                    RizMetre.Tool = dTool;
                    RizMetre.Arz = null;
                    RizMetre.Ertefa = null;
                    RizMetre.Vazn = dVaz;
                    RizMetre.Des = "پل شماره " + PolNum + " ، " + TedadDahaneh + " دهانه " + D + " متری ";
                    RizMetre.FBId = guFBId090206;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "300" + PolNum.ToString("D3") + "50";///آرماتور
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";

                    decimal dMeghdarJoz = 0;
                    if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                        dMeghdarJoz = 0;
                    else
                        dMeghdarJoz += (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                        (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                    RizMetre.MeghdarJoz = RizMetre.MeghdarJoz = Math.Round(dMeghdarJoz,3);

                    _context.RizMetreUserses.Add(RizMetre);

                    clsPolVaAbroBarAvordRizMetre polVaAbroBarAvordRizMetre = new clsPolVaAbroBarAvordRizMetre
                    {
                        PolVaAbroBarAvordId = PolVaAbroId,
                        RizMetreId = RizMetre.ID,
                        PolDetailType = PolDetailTypeEnum.pos
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
                        ItemFBShomareh = "090206",
                        Shomareh = Shomareh090206++,
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
                }
            }
        }
        return true;
    }

}
