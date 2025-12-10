
using System;
using System.Data;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
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


    public JsonResult SaveKhakRiziInfoForBarAvord([FromBody] RequestSaveKhakRiziInfoForBarAvordDto request)
    {
        //try
        //{

        DateTime Now = DateTime.Now;
        Guid BarAvordUserId = request.BarAvordUserId;
        NoeFehrestBaha NoeFBId = request.NoeFBId;
        string FromKM = request.FromKM.ToString("D6");
        string ToKM = request.ToKM.ToString("D6");
        EnumRoadType RoadTypeId = request.RoadTypeId;
        EnumNoeDaneBandi NoeDaneBandiId = request.NoeDaneBandiId;
        string? HajmKhakRiziValue = request.HajmKhakRiziValues;
        long Year = request.Year;

        int intKMNum = 1;
        clsKhakRiziBarAvord? currentKMNum = _context.KhakRiziBarAvords.OrderByDescending(x => x.KMNum).FirstOrDefault(x => x.BarAvordId == BarAvordUserId);
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
                if (item.Trim() != "")
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

        }

        //List<long> HajmKhakRiziIds = lstHajmKhakRiziValue.Select(x => x.Id).ToList();

        List<EnumHajmKhakRizi> hajmKhakRiziEnums =
            lstHajmKhakRiziValue
        .Where(x => Enum.IsDefined(typeof(EnumHajmKhakRizi), (int)x.Id))
        .Select(x => (EnumHajmKhakRizi)(int)x.Id)
        .ToList();

        List<clsKhakRiziDarsad> lstKhakRiziDarsad1 = lstKhakRiziDarsad.Where(x => hajmKhakRiziEnums.Contains(x.NoeHajmKhakRizi)).ToList();


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

            decimal? MeghdarJoz = null;
            foreach (var item in lstHajmKhakRiziValue)
            {
                if ((EnumHajmKhakRizi)item.Id == itemDarsad.NoeHajmKhakRizi)
                {
                    MeghdarJoz = item.Value;
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
                    Shomareh = ItemFBShomareh,
                    NoeFBId = NoeFBId
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

            RizMetre.MeghdarJoz = MeghdarJoz;

            _context.RizMetreUserses.Add(RizMetre);

            clsKhakRiziBarAvordRizMetre KhakRiziBarAvordRizMetre
                = new clsKhakRiziBarAvordRizMetre
                {
                    KhakRiziBarAvordId = khakRiziBarAvord.ID,
                    RizMetreUserId = RizMetre.ID
                };
            _context.KhakRiziBarAvordRizMetres.Add(KhakRiziBarAvordRizMetre);
        }

        _context.SaveChanges();


        return new JsonResult("OK_" + intKMNum);
    }

    public JsonResult UpdateKhakRiziInfoForBarAvord([FromBody] requestUpdateKhakRiziInfoForBarAvordDto request)
    {
        DateTime Now = DateTime.Now;

        int Num = request.Num;
        Guid BarAvordUserId = request.BarAvordUserId;
        NoeFehrestBaha NoeFBId = request.NoeFBId;
        string FromKM = request.FromKM.ToString("D6");
        string ToKM = request.ToKM.ToString("D6");
        EnumRoadType RoadTypeId = request.RoadTypeId;
        EnumNoeDaneBandi NoeDaneBandiId = request.NoeDaneBandiId;
        string? HajmKhakRiziValue = request.HajmKhakRiziValues;
        long Year = request.Year;

        clsKhakRiziBarAvord? currentKhakRiziBarAvord = _context.KhakRiziBarAvords.Where(x => x.BarAvordId == BarAvordUserId && x.KMNum == Num).FirstOrDefault();
        if (currentKhakRiziBarAvord != null)
        {
            _context.Entry(currentKhakRiziBarAvord).CurrentValues.SetValues(new
            {
                FromKM = FromKM,
                ToKM = ToKM,
                NoeRah = RoadTypeId,
                NoeDaneBandi = NoeDaneBandiId,
                NoeHajmKhakRizi_Value = HajmKhakRiziValue,
            });

            _context.SaveChanges();

            ////قبلی های ثبت شده بایستی حذف گردند
            ///

            List<clsKhakRiziBarAvordRizMetre> lstKhakRiziBarAvordRizMetre = _context.KhakRiziBarAvordRizMetres.Where(x => x.KhakRiziBarAvordId == currentKhakRiziBarAvord.ID).ToList();
            List<Guid> lstKhakRiziId = lstKhakRiziBarAvordRizMetre.Select(x => x.RizMetreUserId).ToList();
            _context.KhakRiziBarAvordRizMetres.RemoveRange(lstKhakRiziBarAvordRizMetre);
            List<clsRizMetreUsers> lstRiziMetre = _context.RizMetreUserses.Where(x => lstKhakRiziId.Contains(x.ID)).ToList();
            _context.RizMetreUserses.RemoveRange(lstRiziMetre);


            //int intKMNum = 1;
            //clsKhakRiziBarAvord? currentKMNum = _context.KhakRiziBarAvords.FirstOrDefault(x => x.BarAvordId == BarAvordUserId);
            //if (currentKMNum != null)
            //{
            //    intKMNum = currentKMNum.KMNum + 1;
            //}

            //clsKhakRiziBarAvord khakRiziBarAvord = new clsKhakRiziBarAvord
            //{
            //    BarAvordId = BarAvordUserId,
            //    FromKM = FromKM,
            //    ToKM = ToKM,
            //    KMNum = intKMNum,
            //    NoeDaneBandi = NoeDaneBandiId,
            //    NoeRah = RoadTypeId,
            //    NoeHajmKhakRizi_Value = HajmKhakRiziValue,
            //};
            //_context.KhakRiziBarAvords.Add(khakRiziBarAvord);


            List<clsKhakRiziDarsad> lstKhakRiziDarsad = _context.KhakRiziDarsads.Where(x => x.NoeRah == RoadTypeId && x.NoeDaneBandi == NoeDaneBandiId && x.Year == Year).ToList();

            List<HajmKhakRiziValueDto> lstHajmKhakRiziValue = new List<HajmKhakRiziValueDto>();
            if (HajmKhakRiziValue != null)
            {
                string[] HajmKhakRiziValueSplit = HajmKhakRiziValue.Split(",");
                foreach (var item in HajmKhakRiziValueSplit)
                {
                    if (item.Trim() != "")
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

            }

            //List<long> HajmKhakRiziIds = lstHajmKhakRiziValue.Select(x => x.Id).ToList();

            List<EnumHajmKhakRizi> hajmKhakRiziEnums =
                lstHajmKhakRiziValue
            .Where(x => Enum.IsDefined(typeof(EnumHajmKhakRizi), (int)x.Id))
            .Select(x => (EnumHajmKhakRizi)(int)x.Id)
            .ToList();

            List<clsKhakRiziDarsad> lstKhakRiziDarsad1 = lstKhakRiziDarsad.Where(x => hajmKhakRiziEnums.Contains(x.NoeHajmKhakRizi)).ToList();


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

                decimal? MeghdarJoz = null;
                foreach (var item in lstHajmKhakRiziValue)
                {
                    if ((EnumHajmKhakRizi)item.Id == itemDarsad.NoeHajmKhakRizi)
                    {
                        MeghdarJoz = item.Value;
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
                        Shomareh = ItemFBShomareh,
                        NoeFBId = NoeFBId
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

                RizMetre.MeghdarJoz = MeghdarJoz;

                _context.RizMetreUserses.Add(RizMetre);

                clsKhakRiziBarAvordRizMetre KhakRiziBarAvordRizMetre
                    = new clsKhakRiziBarAvordRizMetre
                    {
                        KhakRiziBarAvordId = currentKhakRiziBarAvord.ID,
                        RizMetreUserId = RizMetre.ID
                    };
                _context.KhakRiziBarAvordRizMetres.Add(KhakRiziBarAvordRizMetre);
            }
        }
        _context.SaveChanges();

        ///بررسی اضافه بهای ثبت شده قبلی
        ///


        clsKhakRiziBarAvord? KhakRiziBarAvord = _context.KhakRiziBarAvords.Where(x => x.BarAvordId == BarAvordUserId && x.KMNum == Num).FirstOrDefault();

        if (KhakRiziBarAvord != null)
        {
            List<resultKhakRiziEzafeBahaBarAvordDto> lstKhakRiziEzafeBahaBarAvord =
                _context.KhakRiziEzafeBahas.Include(x => x.EzafeBahaKhakRizi).Where(x => x.KhakRiziBarAvordId == KhakRiziBarAvord.ID)
                    .Select(x => new resultKhakRiziEzafeBahaBarAvordDto
                    {
                        ConditionContextId = x.EzafeBahaKhakRizi.ConditionContextId,
                    }).ToList();

            //List<clsEzafeBahaKhakRizi> lstEzafeBahaKhakRizi = _context.EzafeBahaKhakRizis
            //    .Where(x => x.Year == request.Year).ToList();

            foreach (var EzafeBahaKhakRizi in lstKhakRiziEzafeBahaBarAvord)
            {
                requestSaveEBKhakRiziDto requestSaveEBKhakRizi = new requestSaveEBKhakRiziDto
                {
                    BarAvordUserId = BarAvordUserId,
                    ConditionContextId = EzafeBahaKhakRizi.ConditionContextId,
                    Year = Year,
                    Num = Num
                };
                KhakRiziCommon.SaveEzafeBahaKhakRizi(requestSaveEBKhakRizi, _context);
            }
        }
        _context.SaveChanges();

        return new JsonResult("OK");
    }

    public JsonResult GetRizMetreForKhakRizi([FromBody] GetRizMetreForKhakRiziDto request)
    {
        long Num = request.Num;
        Guid BarAvordId = request.BarAvordId;
        NoeFehrestBaha NoeFB = request.NoeFB;
        long Year = request.Year;

        List<clsKhakRiziBarAvord> lstKhakRiziBarAvord = _context.KhakRiziBarAvords.Where(x => x.BarAvordId == BarAvordId && x.KMNum == Num).ToList();
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

            // بررسی وجود آیتم تکراری
            bool isDuplicate = lstItemFBShomarehForGet.Any(x => x.ItemFBShomareh == item);
            if (!isDuplicate)
            {
                ItemFBShomarehForGetAndShowAddItemsDto ItemFBShomarehForGet = new ItemFBShomarehForGetAndShowAddItemsDto
                {
                    ItemFBShomareh = item,
                    Des = fehrestBaha.Sharh,
                    ItemFields = ItemFields
                };

                lstItemFBShomarehForGet.Add(ItemFBShomarehForGet);
            }
        }


        var result = new
        {
            lstItemFBShomarehForGet,
            rizMetreUsers
        };

        return new JsonResult(result);
    }


    static string Normalize(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return "000+000";

        // حذف پیشوند اختیاری
        input = input.Trim();
        if (input.StartsWith("fromKM=", StringComparison.OrdinalIgnoreCase))
            input = input.Substring("fromKM=".Length);

        // الگو: عدد یا عدد+عدد
        var m = Regex.Match(input, @"^\s*(\d+)\s*(?:\+\s*(\d+)\s*)?$");
        if (!m.Success)
            return "000+000"; // یا می‌تونی همون ورودی رو برگردونی

        try
        {
            long totalMeters;
            if (m.Groups[2].Success)
            {
                // ورودی به صورت km+m بوده
                long km = long.Parse(m.Groups[1].Value);
                long ms = long.Parse(m.Groups[2].Value);
                totalMeters = km * 1000 + ms;
            }
            else
            {
                // ورودی فقط متر بوده
                totalMeters = long.Parse(m.Groups[1].Value);
            }

            if (totalMeters < 0) totalMeters = 0;

            long kmN = totalMeters / 1000;
            long mN = totalMeters % 1000;

            // km حداقل 3 رقم (بیشتر هم لازم شد خودش بزرگ‌تر می‌شود)
            // m دقیقا 3 رقم
            string kmStr = kmN.ToString("000");
            string mStr = mN.ToString("000");

            return $"{kmStr}+{mStr}";
        }
        catch
        {
            return "000+000";
        }
    }
    public JsonResult GetExistKhakRizi([FromBody] RequestGetExistKhakRiziDto Request)
    {
        Guid BarAvordId = Request.BarAvordId;

        var ListRoadType = EnumExtensions.GetEnumList<EnumRoadType>();
        var ListNoeDaneBandi = EnumExtensions.GetEnumList<EnumNoeDaneBandi>();
        var ListHajmKhakRizi = EnumExtensions.GetEnumList<EnumHajmKhakRizi>();

        List<GetExistKhakRiziDto> lstKhakRizi = _context.KhakRiziBarAvords.Where(x => x.BarAvordId == BarAvordId).Select(x => new GetExistKhakRiziDto
        {
            KhakRiziID = x.ID,
            BarAvordId = x.BarAvordId,
            FromKM = x.FromKM,
            KMNum = x.KMNum,
            NoeDaneBandi = x.NoeDaneBandi,
            ToKM = x.ToKM,
            NoeRah = x.NoeRah,
            NoeHajmKhakRizi_Value = x.NoeHajmKhakRizi_Value,
        }).ToList();

        foreach (var item in lstKhakRizi)
        {
            item.FromKMSplit = Normalize(item.FromKM);
            item.ToKMSplit = Normalize(item.ToKM);
        }


        var result = new
        {
            ListRoadType,
            ListNoeDaneBandi,
            ListHajmKhakRizi,
            lstKhakRizi
        };

        return new JsonResult(result);

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

    public JsonResult GetEzafeBahaKhakRizi([FromBody] requestGetEzafeBahaKhakRiziDto request)
    {
        Guid BarAvordId = request.BarAvordId;
        clsKhakRiziBarAvord? KhakRiziBarAvord = _context.KhakRiziBarAvords.Where(x => x.BarAvordId == BarAvordId && x.KMNum == request.Num).FirstOrDefault();
        if (KhakRiziBarAvord == null)
        {
            return new JsonResult("NOK");
        }
        else
        {
            Guid KhakRiziBarAvordId = KhakRiziBarAvord.ID;
            List<resultKhakRiziEzafeBahaBarAvordDto> lstKhakRiziEzafeBahaBarAvord =
                _context.KhakRiziEzafeBahas.Include(x => x.EzafeBahaKhakRizi).Where(x => x.KhakRiziBarAvordId == KhakRiziBarAvordId)
                .Select(x => new resultKhakRiziEzafeBahaBarAvordDto
                {
                    ConditionContextId = x.EzafeBahaKhakRizi.ConditionContextId,
                }).ToList();

            List<GetEzafeBahaKhakRiziDto> EzafeBahaKhakRizi = _context.EzafeBahaKhakRizis.Include(x => x.ConditionContext).ThenInclude(x => x.ConditionGroup)
                .Where(x => x.Year == request.Year)
                .Select(x => new GetEzafeBahaKhakRiziDto
                {
                    Id = x.ConditionContext.Id,
                    GroupContext = x.ConditionContext.ConditionGroup.ConditionGroupName,
                    ConditionGroupId = x.ConditionContext.ConditionGroupId,
                    Context = x.ConditionContext.Context
                }).ToList();

            var result = new
            {
                EzafeBahaKhakRizi,
                lstKhakRiziEzafeBahaBarAvord
            };

            return new JsonResult(result);
        }
    }

    public JsonResult GetRizMetreEzafeBahaForKhakRizi([FromBody] RequestGetRizMetreEzafeBahaForKhakRiziDto request)
    {
        long ConditionContextId = request.ConditionContextId;
        Guid BarAvordId = request.BarAvordId;
        long Year = request.Year;
        NoeFehrestBaha NoeFB = request.NoeFB;
        int Num = request.Num;

        clsKhakRiziBarAvord? khakRiziBarAvord = _context.KhakRiziBarAvords.FirstOrDefault(x => x.KMNum == Num && x.BarAvordId == BarAvordId);
        if (khakRiziBarAvord != null)
        {
            clsEzafeBahaKhakRizi ezafeBahaKhakRizi = _context.EzafeBahaKhakRizis.First(x => x.ConditionContextId == ConditionContextId);

            List<ItemFBShomarehForGetAndShowAddItemsFieldsDto> lstItemFields = _context.ItemsFieldses
                .Where(x => x.NoeFB == NoeFB).Select(x => new ItemFBShomarehForGetAndShowAddItemsFieldsDto
                {
                    Shomareh = x.ItemShomareh,
                    FieldType = x.FieldType,
                    Vahed = x.Vahed,
                    IsEnteringValue = x.IsEnteringValue
                }).ToList();

            List<RizMetreForKhakRiziEzafeBahaBarAvordDto> lstkhakRiziEzafeBahaRizMetre =
                _context.KhakRiziEzafeBahaRizMetres.Include(x => x.KhakRiziEzafeBaha).ThenInclude(x => x.EzafeBahaKhakRizi)
                .Where(x => x.KhakRiziEzafeBaha.KhakRiziBarAvordId == khakRiziBarAvord.ID && x.KhakRiziEzafeBaha.EzafeBahaKhakRiziId == ezafeBahaKhakRizi.Id)
                .Select(x => new RizMetreForKhakRiziEzafeBahaBarAvordDto
                {
                    Id = x.RizMetreUser.ID,
                    Shomareh = x.RizMetreUser.Shomareh,
                    Arz = x.RizMetreUser.Arz,
                    Des = x.RizMetreUser.Des,
                    Ertefa = x.RizMetreUser.Ertefa,
                    FBId = x.RizMetreUser.FBId,
                    ForItem = x.RizMetreUser.ForItem,
                    MeghdarJoz = x.RizMetreUser.MeghdarJoz,
                    Sharh = x.RizMetreUser.Sharh,
                    Tedad = x.RizMetreUser.Tedad,
                    Tool = x.RizMetreUser.Tool,
                    Type = x.RizMetreUser.Type,
                    Vazn = x.RizMetreUser.Vazn,
                    ItemFBShomareh = x.RizMetreUser.FB.Shomareh,
                    hasDelButton = x.KhakRiziEzafeBaha.EzafeBahaKhakRizi.hasDelButton,
                    hasEditButton = x.KhakRiziEzafeBaha.EzafeBahaKhakRizi.hasEditButton,
                }).ToList();

            List<string> lstItemFBShomareh = lstkhakRiziEzafeBahaRizMetre.Select(x => x.ItemFBShomareh).ToList();


            List<clsFehrestBaha> lstFehrestBahas = _context.FehrestBahas.Where(x => x.Sal == Year && x.NoeFB == NoeFB && lstItemFBShomareh.Select(x => x.Substring(0, 6)).Contains(x.Shomareh.Substring(0, 6).Trim())).ToList();
            List<ItemFBShomarehForGetAndShowAddItemsDto> lstItemFBShomarehForGet = new List<ItemFBShomarehForGetAndShowAddItemsDto>();

            List<ItemFBShomarehForGetAndShowAddItemsFieldsDto> ItemFields = new List<ItemFBShomarehForGetAndShowAddItemsFieldsDto>();

            foreach (var item in lstItemFBShomareh)
            {
                string newItem = item.Substring(0, 6);
                clsFehrestBaha fehrestBaha = lstFehrestBahas.First(x => x.Shomareh == newItem);

                ItemFields = lstItemFields.Where(x => x.Shomareh.Trim() == newItem).ToList();
                foreach (var itemF in ItemFields)
                {
                    if (itemF.FieldType == 2 || itemF.FieldType == 3)
                        itemF.IsEnteringValue = true;
                }

                // بررسی تکراری بودن قبل از افزودن
                bool isDuplicate = lstItemFBShomarehForGet.Any(x => x.ItemFBShomareh == item);
                if (!isDuplicate)
                {
                    ItemFBShomarehForGetAndShowAddItemsDto ItemFBShomarehForGet = new ItemFBShomarehForGetAndShowAddItemsDto
                    {
                        ItemFBShomareh = item,
                        Des = fehrestBaha.Sharh,
                        ItemFields = ItemFields
                    };
                    lstItemFBShomarehForGet.Add(ItemFBShomarehForGet);
                }
            }


            var result = new
            {
                lstItemFBShomarehForGet,
                lstkhakRiziEzafeBahaRizMetre
            };

            return new JsonResult(result);
        }
        else
            return new JsonResult("NOK");
    }

    public JsonResult SaveEBKhakRizi([FromBody] requestSaveEBKhakRiziDto request)
    {
        string Result = KhakRiziCommon.SaveEzafeBahaKhakRizi(request, _context);

        if (Result == "NOK")
        {
            return new JsonResult("NOK");
        }
        else
        {
            _context.SaveChanges();
            return new JsonResult("OK");
        }
    }

    [HttpPost]
    public JsonResult UpdateRizMetreKhakRiziEzafeBaha([FromBody] UpdateRizMetreAKhEzafeBaha request)
    {
        try
        {

            Guid Id = request.Id;

            clsRizMetreUsers? clsRizMetreUsers1 = context.RizMetreUserses.Where(x => x.ID == Id).FirstOrDefault();


            string Sharh = request.Sharh;
            decimal? Tedad = request.Tedad;
            decimal? Tool = request.Tool;
            decimal? Arz = request.Arz;
            decimal? Ertefa = request.Ertefa;
            decimal? Vazn = request.Vazn;
            string? Des = request.Des;
            DastyarCommon DastyarCommon = new DastyarCommon(context);

            DataTable DtRizMetreUsers = new DataTable();
            var varRizMetreUsers = (from RizMetreUsers in context.RizMetreUserses
                                    join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
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
                                    }).Where(x => x.ID == Id).OrderBy(x => x.Shomareh).ToList();
            DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);


            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
            RizMetre.Tedad = Tedad;
            RizMetre.Tool = Tool;
            RizMetre.Arz = Arz;

            RizMetre.Ertefa = Ertefa;
            RizMetre.Vazn = Vazn;

            RizMetre.ID = Id;
            RizMetre.Sharh = Sharh.Trim();
            //RizMetre.Tedad = Tedad;
            //RizMetre.Tool = Tool;
            //RizMetre.Arz = Arz;
            //RizMetre.Ertefa = Ertefa;
            //RizMetre.Vazn = Vazn;
            RizMetre.Des = Des.Trim();
            RizMetre.Type = "2";

            decimal dMeghdarJoz = 0;
            if (Tedad == null && Tool == null && Arz == null && Ertefa == null && Vazn == null)
                dMeghdarJoz = 0;
            else
                dMeghdarJoz += (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                (Arz == null ? 1 : Arz.Value) * (Ertefa == null ? 1 : Ertefa.Value) * (Vazn == null ? 1 : Vazn.Value);

            RizMetre.MeghdarJoz = dMeghdarJoz;




            if (clsRizMetreUsers1 != null)
            {
                RizMetre.FBId = clsRizMetreUsers1.FBId;
                RizMetre.Shomareh = clsRizMetreUsers1.Shomareh;
                RizMetre.ShomarehNew = clsRizMetreUsers1.ShomarehNew;
                RizMetre.OperationsOfHamlId = 1;
                try
                {
                    context.Entry(clsRizMetreUsers1).CurrentValues.SetValues(RizMetre);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return new JsonResult("OK");
        }
        catch (Exception)
        {
            return new JsonResult("NOK");
        }
    }

    public JsonResult DeleteEBKhakRizi([FromBody] requestDeleteEBKhakRiziDto request)
    {
        long ConditionContextId = request.ConditionContextId;
        Guid BarAvordUserId = request.BarAvordUserId;
        int Num = request.Num;

        clsKhakRiziBarAvord? khakRiziBarAvord = _context.KhakRiziBarAvords.FirstOrDefault(x => x.BarAvordId == BarAvordUserId && x.KMNum == Num);

        if (khakRiziBarAvord != null)
        {
            clsEzafeBahaKhakRizi? ezafeBahaKhakRizi = _context.EzafeBahaKhakRizis.FirstOrDefault(x => x.ConditionContextId == ConditionContextId);

            if (ezafeBahaKhakRizi != null)
            {
                List<clsKhakRiziEzafeBaha> lstKhakRiziEzafeBahaBarAvord = _context.KhakRiziEzafeBahas.Include(x => x.KhakRiziEzafeBahaRizMetres)
                     .Where(x => x.KhakRiziBarAvordId == khakRiziBarAvord.ID && x.EzafeBahaKhakRiziId == ezafeBahaKhakRizi.Id)
                     .ToList();

                _context.KhakRiziEzafeBahas.RemoveRange(lstKhakRiziEzafeBahaBarAvord);
            }

            _context.SaveChanges();

            return new JsonResult("OK");
        }
        else
            return new JsonResult("NOK");


    }

}
