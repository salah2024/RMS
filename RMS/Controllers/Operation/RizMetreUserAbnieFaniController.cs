using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.AbnieFani.Dto;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Common;
using RMS.Models.Entity;
using RMS.Models.StoredProceduresData;
using System.Data;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation;

public class RizMetreUserAbnieFaniController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;

    [HttpPost]
    public async Task<JsonResult> ConfirmRizMetreUsersForAbnieFani([FromBody] ConfirmRizMetreUsersForAbnieFaniInputDto request)
    {

        Guid PolVaAbroId = request.PolVaAbroId;
        int PolNum = request.PolNum;
        Guid BarAvordId = request.BarAvordId;
        NoeFehrestBaha NoeFBId = request.NoeFBId;
        string LAbro = request.LAbro;
        string w1 = request.w1;
        string w2 = request.w2;
        string w3 = request.w3;
        string w4 = request.w4;
        string f = request.f;
        string m = request.m;
        string n = request.n;
        string k = request.k;
        string h = request.h;
        string t = request.t;
        string b1 = request.b1;
        string b2 = request.b2;
        string c1 = request.c1;
        string c2 = request.c2;
        string j = request.j;
        string p1 = request.p1;
        string p2 = request.p2;
        string D = request.D;
        string Hs = request.Hs;
        string TedadDahaneh = request.TedadDahaneh;
        string NahveEjraDal = request.NahveEjraDal;
        string LPayeMoarab = request.LPayeMoarab;
        string LKooleMoarab = request.LKooleMoarab;
        string NoeBanaii = request.NoeBanaii;
        string LW1j = request.LW1j;
        string LW1p = request.LW1p;
        string LB1W1 = request.LB1W1;
        string LB2W1 = request.LB2W1;
        string LW2j = request.LW2j;
        string LW2p = request.LW2p;
        string LB1W2 = request.LB1W2;
        string LB2W2 = request.LB2W2;
        string LW3j = request.LW3j;
        string LW3p = request.LW3p;
        string LB1W3 = request.LB1W3;
        string LB2W3 = request.LB2W3;
        string LW4j = request.LW4j;
        string LW4p = request.LW4p;
        string LB1W4 = request.LB1W4;
        string LB2W4 = request.LB2W4;
        string hMinw1 = request.hMinw1;//h بالی
        string hMinw2 = request.hMinw2;//h بالی
        string hMinw3 = request.hMinw3;//h بالی
        string hMinw4 = request.hMinw4;//h بالی

        decimal TophMinw1 = Math.Ceiling(decimal.Parse(hMinw1));
        decimal TophMinw2 = Math.Ceiling(decimal.Parse(hMinw2));
        decimal TophMinw3 = Math.Ceiling(decimal.Parse(hMinw3));
        decimal TophMinw4 = Math.Ceiling(decimal.Parse(hMinw4));
        List<clsAbadDivarBali> lstAbadDivarBalis = _context.AbadDivarBalis.ToList();
        clsAbadDivarBali AbadDivarBaliForhMinw1 = lstAbadDivarBalis.First(x => x.h == TophMinw1);
        clsAbadDivarBali AbadDivarBaliForhMinw2 = lstAbadDivarBalis.First(x => x.h == TophMinw2);
        clsAbadDivarBali AbadDivarBaliForhMinw3 = lstAbadDivarBalis.First(x => x.h == TophMinw3);
        clsAbadDivarBali AbadDivarBaliForhMinw4 = lstAbadDivarBalis.First(x => x.h == TophMinw4);


        int Year = request.Year;

        //try
        //{
        string strPolNum = PolNum.ToString("D3");
        //List<string> lstStr = new List<string>();
        //lstStr.Add("3"); lstStr.Add("5");
        //var RizMetreUsers1 = context.RizMetreUserses.Where(x => x.FB.BarAvordId == BarAvordId && lstStr.Contains(x.Type.Trim().Substring(1, 1))
        //                     && x.Type.Trim().Substring(4, 3) == strPolNum).ToList();


        List<clsPolVaAbroBarAvordRizMetre> lstPolRMU = _context.PolVaAbroBarAvordRizMetres.Include(x => x.RizMetreUsers).Where(x => x.PolVaAbroBarAvordId == PolVaAbroId).ToList();


        if (lstPolRMU.Count != 0)
        {
            List<Guid> lstPolRMUIds = lstPolRMU.Select(x => x.RizMetreId).ToList();
            List<clsRizMetreUsers> lstRizMetreUsers = _context.RizMetreUserses.Where(x => lstPolRMUIds.Contains(x.ID)).ToList();
            if (lstRizMetreUsers.Count != 0)
            {
                context.RizMetreUserses.RemoveRange(lstRizMetreUsers);
                context.SaveChanges();
            }
        }
        //clsRizMetreUserss.Delete("BarAvordId=" + BarAvordID + " and (SUBSTRING(ltrim(rtrim(Type)),1,1) in('3','5')) and SUBSTRING(ltrim(rtrim(Type)),4,3)='" + PolNum.ToString("D3") + "'");
        AbnieFaniCommonGhalebBandi AbnieFaniGhalebBandi = new AbnieFaniCommonGhalebBandi(_context);
        AbnieFaniCommonGhalebBandiChenaj AbnieFaniGhalebBandiChenaj = new AbnieFaniCommonGhalebBandiChenaj(_context);
        AbnieFaniCommonGhalebBandiDalDarja AbnieFaniGhalebBandiDalDarja = new AbnieFaniCommonGhalebBandiDalDarja(_context);
        AbnieFaniCommonGhalebBandiDalPishSakhteh AbnieFaniGhalebBandiDalPishSakhteh = new AbnieFaniCommonGhalebBandiDalPishSakhteh(_context);
        AbnieFaniCommonBoton AbnieFaniBoton = new AbnieFaniCommonBoton(_context);
        AbnieFaniCommonGhalebBandiFendasionDastak AbnieFaniGhalebBandiFendasionDastak = new AbnieFaniCommonGhalebBandiFendasionDastak(_context);
        AbnieFaniCommonGhalebBandiDivarVaSotoonDastak AbnieFaniGhalebBandiDivarVaSotoonDastak = new AbnieFaniCommonGhalebBandiDivarVaSotoonDastak(_context);
        AbnieFaniCommonGhalebBandiSarKalaDastak AbnieFaniGhalebBandiSarKalaDastak = new AbnieFaniCommonGhalebBandiSarKalaDastak(_context);
        AbnieFaniCommonBotonFendasionDastak AbnieFaniBotonFendasionDastak = new AbnieFaniCommonBotonFendasionDastak(_context);
        AbnieFaniCommonArmator AbnieFaniArmator = new AbnieFaniCommonArmator(_context);
        AbnieFaniCommonBanaiiSangi AbnieFaniCommonBanaiiSangi = new AbnieFaniCommonBanaiiSangi(_context);

        string strLAbro = Math.Round(decimal.Parse(LAbro), 3).ToString();
        string strw1 = Math.Round(decimal.Parse(w1), 3).ToString();
        string strw2 = Math.Round(decimal.Parse(w2), 3).ToString();
        string strw3 = Math.Round(decimal.Parse(w3), 3).ToString();
        string strw4 = Math.Round(decimal.Parse(w4), 3).ToString();
        string strf = Math.Round(decimal.Parse(f), 3).ToString();
        string strm = Math.Round(decimal.Parse(m), 3).ToString();
        string strn = Math.Round(decimal.Parse(n), 3).ToString();
        string strk = Math.Round(decimal.Parse(k), 3).ToString();
        string strh = Math.Round(decimal.Parse(h), 3).ToString();
        string strD = Math.Round(decimal.Parse(D), 3).ToString();
        string strLPayeMoarab = Math.Round(decimal.Parse(LPayeMoarab), 3).ToString();
        string strLKooleMoarab = Math.Round(decimal.Parse(LKooleMoarab.Trim() == "" ? "0" : LKooleMoarab.Trim()), 3).ToString();
        string strt = Math.Round(decimal.Parse(t), 3).ToString();
        string strb1 = Math.Round(decimal.Parse(b1), 3).ToString();
        string strb2 = Math.Round(decimal.Parse(b2), 3).ToString();
        string strc1 = Math.Round(decimal.Parse(c1), 3).ToString();
        string strc2 = Math.Round(decimal.Parse(c2), 3).ToString();
        string strp1 = Math.Round(decimal.Parse(p1), 3).ToString();
        string strp2 = Math.Round(decimal.Parse(p2), 3).ToString();
        string strj = Math.Round(decimal.Parse(j), 3).ToString();
        string strLW1j = Math.Round(decimal.Parse(LW1j), 3).ToString();
        string strLW1p = Math.Round(decimal.Parse(LW1p), 3).ToString();
        string strLB1W1 = Math.Round(((decimal.Parse(hMinw1) / 3) + AbadDivarBaliForhMinw1.x + decimal.Parse("0.35") + AbadDivarBaliForhMinw1.b), 3).ToString(); //Math.Round(decimal.Parse(LB1W1), 2).ToString();
        //string strLB2W1 = Math.Round(decimal.Parse(LB2W1), 2).ToString();
        string strLW2j = Math.Round(decimal.Parse(LW2j), 3).ToString();
        string strLW2p = Math.Round(decimal.Parse(LW2p), 3).ToString();
        string strLB1W2 = Math.Round(((decimal.Parse(hMinw2) / 3) + AbadDivarBaliForhMinw2.x + decimal.Parse("0.35") + AbadDivarBaliForhMinw2.b), 3).ToString(); //Math.Round(decimal.Parse(LB1W2), 2).ToString();
        //string strLB2W2 = Math.Round(decimal.Parse(LB2W2), 2).ToString();
        string strLW3j = Math.Round(decimal.Parse(LW3j), 3).ToString();
        string strLW3p = Math.Round(decimal.Parse(LW3p), 3).ToString();
        string strLB1W3 = Math.Round(((decimal.Parse(hMinw3) / 3) + AbadDivarBaliForhMinw3.x + decimal.Parse("0.35") + AbadDivarBaliForhMinw3.b), 3).ToString(); //Math.Round(decimal.Parse(LB1W3), 2).ToString();
        //string strLB2W3 = Math.Round(decimal.Parse(LB2W3), 2).ToString();
        string strLW4j = Math.Round(decimal.Parse(LW4j), 3).ToString();
        string strLW4p = Math.Round(decimal.Parse(LW4p), 3).ToString();
        string strLB1W4 = Math.Round(((decimal.Parse(hMinw4) / 3) + AbadDivarBaliForhMinw4.x + decimal.Parse("0.35") + AbadDivarBaliForhMinw4.b), 3).ToString(); //Math.Round(decimal.Parse(LB1W4), 2).ToString();
        //string strLB2W4 = Math.Round(decimal.Parse(LB2W4), 2).ToString();
        string strhMinw1 = Math.Round(decimal.Parse(hMinw1), 3).ToString();
        string strhMinw2 = Math.Round(decimal.Parse(hMinw2), 3).ToString();
        string strhMinw3 = Math.Round(decimal.Parse(hMinw3), 3).ToString();
        string strhMinw4 = Math.Round(decimal.Parse(hMinw4), 3).ToString();
        string strHs = Math.Round(decimal.Parse(Hs), 3).ToString();

        int intNoeBanaii = int.Parse(NoeBanaii.Trim());
        //if (NoeBanaii.Trim() == "1")
        //{
        AbnieFaniGhalebBandi
            .GhalebBandi(BarAvordId, NoeFBId, PolVaAbroId, PolNum, strLAbro, strw1, strw2, strw3, strw4, strf, strm,
                         strn, strk, strh, strD, TedadDahaneh, strLPayeMoarab, strLKooleMoarab, b1, b2, p1, p2, intNoeBanaii,Year);
        //_context.SaveChanges();

        AbnieFaniGhalebBandiChenaj.GhalebBandiChenaj(BarAvordId, NoeFBId, PolVaAbroId, PolNum, strLAbro, strw1, strw2, strw3, strw4, strf, strm, strn, strk, strh, strt, strb2, strc2, strp2, D, TedadDahaneh);
        //_context.SaveChanges();

        if (NahveEjraDal.Trim() == "1")
            AbnieFaniGhalebBandiDalDarja.GhalebBandiDalDarja(BarAvordId, NoeFBId, PolVaAbroId, PolNum, strLAbro, strw1, strw2, strw3, strw4, strt, strj, strc1, strD, TedadDahaneh);
        else if (NahveEjraDal.Trim() == "2")
            AbnieFaniGhalebBandiDalPishSakhteh.GhalebBandiDalPishSakhteh(BarAvordId, PolVaAbroId, NoeFBId, PolNum, strLAbro, strw1, strw2, strw3, strw4, strt, strj, strc1, strD, TedadDahaneh);
        //_context.SaveChanges();

       await AbnieFaniBoton.Boton(PolVaAbroId, PolNum, BarAvordId, NoeFBId, strLAbro, strw1, strw2, strw3, strw4, strf, strm, strn, strk, strh, strt, strb1, strb2,
                        strj, strc1, strc2, strp1, strp2, strD, TedadDahaneh, NahveEjraDal, strhMinw1, strhMinw2, strhMinw3, strhMinw4, intNoeBanaii,Year);
        //_context.SaveChanges();

        AbnieFaniGhalebBandiFendasionDastak.GhalebBandiFendasionDastak(PolVaAbroId, NoeFBId, PolNum, strD, TedadDahaneh, BarAvordId,
            strLW1j, strLW1p, strLB1W1, strLW2j, strLW2p, strLB1W2, strLW3j, strLW3p, strLB1W3, strLW4j, strLW4p, strLB1W4, strh);
        //_context.SaveChanges();

        if (intNoeBanaii == 1)
        {
            AbnieFaniGhalebBandiDivarVaSotoonDastak.GhalebBandiDivarVaSotoonDastak(PolVaAbroId, NoeFBId, PolNum, D, TedadDahaneh, BarAvordId, strh, strt, strhMinw1,
                strhMinw2, strhMinw3, strhMinw4, strw1, strw2, strw3, strw4);
            //_context.SaveChanges();

            AbnieFaniGhalebBandiSarKalaDastak.GhalebBandiSarKalaDastak(PolVaAbroId, NoeFBId, PolNum, D, TedadDahaneh, BarAvordId, strt, strhMinw1, strhMinw2, strhMinw3, strhMinw4, strw1, strw2, strw3, strw4);
            //_context.SaveChanges();
        }

       await AbnieFaniBotonFendasionDastak.BotonFendasionDastak(PolVaAbroId, NoeFBId, PolNum, strD, TedadDahaneh, BarAvordId, strh, strt, strhMinw1, strhMinw2, strhMinw3, strhMinw4, strw1, strw2, strw3, strw4, intNoeBanaii,Year);
        //_context.SaveChanges();

       await AbnieFaniArmator.Armator(PolVaAbroId, PolNum, strD, TedadDahaneh, BarAvordId, NoeFBId, strLAbro, strHs,Year);
       // _context.SaveChanges();

        if (intNoeBanaii == 2)
        {
           await AbnieFaniCommonBanaiiSangi.BanaiiSangiKoole(PolVaAbroId, PolNum, BarAvordId, NoeFBId, strLAbro, strw1, strw2, strw3, strw4, strf, strm, strn, strk, strh, strt, strb1, strb2,
                                                    strj, strc1, strc2, strp1, strp2, strD, TedadDahaneh, NahveEjraDal, strhMinw1, strhMinw2, strhMinw3, strhMinw4, intNoeBanaii,Year);

           await AbnieFaniCommonBanaiiSangi.BanaiiSangiDastak(PolVaAbroId, PolNum, BarAvordId, NoeFBId, strLAbro, strw1, strw2, strw3, strw4, strf, strm, strn, strk, strh, strt, strb1, strb2,
                                                    strj, strc1, strc2, strp1, strp2, strD, TedadDahaneh, NahveEjraDal, strhMinw1, strhMinw2, strhMinw3, strhMinw4, intNoeBanaii,
                                                    AbadDivarBaliForhMinw1, AbadDivarBaliForhMinw2, AbadDivarBaliForhMinw3, AbadDivarBaliForhMinw4,Year);
        }




        context.SaveChanges();

        //}
        //else if (NoeBanaii.Trim() == "2")
        //{

        //}
        return new JsonResult("OK");
        //return "OK";
        //}
        //catch (Exception)
        //{
        //    return "NOK";
        //}
    }

    public JsonResult SaveEzafeBahaForAbnieFaniPol([FromBody] SaveEzafeBahaForAbnieFaniPolInputDto request)
    {
        Guid PolVaAbroId = request.PolVaAbroId;
        int PolNum = request.PolNum;
        Guid BarAvordId = request.BarAvordId;
        NoeFehrestBaha NoeFBId = request.NoeFBId;
        long QuestionForAbnieFaniId = request.QuestionForAbnieFaniId;
        string ItemsForAdd = request.ItemsForAdd;
        string ItemFBForAdd = request.ItemFBForAdd.Trim();
        DateTime Now = DateTime.Now;

        int LevelNumber = request.LevelNumber;

        string[] strShomareh = { "120701", "120702" };

        //bool blnCheck = false;
        //clsAbnieFaniQueries AbnieFaniQueries = new clsAbnieFaniQueries();
        //DataTable DtQuesForAbnieFaniValues = clsAbnieFaniQueries.ListWithParameterQuesForAbnieFaniValues("PolVaAbroId=" + PolVaAbroId + " and QuestionForAbnieFaniId=" + QuestionForAbnieFaniId);
        //if (DtQuesForAbnieFaniValues.Rows.Count == 0)
        //{
        //    if (AbnieFaniQueries.Save(QuestionForAbnieFaniId, 1, 0, PolVaAbroId))
        //        blnCheck = true;
        //}
        //else
        //    blnCheck = true;

        //if (blnCheck)
        //{

        string strOtherShomareh = "";
        for (int i = 0; i < strShomareh.Length; i++)
        {
            if (strShomareh[i] != ItemFBForAdd.Trim())
                strOtherShomareh = strShomareh[i];
        }
        var varFBUser = context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == ItemFBForAdd).ToList();
        DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

        var varFBUserOtherShomareh = context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strOtherShomareh).ToList();
        DataTable DtFBUserOtherShomareh = clsConvert.ToDataTable(varFBUserOtherShomareh);

        //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='" + ItemFBForAdd.Trim() + "'");
        //DataTable DtFBUserOtherShomareh = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='" + strOtherShomareh + "'");
        //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
        //int FBId = 0;
        //if (DtFBUser.Rows.Count == 0)
        //{

        //    FBId = OperationItemsFB.SaveFB(BarAvordID, ItemFBForAdd.Trim(), 0);
        //}
        //else
        //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

        ItemFBForAdd = ItemFBForAdd.Trim();
        Guid FBId = new Guid();
        if (DtFBUser.Rows.Count == 0)
        {
            clsFB FBSave = new clsFB();
            FBSave.BarAvordId = BarAvordId;
            FBSave.Shomareh = ItemFBForAdd;
            //FBSave.BahayeVahedZarib = 0;
            FBSave.NoeFBId = NoeFBId;
            context.FBs.Add(FBSave);
            context.SaveChanges();
            FBId = FBSave.ID;
        }
        else
            FBId = Guid.Parse(DtFBUser.Rows[0]["Id"].ToString());


        bool blnCheckIsOtherShomarehRizMetreDelete = true;
        if (DtFBUserOtherShomareh.Rows.Count != 0)
        {
            //DataTable DtQuesForAbnieFani = clsQuesForAbnieFani.ListWithParameterQuesForAbnieFaniValues("isezafebaha=1 and ID<>" + QuestionForAbnieFaniId);

            string strParam = "isezafebaha=1 and Id<>" + QuestionForAbnieFaniId;
            var QuesForAbnieFaniParam = new SqlParameter("@Parameter", strParam);

            var QuesForAbnieFani = _context.Set<QuesForAbnieFaniValuesDto>()
                .FromSqlRaw("EXEC QuesForAbnieFaniValuesListWithParameter @Parameter", QuesForAbnieFaniParam)
                .ToList();

            DataTable DtQuesForAbnieFani = clsConvert.ToDataTable(QuesForAbnieFani);

            //DataTable DtQuesForAbnieFani = clsAbnieFaniQueries.ListWithParameterQuesForAbnieFani("isezafebaha=1 and id<>" + QuestionForAbnieFaniId);
            Guid FBIdOtherShomareh = Guid.Parse(DtFBUserOtherShomareh.Rows[0]["Id"].ToString());
            long lngQuesForAbnieFaniId = long.Parse(DtQuesForAbnieFani.Rows[0]["Id"].ToString());
            var varAbnieFaniQueries = context.QuesForAbnieFaniValuess.Where(x => x.PolVaAbroId == PolVaAbroId && x.QuestionForAbnieFaniId == lngQuesForAbnieFaniId).ToList();
            if (varAbnieFaniQueries != null)
            {
                context.QuesForAbnieFaniValuess.RemoveRange(varAbnieFaniQueries);
                context.SaveChanges();
            }
            //clsAbnieFaniQueries.DeleteQuesForAbnieFaniValuesWithParameter("PolVaAbroId=" + PolVaAbroId + " and QuestionForAbnieFaniId=" + DtQuesForAbnieFani.Rows[0]["Id"].ToString());

            var varRizMetreUsers = (from RizMetreUsers in context.RizMetreUserses
                                    join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
                                    where RizMetreUsers.LevelNumber == LevelNumber
                                    select new
                                    {
                                        ID = RizMetreUsers.ID,
                                        Shomareh = RizMetreUsers.Shomareh,
                                        Tedad = RizMetreUsers.Tedad,
                                        Tool = RizMetreUsers.Tool,
                                        Arz = RizMetreUsers.Arz,
                                        Ertefa = RizMetreUsers.Ertefa,
                                        Vazn = RizMetreUsers.Vazn,
                                        Des = RizMetreUsers.Des,
                                        FBId = RizMetreUsers.FBId,
                                        OperationsOfHamlId = RizMetreUsers.OperationsOfHamlId,
                                        ForItem = RizMetreUsers.ForItem,
                                        Type = RizMetreUsers.Type,
                                        UseItem = RizMetreUsers.UseItem,
                                        BarAvordId = FB.BarAvordId
                                    }).Where(x => x.FBId == FBIdOtherShomareh && x.Type == "300" + PolNum.ToString("D3") + "60").OrderBy(x => x.Shomareh).ToList();
            DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);
            // DataTable DtRizMetreUsers = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + FBIdOtherShomareh + " and type=300" + PolNum.ToString("D3") + "60");
            if (DtRizMetreUsers.Rows.Count != 0)
            {
                try
                {
                    var varRizMetreUsers1 = context.RizMetreUserses.Where(x => x.FBId == FBIdOtherShomareh && x.Type == "300" + PolNum.ToString("D3") + "60").ToList();
                    context.RizMetreUserses.RemoveRange(varRizMetreUsers1);
                    context.SaveChanges();
                    blnCheckIsOtherShomarehRizMetreDelete = true;
                }
                catch (Exception)
                {
                    blnCheckIsOtherShomarehRizMetreDelete = false;
                }

                //blnCheckIsOtherShomarehRizMetreDelete = clsRizMetreUserss.Delete("FBId=" + FBIdOtherShomareh + " and type=300" + PolNum.ToString("D3") + "60");
            }
        }

        if (blnCheckIsOtherShomarehRizMetreDelete)
        {
            bool blnAbnieFaniQueriesCheck = false;
            try
            {
                clsQuesForAbnieFaniValues QuesForAbnieFaniValue = new clsQuesForAbnieFaniValues();
                QuesForAbnieFaniValue.QuestionForAbnieFaniId = QuestionForAbnieFaniId;
                QuesForAbnieFaniValue.Value = 1;
                QuesForAbnieFaniValue.ShomarehFBSelectedId = 0;
                QuesForAbnieFaniValue.PolVaAbroId = PolVaAbroId;
                context.QuesForAbnieFaniValuess.Add(QuesForAbnieFaniValue);
                context.SaveChanges();
                blnAbnieFaniQueriesCheck = true;
            }
            catch (Exception)
            {
                blnAbnieFaniQueriesCheck = false;
            }
            //if (AbnieFaniQueries.Save(QuestionForAbnieFaniId, 1, 0, PolVaAbroId))
            if (blnAbnieFaniQueriesCheck)
            {
                //DataTable DtLastRizMetreUsersShomareh = clsRizMetreUserss.GetLastRizMetreUsersShomareh("FBId=" + FBId + " and type=300" + PolNum.ToString("D3") + "60");
                //int Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                long Shomareh = 1;
                clsRizMetreUsers RizMetreUser = context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                if (RizMetreUser != null)
                    Shomareh = RizMetreUser.Shomareh + 1;
                else
                    Shomareh = 1;

                string[] ItemsForAddSplit = ItemsForAdd.Split(',');
                for (int i = 0; i < ItemsForAddSplit.Length - 1; i++)
                {
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    string[] ItemsForAddSplitSplit = ItemsForAddSplit[i].Split("");
                    decimal Tedad = decimal.Parse(ItemsForAddSplitSplit[1].Trim());
                    decimal Vazn = decimal.Parse(ItemsForAddSplitSplit[2].Trim());
                    RizMetre.Shomareh = Shomareh++;
                    RizMetre.Sharh = "اضافه بها بابت مصرف سیمان";
                    RizMetre.Tedad = Tedad;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = Vazn;
                    RizMetre.Des = "";
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "300" + PolNum.ToString("D3") + "60";///اضافه بها بابت مصرف سیمان
                    RizMetre.ForItem = ItemsForAddSplitSplit[0].Trim();
                    RizMetre.UseItem = "";
                    RizMetre.LevelNumber = LevelNumber;
                    RizMetre.InsertDateTime = Now;

                    ///محاسبه مقدار جزء
                    decimal dMeghdarJoz = 0;
                    if (Tedad == 0 && Vazn == 0)
                        dMeghdarJoz = 0;
                    else
                        dMeghdarJoz += (Tedad == 0 ? 1 : Tedad) * (Vazn == 0 ? 1 : Vazn);
                    RizMetre.MeghdarJoz = dMeghdarJoz;


                    context.RizMetreUserses.Add(RizMetre);
                    context.SaveChanges();
                    //RizMetre.Save();
                }
                return new JsonResult("OK");

                //return "OK";
            }
            else
                return new JsonResult("NOK");

            //return "NOK";
        }
        else
            return new JsonResult("NOK");

        //return "NOK";
        //}
        //else
        //    return "NOK";
        /////////////////
    }

    public JsonResult UpdateEzafeBahaForAbnieFaniPol([FromBody] UpdateEzafeBahaForAbnieFaniPolInputDto request)
    {
        Guid BarAvordId = request.BarAvordId;
        Guid PolVaAbroId = request.PolVaAbroId;
        int PolNum = request.PolNum;
        string ItemFBForUpdate = request.ItemFBForUpdate;
        string ItemShomareh = request.ItemShomareh;
        string Hajm = request.Hajm;
        string Meghdar = request.Meghdar;
        DateTime Now = DateTime.Now;


        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        RizMetre.Tedad = decimal.Parse(Hajm.Trim());
        RizMetre.Tool = 0;
        RizMetre.Arz = 0;
        RizMetre.Ertefa = 0;
        RizMetre.Vazn = decimal.Parse(Meghdar.Trim());
        RizMetre.Des = "";
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "60";///اضافه بها بابت مصرف سیمان
        RizMetre.ForItem = ItemShomareh.Trim();
        RizMetre.UseItem = "";
        //RizMetre.UpdateWithShomarehAndForItemAndType(BarAvordId, ItemFBForUpdate);
        return new JsonResult("OK");

        //return "OK";
        /////////////////
    }

    [HttpPost]
    public JsonResult GetRizMetreAbnieFaniWithPolId([FromBody] GetRizMetreAbnieFaniWithPolIdDto request)
    {

        clsPolVaAbroBarAvord? Pol = _context.PolVaAbroBarAvords.Where(x => x.ID == request.PolId).FirstOrDefault();
        if (Pol == null)
        {
            return new JsonResult("NOK_");
        }

        List<ItemFBShomarehForGetAndShowAddItemsFieldsDto> lstItemFields = _context.ItemsFieldses.Where(x => x.NoeFB == request.NoeFB).Select(x => new ItemFBShomarehForGetAndShowAddItemsFieldsDto
        {
            Shomareh = x.ItemShomareh,
            FieldType = x.FieldType,
            Vahed = x.Vahed,
            IsEnteringValue = x.IsEnteringValue
        }).ToList();

        //RizMetreAbnieFaniWithPolIdDto RizMetre = new RizMetreAbnieFaniWithPolIdDto();


        List<RizMetreAbnieFaniWithPolIdDto> lstRizMetre =
            _context.PolVaAbroBarAvordRizMetres.Include(x => x.RizMetreUsers).ThenInclude(x => x.FB).Where(x => x.PolVaAbroBarAvordId == request.PolId).Select(x => new RizMetreAbnieFaniWithPolIdDto
            {
                ID = x.RizMetreId,
                Shomareh = x.RizMetreUsers.Shomareh,
                ShomarehNew = x.RizMetreUsers.ShomarehNew,
                Sharh = x.RizMetreUsers.Sharh,
                Tedad = x.RizMetreUsers.Tedad,
                Tool = x.RizMetreUsers.Tool,
                Arz = x.RizMetreUsers.Arz,
                Ertefa = x.RizMetreUsers.Ertefa,
                Vazn = x.RizMetreUsers.Vazn,
                Des = x.RizMetreUsers.Des,
                MeghdarJoz = x.RizMetreUsers.MeghdarJoz,
                FBShomareh = x.RizMetreUsers.FB.Shomareh
            }).ToList();

        List<string> lstFBShomareh = lstRizMetre.Select(x => x.FBShomareh).ToList();
        List<string> lstFBShomarehNew = new List<string>();
        foreach (var item in lstFBShomareh)
        {
            if (item.Substring(0, 2) != "20")
            {
                if (!lstFBShomarehNew.Contains(item.Substring(0, 6)))
                {
                    lstFBShomarehNew.Add(item.Substring(0, 6));
                }
            }
        }

        List<clsFehrestBaha> lstFehrestBahas = _context.FehrestBahas.Where(x => x.Sal == request.Year && x.NoeFB == request.NoeFB && lstFBShomarehNew.Contains(x.Shomareh.Trim())).ToList();

        List<ItemFBShomarehForGetAndShowAddItemsFieldsDto> itemsFields1 = lstItemFields.Where(x => lstFBShomarehNew.Contains(x.Shomareh)).ToList();

        List<ItemFBShomarehForGetAndShowAddItemsDto> lstItemFBShomarehForGet = new List<ItemFBShomarehForGetAndShowAddItemsDto>();

        List<ItemFBShomarehForGetAndShowAddItemsFieldsDto> ItemFields = new List<ItemFBShomarehForGetAndShowAddItemsFieldsDto>();

        foreach (var item in lstFBShomarehNew)
        {
            clsFehrestBaha fehrestBaha = lstFehrestBahas.First(x => x.Shomareh == item);

            ItemFields = itemsFields1.Where(x => x.Shomareh.Trim() == item).ToList();

            ItemFBShomarehForGetAndShowAddItemsDto ItemFBShomarehForGet = new ItemFBShomarehForGetAndShowAddItemsDto
            {
                ItemFBShomareh = item,
                Des = fehrestBaha.Sharh,
                ItemFields = ItemFields

            };
            lstItemFBShomarehForGet.Add(ItemFBShomarehForGet);
        }

        lstItemFBShomarehForGet = lstItemFBShomarehForGet.OrderBy(x => x.ItemFBShomareh).ToList();

        var result = new
        {
            lst = lstRizMetre,
            lstItemFBShomarehForGet
        };

        return new JsonResult(result);
    }
}
