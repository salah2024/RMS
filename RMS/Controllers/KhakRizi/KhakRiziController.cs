
using System.Data;
using Microsoft.AspNetCore.Mvc;
using RMS.Controllers.KhakRizi.Common;
using RMS.Controllers.KhakRizi.Dto;
using RMS.Models.Common;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.KhakRizi;

public class KhakRiziController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;

    public JsonResult GetDetailsOfKMKhakBardariInfoWithKMKhakRiziId(RequestGetDetailsOfKMKhakBardariInfoWithKMKhakRiziIdDto request)
    {
        Guid AmalyateKhakiInfoForBarAvordId = request.AmalyateKhakiInfoForBarAvordId;

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


    public JsonResult SaveKhakRiziInfoForBarAvord(RequestSaveKhakRiziInfoForBarAvordDto request)
    {
        //try
        //{

        Guid BarAvordUserId = request.BarAvordUserId;
        NoeAmalyatKhaki Type = request.Type;

        long FromKM = request.FromKM;

        long ToKM = request.ToKM;
        short radioNoeRahKhakRizi = request.radioNoeRahKhakRizi;

        string DarsadKRDDaneh = request.DarsadKRDDaneh;

        string DarsadKRRDaneh = request.DarsadKRRDaneh;
        string HajmBetween0To30 = request.HajmBetween0To30;
        string HajmBetween30To100 = request.HajmBetween0To30;

        string HajmBetweenTo100 = request.HajmBetween0To30;
        bool EzafeBahaKRKhakMosalah = request.EzafeBahaKRKhakMosalah;
        string KhakRiziInfoDetails = request.KhakRiziInfoDetails;
        string KhakRiziInfoDetailsCheckBox = request.KhakRiziInfoDetailsCheckBox;

        int intKMNum = 1;
        clsAmalyateKhakiInfoForBarAvord? currentKMNum = _context.AmalyateKhakiInfoForBarAvords.FirstOrDefault(x => x.BaravordUserId == BarAvordUserId && x.Type == Type);
        if (currentKMNum != null)
        {
            intKMNum = currentKMNum.KMNum + 1;
        }

        clsAmalyateKhakiInfoForBarAvord AmalyateKhakiInfoForBarAvord = new clsAmalyateKhakiInfoForBarAvord
        {
            BaravordUserId = BarAvordUserId,
            FromKM = FromKM.ToString("D6"),
            ToKM = ToKM.ToString("D6"),
            Type = Type,
            KMNum = intKMNum,
        };
        _context.AmalyateKhakiInfoForBarAvords.Add(AmalyateKhakiInfoForBarAvord);
        Guid Id = AmalyateKhakiInfoForBarAvord.ID;
        //rmsContext.SaveChanges();
        //long Id = AmalyateKhakiInfoForBarAvord.Save();
        ///////////

        decimal dDarsadKRDDaneh = decimal.Parse(DarsadKRDDaneh.Trim());
        decimal dDarsadKRRDaneh = decimal.Parse(DarsadKRRDaneh.Trim());
        decimal dHajmBetween0To30 = decimal.Parse(HajmBetween0To30.Trim());
        decimal dHajmBetween30To100 = decimal.Parse(HajmBetween30To100.Trim());
        decimal dHajmBetweenTo100 = decimal.Parse(HajmBetweenTo100.Trim());

        clsAmalyateKhakiInfoForBarAvordMore AmalyateKhakiInfoForBarAvordMore = new clsAmalyateKhakiInfoForBarAvordMore();
        AmalyateKhakiInfoForBarAvordMore.Value = radioNoeRahKhakRizi;
        AmalyateKhakiInfoForBarAvordMore.Name = "radioNoeRahKhakRizi";
        AmalyateKhakiInfoForBarAvordMore.AmalyateKhakiInfoForBarAvordId = Id;
        _context.AmalyateKhakiInfoForBarAvordMores.Add(AmalyateKhakiInfoForBarAvordMore);
        _context.SaveChanges();
        //AmalyateKhakiInfoForBarAvord.SaveMore(Id);
        ///////////
        AmalyateKhakiInfoForBarAvordMore = new clsAmalyateKhakiInfoForBarAvordMore();
        AmalyateKhakiInfoForBarAvordMore.Value = dDarsadKRDDaneh;
        AmalyateKhakiInfoForBarAvordMore.Name = "DarsadKRDDaneh";
        AmalyateKhakiInfoForBarAvordMore.AmalyateKhakiInfoForBarAvordId = Id;
        _context.AmalyateKhakiInfoForBarAvordMores.Add(AmalyateKhakiInfoForBarAvordMore);
        //_context.SaveChanges();
        //AmalyateKhakiInfoForBarAvord.SaveMore(Id);
        ///////////
        AmalyateKhakiInfoForBarAvordMore = new clsAmalyateKhakiInfoForBarAvordMore();
        AmalyateKhakiInfoForBarAvordMore.Value = dDarsadKRRDaneh;
        AmalyateKhakiInfoForBarAvordMore.Name = "DarsadKRRDaneh";
        AmalyateKhakiInfoForBarAvordMore.AmalyateKhakiInfoForBarAvordId = Id;
        _context.AmalyateKhakiInfoForBarAvordMores.Add(AmalyateKhakiInfoForBarAvordMore);
        //rmsContext.SaveChanges();
        //AmalyateKhakiInfoForBarAvord.SaveMore(Id);
        ///////////
        AmalyateKhakiInfoForBarAvordMore = new clsAmalyateKhakiInfoForBarAvordMore();
        AmalyateKhakiInfoForBarAvordMore.Value = dHajmBetween0To30;
        AmalyateKhakiInfoForBarAvordMore.Name = "HajmBetween0To30";
        AmalyateKhakiInfoForBarAvordMore.AmalyateKhakiInfoForBarAvordId = Id;
        _context.AmalyateKhakiInfoForBarAvordMores.Add(AmalyateKhakiInfoForBarAvordMore);
        //rmsContext.SaveChanges();
        //AmalyateKhakiInfoForBarAvord.SaveMore(Id);
        ///////////
        AmalyateKhakiInfoForBarAvordMore = new clsAmalyateKhakiInfoForBarAvordMore();
        AmalyateKhakiInfoForBarAvordMore.Value = dHajmBetween30To100;
        AmalyateKhakiInfoForBarAvordMore.Name = "HajmBetween30To100";
        AmalyateKhakiInfoForBarAvordMore.AmalyateKhakiInfoForBarAvordId = Id;
        _context.AmalyateKhakiInfoForBarAvordMores.Add(AmalyateKhakiInfoForBarAvordMore);
        //rmsContext.SaveChanges();
        //AmalyateKhakiInfoForBarAvord.SaveMore(Id);
        ///////////
        AmalyateKhakiInfoForBarAvordMore = new clsAmalyateKhakiInfoForBarAvordMore();
        AmalyateKhakiInfoForBarAvordMore.Value = dHajmBetweenTo100;
        AmalyateKhakiInfoForBarAvordMore.Name = "HajmBetweenTo100";
        AmalyateKhakiInfoForBarAvordMore.AmalyateKhakiInfoForBarAvordId = Id;
        _context.AmalyateKhakiInfoForBarAvordMores.Add(AmalyateKhakiInfoForBarAvordMore);
        //rmsContext.SaveChanges();
        //AmalyateKhakiInfoForBarAvord.SaveMore(Id);
        //if (Id > 0)
        Guid ckId = new Guid();
        if (Id != ckId)
        {
            clsAmalyateKhakiInfoForBarAvordDetails AmalyateKhakiInfoForBarAvordDetails = new clsAmalyateKhakiInfoForBarAvordDetails();
            /////////////
            AmalyateKhakiInfoForBarAvordDetails.AmalyateKhakiInfoForBarAvordId = Id;
            //AmalyateKhakiInfoForBarAvordDetails.NoeKhakBardari = 0; بررسی بشه
            Guid AmalyateKhakiInfoForBarAvordDetailsId = AmalyateKhakiInfoForBarAvordDetails.ID;
            _context.AmalyateKhakiInfoForBarAvordDetailses.Add(AmalyateKhakiInfoForBarAvordDetails);
            //rmsContext.SaveChanges();

            //long AmalyateKhakiInfoForBarAvordDetailsId = AmalyateKhakiInfoForBarAvordDetails.Save();

            //clsAmalyateKhakiInfoForBarAvordDetailsEzafeBaha AmalyateKhakiInfoForBarAvordDetailsEzafeBaha = new tblAmalyateKhakiInfoForBarAvordDetailsEzafeBaha();
            //AmalyateKhakiInfoForBarAvordDetailsEzafeBaha._Name = "KREzafeBahaKhakMosalah";
            //AmalyateKhakiInfoForBarAvordDetailsEzafeBaha._Value = EzafeBahaKRKhakMosalah;
            //AmalyateKhakiInfoForBarAvordDetailsEzafeBaha._AmalyateKhakiInfoForBarAvordDetailsId = AmalyateKhakiInfoForBarAvordDetailsId;
            //rmsContext.AmalyateKhakiInfoForBarAvordDetailsEzafeBahas.Add(AmalyateKhakiInfoForBarAvordDetailsEzafeBaha);
            //rmsContext.SaveChanges();
            //AmalyateKhakiInfoForBarAvordDetails.SaveEzafeBaha(AmalyateKhakiInfoForBarAvordDetailsId);

            if (AmalyateKhakiInfoForBarAvordDetailsId != ckId)
            {
                string[] KhakRiziInfoDetailsSplit = KhakRiziInfoDetails.Split('$');
                for (int i = 0; i < KhakRiziInfoDetailsSplit.Length - 1; i++)
                {
                    string[] KhakRiziInfoDetailsSplitSplit = KhakRiziInfoDetailsSplit[i].Split('_');
                    clsAmalyateKhakiInfoForBarAvordDetailsMore AmalyateKhakiInfoForBarAvordDetailsMore = new clsAmalyateKhakiInfoForBarAvordDetailsMore();
                    AmalyateKhakiInfoForBarAvordDetailsMore.Name = KhakRiziInfoDetailsSplitSplit[0];
                    AmalyateKhakiInfoForBarAvordDetailsMore.Value = decimal.Parse(KhakRiziInfoDetailsSplitSplit[1]);
                    AmalyateKhakiInfoForBarAvordDetailsMore.AmalyateKhakiInfoForBarAvordDetailsId = AmalyateKhakiInfoForBarAvordDetailsId;
                    _context.AmalyateKhakiInfoForBarAvordDetailsMores.Add(AmalyateKhakiInfoForBarAvordDetailsMore);
                    //rmsContext.SaveChanges();
                    //AmalyateKhakiInfoForBarAvordDetails.SaveMore(AmalyateKhakiInfoForBarAvordDetailsId);
                }
                /////////
                //string[] KhakRiziInfoDetailsCheckBoxSplit = KhakRiziInfoDetailsCheckBox.Split('$');
                //for (int i = 0; i < KhakRiziInfoDetailsCheckBoxSplit.Length - 1; i++)
                //{
                //    string[] KhakRiziInfoDetailsCheckBoxSplitSplit = KhakRiziInfoDetailsCheckBoxSplit[i].Split('_');
                //    AmalyateKhakiInfoForBarAvordDetailsEzafeBaha = new tblAmalyateKhakiInfoForBarAvordDetailsEzafeBaha();
                //    AmalyateKhakiInfoForBarAvordDetailsEzafeBaha._Name = KhakRiziInfoDetailsCheckBoxSplitSplit[0];
                //    AmalyateKhakiInfoForBarAvordDetailsEzafeBaha._Value = KhakRiziInfoDetailsCheckBoxSplitSplit[1].Trim() == "true" ? true : false;
                //    AmalyateKhakiInfoForBarAvordDetailsEzafeBaha._AmalyateKhakiInfoForBarAvordDetailsId = AmalyateKhakiInfoForBarAvordDetailsId;
                //    rmsContext.AmalyateKhakiInfoForBarAvordDetailsEzafeBahas.Add(AmalyateKhakiInfoForBarAvordDetailsEzafeBaha);
                //    rmsContext.SaveChanges();
                //    //AmalyateKhakiInfoForBarAvordDetails.SaveEzafeBaha(AmalyateKhakiInfoForBarAvordDetailsId);
                //}
            }
        }

        RequestSaveRizMetreKhakRiziDto requestSaveRizMetreKhakRizi = new RequestSaveRizMetreKhakRiziDto
        {
            BarAvordUserID = BarAvordUserId,
            KMId = Id,
            KMNum = intKMNum,
            KMS = FromKM.ToString("D6"),
            KME = ToKM.ToString("D6"),
            radioNoeRahKhakRizi = radioNoeRahKhakRizi,
            DarsadKRDDaneh = dDarsadKRDDaneh,
            DarsadKRRDaneh = dDarsadKRRDaneh,
            HajmBetween0To30 = dHajmBetween0To30,
            HajmBetween30To100 = dHajmBetween30To100,
            HajmBetweenTo100 = dHajmBetweenTo100,
            EzafeBahaKRKhakMosalah = EzafeBahaKRKhakMosalah
        };

        KhakRiziCommon khakRiziCommon = new KhakRiziCommon();

        khakRiziCommon.SaveRizMetreKhakRizi(requestSaveRizMetreKhakRizi, _context);

        RequestSaveRizMetreBestarKhakRiziDto requestSaveRizMetreBestarKhakRizi = new RequestSaveRizMetreBestarKhakRiziDto
        {
            BarAvordUserID = BarAvordUserId,
            KMId = Id,
            KMNum = intKMNum,
            KMS = FromKM.ToString("D6"),
            KME = ToKM.ToString("D6"),
            KhakRiziInfoDetails = KhakRiziInfoDetails,
            KhakRiziInfoDetailsCheckBox = KhakRiziInfoDetailsCheckBox
        };

        khakRiziCommon.SaveRizMetreBestarKhakRizi(requestSaveRizMetreBestarKhakRizi, _context);

        //khakRiziCommon.SaveRizMetreGharzeh(BarAvordUserId);

        return new JsonResult("OK_" + Id + "_" + intKMNum);


        //return "OK_" + Id + "_" + intKMNum;
        //}
        //catch (Exception)
        //{
        //    return Json("NOK", JsonRequestBehavior.AllowGet);
        //    //return "NOK";
        //}
    }

    public JsonResult UpdateKhakRiziInfoForBarAvord(requestUpdateKhakRiziInfoForBarAvordDto request)
    {
        //try
        //{

        Guid BarAvordUserId = request.BarAvordUserId;
        Guid KMKhakRiziId = request.KMKhakRiziId;
        int KMNum = request.KMNum;
        long FromKM = request.FromKM;
        long ToKM = request.ToKM;
        short radioNoeRahKhakRizi = request.radioNoeRahKhakRizi;
        string DarsadKRDDaneh = request.DarsadKRDDaneh;
        string DarsadKRRDaneh = request.DarsadKRRDaneh;
        string HajmBetween0To30 = request.HajmBetween0To30;
        string HajmBetween30To100 = request.HajmBetween30To100;
        string HajmBetweenTo100 = request.HajmBetweenTo100;
        bool EzafeBahaKRKhakMosalah = request.EzafeBahaKRKhakMosalah;
        string KhakRiziInfoDetails = request.KhakRiziInfoDetails;
        string KhakRiziInfoDetailsCheckBox = request.KhakRiziInfoDetailsCheckBox;

        //clsAmalyateKhakiInfoForBarAvord AmalyateKhakiInfoForBarAvord = new clsAmalyateKhakiInfoForBarAvord();
        //AmalyateKhakiInfoForBarAvord.FromKM = FromKM.ToString("D6");
        //AmalyateKhakiInfoForBarAvord.ToKM = ToKM.ToString("D6");

        clsAmalyateKhakiInfoForBarAvord? currentAmalyateKhakiInfoForBarAvord = _context.AmalyateKhakiInfoForBarAvords.FirstOrDefault(x => x.ID == KMKhakRiziId);

        if (currentAmalyateKhakiInfoForBarAvord != null)
        {
            _context.Entry(currentAmalyateKhakiInfoForBarAvord).CurrentValues.SetValues(new
            {
                FromKM = FromKM.ToString("D6"),
                ToKM = ToKM.ToString("D6"),
            });
        }

        //if (AmalyateKhakiInfoForBarAvord.Update(KMKhakRiziId))
        //{
        var varAmalyateKhakiInfoForBarAvordDetails = _context.AmalyateKhakiInfoForBarAvordDetailses.Where(x => x.AmalyateKhakiInfoForBarAvordId == KMKhakRiziId).ToList();
        if (varAmalyateKhakiInfoForBarAvordDetails.Count != 0)
        {
            _context.AmalyateKhakiInfoForBarAvordDetailses.RemoveRange(varAmalyateKhakiInfoForBarAvordDetails);
            //_context.SaveChanges();
        }
        //clsAmalyateKhakiInfoForBarAvordDetails.DelWithParameter("AmalyateKhakiInfoForBarAvordId=" + KMKhakRiziId);
        var varAmalyateKhakiInfoForBarAvordMore = _context.AmalyateKhakiInfoForBarAvordMores.Where(x => x.AmalyateKhakiInfoForBarAvordId == KMKhakRiziId).ToList();
        if (varAmalyateKhakiInfoForBarAvordMore.Count != 0)
        {
            _context.AmalyateKhakiInfoForBarAvordMores.RemoveRange(varAmalyateKhakiInfoForBarAvordMore);
            //rmsContext.SaveChanges();
        }
        //clsAmalyateKhakiInfoForBarAvord.DelMoreWithParameter("AmalyateKhakiInfoForBarAvordId=" + KMKhakRiziId);
        /////////////
        /////////////
        ///////////

        decimal dDarsadKRDDaneh = decimal.Parse(DarsadKRDDaneh.Trim());
        decimal dDarsadKRRDaneh = decimal.Parse(DarsadKRRDaneh.Trim());
        decimal dHajmBetween0To30 = decimal.Parse(HajmBetween0To30.Trim());
        decimal dHajmBetween30To100 = decimal.Parse(HajmBetween30To100.Trim());
        decimal dHajmBetweenTo100 = decimal.Parse(HajmBetweenTo100.Trim());

        clsAmalyateKhakiInfoForBarAvordMore AmalyateKhakiInfoForBarAvordMore = new clsAmalyateKhakiInfoForBarAvordMore();
        AmalyateKhakiInfoForBarAvordMore.AmalyateKhakiInfoForBarAvordId = KMKhakRiziId;

        AmalyateKhakiInfoForBarAvordMore.Value = radioNoeRahKhakRizi;
        AmalyateKhakiInfoForBarAvordMore.Name = "radioNoeRahKhakRizi";
        _context.AmalyateKhakiInfoForBarAvordMores.Add(AmalyateKhakiInfoForBarAvordMore);
        //rmsContext.SaveChanges();
        //AmalyateKhakiInfoForBarAvord.SaveMore(KMKhakRiziId);
        ///////////
        AmalyateKhakiInfoForBarAvordMore = new clsAmalyateKhakiInfoForBarAvordMore();
        AmalyateKhakiInfoForBarAvordMore.AmalyateKhakiInfoForBarAvordId = KMKhakRiziId;
        AmalyateKhakiInfoForBarAvordMore.Value = dDarsadKRDDaneh;
        AmalyateKhakiInfoForBarAvordMore.Name = "DarsadKRDDaneh";
        _context.AmalyateKhakiInfoForBarAvordMores.Add(AmalyateKhakiInfoForBarAvordMore);
        //rmsContext.SaveChanges();
        //AmalyateKhakiInfoForBarAvord.Value = dDarsadKRDDaneh;
        //AmalyateKhakiInfoForBarAvord.Name = "DarsadKRDDaneh";
        //AmalyateKhakiInfoForBarAvord.SaveMore(KMKhakRiziId);
        ///////////
        AmalyateKhakiInfoForBarAvordMore = new clsAmalyateKhakiInfoForBarAvordMore();
        AmalyateKhakiInfoForBarAvordMore.AmalyateKhakiInfoForBarAvordId = KMKhakRiziId;
        AmalyateKhakiInfoForBarAvordMore.Value = dDarsadKRRDaneh;
        AmalyateKhakiInfoForBarAvordMore.Name = "DarsadKRRDaneh";
        _context.AmalyateKhakiInfoForBarAvordMores.Add(AmalyateKhakiInfoForBarAvordMore);
        //rmsContext.SaveChanges();

        //AmalyateKhakiInfoForBarAvord.Value = dDarsadKRRDaneh;
        //AmalyateKhakiInfoForBarAvord.Name = "DarsadKRRDaneh";
        //AmalyateKhakiInfoForBarAvord.SaveMore(KMKhakRiziId);
        ///////////
        AmalyateKhakiInfoForBarAvordMore = new clsAmalyateKhakiInfoForBarAvordMore();
        AmalyateKhakiInfoForBarAvordMore.AmalyateKhakiInfoForBarAvordId = KMKhakRiziId;
        AmalyateKhakiInfoForBarAvordMore.Value = dHajmBetween0To30;
        AmalyateKhakiInfoForBarAvordMore.Name = "HajmBetween0To30";
        _context.AmalyateKhakiInfoForBarAvordMores.Add(AmalyateKhakiInfoForBarAvordMore);
        //rmsContext.SaveChanges();

        //AmalyateKhakiInfoForBarAvord.Value = dHajmBetween0To30;
        //AmalyateKhakiInfoForBarAvord.Name = "HajmBetween0To30";
        //AmalyateKhakiInfoForBarAvord.SaveMore(KMKhakRiziId);
        ///////////
        AmalyateKhakiInfoForBarAvordMore = new clsAmalyateKhakiInfoForBarAvordMore();
        AmalyateKhakiInfoForBarAvordMore.AmalyateKhakiInfoForBarAvordId = KMKhakRiziId;
        AmalyateKhakiInfoForBarAvordMore.Value = dHajmBetween30To100;
        AmalyateKhakiInfoForBarAvordMore.Name = "HajmBetween30To100";
        _context.AmalyateKhakiInfoForBarAvordMores.Add(AmalyateKhakiInfoForBarAvordMore);
        //rmsContext.SaveChanges();

        //AmalyateKhakiInfoForBarAvord.Value = dHajmBetween30To100;
        //AmalyateKhakiInfoForBarAvord.Name = "HajmBetween30To100";
        //AmalyateKhakiInfoForBarAvord.SaveMore(KMKhakRiziId);
        ///////////
        AmalyateKhakiInfoForBarAvordMore = new clsAmalyateKhakiInfoForBarAvordMore();
        AmalyateKhakiInfoForBarAvordMore.AmalyateKhakiInfoForBarAvordId = KMKhakRiziId;
        AmalyateKhakiInfoForBarAvordMore.Value = dHajmBetweenTo100;
        AmalyateKhakiInfoForBarAvordMore.Name = "HajmBetweenTo100";
        _context.AmalyateKhakiInfoForBarAvordMores.Add(AmalyateKhakiInfoForBarAvordMore);
        //rmsContext.SaveChanges();

        clsAmalyateKhakiInfoForBarAvordDetails AmalyateKhakiInfoForBarAvordDetails = new clsAmalyateKhakiInfoForBarAvordDetails();
        AmalyateKhakiInfoForBarAvordDetails.AmalyateKhakiInfoForBarAvordId = KMKhakRiziId;
        //بررسی شود
        //AmalyateKhakiInfoForBarAvordDetails.Type = 1;
        Guid AmalyateKhakiInfoForBarAvordDetailsId = AmalyateKhakiInfoForBarAvordDetails.ID;
        _context.AmalyateKhakiInfoForBarAvordDetailses.Add(AmalyateKhakiInfoForBarAvordDetails);
        //rmsContext.SaveChanges();
        //AmalyateKhakiInfoForBarAvord.Value = dHajmBetweenTo100;
        //AmalyateKhakiInfoForBarAvord.Name = "HajmBetweenTo100";
        //AmalyateKhakiInfoForBarAvord.SaveMore(KMKhakRiziId);
        /////////
        //long AmalyateKhakiInfoForBarAvordDetailsId = AmalyateKhakiInfoForBarAvordDetails.Save();


        //بررسی شود
        //tblAmalyateKhakiInfoForBarAvordDetailsEzafeBaha AmalyateKhakiInfoForBarAvordDetailsEzafeBaha = new tblAmalyateKhakiInfoForBarAvordDetailsEzafeBaha();
        //AmalyateKhakiInfoForBarAvordDetailsEzafeBaha._Name = "KREzafeBahaKhakMosalah";
        //AmalyateKhakiInfoForBarAvordDetailsEzafeBaha._Value = EzafeBahaKRKhakMosalah;
        //AmalyateKhakiInfoForBarAvordDetailsEzafeBaha._AmalyateKhakiInfoForBarAvordDetailsId = AmalyateKhakiInfoForBarAvordDetailsId;
        //rmsContext.AmalyateKhakiInfoForBarAvordDetailsEzafeBahas.Add(AmalyateKhakiInfoForBarAvordDetailsEzafeBaha);
        //rmsContext.SaveChanges();

        //AmalyateKhakiInfoForBarAvordDetails.SaveEzafeBaha(AmalyateKhakiInfoForBarAvordDetailsId);
        Guid gCheckId = new Guid();
        //if (AmalyateKhakiInfoForBarAvordDetailsId > 0)
        if (AmalyateKhakiInfoForBarAvordDetailsId != gCheckId)
        {
            string[] KhakRiziInfoDetailsSplit = KhakRiziInfoDetails.Split('$');
            for (int i = 0; i < KhakRiziInfoDetailsSplit.Length - 1; i++)
            {
                string[] KhakRiziInfoDetailsSplitSplit = KhakRiziInfoDetailsSplit[i].Split('_');
                AmalyateKhakiInfoForBarAvordMore = new clsAmalyateKhakiInfoForBarAvordMore();
                AmalyateKhakiInfoForBarAvordMore.AmalyateKhakiInfoForBarAvordId = KMKhakRiziId;
                AmalyateKhakiInfoForBarAvordMore.Name = KhakRiziInfoDetailsSplitSplit[0];
                AmalyateKhakiInfoForBarAvordMore.Value = decimal.Parse(KhakRiziInfoDetailsSplitSplit[1]);
                _context.AmalyateKhakiInfoForBarAvordMores.Add(AmalyateKhakiInfoForBarAvordMore);
                //rmsContext.SaveChanges();
                //AmalyateKhakiInfoForBarAvordDetails.Name = KhakRiziInfoDetailsSplitSplit[0];
                //AmalyateKhakiInfoForBarAvordDetails.Value = decimal.Parse(KhakRiziInfoDetailsSplitSplit[1]);
                //AmalyateKhakiInfoForBarAvordDetails.SaveMore(AmalyateKhakiInfoForBarAvordDetailsId);
            }
            /////////
            //string[] KhakRiziInfoDetailsCheckBoxSplit = KhakRiziInfoDetailsCheckBox.Split('$');
            //for (int i = 0; i < KhakRiziInfoDetailsCheckBoxSplit.Length - 1; i++)
            //{
            //    string[] KhakRiziInfoDetailsCheckBoxSplitSplit = KhakRiziInfoDetailsCheckBoxSplit[i].Split('_');
            //    AmalyateKhakiInfoForBarAvordDetailsEzafeBaha = new tblAmalyateKhakiInfoForBarAvordDetailsEzafeBaha();
            //    AmalyateKhakiInfoForBarAvordDetailsEzafeBaha._Name = KhakRiziInfoDetailsCheckBoxSplitSplit[0];
            //    AmalyateKhakiInfoForBarAvordDetailsEzafeBaha._Value = KhakRiziInfoDetailsCheckBoxSplitSplit[1].Trim() == "true" ? true : false;
            //    AmalyateKhakiInfoForBarAvordDetailsEzafeBaha._AmalyateKhakiInfoForBarAvordDetailsId = AmalyateKhakiInfoForBarAvordDetailsId;
            //    rmsContext.AmalyateKhakiInfoForBarAvordDetailsEzafeBahas.Add(AmalyateKhakiInfoForBarAvordDetailsEzafeBaha);
            //    rmsContext.SaveChanges();
            //    //AmalyateKhakiInfoForBarAvordDetails.Name = KhakRiziInfoDetailsCheckBoxSplitSplit[0];
            //    //AmalyateKhakiInfoForBarAvordDetails.boolValue = KhakRiziInfoDetailsCheckBoxSplitSplit[1].Trim() == "true" ? true : false;
            //    //AmalyateKhakiInfoForBarAvordDetails.SaveEzafeBaha(AmalyateKhakiInfoForBarAvordDetailsId);
            //}
            //}
            /////////////

            //tblRizMetreUser.Delete("_BarAvordUserId='" + BarAvordUserId + "' and (SUBSTRING(ltrim(rtrim(_Type)),1,2) in('42','43')) and SUBSTRING(ltrim(rtrim(_Type)),4,3)='" + KMNum.ToString("D3") + "'");
            //AmalyateKhakiInfoForBarAvord.SaveRizMetreKhakRizi(BarAvordUserId, KMKhakRiziId, KMNum, FromKM.ToString("D6")
            //    , ToKM.ToString("D6"), radioNoeRahKhakRizi, dDarsadKRDDaneh, dDarsadKRRDaneh, dHajmBetween0To30, dHajmBetween30To100
            //    , dHajmBetweenTo100, EzafeBahaKRKhakMosalah);

            KhakRiziCommon riziCommon = new KhakRiziCommon();


            RequestSaveRizMetreBestarKhakRiziDto requestSaveRizMetreBestarKhakRizi = new RequestSaveRizMetreBestarKhakRiziDto
            {
                BarAvordUserID = BarAvordUserId,
                KMId = KMKhakRiziId,
                KMNum = KMNum,
                KMS = FromKM.ToString("D6"),
                KME = ToKM.ToString("D6"),
                KhakRiziInfoDetails = KhakRiziInfoDetails,
                KhakRiziInfoDetailsCheckBox = KhakRiziInfoDetailsCheckBox,
            };
            riziCommon.SaveRizMetreBestarKhakRizi(requestSaveRizMetreBestarKhakRizi, _context);

            //AmalyateKhakiInfoForBarAvord.SaveRizMetreGharzeh(BarAvordUserId);

        }
        return new JsonResult("OK");
        //return "OK";
        //}
        //catch (Exception)
        //{
        //    return Json("NOK", JsonRequestBehavior.AllowGet);
        //    //return "NOK";
        //}
    }

}
