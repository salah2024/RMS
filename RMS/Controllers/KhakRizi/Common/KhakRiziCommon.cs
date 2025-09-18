using RMS.Controllers.KhakRizi.Dto;
using RMS.Models.Common;
using RMS.Models.Entity;
using System.Data;
using System.Reflection.Metadata;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.KhakRizi.Common;

public class KhakRiziCommon
{
    public bool SaveRizMetreKhakRizi(RequestSaveRizMetreKhakRiziDto request, ApplicationDbContext _context)
    {
        Guid BaravordUserId = request.BarAvordUserID;
        Guid KMId = request.KMId;
        int KMNum = request.KMNum;
        string KMS = request.KMS;
        string KME = request.KME;
        short radioNoeRahKhakRizi = request.radioNoeRahKhakRizi;
        decimal DarsadKRDDaneh = request.DarsadKRRDaneh;
        decimal DarsadKRRDaneh = request.DarsadKRRDaneh;
        decimal HajmBetween0To30 = request.HajmBetween0To30;
        decimal HajmBetween30To100 = request.HajmBetween30To100;
        decimal HajmBetweenTo100 = request.HajmBetweenTo100;
        bool EzafeBahaKRKhakMosalah = request.EzafeBahaKRKhakMosalah;

        //DataTable DtFBUser = new DataTable();
        //DataTable DtLastRizMetreUsersShomareh = new DataTable();
        //clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        //int Shomareh = 0;
        //int FBId = 0;
        if (radioNoeRahKhakRizi == 1)
        {
            if (HajmBetween0To30 != 0)
            {
                if (DarsadKRDDaneh != 0)
                {
                    clsFB? FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BaravordUserId && x.Shomareh == "031104");

                    Guid FBId = new Guid();
                    if (FBUser == null)
                    {
                        clsFB FBSave = new clsFB();
                        FBSave.BarAvordId = BaravordUserId;
                        FBSave.Shomareh = "031104";
                        FBSave.BahayeVahedZarib = 0;
                        _context.FBs.Add(FBSave);
                        //rmsContext.SaveChanges();
                        FBId = FBSave.ID;
                        //FBId = Operation_ItemsFB.SaveFB(BarAvordID, "031104", 0);
                    }
                    else
                        FBId = FBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["_ID"].ToString());

                    long Shomareh = 1;
                    clsRizMetreUsers? RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                    if (RizMetreUser != null)
                        Shomareh = RizMetreUser.Shomareh + 1;
                    else
                        Shomareh = 1;
                    //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                    //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());

                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh;
                    RizMetre.Sharh = "پخـش، آب پاشي، تسطيح، پروفيله کردن، رگلاژ و کوبيدن قشرهاي خاکريزي و تونان با 100 درصد کوبيدگي";
                    RizMetre.Tedad = DarsadKRDDaneh;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = HajmBetween0To30;
                    RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 0;
                    RizMetre.Type = "420" + KMNum.ToString("D3") + "00";///پخـش، آب پاشي، تسطيح، پروفيله کردن، رگلاژ و کوبيدن قشرهاي خاکريزي و تونان با 100 درصد کوبيدگي
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    //rmsContext.SaveChanges();
                    //RizMetre.Save();

                    if (EzafeBahaKRKhakMosalah)
                    {

                        FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BaravordUserId && x.Shomareh == "031107");

                        FBId = new Guid();
                        if (FBUser == null)
                        {
                            clsFB FBSave = new clsFB();
                            FBSave.BarAvordId = BaravordUserId;
                            FBSave.Shomareh = "031107";
                            FBSave.BahayeVahedZarib = 0;
                            _context.FBs.Add(FBSave);
                            //rmsContext.SaveChanges();
                            FBId = FBSave.ID;
                        }
                        else
                            FBId = FBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["_ID"].ToString());


                        //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='031107'");
                        //FBId = 0;
                        //if (DtFBUser.Rows.Count == 0)
                        //    FBId = Operation_ItemsFB.SaveFB(BarAvordID, "031107", 0);
                        //else
                        //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

                        //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                        //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());

                        RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                        if (RizMetreUser != null)
                            Shomareh = RizMetreUser.Shomareh + 1;
                        else
                            Shomareh = 1;

                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh;
                        RizMetre.Sharh = "اضافه بها مسلح کردن خاک به آیتم 031107";
                        RizMetre.Tedad = DarsadKRDDaneh;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = 0;
                        RizMetre.Ertefa = 0;
                        RizMetre.Vazn = HajmBetween0To30;
                        RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                        RizMetre.FBId = FBId;
                        RizMetre.OperationsOfHamlId = 0;
                        RizMetre.Type = "420" + KMNum.ToString("D3") + "10";///اضافه بها مسلح کردن خاک به آیتم 031107
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        //rmsContext.SaveChanges();
                        //RizMetre.Save();
                    }
                }

                if (DarsadKRRDaneh != 0)
                {


                    clsFB? FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BaravordUserId && x.Shomareh == "031103");

                    Guid FBId = new Guid();
                    if (FBUser == null)
                    {
                        clsFB FBSave = new clsFB();
                        FBSave.BarAvordId = BaravordUserId;
                        FBSave.Shomareh = "031107";
                        FBSave.BahayeVahedZarib = 0;
                        _context.FBs.Add(FBSave);
                        //rmsContext.SaveChanges();
                        FBId = FBSave.ID;
                    }
                    else
                        FBId = FBUser.ID;


                    //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='031103'");
                    //clsOperation_ItemsFB Operation_ItemsFB = new clsOperation_ItemsFB();
                    //FBId = 0;
                    //if (DtFBUser.Rows.Count == 0)
                    //    FBId = Operation_ItemsFB.SaveFB(BarAvordID, "031103", 0);
                    //else
                    //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

                    long Shomareh = 1;
                    clsRizMetreUsers? RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                    if (RizMetreUser != null)
                        Shomareh = RizMetreUser.Shomareh + 1;
                    else
                        Shomareh = 1;

                    //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                    //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh;
                    RizMetre.Sharh = "پخـش، آب پاشي، تسطيح، پروفيله کردن، رگلاژ و کوبيدن قشرهاي خاکريزي و تونان با 95 درصد کوبيدگي";
                    RizMetre.Tedad = DarsadKRDDaneh;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = HajmBetween0To30;
                    RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 0;
                    RizMetre.Type = "420" + KMNum.ToString("D3") + "01";///پخـش، آب پاشي، تسطيح، پروفيله کردن، رگلاژ و کوبيدن قشرهاي خاکريزي و تونان با 95 درصد کوبيدگي
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    //rmsContext.SaveChanges();
                    //RizMetre.Save();

                    if (EzafeBahaKRKhakMosalah)
                    {

                        FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BaravordUserId && x.Shomareh == "031107");

                        FBId = new Guid();
                        if (FBUser == null)
                        {
                            clsFB FBSave = new clsFB();
                            FBSave.BarAvordId = BaravordUserId;
                            FBSave.Shomareh = "031107";
                            FBSave.BahayeVahedZarib = 0;
                            _context.FBs.Add(FBSave);
                            //rmsContext.SaveChanges();
                            FBId = FBSave.ID;
                        }
                        else
                            FBId = FBUser.ID;


                        //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='031107'");
                        //FBId = 0;
                        //if (DtFBUser.Rows.Count == 0)
                        //    FBId = Operation_ItemsFB.SaveFB(BarAvordID, "031107", 0);
                        //else
                        //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

                        RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                        if (RizMetreUser != null)
                            Shomareh = RizMetreUser.Shomareh + 1;
                        else
                            Shomareh = 1;

                        //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                        //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh;
                        RizMetre.Sharh = "اضافه بها مسلح کردن خاک به آیتم 031107";
                        RizMetre.Tedad = DarsadKRDDaneh;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = 0;
                        RizMetre.Ertefa = 0;
                        RizMetre.Vazn = HajmBetween0To30;
                        RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                        RizMetre.FBId = FBId;
                        RizMetre.OperationsOfHamlId = 0;
                        RizMetre.Type = "420" + KMNum.ToString("D3") + "10";///اضافه بها مسلح کردن خاک به آیتم 031107
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        //rmsContext.SaveChanges();
                        //RizMetre.Save();
                    }
                }
            }

            if (HajmBetween30To100 != 0)
            {
                if (DarsadKRDDaneh != 0)
                {



                    clsFB? FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BaravordUserId && x.Shomareh == "031103");

                    Guid FBId = new Guid();
                    if (FBUser == null)
                    {
                        clsFB FBSave = new clsFB();
                        FBSave.BarAvordId = BaravordUserId;
                        FBSave.Shomareh = "031103";
                        FBSave.BahayeVahedZarib = 0;
                        _context.FBs.Add(FBSave);
                        //rmsContext.SaveChanges();
                        FBId = FBSave.ID;
                    }
                    else
                        FBId = FBUser.ID;

                    //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='031103'");
                    //clsOperation_ItemsFB Operation_ItemsFB = new clsOperation_ItemsFB();
                    //FBId = 0;
                    //if (DtFBUser.Rows.Count == 0)
                    //    FBId = Operation_ItemsFB.SaveFB(BarAvordID, "031103", 0);
                    //else
                    //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

                    long Shomareh = 1;
                    clsRizMetreUsers? RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                    if (RizMetreUser != null)
                        Shomareh = RizMetreUser.Shomareh + 1;
                    else
                        Shomareh = 1;

                    //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                    //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh;
                    RizMetre.Sharh = "پخـش، آب پاشي، تسطيح، پروفيله کردن، رگلاژ و کوبيدن قشرهاي خاکريزي و تونان با 95 درصد کوبيدگي";
                    RizMetre.Tedad = DarsadKRDDaneh;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = HajmBetween30To100;
                    RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 0;
                    RizMetre.Type = "420" + KMNum.ToString("D3") + "01";///پخـش، آب پاشي، تسطيح، پروفيله کردن، رگلاژ و کوبيدن قشرهاي خاکريزي و تونان با 95 درصد کوبيدگي
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    //rmsContext.SaveChanges();
                    //RizMetre.Save();

                    if (EzafeBahaKRKhakMosalah)
                    {


                        FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BaravordUserId && x.Shomareh == "031107");

                        FBId = new Guid();
                        if (FBUser == null)
                        {
                            clsFB FBSave = new clsFB();
                            FBSave.BarAvordId = BaravordUserId;
                            FBSave.Shomareh = "031107";
                            FBSave.BahayeVahedZarib = 0;
                            _context.FBs.Add(FBSave);
                            //rmsContext.SaveChanges();
                            FBId = FBSave.ID;
                        }
                        else
                            FBId = FBUser.ID;


                        RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                        if (RizMetreUser != null)
                            Shomareh = RizMetreUser.Shomareh + 1;
                        else
                            Shomareh = 1;

                        //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                        //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh;
                        RizMetre.Sharh = "اضافه بها مسلح کردن خاک به آیتم 031107";
                        RizMetre.Tedad = DarsadKRDDaneh;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = 0;
                        RizMetre.Ertefa = 0;
                        RizMetre.Vazn = HajmBetween0To30;
                        RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                        RizMetre.FBId = FBId;
                        RizMetre.OperationsOfHamlId = 0;
                        RizMetre.Type = "420" + KMNum.ToString("D3") + "10";///اضافه بها مسلح کردن خاک به آیتم 031107
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        //rmsContext.SaveChanges();
                        //RizMetre.Save();
                    }
                }

                if (DarsadKRRDaneh != 0)
                {


                    clsFB? FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BaravordUserId && x.Shomareh == "031102");

                    Guid FBId = new Guid();
                    if (FBUser == null)
                    {
                        clsFB FBSave = new clsFB();
                        FBSave.BarAvordId = BaravordUserId;
                        FBSave.Shomareh = "031102";
                        FBSave.BahayeVahedZarib = 0;
                        _context.FBs.Add(FBSave);
                        //rmsContext.SaveChanges();
                        FBId = FBSave.ID;
                    }
                    else
                        FBId = FBUser.ID;



                    long Shomareh = 1;
                    clsRizMetreUsers? RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                    if (RizMetreUser != null)
                        Shomareh = RizMetreUser.Shomareh + 1;
                    else
                        Shomareh = 1;

                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh;
                    RizMetre.Sharh = "پخـش، آب پاشي، تسطيح، پروفيله کردن، رگلاژ و کوبيدن قشرهاي خاکريزي و تونان با 90 درصد کوبيدگي";
                    RizMetre.Tedad = DarsadKRDDaneh;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = HajmBetween30To100;
                    RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 0;
                    RizMetre.Type = "420" + KMNum.ToString("D3") + "02";///پخـش، آب پاشي، تسطيح، پروفيله کردن، رگلاژ و کوبيدن قشرهاي خاکريزي و تونان با 90 درصد کوبيدگي
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    //rmsContext.SaveChanges();
                    //RizMetre.Save();

                    if (EzafeBahaKRKhakMosalah)
                    {


                        FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BaravordUserId && x.Shomareh == "031107");

                        FBId = new Guid();
                        if (FBUser == null)
                        {
                            clsFB FBSave = new clsFB();
                            FBSave.BarAvordId = BaravordUserId;
                            FBSave.Shomareh = "031107";
                            FBSave.BahayeVahedZarib = 0;
                            _context.FBs.Add(FBSave);
                            //rmsContext.SaveChanges();
                            FBId = FBSave.ID;
                        }
                        else
                            FBId = FBUser.ID;


                        RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                        if (RizMetreUser != null)
                            Shomareh = RizMetreUser.Shomareh + 1;
                        else
                            Shomareh = 1;

                        //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                        //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh;
                        RizMetre.Sharh = "اضافه بها مسلح کردن خاک به آیتم 031102";
                        RizMetre.Tedad = DarsadKRDDaneh;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = 0;
                        RizMetre.Ertefa = 0;
                        RizMetre.Vazn = HajmBetween0To30;
                        RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                        RizMetre.FBId = FBId;
                        RizMetre.OperationsOfHamlId = 0;
                        RizMetre.Type = "420" + KMNum.ToString("D3") + "10";///اضافه بها مسلح کردن خاک به آیتم 031102
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        //rmsContext.SaveChanges();
                        //RizMetre.Save();
                    }
                }
            }
        }
        else if (radioNoeRahKhakRizi == 2)
        {
            if (HajmBetween0To30 != 0)
            {
                if (DarsadKRDDaneh != 0)
                {


                    clsFB? FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BaravordUserId && x.Shomareh == "031103");

                    Guid FBId = new Guid();
                    if (FBUser == null)
                    {
                        clsFB FBSave = new clsFB();
                        FBSave.BarAvordId = BaravordUserId;
                        FBSave.Shomareh = "031107";
                        FBSave.BahayeVahedZarib = 0;
                        _context.FBs.Add(FBSave);
                        //rmsContext.SaveChanges();
                        FBId = FBSave.ID;
                    }
                    else
                        FBId = FBUser.ID;

                    long Shomareh = 1;
                    clsRizMetreUsers? RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                    if (RizMetreUser != null)
                        Shomareh = RizMetreUser.Shomareh + 1;
                    else
                        Shomareh = 1;

                    //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                    //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());

                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh;
                    RizMetre.Sharh = "پخـش، آب پاشي، تسطيح، پروفيله کردن، رگلاژ و کوبيدن قشرهاي خاکريزي و تونان با 95 درصد کوبيدگي";
                    RizMetre.Tedad = DarsadKRDDaneh;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = HajmBetween0To30;
                    RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 0;
                    RizMetre.Type = "420" + KMNum.ToString("D3") + "01";///پخـش، آب پاشي، تسطيح، پروفيله کردن، رگلاژ و کوبيدن قشرهاي خاکريزي و تونان با 95 درصد کوبيدگي
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    //rmsContext.SaveChanges();
                    //RizMetre.Save();

                    if (EzafeBahaKRKhakMosalah)
                    {


                        FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BaravordUserId && x.Shomareh == "031107");

                        FBId = new Guid();
                        if (FBUser == null)
                        {
                            clsFB FBSave = new clsFB();
                            FBSave.BarAvordId = BaravordUserId;
                            FBSave.Shomareh = "031107";
                            FBSave.BahayeVahedZarib = 0;
                            _context.FBs.Add(FBSave);
                            //rmsContext.SaveChanges();
                            FBId = FBSave.ID;
                        }
                        else
                            FBId = FBUser.ID;

                        RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                        if (RizMetreUser != null)
                            Shomareh = RizMetreUser.Shomareh + 1;
                        else
                            Shomareh = 1;

                        //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                        //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh;
                        RizMetre.Sharh = "اضافه بها مسلح کردن خاک به آیتم 031107";
                        RizMetre.Tedad = DarsadKRDDaneh;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = 0;
                        RizMetre.Ertefa = 0;
                        RizMetre.Vazn = HajmBetween0To30;
                        RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                        RizMetre.FBId = FBId;
                        RizMetre.OperationsOfHamlId = 0;
                        RizMetre.Type = "420" + KMNum.ToString("D3") + "10";///اضافه بها مسلح کردن خاک به آیتم 031107
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        //rmsContext.SaveChanges();
                        //RizMetre.Save();
                    }
                }

                if (DarsadKRRDaneh != 0)
                {


                    clsFB? FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BaravordUserId && x.Shomareh == "031102");

                    Guid FBId = new Guid();
                    if (FBUser == null)
                    {
                        clsFB FBSave = new clsFB();
                        FBSave.BarAvordId = BaravordUserId;
                        FBSave.Shomareh = "031102";
                        FBSave.BahayeVahedZarib = 0;
                        _context.FBs.Add(FBSave);
                        //rmsContext.SaveChanges();
                        FBId = FBSave.ID;
                    }
                    else
                        FBId = FBUser.ID;

                    long Shomareh = 1;
                    clsRizMetreUsers? RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                    if (RizMetreUser != null)
                        Shomareh = RizMetreUser.Shomareh + 1;
                    else
                        Shomareh = 1;

                    //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                    //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh;
                    RizMetre.Sharh = "پخـش، آب پاشي، تسطيح، پروفيله کردن، رگلاژ و کوبيدن قشرهاي خاکريزي و تونان با 90 درصد کوبيدگي";
                    RizMetre.Tedad = DarsadKRDDaneh;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = HajmBetween0To30;
                    RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 0;
                    RizMetre.Type = "420" + KMNum.ToString("D3") + "02";///پخـش، آب پاشي، تسطيح، پروفيله کردن، رگلاژ و کوبيدن قشرهاي خاکريزي و تونان با 90 درصد کوبيدگي
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    //rmsContext.SaveChanges();
                    //RizMetre.Save();

                    if (EzafeBahaKRKhakMosalah)
                    {

                        FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BaravordUserId && x.Shomareh == "031107");

                        FBId = new Guid();
                        if (FBUser == null)
                        {
                            clsFB FBSave = new clsFB();
                            FBSave.BarAvordId = BaravordUserId;
                            FBSave.Shomareh = "031107";
                            FBSave.BahayeVahedZarib = 0;
                            _context.FBs.Add(FBSave);
                            //rmsContext.SaveChanges();
                            FBId = FBSave.ID;
                        }
                        else
                            FBId = FBUser.ID;


                        RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                        if (RizMetreUser != null)
                            Shomareh = RizMetreUser.Shomareh + 1;
                        else
                            Shomareh = 1;

                        //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                        //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh;
                        RizMetre.Sharh = "اضافه بها مسلح کردن خاک به آیتم 031107";
                        RizMetre.Tedad = DarsadKRDDaneh;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = 0;
                        RizMetre.Ertefa = 0;
                        RizMetre.Vazn = HajmBetween0To30;
                        RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                        RizMetre.FBId = FBId;
                        RizMetre.OperationsOfHamlId = 0;
                        RizMetre.Type = "420" + KMNum.ToString("D3") + "10";///اضافه بها مسلح کردن خاک به آیتم 031107
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        //rmsContext.SaveChanges();
                        //RizMetre.Save();
                    }
                }
            }

            if (HajmBetween30To100 != 0)
            {
                if (DarsadKRDDaneh != 0)
                {

                    clsFB? FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BaravordUserId && x.Shomareh == "031102");

                    Guid FBId = new Guid();
                    if (FBUser == null)
                    {
                        clsFB FBSave = new clsFB();
                        FBSave.BarAvordId = BaravordUserId;
                        FBSave.Shomareh = "031102";
                        FBSave.BahayeVahedZarib = 0;
                        _context.FBs.Add(FBSave);
                        //rmsContext.SaveChanges();
                        FBId = FBSave.ID;
                    }
                    else
                        FBId = FBUser.ID;

                    long Shomareh = 1;
                    clsRizMetreUsers? RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                    if (RizMetreUser != null)
                        Shomareh = RizMetreUser.Shomareh + 1;
                    else
                        Shomareh = 1;

                    //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                    //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh;
                    RizMetre.Sharh = "پخـش، آب پاشي، تسطيح، پروفيله کردن، رگلاژ و کوبيدن قشرهاي خاکريزي و تونان با 90 درصد کوبيدگي";
                    RizMetre.Tedad = DarsadKRDDaneh;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = HajmBetween30To100;
                    RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 0;
                    RizMetre.Type = "420" + KMNum.ToString("D3") + "02";///پخـش، آب پاشي، تسطيح، پروفيله کردن، رگلاژ و کوبيدن قشرهاي خاکريزي و تونان با 90 درصد کوبيدگي
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    //rmsContext.SaveChanges();
                    //RizMetre.Save();

                    if (EzafeBahaKRKhakMosalah)
                    {


                        FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BaravordUserId && x.Shomareh == "031107");

                        FBId = new Guid();
                        if (FBUser == null)
                        {
                            clsFB FBSave = new clsFB();
                            FBSave.BarAvordId = BaravordUserId;
                            FBSave.Shomareh = "031107";
                            FBSave.BahayeVahedZarib = 0;
                            _context.FBs.Add(FBSave);
                            //rmsContext.SaveChanges();
                            FBId = FBSave.ID;
                        }
                        else
                            FBId = FBUser.ID;

                        RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                        if (RizMetreUser != null)
                            Shomareh = RizMetreUser.Shomareh + 1;
                        else
                            Shomareh = 1;

                        //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                        //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh;
                        RizMetre.Sharh = "اضافه بها مسلح کردن خاک به آیتم 031107";
                        RizMetre.Tedad = DarsadKRDDaneh;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = 0;
                        RizMetre.Ertefa = 0;
                        RizMetre.Vazn = HajmBetween0To30;
                        RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                        RizMetre.FBId = FBId;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "420" + KMNum.ToString("D3") + "10";///اضافه بها مسلح کردن خاک به آیتم 031107
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        //rmsContext.SaveChanges();
                        //RizMetre.Save();
                    }
                }

                if (DarsadKRRDaneh != 0)
                {


                    clsFB? FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BaravordUserId && x.Shomareh == "031101");

                    Guid FBId = new Guid();
                    if (FBUser == null)
                    {
                        clsFB FBSave = new clsFB();
                        FBSave.BarAvordId = BaravordUserId;
                        FBSave.Shomareh = "031101";
                        FBSave.BahayeVahedZarib = 0;
                        _context.FBs.Add(FBSave);
                        //rmsContext.SaveChanges();
                        FBId = FBSave.ID;
                    }
                    else
                        FBId = FBUser.ID;


                    long Shomareh = 1;
                    clsRizMetreUsers? RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                    if (RizMetreUser != null)
                        Shomareh = RizMetreUser.Shomareh + 1;
                    else
                        Shomareh = 1;

                    //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                    //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh;
                    RizMetre.Sharh = "پخـش، آب پاشي، تسطيح، پروفيله کردن، رگلاژ و کوبيدن قشرهاي خاکريزي و تونان با 85 درصد کوبيدگي";
                    RizMetre.Tedad = DarsadKRDDaneh;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = HajmBetween30To100;
                    RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "420" + KMNum.ToString("D3") + "03";///پخـش، آب پاشي، تسطيح، پروفيله کردن، رگلاژ و کوبيدن قشرهاي خاکريزي و تونان با 85 درصد کوبيدگي
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    //rmsContext.SaveChanges();
                    //RizMetre.Save();

                    if (EzafeBahaKRKhakMosalah)
                    {

                        FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BaravordUserId && x.Shomareh == "031107");

                        FBId = new Guid();
                        if (FBUser == null)
                        {
                            clsFB FBSave = new clsFB();
                            FBSave.BarAvordId = BaravordUserId;
                            FBSave.Shomareh = "031107";
                            FBSave.BahayeVahedZarib = 0;
                            _context.FBs.Add(FBSave);
                            //rmsContext.SaveChanges();
                            FBId = FBSave.ID;
                        }
                        else
                            FBId = FBUser.ID;

                        RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                        if (RizMetreUser != null)
                            Shomareh = RizMetreUser.Shomareh + 1;
                        else
                            Shomareh = 1;

                        //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                        //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                        RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh;
                        RizMetre.Sharh = "اضافه بها مسلح کردن خاک به آیتم 031107";
                        RizMetre.Tedad = DarsadKRDDaneh;
                        RizMetre.Tool = 0;
                        RizMetre.Arz = 0;
                        RizMetre.Ertefa = 0;
                        RizMetre.Vazn = HajmBetween0To30;
                        RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                        RizMetre.FBId = FBId;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "420" + KMNum.ToString("D3") + "10";///اضافه بها مسلح کردن خاک به آیتم 031107
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        //rmsContext.SaveChanges();
                        //RizMetre.Save();
                    }
                }
            }
        }

        if (HajmBetweenTo100 != 0)
        {


          clsFB?  FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BaravordUserId && x.Shomareh == "031105");

           Guid FBId = new Guid();
            if (FBUser == null)
            {
                clsFB FBSave = new clsFB();
                FBSave.BarAvordId = BaravordUserId;
                FBSave.Shomareh = "031105";
                FBSave.BahayeVahedZarib = 0;
                _context.FBs.Add(FBSave);
                //rmsContext.SaveChanges();
                FBId = FBSave.ID;
            }
            else
                FBId = FBUser.ID;

           
            long Shomareh = 1;
            clsRizMetreUsers? RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            if (RizMetreUser != null)
                Shomareh = RizMetreUser.Shomareh + 1;
            else
                Shomareh = 1;

            //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
            //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
            RizMetre.Shomareh = Shomareh;
            RizMetre.Sharh = "پخـش، آب پاشي، تسطيح، پروفيله کردن، و کوبيدن قشرهاي خاکريزي سنگي";
            RizMetre.Tedad = DarsadKRDDaneh;
            RizMetre.Tool = 0;
            RizMetre.Arz = 0;
            RizMetre.Ertefa = 0;
            RizMetre.Vazn = HajmBetweenTo100;
            RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
            RizMetre.FBId = FBId;
            RizMetre.OperationsOfHamlId = 0;
            RizMetre.Type = "420" + KMNum.ToString("D3") + "04";///پخـش، آب پاشي، تسطيح، پروفيله کردن، و کوبيدن قشرهاي خاکريزي سنگي
            RizMetre.ForItem = "";
            RizMetre.UseItem = "";
            _context.RizMetreUserses.Add(RizMetre);
            //rmsContext.SaveChanges();
            //RizMetre.Save();
        }

        return true;
    }
    public bool SaveRizMetreBestarKhakRizi(RequestSaveRizMetreBestarKhakRiziDto request, ApplicationDbContext _context)
    {

        Guid BarAvordUserID = request.BarAvordUserID;
        Guid KMId = request.KMId;
        int KMNum = request.KMNum;
        string KMS = request.KMS;
        string KME = request.KME;
        string KhakRiziInfoDetails = request.KhakRiziInfoDetails;
        string KhakRiziInfoDetailsCheckBox = request.KhakRiziInfoDetailsCheckBox;


        DataTable DtFBUser = new DataTable();
        DataTable DtLastRizMetreUsersShomareh = new DataTable();
        //clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        //clsOperation_ItemsFB Operation_ItemsFB = new clsOperation_ItemsFB();

        //int Shomareh = 0;
        //Guid FBId = new Guid();

        string[] strKhakRiziInfoDetailsSplit = KhakRiziInfoDetails.Split('$');
        string[] strKhakRiziInfoDetailsCheckBoxSplit = KhakRiziInfoDetailsCheckBox.Split('$');

        for (int j = 0; j < strKhakRiziInfoDetailsSplit.Length - 1; j++)
        {
            string[] strKhakRiziInfoDetailsSplitSplit = strKhakRiziInfoDetailsSplit[j].Split('_');
            string[] strKhakRiziInfoDetailsCheckBoxSplitSplit = strKhakRiziInfoDetailsCheckBoxSplit[j].Split('_');

            string strId = strKhakRiziInfoDetailsSplitSplit[0].Trim().Substring(4, strKhakRiziInfoDetailsSplitSplit[0].Trim().Length - 4);
            //////////////
            decimal dTool = decimal.Parse(strKhakRiziInfoDetailsSplitSplit[1].Trim());

            strKhakRiziInfoDetailsSplitSplit = strKhakRiziInfoDetailsSplit[++j].Split('_');
            decimal dArz = decimal.Parse(strKhakRiziInfoDetailsSplitSplit[1].Trim());

            strKhakRiziInfoDetailsCheckBoxSplitSplit = strKhakRiziInfoDetailsCheckBoxSplit[j - 1].Split('_');
            bool boolKRShokhmZadan = bool.Parse(strKhakRiziInfoDetailsCheckBoxSplitSplit[1].Trim());

            strKhakRiziInfoDetailsCheckBoxSplitSplit = strKhakRiziInfoDetailsCheckBoxSplit[j].Split('_');
            bool boolKRTastih = bool.Parse(strKhakRiziInfoDetailsCheckBoxSplitSplit[1].Trim());

            if (strId == "0")
            {

                //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='031002'");
                //Guid FBId = new Guid();
                //if (DtFBUser.Rows.Count == 0)
                //    FBId = Operation_ItemsFB.SaveFB(BarAvordID, "031002", 0);
                //else
                //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());


               clsFB? FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordUserID && x.Shomareh == "031002");

               Guid FBId = new Guid();
                if (FBUser == null)
                {
                    clsFB FBSave = new clsFB();
                    FBSave.BarAvordId = BarAvordUserID;
                    FBSave.Shomareh = "031002";
                    FBSave.BahayeVahedZarib = 0;
                    _context.FBs.Add(FBSave);
                    //rmsContext.SaveChanges();
                    FBId = FBSave.ID;
                }
                else
                    FBId = FBUser.ID;

             

                if (dTool != 0 || dArz != 0)
                {
                    clsRizMetreUsers RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                    long Shomareh = 0;
                    if (RizMetreUser != null)
                        Shomareh = RizMetreUser.Shomareh + 1;
                    else
                        Shomareh = 1;

                    //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                    //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh;
                    RizMetre.Sharh = "آب پاشي و کوبيدن بستر خاکريزها يا کـف ترانشه ها و مانند آنها، با تراکم 85 درصد";
                    RizMetre.Tedad = 0;
                    RizMetre.Tool = dTool;
                    RizMetre.Arz = dArz;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = 0;
                    RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "430" + KMNum.ToString("D3") + "03";///آب پاشي و کوبيدن بستر خاکريزها يا کـف ترانشه ها و مانند آنها، با تراکم 85 درصد
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    //rmsContext.SaveChanges();
                    //RizMetre.Save();
                }

                ///////////آیتم مرتبط
                if (boolKRShokhmZadan)
                {

                    FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordUserID && x.Shomareh == "030101");

                    FBId = new Guid();
                    if (FBUser == null)
                    {
                        clsFB FBSave = new clsFB();
                        FBSave.BarAvordId = BarAvordUserID;
                        FBSave.Shomareh = "031107";
                        FBSave.BahayeVahedZarib = 0;
                        _context.FBs.Add(FBSave);
                        //rmsContext.SaveChanges();
                        FBId = FBSave.ID;
                    }
                    else
                        FBId = FBUser.ID;

                    
                    if (dTool != 0 || dArz != 0)
                    {
                        clsRizMetreUsers? RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                        long Shomareh = 0;
                        if (RizMetreUser != null)
                            Shomareh = RizMetreUser.Shomareh + 1;
                        else
                            Shomareh = 1;

                        //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                        //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh;
                        RizMetre.Sharh = "شخم زدن هرنوع زمين غيرسنگي با وسيله مکانيکي، به عمق تا 15 سانتيمتر مربوط به آیتم 030101";
                        RizMetre.Tedad = 0;
                        RizMetre.Tool = dTool;
                        RizMetre.Arz = dArz;
                        RizMetre.Ertefa = 0;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                        RizMetre.FBId = FBId;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "430" + KMNum.ToString("D3") + "10";///شخم زدن هرنوع زمين غيرسنگي با وسيله مکانيکي، به عمق تا 15 سانتيمتر مربوط به آیتم 031002
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        //rmsContext.SaveChanges();

                        //RizMetre.Save();
                    }
                }
                ///////////آیتم مرتبط
                if (boolKRTastih)
                {
                    //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='031001'");
                    //FBId = 0;
                    //if (DtFBUser.Rows.Count == 0)
                    //    FBId = Operation_ItemsFB.SaveFB(BarAvordID, "031001", 0);
                    //else
                    //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());



                    FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordUserID && x.Shomareh == "031001");

                    FBId = new Guid();
                    if (FBUser == null)
                    {
                        clsFB FBSave = new clsFB();
                        FBSave.BarAvordId = BarAvordUserID;
                        FBSave.Shomareh = "031001";
                        FBSave.BahayeVahedZarib = 0;
                        _context.FBs.Add(FBSave);
                        //rmsContext.SaveChanges();
                        FBId = FBSave.ID;
                    }
                    else
                        FBId = FBUser.ID;


                    if (dTool != 0 || dArz != 0)
                    {
                        clsRizMetreUsers? RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                        long Shomareh = 0;
                        if (RizMetreUser != null)
                            Shomareh = RizMetreUser.Shomareh + 1;
                        else
                            Shomareh = 1;

                        //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                        //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh;
                        RizMetre.Sharh = "تسطيح بستر خاکريزها با گريدر مربوط به آیتم 031001";
                        RizMetre.Tedad = 0;
                        RizMetre.Tool = dTool;
                        RizMetre.Arz = dArz;
                        RizMetre.Ertefa = 0;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                        RizMetre.FBId = FBId;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "430" + KMNum.ToString("D3") + "11";///تسطيح بستر خاکريزها با گريدر مربوط به آیتم 031001
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);

                        //RizMetre.Save();
                    }
                }
            }
            else if (strId == "1")
            {
                //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='031003'");
                //FBId = 0;
                //if (DtFBUser.Rows.Count == 0)
                //    FBId = Operation_ItemsFB.SaveFB(BarAvordID, "031003", 0);
                //else
                //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());


               clsFB? FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordUserID && x.Shomareh == "031003");

              Guid  FBId = new Guid();
                if (FBUser == null)
                {
                    clsFB FBSave = new clsFB();
                    FBSave.BarAvordId = BarAvordUserID;
                    FBSave.Shomareh = "031003";
                    FBSave.BahayeVahedZarib = 0;
                    _context.FBs.Add(FBSave);
                    //rmsContext.SaveChanges();
                    FBId = FBSave.ID;
                }
                else
                    FBId = FBUser.ID;

               

                if (dTool != 0 || dArz != 0)
                {
                    clsRizMetreUsers RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                    long Shomareh = 0;
                    if (RizMetreUser != null)
                        Shomareh = RizMetreUser.Shomareh + 1;
                    else
                        Shomareh = 1;

                    //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                    //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh;
                    RizMetre.Sharh = "آب پاشي و کوبيدن بستر خاکريزها يا کـف ترانشه ها و مانند آنها، با تراکم 90 درصد";
                    RizMetre.Tedad = 0;
                    RizMetre.Tool = dTool;
                    RizMetre.Arz = dArz;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = 0;
                    RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "430" + KMNum.ToString("D3") + "03";///آب پاشي و کوبيدن بستر خاکريزها يا کـف ترانشه ها و مانند آنها، با تراکم 90 درصد
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    //rmsContext.SaveChanges();
                    //RizMetre.Save();
                }

                ///////////آیتم مرتبط
                if (boolKRShokhmZadan)
                {
                    //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='030101'");
                    //FBId = 0;
                    //if (DtFBUser.Rows.Count == 0)
                    //    FBId = Operation_ItemsFB.SaveFB(BarAvordID, "030101", 0);
                    //else
                    //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());




                    FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordUserID && x.Shomareh == "030101");

                    FBId = new Guid();
                    if (FBUser == null)
                    {
                        clsFB FBSave = new clsFB();
                        FBSave.BarAvordId = BarAvordUserID;
                        FBSave.Shomareh = "030101";
                        FBSave.BahayeVahedZarib = 0;
                        _context.FBs.Add(FBSave);
                        //rmsContext.SaveChanges();
                        FBId = FBSave.ID;
                    }
                    else
                        FBId = FBUser.ID;


                 
                    if (dTool != 0 || dArz != 0)
                    {
                        clsRizMetreUsers? RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                        long Shomareh = 0;
                        if (RizMetreUser != null)
                            Shomareh = RizMetreUser.Shomareh + 1;
                        else
                            Shomareh = 1;

                        //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                        //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh;
                        RizMetre.Sharh = "شخم زدن هرنوع زمين غيرسنگي با وسيله مکانيکي، به عمق تا 15 سانتيمتر مربوط به آیتم 030101";
                        RizMetre.Tedad = 0;
                        RizMetre.Tool = dTool;
                        RizMetre.Arz = dArz;
                        RizMetre.Ertefa = 0;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                        RizMetre.FBId = FBId;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "430" + KMNum.ToString("D3") + "10";///شخم زدن هرنوع زمين غيرسنگي با وسيله مکانيکي، به عمق تا 15 سانتيمتر مربوط به آیتم 031003
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        //rmsContext.SaveChanges();
                        //RizMetre.Save();
                    }
                }
                ///////////آیتم مرتبط
                if (boolKRTastih)
                {
                    //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='031001'");
                    //FBId = 0;
                    //if (DtFBUser.Rows.Count == 0)
                    //    FBId = Operation_ItemsFB.SaveFB(BarAvordID, "031001", 0);
                    //else
                    //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());


                    FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordUserID && x.Shomareh == "031001");

                    FBId = new Guid();
                    if (FBUser == null)
                    {
                        clsFB FBSave = new clsFB();
                        FBSave.BarAvordId = BarAvordUserID;
                        FBSave.Shomareh = "031001";
                        FBSave.BahayeVahedZarib = 0;
                        _context.FBs.Add(FBSave);
                        //rmsContext.SaveChanges();
                        FBId = FBSave.ID;
                    }
                    else
                        FBId = FBUser.ID;

                    if (dTool != 0 || dArz != 0)
                    {
                        clsRizMetreUsers RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                        long Shomareh = 0;
                        if (RizMetreUser != null)
                            Shomareh = RizMetreUser.Shomareh + 1;
                        else
                            Shomareh = 1;

                        //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                        //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh;
                        RizMetre.Sharh = "تسطيح بستر خاکريزها با گريدر مربوط به آیتم 031001";
                        RizMetre.Tedad = 0;
                        RizMetre.Tool = dTool;
                        RizMetre.Arz = dArz;
                        RizMetre.Ertefa = 0;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                        RizMetre.FBId = FBId;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "430" + KMNum.ToString("D3") + "11";///تسطيح بستر خاکريزها با گريدر مربوط به آیتم 031003
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        //rmsContext.SaveChanges();
                        //RizMetre.Save();
                    }
                }
            }
            else if (strId == "2")
            {
                //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='031004'");
                //FBId = 0;
                //if (DtFBUser.Rows.Count == 0)
                //    FBId = Operation_ItemsFB.SaveFB(BarAvordID, "031004", 0);
                //else
                //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());


               clsFB? FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordUserID && x.Shomareh == "031004");

               Guid FBId = new Guid();
                if (FBUser == null)
                {
                    clsFB FBSave = new clsFB();
                    FBSave.BarAvordId = BarAvordUserID;
                    FBSave.Shomareh = "031004";
                    FBSave.BahayeVahedZarib = 0;
                    _context.FBs.Add(FBSave);
                    //rmsContext.SaveChanges();
                    FBId = FBSave.ID;
                }
                else
                    FBId = FBUser.ID;

              
                if (dTool != 0 || dArz != 0)
                {
                    clsRizMetreUsers? RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                    long Shomareh = 0;
                    if (RizMetreUser != null)
                        Shomareh = RizMetreUser.Shomareh + 1;
                    else
                        Shomareh = 1;

                    //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                    //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh;
                    RizMetre.Sharh = "آب پاشي و کوبيدن بستر خاکريزها يا کـف ترانشه ها و مانند آنها، با تراکم 95 درصد";
                    RizMetre.Tedad = 0;
                    RizMetre.Tool = dTool;
                    RizMetre.Arz = dArz;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = 0;
                    RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "430" + KMNum.ToString("D3") + "02";///آب پاشي و کوبيدن بستر خاکريزها يا کـف ترانشه ها و مانند آنها، با تراکم 95 درصد
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    //rmsContext.SaveChanges();
                    //RizMetre.Save();

                    ///////////آیتم مرتبط
                    if (boolKRShokhmZadan)
                    {


                        FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordUserID && x.Shomareh == "030101");

                        FBId = new Guid();
                        if (FBUser == null)
                        {
                            clsFB FBSave = new clsFB();
                            FBSave.BarAvordId = BarAvordUserID;
                            FBSave.Shomareh = "030101";
                            FBSave.BahayeVahedZarib = 0;
                            _context.FBs.Add(FBSave);
                            //rmsContext.SaveChanges();
                            FBId = FBSave.ID;
                        }
                        else
                            FBId = FBUser.ID;
                     
                        if (dTool != 0 || dArz != 0)
                        {
                            RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                            if (RizMetreUser != null)
                                Shomareh = RizMetreUser.Shomareh + 1;
                            else
                                Shomareh = 1;

                            //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                            //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh;
                            RizMetre.Sharh = "شخم زدن هرنوع زمين غيرسنگي با وسيله مکانيکي، به عمق تا 15 سانتيمتر مربوط به آیتم 030101";
                            RizMetre.Tedad = 0;
                            RizMetre.Tool = dTool;
                            RizMetre.Arz = dArz;
                            RizMetre.Ertefa = 0;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                            RizMetre.FBId = FBId;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "430" + KMNum.ToString("D3") + "10";///شخم زدن هرنوع زمين غيرسنگي با وسيله مکانيکي، به عمق تا 15 سانتيمتر مربوط به آیتم 030101
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            //RizMetre.Save();
                        }
                    }
                    ///////////آیتم مرتبط
                    if (boolKRTastih)
                    {


                        FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordUserID && x.Shomareh == "031001");

                        FBId = new Guid();
                        if (FBUser == null)
                        {
                            clsFB FBSave = new clsFB();
                            FBSave.BarAvordId = BarAvordUserID;
                            FBSave.Shomareh = "031001";
                            FBSave.BahayeVahedZarib = 0;
                            _context.FBs.Add(FBSave);
                            //rmsContext.SaveChanges();
                            FBId = FBSave.ID;
                        }
                        else
                            FBId = FBUser.ID;

                        
                        if (dTool != 0 || dArz != 0)
                        {
                            RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                            if (RizMetreUser != null)
                                Shomareh = RizMetreUser.Shomareh + 1;
                            else
                                Shomareh = 1;

                            //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                            //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                            RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh;
                            RizMetre.Sharh = "تسطيح بستر خاکريزها با گريدر مربوط به آیتم 031001";
                            RizMetre.Tedad = 0;
                            RizMetre.Tool = dTool;
                            RizMetre.Arz = dArz;
                            RizMetre.Ertefa = 0;
                            RizMetre.Vazn = 0;
                            RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                            RizMetre.FBId = FBId;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "430" + KMNum.ToString("D3") + "11";///تسطيح بستر خاکريزها با گريدر مربوط به آیتم 031001
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            _context.RizMetreUserses.Add(RizMetre);
                            //rmsContext.SaveChanges();
                            //RizMetre.Save();
                        }
                    }
                }
            }
            else if (strId == "3")
            {


               clsFB? FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordUserID && x.Shomareh == "031005");

               Guid FBId = new Guid();
                if (FBUser == null)
                {
                    clsFB FBSave = new clsFB();
                    FBSave.BarAvordId = BarAvordUserID;
                    FBSave.Shomareh = "031005";
                    FBSave.BahayeVahedZarib = 0;
                    _context.FBs.Add(FBSave);
                    //rmsContext.SaveChanges();
                    FBId = FBSave.ID;
                }
                else
                    FBId = FBUser.ID;

                
                if (dTool != 0 || dArz != 0)
                {
                    clsRizMetreUsers? RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                    long Shomareh = 0;
                    if (RizMetreUser != null)
                        Shomareh = RizMetreUser.Shomareh + 1;
                    else
                        Shomareh = 1;

                    //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                    //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    RizMetre.Shomareh = Shomareh;
                    RizMetre.Sharh = "آب پاشي و کوبيدن بستر خاکريزها يا کـف ترانشه ها و مانند آنها، با تراکم 100 درصد";
                    RizMetre.Tedad = 0;
                    RizMetre.Tool = dTool;
                    RizMetre.Arz = dArz;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = 0;
                    RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "430" + KMNum.ToString("D3") + "03";///آب پاشي و کوبيدن بستر خاکريزها يا کـف ترانشه ها و مانند آنها، با تراکم 100 درصد
                    RizMetre.ForItem = "";
                    RizMetre.UseItem = "";
                    _context.RizMetreUserses.Add(RizMetre);
                    //RizMetre.Save();
                }


                ///////////آیتم مرتبط
                if (boolKRShokhmZadan)
                {


                    FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordUserID && x.Shomareh == "030101");

                    FBId = new Guid();
                    if (FBUser == null)
                    {
                        clsFB FBSave = new clsFB();
                        FBSave.BarAvordId = BarAvordUserID;
                        FBSave.Shomareh = "030101";
                        FBSave.BahayeVahedZarib = 0;
                        _context.FBs.Add(FBSave);
                        //rmsContext.SaveChanges();
                        FBId = FBSave.ID;
                    }
                    else
                        FBId = FBUser.ID;


                   
                    if (dTool != 0 || dArz != 0)
                    {
                        clsRizMetreUsers? RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                        long Shomareh = 0;
                        if (RizMetreUser != null)
                            Shomareh = RizMetreUser.Shomareh + 1;
                        else
                            Shomareh = 1;

                        //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                        //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh;
                        RizMetre.Sharh = "شخم زدن هرنوع زمين غيرسنگي با وسيله مکانيکي، به عمق تا 15 سانتيمتر مربوط به آیتم 030101";
                        RizMetre.Tedad = 0;
                        RizMetre.Tool = dTool;
                        RizMetre.Arz = dArz;
                        RizMetre.Ertefa = 0;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                        RizMetre.FBId = FBId;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "430" + KMNum.ToString("D3") + "10";///شخم زدن هرنوع زمين غيرسنگي با وسيله مکانيکي، به عمق تا 15 سانتيمتر مربوط به آیتم 030101
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        //rmsContext.SaveChanges();

                        //RizMetre.Save();
                    }
                }
                ///////////آیتم مرتبط
                if (boolKRTastih)
                {

                    FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordUserID && x.Shomareh == "031001");

                    FBId = new Guid();
                    if (FBUser == null)
                    {
                        clsFB FBSave = new clsFB();
                        FBSave.BarAvordId = BarAvordUserID;
                        FBSave.Shomareh = "031001";
                        FBSave.BahayeVahedZarib = 0;
                        _context.FBs.Add(FBSave);
                        //rmsContext.SaveChanges();
                        FBId = FBSave.ID;
                    }
                    else
                        FBId = FBUser.ID;

                    
                    if (dTool != 0 || dArz != 0)
                    {
                        clsRizMetreUsers? RizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                        long Shomareh = 0;
                        if (RizMetreUser != null)
                            Shomareh = RizMetreUser.Shomareh + 1;
                        else
                            Shomareh = 1;

                        //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
                        //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh;
                        RizMetre.Sharh = "تسطيح بستر خاکريزها با گريدر مربوط به آیتم 031001";
                        RizMetre.Tedad = 0;
                        RizMetre.Tool = dTool;
                        RizMetre.Arz = dArz;
                        RizMetre.Ertefa = 0;
                        RizMetre.Vazn = 0;
                        RizMetre.Des = "کیلومتراژ " + KMS + " تا " + KME;
                        RizMetre.FBId = FBId;
                        RizMetre.OperationsOfHamlId = 0;
                        RizMetre.Type = "430" + KMNum.ToString("D3") + "11";///تسطيح بستر خاکريزها با گريدر مربوط به آیتم 031001
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        _context.RizMetreUserses.Add(RizMetre);
                        //rmsContext.SaveChanges();
                        //RizMetre.Save();
                    }
                }
            }
        }
        return true;
    }


    //public bool SaveRizMetreGharzeh(requestSaveRizMetreGharzehDto request, ApplicationDbContext _context)
    //{
    //    Guid BarAvordUserID = request.BarAvordUserID;

    //   clsAmalyateKhakiInfoForBarAvord? amalyateKhakiInfoForBarAvord=
    //        _context.AmalyateKhakiInfoForBarAvords.Where(x => x.BaravordUserId == BarAvordUserID && x.Type == NoeAmalyatKhaki.KhakBardariDarZaminDoj).FirstOrDefault();
    //    //DataTable Dt = tblAmalyateKhakiInfoForBarAvord.ListWithParameter("_BaravordUserId='" + BarAvordUserID + "' and _type=4", 4);

    //    if (amalyateKhakiInfoForBarAvord != null)
    //    {
    //        decimal X = decimal.Parse(amalyateKhakiInfoForBarAvord.FromKM.Trim());// decimal.Parse(Dt.Rows[0]["_FromKM"].ToString().Trim());
    //        decimal Y = decimal.Parse(amalyateKhakiInfoForBarAvord.ToKM.Trim());
    //        Guid KMId = amalyateKhakiInfoForBarAvord.ID;
    //        int KMNum = amalyateKhakiInfoForBarAvord.KMNum;
    //        string strKMNum = KMNum.ToString("D3");

    //        //بررسی بشه
    //        //clsRizMetreUsers.Delete("substring(tblRizMetreUsers._Type,1,3)='440' and substring(tblRizMetreUsers._Type,4,3)='" + strKMNum + "'  and tblFBs._BarAvordUserId='" + BarAvordUserID
    //        //+ "' and tblFBs._shomareh in('030103','030104','030105','030602')");

    //        Guid gId = amalyateKhakiInfoForBarAvord.ID; //Guid.Parse(Dt.Rows[0]["_ID"].ToString().Trim());
    //        List<clsAmalyateKhakiInfoForBarAvordDetails> varMGharzehBarAvordDetails =
    //            _context.AmalyateKhakiInfoForBarAvordDetailses.Where(x => x.AmalyateKhakiInfoForBarAvordId == gId).ToList();
    //        //DataTable DtMGharzehBarAvordDetails = clsConvert.ToDataTable(varMGharzehBarAvordDetails);
    //        //DataTable DtMGharzehBarAvordDetails = clsAmalyateKhakiInfoForBarAvordDetails.ListWithParameter("AmalyateKhakiInfoForBarAvordId=" + Dt.Rows[0]["Id"].ToString().Trim());

    //        string str = "";

    //        List<Guid> lstGABD = varMGharzehBarAvordDetails.Select(x => x.ID).ToList();
    //        //if (DtMGharzehBarAvordDetails.Rows.Count != 0)
    //        //{
    //        //    //str += "AmalyateKhakiInfoForBarAvordDetailsId in(";
    //        //    for (int i = 0; i < DtMGharzehBarAvordDetails.Rows.Count; i++)
    //        //    {
    //        //        lstGABD.Add(Guid.Parse(DtMGharzehBarAvordDetails.Rows[i]["_ID"].ToString()));
    //        //        //    if ((i + 1) != DtMGharzehBarAvordDetails.Rows.Count)
    //        //        //        str += DtMGharzehBarAvordDetails.Rows[i]["Id"].ToString() + ",";
    //        //        //    else
    //        //        //        str += DtMGharzehBarAvordDetails.Rows[i]["Id"].ToString();
    //        //    }
    //        //    //str += ")";
    //        //}

    //        List<clsAmalyateKhakiInfoForBarAvordDetailsMore> lstMGharzehBarAvordDetailsMore = _context.AmalyateKhakiInfoForBarAvordDetailsMores.Where(x => lstGABD.Contains(x.AmalyateKhakiInfoForBarAvordDetailsId)).ToList();
    //        //DataTable DtMGharzehBarAvordDetailsMore = clsConvert.ToDataTable(varMGharzehBarAvordDetailsMore);

    //        //بررسی شود
    //        //var varMGharzehBarAvordDetailsEzafeBaha = _context.AmalyateKhakiInfoForBarAvordDetailsEzafeBahas.Where(x => lstGABD.Contains(x._AmalyateKhakiInfoForBarAvordDetailsId)).ToList();
    //        //DataTable DtMGharzehBarAvordDetailsEzafeBaha = clsConvert.ToDataTable(varMGharzehBarAvordDetailsEzafeBaha);

    //        //DataTable DtMGharzehBarAvordDetailsMore = clsAmalyateKhakiInfoForBarAvordDetails.ListWithParameterMore(str);
    //        //DataTable DtMGharzehBarAvordDetailsEzafeBaha = clsAmalyateKhakiInfoForBarAvordDetails.ListWithParameterEzafeBaha(str);

    //        decimal dGhDetail1 = 0;
    //        decimal dGhDarsad2 = 0;
    //        decimal dGhDarsad3 = 0;
    //        decimal dGhDarsad4 = 0;
    //        //for (int i = 0; i < DtMGharzehBarAvordDetailsMore.Rows.Count; i++)
    //        //{
    //        //    if (DtMGharzehBarAvordDetailsMore.Rows[i]["_Name"].ToString().Trim() == "GhDetail1")
    //        //        dGhDetail1 = decimal.Parse(DtMGharzehBarAvordDetailsMore.Rows[i]["_value"].ToString().Trim());
    //        //    else if (DtMGharzehBarAvordDetailsMore.Rows[i]["_Name"].ToString().Trim() == "GhDarsad2")
    //        //        dGhDarsad2 = decimal.Parse(DtMGharzehBarAvordDetailsMore.Rows[i]["_value"].ToString().Trim());
    //        //    else if (DtMGharzehBarAvordDetailsMore.Rows[i]["_Name"].ToString().Trim() == "GhDarsad3")
    //        //        dGhDarsad3 = decimal.Parse(DtMGharzehBarAvordDetailsMore.Rows[i]["_value"].ToString().Trim());
    //        //}


    //         for (int i = 0; i < lstMGharzehBarAvordDetailsMore.Count; i++)
    //        {
    //            if (lstMGharzehBarAvordDetailsMore[i].Name.Trim() == "GhDetail1")
    //                dGhDetail1 = lstMGharzehBarAvordDetailsMore[i].Value;
    //            else if (lstMGharzehBarAvordDetailsMore[i].Name.Trim() == "GhDarsad2")
    //                dGhDarsad2 = lstMGharzehBarAvordDetailsMore[i].Value;
    //            else if (lstMGharzehBarAvordDetailsMore[i].Name.Trim() == "GhDarsad3")
    //                dGhDarsad3 = lstMGharzehBarAvordDetailsMore[i].Value;
    //        }

    //        ////////////
    //        bool bGhFaseleHaml1 = false;
    //        bool bGhFaseleHaml2 = false;
    //        bool bGhFaseleHaml3 = false;
    //        bool bGhFaseleHaml4 = false;
    //        //for (int i = 0; i < DtMGharzehBarAvordDetailsEzafeBaha.Rows.Count; i++)
    //        //{
    //        //    if (DtMGharzehBarAvordDetailsEzafeBaha.Rows[i]["_Name"].ToString().Trim() == "GhFaseleHaml1")
    //        //        bGhFaseleHaml1 = bool.Parse(DtMGharzehBarAvordDetailsEzafeBaha.Rows[i]["_value"].ToString().Trim());
    //        //    else if (DtMGharzehBarAvordDetailsEzafeBaha.Rows[i]["_Name"].ToString().Trim() == "GhFaseleHaml2")
    //        //        bGhFaseleHaml2 = bool.Parse(DtMGharzehBarAvordDetailsEzafeBaha.Rows[i]["_value"].ToString().Trim());
    //        //    else if (DtMGharzehBarAvordDetailsEzafeBaha.Rows[i]["_Name"].ToString().Trim() == "GhFaseleHaml3")
    //        //        bGhFaseleHaml3 = bool.Parse(DtMGharzehBarAvordDetailsEzafeBaha.Rows[i]["_value"].ToString().Trim());
    //        //    else if (DtMGharzehBarAvordDetailsEzafeBaha.Rows[i]["_Name"].ToString().Trim() == "GhFaseleHaml4")
    //        //        bGhFaseleHaml4 = bool.Parse(DtMGharzehBarAvordDetailsEzafeBaha.Rows[i]["_value"].ToString().Trim());
    //        //}

    //        string strParam1 = "_BarAvordUserId='" + BarAvordUserID + "' and tblAmalyateKhakiInfoForBarAvords._type=1 and (tblAmalyateKhakiInfoForBarAvordDetailsMores._Name ='Haml') and (tblAmalyateKhakiInfoForBarAvordDetails._Type in (5,6,7))";
    //        DataTable DtAmalyateKhakiAllInfo = tblAmalyateKhakiInfoForBarAvord.AmalyateKhakiGetAllInfoForBarAvord(strParam1);
    //        decimal dValue1 = decimal.Parse(DtAmalyateKhakiAllInfo.Rows[0]["_sumDetailsMoreValue"].ToString() == "" ? "0" : DtAmalyateKhakiAllInfo.Rows[0]["_sumDetailsMoreValue"].ToString());

    //        strParam1 = "_BarAvordUserId='" + BarAvordUserID + "' and tblAmalyateKhakiInfoForBarAvords._type=2 and (tblAmalyateKhakiInfoForBarAvordDetailsMores._Name ='Haml') and (tblAmalyateKhakiInfoForBarAvordDetails._Type in (2))";
    //        DtAmalyateKhakiAllInfo = tblAmalyateKhakiInfoForBarAvord.AmalyateKhakiGetAllInfoForBarAvord(strParam1);
    //        decimal dValue2 = decimal.Parse(DtAmalyateKhakiAllInfo.Rows[0]["_sumDetailsMoreValue"].ToString() == "" ? "0" : DtAmalyateKhakiAllInfo.Rows[0]["_sumDetailsMoreValue"].ToString());

    //        strParam1 = "_BarAvordUserId='" + _BaravordUserId + "' and tblAmalyateKhakiInfoForBarAvords._type=3 and (tblAmalyateKhakiInfoForBarAvordMores._Name ='HajmBetweenTo100') ";
    //        DtAmalyateKhakiAllInfo = tblAmalyateKhakiInfoForBarAvord.AmalyateKhakiGetAllInfoForBarAvordWithOutDetails(strParam1);
    //        decimal dValue3 = decimal.Parse(DtAmalyateKhakiAllInfo.Rows[0]["_sumMoreValue"].ToString() == "" ? "0" : DtAmalyateKhakiAllInfo.Rows[0]["_sumMoreValue"].ToString());

    //        decimal dGharzehKhBSangi = dValue3 - (dValue1 + dValue2);

    //        strParam1 = "_BarAvordUserId='" + BarAvordUserID + "' and tblAmalyateKhakiInfoForBarAvords._type=1 and (tblAmalyateKhakiInfoForBarAvordDetailsMores._Name ='Haml') and (tblAmalyateKhakiInfoForBarAvordDetails._Type in (3,4))";
    //        DtAmalyateKhakiAllInfo = tblAmalyateKhakiInfoForBarAvord.AmalyateKhakiGetAllInfoForBarAvord(strParam1);
    //        dValue1 = decimal.Parse(DtAmalyateKhakiAllInfo.Rows[0]["_sumDetailsMoreValue"].ToString() == "" ? "0" : DtAmalyateKhakiAllInfo.Rows[0]["_sumDetailsMoreValue"].ToString());

    //        strParam1 = "_BarAvordUserId='" + BarAvordUserID + "' and tblAmalyateKhakiInfoForBarAvords._type=2 and (tblAmalyateKhakiInfoForBarAvordDetailsMores._Name ='Haml') and (tblAmalyateKhakiInfoForBarAvordDetails._Type in (1))";
    //        DtAmalyateKhakiAllInfo = tblAmalyateKhakiInfoForBarAvord.AmalyateKhakiGetAllInfoForBarAvord(strParam1);
    //        dValue2 = decimal.Parse(DtAmalyateKhakiAllInfo.Rows[0]["_sumDetailsMoreValue"].ToString() == "" ? "0" : DtAmalyateKhakiAllInfo.Rows[0]["_sumDetailsMoreValue"].ToString());

    //        strParam1 = "_BarAvordUserId='" + BarAvordUserID + "' and tblAmalyateKhakiInfoForBarAvords._type=3 and (tblAmalyateKhakiInfoForBarAvordMores._Name in('HajmBetween0To30','HajmBetween30To100')) ";
    //        DtAmalyateKhakiAllInfo = tblAmalyateKhakiInfoForBarAvord.AmalyateKhakiGetAllInfoForBarAvordWithOutDetails(strParam1);
    //        dValue3 = decimal.Parse(DtAmalyateKhakiAllInfo.Rows[0]["_sumMoreValue"].ToString() == "" ? "0" : DtAmalyateKhakiAllInfo.Rows[0]["_sumMoreValue"].ToString());

    //        decimal dGharzehKhBNarmSakht = dValue3 - (dValue1 + dValue2);

    //        decimal sumAll = dGharzehKhBSangi + dGharzehKhBNarmSakht + dGhDetail1;
    //        decimal dGhDarsad1 = dGhDetail1 / sumAll * 100;
    //        dGhDarsad4 = 100 - (dGhDarsad1 + dGhDarsad2 + dGhDarsad3);

    //        decimal dGhDetail2 = dGhDarsad2 * sumAll / 100;
    //        decimal dGhDetail3 = dGhDarsad3 * sumAll / 100;
    //        decimal dGhDetail4 = dGhDarsad4 * sumAll / 100;

    //        //clsOperation_ItemsFB Operation_ItemsFBFH = new clsOperation_ItemsFB();
    //        var varFBUserFH = rmsContext.FBs.Where(x => x._BaravordUserId == BarAvordUserID && x._Shomareh == "030602").ToList();
    //        DataTable DtFBUserFH = clsConvert.ToDataTable(varFBUserFH);
    //        //DataTable DtFBUserFH = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='030602'");
    //        DataTable DtLastRizMetreUsersShomarehFH = new DataTable();

    //        Guid FBIdFH = new Guid();
    //        if (DtFBUserFH.Rows.Count == 0)
    //        {
    //            //intFBId = Operation_ItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["Id"].ToString()), Dr[0]["AddedItems"].ToString().Trim() + strStatus, dPercent);
    //            tblFB FB = new tblFB();
    //            FB._BaravordUserId = BarAvordUserID;
    //            FB._Shomareh = "030602";// Dr[0]["_AddedItems"].ToString().Trim() + strStatus;
    //            FB._BahayeVahedZarib = 0;
    //            rmsContext.FBs.Add(FB);
    //            rmsContext.SaveChanges();
    //            FBIdFH = FB._ID;
    //        }
    //        else
    //            FBIdFH = Guid.Parse(DtFBUserFH.Rows[0]["_ID"].ToString());

    //        //int FBIdFH = 0;
    //        //int ShomarehFH = 0;
    //        //if (DtFBUserFH.Rows.Count == 0)
    //        //    FBIdFH = Operation_ItemsFBFH.SaveFB(BarAvordID, "030602", 0);
    //        //else
    //        //    FBIdFH = int.Parse(DtFBUserFH.Rows[0]["Id"].ToString());

    //        int ShomarehFH = 1;
    //        var varLastRizMetreUsersShomarehFH = rmsContext.RizMetreUsers.Where(x => x._FBId == FBIdFH).ToList();
    //        DtLastRizMetreUsersShomarehFH = clsConvert.ToDataTable(varLastRizMetreUsersShomarehFH);
    //        if (DtLastRizMetreUsersShomarehFH.Rows.Count != 0)
    //        {
    //            ShomarehFH = int.Parse(DtLastRizMetreUsersShomarehFH.Rows[0]["_Shomareh"].ToString().Trim()) + 1;
    //        }

    //        //DtLastRizMetreUsersShomarehFH = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBIdFH);
    //        //ShomarehFH = int.Parse(DtLastRizMetreUsersShomarehFH.Rows[0]["lastShomareh"].ToString().Trim());

    //        //clsOperation_ItemsFB Operation_ItemsFB = new clsOperation_ItemsFB();
    //        //clsRizMetreUsers RizMetre = new clsRizMetreUsers();


    //        var varFBUser = rmsContext.FBs.Where(x => x._BaravordUserId == BarAvordUserID && x._Shomareh == "030103").ToList();
    //        DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

    //        //DataTable DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='030103'");
    //        //DataTable DtLastRizMetreUsersShomareh = new DataTable();
    //        //int FBId = 0;
    //        //int Shomareh = 0;
    //        //if (DtFBUser.Rows.Count == 0)
    //        //    FBId = Operation_ItemsFB.SaveFB(BarAvordID, "030103", 0);
    //        //else
    //        //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());


    //        Guid FBId = new Guid();
    //        if (DtFBUserFH.Rows.Count == 0)
    //        {
    //            tblFB FB = new tblFB();
    //            FB._BaravordUserId = BarAvordUserID;
    //            FB._Shomareh = "030103";
    //            FB._BahayeVahedZarib = 0;
    //            rmsContext.FBs.Add(FB);
    //            rmsContext.SaveChanges();
    //            FBId = FB._ID;
    //        }
    //        else
    //            FBId = Guid.Parse(DtFBUserFH.Rows[0]["_ID"].ToString());


    //        int Shomareh = 1;
    //        var varLastRizMetreUsersShomareh = rmsContext.RizMetreUsers.Where(x => x._FBId == FBId).ToList();
    //        DataTable DtLastRizMetreUsersShomareh = clsConvert.ToDataTable(varLastRizMetreUsersShomareh);
    //        if (DtLastRizMetreUsersShomareh.Rows.Count != 0)
    //        {
    //            Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["_Shomareh"].ToString().Trim()) + 1;
    //        }

    //        //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
    //        //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());

    //        if (dGhDetail1 != 0)
    //        {
    //            tblRizMetreUser RizMetre = new tblRizMetreUser();
    //            RizMetre._Shomareh = Shomareh++;
    //            RizMetre._Sharh = "خاکبرداری نباتی در زمین محل قرضه";
    //            RizMetre._Tedad = 1;
    //            RizMetre._Tool = 0;
    //            RizMetre._Arz = 0;
    //            RizMetre._Ertefa = 0;
    //            RizMetre._Vazn = dGhDetail1;
    //            RizMetre._Des = "مختصات x: " + X + " ، Y: " + Y;
    //            RizMetre._FBId = FBId;
    //            RizMetre._OperationsOfHamlId = 0;
    //            RizMetre._Type = "440" + KMNum.ToString("D3") + "00";///خاکبرداری نباتی در زمین محل قرضه
    //            RizMetre._ForItem = "";
    //            RizMetre._UseItem = "";
    //            rmsContext.RizMetreUsers.Add(RizMetre);
    //            rmsContext.SaveChanges();
    //            //RizMetre.Save();
    //            if (bGhFaseleHaml1)
    //            {
    //                RizMetre = new tblRizMetreUser();
    //                RizMetre._Shomareh = ShomarehFH++;
    //                RizMetre._Sharh = "اضافه بها حمل خاکبرداری نباتی در زمین محل قرضه به آیتم 030103 ";
    //                RizMetre._Tedad = 1;
    //                RizMetre._Tool = 0;
    //                RizMetre._Arz = 0;
    //                RizMetre._Ertefa = 0;
    //                RizMetre._Vazn = dGhDetail1;
    //                RizMetre._Des = "مختصات x: " + X + " ، Y: " + Y;
    //                RizMetre._FBId = FBId;
    //                RizMetre._OperationsOfHamlId = 0;
    //                RizMetre._Type = "440" + KMNum.ToString("D3") + "10";///اضافه بها حمل خاکبرداری نباتی در زمین محل قرضه به آیتم 030103
    //                RizMetre._ForItem = "";
    //                RizMetre._UseItem = "";
    //                rmsContext.RizMetreUsers.Add(RizMetre);
    //                rmsContext.SaveChanges();

    //                //RizMetre.Save();
    //            }
    //        }
    //        if (dGhDetail2 != 0)
    //        {
    //            tblRizMetreUser RizMetre = new tblRizMetreUser();
    //            RizMetre._Shomareh = Shomareh;
    //            RizMetre._Sharh = "خاکبرداری در زمین نرم";
    //            RizMetre._Tedad = 1;
    //            RizMetre._Tool = 0;
    //            RizMetre._Arz = 0;
    //            RizMetre._Ertefa = 0;
    //            RizMetre._Vazn = dGhDetail2;
    //            RizMetre._Des = "مختصات x: " + X + " ، Y: " + Y;
    //            RizMetre._FBId = FBId;
    //            RizMetre._OperationsOfHamlId = 0;
    //            RizMetre._Type = "440" + KMNum.ToString("D3") + "01";///خاکبرداری در زمین نرم
    //            RizMetre._ForItem = "";
    //            RizMetre._UseItem = "";
    //            rmsContext.RizMetreUsers.Add(RizMetre);
    //            rmsContext.SaveChanges();

    //            //RizMetre.Save();
    //            if (bGhFaseleHaml2)
    //            {
    //                RizMetre = new tblRizMetreUser();
    //                RizMetre._Shomareh = ShomarehFH++;
    //                RizMetre._Sharh = "اضافه بها حمل خاکبرداری در زمین نرم به آیتم 030103 ";
    //                RizMetre._Tedad = 1;
    //                RizMetre._Tool = 0;
    //                RizMetre._Arz = 0;
    //                RizMetre._Ertefa = 0;
    //                RizMetre._Vazn = dGhDetail2;
    //                RizMetre._Des = "مختصات x: " + X + " ، Y: " + Y;
    //                RizMetre._FBId = FBId;
    //                RizMetre._OperationsOfHamlId = 0;
    //                RizMetre._Type = "440" + KMNum.ToString("D3") + "10";///اضافه بها حمل خاکبرداری در زمین نرم به آیتم 030103
    //                RizMetre._ForItem = "";
    //                RizMetre._UseItem = "";
    //                rmsContext.RizMetreUsers.Add(RizMetre);
    //                rmsContext.SaveChanges();

    //                //RizMetre.Save();
    //            }
    //        }

    //        if (dGhDetail3 != 0)
    //        {
    //            varFBUser = rmsContext.FBs.Where(x => x._BaravordUserId == BarAvordUserID && x._Shomareh == "030104").ToList();
    //            DtFBUser = clsConvert.ToDataTable(varFBUser);


    //            //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='030104'");

    //            FBId = new Guid();
    //            if (DtFBUserFH.Rows.Count == 0)
    //            {
    //                tblFB FB = new tblFB();
    //                FB._BaravordUserId = BarAvordUserID;
    //                FB._Shomareh = "030104";
    //                FB._BahayeVahedZarib = 0;
    //                rmsContext.FBs.Add(FB);
    //                rmsContext.SaveChanges();
    //                FBId = FB._ID;
    //            }
    //            else
    //                FBId = Guid.Parse(DtFBUserFH.Rows[0]["_ID"].ToString());

    //            //DtLastRizMetreUsersShomareh = new DataTable();
    //            //FBId = 0;
    //            //Shomareh = 0;
    //            //if (DtFBUser.Rows.Count == 0)
    //            //    FBId = Operation_ItemsFB.SaveFB(BarAvordID, "030104", 0);
    //            //else
    //            //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

    //            Shomareh = 1;
    //            varLastRizMetreUsersShomareh = rmsContext.RizMetreUsers.Where(x => x._FBId == FBId).ToList();
    //            DtLastRizMetreUsersShomareh = clsConvert.ToDataTable(varLastRizMetreUsersShomareh);
    //            if (DtLastRizMetreUsersShomareh.Rows.Count != 0)
    //            {
    //                Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["_Shomareh"].ToString().Trim()) + 1;
    //            }

    //            //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
    //            //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());

    //            tblRizMetreUser RizMetre = new tblRizMetreUser();
    //            RizMetre._Shomareh = Shomareh;
    //            RizMetre._Sharh = "خاکبرداری در زمین دج";
    //            RizMetre._Tedad = 1;
    //            RizMetre._Tool = 0;
    //            RizMetre._Arz = 0;
    //            RizMetre._Ertefa = 0;
    //            RizMetre._Vazn = dGhDetail3;
    //            RizMetre._Des = "مختصات x: " + X + " ، Y: " + Y;
    //            RizMetre._FBId = FBId;
    //            RizMetre._OperationsOfHamlId = 0;
    //            RizMetre._Type = "440" + KMNum.ToString("D3") + "02";///خاکبرداری در زمین دج
    //            RizMetre._ForItem = "";
    //            RizMetre._UseItem = "";
    //            rmsContext.RizMetreUsers.Add(RizMetre);
    //            rmsContext.SaveChanges();

    //            //RizMetre.Save();

    //            if (bGhFaseleHaml3)
    //            {
    //                RizMetre = new tblRizMetreUser();
    //                RizMetre._Shomareh = ShomarehFH++;
    //                RizMetre._Sharh = "اضافه بها حمل خاکبرداری در زمین دج به آیتم 030104 ";
    //                RizMetre._Tedad = 1;
    //                RizMetre._Tool = 0;
    //                RizMetre._Arz = 0;
    //                RizMetre._Ertefa = 0;
    //                RizMetre._Vazn = dGhDetail3;
    //                RizMetre._Des = "مختصات x: " + X + " ، Y: " + Y;
    //                RizMetre._FBId = FBId;
    //                RizMetre._OperationsOfHamlId = 0;
    //                RizMetre._Type = "440" + KMNum.ToString("D3") + "10";///اضافه بها حمل خاکبرداری در زمین دج به آیتم 030104
    //                RizMetre._ForItem = "";
    //                RizMetre._UseItem = "";
    //                rmsContext.RizMetreUsers.Add(RizMetre);
    //                rmsContext.SaveChanges();

    //                //RizMetre.Save();
    //            }

    //        }

    //        if (dGhDetail4 != 0)
    //        {
    //            varFBUser = rmsContext.FBs.Where(x => x._BaravordUserId == BarAvordUserID && x._Shomareh == "030105").ToList();
    //            DtFBUser = clsConvert.ToDataTable(varFBUser);

    //            //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='030105'");

    //            FBId = new Guid();
    //            if (DtFBUserFH.Rows.Count == 0)
    //            {
    //                tblFB FB = new tblFB();
    //                FB._BaravordUserId = BarAvordUserID;
    //                FB._Shomareh = "030105";
    //                FB._BahayeVahedZarib = 0;
    //                rmsContext.FBs.Add(FB);
    //                rmsContext.SaveChanges();
    //                FBId = FB._ID;
    //            }
    //            else
    //                FBId = Guid.Parse(DtFBUserFH.Rows[0]["_ID"].ToString());

    //            //DtLastRizMetreUsersShomareh = new DataTable();
    //            //FBId = 0;
    //            //Shomareh = 0;
    //            //if (DtFBUser.Rows.Count == 0)
    //            //    FBId = Operation_ItemsFB.SaveFB(BarAvordID, "030105", 0);
    //            //else
    //            //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

    //            //DtLastRizMetreUsersShomareh = clsRizMetreUsers.GetLastRizMetreUsersShomareh("FBId=" + FBId);
    //            //Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());

    //            Shomareh = 1;
    //            varLastRizMetreUsersShomareh = rmsContext.RizMetreUsers.Where(x => x._FBId == FBId).ToList();
    //            DtLastRizMetreUsersShomareh = clsConvert.ToDataTable(varLastRizMetreUsersShomareh);
    //            if (DtLastRizMetreUsersShomareh.Rows.Count != 0)
    //            {
    //                Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["_Shomareh"].ToString().Trim()) + 1;
    //            }

    //            tblRizMetreUser RizMetre = new tblRizMetreUser();
    //            RizMetre._Shomareh = Shomareh;
    //            RizMetre._Sharh = "خاکبرداری در زمین سنگی";
    //            RizMetre._Tedad = 1;
    //            RizMetre._Tool = 0;
    //            RizMetre._Arz = 0;
    //            RizMetre._Ertefa = 0;
    //            RizMetre._Vazn = dGhDetail4;
    //            RizMetre._Des = "مختصات x: " + X + " ، Y: " + Y;
    //            RizMetre._FBId = FBId;
    //            RizMetre._OperationsOfHamlId = 0;
    //            RizMetre._Type = "440" + KMNum.ToString("D3") + "03";///خاکبرداری در زمین سنگی
    //            RizMetre._ForItem = "";
    //            RizMetre._UseItem = "";
    //            rmsContext.RizMetreUsers.Add(RizMetre);
    //            rmsContext.SaveChanges();

    //            //RizMetre.Save();

    //            if (bGhFaseleHaml4)
    //            {
    //                RizMetre = new tblRizMetreUser();
    //                RizMetre._Shomareh = ShomarehFH++;
    //                RizMetre._Sharh = "اضافه بها حمل خاکبرداری در زمین نرم به آیتم 030105 ";
    //                RizMetre._Tedad = 1;
    //                RizMetre._Tool = 0;
    //                RizMetre._Arz = 0;
    //                RizMetre._Ertefa = 0;
    //                RizMetre._Vazn = dGhDetail4;
    //                RizMetre._Des = "مختصات x: " + X + " ، Y: " + Y;
    //                RizMetre._FBId = FBId;
    //                RizMetre._OperationsOfHamlId = 0;
    //                RizMetre._Type = "440" + KMNum.ToString("D3") + "10";///اضافه بها حمل خاکبرداری در زمین سنگی به آیتم 030105
    //                RizMetre._ForItem = "";
    //                RizMetre._UseItem = "";
    //                rmsContext.RizMetreUsers.Add(RizMetre);
    //                rmsContext.SaveChanges();

    //                //RizMetre.Save();
    //            }
    //        }
    //        return true;
    //    }
    //    else
    //        return false;
    //}
}
