using Microsoft.AspNetCore.Mvc;
using RMS.Models.Entity;

namespace RMS.Controllers.KhakRizi;

public class KhakRiziController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;

    public JsonResult GetDetailsOfKMKhakBardariInfoWithKMKhakRiziId(Guid AmalyateKhakiInfoForBarAvordId)
    {
        List<clsAmalyateKhakiInfoForBarAvordMore> tblKMAmalyateKhakiBarAvordMore = _context.AmalyateKhakiInfoForBarAvordMores.Where(x => x.AmalyateKhakiInfoForBarAvordId == AmalyateKhakiInfoForBarAvordId).ToList();
        //DataTable DtKMAmalyateKhakiBarAvordMore = clsConvert.ToDataTable(varKMAmalyateKhakiBarAvordMore);
        //DataTable DtKMAmalyateKhakiBarAvordMore = clsAmalyateKhakiInfoForBarAvord.ListWithParameterMore("AmalyateKhakiInfoForBarAvordId=" + AmalyateKhakiInfoForBarAvordId);
        List<clsAmalyateKhakiInfoForBarAvordDetails> tblKMAmalyateKhakiBarAvordDetails = _context.AmalyateKhakiInfoForBarAvordDetailses.Where(x => x.AmalyateKhakiInfoForBarAvordId == AmalyateKhakiInfoForBarAvordId).ToList();
        //DataTable DtKMAmalyateKhakiBarAvordDetails = clsConvert.ToDataTable(varKMAmalyateKhakiBarAvordDetails);
        //DataTable DtKMAmalyateKhakiBarAvordDetails = clsAmalyateKhakiInfoForBarAvordDetails.ListWithParameter("AmalyateKhakiInfoForBarAvordId=" + AmalyateKhakiInfoForBarAvordId);

        //string str = "";
        //if (DtKMAmalyateKhakiBarAvordDetails.Rows.Count != 0)
        //{
        //    str += "AmalyateKhakiInfoForBarAvordDetailsId in(";
        //    for (int i = 0; i < DtKMAmalyateKhakiBarAvordDetails.Rows.Count; i++)
        //    {
        //        if ((i + 1) != DtKMAmalyateKhakiBarAvordDetails.Rows.Count)
        //            str += DtKMAmalyateKhakiBarAvordDetails.Rows[i]["Id"].ToString() + ",";
        //        else
        //            str += DtKMAmalyateKhakiBarAvordDetails.Rows[i]["Id"].ToString();
        //    }
        //    str += ")";
        //}

        List<Guid> gKMAId = tblKMAmalyateKhakiBarAvordDetails.Select(x => x.ID).ToList();
        //if (varKMAmalyateKhakiBarAvordDetails.Count != 0)
        //{
        //    for (int i = 0; i < varKMAmalyateKhakiBarAvordDetails.Rows.Count; i++)
        //    {
        //        gKMAId.Add(Guid.Parse(DtKMAmalyateKhakiBarAvordDetails.Rows[i]["_ID"].ToString()));
        //    }
        //}

        var tblKMAmalyateKhakiBarAvordDetailsMore = _context.AmalyateKhakiInfoForBarAvordDetailsMores.Where(x => gKMAId.Contains(x.AmalyateKhakiInfoForBarAvordDetailsId)).ToList();
        //DataTable DtKMAmalyateKhakiBarAvordDetailsMore = clsConvert.ToDataTable(varKMAmalyateKhakiBarAvordDetailsMore);


        //DataTable DtKMAmalyateKhakiBarAvordDetailsMore = clsAmalyateKhakiInfoForBarAvordDetails.ListWithParameterMore(str);
        var tblKMAmalyateKhakiBarAvordDetailsEzafeBaha = _context.AmalyateKhakiInfoForBarAvordDetailsMores.Where(x => gKMAId.Contains(x.AmalyateKhakiInfoForBarAvordDetailsId)).ToList();
        //DataTable DtKMAmalyateKhakiBarAvordDetailsEzafeBaha = clsConvert.ToDataTable(varKMAmalyateKhakiBarAvordDetailsEzafeBaha);
        //DataTable DtKMAmalyateKhakiBarAvordDetailsEzafeBaha = clsAmalyateKhakiInfoForBarAvordDetails.ListWithParameterEzafeBaha(str);

        //DataSet Ds = new DataSet();

        var result = new
        {
            tblKMAmalyateKhakiBarAvordMore,
            tblKMAmalyateKhakiBarAvordDetails,
            tblKMAmalyateKhakiBarAvordDetailsMore,
            tblKMAmalyateKhakiBarAvordDetailsEzafeBaha
        };

        return new JsonResult(result);

        //DtKMAmalyateKhakiBarAvordDetails.TableName = "tblKMAmalyateKhakiBarAvordDetails";
        //DtKMAmalyateKhakiBarAvordMore.TableName = "tblKMAmalyateKhakiBarAvordMore";
        //DtKMAmalyateKhakiBarAvordDetailsMore.TableName = "tblKMAmalyateKhakiBarAvordDetailsMore";
        //DtKMAmalyateKhakiBarAvordDetailsEzafeBaha.TableName = "tblKMAmalyateKhakiBarAvordDetailsEzafeBaha";

        //Ds.Tables.Add(DtKMAmalyateKhakiBarAvordDetails);
        //Ds.Tables.Add(DtKMAmalyateKhakiBarAvordMore);
        //Ds.Tables.Add(DtKMAmalyateKhakiBarAvordDetailsMore);
        //Ds.Tables.Add(DtKMAmalyateKhakiBarAvordDetailsEzafeBaha);

        //return Json(Ds.GetXml(), JsonRequestBehavior.AllowGet);
        //return Ds.GetXml();
    }
}
