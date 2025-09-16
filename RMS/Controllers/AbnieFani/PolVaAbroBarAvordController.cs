using System.Data;
using Microsoft.AspNetCore.Mvc;
using RMS.Controllers.AbnieFani.Dto;
using RMS.Models.Common;
using RMS.Models.Entity;

namespace RMS.Controllers.AbnieFani
{
    public class PolVaAbroBarAvordController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        // GET: PolVaAbroBarAvord
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetExistingPolInfoWithBarAvordId([FromBody] GetExistingPolInfoWithBarAvordIdInputDto request)
        {
            var varPolVaAbroBarAvord = _context.PolVaAbroBarAvords.Where(x => x.BarAvordId == request.BarAvordUserId).ToList();
            DataTable DtPolVaAbroBarAvord = clsConvert.ToDataTable(varPolVaAbroBarAvord);
            //DataTable DtPolVaAbroBarAvord = clsPolVaAbroBarAvord.ListWithParametr("BarAvordId=" + BarAvordId);

            //string strParam = "";
            List<Guid> lngPolVaAbroBarAvordIds = new List<Guid>();
            if (DtPolVaAbroBarAvord.Rows.Count != 0)
            {
                //strParam = "PolVaAbroId in(";
                for (int i = 0; i < DtPolVaAbroBarAvord.Rows.Count; i++)
                {
                    lngPolVaAbroBarAvordIds.Add(Guid.Parse(DtPolVaAbroBarAvord.Rows[i]["ID"].ToString()));
                    //strParam += DtPolVaAbroBarAvord.Rows[i]["Id"].ToString();
                    //if ((i + 1) != DtPolVaAbroBarAvord.Rows.Count)
                    //    strParam += ",";
                }
                //strParam += ")";
            }

            var varDastakPolInfo = _context.DastakPolInfos.Where(x => lngPolVaAbroBarAvordIds.Contains(x.PolVaAbroId)).ToList();
            DataTable DtDastakPolInfo = clsConvert.ToDataTable(varDastakPolInfo);
            //DataTable DtDastakPolInfo = clsDastakPolInfo.ListWithParametr(strParam);

            DataSet Ds = new DataSet();

            DtPolVaAbroBarAvord.TableName = "tblPolVaAbroBarAvord";
            DtDastakPolInfo.TableName = "tblDastakPolInfo";
            DtDastakPolInfo.Columns.Remove("PolVaAbroBarAvord");

            DtPolVaAbroBarAvord.Columns.Remove("BaravordUser");
            //DtPolVaAbroBarAvord.Columns.Remove("QuesForAbnieFaniValues1");
            //DtPolVaAbroBarAvord.Columns.Remove("DastakPolInfos");

            Ds.Tables.Add(DtPolVaAbroBarAvord);
            Ds.Tables.Add(DtDastakPolInfo);
            return new JsonResult(Ds.GetXml());
            //return Content(Ds.GetXml());
        }

