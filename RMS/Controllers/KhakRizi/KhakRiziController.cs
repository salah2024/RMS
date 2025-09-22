
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.AmalyateKhaki.Dto;
using RMS.Controllers.KhakRizi.Common;
using RMS.Controllers.KhakRizi.Dto;
using RMS.Controllers.KhakRizi.EnumKhakRizi;
using RMS.Controllers.Operation.Dto;
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

        DateTime Now = DateTime.Now;
        Guid BarAvordUserId = request.BarAvordUserId;
        string FromKM = request.FromKM.ToString("D6"); ;
        string ToKM = request.ToKM.ToString("D6"); ;
        EnumRoadType RoadTypeId = request.RoadTypeId;
        EnumNoeDaneBandi NoeDaneBandiId = request.NoeDaneBandiId;
        string? HajmKhakRiziValue = request.HajmKhakRiziValues;
        long Year = request.Year;

        int intKMNum = 1;
        clsKhakRiziBarAvord? currentKMNum = _context.KhakRiziBarAvords.FirstOrDefault(x => x.BarAvordId == BarAvordUserId);
        if (currentKMNum != null)
        {
            intKMNum = currentKMNum.KMNum + 1;
        }

        clsKhakRiziBarAvord khakRiziBarAvord = new clsKhakRiziBarAvord
        {
            BarAvordId = BarAvordUserId,
            FromKM = FromKM,
            ToKM = ToKM,
            KMNum = intKMNum,
            NoeDaneBandi = NoeDaneBandiId,
            NoeRah = RoadTypeId,
            NoeHajmKhakRizi_Value = HajmKhakRiziValue,
        };
        _context.KhakRiziBarAvords.Add(khakRiziBarAvord);


        List<clsKhakRiziDarsad> lstKhakRiziDarsad = _context.KhakRiziDarsads.Where(x => x.NoeRah == RoadTypeId && x.NoeDaneBandi == NoeDaneBandiId && x.Year == Year).ToList();

        List<HajmKhakRiziValueDto> lstHajmKhakRiziValue = new List<HajmKhakRiziValueDto>();
        if (HajmKhakRiziValue != null)
        {
            string[] HajmKhakRiziValueSplit = HajmKhakRiziValue.Split(",");
            foreach (var item in HajmKhakRiziValueSplit)
            {
                string[] strItem = item.Split("_");
                if (strItem[1].Trim() != "0")
                {
                    long HajmKhakRiziId = long.Parse(strItem[0].Trim());
                    decimal dValue = decimal.Parse(strItem[1].Trim());

                    lstHajmKhakRiziValue.Add(new HajmKhakRiziValueDto
                    {
                        Id = HajmKhakRiziId,
                        Value = dValue,
                    });
                }
            }
        }

        List<long> HajmKhakRiziIds = lstHajmKhakRiziValue.Select(x => x.Id).ToList();


        List<clsKhakRiziDarsad> lstKhakRiziDarsad1 = lstKhakRiziDarsad.Where(x => HajmKhakRiziIds.Contains(x.Id)).ToList();

        List<clsKhakRiziItem> lstKhakRiziItem = _context.KhakRiziItems.Where(x => x.Year == Year).ToList();


        long Shomareh = 1;
        clsRizMetreUsers? rizMetreUser = _context.RizMetreUserses.Include(x => x.FB).OrderByDescending(x => x.Shomareh).FirstOrDefault(x => x.FB.BarAvordId == BarAvordUserId);
        if (rizMetreUser != null)
        {
            Shomareh = rizMetreUser.Shomareh + 1;
        }




        foreach (var itemDarsad in lstKhakRiziDarsad1)
        {
            string ItemFBShomareh = "";
            foreach (var KhakRiziItem in lstKhakRiziItem)
            {
                string strCondition = KhakRiziItem.Condition.Trim();
                if (strCondition != "")
                {
                    string strConditionOp = strCondition.Replace("x", itemDarsad.Darsad.ToString().Trim());
                    StringToFormula StringToFormula = new StringToFormula();
                    bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                    if (blnCheck)
                    {
                        ItemFBShomareh = KhakRiziItem.ItemFBShomareh;
                        break;
                    }
                }
            }


            clsFB? FB = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordUserId && x.Shomareh == ItemFBShomareh);
            Guid gFBId = new Guid();
            if (FB != null)
            {
                gFBId = FB.ID;
            }
            else
            {
                clsFB newFB = new clsFB
                {
                    BarAvordId = BarAvordUserId,
                    InsertDateTime = Now,
                    Shomareh = ItemFBShomareh
                };
                _context.FBs.Add(newFB);
                gFBId = newFB.ID;
            }

            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = Shomareh++;
            RizMetre.Sharh = "";
            RizMetre.Tedad = null;
            RizMetre.Tool = null;
            RizMetre.Arz = null;
            RizMetre.Ertefa = null;
            RizMetre.Vazn = null;
            RizMetre.Des = "کیلومتراژ " + FromKM + " تا " + ToKM;
            RizMetre.FBId = gFBId;
            RizMetre.OperationsOfHamlId = 1;
            RizMetre.Type = "1";
            RizMetre.ForItem = "";
            RizMetre.UseItem = "";

            RizMetre.MeghdarJoz = null;

            _context.RizMetreUserses.Add(RizMetre);

            clsKhakRiziBarAvordRizMetre KhakRiziBarAvordRizMetre
                = new clsKhakRiziBarAvordRizMetre
                {
                    KhakRiziBarAvordId = khakRiziBarAvord.ID,
                    RizMetreUserId = RizMetre.ID
                };
            _context.KhakRiziBarAvordRizMetres.Add(KhakRiziBarAvordRizMetre);
        }



        //KhakRiziCommon khakRiziCommon = new KhakRiziCommon();

        //khakRiziCommon.SaveRizMetreKhakRizi(requestSaveRizMetreKhakRizi, _context);

        //RequestSaveRizMetreBestarKhakRiziDto requestSaveRizMetreBestarKhakRizi = new RequestSaveRizMetreBestarKhakRiziDto
        //{
        //    BarAvordUserID = BarAvordUserId,
        //    KMId = Id,
        //    KMNum = intKMNum,
        //    KMS = FromKM.ToString("D6"),
        //    KME = ToKM.ToString("D6"),
        //    KhakRiziInfoDetails = KhakRiziInfoDetails,
        //    KhakRiziInfoDetailsCheckBox = KhakRiziInfoDetailsCheckBox
        //};

        //khakRiziCommon.SaveRizMetreBestarKhakRizi(requestSaveRizMetreBestarKhakRizi, _context);

        //khakRiziCommon.SaveRizMetreGharzeh(BarAvordUserId);

        return new JsonResult("OK_" + intKMNum);


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

    public JsonResult GetRizMetreForKhakRizi([FromBody] GetRizMetreForKhakRiziDto request)
    {
        Guid BarAvordId = request.BarAvordId;
        NoeFehrestBaha NoeFB = request.NoeFB;
        long Year = request.Year;

        List<clsKhakRiziBarAvord> lstKhakRiziBarAvord = _context.KhakRiziBarAvords.Where(x => x.BarAvordId == BarAvordId).ToList();
        List<Guid> lstKhakRiziBarAvordIds = lstKhakRiziBarAvord.Select(x => x.ID).ToList();

        List<clsKhakRiziBarAvordRizMetre> lstKhakRiziRizMetre =
            _context.KhakRiziBarAvordRizMetres.Where(x => lstKhakRiziBarAvordIds.Contains(x.KhakRiziBarAvordId)).ToList();

        List<ItemFBShomarehForGetAndShowAddItemsFieldsDto> lstItemFields = _context.ItemsFieldses.Where(x => x.NoeFB == NoeFB).Select(x => new ItemFBShomarehForGetAndShowAddItemsFieldsDto
        {
            Shomareh = x.ItemShomareh,
            FieldType = x.FieldType,
            Vahed = x.Vahed,
            IsEnteringValue = x.IsEnteringValue
        }).ToList();


        List<Guid> lstRizMetreIds = lstKhakRiziRizMetre.Select(x => x.RizMetreUserId).ToList();
        List<KhakRiziRizMetreDto> rizMetreUsers = _context.RizMetreUserses.Include(x => x.FB)
            .Where(x => lstRizMetreIds.Contains(x.ID)).Select(x => new KhakRiziRizMetreDto
            {
                Shomareh = x.Shomareh,
                ShomarehNew = x.ShomarehNew,
                Sharh = x.Sharh,
                Tedad = x.Tedad,
                Tool = x.Tool,
                Arz = x.Arz,
                Ertefa = x.Ertefa,
                Vazn = x.Vazn,
                MeghdarJoz = x.MeghdarJoz,
                Des = x.Des,
                FBId = x.FBId,
                ForItem = x.ForItem,
                Type = x.Type,
                UseItem = x.Type,
                ItemFBShomareh = x.FB.Shomareh
            }).ToList();

        List<string> lstItemFBShomareh = rizMetreUsers.Select(x => x.ItemFBShomareh).ToList();


        List<clsFehrestBaha> lstFehrestBahas = _context.FehrestBahas.Where(x => x.Sal == Year && x.NoeFB == NoeFB && lstItemFBShomareh.Contains(x.Shomareh.Trim())).ToList();
        List<ItemFBShomarehForGetAndShowAddItemsDto> lstItemFBShomarehForGet = new List<ItemFBShomarehForGetAndShowAddItemsDto>();

        List<ItemFBShomarehForGetAndShowAddItemsFieldsDto> ItemFields = new List<ItemFBShomarehForGetAndShowAddItemsFieldsDto>();

        foreach (var item in lstItemFBShomareh)
        {
            clsFehrestBaha fehrestBaha = lstFehrestBahas.First(x => x.Shomareh == item);

            ItemFields = lstItemFields.Where(x => x.Shomareh.Trim() == item).ToList();
            ItemFBShomarehForGetAndShowAddItemsDto ItemFBShomarehForGet = new ItemFBShomarehForGetAndShowAddItemsDto
            {
                ItemFBShomareh = item,
                Des = fehrestBaha.Sharh,
                ItemFields = ItemFields

            };
            lstItemFBShomarehForGet.Add(ItemFBShomarehForGet);
        }

        var result = new
        {
            lstItemFBShomarehForGet,
            rizMetreUsers
        };

        return new JsonResult(result);
    }

    public JsonResult GetExistKhakRizi([FromBody] RequestGetExistKhakRiziDto Request)
    {
        Guid BarAvordId = Request.BarAvordId;
        List<clsKhakRiziBarAvord> lstKhakRizi = _context.KhakRiziBarAvords.Where(x => x.BarAvordId == BarAvordId).ToList();

        return new JsonResult(lstKhakRizi);

    }
    public JsonResult GetDataForKhakRizi()
    {
        var ListRoadType = EnumExtensions.GetEnumList<EnumRoadType>();
        var ListNoeDaneBandi = EnumExtensions.GetEnumList<EnumNoeDaneBandi>();
        var ListHajmKhakRizi = EnumExtensions.GetEnumList<EnumHajmKhakRizi>();

        var result = new
        {
            ListRoadType,
            ListNoeDaneBandi,
            ListHajmKhakRizi
        };

        return new JsonResult(result);
    }

}
