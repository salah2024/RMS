using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.AmalyateKhaki.Common;
using RMS.Controllers.AmalyateKhaki.Dto;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Common;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.AmalyateKhaki;

public class AmalyateKhakiInfoForBarAvordsController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;


    public JsonResult ReturnNoeKhakBardari([FromBody] ReturnNoeKhakBardariDto request)
    {
        List<clsNoeKhakBardari> lstNoeKhakBardari = _context.NoeKhakBardaris.Where(x => x.Year == request.Year).ToList();
        return new JsonResult(lstNoeKhakBardari);
    }

    public JsonResult SaveKhakBardariInfoForBarAvord([FromBody] SaveKhakBardariInfoForBarAvordDto request)
    {

        Guid BarAvordUserId = request.BarAvordUserId;
        NoeAmalyatKhaki Type = request.Type;
        long FromKM = request.FromKM;
        long ToKM = request.ToKM;
        string HKB = request.HKB;

        DateTime Now = DateTime.Now;
        List<KhakBardariInfoForBarAvordItemsDto> lstItems = request.lstItems;
        List<long> lstlngNoeKhakBardari = lstItems.Select(x => x.NoeKhakBardari).ToList();

        List<clsNoeKhakBardari> lstNoeKB = _context.NoeKhakBardaris.Where(x => lstlngNoeKhakBardari.Contains(x.Id)).ToList();

        decimal dHKB = decimal.Parse(HKB);
        int intKMNum = 1;
        clsAmalyateKhakiInfoForBarAvord? KMNum = _context.AmalyateKhakiInfoForBarAvords.OrderByDescending(x => x.KMNum).FirstOrDefault(x => x.BaravordUserId == BarAvordUserId && x.Type == Type);

        if (KMNum != null)
        {
            intKMNum = KMNum.KMNum + 1; //int.Parse(DtKMNum.Rows[0]["KMNum"].ToString()) + 1;
        }

        clsAmalyateKhakiInfoForBarAvord AmalyateKhakiInfoForBarAvord = new clsAmalyateKhakiInfoForBarAvord();
        AmalyateKhakiInfoForBarAvord.BaravordUserId = BarAvordUserId;
        AmalyateKhakiInfoForBarAvord.FromKM = FromKM.ToString("D6");
        AmalyateKhakiInfoForBarAvord.ToKM = ToKM.ToString("D6");
        AmalyateKhakiInfoForBarAvord.Type = Type;
        AmalyateKhakiInfoForBarAvord.Name = "";
        AmalyateKhakiInfoForBarAvord.KMNum = intKMNum;
        AmalyateKhakiInfoForBarAvord.Value = dHKB;
        _context.AmalyateKhakiInfoForBarAvords.Add(AmalyateKhakiInfoForBarAvord);
        //_context.SaveChanges();
        Guid Id = AmalyateKhakiInfoForBarAvord.ID;

        ///////////
        clsAmalyateKhakiInfoForBarAvordMore AmalyateKhakiInfoForBarAvordMore = new clsAmalyateKhakiInfoForBarAvordMore();
        AmalyateKhakiInfoForBarAvordMore.AmalyateKhakiInfoForBarAvordId = Id;
        AmalyateKhakiInfoForBarAvordMore.Value = dHKB;
        AmalyateKhakiInfoForBarAvordMore.Name = "HKB";
        _context.AmalyateKhakiInfoForBarAvordMores.Add(AmalyateKhakiInfoForBarAvordMore);
        _context.SaveChanges();

        Guid guCheckID = new Guid();
        if (Id != guCheckID)
        {

            clsAmalyateKhakiInfoForBarAvordDetails AmalyateKhakiInfoForBarAvordDetails = new clsAmalyateKhakiInfoForBarAvordDetails();

            long Shomareh = 1;
            clsRizMetreUsers? rizMetreUser = _context.RizMetreUserses.Include(x => x.FB).OrderByDescending(x => x.Shomareh).FirstOrDefault(x => x.FB.BarAvordId == BarAvordUserId);
            if (rizMetreUser != null)
            {
                Shomareh = rizMetreUser.Shomareh + 1;
            }

            foreach (var item in lstItems)
            {
                AmalyateKhakiInfoForBarAvordDetails
                    = new clsAmalyateKhakiInfoForBarAvordDetails
                    {
                        AmalyateKhakiInfoForBarAvordId = Id,
                        NoeKhakBardariId = item.NoeKhakBardari,
                        Name = ""
                    };
                _context.AmalyateKhakiInfoForBarAvordDetailses.Add(AmalyateKhakiInfoForBarAvordDetails);

                Guid AmalyateKhakiInfoForBarAvordDetailsId = AmalyateKhakiInfoForBarAvordDetails.ID;

                if (AmalyateKhakiInfoForBarAvordDetailsId != guCheckID)
                {

                    clsAmalyateKhakiInfoForBarAvordDetailsMore AmalyateKhakiInfoForBarAvordDetailsMore = new clsAmalyateKhakiInfoForBarAvordDetailsMore();
                    AmalyateKhakiInfoForBarAvordDetailsMore.Value = decimal.Parse(item.DetailValue);
                    AmalyateKhakiInfoForBarAvordDetailsMore.Name = "KhDetail";
                    AmalyateKhakiInfoForBarAvordDetailsMore.AmalyateKhakiInfoForBarAvordDetailsId = AmalyateKhakiInfoForBarAvordDetailsId;
                    _context.AmalyateKhakiInfoForBarAvordDetailsMores.Add(AmalyateKhakiInfoForBarAvordDetailsMore);

                    AmalyateKhakiInfoForBarAvordDetailsMore = new clsAmalyateKhakiInfoForBarAvordDetailsMore();
                    AmalyateKhakiInfoForBarAvordDetailsMore.Value = decimal.Parse(item.DarsadValue);
                    AmalyateKhakiInfoForBarAvordDetailsMore.Name = "DarsadKhDetail";
                    AmalyateKhakiInfoForBarAvordDetailsMore.AmalyateKhakiInfoForBarAvordDetailsId = AmalyateKhakiInfoForBarAvordDetailsId;
                    _context.AmalyateKhakiInfoForBarAvordDetailsMores.Add(AmalyateKhakiInfoForBarAvordDetailsMore);

                    AmalyateKhakiInfoForBarAvordDetailsMore = new clsAmalyateKhakiInfoForBarAvordDetailsMore();
                    AmalyateKhakiInfoForBarAvordDetailsMore.Value = decimal.Parse(item.DetailValueOfReCycle);
                    AmalyateKhakiInfoForBarAvordDetailsMore.Name = "ReUseHajm";
                    AmalyateKhakiInfoForBarAvordDetailsMore.AmalyateKhakiInfoForBarAvordDetailsId = AmalyateKhakiInfoForBarAvordDetailsId;
                    _context.AmalyateKhakiInfoForBarAvordDetailsMores.Add(AmalyateKhakiInfoForBarAvordDetailsMore);

                    AmalyateKhakiInfoForBarAvordDetailsMore = new clsAmalyateKhakiInfoForBarAvordDetailsMore();
                    AmalyateKhakiInfoForBarAvordDetailsMore.Value = decimal.Parse(item.DarsadValueOfReCycle);
                    AmalyateKhakiInfoForBarAvordDetailsMore.Name = "DarsadReUseHajm";
                    AmalyateKhakiInfoForBarAvordDetailsMore.AmalyateKhakiInfoForBarAvordDetailsId = AmalyateKhakiInfoForBarAvordDetailsId;
                    _context.AmalyateKhakiInfoForBarAvordDetailsMores.Add(AmalyateKhakiInfoForBarAvordDetailsMore);

                    AmalyateKhakiInfoForBarAvordDetailsMore = new clsAmalyateKhakiInfoForBarAvordDetailsMore();
                    AmalyateKhakiInfoForBarAvordDetailsMore.Value = decimal.Parse(item.DetailValueOfVarize);
                    AmalyateKhakiInfoForBarAvordDetailsMore.Name = "Varizi";
                    AmalyateKhakiInfoForBarAvordDetailsMore.AmalyateKhakiInfoForBarAvordDetailsId = AmalyateKhakiInfoForBarAvordDetailsId;
                    _context.AmalyateKhakiInfoForBarAvordDetailsMores.Add(AmalyateKhakiInfoForBarAvordDetailsMore);

                    AmalyateKhakiInfoForBarAvordDetailsMore = new clsAmalyateKhakiInfoForBarAvordDetailsMore();
                    AmalyateKhakiInfoForBarAvordDetailsMore.Value = decimal.Parse(item.DarsadValueOfVarize);
                    AmalyateKhakiInfoForBarAvordDetailsMore.Name = "DarsadVarizi";
                    AmalyateKhakiInfoForBarAvordDetailsMore.AmalyateKhakiInfoForBarAvordDetailsId = AmalyateKhakiInfoForBarAvordDetailsId;
                    _context.AmalyateKhakiInfoForBarAvordDetailsMores.Add(AmalyateKhakiInfoForBarAvordDetailsMore);

                    AmalyateKhakiInfoForBarAvordDetailsMore = new clsAmalyateKhakiInfoForBarAvordDetailsMore();
                    AmalyateKhakiInfoForBarAvordDetailsMore.Value = decimal.Parse(item.DetailValueOfHaml);
                    AmalyateKhakiInfoForBarAvordDetailsMore.Name = "Haml";
                    AmalyateKhakiInfoForBarAvordDetailsMore.AmalyateKhakiInfoForBarAvordDetailsId = AmalyateKhakiInfoForBarAvordDetailsId;
                    _context.AmalyateKhakiInfoForBarAvordDetailsMores.Add(AmalyateKhakiInfoForBarAvordDetailsMore);

                    AmalyateKhakiInfoForBarAvordDetailsMore = new clsAmalyateKhakiInfoForBarAvordDetailsMore();
                    AmalyateKhakiInfoForBarAvordDetailsMore.Value = decimal.Parse(item.DarsadValueOfHaml);
                    AmalyateKhakiInfoForBarAvordDetailsMore.Name = "DarsadHaml";
                    AmalyateKhakiInfoForBarAvordDetailsMore.AmalyateKhakiInfoForBarAvordDetailsId = AmalyateKhakiInfoForBarAvordDetailsId;
                    _context.AmalyateKhakiInfoForBarAvordDetailsMores.Add(AmalyateKhakiInfoForBarAvordDetailsMore);
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
                            Shomareh = strCurrentShomareh
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
                        RizMetre.Des = "کیلومتراژ " + FromKM + " تا " + ToKM;
                        RizMetre.FBId = gFBId;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "1";//"400" + KMNum.ToString("D3") + "05";///خاکبرداری با مواد سوزا
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

                        clsAmalyateKhakiInfoForBarAvordDetailsRizMetre AmalyateKhakiInfoForBarAvordDetailsRizMetre
                            = new clsAmalyateKhakiInfoForBarAvordDetailsRizMetre
                            {
                                AmalyateKhakiInfoForBarAvordDetailsId = AmalyateKhakiInfoForBarAvordDetails.ID,
                                RizMetreUserId = RizMetre.ID
                            };
                        _context.AmalyateKhakiInfoForBarAvordDetailsRizMetres.Add(AmalyateKhakiInfoForBarAvordDetailsRizMetre);
                    }
                }
            }
        }

        _context.SaveChanges();
        return new JsonResult("OK_" + Id + "_" + intKMNum);
    }


    public JsonResult UpdateKhakBardariInfoForBarAvord([FromBody] UpdateKhakBardariInfoForBarAvordDto request)
    {
        try
        {
            Guid BarAvordUserId = request.BarAvordUserId;
            long FromKM = request.FromKM;
            long ToKM = request.ToKM;
            string HKB = request.HKB;
            Guid KMKhakBardariId = request.KMKhakBardariId;
            long Year = request.Year;

            DateTime Now = DateTime.Now;
            List<KhakBardariInfoForBarAvordItemsForUpdateDto> lstItems = request.lstItems;
            List<long> lstlngNoeKhakBardari = lstItems.Select(x => x.NoeKhakBardari).ToList();

            List<clsNoeKhakBardari> lstNoeKB = _context.NoeKhakBardaris.Where(x => lstlngNoeKhakBardari.Contains(x.Id)).ToList();


            decimal dHKB = decimal.Parse(HKB);

            clsAmalyateKhakiInfoForBarAvord? currentAmalyateKhakiInfoForBarAvord = _context.AmalyateKhakiInfoForBarAvords.FirstOrDefault(x => x.BaravordUserId == BarAvordUserId);
            if (currentAmalyateKhakiInfoForBarAvord != null)
            {

                _context.Entry(currentAmalyateKhakiInfoForBarAvord).CurrentValues.SetValues(new
                {
                    FromKM = FromKM.ToString("D6"),
                    ToKM = ToKM.ToString("D6"),
                    Value = dHKB
                });

                List<clsAmalyateKhakiInfoForBarAvordDetails> lstAmalyateKhakiInfoForBarAvordDetails = _context.AmalyateKhakiInfoForBarAvordDetailses.Where(x => x.AmalyateKhakiInfoForBarAvordId == KMKhakBardariId).ToList();

                List<Guid> lstAmalyateKhakiInfoForBarAvordDetailIds = lstAmalyateKhakiInfoForBarAvordDetails.Select(x => x.ID).ToList();
                List<clsAmalyateKhakiInfoForBarAvordDetailsRizMetre> lstAmalyateKhakiInfoForBarAvordDetailsRizMetres
                    = _context.AmalyateKhakiInfoForBarAvordDetailsRizMetres.Where(x => lstAmalyateKhakiInfoForBarAvordDetailIds.Contains(x.AmalyateKhakiInfoForBarAvordDetailsId)).ToList();

                if (lstAmalyateKhakiInfoForBarAvordDetails.Count != 0)
                {
                    _context.AmalyateKhakiInfoForBarAvordDetailses.RemoveRange(lstAmalyateKhakiInfoForBarAvordDetails);
                    _context.AmalyateKhakiInfoForBarAvordDetailsRizMetres.RemoveRange(lstAmalyateKhakiInfoForBarAvordDetailsRizMetres);
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
                    clsAmalyateKhakiInfoForBarAvordDetails AmalyateKhakiInfoForBarAvordDetails = new clsAmalyateKhakiInfoForBarAvordDetails();
                    AmalyateKhakiInfoForBarAvordDetails.AmalyateKhakiInfoForBarAvordId = KMKhakBardariId;
                    AmalyateKhakiInfoForBarAvordDetails.NoeKhakBardariId = item.NoeKhakBardari;
                    AmalyateKhakiInfoForBarAvordDetails.Name = "";
                    _context.AmalyateKhakiInfoForBarAvordDetailses.Add(AmalyateKhakiInfoForBarAvordDetails);
                    Guid AmalyateKhakiInfoForBarAvordDetailsId = AmalyateKhakiInfoForBarAvordDetails.ID;

                    Guid guCheck = new Guid();

                    if (AmalyateKhakiInfoForBarAvordDetailsId != guCheck)
                    {
                        decimal dDetailValue = decimal.Parse(item.DetailValue);
                        if (dDetailValue != 0)
                        {
                            clsAmalyateKhakiInfoForBarAvordDetailsMore AmalyateKhakiInfoForBarAvordDetailsMore = new clsAmalyateKhakiInfoForBarAvordDetailsMore();
                            AmalyateKhakiInfoForBarAvordDetailsMore.AmalyateKhakiInfoForBarAvordDetailsId = AmalyateKhakiInfoForBarAvordDetailsId;
                            AmalyateKhakiInfoForBarAvordDetailsMore.Value = dDetailValue;
                            AmalyateKhakiInfoForBarAvordDetailsMore.Name = "KhDetail";
                            _context.AmalyateKhakiInfoForBarAvordDetailsMores.Add(AmalyateKhakiInfoForBarAvordDetailsMore);

                            decimal dDarsadValue = decimal.Parse(item.DarsadValue);
                            AmalyateKhakiInfoForBarAvordDetailsMore = new clsAmalyateKhakiInfoForBarAvordDetailsMore();
                            AmalyateKhakiInfoForBarAvordDetailsMore.AmalyateKhakiInfoForBarAvordDetailsId = AmalyateKhakiInfoForBarAvordDetailsId;
                            AmalyateKhakiInfoForBarAvordDetailsMore.Value = dDarsadValue;
                            AmalyateKhakiInfoForBarAvordDetailsMore.Name = "DarsadKhDetail";
                            _context.AmalyateKhakiInfoForBarAvordDetailsMores.Add(AmalyateKhakiInfoForBarAvordDetailsMore);
                            //_context.SaveChanges();
                        }

                        decimal dDetailValueOfReCycle = decimal.Parse(item.DetailValueOfReCycle);
                        if (dDetailValueOfReCycle != 0)
                        {
                            clsAmalyateKhakiInfoForBarAvordDetailsMore AmalyateKhakiInfoForBarAvordDetailsMore = new clsAmalyateKhakiInfoForBarAvordDetailsMore();
                            AmalyateKhakiInfoForBarAvordDetailsMore.AmalyateKhakiInfoForBarAvordDetailsId = AmalyateKhakiInfoForBarAvordDetailsId;
                            AmalyateKhakiInfoForBarAvordDetailsMore.Value = dDetailValueOfReCycle;
                            AmalyateKhakiInfoForBarAvordDetailsMore.Name = "ReUseHajm";
                            _context.AmalyateKhakiInfoForBarAvordDetailsMores.Add(AmalyateKhakiInfoForBarAvordDetailsMore);

                            decimal dDarsadValueOfReCycle = decimal.Parse(item.DarsadValueOfReCycle);
                            AmalyateKhakiInfoForBarAvordDetailsMore = new clsAmalyateKhakiInfoForBarAvordDetailsMore();
                            AmalyateKhakiInfoForBarAvordDetailsMore.AmalyateKhakiInfoForBarAvordDetailsId = AmalyateKhakiInfoForBarAvordDetailsId;
                            AmalyateKhakiInfoForBarAvordDetailsMore.Value = dDarsadValueOfReCycle;
                            AmalyateKhakiInfoForBarAvordDetailsMore.Name = "DarsadReUseHajm";
                            _context.AmalyateKhakiInfoForBarAvordDetailsMores.Add(AmalyateKhakiInfoForBarAvordDetailsMore);

                            //AmalyateKhakiInfoForBarAvordDetails.SaveMore(AmalyateKhakiInfoForBarAvordDetailsId);
                        }

                        decimal dDetailValueOfVarize = decimal.Parse(item.DetailValueOfVarize);
                        if (dDetailValueOfVarize != 0)
                        {
                            clsAmalyateKhakiInfoForBarAvordDetailsMore AmalyateKhakiInfoForBarAvordDetailsMore = new clsAmalyateKhakiInfoForBarAvordDetailsMore();
                            AmalyateKhakiInfoForBarAvordDetailsMore.AmalyateKhakiInfoForBarAvordDetailsId = AmalyateKhakiInfoForBarAvordDetailsId;
                            AmalyateKhakiInfoForBarAvordDetailsMore.Value = dDetailValueOfVarize;
                            AmalyateKhakiInfoForBarAvordDetailsMore.Name = "Varizi";
                            _context.AmalyateKhakiInfoForBarAvordDetailsMores.Add(AmalyateKhakiInfoForBarAvordDetailsMore);

                            decimal dDarsadValueOfVarize = decimal.Parse(item.DarsadValueOfVarize);
                            AmalyateKhakiInfoForBarAvordDetailsMore = new clsAmalyateKhakiInfoForBarAvordDetailsMore();
                            AmalyateKhakiInfoForBarAvordDetailsMore.AmalyateKhakiInfoForBarAvordDetailsId = AmalyateKhakiInfoForBarAvordDetailsId;
                            AmalyateKhakiInfoForBarAvordDetailsMore.Value = dDarsadValueOfVarize;
                            AmalyateKhakiInfoForBarAvordDetailsMore.Name = "DarsadVarizi";
                            _context.AmalyateKhakiInfoForBarAvordDetailsMores.Add(AmalyateKhakiInfoForBarAvordDetailsMore);
                            //_context.SaveChanges();
                        }

                        decimal dDetailValueOfHaml = decimal.Parse(item.DetailValueOfHaml);
                        if (dDetailValueOfHaml != 0)
                        {
                            clsAmalyateKhakiInfoForBarAvordDetailsMore AmalyateKhakiInfoForBarAvordDetailsMore = new clsAmalyateKhakiInfoForBarAvordDetailsMore();
                            AmalyateKhakiInfoForBarAvordDetailsMore.AmalyateKhakiInfoForBarAvordDetailsId = AmalyateKhakiInfoForBarAvordDetailsId;
                            AmalyateKhakiInfoForBarAvordDetailsMore.Value = dDetailValueOfHaml;
                            AmalyateKhakiInfoForBarAvordDetailsMore.Name = "Haml";
                            _context.AmalyateKhakiInfoForBarAvordDetailsMores.Add(AmalyateKhakiInfoForBarAvordDetailsMore);

                            decimal dDarsadValueOfHaml = decimal.Parse(item.DarsadValueOfHaml);
                            AmalyateKhakiInfoForBarAvordDetailsMore = new clsAmalyateKhakiInfoForBarAvordDetailsMore();
                            AmalyateKhakiInfoForBarAvordDetailsMore.AmalyateKhakiInfoForBarAvordDetailsId = AmalyateKhakiInfoForBarAvordDetailsId;
                            AmalyateKhakiInfoForBarAvordDetailsMore.Value = dDarsadValueOfHaml;
                            AmalyateKhakiInfoForBarAvordDetailsMore.Name = "DarsadHaml";
                            _context.AmalyateKhakiInfoForBarAvordDetailsMores.Add(AmalyateKhakiInfoForBarAvordDetailsMore);
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
                                Shomareh = strCurrentShomareh
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
                            RizMetre.Des = "کیلومتراژ " + FromKM + " تا " + ToKM;
                            RizMetre.FBId = gFBId;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "1";//"400" + KMNum.ToString("D3") + "05";///خاکبرداری با مواد سوزا
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";

                            RizMetre.MeghdarJoz = Hajm;

                            _context.RizMetreUserses.Add(RizMetre);

                            clsAmalyateKhakiInfoForBarAvordDetailsRizMetre AmalyateKhakiInfoForBarAvordDetailsRizMetre
                                = new clsAmalyateKhakiInfoForBarAvordDetailsRizMetre
                                {
                                    AmalyateKhakiInfoForBarAvordDetailsId = AmalyateKhakiInfoForBarAvordDetails.ID,
                                    RizMetreUserId = RizMetre.ID
                                };
                            _context.AmalyateKhakiInfoForBarAvordDetailsRizMetres.Add(AmalyateKhakiInfoForBarAvordDetailsRizMetre);
                        }
                    }
                }

                ////
                /////دریافت اضافه بهاهای درج شده
                ////

                List<clsAmalyateKhakiInfoForBarAvordEzafeBaha> lstAKhForBEB =
                        _context.AmalyateKhakiInfoForBarAvordEzafeBahas.Where(x => x.AmalyateKhakiInfoForBarAvordId == currentAmalyateKhakiInfoForBarAvord.ID).ToList();

                ///حذف اضافه بهاهای قبلی
                _context.AmalyateKhakiInfoForBarAvordEzafeBahas.RemoveRange(lstAKhForBEB);


                SaveEzafeBahaAKhDto request1 = new SaveEzafeBahaAKhDto
                {
                    AmalyateKhakiInfoForBarAvordId = currentAmalyateKhakiInfoForBarAvord.ID,
                    BarAvordUserId = BarAvordUserId,
                    Year = Year
                };
                _context.SaveChanges();

                List<long> lstAKh = lstAKhForBEB.Select(x => x.NoeKhakBardariEzafeBahaId).ToList();

                foreach (var item in lstAKh)
                {
                    AmalyateKhakiCommon common = new AmalyateKhakiCommon();
                    request1.NoeKhakBardariEzafeBahaId = item;
                    common.SaveEzafeBahaAKh(request1, context);
                    //if ()
                    //    return new JsonResult("OK");
                    //else
                    //    return new JsonResult("نوع اضافه بها یافت نشد");
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


    public JsonResult GetExistingKMAmalyateKhakiInfoWithBarAvordId([FromBody] RequestExistingKMAmalyateKhakiInfoWithBarAvord request)
    {

        string strParam1 = "BarAvordUserId='" + request.BaravordId + "' and Type=" + request.Type;
        var Param = new SqlParameter("@Parameter", strParam1);
        var TypeParam = new SqlParameter("@Type", request.Type);

        var GetExistingKMAmalyateKhakiInfoWithBarAvord = _context.Set<GetExistingKMAmalyateKhakiInfoWithBarAvordDto>()
            .FromSqlRaw("EXEC AmalyateKhakiInfoForBarAvordListWithParameter @Parameter,@Type", Param, TypeParam)
            .ToList();

        //DataTable DtKMAmalyateKhakiBarAvord = clsConvert.ToDataTable(GetExistingKMAmalyateKhakiInfoWithBarAvord);
        var DtKMAmalyateKhakiBarAvord = GetExistingKMAmalyateKhakiInfoWithBarAvord.ToList();
        //DataSet Ds = new DataSet();

        //DtKMAmalyateKhakiBarAvord.TableName = "tblKMAmalyateKhakiBarAvord";

        //Ds.Tables.Add(DtKMAmalyateKhakiBarAvord);

        return new JsonResult(DtKMAmalyateKhakiBarAvord);
    }


    public JsonResult GetDetailsOfKMKhakBardariInfoWithKMKhakBardariId([FromBody] GetDetailsOfKMKhakBardariInfoDto request)
    {
        Guid AmalyateKhakiInfoForBarAvordId = request.AmalyateKhakiInfoForBarAvordId;

        NoeFehrestBaha NoeFB = request.NoeFB;
        int Year = request.Year;

        List<clsAmalyateKhakiInfoForBarAvordMore> KMAmalyateKhakiBarAvordMore = context.AmalyateKhakiInfoForBarAvordMores.Where(x => x.AmalyateKhakiInfoForBarAvordId == AmalyateKhakiInfoForBarAvordId).ToList();

        //List<AmalyateKhakiInfoForBarAvordDetailsDto> KMAmalyateKhakiBarAvordDetails = context.AmalyateKhakiInfoForBarAvordDetailses.Include(x => x.NoeKhakBardari)
        //    .Where(x => x.AmalyateKhakiInfoForBarAvordId == AmalyateKhakiInfoForBarAvordId).Select(x => new AmalyateKhakiInfoForBarAvordDetailsDto
        //    {
        //        ID = x.ID,
        //        AmalyateKhakiInfoForBarAvordId = x.AmalyateKhakiInfoForBarAvordId,
        //        NoeKhakBardariId = x.NoeKhakBardariId,
        //        Title = x.NoeKhakBardari.Title,
        //        Value = x.Value
        //    }).ToList();



        List<AmalyateKhakiInfoForBarAvordDetailsDto> KMAmalyateKhakiBarAvordDetails = context.NoeKhakBardaris
.GroupJoin(
    context.AmalyateKhakiInfoForBarAvordDetailses
        .Where(x => x.AmalyateKhakiInfoForBarAvordId == AmalyateKhakiInfoForBarAvordId),
    noe => noe.Id,
    detail => detail.NoeKhakBardariId,
    (noe, details) => new { noe, details }
)
.SelectMany(
    x => x.details.DefaultIfEmpty(),
    (x, detail) => new AmalyateKhakiInfoForBarAvordDetailsDto
    {
        ID = detail != null ? detail.ID : (Guid?)null,
        AmalyateKhakiInfoForBarAvordId = detail != null ? detail.AmalyateKhakiInfoForBarAvordId : (Guid?)null,
        NoeKhakBardariId = x.noe.Id,
        Title = x.noe.Title,
        Value = detail != null ? detail.Value : (decimal?)null
    }
)
.ToList();


        List<Guid> gKMAId = KMAmalyateKhakiBarAvordDetails
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


        List<AKhIRizMetreDto> lstAKhInfoRizMetre = _context.AmalyateKhakiInfoForBarAvordDetailsRizMetres.Include(x => x.RizMetreUser).ThenInclude(x => x.FB)
              .Where(x => gKMAId.Contains(x.AmalyateKhakiInfoForBarAvordDetailsId)).Select(x => new AKhIRizMetreDto
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

        List<string> lstItemFBShomareh = lstAKhInfoRizMetre.Select(x => x.ItemFBShomareh).Distinct().ToList();

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

        //if (KMAmalyateKhakiBarAvordDetails.Count != 0)
        //{
        //    for (int i = 0; i < KMAmalyateKhakiBarAvordDetails.Count; i++)
        //    {
        //        gKMAId.Add(KMAmalyateKhakiBarAvordDetails[i].ID);
        //    }
        //}

        List<clsAmalyateKhakiInfoForBarAvordDetailsMore> KMAmalyateKhakiBarAvordDetailsMore = context.AmalyateKhakiInfoForBarAvordDetailsMores.Where(x => gKMAId.Contains(x.AmalyateKhakiInfoForBarAvordDetailsId)).ToList();
        //DataTable DtKMAmalyateKhakiBarAvordDetailsMore = clsConvert.ToDataTable(varKMAmalyateKhakiBarAvordDetailsMore);


        List<clsAmalyateKhakiInfoForBarAvordEzafeBaha> KMAmalyateKhakiBarAvordDetailsEzafeBaha = context.AmalyateKhakiInfoForBarAvordEzafeBahas.Where(x => gKMAId.Contains(x.AmalyateKhakiInfoForBarAvordId)).ToList();


        //DataSet Ds = new DataSet();

        //DtKMAmalyateKhakiBarAvordDetails.TableName = "tblKMAmalyateKhakiBarAvordDetails";
        //DtKMAmalyateKhakiBarAvordMore.TableName = "tblKMAmalyateKhakiBarAvordMore";
        //DtKMAmalyateKhakiBarAvordDetailsMore.TableName = "tblKMAmalyateKhakiBarAvordDetailsMore";
        //DtKMAmalyateKhakiBarAvordDetailsEzafeBaha.TableName = "tblKMAmalyateKhakiBarAvordDetailsEzafeBaha";

        //DtKMAmalyateKhakiBarAvordDetails.Columns.Remove("AmalyateKhakiInfoForBarAvord");
        //DtKMAmalyateKhakiBarAvordDetails.Columns.Remove("AmalyateKhakiInfoForBarAvordDetailsEzafeBahas");
        //DtKMAmalyateKhakiBarAvordDetails.Columns.Remove("AmalyateKhakiInfoForBarAvordDetailsMores");
        /////////////////
        //DtKMAmalyateKhakiBarAvordMore.Columns.Remove("AmalyateKhakiInfoForBarAvord");
        ///////////////
        //DtKMAmalyateKhakiBarAvordDetailsMore.Columns.Remove("AmalyateKhakiInfoForBarAvordDetails");
        /////////////
        //DtKMAmalyateKhakiBarAvordDetailsEzafeBaha.Columns.Remove("AmalyateKhakiInfoForBarAvordDetails");
        /////////////
        //Ds.Tables.Add(DtKMAmalyateKhakiBarAvordDetails);
        //Ds.Tables.Add(DtKMAmalyateKhakiBarAvordMore);
        //Ds.Tables.Add(DtKMAmalyateKhakiBarAvordDetailsMore);
        //Ds.Tables.Add(DtKMAmalyateKhakiBarAvordDetailsEzafeBaha);

        var results = new
        {
            KMAmalyateKhakiBarAvordDetails,
            KMAmalyateKhakiBarAvordMore,
            KMAmalyateKhakiBarAvordDetailsMore,
            KMAmalyateKhakiBarAvordDetailsEzafeBaha,
            lstAKhInfoRizMetre,
            lstItemFBShomarehForGet
        };
        return new JsonResult(results);
        //return Ds.GetXml();
    }

    public JsonResult GetEzafeBaha([FromBody] GetEzafeBahaForAKhDto request)
    {
        List<clsAmalyateKhakiInfoForBarAvordDetails> lstAKIForBD =
            _context.AmalyateKhakiInfoForBarAvordDetailses.Include(x => x.AmalyateKhakiInfoForBarAvord)
            .Where(x => x.AmalyateKhakiInfoForBarAvordId == request.AmalyateKhakiInfoForBarAvordId).ToList();

        List<long> lstNoeKhakBardariId = lstAKIForBD.Select(x => x.NoeKhakBardariId).ToList();

        List<NoeKhakBardariEzafeBahaDto> lstNoeKhakBardariEzafeBaha
            = _context.NoeKhakBardari_NoeKhakBardariEzafeBahas.Where(x => lstNoeKhakBardariId.Contains(x.NoeKhakBardariId))
            .Include(x => x.NoeKhakBardariEzafeBaha).Select(x => new NoeKhakBardariEzafeBahaDto
            {
                Id = x.NoeKhakBardariEzafeBaha.Id,
                NoeKhakBardariEzafeBaha = x.NoeKhakBardariEzafeBaha.Title,
                hasEnteringValue = x.NoeKhakBardariEzafeBaha.hasEnteringValue,
                CountForEnteringValue = x.NoeKhakBardariEzafeBaha.CountForEnteringValue,
                DefaultForEnteringValue = x.NoeKhakBardariEzafeBaha.DefaultForEnteringValue,
                DesForEnteringValue = x.NoeKhakBardariEzafeBaha.DesForEnteringValue
            }).Distinct().ToList();

        List<clsAmalyateKhakiInfoForBarAvordEzafeBaha> lstAKhForBEB =
            _context.AmalyateKhakiInfoForBarAvordEzafeBahas.Where(x => x.AmalyateKhakiInfoForBarAvordId == request.AmalyateKhakiInfoForBarAvordId).ToList();

        var Result = new
        {
            lstNoeKhakBardariEzafeBaha,
            lstAKhForBEB
        };

        return new JsonResult(Result);
    }

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


    public JsonResult SaveEzafeBahaAKh([FromBody] SaveEzafeBahaAKhDto request)
    {

        AmalyateKhakiCommon common = new AmalyateKhakiCommon();
        if (common.SaveEzafeBahaAKh(request, _context))
        {
            _context.SaveChanges();
            return new JsonResult("OK");
        }
        else
            return new JsonResult("نوع اضافه بها یافت نشد");


        //clsNoeKhakBardariEzafeBaha? NoeKhB_EB = _context.NoeKhakBardariEzafeBahas.FirstOrDefault(x => x.Id == request.NoeKhakBardariEzafeBahaId);
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

        //List<long> noeKhB = _context.NoeKhakBardari_NoeKhakBardariEzafeBahas
        //      .Where(x => x.NoeKhakBardariEzafeBahaId == request.NoeKhakBardariEzafeBahaId).Select(x => x.NoeKhakBardariId).ToList();


        //List<AmalyateKhakiInfoForBarAvordDetailsInsertedDto> lstAKhForBD =
        //    _context.AmalyateKhakiInfoForBarAvordDetailsRizMetres.Include(x => x.AmalyateKhakiInfoForBarAvordDetails).ThenInclude(x => x.lstAmalyateKhakiInfoForBarAvordDetailsMore)
        //     .Where(x => x.AmalyateKhakiInfoForBarAvordDetails.AmalyateKhakiInfoForBarAvordId == request.AmalyateKhakiInfoForBarAvordId
        //                 && noeKhB.Contains(x.AmalyateKhakiInfoForBarAvordDetails.NoeKhakBardariId))
        //     .Select(x => new AmalyateKhakiInfoForBarAvordDetailsInsertedDto
        //     {
        //         RizMetreId = x.RizMetreUserId,
        //         //AmalyateKhakiInfoForBarAvordDetailId = x.AmalyateKhakiInfoForBarAvordDetailsId,
        //         //AmalyateKhakiInfoForBarAvordId = x.AmalyateKhakiInfoForBarAvordDetails.AmalyateKhakiInfoForBarAvordId,
        //         NoeKhakBardariId = x.AmalyateKhakiInfoForBarAvordDetails.NoeKhakBardariId,
        //         lstAmalyateKhakiInfoForBarAvordDetailsMore = x.AmalyateKhakiInfoForBarAvordDetails.lstAmalyateKhakiInfoForBarAvordDetailsMore.ToList()
        //     }).ToList();



        //clsAmalyateKhakiInfoForBarAvordEzafeBaha amalyateKhakiInfoForBarAvordEzafeBaha = new clsAmalyateKhakiInfoForBarAvordEzafeBaha
        //{
        //    AmalyateKhakiInfoForBarAvordId = request.AmalyateKhakiInfoForBarAvordId,
        //    NoeKhakBardariEzafeBahaId = NoeKhB_EB.Id,
        //    InsertDateTime = Now
        //};
        //_context.AmalyateKhakiInfoForBarAvordEzafeBahas.Add(amalyateKhakiInfoForBarAvordEzafeBaha);

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
        //    clsAmalyateKhakiInfoForBarAvordDetailsMore? AmalyateKhakiInfoForBarAvordDetailsMore = null;
        //    switch (strCondition)
        //    {
        //        case "z*m":
        //            {
        //                AmalyateKhakiInfoForBarAvordDetailsMore =
        //                                    item.lstAmalyateKhakiInfoForBarAvordDetailsMore.FirstOrDefault(x => x.Name.ToLower() == "haml");
        //                if (AmalyateKhakiInfoForBarAvordDetailsMore != null)
        //                {
        //                    MeghdarJoz = AmalyateKhakiInfoForBarAvordDetailsMore.Value;
        //                }
        //                break;
        //            }
        //        case "(z+y)*m":
        //            {
        //                decimal? d1 = null;
        //                decimal? d2 = null;
        //                AmalyateKhakiInfoForBarAvordDetailsMore =
        //                             item.lstAmalyateKhakiInfoForBarAvordDetailsMore.FirstOrDefault(x => x.Name.ToLower() == "haml");
        //                if (AmalyateKhakiInfoForBarAvordDetailsMore != null)
        //                {
        //                    d1 = AmalyateKhakiInfoForBarAvordDetailsMore.Value;
        //                }

        //                AmalyateKhakiInfoForBarAvordDetailsMore =
        //                             item.lstAmalyateKhakiInfoForBarAvordDetailsMore.FirstOrDefault(x => x.Name.ToLower() == "reusehajm");
        //                if (AmalyateKhakiInfoForBarAvordDetailsMore != null)
        //                {
        //                    d2 = AmalyateKhakiInfoForBarAvordDetailsMore.Value;
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

        //    clsAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre AmalyateKhakiInfoForBarAvordEzafeBahaRizMetre
        //        = new clsAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre
        //        {
        //            AmalyateKhakiInfoForBarAvordEzafeBahaId = amalyateKhakiInfoForBarAvordEzafeBaha.ID,
        //            RizMetreUserId = RizMetre.ID
        //        };
        //    _context.AmalyateKhakiInfoForBarAvordEzafeBahaRizMetres.Add(AmalyateKhakiInfoForBarAvordEzafeBahaRizMetre);
        //}
        //_context.SaveChanges();
    }

    public JsonResult GetAKh_EzafeBaha([FromBody] GetAKh_EzafeBahaDto request)
    {
        long Year = request.Year;
        NoeFehrestBaha NoeFB = request.NoeFB;

        Guid AmalyateKhakiInfoForBarAvordId = request.AmalyateKhakiInfoForBarAvordId;

        List<ItemFBShomarehForGetAndShowAddItemsFieldsDto> lstItemFields = _context.ItemsFieldses.Where(x => x.NoeFB == NoeFB).Select(x => new ItemFBShomarehForGetAndShowAddItemsFieldsDto
        {
            Shomareh = x.ItemShomareh,
            FieldType = x.FieldType,
            Vahed = x.Vahed,
            IsEnteringValue = x.IsEnteringValue
        }).ToList();

        List<AKhI_EzafeBahaRizMetreDto> lstAKhInfoRizMetre = _context.AmalyateKhakiInfoForBarAvordEzafeBahaRizMetres
            .Include(x => x.AmalyateKhakiInfoForBarAvordEzafeBaha).ThenInclude(x => x.NoeKhakBardariEzafeBaha)
            .Where(x => x.AmalyateKhakiInfoForBarAvordEzafeBaha.AmalyateKhakiInfoForBarAvordId == AmalyateKhakiInfoForBarAvordId
                   && x.AmalyateKhakiInfoForBarAvordEzafeBaha.NoeKhakBardariEzafeBahaId == request.NoeKhakBardariEzafeBahaId)
            .Select(x => new AKhI_EzafeBahaRizMetreDto
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
                hasDelButton = x.AmalyateKhakiInfoForBarAvordEzafeBaha.NoeKhakBardariEzafeBaha.EnableDeleting,
                hasEditButton = x.AmalyateKhakiInfoForBarAvordEzafeBaha.NoeKhakBardariEzafeBaha.EnableEditing,
            }).ToList();

        List<string> lstItemFBShomareh = lstAKhInfoRizMetre.Select(x => x.ItemFBShomareh).Distinct().ToList();

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
            lstAKhInfoRizMetre,
            lstItemFBShomarehForGet
        };

        return new JsonResult(result);

    }

    public JsonResult DeleteEzafeBahaAKh([FromBody] DeleteEzafeBahaAKhDto request)
    {
        List<long> noeKhB = _context.NoeKhakBardari_NoeKhakBardariEzafeBahas
                            .Where(x => x.NoeKhakBardariEzafeBahaId == request.NoeKhakBardariEzafeBahaId).Select(x => x.NoeKhakBardariId).ToList();

        clsAmalyateKhakiInfoForBarAvordEzafeBaha? amalyateKhakiInfoForBarAvordEzafeBaha =
            _context.AmalyateKhakiInfoForBarAvordEzafeBahas.FirstOrDefault(x => x.NoeKhakBardariEzafeBahaId == request.NoeKhakBardariEzafeBahaId
                                                                        && x.AmalyateKhakiInfoForBarAvordId == x.AmalyateKhakiInfoForBarAvordId);

        //List<AmalyateKhakiInfoForBarAvordDetailsInsertedDto> lstAKhForBDRizmetre =
        //    _context.AmalyateKhakiInfoForBarAvordDetailsRizMetres.Include(x => x.AmalyateKhakiInfoForBarAvordDetails)
        //     .Where(x => x.AmalyateKhakiInfoForBarAvordDetails.AmalyateKhakiInfoForBarAvordId == request.AmalyateKhakiInfoForBarAvordId
        //                 && noeKhB.Contains(x.AmalyateKhakiInfoForBarAvordDetails.NoeKhakBardariId))
        //     .Select(x => new AmalyateKhakiInfoForBarAvordDetailsInsertedDto
        //     {
        //         RizMetreId = x.RizMetreUserId,
        //         AmalyateKhakiInfoForBarAvordDetailId = x.AmalyateKhakiInfoForBarAvordDetailsId,
        //         AmalyateKhakiInfoForBarAvordId = x.AmalyateKhakiInfoForBarAvordDetails.AmalyateKhakiInfoForBarAvordId,
        //         NoeKhakBardariId = x.AmalyateKhakiInfoForBarAvordDetails.NoeKhakBardariId
        //     }).ToList();

        //List<Guid> lstAmalyateKhakiInfoForBarAvordIds = lstAKhForBDRizmetre.Select(x => x.AmalyateKhakiInfoForBarAvordId).ToList();

        //List<clsAmalyateKhakiInfoForBarAvordEzafeBaha> lstAKhForBE =
        //    _context.AmalyateKhakiInfoForBarAvordEzafeBahas.Where(x => lstAmalyateKhakiInfoForBarAvordIds.Contains(x.AmalyateKhakiInfoForBarAvordId)).ToList();

        //_context.AmalyateKhakiInfoForBarAvordEzafeBahas.RemoveRange(lstAKhForBE);

        if (amalyateKhakiInfoForBarAvordEzafeBaha != null)
        {
            _context.AmalyateKhakiInfoForBarAvordEzafeBahas.Remove(amalyateKhakiInfoForBarAvordEzafeBaha);


            List<clsAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre> lstlstAKhForBERizMetre =
                _context.AmalyateKhakiInfoForBarAvordEzafeBahaRizMetres.Where(x => x.AmalyateKhakiInfoForBarAvordEzafeBahaId == amalyateKhakiInfoForBarAvordEzafeBaha.ID).ToList();

            _context.AmalyateKhakiInfoForBarAvordEzafeBahaRizMetres.RemoveRange(lstlstAKhForBERizMetre);
        }
        _context.SaveChanges();

        return new JsonResult("OK");
    }

    public JsonResult OverLowKMCheck(OverLowKMCheckDto request)
    {
        List<clsAmalyateKhakiInfoForBarAvord> lstAKhInfo = _context.AmalyateKhakiInfoForBarAvords.Where(x => x.BaravordUserId == request.BarAvordId).ToList();

        long KMS = request.KMS;
        long KME = request.KME;

        List<OverLowKMCheckK_Start_EndDto> lstKMS = 
            lstAKhInfo.Select(x => new OverLowKMCheckK_Start_EndDto
        {
            KStart=long.Parse(x.FromKM),
            KEnd=long.Parse(x.ToKM)
        }).ToList();

        var overlaps = lstKMS.Where(r =>
            Math.Max(r.KStart, KMS) <= Math.Min(r.KEnd, KME)
        ).ToList();

        return new JsonResult("OK");
    }
}
