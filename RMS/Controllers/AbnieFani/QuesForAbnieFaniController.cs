using System.Data;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.AbnieFani.Dto;
using RMS.Models.Common;
using RMS.Models.Entity;
using RMS.Models.StoredProceduresData;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.AbnieFani
{
    public class QuesForAbnieFaniController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        // GET: QuesForAbnieFani
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetQuesForAbnieFaniEzafeBaha([FromBody] GetQuesForAbnieFaniEzafeBahaInputDto request)
        {
            Guid BarAvordId = request.BarAvordUserId;
            Guid PolVaAbroId = request.PolVaAbroId;
            int PolNum = request.PolNum;
            int Year = request.Year;
            NoeFehrestBaha NoeFB = request.NoeFB;

            var varAbnieFaniQueries = _context.QuesForAbnieFanis.Where(x => x.Year == Year && x.NoeFB == NoeFB && x.IsEzafeBaha == true).ToList();
            DataTable DtAbnieFaniQueries = clsConvert.ToDataTable(varAbnieFaniQueries);
            //DataTable DtAbnieFaniQueries = clsAbnieFaniQueries.ListWithParameterQuesForAbnieFani("year=" + year + " and NoeFB=" + NoeFB + " and IsEzafeBaha=1");

            List<long> strAbnieFaniQueriesId = new List<long>();
            if (DtAbnieFaniQueries.Rows.Count != 0)
            {
                //strAbnieFaniQueriesId = "QuesForAbnieFaniId in(";
                for (int i = 0; i < DtAbnieFaniQueries.Rows.Count; i++)
                {
                    strAbnieFaniQueriesId.Add(long.Parse(DtAbnieFaniQueries.Rows[i]["ID"].ToString().Trim()));
                    //strAbnieFaniQueriesId += DtAbnieFaniQueries.Rows[i]["Id"].ToString().Trim();
                    //if ((i + 1) != DtAbnieFaniQueries.Rows.Count)
                    //    strAbnieFaniQueriesId += ",";
                }
                //strAbnieFaniQueriesId += ")";
            }

            var varShomarehFBForQuesForAbnieFani = (from FehrestBaha in _context.FehrestBahas
                                                    join tQuesForAbnieFani in _context.QuesForAbnieFanis
                                                    on FehrestBaha.Sal equals tQuesForAbnieFani.Year
                                                    where FehrestBaha.NoeFB == tQuesForAbnieFani.NoeFB
                                                    join ShomarehFBForQuesForAbnieFani in _context.ShomarehFBForQuesForAbnieFanis
                                                    on tQuesForAbnieFani.Id equals ShomarehFBForQuesForAbnieFani.QuesForAbnieFaniId
                                                    where FehrestBaha.Shomareh == ShomarehFBForQuesForAbnieFani.Shomareh
                                                    select new
                                                    {
                                                        ShomarehFBForQuesForAbnieFani.Id,
                                                        ShomarehFBForQuesForAbnieFani.QuesForAbnieFaniId,
                                                        ShomarehFBForQuesForAbnieFani.Shomareh,
                                                        FehrestBaha.Sharh
                                                    }).Where(x => strAbnieFaniQueriesId.Contains(x.Id)).ToList();
            DataTable DtShomarehFBForQuesForAbnieFani = clsConvert.ToDataTable(varShomarehFBForQuesForAbnieFani);
            //DataTable DtShomarehFBForQuesForAbnieFani = clsAbnieFaniQueries.ListWithParameterShomarehFBForQuesForAbnieFani(strAbnieFaniQueriesId);


            DataSet Ds = new DataSet();
            DtAbnieFaniQueries.TableName = "tblAbnieFaniQueries";
            Ds.Tables.Add(DtAbnieFaniQueries);
            string strAbnieFaniQueries = "";
            if (DtAbnieFaniQueries.Rows.Count != 0)
            {
                strAbnieFaniQueries += " QuesForAbnieFaniId in(";
                for (int i = 0; i < DtAbnieFaniQueries.Rows.Count; i++)
                {
                    if ((i + 1) == DtAbnieFaniQueries.Rows.Count)
                        strAbnieFaniQueries += DtAbnieFaniQueries.Rows[i]["ID"].ToString();
                    else
                        strAbnieFaniQueries += DtAbnieFaniQueries.Rows[i]["ID"].ToString() + ",";
                }
                strAbnieFaniQueries += ")";
            }
            //DataTable DtItemsFBDependQuestionForAbnieFani = clsItemsFBDependQuestionForAbnieFani.ListWithParameterItemsFBDependQuestionForAbnieFaniWithHajm(strAbnieFaniQueries, BarAvordId);

            var ItemsFBDependQuestionForAbnieFaniParam = new SqlParameter("@Parameter", strAbnieFaniQueries);
            var BarAvordId1 = new SqlParameter("@BarAvordId", "'" + BarAvordId + "'");

            var ItemsFBDependQuestionForAbnieFani = _context.Set<ItemsFBDependQuestionForAbnieFaniForSPDto>()
                .FromSqlRaw("EXEC ItemsFBDependQuestionForAbnieFaniWithHajmListWithParametr @Parameter,@BarAvordId", ItemsFBDependQuestionForAbnieFaniParam, BarAvordId1)
                .ToList();

            DataTable DtItemsFBDependQuestionForAbnieFani = clsConvert.ToDataTable(ItemsFBDependQuestionForAbnieFani);

            // DataTable DtItemsFBDependQuestionForAbnieFani = clsAbnieFaniQueries.ListWithParameterItemsFBDependQuestionForAbnieFaniWithHajm(strAbnieFaniQueries, BarAvordId);

            DtItemsFBDependQuestionForAbnieFani.TableName = "tblItemsFBDependQuestionForAbnieFani";
            Ds.Tables.Add(DtItemsFBDependQuestionForAbnieFani);

            //DataTable DtQuesForAbnieFaniValues = clsQuesForAbnieFani.ListWithParameterQuesForAbnieFaniValues("_PolVaAbroId='" + PolVaAbroId + "'");

            string strParam1 = "PolVaAbroId='" + PolVaAbroId + "'";
            var QuesForAbnieFaniValuesParam = new SqlParameter("@Parameter", strParam1);

            var QuesForAbnieFaniValues = _context.Set<QuesForAbnieFaniValuesDto>()
                .FromSqlRaw("EXEC QuesForAbnieFaniValuesListWithParameter @Parameter", QuesForAbnieFaniValuesParam)
                .ToList();

            DataTable DtQuesForAbnieFaniValues = clsConvert.ToDataTable(QuesForAbnieFaniValues);

            //DataTable DtQuesForAbnieFaniValues = clsAbnieFaniQueries.ListWithParameterQuesForAbnieFaniValues("PolVaAbroId=" + PolVaAbroId);
            DtQuesForAbnieFaniValues.TableName = "clsQuesForAbnieFaniValues";
            Ds.Tables.Add(DtQuesForAbnieFaniValues);

            DtShomarehFBForQuesForAbnieFani.TableName = "tblShomarehFBForQuesForAbnieFani";
            Ds.Tables.Add(DtShomarehFBForQuesForAbnieFani);

            List<string> strShomareh = new List<string>();

            var varFB = _context.FBs.Where(x => x.BarAvordId == BarAvordId && strShomareh.Contains(x.Shomareh)).ToList();
            DataTable DtFB = clsConvert.ToDataTable(varFB);
            //DataTable DtFB = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordId + " and Shomareh in('120701','120702')");
            List<Guid> strParamFB = new List<Guid>();
            if (DtFB.Rows.Count != 0)
            {
                //strParamFB += "FBId in(";
                for (int i = 0; i < DtFB.Rows.Count; i++)
                {
                    //if ((i + 1) != DtFB.Rows.Count)
                    //    strParamFB += DtFB.Rows[i]["Id"].ToString() + ",";
                    //else
                    //    strParamFB += DtFB.Rows[i]["Id"].ToString();
                    strParamFB.Add(Guid.Parse(DtFB.Rows[i]["ID"].ToString()));
                }
                //strParamFB += ")";
            }

            if (DtFB.Rows.Count != 0)
            {
                var varRizMetreUsersEzafeBahaValue = (from RizMetreUsers in _context.RizMetreUserses
                                                      join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
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
                                                          BarAvordId = FB.BarAvordId,
                                                          FBID = FB.ID
                                                      }).Where(x => x.Type == "300" + PolNum.ToString("D3") + "60" && strParamFB.Contains(x.FBID)).OrderBy(x => x.Shomareh).ToList();
                DataTable DtRizMetreUsersEzafeBahaValue = clsConvert.ToDataTable(varRizMetreUsersEzafeBahaValue);
                //DataTable DtRizMetreUsersEzafeBahaValue = clsRizMetreUsers.RizMetreUsersListWithParameter(strParamFB + " and type=300" + PolNum.ToString("D3") + "60");
                DtRizMetreUsersEzafeBahaValue.Columns.Add("_ShomarehFB");
                for (int j = 0; j < DtFB.Rows.Count; j++)
                {
                    DataRow[] Dr = DtFB.Select("ID=" + DtFB.Rows[j]["ID"].ToString());
                    for (int i = 0; i < DtRizMetreUsersEzafeBahaValue.Rows.Count; i++)
                    {
                        if (DtRizMetreUsersEzafeBahaValue.Rows[i]["FBId"].ToString() == Dr[0]["ID"].ToString())
                            DtRizMetreUsersEzafeBahaValue.Rows[i]["ShomarehFB"] = Dr[0]["Shomareh"].ToString();
                    }
                }
                DtRizMetreUsersEzafeBahaValue.TableName = "tblRizMetreUsersEzafeBahaValue";
                Ds.Tables.Add(DtRizMetreUsersEzafeBahaValue);
            }
            return new JsonResult(Ds.GetXml());

            //return Content(Ds.GetXml());
        }


        public JsonResult GetQuestionForAbnieFani([FromBody] GetQuestionForAbnieFaniInputDto request)
        {

            Guid PolVaAbroId = request.PolVaAbroId;
            int Year = request.Year;
            NoeFehrestBaha NoeFB = request.NoeFB;


            var varAbnieFaniQueries = _context.QuesForAbnieFanis.Where(x => x.Year == Year && x.NoeFB == NoeFB && x.IsEzafeBaha == false).ToList();
            DataTable DtAbnieFaniQueries = clsConvert.ToDataTable(varAbnieFaniQueries);
            //DataTable DtAbnieFaniQueries = clsAbnieFaniQueries.ListWithParameterQuesForAbnieFani("year=" + year + " and NoeFB=" + NoeFB + " and IsEzafeBaha=0");
            List<long> strAbnieFaniQueriesId = new List<long>();
            if (DtAbnieFaniQueries.Rows.Count != 0)
            {
                //strAbnieFaniQueriesId = "QuesForAbnieFaniId in(";
                for (int i = 0; i < DtAbnieFaniQueries.Rows.Count; i++)
                {
                    strAbnieFaniQueriesId.Add(long.Parse(DtAbnieFaniQueries.Rows[i]["ID"].ToString().Trim()));
                    //strAbnieFaniQueriesId += DtAbnieFaniQueries.Rows[i]["Id"].ToString().Trim();
                    //if ((i + 1) != DtAbnieFaniQueries.Rows.Count)
                    //    strAbnieFaniQueriesId += ",";
                }
                //strAbnieFaniQueriesId += ")";
            }
            var varShomarehFBForQuesForAbnieFani = (from tblFehrestBaha in _context.FehrestBahas
                                                    join clsQuesForAbnieFani in _context.QuesForAbnieFanis
                                                    on tblFehrestBaha.Sal equals clsQuesForAbnieFani.Year
                                                    where
                                                    tblFehrestBaha.NoeFB == clsQuesForAbnieFani.NoeFB
                                                    join tblShomarehFBForQuesForAbnieFani in _context.ShomarehFBForQuesForAbnieFanis
                                                    on clsQuesForAbnieFani.Id equals tblShomarehFBForQuesForAbnieFani.QuesForAbnieFaniId
                                                    where tblFehrestBaha.Shomareh == tblShomarehFBForQuesForAbnieFani.Shomareh
                                                    select new
                                                    {
                                                        tblShomarehFBForQuesForAbnieFani.Id,
                                                        tblShomarehFBForQuesForAbnieFani.QuesForAbnieFaniId,
                                                        tblShomarehFBForQuesForAbnieFani.Shomareh,
                                                        tblFehrestBaha.Sharh
                                                    }).Where(x => strAbnieFaniQueriesId.Contains(x.QuesForAbnieFaniId)).ToList();
            DataTable DtShomarehFBForQuesForAbnieFani = clsConvert.ToDataTable(varShomarehFBForQuesForAbnieFani);
            //DataTable DtShomarehFBForQuesForAbnieFani = clsAbnieFaniQueries.ListWithParameterShomarehFBForQuesForAbnieFani(strAbnieFaniQueriesId);
            DataSet Ds = new DataSet();
            DtAbnieFaniQueries.TableName = "tblAbnieFaniQueries";
            Ds.Tables.Add(DtAbnieFaniQueries);

            //DataTable DtQuesForAbnieFaniValues = clsQuesForAbnieFani.ListWithParameterQuesForAbnieFaniValues("_PolVaAbroId='" + PolVaAbroId + "'");

            string strParam = "PolVaAbroId='" + PolVaAbroId + "'";
            var QuesForAbnieFaniValuesParam = new SqlParameter("@Parameter", strParam);

            var QuesForAbnieFaniValues = _context.Set<QuesForAbnieFaniValuesDto>()
                .FromSqlRaw("EXEC QuesForAbnieFaniValuesListWithParameter @Parameter", QuesForAbnieFaniValuesParam)
                .ToList();

            DataTable DtQuesForAbnieFaniValues = clsConvert.ToDataTable(QuesForAbnieFaniValues);



            DtQuesForAbnieFaniValues.TableName = "clsQuesForAbnieFaniValues";
            Ds.Tables.Add(DtQuesForAbnieFaniValues);

            DtShomarehFBForQuesForAbnieFani.TableName = "tblShomarehFBForQuesForAbnieFani";
            Ds.Tables.Add(DtShomarehFBForQuesForAbnieFani);
            return new JsonResult(Ds.GetXml());
        }

    }
}