        public JsonResult SaveAbroInfo([FromBody] SaveAbroPolInfoDto request)
        {
            try
            {
                Guid BarAvordId = request.BarAvordId;
                string TedadDahaneh = request.TedadDahaneh;
                string DahaneAbro = request.DahaneAbro;
                string HadeAksarErtefa = request.HadeAksarErtefa;
                string ErtefaKhakriz = request.ErtefaKhakriz;
                string NoeBanaii = request.NoeBanaii;
                string NahveEjraDal = request.NahveEjraDal;

                string w1 = request.W1;
                string w2 = request.W2;
                string w3 = request.W3;
                string w4 = request.W4;

                string alfaw1 = request.AlfaW1;
                string alfaw2 = request.AlfaW2;
                string alfaw3 = request.AlfaW3;
                string alfaw4 = request.AlfaW4;

                string hMinw1 = request.HMinW1;
                string hMinw2 = request.HMinW2;
                string hMinw3 = request.HMinW3;
                string hMinw4 = request.HMinW4;

                string alfa = request.Alfa;
                string LAbro = request.LAbro;
                string SAbroX = request.SAbroX;
                string SAbroY = request.SAbroY;

                clsPolVaAbroBarAvord PolVaAbroBarAvord = new clsPolVaAbroBarAvord();
                PolVaAbroBarAvord.BarAvordId = BarAvordId;
                PolVaAbroBarAvord.TedadDahaneh = int.Parse(TedadDahaneh.Trim());
                PolVaAbroBarAvord.DahaneAbro = int.Parse(DahaneAbro.Trim());
                PolVaAbroBarAvord.HadAksarErtefaKoole = decimal.Parse(HadeAksarErtefa.Trim());
                PolVaAbroBarAvord.Hs = ErtefaKhakriz.Trim();
                PolVaAbroBarAvord.ZavieBie = int.Parse(alfa.Trim());
                PolVaAbroBarAvord.ToolAbro = decimal.Parse(LAbro.Trim());
                PolVaAbroBarAvord.X = decimal.Parse(SAbroX.Trim());
                PolVaAbroBarAvord.Y = decimal.Parse(SAbroY.Trim());
                PolVaAbroBarAvord.NoeBanaii = short.Parse(NoeBanaii.Trim());
                PolVaAbroBarAvord.NahveEjraDal = short.Parse(NahveEjraDal.Trim());
                clsPolVaAbroBarAvord PolVaAbroBarAvord1 = _context.PolVaAbroBarAvords.Where(x => x.BarAvordId == BarAvordId).OrderByDescending(x => x.PolNum).FirstOrDefault();
                long PolNum = 0;
                if (PolVaAbroBarAvord1 != null)
                    PolNum = PolVaAbroBarAvord1.PolNum + 1;
                else
                    PolNum = 1;
                //int PolNum = int.Parse(clsPolVaAbroBarAvord.MaxPolVaAbroNumWithParameter("BarAvordId=" + BarAvordId).Rows[0]["MaxPolNum"].ToString());
                PolVaAbroBarAvord.PolNum = PolNum;
                //int PolVaAbroBarAvordId = PolVaAbroBarAvord.Save();
                Guid PolVaAbroBarAvordId = new Guid();
                try
                {
                    _context.PolVaAbroBarAvords.Add(PolVaAbroBarAvord);
                    _context.SaveChanges();
                    PolVaAbroBarAvordId = PolVaAbroBarAvord.ID;
                }
                catch (Exception)
                {
                }
                if (PolVaAbroBarAvordId != Guid.NewGuid())
                {
                    clsDastakPolInfo DastakPolInfo = new clsDastakPolInfo();
                    DastakPolInfo.PolVaAbroId = PolVaAbroBarAvordId;

                    DastakPolInfo.Shomareh = 1;
                    DastakPolInfo.ToolW = decimal.Parse(w1.Trim());
                    DastakPolInfo.Zavie = int.Parse(alfaw1.Trim());
                    DastakPolInfo.hMin = decimal.Parse(hMinw1.Trim());
                    _context.DastakPolInfos.Add(DastakPolInfo);
                    _context.SaveChanges();
                    //DastakPolInfo.Save();

                    DastakPolInfo = new clsDastakPolInfo();
                    DastakPolInfo.PolVaAbroId = PolVaAbroBarAvordId;
                    DastakPolInfo.Shomareh = 2;
                    DastakPolInfo.ToolW = decimal.Parse(w2.Trim());
                    DastakPolInfo.Zavie = int.Parse(alfaw2.Trim());
                    DastakPolInfo.hMin = decimal.Parse(hMinw2.Trim());
                    _context.DastakPolInfos.Add(DastakPolInfo);
                    _context.SaveChanges();
                    //DastakPolInfo.Save();

                    DastakPolInfo = new clsDastakPolInfo();
                    DastakPolInfo.PolVaAbroId = PolVaAbroBarAvordId;
                    DastakPolInfo.Shomareh = 3;
                    DastakPolInfo.ToolW = decimal.Parse(w3.Trim());
                    DastakPolInfo.Zavie = int.Parse(alfaw3.Trim());
                    DastakPolInfo.hMin = decimal.Parse(hMinw3.Trim());
                    _context.DastakPolInfos.Add(DastakPolInfo);
                    _context.SaveChanges();
                    //DastakPolInfo.Save();

                    DastakPolInfo = new clsDastakPolInfo();
                    DastakPolInfo.PolVaAbroId = PolVaAbroBarAvordId;
                    DastakPolInfo.Shomareh = 4;
                    DastakPolInfo.ToolW = decimal.Parse(w4.Trim());
                    DastakPolInfo.Zavie = int.Parse(alfaw4.Trim());
                    DastakPolInfo.hMin = decimal.Parse(hMinw4.Trim());
                    _context.DastakPolInfos.Add(DastakPolInfo);
                    _context.SaveChanges();
                    //DastakPolInfo.Save();

                }
                return new JsonResult("OK_" + PolVaAbroBarAvordId + "_" + PolNum);
                //return "OK_" + PolVaAbroBarAvordId + "_" + PolNum;
            }
            catch (Exception)
            {
                return new JsonResult("NOK_");
                //return null;
            }
        }

