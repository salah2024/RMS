using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.AbnieFani.Dto;
using RMS.Models.Common;
using RMS.Models.Entity;
using RMS.Models.StoredProceduresData;

namespace RMS.Controllers.AbnieFani
{
    public class QuesForAbnieFaniValuesController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        // GET: QuesForAbnieFaniValues
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ConfirmQuesForAbnieFaniValues([FromBody] ConfirmQuesForAbnieFaniValuesInputDto request)
        {
            try
            {
                Guid PolVaAbroId = request.PolVaAbroId;
                string QuesVal = request.QuesVal;
                //DataTable DtAbnieFaniQueries = tblQuesForAbnieFani.ListWithParameterQuesForAbnieFaniValues("_PolVaAbroId='" + PolVaAbroId + "'");

                string strParam1 = "PolVaAbroId='" + PolVaAbroId + "'";
                var QuesForAbnieFaniValuesParam = new SqlParameter("@Parameter", strParam1);

                var QuesForAbnieFaniValues = _context.Set<QuesForAbnieFaniValuesDto>()
                    .FromSqlRaw("EXEC QuesForAbnieFaniValuesListWithParameter @Parameter", QuesForAbnieFaniValuesParam)
                    .ToList();

                DataTable DtAbnieFaniQueries = clsConvert.ToDataTable(QuesForAbnieFaniValues);

                //DataTable DtAbnieFaniQueries = clsAbnieFaniQueries.ListWithParameterQuesForAbnieFaniValues("PolVaAbroId=" + PolVaAbroId);
                if (DtAbnieFaniQueries.Rows.Count != 0)
                {
                    clsQuesForAbnieFaniValues QuesForAbnieFaniValues1 = _context.QuesForAbnieFaniValuess.Where(x => x.PolVaAbroId == PolVaAbroId).FirstOrDefault();
                    _context.QuesForAbnieFaniValuess.Remove(QuesForAbnieFaniValues1);
                    _context.SaveChanges();
                    //clsAbnieFaniQueries.DeleteQuesForAbnieFaniValuesWithParameter("PolVaAbroId=" + PolVaAbroId);
                }
                //clsAbnieFaniQueries AbnieFaniQueries = new clsAbnieFaniQueries();
                clsQuesForAbnieFani AbnieFaniQueries = new clsQuesForAbnieFani();
                string[] strSplit = QuesVal.Split(',');
                string strValue = "";
                string strId = "";
                string strShomarehFBSelectedId = "";
                for (int i = 0; i < strSplit.Length - 1; i++)
                {
                    string[] strSplitSplit = strSplit[i].Split('_');
                    for (int j = 0; j < strSplitSplit.Length; j++)
                    {
                        string strType = strSplitSplit[j].ToString().Substring(0, 2);
                        string strVal = strSplitSplit[j].ToString().Substring(2, strSplitSplit[j].ToString().Length - 2);
                        switch (strType)
                        {
                            case "id":
                                {
                                    strId = strVal;
                                    break;
                                }
                            case "vl":
                                {
                                    strValue = strVal;
                                    break;
                                }
                            case "qu":
                                {
                                    strShomarehFBSelectedId = strVal;
                                    break;
                                }
                            default:
                                break;
                        }
                    }

                    clsQuesForAbnieFaniValues QuesForAbnieFaniValues2 = new clsQuesForAbnieFaniValues();
                    QuesForAbnieFaniValues2.QuestionForAbnieFaniId = long.Parse(strId);
                    QuesForAbnieFaniValues2.Value = decimal.Parse(strValue);
                    QuesForAbnieFaniValues2.ShomarehFBSelectedId = int.Parse(strShomarehFBSelectedId);
                    QuesForAbnieFaniValues2.PolVaAbroId = PolVaAbroId;
                    _context.QuesForAbnieFaniValuess.Add(QuesForAbnieFaniValues2);
                    _context.SaveChanges();

                    //AbnieFaniQueries.Save(int.Parse(strId), decimal.Parse(strValue), int.Parse(strShomarehFBSelectedId), PolVaAbroId);
                }
                return new JsonResult("OK_");
            }
            catch (Exception)
            {
                return new JsonResult("NOK_");
            }
        }

        public JsonResult DeleteEzafeBahaForAbnieFaniPol([FromBody] DeleteEzafeBahaForAbnieFaniPolInputDto request)
        {

			Guid BarAvordUserID = request.BarAvordUserID;
			Guid PolVaAbroId = request.PolVaAbroId;
			long QuestionId = request.QuestionId;
			string ItemFBForDel = request.ItemFBForDel.Trim();

			//ItemFBForDel = ItemFBForDel.Trim();
            bool blnCheck = false;
            try
            {
                var varQuesForAbnieFaniValues = _context.QuesForAbnieFaniValuess.Where(x => x.QuestionForAbnieFaniId == QuestionId
                                                                              && x.PolVaAbroId == PolVaAbroId).ToList();
                _context.QuesForAbnieFaniValuess.RemoveRange(varQuesForAbnieFaniValues);
                _context.SaveChanges();
                blnCheck = true;
            }
            catch (Exception)
            {
                blnCheck = false;
            }
            //if (clsAbnieFaniQueries.DeleteQuesForAbnieFaniValuesWithParameter("QuestionForAbnieFaniId=" + QuestionId + " and PolVaAbroId=" + PolVaAbroId))
            if (blnCheck)
            {
                //clsOperation_ItemsFB Operation_ItemsFB = new clsOperation_ItemsFB();
                bool blnCheck1 = false;
                try
                {
                    var varFb = _context.FBs.Where(x => x.BarAvordId == BarAvordUserID && x.Shomareh == ItemFBForDel).ToList();
                    _context.FBs.RemoveRange(varFb);
                    _context.SaveChanges();
                    blnCheck1 = true;
                }
                catch (Exception)
                {
                    blnCheck1 = true;
                }

                //if (Operation_ItemsFB.DeleteFBWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='" + ItemFBForDel.Trim() + "'"))
                if (blnCheck1)
                    return new JsonResult("OK_");
                //return "OK_";
                else
                    return new JsonResult("NOK_");
                //return "NOK_";
            }
            else
                return new JsonResult("NOK_");
            //return "NOK_";
        }
    }
}
