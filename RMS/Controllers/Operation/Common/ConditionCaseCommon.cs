using Microsoft.EntityFrameworkCore;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Common;
using RMS.Models.Common.Dto;
using RMS.Models.Entity;
using System.Data;
using System.Text.RegularExpressions;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation.Common;

public class ConditionCaseCommon
{
    public async Task<ConditionCaseCommonCaseResultDto> Case1(ConditionCaseCommonCase1Dto Request, ApplicationDbContext _context)
    {
        string strFinalWorking = Request.strFinalWorking;
        string strFinalWorking1 = "";
        string strFinalWorking2 = "";
        decimal Meghdar = Request.Meghdar;
        decimal Meghdar2 = Request.Meghdar2;
        string strCondition = Request.strCondition;
        string strItemsFBShomareh = Request.strItemsFBShomareh;
        long lngItemsHasCondition_ConditionContextId = Request.lngItemsHasCondition_ConditionContextId;
        Guid BarAvordId = Request.BarAvordId;
        NoeFehrestBaha NoeFBId = Request.NoeFBId;
        long ConditionGroupId = Request.ConditionGroupId;
        string strCharacterPlus = Request.strCharacterPlus;
        string FBShomarehAddedItem = Request.FBShomarehAddedItem;
        string DesOfAddingItems = Request.DesOfAddingItems;
        Guid BaseFBId = Request.BaseFBId;
        string FieldsAdding = Request.FieldsAdding == null ? "1,2,3,4,5" : Request.FieldsAdding;
        long ShomareNew = Request.ShomarehNew;
        string? ConditionContextRel = Request.ConditionContextRel;
        long? ConditionContextId = Request.ConditionContextId;
        int Year = Request.Year;
        DateTime Now = Request.Now;
        string blnCheckIsExistErrors = Request.blnCheckIsExistErrors;
        int LevelNumber = Request.LevelNumber;
        bool blnCheckAgain = Request.blnCheckAgain;
        Guid? RMIdForEdit = Request.RMIdForEdit;
        bool Status = Request.Status;
        bool blnCheckIsExistWarning = Request.blnCheckIsExistWarning;
        string strWarning = Request.strWarning;

        bool blnItemsHasConditionAddedIsNew = false;
        bool blnItemsHasConditionAddedToContext = false;

        decimal dSumAllRizMetre = 0;
        var matchCondition = Regex.Match(strCondition, @"\b([a-zA-Z])\b");
        //string variableNameCondition = matchCondition.Success ? matchCondition.Groups[1].Value : null;
        clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();


        var variableNamesCondition1 = new List<string>();
        foreach (Match match in Regex.Matches(strCondition, @"\b[a-zA-Z]+\b"))
        {
            if (!variableNamesCondition1.Contains(match.Value))
                variableNamesCondition1.Add(match.Value);
        }

        string strConditionOp = "";
        decimal FinalMeghdar = (Meghdar == 0 ? 1 : Meghdar) * (Meghdar2 == 0 ? 1 : Meghdar2);

        ///طبق متغییر موجود در شرط بررسی میگردد که مقدار ورودی جایگزین کدام متغییر گردد
        ///و در نهایت برای فیلدهای طول عرض ارتفاع وزن استفاده میگردد
        foreach (var variableNameCondition in variableNamesCondition1)
        {
            switch (variableNameCondition)
            {
                case "x":
                    {
                        strConditionOp = strCondition.Replace("x", FinalMeghdar.ToString().Trim());
                        break;
                    }
                case "y":
                    {
                        strConditionOp = strCondition.Replace("y", FinalMeghdar.ToString().Trim());
                        break;
                    }
                case "z":
                    {
                        strConditionOp = strCondition.Replace("z", FinalMeghdar.ToString().Trim());
                        break;
                    }
                case "w":
                    {
                        strConditionOp = strCondition.Replace("w", FinalMeghdar.ToString().Trim());
                        break;
                    }
                default:
                    break;
            }

            strCondition = strConditionOp;
        }

        if (blnCheckAgain)
        {

            bool blnCheck = true;
            StringToFormula StringToFormula = new StringToFormula();
            if (strConditionOp != "")
            {
                blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
            }
            if (blnCheck)
            {

                blnCheckIsExistErrors = "false";

                bool blnCheckSave = false;


                clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                                 .FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh == strItemsFBShomareh && x.NoeFB == NoeFBId &&
                                     x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);

                Guid currentItemsHasConditionAddedToFBId = Guid.NewGuid();
                if (currentItemsHasConditionAddedToFBs == null)
                {
                    ItemsHasConditionAddedToFB.ID = currentItemsHasConditionAddedToFBId;
                    ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                    ItemsHasConditionAddedToFB.FBShomareh = strItemsFBShomareh;
                    ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
                    ItemsHasConditionAddedToFB.Meghdar = Meghdar;
                    ItemsHasConditionAddedToFB.Meghdar2 = Meghdar2;
                    ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                    ItemsHasConditionAddedToFB.NoeFB = NoeFBId;

                    blnItemsHasConditionAddedIsNew = true;

                    //_context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                    //await _context.SaveChangesAsync();
                    blnCheckSave = true;
                    //currentItemsHasConditionAddedToFBId = ItemsHasConditionAddedToFB.ID;

                }
                else
                {
                    blnCheckSave = true;
                    currentItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBs.ID;
                }


                if (blnCheckSave)
                {
                    decimal dPercent = 0;
                    decimal dPercent2 = 0;


                    string[] strFinalWorkingSplit = strFinalWorking.Split('_');
                    strFinalWorking1 = strFinalWorkingSplit[0];
                    if (strFinalWorkingSplit.Length > 1)
                        strFinalWorking2 = strFinalWorkingSplit[1];

                    var match = Regex.Match(strFinalWorking1, @"\b([a-zA-Z])\b");

                    string variableName1 = match.Success ? match.Groups[1].Value : null;

                    string variableName2 = "";

                    if (strFinalWorking1.Contains("="))
                    {
                        string[] FinalWorkingSplit = strFinalWorking1.Split("=");

                        switch (variableName1)
                        {
                            case "x":
                                {
                                    string strFW = FinalWorkingSplit[1].Replace("x", FinalMeghdar.ToString().Trim());
                                    dPercent = decimal.Parse(StringToFormula.Eval(strFW).ToString());

                                    //decimal.Parse(FinalWorkingSplit[1]);
                                    break;
                                }
                            case "y":
                                {
                                    string strFW = FinalWorkingSplit[1].Replace("y", FinalMeghdar.ToString().Trim());
                                    dPercent = decimal.Parse(StringToFormula.Eval(strFW).ToString());

                                    //dPercent = decimal.Parse(FinalWorkingSplit[1]);
                                    break;
                                }
                            case "z":
                                {
                                    string strFW = FinalWorkingSplit[1].Replace("z", FinalMeghdar.ToString().Trim());
                                    dPercent = decimal.Parse(StringToFormula.Eval(strFW).ToString());

                                    //dPercent = decimal.Parse(FinalWorkingSplit[1]);
                                    break;
                                }
                            case "w":
                                {
                                    string strFW = FinalWorkingSplit[1].Replace("w", FinalMeghdar.ToString().Trim());
                                    dPercent = decimal.Parse(StringToFormula.Eval(strFW).ToString());

                                    //dPercent = decimal.Parse(FinalWorkingSplit[1]);
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }

                    }
                    else
                    {
                        //match = Regex.Match(strFinalWorking1, @"\b([a-zA-Z])\b");

                        //variableName1 = match.Success ? match.Groups[1].Value : null;

                        ///
                        //مقدار ورودی بنا به این در بخش عملیات نهایی چه حرفی گذاشته شده است
                        //در یکی از فیلدهای طول عرض ارتفاع وزن گذاشته میشود
                        switch (variableName1)
                        {
                            //طول
                            case "x":
                                {

                                    strFinalWorking1 = strFinalWorking1.Replace("x", FinalMeghdar.ToString().Trim());
                                    break;
                                }
                            //عرض
                            case "y":
                                {
                                    strFinalWorking1 = strFinalWorking1.Replace("y", FinalMeghdar.ToString().Trim());
                                    break;
                                }
                            //ارتفاع
                            case "z":
                                {
                                    strFinalWorking1 = strFinalWorking1.Replace("z", FinalMeghdar.ToString().Trim());
                                    break;
                                }
                            //وزن
                            case "w":
                                {
                                    strFinalWorking1 = strFinalWorking1.Replace("w", FinalMeghdar.ToString().Trim());
                                    break;
                                }
                            default:
                                break;
                        }
                        if (strFinalWorking1 != "")
                            dPercent = decimal.Parse(StringToFormula.Eval(strFinalWorking1).ToString());
                    }

                    if (strFinalWorkingSplit.Length > 1)
                    {
                        if (strFinalWorking2.Contains("="))
                        {
                            string[] FinalWorkingSplit = strFinalWorking1.Split("=");

                            switch (variableName2)
                            {
                                case "x":
                                    {
                                        string strFW = FinalWorkingSplit[1].Replace("x", FinalMeghdar.ToString().Trim());
                                        dPercent2 = decimal.Parse(StringToFormula.Eval(strFW).ToString());

                                        //decimal.Parse(FinalWorkingSplit[1]);
                                        break;
                                    }
                                case "y":
                                    {
                                        string strFW = FinalWorkingSplit[1].Replace("y", FinalMeghdar.ToString().Trim());
                                        dPercent2 = decimal.Parse(StringToFormula.Eval(strFW).ToString());

                                        //dPercent = decimal.Parse(FinalWorkingSplit[1]);
                                        break;
                                    }
                                case "z":
                                    {
                                        string strFW = FinalWorkingSplit[1].Replace("z", FinalMeghdar.ToString().Trim());
                                        dPercent2 = decimal.Parse(StringToFormula.Eval(strFW).ToString());

                                        //dPercent = decimal.Parse(FinalWorkingSplit[1]);
                                        break;
                                    }
                                case "w":
                                    {
                                        string strFW = FinalWorkingSplit[1].Replace("w", FinalMeghdar.ToString().Trim());
                                        dPercent2 = decimal.Parse(StringToFormula.Eval(strFW).ToString());

                                        //dPercent = decimal.Parse(FinalWorkingSplit[1]);
                                        break;
                                    }
                                default:
                                    {
                                        break;
                                    }
                            }

                        }
                        else
                        {
                            ///
                            //مقدار ورودی بنا به این در بخش عملیات نهایی چه حرفی گذاشته شده است
                            //در یکی از فیلدهای طول عرض ارتفاع وزن گذاشته میشود
                            switch (variableName2)
                            {
                                //طول
                                case "x":
                                    {

                                        strFinalWorking1 = strFinalWorking2.Replace("x", FinalMeghdar.ToString().Trim());
                                        break;
                                    }
                                //عرض
                                case "y":
                                    {
                                        strFinalWorking1 = strFinalWorking2.Replace("y", FinalMeghdar.ToString().Trim());
                                        break;
                                    }
                                //ارتفاع
                                case "z":
                                    {
                                        strFinalWorking1 = strFinalWorking2.Replace("z", FinalMeghdar.ToString().Trim());
                                        break;
                                    }
                                //وزن
                                case "w":
                                    {
                                        strFinalWorking1 = strFinalWorking2.Replace("w", FinalMeghdar.ToString().Trim());
                                        break;
                                    }
                                default:
                                    break;
                            }
                            if (strFinalWorking2 != "")
                                dPercent2 = decimal.Parse(StringToFormula.Eval(strFinalWorking2).ToString());
                        }
                    }


                    string strDesOfAddingItems = DesOfAddingItems.ToString().Replace("x", FinalMeghdar.ToString().Trim()).Replace("y", strItemsFBShomareh).Replace("z", (dPercent * 100).ToString("0.##")).Trim();

                    clsBaravordUser? varBA = _context.BaravordUsers.First(x => x.ID == BarAvordId);
                    //DataTable DtBA = clsConvert.ToDataTable(varBA);

                    Guid guBAId = varBA.ID; //Guid.Parse(DtBA.Rows[0]["ID"].ToString());


                    ///////////////////////////////////////
                    /////فیلدهای آیتمی که درج میگردد//////
                    ///////////////////////////////////////

                    List<clsItemsFields> lstCurrentItemFields = await _context.ItemsFieldses.Where(x => x.ItemShomareh == FBShomarehAddedItem && x.NoeFB == NoeFBId).ToListAsync();


                    string FBShomarehAddedItemPlus = FBShomarehAddedItem + strCharacterPlus; //"A";
                    clsFB? varFBUser = await _context.FBs.FirstOrDefaultAsync(x => x.BarAvordId == BarAvordId && x.Shomareh == FBShomarehAddedItemPlus && x.NoeFBId == NoeFBId);
                    //DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);


                    Guid FBId = new Guid();
                    if (varFBUser == null)
                    {
                        clsFB FB = new clsFB();
                        FB.BarAvordId = guBAId;//Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                        FB.NoeFBId = NoeFBId;
                        FB.Shomareh = FBShomarehAddedItemPlus; //"A";
                        FB.BahayeVahedZarib = 0;// dPercent;
                        FB.BahayeVahedSharh = strDesOfAddingItems;
                        FB.NoeFBId = NoeFBId;
                        _context.FBs.Add(FB);
                        //await _context.SaveChangesAsync();
                        FBId = FB.ID;
                        varFBUser = FB;

                    }
                    else
                    {
                        varFBUser.BahayeVahedSharh = strDesOfAddingItems;
                        FBId = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                    }


                    List<RizMetreUsersForItemsAddingToFBInputDto> varRizMetreUserses = new List<RizMetreUsersForItemsAddingToFBInputDto>();




                    if (RMIdForEdit == null)
                    {
                        varRizMetreUserses =
                            await (from RUsers in _context.RizMetreUserses
                                   join fb in _context.FBs on RUsers.FBId equals fb.ID
                                   where RUsers.LevelNumber == LevelNumber && RUsers.FBId == BaseFBId && RUsers.Type == "1"
                                   select new RizMetreUsersForItemsAddingToFBInputDto()
                                   {
                                       Id = RUsers.ID,
                                       Shomareh = RUsers.Shomareh,
                                       Sharh = RUsers.Sharh,
                                       Tedad = RUsers.Tedad,
                                       Tool = RUsers.Tool,
                                       Arz = RUsers.Arz,
                                       Ertefa = RUsers.Ertefa,
                                       Vazn = RUsers.Vazn,
                                       Des = RUsers.Des,
                                       ForItem = RUsers.ForItem,
                                       Type = RUsers.Type,
                                       UseItem = RUsers.UseItem,
                                       BarAvordId = fb.BarAvordId,
                                       FBId = RUsers.FBId
                                   }).OrderBy(x => x.Shomareh).ToListAsync();
                    }
                    else
                    {
                        varRizMetreUserses =
                            await (from RUsers in _context.RizMetreUserses
                                   join fb in _context.FBs on RUsers.FBId equals fb.ID
                                   where RUsers.LevelNumber == LevelNumber && RUsers.FBId == BaseFBId && RUsers.Type == "1" && RUsers.ID == RMIdForEdit
                                   select new RizMetreUsersForItemsAddingToFBInputDto()
                                   {
                                       Id = RUsers.ID,
                                       Shomareh = RUsers.Shomareh,
                                       Sharh = RUsers.Sharh,
                                       Tedad = RUsers.Tedad,
                                       Tool = RUsers.Tool,
                                       Arz = RUsers.Arz,
                                       Ertefa = RUsers.Ertefa,
                                       Vazn = RUsers.Vazn,
                                       Des = RUsers.Des,
                                       ForItem = RUsers.ForItem,
                                       Type = RUsers.Type,
                                       UseItem = RUsers.UseItem,
                                       BarAvordId = fb.BarAvordId,
                                       FBId = RUsers.FBId
                                   }).OrderBy(x => x.Shomareh).ToListAsync();
                    }
                    //DataTable DtRizMetreUserses = clsConvert.ToDataTable(varRizMetreUserses);


                    string strShomareh1 = strItemsFBShomareh;//DtFB.Rows[0]["Shomareh"].ToString().Trim();
                    Guid guFBId = BaseFBId; //Guid.Parse(DtFB.Rows[0]["ID"].ToString());

                    var varRizMetreUsersesCurrent = await (from RizMetreUserses in _context.RizMetreUserses
                                                           join FB in _context.FBs on RizMetreUserses.FBId equals FB.ID
                                                           where RizMetreUserses.LevelNumber == LevelNumber
                                                           select new
                                                           {
                                                               ID = RizMetreUserses.ID,
                                                               Shomareh = RizMetreUserses.Shomareh,
                                                               Tedad = RizMetreUserses.Tedad,
                                                               Tool = RizMetreUserses.Tool,
                                                               Arz = RizMetreUserses.Arz,
                                                               Ertefa = RizMetreUserses.Ertefa,
                                                               Vazn = RizMetreUserses.Vazn,
                                                               Des = RizMetreUserses.Des,
                                                               FBId = RizMetreUserses.FBId,
                                                               OperationsOfHamlId = RizMetreUserses.OperationsOfHamlId,
                                                               ForItem = RizMetreUserses.ForItem,
                                                               Type = RizMetreUserses.Type,
                                                               UseItem = RizMetreUserses.UseItem,
                                                               BarAvordId = FB.BarAvordId
                                                           }).Where(x => x.FBId == FBId && x.ForItem == strShomareh1 && x.Type == "2").ToListAsync();

                    string[] FieldsAddingSplit = FieldsAdding.Split(",");

                    foreach (var DtRizMetreUserses11 in varRizMetreUserses)
                    {
                        var DrRizMetreUsersesCurrent = varRizMetreUsersesCurrent.Where(x => x.Shomareh == DtRizMetreUserses11.Shomareh).ToList(); //DtRizMetreUsersesCurrent.Select("shomareh=" + long.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString()));
                        if (DrRizMetreUsersesCurrent.Count == 0)
                        {
                            decimal? dTedad = null;
                            decimal? dTool = null;
                            decimal? dArz = null;
                            decimal? dErtefa = null;
                            decimal? dVazn = null;

                            List<string> lst = new List<string>();
                            for (int j = 0; j < FieldsAddingSplit.Length; j++)
                            {
                                lst.Add(FieldsAddingSplit[j]);
                            }

                            var strCal = lst.Where(a => a.Substring(0, 1) == "1").ToList();
                            if (strCal.Count != 0)
                            {
                                var tedadField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tedad);
                                dTedad = tedadField?.DefaultValue ?? DtRizMetreUserses11.Tedad;
                                if (tedadField != null)
                                {
                                    if (tedadField.EssentialValue)
                                        if (dTedad == null)
                                            dTedad = 0;
                                }

                            }
                            strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                            if (strCal.Count != 0)
                            {
                                var toolField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tool);
                                dTool = toolField?.DefaultValue ?? DtRizMetreUserses11.Tool;
                                if (toolField != null)
                                {
                                    if (toolField.EssentialValue)
                                        if (dTool == null)
                                            dTool = 0;
                                }

                                if (variableName1 == "x")
                                    dTool = dPercent;

                                if (variableName2 == "x")
                                    dTool = dPercent2;

                            }
                            strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                            if (strCal.Count != 0)
                            {
                                var arzField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Arz);
                                dArz = arzField?.DefaultValue ?? DtRizMetreUserses11.Arz;
                                if (arzField != null)
                                {
                                    if (arzField.EssentialValue)
                                        if (dArz == null)
                                            dArz = 0;
                                }
                                if (variableName1 == "y")
                                    dArz = dPercent;
                                if (variableName2 == "y")
                                    dArz = dPercent2;
                            }
                            strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                            if (strCal.Count != 0)
                            {

                                var ertefaField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Ertefa);
                                dErtefa = ertefaField?.DefaultValue ?? DtRizMetreUserses11.Ertefa;
                                if (ertefaField != null)
                                {
                                    if (ertefaField.EssentialValue)
                                        if (dErtefa == null)
                                            dErtefa = 0;
                                }
                                if (variableName1 == "z")
                                    dErtefa = dPercent;
                                if (variableName2 == "z")
                                    dErtefa = dPercent2;
                            }
                            strCal = lst.Where(a => a.Substring(0, 1) == "5").ToList();
                            if (strCal.Count != 0)
                            {

                                var vaznField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Vazn);
                                dVazn = vaznField?.DefaultValue ?? DtRizMetreUserses11.Vazn;
                                if (vaznField != null)
                                {
                                    if (vaznField.EssentialValue)
                                        if (dVazn == null)
                                            dVazn = 0;
                                }
                                if (variableName1 == "w")
                                    dVazn = dPercent;
                                if (variableName2 == "w")
                                    dVazn = dPercent2;
                            }

                            clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                            RizMetreUserses.Shomareh = DtRizMetreUserses11.Shomareh;
                            ShomareNew++;
                            RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                            RizMetreUserses.Sharh = DtRizMetreUserses11.Sharh;// "].ToString().Trim();
                            RizMetreUserses.Tedad = dTedad;
                            RizMetreUserses.Tool = dTool;
                            RizMetreUserses.Arz = dArz;
                            RizMetreUserses.Ertefa = dErtefa;
                            RizMetreUserses.Vazn = dVazn;
                            RizMetreUserses.Des = DtRizMetreUserses11.Des;// "].ToString().Trim();// ItemsAdding.DesOfAddingItems.ToString().Trim() + " به آیتم " + DtFB.Rows[0]["Shomareh"].ToString().Trim()
                                                                          //+ " - ریز متره شماره " + DtRizMetreUserses.Rows[i]["Shomareh"].ToString();
                            RizMetreUserses.FBId = FBId;
                            RizMetreUserses.OperationsOfHamlId = 1;
                            RizMetreUserses.Type = "2";
                            RizMetreUserses.ForItem = strItemsFBShomareh; //DtFB.Rows[0]["Shomareh"].ToString().Trim();
                            RizMetreUserses.UseItem = "";
                            RizMetreUserses.LevelNumber = LevelNumber;
                            RizMetreUserses.ConditionContextRel = ConditionContextRel;
                            RizMetreUserses.ConditionContextId = ConditionContextId;
                            RizMetreUserses.InsertDateTime = Now;

                            blnCheckIsExistWarning = false;
                            strWarning = "";
                            Status = true;


                            //در صورتی که شرایط برای آیتم قبلا درج نشده بود
                            if (blnItemsHasConditionAddedIsNew)
                            {
                                //در صورتی که شرایط برای آیتم در همین روال به کانتکست اضافه نشده بود
                                if (!blnItemsHasConditionAddedToContext)
                                {
                                    _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                    blnItemsHasConditionAddedToContext = true;
                                }
                            }
                            ////////////////
                            //////


                            ///محاسبه مقدار جزء
                            decimal? dMeghdarJoz = null;
                            if (dTedad == 0 && dTool == 0 && dArz == 0 && dErtefa == 0 && dVazn == 0)
                                dMeghdarJoz = null;
                            else
                                dMeghdarJoz = (dTedad == null ? 1 : dTedad.Value) * (dTool == null ? 1 : dTool.Value) *
                                (dArz == null ? 1 : dArz.Value) * (dErtefa == null ? 1 : dErtefa.Value) * (dVazn == null ? 1 : dVazn.Value);
                            RizMetreUserses.MeghdarJoz = dMeghdarJoz;

                            _context.RizMetreUserses.Add(RizMetreUserses);


                            _context.ItemsHasConditionAddedToFBRizMetres.Add(
                                new clsItemsHasConditionAddedToFBRizMetre
                                {
                                    ItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBId,
                                    RizMetreId = RizMetreUserses.ID,
                                    BaseRizMetreId = DtRizMetreUserses11.Id
                                });

                            dSumAllRizMetre += dMeghdarJoz ?? 0;


                            //Start///////////////////////
                            ////////////حمل///////////////
                            //////////////////////////////
                            ///آیتم های حمل درج میگردد
                            ///

                            SaveHamlDto requestSaveHaml = new SaveHamlDto
                            {
                                BarAvordUserId = guBAId,
                                Year = Year,
                                ItemFBShomareh = FBShomarehAddedItemPlus,
                                NoeFBId = NoeFBId,
                                //BarAvordHamlId = gBarAvordHamlId,
                                Shomareh = RizMetreUserses.Shomareh,
                                MeghdarJoz = dMeghdarJoz,
                                LevelNumber = 1,
                                ConditionContextId = ConditionContextId,
                                BaseFBItem = strItemsFBShomareh
                            };
                            await HamlCommon.SaveHaml(requestSaveHaml, _context);
                            //RizMetreUserses.Save();
                        }
                        else
                        {
                            blnCheckIsExistWarning = false;
                            strWarning = "";
                            Status = true;

                        }
                    }

                    /////////////////////////////////////
                    //////دریافت جمع ریزمتره های جاری///
                    /////////////////////////////////////
                    decimal? dSumRizMetreCurrentFB = RizMetreCommon.SumRizMetreCurrentFB(FBId, _context);

                    decimal? dSumRizMetreCurrentFBAll = dSumRizMetreCurrentFB.HasValue
                           ? (dSumRizMetreCurrentFB ?? 0) + dSumAllRizMetre
                           : null;

                    varFBUser.SumRizMetre = dSumRizMetreCurrentFBAll;

                    //await _context.SaveChangesAsync();
                }
                return new ConditionCaseCommonCaseResultDto
                {
                    blnCheckIsExistWarning = blnCheckIsExistWarning,
                    blnStatus = Status,
                    strWarning = strWarning
                };
            }
            else
            {
                return new ConditionCaseCommonCaseResultDto
                {
                    blnCheckIsExistWarning = blnCheckIsExistWarning,
                    blnStatus = Status,
                    strWarning = strWarning
                };
                //return false;
                //if (blnCheckIsExistErrors != "false")
                //{
                //    blnCheckIsExistErrors = "true";
                //    strErrors = strCondition.Replace("x", "عدد وارد شده");
                //}

                //clsFB? varFBUsersAdded = await _context.FBs.FirstOrDefaultAsync(x => x.BarAvordId == BarAvordId && x.Shomareh == ItemsAdding.AddedItems.ToString().Trim() + strCharacterPlus);
                //if (varFBUsersAdded != null)
                //{
                //    Guid guFBUsersAddedId = varFBUsersAdded.ID;
                //    clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                //    if (Fb != null)
                //    {
                //        _context.RizMetreUserses.Where(x => x.FBId == Fb.ID).ExecuteDelete();

                //        _context.FBs.Remove(Fb);
                //        await _context.SaveChangesAsync();
                //    }
                //}
                //return new JsonResult("CI");//CheckInput
            }
        }
        else
        {
            return new ConditionCaseCommonCaseResultDto
            {
                blnCheckIsExistWarning = blnCheckIsExistWarning,
                blnStatus = Status,
                strWarning = strWarning
            };
            //return false;
        }
    }

    public async Task<ConditionCaseCommonCaseResultDto> Case2(ConditionCaseCommonCase1Dto Request, ApplicationDbContext _context)
    {
        string strFinalWorking = Request.strFinalWorking;
        string strFinalWorking1 = "";
        string strFinalWorking2 = "";
        string[] strConditionSplit = Request.strCondition.Split("_");

        string strItemsFBShomareh = Request.strItemsFBShomareh;
        long lngItemsHasCondition_ConditionContextId = Request.lngItemsHasCondition_ConditionContextId;
        Guid BarAvordId = Request.BarAvordId;
        NoeFehrestBaha NoeFBId = Request.NoeFBId;
        long ConditionGroupId = Request.ConditionGroupId;
        string strCharacterPlus = Request.strCharacterPlus;
        string FBShomarehAddedItem = Request.FBShomarehAddedItem;
        string DesOfAddingItems = Request.DesOfAddingItems;
        Guid BaseFBId = Request.BaseFBId;
        string FieldsAdding = Request.FieldsAdding == null ? "1,2,3,4,5" : Request.FieldsAdding;
        long ShomareNew = Request.ShomarehNew;
        string? ConditionContextRel = Request.ConditionContextRel;
        long? ConditionContextId = Request.ConditionContextId;
        int Year = Request.Year;
        DateTime Now = Request.Now;
        string blnCheckIsExistErrors = Request.blnCheckIsExistErrors;
        bool blnCheckAgain = Request.blnCheckAgain;
        Guid? RMIdForEdit = Request.RMIdForEdit;
        bool Status = Request.Status;
        bool blnCheckIsExistWarning = Request.blnCheckIsExistWarning;
        string strWarning = Request.strWarning;

        bool blnItemsHasConditionAddedIsNew = false;
        bool blnItemsHasConditionAddedToContext = false;

        clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();

        int LevelNumber = Request.LevelNumber;

        string strDesOfAddingItems = DesOfAddingItems.ToString();

        decimal dSumAllRizMetre = 0;
        //string variableNameCondition1 = "";
        //string variableNameCondition2 = "";
        StringToFormula StringToFormula = new StringToFormula();


        if (blnCheckAgain)
        {
            clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                             .FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh == strItemsFBShomareh && x.NoeFB == NoeFBId &&
                                 x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);

            bool blnCheckSave = false;
            Guid currentItemsHasConditionAddedToFBId = Guid.NewGuid();
            if (currentItemsHasConditionAddedToFBs == null)
            {
                ItemsHasConditionAddedToFB.ID = currentItemsHasConditionAddedToFBId;
                ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                ItemsHasConditionAddedToFB.FBShomareh = strItemsFBShomareh;
                ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
                ItemsHasConditionAddedToFB.Meghdar = 0;
                ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                ItemsHasConditionAddedToFB.NoeFB = NoeFBId;

                blnItemsHasConditionAddedIsNew = true;
                //_context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                //await _context.SaveChangesAsync();
                //currentItemsHasConditionAddedToFBId = ItemsHasConditionAddedToFB.ID;
                blnCheckSave = true;
            }
            else
            {
                currentItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBs.ID;
                blnCheckSave = true;
            }

            if (blnCheckSave)
            {

                var varBA = _context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();
                DataTable DtBA = clsConvert.ToDataTable(varBA);


                ///////////////////////////////////////
                /////فیلدهای آیتمی که درج میگردد//////
                ///////////////////////////////////////

                List<clsItemsFields> lstCurrentItemFields = _context.ItemsFieldses.Where(x => x.ItemShomareh == FBShomarehAddedItem && x.NoeFB == NoeFBId).ToList();

                //Guid guBAId = BarAvordId;//Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                string FBShomarehAddedItemPlus = FBShomarehAddedItem + strCharacterPlus;
                clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == FBShomarehAddedItemPlus && x.NoeFBId == NoeFBId);

                Guid FBId = new Guid();
                if (varFBUser == null)
                {
                    clsFB FB = new clsFB();
                    FB.BarAvordId = BarAvordId; //Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                    FB.Shomareh = FBShomarehAddedItemPlus;
                    FB.BahayeVahedZarib = 0;
                    FB.BahayeVahedSharh = strDesOfAddingItems;
                    FB.NoeFBId = NoeFBId;
                    _context.FBs.Add(FB);
                    //await _context.SaveChangesAsync();
                    FBId = FB.ID;
                    varFBUser = FB;
                }
                else
                {
                    FBId = varFBUser.ID;
                }



                List<RizMetreUsersForItemsAddingToFBInputDto> varRizMetreUserses = new List<RizMetreUsersForItemsAddingToFBInputDto>();

                if (RMIdForEdit == null)
                {
                    varRizMetreUserses =
                await (from RUsers in _context.RizMetreUserses
                       join fb in _context.FBs on RUsers.FBId equals fb.ID
                       where RUsers.LevelNumber == LevelNumber && RUsers.FBId == BaseFBId && RUsers.Type == "1"
                       select new RizMetreUsersForItemsAddingToFBInputDto()
                       {
                           Id = RUsers.ID,
                           Shomareh = RUsers.Shomareh,
                           Sharh = RUsers.Sharh,
                           Tedad = RUsers.Tedad,
                           Tool = RUsers.Tool,
                           Arz = RUsers.Arz,
                           Ertefa = RUsers.Ertefa,
                           Vazn = RUsers.Vazn,
                           Des = RUsers.Des,
                           ForItem = RUsers.ForItem,
                           Type = RUsers.Type,
                           UseItem = RUsers.UseItem,
                           BarAvordId = fb.BarAvordId,
                           FBId = RUsers.FBId
                       }).OrderBy(x => x.Shomareh).ToListAsync();
                }
                else
                {
                    varRizMetreUserses =
               await (from RUsers in _context.RizMetreUserses
                      join fb in _context.FBs on RUsers.FBId equals fb.ID
                      where RUsers.LevelNumber == LevelNumber && RUsers.FBId == BaseFBId && RUsers.Type == "1" && RUsers.ID == RMIdForEdit
                      select new RizMetreUsersForItemsAddingToFBInputDto()
                      {
                          Id = RUsers.ID,
                          Shomareh = RUsers.Shomareh,
                          Sharh = RUsers.Sharh,
                          Tedad = RUsers.Tedad,
                          Tool = RUsers.Tool,
                          Arz = RUsers.Arz,
                          Ertefa = RUsers.Ertefa,
                          Vazn = RUsers.Vazn,
                          Des = RUsers.Des,
                          ForItem = RUsers.ForItem,
                          Type = RUsers.Type,
                          UseItem = RUsers.UseItem,
                          BarAvordId = fb.BarAvordId,
                          FBId = RUsers.FBId
                      }).OrderBy(x => x.Shomareh).ToListAsync();
                }


                //string strShomareh1 = varFBShomareh;
                Guid guFBId = BaseFBId;
                var varRizMetreUsersesCurrent = (from RizMetreUserses in _context.RizMetreUserses
                                                 join FB in _context.FBs on RizMetreUserses.FBId equals FB.ID
                                                 where RizMetreUserses.LevelNumber == LevelNumber
                                                 select new
                                                 {
                                                     ID = RizMetreUserses.ID,
                                                     Shomareh = RizMetreUserses.Shomareh,
                                                     Tedad = RizMetreUserses.Tedad,
                                                     Tool = RizMetreUserses.Tool,
                                                     Arz = RizMetreUserses.Arz,
                                                     Ertefa = RizMetreUserses.Ertefa,
                                                     Vazn = RizMetreUserses.Vazn,
                                                     Des = RizMetreUserses.Des,
                                                     FBId = RizMetreUserses.FBId,
                                                     OperationsOfHamlId = RizMetreUserses.OperationsOfHamlId,
                                                     ForItem = RizMetreUserses.ForItem,
                                                     Type = RizMetreUserses.Type,
                                                     UseItem = RizMetreUserses.UseItem,
                                                     BarAvordId = FB.BarAvordId
                                                 }).Where(x => x.FBId == FBId && x.ForItem == strItemsFBShomareh && x.Type == "2").ToList();
                DataTable DtRizMetreUsersesCurrent = clsConvert.ToDataTable(varRizMetreUsersesCurrent);

                //var matchCondition = Regex.Match(strFinalWorking, @"\b([a-zA-Z])\b");
                //string variableNameCondition = matchCondition.Success ? matchCondition.Groups[1].Value : null;
                //string strConditionOp = "";

                string[] strFinalWorkingSplit = strFinalWorking.Split('_');

                string[] FieldsAddingSplit = FieldsAdding.Split(",");
                foreach (var DtRizMetreUserses11 in varRizMetreUserses)
                {
                    string strConditionOp = "";
                    string strConditionOp1 = "";

                    string strCondition1 = strConditionSplit[0];
                    string strCondition2 = "";
                    var matchCondition1 = Regex.Match(strCondition1, @"\b([a-zA-Z])\b");


                    var variableNamesCondition1 = new List<string>();
                    var variableNamesCondition2 = new List<string>();
                    foreach (Match match in Regex.Matches(strCondition1, @"\b[a-zA-Z]+\b"))
                    {
                        if (!variableNamesCondition1.Contains(match.Value))
                            variableNamesCondition1.Add(match.Value);
                    }

                    if (strConditionSplit.Length > 1)
                    {
                        strCondition2 = strConditionSplit[1];
                        var matchCondition2 = Regex.Match(strCondition2, @"\b([a-zA-Z])\b");

                        foreach (Match match in Regex.Matches(strCondition2, @"\b[a-zA-Z]+\b"))
                        {
                            if (!variableNamesCondition2.Contains(match.Value))
                                variableNamesCondition2.Add(match.Value);
                        }
                    }

                    strFinalWorking1 = strFinalWorkingSplit[0];
                    if (strFinalWorkingSplit.Length > 1)
                        strFinalWorking2 = strFinalWorkingSplit[1];


                    string strTool = DtRizMetreUserses11.Tool == null ? "0" : DtRizMetreUserses11.Tool.Value.ToString().Trim();
                    string strArz = DtRizMetreUserses11.Arz == null ? "0" : DtRizMetreUserses11.Arz.Value.ToString().Trim();
                    string strErtefa = DtRizMetreUserses11.Ertefa == null ? "0" : DtRizMetreUserses11.Ertefa.Value.ToString().Trim();
                    string strVazn = DtRizMetreUserses11.Vazn == null ? "0" : DtRizMetreUserses11.Vazn.Value.ToString().Trim();

                    foreach (var variableNameCondition1 in variableNamesCondition1)
                    {
                        switch (variableNameCondition1)
                        {
                            case "x":
                                {
                                    strCondition1 = strCondition1.Replace("x", strTool);
                                    break;
                                }
                            case "y":
                                {
                                    strCondition1 = strCondition1.Replace("y", strArz);
                                    break;
                                }
                            case "z":
                                {
                                    strCondition1 = strCondition1.Replace("z", strErtefa);
                                    break;
                                }
                            case "w":
                                {
                                    strCondition1 = strCondition1.Replace("w", strVazn);
                                    break;
                                }
                            default:
                                break;
                        }
                    }
                    strConditionOp = strCondition1;

                    if (strConditionSplit.Length > 1)
                    {
                        foreach (var variableNameCondition2 in variableNamesCondition2)
                        {
                            switch (variableNameCondition2)
                            {
                                case "x":
                                    {
                                        strCondition2 = strCondition2.Replace("x", strTool);
                                        break;
                                    }
                                case "y":
                                    {
                                        strCondition2 = strCondition2.Replace("y", strArz);
                                        break;
                                    }
                                case "z":
                                    {
                                        strCondition2 = strCondition2.Replace("z", strErtefa);
                                        break;
                                    }
                                case "w":
                                    {
                                        strCondition2 = strCondition2.Replace("w", strVazn);
                                        break;
                                    }
                                default:
                                    break;
                            }
                        }
                        strConditionOp1 = strCondition2;
                    }
                    bool blnCheck = true;
                    bool blnCheck1 = true;
                    if (strConditionOp != "")
                        blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                    if (strConditionOp1 != "")
                        blnCheck1 = StringToFormula.RelationalExpression2(strConditionOp1);

                    if (blnCheck && blnCheck1)
                    {
                        blnCheckAgain = false;

                        decimal dPercent = 0;
                        decimal dPercent2 = 0;

                        string variableName = "";
                        string variableName2 = "";

                        if (strFinalWorking1.Contains("="))
                        {
                            string[] FinalWorkingSplit = strFinalWorking1.Split("=");

                            //var match = Regex.Match(FinalWorkingSplit[1], @"\b([a-zA-Z])\b");
                            //variableName = match.Success ? match.Groups[1].Value : null;

                            var lstVariableName = new List<string>();
                            foreach (Match match in Regex.Matches(FinalWorkingSplit[1], @"\b[a-zA-Z]+\b"))
                            {
                                if (!lstVariableName.Contains(match.Value))
                                    lstVariableName.Add(match.Value);
                            }

                            switch (FinalWorkingSplit[0])
                            {
                                case "x":
                                    {

                                        string strFW = FinalWorkingSplit[1];
                                        foreach (var VName in lstVariableName)
                                        {
                                            switch (VName)
                                            {
                                                case "x":
                                                    {
                                                        strFW = strFW.Replace("x", strTool);
                                                        break;
                                                    }
                                                case "y":
                                                    {
                                                        strFW = strFW.Replace("y", strArz);
                                                        break;
                                                    }
                                                case "z":
                                                    {
                                                        strFW = strFW.Replace("z", strErtefa);
                                                        break;
                                                    }
                                                case "w":
                                                    {
                                                        strFW = strFW.Replace("w", strVazn);
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                        }
                                        if (strFW != "")
                                            dPercent = decimal.Parse(StringToFormula.Eval(strFW).ToString());
                                        else
                                            dPercent = decimal.Parse(FinalWorkingSplit[1]);

                                        break;
                                    }
                                case "y":
                                    {
                                        string strFW = FinalWorkingSplit[1];
                                        foreach (var VName in lstVariableName)
                                        {

                                            switch (variableName)
                                            {
                                                case "x":
                                                    {
                                                        strFW = strFW.Replace("x", strTool);
                                                        break;
                                                    }
                                                case "y":
                                                    {
                                                        strFW = strFW.Replace("y", strArz);
                                                        break;
                                                    }
                                                case "z":
                                                    {
                                                        strFW = strFW.Replace("z", strErtefa);
                                                        break;
                                                    }
                                                case "w":
                                                    {
                                                        strFW = strFW.Replace("w", strVazn);
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                        }
                                        if (strFW != "")
                                            dPercent = decimal.Parse(StringToFormula.Eval(strFW).ToString());
                                        else
                                            dPercent = decimal.Parse(FinalWorkingSplit[1]);

                                        break;
                                    }
                                case "z":
                                    {
                                        string strFW = FinalWorkingSplit[1];
                                        foreach (var VName in lstVariableName)
                                        {
                                            switch (VName)
                                            {
                                                case "x":
                                                    {
                                                        strFW = strFW.Replace("x", strTool);
                                                        break;
                                                    }
                                                case "y":
                                                    {
                                                        strFW = strFW.Replace("y", strArz);
                                                        break;
                                                    }
                                                case "z":
                                                    {
                                                        strFW = strFW.Replace("z", strErtefa);
                                                        break;
                                                    }
                                                case "w":
                                                    {
                                                        strFW = strFW.Replace("w", strVazn);
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                        }
                                        if (strFW != "")
                                            dPercent = decimal.Parse(StringToFormula.Eval(strFW).ToString());
                                        else
                                            dPercent = decimal.Parse(FinalWorkingSplit[1]);
                                        break;
                                    }
                                case "w":
                                    {
                                        string strFW = FinalWorkingSplit[1];
                                        foreach (var VName in lstVariableName)
                                        {
                                            switch (VName)
                                            {
                                                case "x":
                                                    {
                                                        strFW = strFW.Replace("x", strTool);
                                                        break;
                                                    }
                                                case "y":
                                                    {
                                                        strFW = strFW.Replace("y", strArz);
                                                        break;
                                                    }
                                                case "z":
                                                    {
                                                        strFW = strFW.Replace("z", strErtefa);
                                                        break;
                                                    }
                                                case "w":
                                                    {
                                                        strFW = strFW.Replace("w", strVazn);
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                        }
                                        if (strFW != "")
                                            dPercent = decimal.Parse(StringToFormula.Eval(strFW).ToString());
                                        else
                                            dPercent = decimal.Parse(FinalWorkingSplit[1]);
                                        break;
                                    }
                                default:
                                    {
                                        break;
                                    }
                            }

                            variableName = lstVariableName.Count != 0 ? lstVariableName.First() : "";// FinalWorkingSplit[0];

                        }
                        else
                        {
                            //var match = Regex.Match(strFinalWorking1, @"\b([a-zA-Z])\b");
                            //variableName = match.Success ? match.Groups[1].Value : null;

                            var lstVariableName = new List<string>();
                            foreach (Match match in Regex.Matches(strFinalWorking1, @"\b[a-zA-Z]+\b"))
                            {
                                if (!lstVariableName.Contains(match.Value))
                                    lstVariableName.Add(match.Value);
                            }

                            ///
                            //مقدار ورودی بنا به این در بخش عملیات نهایی چه حرفی گذاشته شده است
                            //در یکی از فیلدهای طول عرض ارتفاع وزن گذاشته میشود

                            foreach (var VName in lstVariableName)
                            {
                                switch (VName)
                                {
                                    //طول
                                    case "x":
                                        {

                                            strFinalWorking1 = strFinalWorking1.Replace("x", strTool);
                                            break;
                                        }
                                    //عرض
                                    case "y":
                                        {
                                            strFinalWorking1 = strFinalWorking1.Replace("y", strArz);
                                            break;
                                        }
                                    //ارتفاع
                                    case "z":
                                        {
                                            strFinalWorking1 = strFinalWorking1.Replace("z", strErtefa);
                                            break;
                                        }
                                    //وزن
                                    case "w":
                                        {
                                            strFinalWorking1 = strFinalWorking1.Replace("w", strVazn);
                                            break;
                                        }
                                    default:
                                        break;
                                }
                            }
                            if (strFinalWorking1 != "")
                                dPercent = decimal.Parse(StringToFormula.Eval(strFinalWorking1).ToString());
                        }

                        if (strFinalWorkingSplit.Length > 1)
                        {

                            if (strFinalWorking2.Contains("="))
                            {
                                string[] FinalWorkingSplit = strFinalWorking2.Split("=");
                                //var match2 = Regex.Match(FinalWorkingSplit[1], @"\b([a-zA-Z])\b");
                                //variableName2 = match2.Success ? match2.Groups[1].Value : null;

                                var lstVariableName = new List<string>();
                                foreach (Match match in Regex.Matches(strFinalWorking2, @"\b[a-zA-Z]+\b"))
                                {
                                    if (!lstVariableName.Contains(match.Value))
                                        lstVariableName.Add(match.Value);
                                }

                                switch (FinalWorkingSplit[0])
                                {
                                    case "x":
                                        {
                                            string strFW = FinalWorkingSplit[1];
                                            foreach (var VName in lstVariableName)
                                            {

                                                switch (VName)
                                                {
                                                    case "x":
                                                        {
                                                            strFW = strFW.Replace("x", strTool);
                                                            break;
                                                        }
                                                    case "y":
                                                        {
                                                            strFW = strFW.Replace("y", strArz);
                                                            break;
                                                        }
                                                    case "z":
                                                        {
                                                            strFW = strFW.Replace("z", strErtefa);
                                                            break;
                                                        }
                                                    case "w":
                                                        {
                                                            strFW = strFW.Replace("w", strVazn);
                                                            break;
                                                        }
                                                    default:
                                                        break;
                                                }
                                            }
                                            if (strFW != "")
                                                dPercent2 = decimal.Parse(StringToFormula.Eval(strFW).ToString());
                                            else
                                                dPercent2 = decimal.Parse(FinalWorkingSplit[1]);
                                            break;
                                        }
                                    case "y":
                                        {
                                            string strFW = FinalWorkingSplit[1];
                                            foreach (var VName in lstVariableName)
                                            {
                                                switch (VName)
                                                {
                                                    case "x":
                                                        {
                                                            strFW = strFW.Replace("x", strTool);
                                                            break;
                                                        }
                                                    case "y":
                                                        {
                                                            strFW = strFW.Replace("y", strArz);
                                                            break;
                                                        }
                                                    case "z":
                                                        {
                                                            strFW = strFW.Replace("z", strErtefa);
                                                            break;
                                                        }
                                                    case "w":
                                                        {
                                                            strFW = strFW.Replace("w", strVazn);
                                                            break;
                                                        }
                                                    default:
                                                        break;
                                                }
                                            }
                                            if (strFW != "")
                                                dPercent2 = decimal.Parse(StringToFormula.Eval(strFW).ToString());
                                            else
                                                dPercent2 = decimal.Parse(FinalWorkingSplit[1]);
                                            break;
                                        }
                                    case "z":
                                        {
                                            string strFW = FinalWorkingSplit[1];
                                            foreach (var VName in lstVariableName)
                                            {
                                                switch (VName)
                                                {
                                                    case "x":
                                                        {
                                                            strFW = strFW.Replace("x", strTool);
                                                            break;
                                                        }
                                                    case "y":
                                                        {
                                                            strFW = strFW.Replace("y", strArz);
                                                            break;
                                                        }
                                                    case "z":
                                                        {
                                                            strFW = strFW.Replace("z", strErtefa);
                                                            break;
                                                        }
                                                    case "w":
                                                        {
                                                            strFW = strFW.Replace("w", strVazn);
                                                            break;
                                                        }
                                                    default:
                                                        break;
                                                }
                                            }
                                            if (strFW != "")
                                                dPercent2 = decimal.Parse(StringToFormula.Eval(strFW).ToString());
                                            else
                                                dPercent2 = decimal.Parse(FinalWorkingSplit[1]);
                                            break;
                                        }
                                    case "w":
                                        {
                                            string strFW = FinalWorkingSplit[1];
                                            foreach (var VName in lstVariableName)
                                            {
                                                switch (VName)
                                                {
                                                    case "x":
                                                        {
                                                            strFW = strFW.Replace("x", strTool);
                                                            break;
                                                        }
                                                    case "y":
                                                        {
                                                            strFW = strFW.Replace("y", strArz);
                                                            break;
                                                        }
                                                    case "z":
                                                        {
                                                            strFW = strFW.Replace("z", strErtefa);
                                                            break;
                                                        }
                                                    case "w":
                                                        {
                                                            strFW = strFW.Replace("w", strVazn);
                                                            break;
                                                        }
                                                    default:
                                                        break;
                                                }
                                            }
                                            if (strFW != "")
                                                dPercent2 = decimal.Parse(StringToFormula.Eval(strFW).ToString());
                                            else
                                                dPercent2 = decimal.Parse(FinalWorkingSplit[1]);

                                            break;
                                        }
                                    default:
                                        {
                                            break;
                                        }
                                }

                                variableName2 = lstVariableName.Count != 0 ? lstVariableName.First() : "";// FinalWorkingSplit[0];

                            }
                            else
                            {
                                //var match2 = Regex.Match(strFinalWorking2, @"\b([a-zA-Z])\b");
                                //variableName2 = match2.Success ? match2.Groups[1].Value : null;

                                var lstVariableName = new List<string>();
                                foreach (Match match in Regex.Matches(strFinalWorking2, @"\b[a-zA-Z]+\b"))
                                {
                                    if (!lstVariableName.Contains(match.Value))
                                        lstVariableName.Add(match.Value);
                                }

                                ///
                                //مقدار ورودی بنا به این در بخش عملیات نهایی چه حرفی گذاشته شده است
                                //در یکی از فیلدهای طول عرض ارتفاع وزن گذاشته میشود

                                foreach (var VName in lstVariableName)
                                {
                                    switch (VName)
                                    {
                                        //طول
                                        case "x":
                                            {

                                                strFinalWorking2 = strFinalWorking2.Replace("x", strTool);
                                                break;
                                            }
                                        //عرض
                                        case "y":
                                            {
                                                strFinalWorking2 = strFinalWorking2.Replace("y", strArz);
                                                break;
                                            }
                                        //ارتفاع
                                        case "z":
                                            {
                                                strFinalWorking2 = strFinalWorking2.Replace("z", strErtefa);
                                                break;
                                            }
                                        //وزن
                                        case "w":
                                            {
                                                strFinalWorking2 = strFinalWorking2.Replace("w", strVazn);
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                }
                                if (strFinalWorking2 != "")
                                    dPercent2 = decimal.Parse(StringToFormula.Eval(strFinalWorking2).ToString());
                            }

                        }

                        DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + DtRizMetreUserses11.Shomareh);
                        if (DrRizMetreUsersesCurrent.Length == 0)
                        {
                            List<string> lst = new List<string>();
                            for (int j = 0; j < FieldsAddingSplit.Length; j++)
                            {
                                lst.Add(FieldsAddingSplit[j]);
                            }


                            decimal? dTedad = null;
                            decimal? dTool = null;
                            decimal? dArz = null;
                            decimal? dErtefa = null;
                            decimal? dVazn = null;



                            var strCal = lst.Where(a => a.Substring(0, 1) == "1").ToList();
                            if (strCal.Count != 0)
                            {
                                var tedadField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tedad);
                                dTedad = tedadField?.DefaultValue ?? DtRizMetreUserses11.Tedad;
                            }
                            strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                            if (strCal.Count != 0)
                            {
                                var toolField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tool);
                                dTool = toolField?.DefaultValue ?? DtRizMetreUserses11.Tool;
                                if (variableName == "x")
                                    dTool = dPercent;
                                if (variableName2 == "x")
                                    dTool = dPercent2;
                            }
                            strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                            if (strCal.Count != 0)
                            {
                                var arzField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Arz);
                                dArz = arzField?.DefaultValue ?? DtRizMetreUserses11.Arz;
                                if (variableName == "y")
                                    dArz = dPercent;
                                if (variableName2 == "y")
                                    dArz = dPercent2;
                            }
                            strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                            if (strCal.Count != 0)
                            {
                                var ertefaField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Ertefa);
                                dErtefa = ertefaField?.DefaultValue ?? DtRizMetreUserses11.Ertefa;
                                if (variableName == "z")
                                    dErtefa = dPercent;
                                if (variableName2 == "z")
                                    dErtefa = dPercent2;
                            }
                            strCal = lst.Where(a => a.Substring(0, 1) == "5").ToList();
                            if (strCal.Count != 0)
                            {
                                var vaznField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Vazn);
                                dVazn = vaznField?.DefaultValue ?? DtRizMetreUserses11.Vazn;
                                if (variableName == "w")
                                    dVazn = dPercent;
                                if (variableName2 == "w")
                                    dVazn = dPercent2;
                            }

                            clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                            RizMetreUserses.Shomareh = DtRizMetreUserses11.Shomareh;// long.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString());
                            ShomareNew++;
                            RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                            RizMetreUserses.Sharh = DtRizMetreUserses11.Sharh;// DtRizMetreUserses.Rows[i]["Sharh"].ToString().Trim();
                            RizMetreUserses.Tedad = dTedad;
                            RizMetreUserses.Tool = dTool;
                            RizMetreUserses.Arz = dArz;
                            RizMetreUserses.Ertefa = dErtefa;
                            RizMetreUserses.Vazn = dVazn;
                            RizMetreUserses.Des = DtRizMetreUserses11.Des;// DtRizMetreUserses.Rows[i]["Des"].ToString().Trim(); //ItemsAdding.DesOfAddingItems.ToString().Trim() + " به آیتم " + varFBShomareh
                                                                          //+ " - ریز متره شماره " + DtRizMetreUserses.Rows[i]["Shomareh"].ToString();
                            RizMetreUserses.FBId = FBId;
                            RizMetreUserses.OperationsOfHamlId = 1;
                            RizMetreUserses.Type = "2";
                            RizMetreUserses.ForItem = strItemsFBShomareh;
                            RizMetreUserses.UseItem = "";
                            RizMetreUserses.LevelNumber = LevelNumber;
                            RizMetreUserses.InsertDateTime = Now;

                            blnCheckIsExistWarning = false;
                            strWarning = "";
                            Status = true;

                            //در صورتی که شرایط برای آیتم قبلا درج نشده بود
                            if (blnItemsHasConditionAddedIsNew)
                            {
                                //در صورتی که شرایط برای آیتم در همین روال به کانتکست اضافه نشده بود
                                if (!blnItemsHasConditionAddedToContext)
                                {
                                    _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                    blnItemsHasConditionAddedToContext = true;
                                }
                            }
                            ////////////////
                            //////
                            /////////////////
                            ///////////////
                            /////////////////
                            //////////////////

                            ///محاسبه مقدار جزء
                            decimal? dMeghdarJoz = null;
                            if (dTedad == null && dTool == null && dArz == null && dErtefa == null && dVazn == null)
                                dMeghdarJoz = null;
                            else
                                dMeghdarJoz = (dTedad == null ? 1 : dTedad.Value) * (dTool == null ? 1 : dTool.Value) *
                                (dArz == null ? 1 : dArz.Value) * (dErtefa == null ? 1 : dErtefa.Value) * (dVazn == null ? 1 : dVazn.Value);
                            RizMetreUserses.MeghdarJoz = dMeghdarJoz;

                            RizMetreUserses.ConditionContextRel = ConditionContextRel;
                            RizMetreUserses.ConditionContextId = ConditionContextId;

                            _context.RizMetreUserses.Add(RizMetreUserses);


                            _context.ItemsHasConditionAddedToFBRizMetres.Add(
                            new clsItemsHasConditionAddedToFBRizMetre
                            {
                                ItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBId,
                                RizMetreId = RizMetreUserses.ID,
                                BaseRizMetreId = DtRizMetreUserses11.Id
                            });

                            dSumAllRizMetre += dMeghdarJoz ?? 0;

                            //Start///////////////////////
                            ////////////حمل///////////////
                            //////////////////////////////
                            ///آیتم های حمل درج میگردد
                            ///


                            SaveHamlDto requestSaveHaml = new SaveHamlDto
                            {
                                BarAvordUserId = BarAvordId,
                                Year = Year,
                                ItemFBShomareh = FBShomarehAddedItem,
                                NoeFBId = NoeFBId,
                                //BarAvordHamlId = gBarAvordHamlId,
                                Shomareh = DtRizMetreUserses11.Shomareh,
                                MeghdarJoz = dMeghdarJoz,
                                LevelNumber = 1,
                                BaseFBItem = strItemsFBShomareh
                            };
                            await HamlCommon.SaveHaml(requestSaveHaml, _context);

                            //End//////////////////////////
                            /////////////حمل//////////////
                            //////////////////////////////
                            ///
                            //}
                        }
                    }
                    /////////////////////////////////////
                    //////دریافت جمع ریزمتره های جاری///
                    /////////////////////////////////////
                    decimal? dSumRizMetreCurrentFB = RizMetreCommon.SumRizMetreCurrentFB(FBId, _context);

                    decimal? dSumRizMetreCurrentFBAll = dSumRizMetreCurrentFB.HasValue
                           ? (dSumRizMetreCurrentFB ?? 0) + dSumAllRizMetre
                           : null;

                    varFBUser.SumRizMetre = dSumRizMetreCurrentFBAll;

                    //await _context.SaveChangesAsync();

                }
            }
        }

        return new ConditionCaseCommonCaseResultDto
        {
            blnCheckIsExistWarning = blnCheckIsExistWarning,
            blnStatus = Status,
            strWarning = strWarning
        };
    }

    public async Task<ConditionCaseCommonCaseResultDto> Case15(ConditionCaseCommonCase1Dto Request, ApplicationDbContext _context)
    {
        string strFinalWorking = Request.strFinalWorking;
        string strCondition = Request.strCondition;
        string strItemsFBShomareh = Request.strItemsFBShomareh;
        long lngItemsHasCondition_ConditionContextId = Request.lngItemsHasCondition_ConditionContextId;
        Guid BarAvordId = Request.BarAvordId;
        NoeFehrestBaha NoeFBId = Request.NoeFBId;
        long ConditionGroupId = Request.ConditionGroupId;
        string strCharacterPlus = Request.strCharacterPlus;
        string FBShomarehAddedItem = Request.FBShomarehAddedItem;
        string DesOfAddingItems = Request.DesOfAddingItems;
        Guid BaseFBId = Request.BaseFBId;
        string FieldsAdding = Request.FieldsAdding == null ? "1,2,3,4,5" : Request.FieldsAdding;
        long ShomareNew = Request.ShomarehNew;
        string? ConditionContextRel = Request.ConditionContextRel;
        long? ConditionContextId = Request.ConditionContextId;
        int Year = Request.Year;
        DateTime Now = Request.Now;
        string blnCheckIsExistErrors = Request.blnCheckIsExistErrors;
        int LevelNumber = Request.LevelNumber;
        Guid? RMIdForEdit = Request.RMIdForEdit;

        bool blnCheckIsExistWarning = Request.blnCheckIsExistWarning;
        string strWarning = Request.strWarning;
        bool Status = Request.Status;

        decimal dSumAllRizMetre = 0;
        var matchCondition = Regex.Match(strFinalWorking, @"\b([a-zA-Z])\b");
        string variableNameCondition = matchCondition.Success ? matchCondition.Groups[1].Value : null;
        string strConditionOp = "";
        StringToFormula StringToFormula = new StringToFormula();

        bool blnItemsHasConditionAddedIsNew = false;
        bool blnItemsHasConditionAddedToContext = false;

        clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();

        //بررسی اینکه در تراش یا لکه گیری هست یا خیر
        //اگر باشد دیگر صعوبت درج نمیشود

        RizMetreCommon rizMetreCommon = new RizMetreCommon();
        long[] lngConditionGroupId = { 31, 4 };

        GetAndShowAddItemsInputForSoubatDto request2 = new GetAndShowAddItemsInputForSoubatDto()
        {
            ShomarehFB = strItemsFBShomareh,
            BarAvordUserId = BarAvordId,
            NoeFB = NoeFBId,
            Year = Year,
            LevelNumber = LevelNumber,
            ConditionGroupId = lngConditionGroupId
        };
        List<RizMetreForGetAndShowAddItemsDto> lstRM = rizMetreCommon.GetAndShowAddItemsForSoubat(request2, _context);

        string[] strConditionSplit = strCondition.Split("_");

        string strFinalWorking1 = strFinalWorking;
        string[] strFinalWorkingSplit = strFinalWorking1.Split('_');
        string strFinalWorking2 = strFinalWorkingSplit[0];
        //decimal dBahayeVahedZarib = strFinalWorkingSplit.Length > 1 ? decimal.Parse(strFinalWorkingSplit[1]) : 0;

        DataTable DtRizMetreUserses = new DataTable();
        string strForItem = "";
        string strUseItem = "";
        string strItemFBShomareh = strItemsFBShomareh;
        string strDesOfAddingItems = DesOfAddingItems;

        Guid guFBId = BaseFBId;
        strUseItem = strItemsFBShomareh;
        strForItem = FBShomarehAddedItem;



        ///////////////////////////////////////
        /////فیلدهای آیتمی که درج میگردد//////
        ///////////////////////////////////////
        List<clsItemsFields> lstCurrentItemFields = _context.ItemsFieldses.Where(x => x.ItemShomareh == FBShomarehAddedItem && x.NoeFB == NoeFBId).ToList();

        List<RizMetreUsersForItemsAddingToFBInputDto> varRizMetreUserses = new List<RizMetreUsersForItemsAddingToFBInputDto>();

        if (RMIdForEdit == null)
        {

            varRizMetreUserses = (from RUsers in _context.RizMetreUserses
                                  join fb in _context.FBs on RUsers.FBId equals fb.ID
                                  where RUsers.LevelNumber == LevelNumber && RUsers.FBId == guFBId && RUsers.Type == "1"
                                  select new RizMetreUsersForItemsAddingToFBInputDto
                                  {
                                      Id = RUsers.ID,
                                      Shomareh = RUsers.Shomareh,
                                      Sharh = RUsers.Sharh,
                                      Tedad = RUsers.Tedad,
                                      Tool = RUsers.Tool,
                                      Arz = RUsers.Arz,
                                      Ertefa = RUsers.Ertefa,
                                      Vazn = RUsers.Vazn,
                                      Des = RUsers.Des,
                                      ForItem = RUsers.ForItem,
                                      Type = RUsers.Type,
                                      FBId = RUsers.FBId
                                  }).OrderBy(x => x.Shomareh).ToList();
        }
        else
        {
            varRizMetreUserses = (from RUsers in _context.RizMetreUserses
                                  join fb in _context.FBs on RUsers.FBId equals fb.ID
                                  where RUsers.LevelNumber == LevelNumber && RUsers.FBId == guFBId && RUsers.Type == "1" && RUsers.ID == RMIdForEdit
                                  select new RizMetreUsersForItemsAddingToFBInputDto
                                  {
                                      Id = RUsers.ID,
                                      Shomareh = RUsers.Shomareh,
                                      Sharh = RUsers.Sharh,
                                      Tedad = RUsers.Tedad,
                                      Tool = RUsers.Tool,
                                      Arz = RUsers.Arz,
                                      Ertefa = RUsers.Ertefa,
                                      Vazn = RUsers.Vazn,
                                      Des = RUsers.Des,
                                      ForItem = RUsers.ForItem,
                                      Type = RUsers.Type,
                                      FBId = RUsers.FBId
                                  }).OrderBy(x => x.Shomareh).ToList();
        }


        List<RizMetreUsersForItemsAddingToFBInputDto> varRizMetreUsersesWithoutLakeAndTarash = new List<RizMetreUsersForItemsAddingToFBInputDto>();

        foreach (var RM1 in varRizMetreUserses)
        {
            RizMetreForGetAndShowAddItemsDto? FindRM = lstRM.FirstOrDefault(x => x.Shomareh == RM1.Shomareh);
            if (FindRM == null)
            {
                varRizMetreUsersesWithoutLakeAndTarash.Add(varRizMetreUserses.FirstOrDefault(x => x.Shomareh == RM1.Shomareh));
            }
        }

        List<Guid> lstRMIds = varRizMetreUsersesWithoutLakeAndTarash.Select(x => x.Id).ToList();

        List<clsRizMetreUsers> lstRizMetreUser = _context.RizMetreUserses.Where(x => lstRMIds.Contains(x.ID)).ToList();

        if (varRizMetreUsersesWithoutLakeAndTarash.Count != 0)
        {
            var varRizMetreUsersesCurrent = (from RizMetreUserses in _context.RizMetreUserses
                                             join FB in _context.FBs on RizMetreUserses.FBId equals FB.ID
                                             where RizMetreUserses.LevelNumber == LevelNumber
                                             select new
                                             {
                                                 ID = RizMetreUserses.ID,
                                                 Shomareh = RizMetreUserses.Shomareh,
                                                 Tedad = RizMetreUserses.Tedad,
                                                 Tool = RizMetreUserses.Tool,
                                                 Arz = RizMetreUserses.Arz,
                                                 Ertefa = RizMetreUserses.Ertefa,
                                                 Vazn = RizMetreUserses.Vazn,
                                                 Des = RizMetreUserses.Des,
                                                 FBId = RizMetreUserses.FBId,
                                                 OperationsOfHamlId = RizMetreUserses.OperationsOfHamlId,
                                                 ForItem = RizMetreUserses.ForItem,
                                                 Type = RizMetreUserses.Type,
                                                 UseItem = RizMetreUserses.UseItem,
                                                 BarAvordId = FB.BarAvordId
                                             }).Where(x => x.ForItem == strForItem && x.Type == "2" && x.UseItem == strUseItem).ToList();

            //DataTable DtRizMetreUsersesCurrent = clsConvert.ToDataTable(varRizMetreUsersesCurrent);

            List<clsFB> lstFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFBId).ToList();
            string strItemShomareh = FBShomarehAddedItem + strCharacterPlus;
            clsFB? varFBUser = lstFBUser.FirstOrDefault(x => x.Shomareh == strItemShomareh);

            Guid FBId = new Guid();
            if (varFBUser == null)
            {
                clsFB FB = new clsFB();
                FB.BarAvordId = BarAvordId; //Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                FB.Shomareh = strItemShomareh;
                //FB.BahayeVahedZarib = dBahayeVahedZarib;
                FB.BahayeVahedSharh = strDesOfAddingItems;
                FB.NoeFBId = NoeFBId;
                _context.FBs.Add(FB);
                //await _context.SaveChangesAsync();
                FBId = FB.ID;
                varFBUser = FB;
            }
            else
                FBId = varFBUser.ID;

            bool blnCheckSave = false;
            string strCurrentFBShomareh = strItemsFBShomareh;// Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();

            //long lngItemsHasCondition_ConditionContextId = long.Parse(ItemsAdding.ItemsHasCondition_ConditionContextId.ToString());
            clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                 .FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh == strCurrentFBShomareh && x.NoeFB == NoeFBId &&
                     x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);


            Guid currentItemsHasConditionAddedToFBId = Guid.NewGuid();
            if (currentItemsHasConditionAddedToFBs == null)
            {
                ItemsHasConditionAddedToFB.ID = currentItemsHasConditionAddedToFBId;
                ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                ItemsHasConditionAddedToFB.FBShomareh = strCurrentFBShomareh;
                ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
                //ItemsHasConditionAddedToFB.Meghdar = RM.Tool != null ? RM.Tool.Value : 0;
                ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                ItemsHasConditionAddedToFB.NoeFB = NoeFBId;

                blnItemsHasConditionAddedIsNew = true;
                //_context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                //await _context.SaveChangesAsync();
                blnCheckSave = true;
                //currentItemsHasConditionAddedToFBId = ItemsHasConditionAddedToFB.ID;

            }
            else
            {
                blnCheckSave = true;
                currentItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBs.ID;
            }

            if (blnCheckSave)
            {
                //List<clsFB> varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strItemShomareh).ToList();
                string[] FieldsAddingSplit = FieldsAdding.Split(",");
                foreach (var RM in lstRizMetreUser)
                {
                    strConditionOp = strConditionSplit[0].Replace("y", RM.Arz == null ? "" : RM.Arz.Value.ToString().Trim());
                    //StringToFormula StringToFormula = new StringToFormula();
                    bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                    if (blnCheck)
                    {



                        decimal? dArz = null;
                        if (RM.Arz <= 2)
                            dArz = RM.Arz;
                        else
                        {
                            dArz = 2;
                            ///بایستی سابقه در جدول سوابق ریزمتره دخیره گردد تا در صورتی که تیک تعریض برداشته شد، آیتم اصلی به حالت اولش برگردد
                            ///
                            clsRizMetreUsersHistory rizMetreUsersHistory = new clsRizMetreUsersHistory
                            {
                                RizMetreUsersId = RM.ID,
                                Shomareh = RM.Shomareh,
                                ShomarehNew = RM.ShomarehNew,
                                Sharh = RM.Sharh,
                                Tedad = RM.Tedad,
                                Tool = RM.Tool,
                                Arz = RM.Arz,
                                Ertefa = RM.Ertefa,
                                Vazn = RM.Vazn,
                                MeghdarJoz = RM.MeghdarJoz,
                                Des = RM.Des
                            };
                            _context.RizMetreUsersHistories.Add(rizMetreUsersHistory);

                            RM.Arz = 2;

                            decimal? dMeghdarJoz = null;

                            dMeghdarJoz = (RM.Tedad == null ? 1 : RM.Tedad.Value) * (RM.Tool == null ? 1 : RM.Tool.Value) *
                            2 * (RM.Ertefa == null ? 1 : RM.Ertefa.Value) * (RM.Vazn == null ? 1 : RM.Vazn.Value);

                            RM.MeghdarJoz = dMeghdarJoz;

                            dSumAllRizMetre += dMeghdarJoz ?? 0;
                        }

                        var DrRizMetreUsersesCurrent = varRizMetreUsersesCurrent.Where(x => x.Shomareh == RM.Shomareh && x.FBId == FBId).ToList();

                        if (DrRizMetreUsersesCurrent.Count == 0)
                        {

                            clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                            RizMetreUserses.Shomareh = RM.Shomareh;
                            ShomareNew++;
                            RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                            List<string> lst = new List<string>();
                            for (int j = 0; j < FieldsAddingSplit.Length; j++)
                            {
                                lst.Add(FieldsAddingSplit[j]);
                            }


                            decimal? dTedad = null;
                            decimal? dTool = null;
                            decimal? dErtefa = null;
                            decimal? dVazn = null;



                            var strCal = lst.Where(a => a.Substring(0, 1) == "1").ToList();
                            if (strCal.Count != 0)
                            {
                                var tedadField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tedad);
                                dTedad = tedadField?.DefaultValue ?? RM.Tedad;
                            }
                            strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                            if (strCal.Count != 0)
                            {
                                var toolField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tool);
                                dTool = toolField?.DefaultValue ?? RM.Tool;
                            }
                            //strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                            //if (strCal.Count != 0)
                            //{
                            //    dArz = Arz;
                            //}
                            strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                            if (strCal.Count != 0)
                            {
                                var ertefaField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Ertefa);
                                dErtefa = ertefaField?.DefaultValue ?? RM.Ertefa;
                            }
                            strCal = lst.Where(a => a.Substring(0, 1) == "5").ToList();
                            if (strCal.Count != 0)
                            {
                                var vaznField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Vazn);
                                dVazn = vaznField?.DefaultValue ?? RM.Vazn;
                            }


                            RizMetreUserses.Sharh = RM.Sharh;
                            RizMetreUserses.Tedad = dTedad;
                            RizMetreUserses.Tool = dTool;
                            RizMetreUserses.Arz = dArz;
                            RizMetreUserses.Ertefa = dErtefa;
                            RizMetreUserses.Vazn = dVazn;
                            RizMetreUserses.Des = RM.Des;

                            RizMetreUserses.FBId = FBId;
                            RizMetreUserses.OperationsOfHamlId = 1;
                            RizMetreUserses.Type = "2";
                            RizMetreUserses.ForItem = strForItem;
                            RizMetreUserses.UseItem = strUseItem;
                            RizMetreUserses.LevelNumber = LevelNumber;
                            RizMetreUserses.InsertDateTime = Now;

                            blnCheckIsExistWarning = false;
                            strWarning = "";
                            Status = true;

                            //در صورتی که شرایط برای آیتم قبلا درج نشده بود
                            if (blnItemsHasConditionAddedIsNew)
                            {
                                //در صورتی که شرایط برای آیتم در همین روال به کانتکست اضافه نشده بود
                                if (!blnItemsHasConditionAddedToContext)
                                {
                                    _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                    blnItemsHasConditionAddedToContext = true;
                                }
                            }
                            ////////////////
                            //////
                            ///
                            ///محاسبه مقدار جزء
                            decimal? dMeghdarJoz = null;
                            if (dTedad == null && dTool == null && dArz == null && dErtefa == null && dArz == null && dVazn == null)
                                dMeghdarJoz = null;
                            else
                                dMeghdarJoz = (dTedad == null ? 1 : dTedad.Value) * (dTool == null ? 1 : dTool.Value) *
                                (dArz == null ? 1 : dArz.Value) * (dErtefa == null ? 1 : dErtefa.Value) * (dVazn == null ? 1 : dVazn.Value);

                            RizMetreUserses.MeghdarJoz = dMeghdarJoz;

                            _context.RizMetreUserses.Add(RizMetreUserses);

                            _context.ItemsHasConditionAddedToFBRizMetres.Add(
                            new clsItemsHasConditionAddedToFBRizMetre
                            {
                                ItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBId,
                                RizMetreId = RizMetreUserses.ID,
                                BaseRizMetreId = RM.ID
                            });

                            dSumAllRizMetre += dMeghdarJoz ?? 0;
                            //Start///////////////////////
                            ////////////حمل///////////////
                            //////////////////////////////
                            ///آیتم های حمل درج میگردد
                            ///


                            SaveHamlDto requestSaveHaml = new SaveHamlDto
                            {
                                BarAvordUserId = BarAvordId,
                                Year = Year,
                                ItemFBShomareh = strItemShomareh,
                                NoeFBId = NoeFBId,
                                //BarAvordHamlId = gBarAvordHamlId,
                                Shomareh = RizMetreUserses.Shomareh,
                                MeghdarJoz = dMeghdarJoz,
                                LevelNumber = 1,
                                BaseFBItem = strItemsFBShomareh
                            };
                            await HamlCommon.SaveHaml(requestSaveHaml, _context);


                        }
                    }
                }
            }
            /////////////////////////////////////
            //////دریافت جمع ریزمتره های جاری///
            /////////////////////////////////////
            decimal? dSumRizMetreCurrentFB = RizMetreCommon.SumRizMetreCurrentFB(FBId, _context);

            decimal? dSumRizMetreCurrentFBAll = dSumRizMetreCurrentFB.HasValue
                   ? (dSumRizMetreCurrentFB ?? 0) + dSumAllRizMetre
                   : null;

            varFBUser.SumRizMetre = dSumRizMetreCurrentFBAll;

            //blnCheckIsExistWarning = false;

            //await _context.SaveChangesAsync();
            //return true;
            return new ConditionCaseCommonCaseResultDto
            {
                blnCheckIsExistWarning = blnCheckIsExistWarning,
                blnStatus = Status,
                strWarning = strWarning
            };

        }
        else
        {
            blnCheckIsExistWarning = true;
            strWarning += "هیچ ریزمتره ای دارای شرایط لازمه نبود";
            //return false;
            return new ConditionCaseCommonCaseResultDto
            {
                blnCheckIsExistWarning = blnCheckIsExistWarning,
                blnStatus = Status,
                strWarning = strWarning
            };
        }
    }
    public async Task<ConditionCaseCommonCaseResultDto> Case17(ConditionCaseCommonCase1Dto Request, ApplicationDbContext _context)
    {
        string strFinalWorking = Request.strFinalWorking;
        string strFinalWorking1 = "";
        string strFinalWorking2 = "";
        string[] strConditionSplit = Request.strCondition.Split("_");

        string strCondition = Request.strCondition;

        string strItemsFBShomareh = Request.strItemsFBShomareh;
        long lngItemsHasCondition_ConditionContextId = Request.lngItemsHasCondition_ConditionContextId;
        Guid BarAvordId = Request.BarAvordId;
        NoeFehrestBaha NoeFBId = Request.NoeFBId;
        long ConditionGroupId = Request.ConditionGroupId;
        string strCharacterPlus = Request.strCharacterPlus;
        string FBShomarehAddedItem = Request.FBShomarehAddedItem;
        string DesOfAddingItems = Request.DesOfAddingItems;
        Guid BaseFBId = Request.BaseFBId;
        string FieldsAdding = Request.FieldsAdding == null ? "1,2,3,4,5" : Request.FieldsAdding;
        long ShomareNew = Request.ShomarehNew;
        string? ConditionContextRel = Request.ConditionContextRel;
        long? ConditionContextId = Request.ConditionContextId;
        int Year = Request.Year;
        DateTime Now = Request.Now;
        string blnCheckIsExistErrors = Request.blnCheckIsExistErrors;
        bool blnCheckAgain = Request.blnCheckAgain;
        bool blnCheckIsExistWarning = Request.blnCheckIsExistWarning;
        Guid? RMIdForEdit = Request.RMIdForEdit;
        int LevelNumber = Request.LevelNumber;
        string strWarning = Request.strWarning;
        bool Status = Request.Status;

        decimal Meghdar = Request.Meghdar;
        decimal Meghdar2 = Request.Meghdar2;

        decimal FinalMeghdar = (Meghdar == 0 ? 1 : Meghdar) * (Meghdar2 == 0 ? 1 : Meghdar2);

        string strDesOfAddingItems = DesOfAddingItems.ToString();

        decimal dSumAllRizMetre = 0;
        //string variableNameCondition1 = "";
        //string variableNameCondition2 = "";
        string strConditionOp = "";
        string strConditionOp1 = "";
        StringToFormula StringToFormula = new StringToFormula();

        bool blnItemsHasConditionAddedIsNew = false;
        bool blnItemsHasConditionAddedToContext = false;

        clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();


        if (blnCheckAgain)
        {
            //decimal dSumAllRizMetre = 0;

            DataTable DtRizMetreUserses = new DataTable();
            string strForItem = "";
            string strUseItem = "";
            string strItemFBShomareh = strItemsFBShomareh;

            Guid guFBId = BaseFBId;
            strUseItem = strItemsFBShomareh;
            strForItem = strItemsFBShomareh;

            ///////////////////////////////////////
            /////فیلدهای آیتمی که درج میگردد//////
            ///////////////////////////////////////

            List<clsItemsFields> lstCurrentItemFields = _context.ItemsFieldses.Where(x => x.ItemShomareh == FBShomarehAddedItem && x.NoeFB == NoeFBId).ToList();

            List<RizMetreUsersForItemsAddingToFBInputDto> varRizMetreUserses = new List<RizMetreUsersForItemsAddingToFBInputDto>();


            ///یعنی کل آیتم های پایه بایستی جهت درج بررسی گردند
            /////
            if (RMIdForEdit == null)
            {
                varRizMetreUserses =
                     (from RUsers in _context.RizMetreUserses
                      join fb in _context.FBs on RUsers.FBId equals fb.ID
                      where RUsers.LevelNumber == LevelNumber && RUsers.FBId == guFBId && RUsers.Type == "1"
                      select new RizMetreUsersForItemsAddingToFBInputDto
                      {
                          Shomareh = RUsers.Shomareh,
                          Sharh = RUsers.Sharh,
                          Tedad = RUsers.Tedad,
                          Tool = RUsers.Tool,
                          Arz = RUsers.Arz,
                          Ertefa = RUsers.Ertefa,
                          Vazn = RUsers.Vazn,
                          Des = RUsers.Des,
                          ForItem = RUsers.ForItem,
                          Type = RUsers.Type,
                          FBId = RUsers.FBId,
                          Id = RUsers.ID
                      }).OrderBy(x => x.Shomareh).ToList();
            }
            ///در عیر اینصورت فقط ردیف پایه ای که ویرایش شده بایستی بررسی گردد
            ///
            else
            {
                varRizMetreUserses =
                    (from RUsers in _context.RizMetreUserses
                     join fb in _context.FBs on RUsers.FBId equals fb.ID
                     where RUsers.LevelNumber == LevelNumber && RUsers.FBId == guFBId && RUsers.Type == "1" && RUsers.ID == RMIdForEdit
                     select new RizMetreUsersForItemsAddingToFBInputDto
                     {
                         Shomareh = RUsers.Shomareh,
                         Sharh = RUsers.Sharh,
                         Tedad = RUsers.Tedad,
                         Tool = RUsers.Tool,
                         Arz = RUsers.Arz,
                         Ertefa = RUsers.Ertefa,
                         Vazn = RUsers.Vazn,
                         Des = RUsers.Des,
                         ForItem = RUsers.ForItem,
                         Type = RUsers.Type,
                         FBId = RUsers.FBId,
                         Id = RUsers.ID
                     }).OrderBy(x => x.Shomareh).ToList();
            }


            //long lngItemsHasCondition_ConditionContextId = long.Parse(ItemsAdding.ItemsHasCondition_ConditionContextId.ToString());

            clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                             .FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh == strItemFBShomareh && x.NoeFB == NoeFBId &&
                                 x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);

            Guid currentItemsHasConditionAddedToFBId = Guid.NewGuid();

            bool blnCheckSave = false;
            if (currentItemsHasConditionAddedToFBs == null)
            {
                ItemsHasConditionAddedToFB.ID = currentItemsHasConditionAddedToFBId;
                ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                ItemsHasConditionAddedToFB.FBShomareh = strItemFBShomareh;
                ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
                ItemsHasConditionAddedToFB.Meghdar = 0;
                ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                ItemsHasConditionAddedToFB.NoeFB = NoeFBId;

                //await _context.SaveChangesAsync();
                //currentItemsHasConditionAddedToFBId = ItemsHasConditionAddedToFB.ID;

                blnCheckSave = true;

                blnItemsHasConditionAddedIsNew = true;

            }
            else
            {
                currentItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBs.ID;
                blnCheckSave = true;
            }

            //if (ItemsHasConditionAddedToFB.Save())
            if (blnCheckSave)
            {
                //متغییری که بعد از علامت & میاد فیلد قرار گیری تعداد تکرار میباشد
                //مثلا اگر 
                //x
                //باشد یعنی در فیلد طول قرار میگیرد
                string[] strFinalWorkingSplitFieldForCount = strFinalWorking.Split('&');


                string[] strFinalWorkingBahayeVahedZarib = strFinalWorkingSplitFieldForCount[0].Split(',');
                string[] strFinalWorkingSplit1 = strFinalWorkingSplitFieldForCount[0].Split(',')[0].Split('_');
                int intDivided = strFinalWorkingSplit1.Length == 1 ? 1 : int.Parse(strFinalWorkingSplit1[1]);
                string[] strFinalWorkingSplit = strFinalWorkingSplit1[0].Split('-');

                List<clsFB> lstFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFBId).ToList();
                string strItemShomareh = FBShomarehAddedItem + strCharacterPlus.Trim();

                clsFB? varFBUser = lstFBUser.FirstOrDefault(x => x.Shomareh == strItemShomareh);

                Guid FBId = new Guid();
                if (varFBUser == null)
                {
                    clsFB FB = new clsFB();
                    FB.BarAvordId = BarAvordId; //Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                    FB.Shomareh = FBShomarehAddedItem + strCharacterPlus.Trim();
                    //FB.BahayeVahedZarib = strFinalWorkingBahayeVahedZarib.Length > 1 ? decimal.Parse(strFinalWorkingBahayeVahedZarib[1]) : 0;
                    FB.BahayeVahedSharh = strDesOfAddingItems;
                    FB.NoeFBId = NoeFBId;
                    _context.FBs.Add(FB);
                    //await _context.SaveChangesAsync();
                    FBId = FB.ID;
                    varFBUser = FB;
                }
                else
                    FBId = varFBUser.ID;


                //bool blnCheckEqual = false;

                string CharForReplace = "";
                string strTAEForReoplace = "";




                string[] strFinalWorkingSplitEqual = strFinalWorkingSplit[0].Split('=');
                if (strFinalWorkingSplitEqual.Length > 1)
                {
                    CharForReplace = strFinalWorkingSplitEqual[0].Trim();
                    strTAEForReoplace = decimal.Parse(strFinalWorkingSplitEqual[1]).ToString("G0").Trim();
                    //blnCheckEqual = true;
                }

                int intCount = 1;
                string VariableName = "x";
                if (strFinalWorkingSplitFieldForCount.Length > 1)
                {
                    VariableName = strFinalWorkingSplitFieldForCount[1];
                }
                string[] FieldsAddingSplit = FieldsAdding.Split(",");


                foreach (var RM in varRizMetreUserses)
                {


                    List<string> lst = new List<string>();
                    for (int j = 0; j < FieldsAddingSplit.Length; j++)
                    {
                        lst.Add(FieldsAddingSplit[j]);
                    }

                    decimal? Tedad = null;
                    decimal? Tool = null;
                    decimal? Arz = null;
                    decimal? Ertefa = null;
                    decimal? Vazn = null;


                    var strCal = lst.Where(a => a.Substring(0, 1) == "1").ToList();
                    if (strCal.Count != 0)
                    {
                        var tedadField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tedad);
                        Tedad = tedadField?.DefaultValue ?? RM.Tedad;
                    }

                    strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                    if (strCal.Count != 0)
                    {

                        var toolField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tool);
                        Tool = toolField?.DefaultValue ?? RM.Tool;
                    }

                    strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                    if (strCal.Count != 0)
                    {
                        var arzField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Arz);
                        Arz = arzField?.DefaultValue ?? RM.Arz;
                    }

                    strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                    if (strCal.Count != 0)
                    {
                        var ertefaField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Ertefa);
                        Ertefa = ertefaField?.DefaultValue ?? RM.Ertefa;
                    }

                    strCal = lst.Where(a => a.Substring(0, 1) == "5").ToList();
                    if (strCal.Count != 0)
                    {
                        var vaznField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Vazn);
                        Vazn = vaznField?.DefaultValue ?? RM.Vazn;
                    }

                    //decimal? dErtefa = null;
                    //decimal? dTool = null;
                    if (Meghdar != 0 || Meghdar2 != 0)
                    {
                        CharForReplace = "x";
                        strTAEForReoplace = FinalMeghdar.ToString();
                    }
                    else
                    {
                        if (strFinalWorkingSplit[0] == "z")
                        {
                            CharForReplace = "z";
                            strTAEForReoplace = RM.Ertefa != null ? RM.Ertefa.Value.ToString().Trim() : "";
                            Ertefa = intDivided;
                            Tool = RM.Tool;
                        }
                        else if (strFinalWorkingSplit[0] == "x")
                        {
                            CharForReplace = "x";
                            strTAEForReoplace = RM.Tool != null ? RM.Tool.Value.ToString().Trim() : "";
                            Ertefa = RM.Ertefa;
                            Tool = intDivided;
                        }
                    }

                    long lngStartValue = long.Parse(strFinalWorkingSplit[1]) - intDivided;

                    if (CharForReplace != "")
                        strConditionOp = strCondition.Replace(CharForReplace, strTAEForReoplace);
                    StringToFormula stringToFormula = new StringToFormula();

                    bool blnCheck = true;
                    if (strConditionOp != "") blnCheck = stringToFormula.RelationalExpression2(strConditionOp);

                    if (blnCheck)
                    {
                        if (strFinalWorking != "")
                        {
                            string strFinalWorking11 = "";
                            if (CharForReplace != "")
                            {
                                if (strTAEForReoplace != "")
                                {
                                    strFinalWorking11 = strFinalWorkingSplit1[0].Replace(CharForReplace, strTAEForReoplace);

                                    int intReapetCount = (int.Parse(Math.Floor(stringToFormula.Eval(strFinalWorking11)).ToString("n0")) / intDivided);

                                    //به تعداد intReapetCount
                                    //رکورد با ارتفاع intDivided درج می گردد

                                    //Vazn = 1;
                                    intCount = 1;


                                    for (int i = 0; i < intReapetCount; i++)
                                    {

                                        switch (VariableName)
                                        {
                                            case "x":
                                                {
                                                    Tool = intCount;
                                                    break;
                                                }
                                            case "y":
                                                {
                                                    Arz = intCount;
                                                    break;
                                                }
                                            case "z":
                                                {
                                                    Ertefa = intCount;
                                                    break;
                                                }
                                            case "w":
                                                {
                                                    Vazn = intCount;
                                                    break;
                                                }
                                            default:
                                                break;
                                        }

                                        long lngCountInDivid = intCount * intDivided;

                                        string strDes = " از " + (lngStartValue + lngCountInDivid).ToString("0.##") + " تا " + (lngCountInDivid + intDivided).ToString("0.##") + " متر ، " + intCount + " بار ";

                                        //decimal dErtefa = intDivided;
                                        clsRizMetreUsers RizMetreUserses1 = new clsRizMetreUsers();
                                        RizMetreUserses1.Shomareh = RM.Shomareh;
                                        ShomareNew++;
                                        RizMetreUserses1.ShomarehNew = ShomareNew.ToString();

                                        RizMetreUserses1.Sharh = RM.Sharh;
                                        RizMetreUserses1.Tedad = Tedad;
                                        RizMetreUserses1.Tool = Tool;
                                        RizMetreUserses1.Arz = Arz;
                                        RizMetreUserses1.Ertefa = Ertefa;
                                        RizMetreUserses1.Vazn = Vazn;
                                        RizMetreUserses1.Des = strDes;

                                        RizMetreUserses1.FBId = FBId;
                                        RizMetreUserses1.OperationsOfHamlId = 1;
                                        RizMetreUserses1.Type = "2";
                                        RizMetreUserses1.ForItem = strForItem;
                                        RizMetreUserses1.UseItem = strUseItem;
                                        RizMetreUserses1.LevelNumber = LevelNumber;
                                        RizMetreUserses1.InsertDateTime = Now;

                                        RizMetreUserses1.ConditionContextRel = ConditionContextRel;
                                        RizMetreUserses1.ConditionContextId = ConditionContextId;


                                        //Vazn++;
                                        intCount++;

                                        ///محاسبه مقدار جزء
                                        decimal? dMeghdarJoz1 = null;
                                        if (Tedad == null && Tool == null && Arz == null && Vazn == null)
                                            dMeghdarJoz1 = null;
                                        else
                                            dMeghdarJoz1 = (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                                            (Arz == null ? 1 : Arz.Value) * (Ertefa == null ? 1 : Ertefa) * (Vazn == null ? 1 : Vazn.Value);
                                        RizMetreUserses1.MeghdarJoz = dMeghdarJoz1;

                                        _context.RizMetreUserses.Add(RizMetreUserses1);

                                        blnCheckIsExistWarning = false;
                                        strWarning = "";
                                        Status = true;
                                        //در صورتی که شرایط برای آیتم قبلا درج نشده بود
                                        if (blnItemsHasConditionAddedIsNew)
                                        {
                                            //در صورتی که شرایط برای آیتم در همین روال به کانتکست اضافه نشده بود
                                            if (!blnItemsHasConditionAddedToContext)
                                            {
                                                _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                                blnItemsHasConditionAddedToContext = true;
                                            }
                                        }
                                        ////////////////
                                        //////



                                        _context.ItemsHasConditionAddedToFBRizMetres.Add(
                                            new clsItemsHasConditionAddedToFBRizMetre
                                            {
                                                ItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBId,
                                                RizMetreId = RizMetreUserses1.ID,
                                                BaseRizMetreId = RM.Id
                                            });



                                        dSumAllRizMetre += dMeghdarJoz1 ?? 0;
                                    }
                                    int intRemain = intReapetCount * intDivided;

                                    strFinalWorking1 = strFinalWorkingSplit1[0].Replace(CharForReplace, strTAEForReoplace);

                                    decimal? dLastErtefa = Ertefa;
                                    decimal? dLastTool = Tool;
                                    //رکورد آخری هم بنا به شرط فاینل ارتفاع یا طول درج میگردد
                                    if (strFinalWorkingSplit[0] == "z")
                                    {
                                        dLastErtefa = decimal.Parse(stringToFormula.Eval(strFinalWorking1).ToString("0.##"));
                                        dLastErtefa -= intRemain;
                                        if (dLastErtefa > 0)
                                        {


                                            switch (VariableName)
                                            {
                                                case "x":
                                                    {
                                                        Tool = intCount;
                                                        break;
                                                    }
                                                case "y":
                                                    {
                                                        Arz = intCount;
                                                        break;
                                                    }
                                                case "z":
                                                    {
                                                        Ertefa = intCount;
                                                        break;
                                                    }
                                                case "w":
                                                    {
                                                        Vazn = intCount;
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }

                                            long lngCountInDivid = intCount * intDivided;

                                            string strDes = " از " + (lngStartValue + lngCountInDivid).ToString("0.##") + " تا " + decimal.Parse(strTAEForReoplace).ToString("0.##") + " متر ، " + intCount + " بار ";

                                            clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                            RizMetreUserses.Shomareh = RM.Shomareh;
                                            ShomareNew++;
                                            RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                                            RizMetreUserses.Sharh = RM.Sharh;
                                            RizMetreUserses.Tedad = Tedad;
                                            RizMetreUserses.Tool = Tool;
                                            RizMetreUserses.Arz = Arz;
                                            RizMetreUserses.Ertefa = dLastErtefa;
                                            RizMetreUserses.Vazn = Vazn;
                                            RizMetreUserses.Des = strDes;

                                            RizMetreUserses.FBId = FBId;
                                            RizMetreUserses.OperationsOfHamlId = 1;
                                            RizMetreUserses.Type = "2";
                                            RizMetreUserses.ForItem = strForItem;
                                            RizMetreUserses.UseItem = strUseItem;
                                            RizMetreUserses.LevelNumber = LevelNumber;
                                            RizMetreUserses.InsertDateTime = Now;

                                            RizMetreUserses.ConditionContextRel = ConditionContextRel;
                                            RizMetreUserses.ConditionContextId = ConditionContextId;



                                            ///محاسبه مقدار جزء
                                            decimal? dMeghdarJoz = null;
                                            if (Tedad == null && dLastTool == null && dLastErtefa == null && Arz == null && Vazn == null)
                                                dMeghdarJoz = null;
                                            else
                                                dMeghdarJoz = (Tedad == null ? 1 : Tedad.Value) * (dLastTool == null ? 1 : dLastTool.Value) *
                                                (Arz == null ? 1 : Arz.Value) * (dLastErtefa == null ? 1 : dLastErtefa.Value) * (Vazn == null ? 1 : Vazn.Value);
                                            RizMetreUserses.MeghdarJoz = dMeghdarJoz;

                                            _context.RizMetreUserses.Add(RizMetreUserses);

                                            blnCheckIsExistWarning = false;
                                            strWarning = "";
                                            Status = true;
                                            //در صورتی که شرایط برای آیتم قبلا درج نشده بود
                                            if (blnItemsHasConditionAddedIsNew)
                                            {
                                                //در صورتی که شرایط برای آیتم در همین روال به کانتکست اضافه نشده بود
                                                if (!blnItemsHasConditionAddedToContext)
                                                {
                                                    _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                                    blnItemsHasConditionAddedToContext = true;
                                                }
                                            }
                                            ////////////////
                                            ///


                                            _context.ItemsHasConditionAddedToFBRizMetres.Add(
                                            new clsItemsHasConditionAddedToFBRizMetre
                                            {
                                                ItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBId,
                                                RizMetreId = RizMetreUserses.ID,
                                                BaseRizMetreId = RM.Id

                                            });


                                            dSumAllRizMetre += dMeghdarJoz ?? 0;

                                            //Start///////////////////////
                                            ////////////حمل///////////////
                                            //////////////////////////////
                                            ///آیتم های حمل درج میگردد
                                            ///

                                            SaveHamlDto requestSaveHaml = new SaveHamlDto
                                            {
                                                BarAvordUserId = BarAvordId,
                                                Year = Year,
                                                ItemFBShomareh = strItemShomareh,
                                                NoeFBId = NoeFBId,
                                                //BarAvordHamlId = gBarAvordHamlId,
                                                Shomareh = RizMetreUserses.Shomareh,
                                                MeghdarJoz = dMeghdarJoz,
                                                LevelNumber = 1,
                                            };
                                            await HamlCommon.SaveHaml(requestSaveHaml, _context);
                                        }
                                    }
                                    else if (strFinalWorkingSplit[0] == "x")
                                    {
                                        dLastTool = decimal.Parse(stringToFormula.Eval(strFinalWorking1).ToString("0.##"));
                                        dLastTool -= intRemain;
                                        if (dLastTool > 0)
                                        {

                                            switch (VariableName)
                                            {
                                                case "x":
                                                    {
                                                        Tool = intCount;
                                                        break;
                                                    }
                                                case "y":
                                                    {
                                                        Arz = intCount;
                                                        break;
                                                    }
                                                case "z":
                                                    {
                                                        Ertefa = intCount;
                                                        break;
                                                    }
                                                case "w":
                                                    {
                                                        Vazn = intCount;
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                            long lngCountInDivid = intCount * intDivided;

                                            string strDes = " از " + (lngStartValue + lngCountInDivid).ToString("0.##") + " تا " + decimal.Parse(strTAEForReoplace).ToString("0.##") + " متر ، " + intCount + " بار ";


                                            clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                            RizMetreUserses.Shomareh = RM.Shomareh;
                                            ShomareNew++;
                                            RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                                            RizMetreUserses.Sharh = RM.Sharh;
                                            RizMetreUserses.Tedad = Tedad;
                                            RizMetreUserses.Tool = dLastTool;
                                            RizMetreUserses.Arz = Arz;
                                            RizMetreUserses.Ertefa = Ertefa;
                                            RizMetreUserses.Vazn = Vazn;
                                            RizMetreUserses.Des = strDes;

                                            RizMetreUserses.FBId = FBId;
                                            RizMetreUserses.OperationsOfHamlId = 1;
                                            RizMetreUserses.Type = "2";
                                            RizMetreUserses.ForItem = strForItem;
                                            RizMetreUserses.UseItem = strUseItem;
                                            RizMetreUserses.LevelNumber = LevelNumber;
                                            RizMetreUserses.InsertDateTime = Now;

                                            RizMetreUserses.ConditionContextRel = ConditionContextRel;
                                            RizMetreUserses.ConditionContextId = ConditionContextId;

                                            ///محاسبه مقدار جزء
                                            decimal? dMeghdarJoz = null;
                                            if (Tedad == null && dLastTool == null && dLastErtefa == null && Arz == null && Vazn == null)
                                                dMeghdarJoz = null;
                                            else
                                                dMeghdarJoz = (Tedad == null ? 1 : Tedad.Value) * (dLastTool == null ? 1 : dLastTool.Value) *
                                                (Arz == null ? 1 : Arz.Value) * (dLastErtefa == null ? 1 : dLastErtefa.Value) * (Vazn == null ? 1 : Vazn.Value);
                                            RizMetreUserses.MeghdarJoz = dMeghdarJoz;

                                            _context.RizMetreUserses.Add(RizMetreUserses);

                                            blnCheckIsExistWarning = false;
                                            strWarning = "";
                                            Status = true;

                                            //در صورتی که شرایط برای آیتم قبلا درج نشده بود
                                            if (blnItemsHasConditionAddedIsNew)
                                            {
                                                //در صورتی که شرایط برای آیتم در همین روال به کانتکست اضافه نشده بود
                                                if (!blnItemsHasConditionAddedToContext)
                                                {
                                                    _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                                    blnItemsHasConditionAddedToContext = true;
                                                }
                                            }

                                            _context.ItemsHasConditionAddedToFBRizMetres.Add(
                                            new clsItemsHasConditionAddedToFBRizMetre
                                            {
                                                ItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBId,
                                                RizMetreId = RizMetreUserses.ID,
                                                BaseRizMetreId = RM.Id

                                            });


                                            dSumAllRizMetre += dMeghdarJoz ?? 0;


                                            //Start///////////////////////
                                            ////////////حمل///////////////
                                            //////////////////////////////
                                            ///آیتم های حمل درج میگردد
                                            ///


                                            SaveHamlDto requestSaveHaml = new SaveHamlDto
                                            {
                                                BarAvordUserId = BarAvordId,
                                                Year = Year,
                                                ItemFBShomareh = strItemShomareh,
                                                NoeFBId = NoeFBId,
                                                //BarAvordHamlId = gBarAvordHamlId,
                                                Shomareh = RizMetreUserses.Shomareh,
                                                MeghdarJoz = dMeghdarJoz,
                                                LevelNumber = 1,
                                                BaseFBItem = strItemsFBShomareh
                                            };
                                            await HamlCommon.SaveHaml(requestSaveHaml, _context);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //}
                }
                /////////////////////////////////////
                //////دریافت جمع ریزمتره های جاری///
                /////////////////////////////////////
                decimal? dSumRizMetreCurrentFB = RizMetreCommon.SumRizMetreCurrentFB(FBId, _context);

                decimal? dSumRizMetreCurrentFBAll = dSumRizMetreCurrentFB.HasValue
                       ? (dSumRizMetreCurrentFB ?? 0) + dSumAllRizMetre
                       : null;

                varFBUser.SumRizMetre = dSumRizMetreCurrentFBAll;

                //await _context.SaveChangesAsync();
            }
        }

        return new ConditionCaseCommonCaseResultDto
        {
            blnCheckIsExistWarning = blnCheckIsExistWarning,
            blnStatus = Status,
            strWarning = strWarning
        };
    }

}