        [HttpPost]
        public JsonResult UpdateAbroInfo([FromBody] UpdateAbroInfoInputDto request)
        {
            Guid PolId = request.PolId;
            string TedadDahaneh = request.TedadDahaneh;
            string DahaneAbro = request.DahaneAbro;
            string HadeAksarErtefa = request.HadeAksarErtefa;
            string ErtefaKhakriz = request.ErtefaKhakriz;
            string NoeBanaii = request.NoeBanaii;
            string NahveEjraDal = request.NahveEjraDal;

            string w1 = request.W1;
            string w2 = request.W2;
            string w3 = request.W3;
            string w4 = request.W4;

            string alfaw1 = request.AlfaW1;
            string alfaw2 = request.AlfaW2;
            string alfaw3 = request.AlfaW3;
            string alfaw4 = request.AlfaW4;

            string hMinw1 = request.HMinW1;
            string hMinw2 = request.HMinW2;
            string hMinw3 = request.HMinW3;
            string hMinw4 = request.HMinW4;

            string alfa = request.Alfa;
            string LAbro = request.LAbro;
            string SAbroX = request.SAbroX;
            string SAbroY = request.SAbroY;

            //try
            //{
            clsPolVaAbroBarAvord PolVaAbroBarAvord1 = _context.PolVaAbroBarAvords.Where(x => x.ID == PolId).FirstOrDefault();
            clsPolVaAbroBarAvord PolVaAbroBarAvord = new clsPolVaAbroBarAvord();
            PolVaAbroBarAvord.ID = PolId;
            PolVaAbroBarAvord.TedadDahaneh = int.Parse(TedadDahaneh.Trim());
            PolVaAbroBarAvord.DahaneAbro = int.Parse(DahaneAbro.Trim());
            PolVaAbroBarAvord.HadAksarErtefaKoole = decimal.Parse(HadeAksarErtefa.Trim());
            PolVaAbroBarAvord.Hs = ErtefaKhakriz.Trim();
            PolVaAbroBarAvord.ZavieBie = int.Parse(alfa.Trim());
            PolVaAbroBarAvord.ToolAbro = decimal.Parse(LAbro.Trim());
            PolVaAbroBarAvord.X = decimal.Parse(SAbroX.Trim());
            PolVaAbroBarAvord.Y = decimal.Parse(SAbroY.Trim());
            PolVaAbroBarAvord.NoeBanaii = short.Parse(NoeBanaii.Trim());
            PolVaAbroBarAvord.NahveEjraDal = short.Parse(NahveEjraDal.Trim());
            PolVaAbroBarAvord.PolNum = PolVaAbroBarAvord1.PolNum;
            if (PolVaAbroBarAvord1 != null)
            {
                bool blnCheckUpdate = false;
                try
                {
                    _context.Entry(PolVaAbroBarAvord1).CurrentValues.SetValues(PolVaAbroBarAvord);
                    _context.SaveChanges();
                    blnCheckUpdate = true;
                }
                catch (Exception)
                {
                    blnCheckUpdate = false;
                }
                //if (PolVaAbroBarAvord.Update())
                if (blnCheckUpdate)
                {


                    clsDastakPolInfo DastakPolInfo = new clsDastakPolInfo();
                    DastakPolInfo.PolVaAbroId = PolId;

                    clsDastakPolInfo DastakPolInfo1 = _context.DastakPolInfos.Where(x => x.PolVaAbroId == PolId && x.Shomareh == 1).FirstOrDefault();
                    DastakPolInfo.Shomareh = 1;
                    DastakPolInfo.ToolW = decimal.Parse(w1.Trim());
                    DastakPolInfo.Zavie = int.Parse(alfaw1.Trim());
                    DastakPolInfo.hMin = decimal.Parse(hMinw1.Trim());
                    _context.Entry(DastakPolInfo1).CurrentValues.SetValues(DastakPolInfo);
                    _context.SaveChanges();

                    //DastakPolInfo.Update();
                    DastakPolInfo1 = _context.DastakPolInfos.Where(x => x.PolVaAbroId == PolId && x.Shomareh == 2).FirstOrDefault();
                    DastakPolInfo.Shomareh = 2;
                    DastakPolInfo.ToolW = decimal.Parse(w2.Trim());
                    DastakPolInfo.Zavie = int.Parse(alfaw2.Trim());
                    DastakPolInfo.hMin = decimal.Parse(hMinw2.Trim());
                    _context.Entry(DastakPolInfo1).CurrentValues.SetValues(DastakPolInfo);
                    _context.SaveChanges();
                    //DastakPolInfo.Update();

                    DastakPolInfo1 = _context.DastakPolInfos.Where(x => x.PolVaAbroId == PolId && x.Shomareh == 3).FirstOrDefault();
                    DastakPolInfo.Shomareh = 3;
                    DastakPolInfo.ToolW = decimal.Parse(w3.Trim());
                    DastakPolInfo.Zavie = int.Parse(alfaw3.Trim());
                    DastakPolInfo.hMin = decimal.Parse(hMinw3.Trim());
                    _context.Entry(DastakPolInfo1).CurrentValues.SetValues(DastakPolInfo);
                    _context.SaveChanges();
                    //DastakPolInfo.Update();

                    DastakPolInfo1 = _context.DastakPolInfos.Where(x => x.PolVaAbroId == PolId && x.Shomareh == 4).FirstOrDefault();
                    DastakPolInfo.Shomareh = 4;
                    DastakPolInfo.ToolW = decimal.Parse(w4.Trim());
                    DastakPolInfo.Zavie = int.Parse(alfaw4.Trim());
                    DastakPolInfo.hMin = decimal.Parse(hMinw4.Trim());
                    _context.Entry(DastakPolInfo1).CurrentValues.SetValues(DastakPolInfo);
                    _context.SaveChanges();
                    //DastakPolInfo.Update();
                }
                //return "OK_" + PolId;
                return new JsonResult("OK_" + PolId);
            }
            else
            {
                return new JsonResult("NOK_");
            }
            //}
            //catch (Exception)
            //{
            //    return Json("NOK_", JsonRequestBehavior.AllowGet);
            //}
        }


    }
}
