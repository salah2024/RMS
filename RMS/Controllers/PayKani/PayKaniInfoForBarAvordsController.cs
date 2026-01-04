using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Common;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;
using RMS.Controllers.AmalyateKhaki.Dto;
using RMS.Controllers.AmalyateKhaki.Common;
using Microsoft.AspNetCore.Authorization;
using RMS.Controllers.PayKani.Dto;

namespace RMS.Controllers.PayKani;
public class PayKaniInfoForBarAvordsController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;


    public JsonResult ReturnNoePayKani([FromBody] ReturnNoePayKaniDto request)
    {
        List<clsNoeKhakBardari> lstNoePayKani = _context.NoeKhakBardaris.Where(x => x.Year == request.Year && x.Type == request.Type).ToList();
        return new JsonResult(lstNoePayKani);
    }


    [HttpPost]
    public JsonResult SavePayKaniInfoForBarAvord([FromBody] SavePayKaniInfoForBarAvordDto request)
    {
        Guid BarAvordUserId = request.BarAvordUserId;
        NoeFehrestBaha NoeFBId = request.NoeFBId;

        int Type = request.Type;
        long FromKM = request.FromKM;
        long ToKM = request.ToKM;
        string HKB = request.HKB;

        DateTime Now = DateTime.Now;
        List<PayKaniInfoForBarAvordItemsDto> lstItems = request.lstItems;
        List<long> lstlngNoePayKani = lstItems.Select(x => x.NoeKhakBardari).ToList();

        List<clsNoeKhakBardari> lstNoeKB = _context.NoeKhakBardaris.Where(x => lstlngNoePayKani.Contains(x.Id)).ToList();

        decimal dHKB = decimal.Parse(HKB);

        clsPayKaniInfoForBarAvord PayKaniInfoForBarAvord = new clsPayKaniInfoForBarAvord();
        PayKaniInfoForBarAvord.BaravordUserId = BarAvordUserId;
        PayKaniInfoForBarAvord.Name = "";
        PayKaniInfoForBarAvord.Value = dHKB;
        PayKaniInfoForBarAvord.Type = Type;
        _context.PayKaniInfoForBarAvords.Add(PayKaniInfoForBarAvord);
        //_context.SaveChanges();
        Guid Id = PayKaniInfoForBarAvord.ID;

        ///////////
        clsPayKaniInfoForBarAvordMore PayKaniInfoForBarAvordMore = new clsPayKaniInfoForBarAvordMore();
        PayKaniInfoForBarAvordMore.PayKaniInfoForBarAvordId = Id;
        PayKaniInfoForBarAvordMore.Value = dHKB;
        PayKaniInfoForBarAvordMore.Name = "HKB";
        _context.PayKaniInfoForBarAvordMores.Add(PayKaniInfoForBarAvordMore);
        _context.SaveChanges();

        Guid guCheckID = new Guid();
        if (Id != guCheckID)
        {

            clsPayKaniInfoForBarAvordDetails PayKaniInfoForBarAvordDetails = new clsPayKaniInfoForBarAvordDetails();

            long Shomareh = 1;
            clsRizMetreUsers? rizMetreUser = _context.RizMetreUserses.Include(x => x.FB).OrderByDescending(x => x.Shomareh).FirstOrDefault(x => x.FB.BarAvordId == BarAvordUserId);
            if (rizMetreUser != null)
            {
                Shomareh = rizMetreUser.Shomareh + 1;
            }

            foreach (var item in lstItems)
            {
                PayKaniInfoForBarAvordDetails
                    = new clsPayKaniInfoForBarAvordDetails
                    {
                        PayKaniInfoForBarAvordId = Id,
                        NoeKhakBardariId = item.NoeKhakBardari,
                        Name = ""
                    };
                _context.PayKaniInfoForBarAvordDetailses.Add(PayKaniInfoForBarAvordDetails);

                Guid PayKaniInfoForBarAvordDetailsId = PayKaniInfoForBarAvordDetails.ID;

                if (PayKaniInfoForBarAvordDetailsId != guCheckID)
                {

                    clsPayKaniInfoForBarAvordDetailsMore PayKaniInfoForBarAvordDetailsMore = new clsPayKaniInfoForBarAvordDetailsMore();
                    PayKaniInfoForBarAvordDetailsMore.Value = decimal.Parse(item.DetailValue);
                    PayKaniInfoForBarAvordDetailsMore.Name = "KhDetail";
                    PayKaniInfoForBarAvordDetailsMore.PayKaniInfoForBarAvordDetailsId = PayKaniInfoForBarAvordDetailsId;
                    _context.PayKaniInfoForBarAvordDetailsMores.Add(PayKaniInfoForBarAvordDetailsMore);

                    PayKaniInfoForBarAvordDetailsMore = new clsPayKaniInfoForBarAvordDetailsMore();
                    PayKaniInfoForBarAvordDetailsMore.Value = decimal.Parse(item.DarsadValue);
                    PayKaniInfoForBarAvordDetailsMore.Name = "DarsadKhDetail";
                    PayKaniInfoForBarAvordDetailsMore.PayKaniInfoForBarAvordDetailsId = PayKaniInfoForBarAvordDetailsId;
                    _context.PayKaniInfoForBarAvordDetailsMores.Add(PayKaniInfoForBarAvordDetailsMore);

                    PayKaniInfoForBarAvordDetailsMore = new clsPayKaniInfoForBarAvordDetailsMore();
                    PayKaniInfoForBarAvordDetailsMore.Value = decimal.Parse(item.DetailValueOfReCycle);
                    PayKaniInfoForBarAvordDetailsMore.Name = "ReUseHajm";
                    PayKaniInfoForBarAvordDetailsMore.PayKaniInfoForBarAvordDetailsId = PayKaniInfoForBarAvordDetailsId;
                    _context.PayKaniInfoForBarAvordDetailsMores.Add(PayKaniInfoForBarAvordDetailsMore);

                    PayKaniInfoForBarAvordDetailsMore = new clsPayKaniInfoForBarAvordDetailsMore();
                    PayKaniInfoForBarAvordDetailsMore.Value = decimal.Parse(item.DarsadValueOfReCycle);
                    PayKaniInfoForBarAvordDetailsMore.Name = "DarsadReUseHajm";
                    PayKaniInfoForBarAvordDetailsMore.PayKaniInfoForBarAvordDetailsId = PayKaniInfoForBarAvordDetailsId;
                    _context.PayKaniInfoForBarAvordDetailsMores.Add(PayKaniInfoForBarAvordDetailsMore);

                    PayKaniInfoForBarAvordDetailsMore = new clsPayKaniInfoForBarAvordDetailsMore();
                    PayKaniInfoForBarAvordDetailsMore.Value = decimal.Parse(item.DetailValueOfVarize);
                    PayKaniInfoForBarAvordDetailsMore.Name = "Varizi";
                    PayKaniInfoForBarAvordDetailsMore.PayKaniInfoForBarAvordDetailsId = PayKaniInfoForBarAvordDetailsId;
                    _context.PayKaniInfoForBarAvordDetailsMores.Add(PayKaniInfoForBarAvordDetailsMore);

                    PayKaniInfoForBarAvordDetailsMore = new clsPayKaniInfoForBarAvordDetailsMore();
                    PayKaniInfoForBarAvordDetailsMore.Value = decimal.Parse(item.DarsadValueOfVarize);
                    PayKaniInfoForBarAvordDetailsMore.Name = "DarsadVarizi";
                    PayKaniInfoForBarAvordDetailsMore.PayKaniInfoForBarAvordDetailsId = PayKaniInfoForBarAvordDetailsId;
                    _context.PayKaniInfoForBarAvordDetailsMores.Add(PayKaniInfoForBarAvordDetailsMore);

                    PayKaniInfoForBarAvordDetailsMore = new clsPayKaniInfoForBarAvordDetailsMore();
                    PayKaniInfoForBarAvordDetailsMore.Value = decimal.Parse(item.DetailValueOfHaml);
                    PayKaniInfoForBarAvordDetailsMore.Name = "Haml";
                    PayKaniInfoForBarAvordDetailsMore.PayKaniInfoForBarAvordDetailsId = PayKaniInfoForBarAvordDetailsId;
                    _context.PayKaniInfoForBarAvordDetailsMores.Add(PayKaniInfoForBarAvordDetailsMore);

                    PayKaniInfoForBarAvordDetailsMore = new clsPayKaniInfoForBarAvordDetailsMore();
                    PayKaniInfoForBarAvordDetailsMore.Value = decimal.Parse(item.DarsadValueOfHaml);
                    PayKaniInfoForBarAvordDetailsMore.Name = "DarsadHaml";
                    PayKaniInfoForBarAvordDetailsMore.PayKaniInfoForBarAvordDetailsId = PayKaniInfoForBarAvordDetailsId;
                    _context.PayKaniInfoForBarAvordDetailsMores.Add(PayKaniInfoForBarAvordDetailsMore);
                }

                clsNoeKhakBardari? noeKhakBardari = lstNoeKB.FirstOrDefault(x => x.Id == item.NoeKhakBardari);
                if (noeKhakBardari != null)
                {
                    string strCurrentShomareh = noeKhakBardari.FBItemShomareh.Trim();
                    clsFB? FB = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordUserId && x.Shomareh == strCurrentShomareh);
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
                            Shomareh = strCurrentShomareh,
                            NoeFBId = NoeFBId
                        };
                        _context.FBs.Add(newFB);
                        gFBId = newFB.ID;
                    }

                    decimal Hajm = decimal.Parse(item.DetailValue);
                    if (Hajm != 0)
                    {
                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh++;
                        RizMetre.Sharh = noeKhakBardari.Title;
                        RizMetre.Tedad = null;
                        RizMetre.Tool = null;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = null;
                        RizMetre.Vazn = Hajm;
                        RizMetre.Des = "";
                        RizMetre.FBId = gFBId;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "1";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";

                        RizMetre.MeghdarJoz = Hajm;

                        ///محاسبه مقدار جزء
                        //decimal? dMeghdarJoz = null;
                        //if (dTedad == null && dTool == null && dArz == null && dErtefa == null && dVazn == null)
                        //    dMeghdarJoz = null;
                        //else
                        //    dMeghdarJoz = (dTedad == null ? 1 : dTedad.Value) * (dTool == null ? 1 : dTool.Value) *
                        //    (dArz == null ? 1 : dArz.Value) * (dErtefa == null ? 1 : dErtefa.Value) * (dVazn == null ? 1 : dVazn.Value);


                        _context.RizMetreUserses.Add(RizMetre);

                        clsPayKaniInfoForBarAvordDetailsRizMetre PayKaniInfoForBarAvordDetailsRizMetre
                            = new clsPayKaniInfoForBarAvordDetailsRizMetre
                            {
                                PayKaniInfoForBarAvordDetailsId = PayKaniInfoForBarAvordDetails.ID,
                                RizMetreUserId = RizMetre.ID
                            };
                        _context.PayKaniInfoForBarAvordDetailsRizMetres.Add(PayKaniInfoForBarAvordDetailsRizMetre);
                    }
                }
            }
        }

        _context.SaveChanges();
        return new JsonResult("OK_" + Id);
    }


    public JsonResult UpdatePayKaniInfoForBarAvord([FromBody] UpdatePayKaniInfoForBarAvordDto request)
    {
        try
        {
            Guid BarAvordUserId = request.BarAvordUserId;
            NoeFehrestBaha NoeFBId = request.NoeFBId;
            string HKB = request.HKB;
            Guid KMPayKaniId = request.KMPayKaniId;
            long Year = request.Year;

            DateTime Now = DateTime.Now;
            List<PayKaniInfoForBarAvordItemsForUpdateDto> lstItems = request.lstItems;
            List<long> lstlngNoeKhakBardari = lstItems.Select(x => x.NoeKhakBardari).ToList();

            List<clsNoeKhakBardari> lstNoeKB = _context.NoeKhakBardaris.Where(x => lstlngNoeKhakBardari.Contains(x.Id)).ToList();


            decimal dHKB = decimal.Parse(HKB);

            clsPayKaniInfoForBarAvord? currentPayKaniInfoForBarAvord = _context.PayKaniInfoForBarAvords.FirstOrDefault(x => x.BaravordUserId == BarAvordUserId && x.NoeFBId == NoeFBId);
            if (currentPayKaniInfoForBarAvord != null)
            {

                _context.Entry(currentPayKaniInfoForBarAvord).CurrentValues.SetValues(new
                {
                    Value = dHKB
                });

                List<clsPayKaniInfoForBarAvordDetails> lstPayKaniInfoForBarAvordDetails = _context.PayKaniInfoForBarAvordDetailses.Where(x => x.PayKaniInfoForBarAvordId == KMPayKaniId).ToList();

                List<Guid> lstPayKaniInfoForBarAvordDetailIds = lstPayKaniInfoForBarAvordDetails.Select(x => x.ID).ToList();
                List<clsPayKaniInfoForBarAvordDetailsRizMetre> lstPayKaniInfoForBarAvordDetailsRizMetres
                    = _context.PayKaniInfoForBarAvordDetailsRizMetres.Where(x => lstPayKaniInfoForBarAvordDetailIds.Contains(x.PayKaniInfoForBarAvordDetailsId)).ToList();

                if (lstPayKaniInfoForBarAvordDetails.Count != 0)
                {
                    _context.PayKaniInfoForBarAvordDetailses.RemoveRange(lstPayKaniInfoForBarAvordDetails);
                    _context.PayKaniInfoForBarAvordDetailsRizMetres.RemoveRange(lstPayKaniInfoForBarAvordDetailsRizMetres);
                }

                /////////////
                ///

                long Shomareh = 1;
                clsRizMetreUsers? rizMetreUser = _context.RizMetreUserses.Include(x => x.FB).OrderByDescending(x => x.Shomareh).FirstOrDefault(x => x.FB.BarAvordId == BarAvordUserId);
                if (rizMetreUser != null)
                {
                    Shomareh = rizMetreUser.Shomareh + 1;
                }


                foreach (var item in lstItems)
                {
                    clsPayKaniInfoForBarAvordDetails PayKaniInfoForBarAvordDetails = new clsPayKaniInfoForBarAvordDetails();
                    PayKaniInfoForBarAvordDetails.PayKaniInfoForBarAvordId = KMPayKaniId;
                    PayKaniInfoForBarAvordDetails.NoeKhakBardariId = item.NoeKhakBardari;
                    PayKaniInfoForBarAvordDetails.Name = "";
                    _context.PayKaniInfoForBarAvordDetailses.Add(PayKaniInfoForBarAvordDetails);
                    Guid PayKaniInfoForBarAvordDetailsId = PayKaniInfoForBarAvordDetails.ID;

                    Guid guCheck = new Guid();

                    if (PayKaniInfoForBarAvordDetailsId != guCheck)
                    {
                        decimal dDetailValue = decimal.Parse(item.DetailValue);
                        if (dDetailValue != 0)
                        {
                            clsPayKaniInfoForBarAvordDetailsMore PayKaniInfoForBarAvordDetailsMore = new clsPayKaniInfoForBarAvordDetailsMore();
                            PayKaniInfoForBarAvordDetailsMore.PayKaniInfoForBarAvordDetailsId = PayKaniInfoForBarAvordDetailsId;
                            PayKaniInfoForBarAvordDetailsMore.Value = dDetailValue;
                            PayKaniInfoForBarAvordDetailsMore.Name = "KhDetail";
                            _context.PayKaniInfoForBarAvordDetailsMores.Add(PayKaniInfoForBarAvordDetailsMore);

                            decimal dDarsadValue = decimal.Parse(item.DarsadValue);
                            PayKaniInfoForBarAvordDetailsMore = new clsPayKaniInfoForBarAvordDetailsMore();
                            PayKaniInfoForBarAvordDetailsMore.PayKaniInfoForBarAvordDetailsId = PayKaniInfoForBarAvordDetailsId;
                            PayKaniInfoForBarAvordDetailsMore.Value = dDarsadValue;
                            PayKaniInfoForBarAvordDetailsMore.Name = "DarsadKhDetail";
                            _context.PayKaniInfoForBarAvordDetailsMores.Add(PayKaniInfoForBarAvordDetailsMore);
                            //_context.SaveChanges();
                        }

                        decimal dDetailValueOfReCycle = decimal.Parse(item.DetailValueOfReCycle);
                        if (dDetailValueOfReCycle != 0)
                        {
                            clsPayKaniInfoForBarAvordDetailsMore PayKaniInfoForBarAvordDetailsMore = new clsPayKaniInfoForBarAvordDetailsMore();
                            PayKaniInfoForBarAvordDetailsMore.PayKaniInfoForBarAvordDetailsId = PayKaniInfoForBarAvordDetailsId;
                            PayKaniInfoForBarAvordDetailsMore.Value = dDetailValueOfReCycle;
                            PayKaniInfoForBarAvordDetailsMore.Name = "ReUseHajm";
                            _context.PayKaniInfoForBarAvordDetailsMores.Add(PayKaniInfoForBarAvordDetailsMore);

                            decimal dDarsadValueOfReCycle = decimal.Parse(item.DarsadValueOfReCycle);
                            PayKaniInfoForBarAvordDetailsMore = new clsPayKaniInfoForBarAvordDetailsMore();
                            PayKaniInfoForBarAvordDetailsMore.PayKaniInfoForBarAvordDetailsId = PayKaniInfoForBarAvordDetailsId;
                            PayKaniInfoForBarAvordDetailsMore.Value = dDarsadValueOfReCycle;
                            PayKaniInfoForBarAvordDetailsMore.Name = "DarsadReUseHajm";
                            _context.PayKaniInfoForBarAvordDetailsMores.Add(PayKaniInfoForBarAvordDetailsMore);

                            //PayKaniInfoForBarAvordDetails.SaveMore(PayKaniInfoForBarAvordDetailsId);
                        }

                        decimal dDetailValueOfVarize = decimal.Parse(item.DetailValueOfVarize);
                        if (dDetailValueOfVarize != 0)
                        {
                            clsPayKaniInfoForBarAvordDetailsMore PayKaniInfoForBarAvordDetailsMore = new clsPayKaniInfoForBarAvordDetailsMore();
                            PayKaniInfoForBarAvordDetailsMore.PayKaniInfoForBarAvordDetailsId = PayKaniInfoForBarAvordDetailsId;
                            PayKaniInfoForBarAvordDetailsMore.Value = dDetailValueOfVarize;
                            PayKaniInfoForBarAvordDetailsMore.Name = "Varizi";
                            _context.PayKaniInfoForBarAvordDetailsMores.Add(PayKaniInfoForBarAvordDetailsMore);

                            decimal dDarsadValueOfVarize = decimal.Parse(item.DarsadValueOfVarize);
                            PayKaniInfoForBarAvordDetailsMore = new clsPayKaniInfoForBarAvordDetailsMore();
                            PayKaniInfoForBarAvordDetailsMore.PayKaniInfoForBarAvordDetailsId = PayKaniInfoForBarAvordDetailsId;
                            PayKaniInfoForBarAvordDetailsMore.Value = dDarsadValueOfVarize;
                            PayKaniInfoForBarAvordDetailsMore.Name = "DarsadVarizi";
                            _context.PayKaniInfoForBarAvordDetailsMores.Add(PayKaniInfoForBarAvordDetailsMore);
                            //_context.SaveChanges();
                        }

                        decimal dDetailValueOfHaml = decimal.Parse(item.DetailValueOfHaml);
                        if (dDetailValueOfHaml != 0)
                        {
                            clsPayKaniInfoForBarAvordDetailsMore PayKaniInfoForBarAvordDetailsMore = new clsPayKaniInfoForBarAvordDetailsMore();
                            PayKaniInfoForBarAvordDetailsMore.PayKaniInfoForBarAvordDetailsId = PayKaniInfoForBarAvordDetailsId;
                            PayKaniInfoForBarAvordDetailsMore.Value = dDetailValueOfHaml;
                            PayKaniInfoForBarAvordDetailsMore.Name = "Haml";
                            _context.PayKaniInfoForBarAvordDetailsMores.Add(PayKaniInfoForBarAvordDetailsMore);

                            decimal dDarsadValueOfHaml = decimal.Parse(item.DarsadValueOfHaml);
                            PayKaniInfoForBarAvordDetailsMore = new clsPayKaniInfoForBarAvordDetailsMore();
                            PayKaniInfoForBarAvordDetailsMore.PayKaniInfoForBarAvordDetailsId = PayKaniInfoForBarAvordDetailsId;
                            PayKaniInfoForBarAvordDetailsMore.Value = dDarsadValueOfHaml;
                            PayKaniInfoForBarAvordDetailsMore.Name = "DarsadHaml";
                            _context.PayKaniInfoForBarAvordDetailsMores.Add(PayKaniInfoForBarAvordDetailsMore);
                        }


                    }
                    /////////////


                    clsNoeKhakBardari? noeKhakBardari = lstNoeKB.FirstOrDefault(x => x.Id == item.NoeKhakBardari);
                    if (noeKhakBardari != null)
                    {
                        string strCurrentShomareh = noeKhakBardari.FBItemShomareh.Trim();
                        clsFB? FB = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordUserId && x.Shomareh == strCurrentShomareh);
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
                                Shomareh = strCurrentShomareh,
                                NoeFBId = NoeFBId
                            };
                            _context.FBs.Add(newFB);
                            gFBId = newFB.ID;
                        }

                        decimal Hajm = decimal.Parse(item.DetailValue);
                        if (Hajm != 0)
                        {
                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh++;
                            RizMetre.Sharh = noeKhakBardari.Title;
                            RizMetre.Tedad = null;
                            RizMetre.Tool = null;
                            RizMetre.Arz = null;
                            RizMetre.Ertefa = null;
                            RizMetre.Vazn = Hajm;
                            RizMetre.Des = "";
                            RizMetre.FBId = gFBId;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "1";
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";

                            RizMetre.MeghdarJoz = Hajm;

                            _context.RizMetreUserses.Add(RizMetre);

                            clsPayKaniInfoForBarAvordDetailsRizMetre PayKaniInfoForBarAvordDetailsRizMetre
                                = new clsPayKaniInfoForBarAvordDetailsRizMetre
                                {
                                    PayKaniInfoForBarAvordDetailsId = PayKaniInfoForBarAvordDetails.ID,
                                    RizMetreUserId = RizMetre.ID
                                };
                            _context.PayKaniInfoForBarAvordDetailsRizMetres.Add(PayKaniInfoForBarAvordDetailsRizMetre);
                        }
                    }
                }
                _context.SaveChanges();

            }
            return new JsonResult("OK_");
        }
        catch (Exception)
        {
            return new JsonResult("NOK_");
        }
    }


    public JsonResult GetExistingKMPayKaniInfoWithBarAvordId([FromBody] RequestExistingKMPayKaniInfoWithBarAvord request)
    {

        List<clsPayKaniInfoForBarAvord> GetExistingKMPayKaniInfoWithBarAvord =
            _context.PayKaniInfoForBarAvords.Where(x => x.BaravordUserId == request.BaravordId && x.Type == request.Type).ToList();
        //string strParam1 = "BarAvordUserId='" + request.BaravordId + "' and Type=" + request.Type;
        //var Param = new SqlParameter("@Parameter", strParam1);

        //var GetExistingKMAmalyateKhakiInfoWithBarAvord = _context.Set<GetExistingKMPayKaniInfoWithBarAvordDto>()
        //    .FromSqlRaw("EXEC PayKaniInfoForBarAvordListWithParameter @Parameter", Param)
        //    .ToList();

        //var DtKMAmalyateKhakiBarAvord = GetExistingKMAmalyateKhakiInfoWithBarAvord.ToList();

        return new JsonResult(GetExistingKMPayKaniInfoWithBarAvord);
    }


    public JsonResult GetDetailsOfKMPayKaniInfoWithKMPayKaniId([FromBody] GetDetailsOfKMPayKaniInfoDto request)
    {
        Guid PayKaniInfoForBarAvordId = request.PayKaniInfoForBarAvordId;

        NoeFehrestBaha NoeFB = request.NoeFB;
        int Year = request.Year;
        int Type = request.Type;

        List<clsPayKaniInfoForBarAvordMore> KMPayKaniBarAvordMore =
            context.PayKaniInfoForBarAvordMores.Where(x => x.PayKaniInfoForBarAvordId == PayKaniInfoForBarAvordId).ToList();

        //List<PayKaniInfoForBarAvordDetailsDto> KMPayKaniBarAvordDetails = context.PayKaniInfoForBarAvordDetailses.Include(x => x.NoePayKani)
        //    .Where(x => x.PayKaniInfoForBarAvordId == PayKaniInfoForBarAvordId).Select(x => new PayKaniInfoForBarAvordDetailsDto
        //    {
        //        ID = x.ID,
        //        PayKaniInfoForBarAvordId = x.PayKaniInfoForBarAvordId,
        //        NoePayKaniId = x.NoePayKaniId,
        //        Title = x.NoePayKani.Title,
        //        Value = x.Value
        //    }).ToList();



        List<PayKaniInfoForBarAvordDetailsDto> KMPayKaniBarAvordDetails = context.NoeKhakBardaris
        .GroupJoin(
            context.PayKaniInfoForBarAvordDetailses
                .Where(x => x.PayKaniInfoForBarAvordId == PayKaniInfoForBarAvordId),
                noe => noe.Id,
                detail => detail.NoeKhakBardariId,
                  (noe, details) => new { noe, details }
                )
                .SelectMany(
            x => x.details.DefaultIfEmpty(),
            (x, detail) => new PayKaniInfoForBarAvordDetailsDto
            {
                ID = detail != null ? detail.ID : (Guid?)null,
                PayKaniInfoForBarAvordId = detail != null ? detail.PayKaniInfoForBarAvordId : (Guid?)null,
                NoeKhakBardariId = x.noe.Id,
                Title = x.noe.Title,
                Value = detail != null ? detail.Value : (decimal?)null,
                Type = x.noe.Type
            }).Where(x => x.Type == Type).ToList();


        List<Guid> gKMAId = KMPayKaniBarAvordDetails
            .Where(x => x.ID.HasValue)
            .Select(x => x.ID!.Value)
            .ToList();

        List<ItemFBShomarehForGetAndShowAddItemsFieldsDto> lstItemFields = _context.ItemsFieldses.Where(x => x.NoeFB == NoeFB).Select(x => new ItemFBShomarehForGetAndShowAddItemsFieldsDto
        {
            Shomareh = x.ItemShomareh,
            FieldType = x.FieldType,
            Vahed = x.Vahed,
            IsEnteringValue = x.IsEnteringValue
        }).ToList();


        List<PayKaniRizMetreDto> lstPayKaniInfoRizMetre = _context.PayKaniInfoForBarAvordDetailsRizMetres.Include(x => x.RizMetreUser).ThenInclude(x => x.FB)
              .Where(x => gKMAId.Contains(x.PayKaniInfoForBarAvordDetailsId)).Select(x => new PayKaniRizMetreDto
              {
                  Shomareh = x.RizMetreUser.Shomareh,
                  ShomarehNew = x.RizMetreUser.ShomarehNew,
                  Sharh = x.RizMetreUser.Sharh,
                  Tedad = x.RizMetreUser.Tedad,
                  Tool = x.RizMetreUser.Tool,
                  Arz = x.RizMetreUser.Arz,
                  Ertefa = x.RizMetreUser.Ertefa,
                  Vazn = x.RizMetreUser.Vazn,
                  MeghdarJoz = x.RizMetreUser.MeghdarJoz,
                  Des = x.RizMetreUser.Des,
                  FBId = x.RizMetreUser.FBId,
                  ForItem = x.RizMetreUser.ForItem,
                  Type = x.RizMetreUser.Type,
                  UseItem = x.RizMetreUser.Type,
                  ItemFBShomareh = x.RizMetreUser.FB.Shomareh
              }).ToList();

        List<string> lstItemFBShomareh = lstPayKaniInfoRizMetre.Select(x => x.ItemFBShomareh).Distinct().ToList();

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

        //if (KMPayKaniBarAvordDetails.Count != 0)
        //{
        //    for (int i = 0; i < KMPayKaniBarAvordDetails.Count; i++)
        //    {
        //        gKMAId.Add(KMPayKaniBarAvordDetails[i].ID);
        //    }
        //}

        List<clsPayKaniInfoForBarAvordDetailsMore> KMPayKaniBarAvordDetailsMore = context.PayKaniInfoForBarAvordDetailsMores.Where(x => gKMAId.Contains(x.PayKaniInfoForBarAvordDetailsId)).ToList();
        //DataTable DtKMPayKaniBarAvordDetailsMore = clsConvert.ToDataTable(varKMPayKaniBarAvordDetailsMore);


        List<clsPayKaniInfoForBarAvordEzafeBaha> KMPayKaniBarAvordDetailsEzafeBaha = context.PayKaniInfoForBarAvordEzafeBahas.Where(x => gKMAId.Contains(x.PayKaniInfoForBarAvordId)).ToList();


        //DataSet Ds = new DataSet();

        //DtKMPayKaniBarAvordDetails.TableName = "tblKMPayKaniBarAvordDetails";
        //DtKMPayKaniBarAvordMore.TableName = "tblKMPayKaniBarAvordMore";
        //DtKMPayKaniBarAvordDetailsMore.TableName = "tblKMPayKaniBarAvordDetailsMore";
        //DtKMPayKaniBarAvordDetailsEzafeBaha.TableName = "tblKMPayKaniBarAvordDetailsEzafeBaha";

        //DtKMPayKaniBarAvordDetails.Columns.Remove("PayKaniInfoForBarAvord");
        //DtKMPayKaniBarAvordDetails.Columns.Remove("PayKaniInfoForBarAvordDetailsEzafeBahas");
        //DtKMPayKaniBarAvordDetails.Columns.Remove("PayKaniInfoForBarAvordDetailsMores");
        /////////////////
        //DtKMPayKaniBarAvordMore.Columns.Remove("PayKaniInfoForBarAvord");
        ///////////////
        //DtKMPayKaniBarAvordDetailsMore.Columns.Remove("PayKaniInfoForBarAvordDetails");
        /////////////
        //DtKMPayKaniBarAvordDetailsEzafeBaha.Columns.Remove("PayKaniInfoForBarAvordDetails");
        /////////////
        //Ds.Tables.Add(DtKMPayKaniBarAvordDetails);
        //Ds.Tables.Add(DtKMPayKaniBarAvordMore);
        //Ds.Tables.Add(DtKMPayKaniBarAvordDetailsMore);
        //Ds.Tables.Add(DtKMPayKaniBarAvordDetailsEzafeBaha);

        var results = new
        {
            KMPayKaniBarAvordDetails,
            KMPayKaniBarAvordMore,
            KMPayKaniBarAvordDetailsMore,
            KMPayKaniBarAvordDetailsEzafeBaha,
            lstPayKaniInfoRizMetre,
            lstItemFBShomarehForGet
        };
        return new JsonResult(results);
        //return Ds.GetXml();
    }

    //public JsonResult GetEzafeBaha([FromBody] GetEzafeBahaForPayKaniDto request)
    //{
    //    List<clsPayKaniInfoForBarAvordDetails> lstAKIForBD =
    //        _context.PayKaniInfoForBarAvordDetailses.Include(x => x.PayKaniInfoForBarAvord)
    //        .Where(x => x.PayKaniInfoForBarAvordId == request.PayKaniInfoForBarAvordId).ToList();

    //    List<long> lstNoePayKaniId = lstAKIForBD.Select(x => x.NoePayKaniId).ToList();

    //    List<NoeKhakRiziEzafeBahaDto> lstNoePayKaniEzafeBaha
    //        = _context.NoePayKani_NoePayKaniEzafeBahas.Where(x => lstNoePayKaniId.Contains(x.NoePayKaniId))
    //        .Include(x => x.NoePayKaniEzafeBaha).Select(x => new NoePayKaniEzafeBahaDto
    //        {
    //            Id = x.NoePayKaniEzafeBaha.Id,
    //            NoePayKaniEzafeBaha = x.NoePayKaniEzafeBaha.Title,
    //            hasEnteringValue = x.NoePayKaniEzafeBaha.hasEnteringValue,
    //            CountForEnteringValue = x.NoePayKaniEzafeBaha.CountForEnteringValue,
    //            DefaultForEnteringValue = x.NoePayKaniEzafeBaha.DefaultForEnteringValue,
    //            DesForEnteringValue = x.NoePayKaniEzafeBaha.DesForEnteringValue
    //        }).Distinct().ToList();

    //    List<clsPayKaniInfoForBarAvordEzafeBaha> lstAKhForBEB =
    //        _context.PayKaniInfoForBarAvordEzafeBahas.Where(x => x.PayKaniInfoForBarAvordId == request.PayKaniInfoForBarAvordId).ToList();

    //    var Result = new
    //    {
    //        lstNoePayKaniEzafeBaha,
    //        lstAKhForBEB
    //    };

    //    return new JsonResult(Result);
    //}

    [HttpPost]
    public JsonResult UpdateRizMetreAKhEzafeBaha([FromBody] UpdateRizMetreAKhEzafeBaha request)
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
            return new JsonResult("OK_");
        }
        catch (Exception)
        {
            return new JsonResult("NOK_");
        }
    }

    public JsonResult SumRizMetreWithOperationId([FromBody] SumRizMetreWithOperationIdDto request)
    {
        Guid BarAvordUserId = request.BarAvordUserId;
        long OpId = request.OpId;
        int Year = request.Year;
        NoeFehrestBaha NoeFBId = request.NoeFBId;

        clsOperation_ItemsFB? OpeartionItem = _context.Operation_ItemsFBs.Include(x => x.Operation).FirstOrDefault(x => x.OperationId == OpId && x.Year == Year);
        if (OpeartionItem != null)
        {
            string ItemsFBShomareh = OpeartionItem.ItemsFBShomareh.Trim();
            List<clsFB> lstFBs = _context.FBs.Where(x => x.BarAvordId == BarAvordUserId && x.NoeFBId == NoeFBId && x.Shomareh == ItemsFBShomareh).ToList();
            decimal? dSumRizMetre = 0;
            foreach (var FB in lstFBs)
            {
                dSumRizMetre += _context.RizMetreUserses.Where(x => x.FBId == FB.ID).Sum(x => x.MeghdarJoz);
            }

            return new JsonResult("OK_" + dSumRizMetre);
        }
        else
            return new JsonResult("NOK");

    }


    public JsonResult SaveEzafeBahaPayKani([FromBody] SaveEzafeBahaPayKaniDto request)
    {

        PayKaniCommon common = new PayKaniCommon();
        if (common.SaveEzafeBahaPayKani(request, _context))
        {
            _context.SaveChanges();
            return new JsonResult("OK");
        }
        else
            return new JsonResult("نوع اضافه بها یافت نشد");


        //clsNoePayKaniEzafeBaha? NoeKhB_EB = _context.NoePayKaniEzafeBahas.FirstOrDefault(x => x.Id == request.NoePayKaniEzafeBahaId);
        //string strCurrentShomareh = "";
        //string? strCondition = "";
        //if (NoeKhB_EB != null)
        //{
        //    strCurrentShomareh = NoeKhB_EB.FBItemShomareh;
        //    strCondition = NoeKhB_EB.Condition;
        //}
        //else
        //{
        //    return new JsonResult("نوع اضافه بها یافت نشد");
        //}
        //DateTime Now = DateTime.Now;
        //Guid BarAvordUserId = request.BarAvordUserId;

        //long Shomareh = 1;
        //clsRizMetreUsers? rizMetreUser = _context.RizMetreUserses.Include(x => x.FB).OrderByDescending(x => x.Shomareh).FirstOrDefault(x => x.FB.BarAvordId == BarAvordUserId);
        //if (rizMetreUser != null)
        //{
        //    Shomareh = rizMetreUser.Shomareh + 1;
        //}

        //List<long> noeKhB = _context.NoePayKani_NoePayKaniEzafeBahas
        //      .Where(x => x.NoePayKaniEzafeBahaId == request.NoePayKaniEzafeBahaId).Select(x => x.NoePayKaniId).ToList();


        //List<PayKaniInfoForBarAvordDetailsInsertedDto> lstAKhForBD =
        //    _context.PayKaniInfoForBarAvordDetailsRizMetres.Include(x => x.PayKaniInfoForBarAvordDetails).ThenInclude(x => x.lstPayKaniInfoForBarAvordDetailsMore)
        //     .Where(x => x.PayKaniInfoForBarAvordDetails.PayKaniInfoForBarAvordId == request.PayKaniInfoForBarAvordId
        //                 && noeKhB.Contains(x.PayKaniInfoForBarAvordDetails.NoePayKaniId))
        //     .Select(x => new PayKaniInfoForBarAvordDetailsInsertedDto
        //     {
        //         RizMetreId = x.RizMetreUserId,
        //         //PayKaniInfoForBarAvordDetailId = x.PayKaniInfoForBarAvordDetailsId,
        //         //PayKaniInfoForBarAvordId = x.PayKaniInfoForBarAvordDetails.PayKaniInfoForBarAvordId,
        //         NoePayKaniId = x.PayKaniInfoForBarAvordDetails.NoePayKaniId,
        //         lstPayKaniInfoForBarAvordDetailsMore = x.PayKaniInfoForBarAvordDetails.lstPayKaniInfoForBarAvordDetailsMore.ToList()
        //     }).ToList();



        //clsPayKaniInfoForBarAvordEzafeBaha PayKaniInfoForBarAvordEzafeBaha = new clsPayKaniInfoForBarAvordEzafeBaha
        //{
        //    PayKaniInfoForBarAvordId = request.PayKaniInfoForBarAvordId,
        //    NoePayKaniEzafeBahaId = NoeKhB_EB.Id,
        //    InsertDateTime = Now
        //};
        //_context.PayKaniInfoForBarAvordEzafeBahas.Add(PayKaniInfoForBarAvordEzafeBaha);

        //clsFB? FB = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordUserId && x.Shomareh == strCurrentShomareh);
        //Guid gFBId = new Guid();
        //if (FB != null)
        //{
        //    gFBId = FB.ID;
        //}
        //else
        //{
        //    clsFB newFB = new clsFB
        //    {
        //        BarAvordId = BarAvordUserId,
        //        InsertDateTime = Now,
        //        Shomareh = strCurrentShomareh
        //    };
        //    _context.FBs.Add(newFB);
        //    gFBId = newFB.ID;
        //}
        //foreach (var item in lstAKhForBD)
        //{

        //    decimal? MeghdarJoz = null;
        //    clsPayKaniInfoForBarAvordDetailsMore? PayKaniInfoForBarAvordDetailsMore = null;
        //    switch (strCondition)
        //    {
        //        case "z*m":
        //            {
        //                PayKaniInfoForBarAvordDetailsMore =
        //                                    item.lstPayKaniInfoForBarAvordDetailsMore.FirstOrDefault(x => x.Name.ToLower() == "haml");
        //                if (PayKaniInfoForBarAvordDetailsMore != null)
        //                {
        //                    MeghdarJoz = PayKaniInfoForBarAvordDetailsMore.Value;
        //                }
        //                break;
        //            }
        //        case "(z+y)*m":
        //            {
        //                decimal? d1 = null;
        //                decimal? d2 = null;
        //                PayKaniInfoForBarAvordDetailsMore =
        //                             item.lstPayKaniInfoForBarAvordDetailsMore.FirstOrDefault(x => x.Name.ToLower() == "haml");
        //                if (PayKaniInfoForBarAvordDetailsMore != null)
        //                {
        //                    d1 = PayKaniInfoForBarAvordDetailsMore.Value;
        //                }

        //                PayKaniInfoForBarAvordDetailsMore =
        //                             item.lstPayKaniInfoForBarAvordDetailsMore.FirstOrDefault(x => x.Name.ToLower() == "reusehajm");
        //                if (PayKaniInfoForBarAvordDetailsMore != null)
        //                {
        //                    d2 = PayKaniInfoForBarAvordDetailsMore.Value;
        //                }

        //                MeghdarJoz = d1 * d2;
        //                break;
        //            }
        //        default:
        //            break;
        //    }

        //    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        //    RizMetre.Shomareh = Shomareh++;
        //    RizMetre.Sharh = "";
        //    RizMetre.Tedad = null;
        //    RizMetre.Tool = null;
        //    RizMetre.Arz = null;
        //    RizMetre.Ertefa = null;
        //    RizMetre.Vazn = null;
        //    RizMetre.Des = "";
        //    RizMetre.FBId = gFBId;
        //    RizMetre.OperationsOfHamlId = 1;
        //    RizMetre.Type = "1";
        //    RizMetre.ForItem = "";
        //    RizMetre.UseItem = "";
        //    RizMetre.MeghdarJoz = MeghdarJoz;
        //    _context.RizMetreUserses.Add(RizMetre);

        //    clsPayKaniInfoForBarAvordEzafeBahaRizMetre PayKaniInfoForBarAvordEzafeBahaRizMetre
        //        = new clsPayKaniInfoForBarAvordEzafeBahaRizMetre
        //        {
        //            PayKaniInfoForBarAvordEzafeBahaId = PayKaniInfoForBarAvordEzafeBaha.ID,
        //            RizMetreUserId = RizMetre.ID
        //        };
        //    _context.PayKaniInfoForBarAvordEzafeBahaRizMetres.Add(PayKaniInfoForBarAvordEzafeBahaRizMetre);
        //}
        //_context.SaveChanges();
    }

    //public JsonResult GetAKh_EzafeBaha([FromBody] GetAKh_EzafeBahaDto request)
    //{
    //    long Year = request.Year;
    //    NoeFehrestBaha NoeFB = request.NoeFB;

    //    Guid PayKaniInfoForBarAvordId = request.PayKaniInfoForBarAvordId;

    //    List<ItemFBShomarehForGetAndShowAddItemsFieldsDto> lstItemFields = _context.ItemsFieldses.Where(x => x.NoeFB == NoeFB).Select(x => new ItemFBShomarehForGetAndShowAddItemsFieldsDto
    //    {
    //        Shomareh = x.ItemShomareh,
    //        FieldType = x.FieldType,
    //        Vahed = x.Vahed,
    //        IsEnteringValue = x.IsEnteringValue
    //    }).ToList();

    //    List<AKhI_EzafeBahaRizMetreDto> lstAKhInfoRizMetre = _context.PayKaniInfoForBarAvordEzafeBahaRizMetres
    //        .Include(x => x.PayKaniInfoForBarAvordEzafeBaha).ThenInclude(x => x.NoePayKaniEzafeBaha)
    //        .Where(x => x.PayKaniInfoForBarAvordEzafeBaha.PayKaniInfoForBarAvordId == PayKaniInfoForBarAvordId
    //               && x.PayKaniInfoForBarAvordEzafeBaha.NoePayKaniEzafeBahaId == request.NoePayKaniEzafeBahaId)
    //        .Select(x => new AKhI_EzafeBahaRizMetreDto
    //        {
    //            RMId = x.RizMetreUser.ID,
    //            Shomareh = x.RizMetreUser.Shomareh,
    //            ShomarehNew = x.RizMetreUser.ShomarehNew,
    //            Sharh = x.RizMetreUser.Sharh,
    //            Tedad = x.RizMetreUser.Tedad,
    //            Tool = x.RizMetreUser.Tool,
    //            Arz = x.RizMetreUser.Arz,
    //            Ertefa = x.RizMetreUser.Ertefa,
    //            Vazn = x.RizMetreUser.Vazn,
    //            MeghdarJoz = x.RizMetreUser.MeghdarJoz,
    //            Des = x.RizMetreUser.Des,
    //            FBId = x.RizMetreUser.FBId,
    //            ForItem = x.RizMetreUser.ForItem,
    //            Type = x.RizMetreUser.Type,
    //            UseItem = x.RizMetreUser.Type,
    //            ItemFBShomareh = x.RizMetreUser.FB.Shomareh,
    //            hasDelButton = x.PayKaniInfoForBarAvordEzafeBaha.NoePayKaniEzafeBaha.EnableDeleting,
    //            hasEditButton = x.PayKaniInfoForBarAvordEzafeBaha.NoePayKaniEzafeBaha.EnableEditing,
    //        }).ToList();

    //    List<string> lstItemFBShomareh = lstAKhInfoRizMetre.Select(x => x.ItemFBShomareh).Distinct().ToList();

    //    List<clsFehrestBaha> lstFehrestBahas = _context.FehrestBahas.Where(x => x.Sal == Year && x.NoeFB == NoeFB && lstItemFBShomareh.Contains(x.Shomareh.Trim())).ToList();

    //    List<ItemFBShomarehForGetAndShowAddItemsDto> lstItemFBShomarehForGet = new List<ItemFBShomarehForGetAndShowAddItemsDto>();

    //    List<ItemFBShomarehForGetAndShowAddItemsFieldsDto> ItemFields = new List<ItemFBShomarehForGetAndShowAddItemsFieldsDto>();

    //    foreach (var item in lstItemFBShomareh)
    //    {
    //        clsFehrestBaha fehrestBaha = lstFehrestBahas.First(x => x.Shomareh == item);

    //        ItemFields = lstItemFields.Where(x => x.Shomareh.Trim() == item).ToList();
    //        ItemFBShomarehForGetAndShowAddItemsDto ItemFBShomarehForGet = new ItemFBShomarehForGetAndShowAddItemsDto
    //        {
    //            ItemFBShomareh = item,
    //            Des = fehrestBaha.Sharh,
    //            ItemFields = ItemFields

    //        };
    //        lstItemFBShomarehForGet.Add(ItemFBShomarehForGet);
    //    }

    //    var result = new
    //    {
    //        lstAKhInfoRizMetre,
    //        lstItemFBShomarehForGet
    //    };

    //    return new JsonResult(result);

    //}

    //public JsonResult DeleteEzafeBahaAKh([FromBody] DeleteEzafeBahaAKhDto request)
    //{
    //    List<long> noeKhB = _context.NoePayKani_NoePayKaniEzafeBahas
    //                        .Where(x => x.NoePayKaniEzafeBahaId == request.NoePayKaniEzafeBahaId).Select(x => x.NoePayKaniId).ToList();

    //    clsPayKaniInfoForBarAvordEzafeBaha? PayKaniInfoForBarAvordEzafeBaha =
    //        _context.PayKaniInfoForBarAvordEzafeBahas.FirstOrDefault(x => x.NoePayKaniEzafeBahaId == request.NoePayKaniEzafeBahaId
    //                                                                    && x.PayKaniInfoForBarAvordId == x.PayKaniInfoForBarAvordId);

    //    //List<PayKaniInfoForBarAvordDetailsInsertedDto> lstAKhForBDRizmetre =
    //    //    _context.PayKaniInfoForBarAvordDetailsRizMetres.Include(x => x.PayKaniInfoForBarAvordDetails)
    //    //     .Where(x => x.PayKaniInfoForBarAvordDetails.PayKaniInfoForBarAvordId == request.PayKaniInfoForBarAvordId
    //    //                 && noeKhB.Contains(x.PayKaniInfoForBarAvordDetails.NoePayKaniId))
    //    //     .Select(x => new PayKaniInfoForBarAvordDetailsInsertedDto
    //    //     {
    //    //         RizMetreId = x.RizMetreUserId,
    //    //         PayKaniInfoForBarAvordDetailId = x.PayKaniInfoForBarAvordDetailsId,
    //    //         PayKaniInfoForBarAvordId = x.PayKaniInfoForBarAvordDetails.PayKaniInfoForBarAvordId,
    //    //         NoePayKaniId = x.PayKaniInfoForBarAvordDetails.NoePayKaniId
    //    //     }).ToList();

    //    //List<Guid> lstPayKaniInfoForBarAvordIds = lstAKhForBDRizmetre.Select(x => x.PayKaniInfoForBarAvordId).ToList();

    //    //List<clsPayKaniInfoForBarAvordEzafeBaha> lstAKhForBE =
    //    //    _context.PayKaniInfoForBarAvordEzafeBahas.Where(x => lstPayKaniInfoForBarAvordIds.Contains(x.PayKaniInfoForBarAvordId)).ToList();

    //    //_context.PayKaniInfoForBarAvordEzafeBahas.RemoveRange(lstAKhForBE);

    //    if (PayKaniInfoForBarAvordEzafeBaha != null)
    //    {
    //        _context.PayKaniInfoForBarAvordEzafeBahas.Remove(PayKaniInfoForBarAvordEzafeBaha);


    //        List<clsPayKaniInfoForBarAvordEzafeBahaRizMetre> lstlstAKhForBERizMetre =
    //            _context.PayKaniInfoForBarAvordEzafeBahaRizMetres.Where(x => x.PayKaniInfoForBarAvordEzafeBahaId == PayKaniInfoForBarAvordEzafeBaha.ID).ToList();

    //        _context.PayKaniInfoForBarAvordEzafeBahaRizMetres.RemoveRange(lstlstAKhForBERizMetre);
    //    }
    //    _context.SaveChanges();

    //    return new JsonResult("OK");
    //}

    public JsonResult GetPK_EzafeBaha([FromBody] GetPK_EzafeBahaDto request)
    {
        long Year = request.Year;
        NoeFehrestBaha NoeFB = request.NoeFB;
        Guid BarAvordId = request.BarAvordId;
        // Guid PayKaniInfoForBarAvordId = request.PayKaniInfoForBarAvordId;

        List<ItemFBShomarehForGetAndShowAddItemsFieldsDto> lstItemFields = _context.ItemsFieldses.Where(x => x.NoeFB == NoeFB).Select(x => new ItemFBShomarehForGetAndShowAddItemsFieldsDto
        {
            Shomareh = x.ItemShomareh,
            FieldType = x.FieldType,
            Vahed = x.Vahed,
            IsEnteringValue = x.IsEnteringValue
        }).ToList();

        List<PayKaniI_EzafeBahaRizMetreDto> lstPKInfoRizMetre = _context.PayKaniInfoForBarAvordEzafeBahaRizMetres
            .Include(x => x.PayKaniInfoForBarAvordEzafeBaha).ThenInclude(x => x.NoeKhakBardariEzafeBaha)
            .Include(x => x.PayKaniInfoForBarAvordEzafeBaha).ThenInclude(x => x.PayKaniInfoForBarAvord)
            .Where(x => x.PayKaniInfoForBarAvordEzafeBaha.PayKaniInfoForBarAvord.BaravordUserId == BarAvordId
                        && x.PayKaniInfoForBarAvordEzafeBaha.PayKaniInfoForBarAvord.NoeFBId == NoeFB
                        && x.PayKaniInfoForBarAvordEzafeBaha.NoeKhakBardariEzafeBahaId == request.NoeKhakBardariEzafeBahaId)
            .Select(x => new PayKaniI_EzafeBahaRizMetreDto
            {
                RMId = x.RizMetreUser.ID,
                Shomareh = x.RizMetreUser.Shomareh,
                ShomarehNew = x.RizMetreUser.ShomarehNew,
                Sharh = x.RizMetreUser.Sharh,
                Tedad = x.RizMetreUser.Tedad,
                Tool = x.RizMetreUser.Tool,
                Arz = x.RizMetreUser.Arz,
                Ertefa = x.RizMetreUser.Ertefa,
                Vazn = x.RizMetreUser.Vazn,
                MeghdarJoz = x.RizMetreUser.MeghdarJoz,
                Des = x.RizMetreUser.Des,
                FBId = x.RizMetreUser.FBId,
                ForItem = x.RizMetreUser.ForItem,
                Type = x.RizMetreUser.Type,
                UseItem = x.RizMetreUser.Type,
                ItemFBShomareh = x.RizMetreUser.FB.Shomareh,
                hasDelButton = x.PayKaniInfoForBarAvordEzafeBaha.NoeKhakBardariEzafeBaha.EnableDeleting,
                hasEditButton = x.PayKaniInfoForBarAvordEzafeBaha.NoeKhakBardariEzafeBaha.EnableEditing,
            }).ToList();

        List<string> lstItemFBShomareh = lstPKInfoRizMetre.Select(x => x.ItemFBShomareh).Distinct().ToList();

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
            lstPKInfoRizMetre,
            lstItemFBShomarehForGet
        };

        return new JsonResult(result);

    }



    [HttpPost]
    public JsonResult GetEzafeBaha([FromBody] GetEzafeBahaForPayKaniDto request)
    {
        List<clsPayKaniInfoForBarAvordDetails> lstPKIForBD =
            _context.PayKaniInfoForBarAvordDetailses.Include(x => x.PayKaniInfoForBarAvord)
            .Where(x => x.PayKaniInfoForBarAvord.BaravordUserId == request.BarAvordUserId).ToList();

        List<long> lstNoePayKaniId = lstPKIForBD.Select(x => x.NoeKhakBardariId).ToList();

        List<NoePayKaniEzafeBahaDto> lstNoePayKaniEzafeBaha
            = _context.NoeKhakBardari_NoeKhakBardariEzafeBahas.Include(x => x.NoeKhakBardari).Where(x => lstNoePayKaniId.Contains(x.NoeKhakBardariId) && x.NoeKhakBardari.Type == 2)
            .Include(x => x.NoeKhakBardariEzafeBaha).Select(x => new NoePayKaniEzafeBahaDto
            {
                Id = x.NoeKhakBardariEzafeBaha.Id,
                NoePayKaniEzafeBaha = x.NoeKhakBardariEzafeBaha.Title,
                hasEnteringValue = x.NoeKhakBardariEzafeBaha.hasEnteringValue,
                CountForEnteringValue = x.NoeKhakBardariEzafeBaha.CountForEnteringValue,
                DefaultForEnteringValue = x.NoeKhakBardariEzafeBaha.DefaultForEnteringValue,
                DesForEnteringValue = x.NoeKhakBardariEzafeBaha.DesForEnteringValue
            }).Distinct().ToList();

        List<clsPayKaniInfoForBarAvordEzafeBaha> lstPKForBEB =
            _context.PayKaniInfoForBarAvordEzafeBahas.Where(x => x.PayKaniInfoForBarAvord.BaravordUserId == request.BarAvordUserId).ToList();

        var Result = new
        {
            lstNoePayKaniEzafeBaha,
            lstPKForBEB
        };

        return new JsonResult(Result);
    }


    //public JsonResult OverLowKMCheck(OverLowKMCheckDto request)
    //{
    //    List<clsPayKaniInfoForBarAvord> lstAKhInfo = _context.PayKaniInfoForBarAvords.Where(x => x.BaravordUserId == request.BarAvordId).ToList();

    //    long KMS = request.KMS;
    //    long KME = request.KME;

    //    List<OverLowKMCheckK_Start_EndDto> lstKMS =
    //        lstAKhInfo.Select(x => new OverLowKMCheckK_Start_EndDto
    //        {
    //            KStart = long.Parse(x.FromKM),
    //            KEnd = long.Parse(x.ToKM)
    //        }).ToList();

    //    var overlaps = lstKMS.Where(r =>
    //        Math.Max(r.KStart, KMS) <= Math.Min(r.KEnd, KME)
    //    ).ToList();

    //    return new JsonResult("OK");
    //}
}
