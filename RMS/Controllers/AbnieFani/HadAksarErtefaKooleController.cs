using System.Data;
using Microsoft.AspNetCore.Mvc;
using RMS.Controllers.AbnieFani.Dto;
using RMS.Models.Common;

namespace RMS.Controllers.AbnieFani
{
    public class HadAksarErtefaKooleController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        // GET: HadAksarErtefaKoole
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetPolInfo()
        {
            //    try
            //    {
            var HadAksarErtefaKoole = _context.HadAksarErtefaKooles.ToList();
            DataTable DtHadAksarErtefaKoole = clsConvert.ToDataTable(HadAksarErtefaKoole);
            //DtHadAksarErtefaKoole.Columns.Remove("AbadeKooles");
            //DataTable DtHadAksarErtefaKoole = clsHadAksarErtefaKoole.ListWithParametr("");
            DataSet Ds = new DataSet();
            DtHadAksarErtefaKoole.TableName = "HadAksarErtefaKoole";
            Ds.Tables.Add(DtHadAksarErtefaKoole);
            //return Content(Ds.GetXml(), "text/xml");
            return new JsonResult(Ds.GetXml());

            //return Content("<xml>sdfsdfsdf</xml>", "text/xml");
            //}
            //catch (Exception)
            //{
            //    return Content("NOK");
            //}
        }

        [HttpPost]
        public JsonResult GetAbadKooleInfo([FromBody] GetAbadKooleInputDto request)
        {
            try
            {

                int TedadDahaneh = request.TedadDahaneh;
                int DahaneAbro = request.DahaneAbro;
                decimal HadAksarErtefaKoole = request.HadAksarErtefaKoole;

                var varHadAksarErtefaKoole = _context.HadAksarErtefaKooles.Where(x => x.TedadDahaneh == TedadDahaneh &&
                x.DahaneAbro == DahaneAbro && x.HadAksarErtefaKoole == HadAksarErtefaKoole).ToList();
                DataTable DtHadAksarErtefaKoole = clsConvert.ToDataTable(varHadAksarErtefaKoole);
                //DataTable DtHadAksarErtefaKoole = clsHadAksarErtefaKoole.ListWithParametr("TedadDahaneh=" + TedadDahaneh
                //    + " and DahaneAbro=" + DahaneAbro + " and HadAksarErtefaKoole=" + HadAksarErtefaKoole);
                long lngId = long.Parse(DtHadAksarErtefaKoole.Rows[0]["ID"].ToString());
                var varAbadeKoole = _context.AbadeKooles.Where(x => x.HadAksarErtefaKooleId == lngId).ToList();
                DataTable DtAbadeKoole = clsConvert.ToDataTable(varAbadeKoole);
                //DtHadAksarErtefaKoole.Columns.Remove("AbadeKooles");
                DtAbadeKoole.Columns.Remove("HadAksarErtefaKoole");
                //DtAbadeKoole.Columns.Remove("AbroDaliDetails1");
                //DataTable DtAbadeKoole = clsAbadeKoole.ListWithParametr("HadAksarErtefaKooleId=" + DtHadAksarErtefaKoole.Rows[0]["Id"].ToString());
                DataSet Ds = new DataSet();
                DtAbadeKoole.TableName = "AbadeKoole";
                Ds.Tables.Add(DtAbadeKoole);
                return new JsonResult(Ds.GetXml());
            }
            catch (Exception)
            {
                return new JsonResult("NOK");
            }
        }
    }
}
