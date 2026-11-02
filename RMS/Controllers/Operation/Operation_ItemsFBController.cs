using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Common;
using RMS.Models.Entity;
using System.Data;
using System.Text;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation;

public class OperationRequest
{
    public long Operation { get; set; }
}
public class Operation_ItemsFBController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;

    public IActionResult Index()
    {
        return View();
    }


    public JsonResult GetOperation_ItemsFB([FromBody] OperationRequestDto request)
    {
        NoeFehrestBaha NoeFBId = request.NoeFBId;
        int LevelNumber = request.LevelNumber;
        DastyarCommon DastyarCommon = new DastyarCommon(_context);
        clsOperation_ItemsFB Operation_ItemsFB = new clsOperation_ItemsFB();
        var varBA = _context.BaravordUsers.Where(x => x.ID == request.BarAvordUserId).ToList();
        DataTable DtBA = varBA.ToDataTable();
        var varDt = _context.Operation_ItemsFBs.Where(x => x.OperationId == request.Operation && x.Year == request.Year && x.NoeFB == NoeFBId).ToList();
        DataTable Dt = varDt.ToDataTable();
        string strItemShomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
        var varFB = _context.FehrestBahas.Where(x => x.Shomareh == strItemShomareh && x.Sal == request.Year && x.NoeFB == NoeFBId).ToList();
        DataTable DtFB = varFB.ToDataTable();
        Guid DtBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
        var varFBUser = _context.FBs.Where(x => x.BarAvordId == DtBAId && x.NoeFBId == NoeFBId && x.Shomareh == strItemShomareh).ToList();
        DataTable DtFBUser = varFBUser.ToDataTable();
        var varItemsForGetValues = _context.ItemsForGetValuess.Where(x => x.ItemShomareh == strItemShomareh && x.Year == request.Year).ToList();
        DataTable DtItemsForGetValues = varItemsForGetValues.ToDataTable();
        Guid FBId = new Guid();
        if (DtFBUser.Rows.Count == 0)
        {
            clsFB FB = new clsFB();
            FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
            FB.Shomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
            FB.BahayeVahedZarib = 0;
            FB.NoeFBId = NoeFBId;
            _context.FBs.Add(FB);
            _context.SaveChanges();
            FBId = FB.ID;
        }
        else
            FBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
        /////////////////

        var varOpItems = _context.Operation_ItemsFBs.Where(x => x.ItemsFBShomareh == strItemShomareh && x.Year == request.Year && x.NoeFB == NoeFBId).ToList();
        DataTable DtOpItems = varOpItems.ToDataTable();
        string strItemShomareh1 = DtOpItems.Rows[0]["ItemsFBShomareh"].ToString().Trim();

        List<clsItemsFields> ItemFields = _context.ItemsFieldses.Where(x => x.ItemShomareh == strItemShomareh1.Substring(0, 6) && x.NoeFB == NoeFBId).OrderBy(x => x.FieldType).ToList();

        string lstItemsFields = "";
        string lstItemsFieldEssentialValue = "";
        string lstDefaultMaxMin = "";

        bool blnItemHasEzafeBaha = false;

        foreach (var item in ItemFields)
        {
            lstItemsFields += item.IsEnteringValue + ",";
            lstItemsFieldEssentialValue += item.EssentialValue + ",";
            if (item.DefaultValue != null || item.MaxValue != null || item.MinValue != null)
            {
                lstDefaultMaxMin += item.DefaultValue + "_" + item.MinValue + "_" + item.MaxValue + ",";
            }
            else
                lstDefaultMaxMin += ",";
        }


        Guid guIDCheck = new Guid();
        if (FBId != guIDCheck)
        {
            bool blnIsEzafeBahaAddedItems = false;
            var varRizMetreUsers = (from RUsers in _context.RizMetreUserses
                                    join fb in _context.FBs on RUsers.FBId equals fb.ID
                                    where RUsers.LevelNumber == request.LevelNumber
                                    select new
                                    {
                                        RUsers.ID,
                                        RUsers.Shomareh,
                                        RUsers.ShomarehNew,
                                        RUsers.Sharh,
                                        RUsers.Tedad,
                                        RUsers.Tool,
                                        RUsers.Arz,
                                        RUsers.Ertefa,
                                        RUsers.Vazn,
                                        RUsers.Des,
                                        RUsers.ForItem,
                                        RUsers.Type,
                                        RUsers.UseItem,
                                        fb.BarAvordId,
                                        RUsers.FBId
                                    }).Where(x => x.FBId == FBId).OrderBy(x => x.Shomareh).OrderBy(x => x.Shomareh).ToList();

            string str = "";
            string strItemsShowInRelItems = "";
            var varItemsHasCondition = (from tblItemsHasCondition in _context.ItemsHasConditions
                                        join tblItemsHasCondition_ConditionContext in _context.ItemsHasCondition_ConditionContexts
                                        on tblItemsHasCondition.Id equals tblItemsHasCondition_ConditionContext.ItemsHasConditionId
                                        join tblConditionContext in _context.ConditionContexts on
                                        tblItemsHasCondition_ConditionContext.ConditionContextId equals tblConditionContext.Id
                                        join tblConditionGroup in _context.ConditionGroups on
                                        tblConditionContext.ConditionGroupId equals tblConditionGroup.Id
                                        select new
                                        {
                                            tblItemsHasCondition_ConditionContext.Id,
                                            ItemsHasConditionId = tblItemsHasCondition.Id,
                                            ItemFBShomareh = tblItemsHasCondition.ItemFBShomareh,
                                            tblItemsHasCondition_ConditionContext.HasEnteringValue,
                                            tblConditionContext.Context,
                                            tblItemsHasCondition_ConditionContext.Des,
                                            tblConditionGroup.ConditionGroupName,
                                            ConditionGroupId = tblConditionGroup.Id,
                                            tblItemsHasCondition_ConditionContext.DefaultValue,
                                            tblItemsHasCondition_ConditionContext.IsShow,
                                            tblItemsHasCondition_ConditionContext.ParentId,
                                            tblItemsHasCondition_ConditionContext.MoveToRel,
                                            tblItemsHasCondition_ConditionContext.ViewCheckAllRecords,
                                            tblItemsHasCondition.Year,
                                            tblItemsHasCondition.NoeFB
                                        }).Where(x => x.ItemFBShomareh == strItemShomareh && x.Year == request.Year && x.NoeFB == request.NoeFBId).ToList();

            DataTable DtItemsHasCondition = varItemsHasCondition.ToDataTable();

            var varItemsHasConditionAddedToFB = (from tblItemsHasConditionAddedToFB in _context.ItemsHasConditionAddedToFBs
                                                 join tblItemsHasCondition_ConditionContext in _context.ItemsHasCondition_ConditionContexts
                                                 on tblItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId equals tblItemsHasCondition_ConditionContext.Id
                                                 join tblItemsHasCondition in _context.ItemsHasConditions on tblItemsHasCondition_ConditionContext.ItemsHasConditionId equals tblItemsHasCondition.Id
                                                 select new
                                                 {
                                                     tblItemsHasConditionAddedToFB.ID,
                                                     tblItemsHasConditionAddedToFB.FBShomareh,
                                                     tblItemsHasConditionAddedToFB.BarAvordId,
                                                     tblItemsHasConditionAddedToFB.Meghdar,
                                                     tblItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId,
                                                     tblItemsHasConditionAddedToFB.ConditionGroupId,
                                                     ItemFBShomareh = tblItemsHasCondition.ItemFBShomareh,
                                                     tblItemsHasConditionAddedToFB.NoeFB
                                                 }).Where(x => x.FBShomareh == strItemShomareh && x.BarAvordId == request.BarAvordUserId && x.NoeFB == request.NoeFBId).ToList();
            DataTable DtItemsHasConditionAddedToFB = varItemsHasConditionAddedToFB.ToDataTable();



            //////////////////
            //////////////////
            //////////////////
            //        var sb = new StringBuilder();

            //        var gridId = $"Grid{(int)NoeFBId}_{Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim()}";

            //        var field0Vahed = ItemFields[0].Vahed.ToString().Trim();
            //        var field1Vahed = ItemFields[1].Vahed.ToString().Trim();
            //        var field2Vahed = ItemFields[2].Vahed.ToString().Trim();
            //        var field3Vahed = ItemFields[3].Vahed.ToString().Trim();
            //        var field4Vahed = ItemFields[4].Vahed.ToString().Trim();
            //        var field5Vahed = ItemFields[5].Vahed.ToString().Trim();

            //        var firstColumnTitle = blnIsEzafeBahaAddedItems ? "انتخاب" : "ردیف";

            //        sb.Append($"""
            //<div id="{gridId}">

            //    <div class="row styleHeaderTable">

            //        <div class="col-md-1 spanStyleMitraSmall">
            //            <span id="spanFieldShomarehName">
            //                {firstColumnTitle}
            //            </span>
            //        </div>

            //        <div class="col-md-2 spanStyleMitraSmall">
            //            شرح
            //        </div>

            //        <div class="col-md-1 spanStyleMitraSmall">
            //            <div style="padding-bottom:3px;border-bottom:1px solid #f2eaea">
            //                تعداد
            //            </div>
            //            <div class="VahedStyle">
            //                {field0Vahed}
            //            </div>
            //        </div>

            //        <div class="col-md-1 spanStyleMitraSmall">
            //            <div style="padding-bottom:3px;border-bottom:1px solid #f2eaea">
            //                طول
            //            </div>
            //            <div class="VahedStyle">
            //                {field1Vahed}
            //            </div>
            //        </div>

            //        <div class="col-md-1 spanStyleMitraSmall">
            //            <div style="padding-bottom:3px;border-bottom:1px solid #f2eaea">
            //                عرض
            //            </div>
            //            <div class="VahedStyle">
            //                {field2Vahed}
            //            </div>
            //        </div>

            //        <div class="col-md-1 spanStyleMitraSmall">
            //            <div style="padding-bottom:3px;border-bottom:1px solid #f2eaea">
            //                ارتفاع
            //            </div>
            //            <div class="VahedStyle">
            //                {field3Vahed}
            //            </div>
            //        </div>

            //        <div class="col-md-1 spanStyleMitraSmall">
            //            <div style="padding-bottom:3px;border-bottom:1px solid #f2eaea">
            //                وزن یا...
            //            </div>
            //            <div class="VahedStyle">
            //                {field4Vahed}
            //            </div>
            //        </div>

            //        <div class="col-md-1 spanStyleMitraSmall">
            //            <div style="padding-bottom:3px;border-bottom:1px solid #f2eaea">
            //                <span>مقدار جزء</span>
            //            </div>
            //            <div class="VahedStyle">
            //                {field5Vahed}
            //            </div>
            //        </div>

            //        <div class="col-md-2 spanStyleMitraSmall">
            //            توضیحات
            //        </div>

            //        <div class="col-md-1 spanStyleMitraSmall">
            //            <span>ویرایش/حذف</span>
            //        </div>

            //    </div>

            //    <div class="row styleFieldTable">
            //        <div class="col-md-12 RMCollectStyle">
            //        </div>
            //    </div>

            //</div>
            //""");

            ////////////////
            /////////////////
            //////////////////


            //str += "<div class=\"\" id=\"Grid" + (int)NoeFBId + "_" + Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim() + "\">";
            //str += "<div class=\"row styleHeaderTable\"><div class=\"col-md-1 spanStyleMitraSmall\"><span id=\"spanFieldShomarehName\">" + (blnIsEzafeBahaAddedItems ? "انتخاب" : "ردیف") + "</span></div><div class=\"col-md-2 spanStyleMitraSmall\">شرح</div>";
            //str += "<div class=\"col-md-1 spanStyleMitraSmall\"><div style=\"padding-bottom:3px;border-bottom:1px solid #f2eaea\">تعداد</div><div class=\"VahedStyle\">" + ItemFields[0].Vahed.ToString().Trim() + " </div></div><div class=\"col-md-1 spanStyleMitraSmall\"><div style=\"padding-bottom:3px;border-bottom:1px solid #f2eaea\">طول</div><div class=\"VahedStyle\">" + ItemFields[1].Vahed.ToString().Trim() + "</div></div>";
            //str += "<div class=\"col-md-1 spanStyleMitraSmall\"><div style=\"padding-bottom:3px;border-bottom:1px solid #f2eaea\">عرض</div><div class=\"VahedStyle\">" + (ItemFields[2].Vahed==null?"": ItemFields[2].Vahed.ToString().Trim()) + "</div></div><div class=\"col-md-1 spanStyleMitraSmall\"><div style=\"padding-bottom:3px;border-bottom:1px solid #f2eaea\">ارتفاع</div><div class=\"VahedStyle\">" + (ItemFields[3].Vahed==null?"": ItemFields[3].Vahed.ToString().Trim()) + "</div></div>";
            //str += "<div class=\"col-md-1 spanStyleMitraSmall\"><div style=\"padding-bottom:3px;border-bottom: 1px solid #f2eaea;\">وزن یا...</div><div class=\"VahedStyle\">" + ItemFields[4].Vahed==null?"": ItemFields[4].Vahed.ToString().Trim() + "</div></div><div class=\"col-md-1 spanStyleMitraSmall\"><div style=\"padding-bottom:3px;border-bottom:1px solid #f2eaea\"><span>مقدار جزء</span></div><div class=\"VahedStyle\">" + (ItemFields.Count > 5 ? ItemFields[5].Vahed==null?"": ItemFields[5].Vahed.ToString().Trim() : "") + "</div></div><div class=\"col-md-2 spanStyleMitraSmall\">توضیحات</div>";
            //str += "<div class=\"col-md-1 spanStyleMitraSmall\"><span>ویرایش/حذف</span></div></div>";

            //str += "<div class=\"row styleFieldTable\">";
            //str += "<div class=\"col-md-12 RMCollectStyle\">";
            //str += "</div>";
            //str += "</div>";
            //str += "</div>";




            var sb = new StringBuilder();

            string gridId =
                $"Grid{(int)NoeFBId}_{Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim()}";

            string GetVahed(int index)
            {
                return ItemFields.Count > index
                    ? ItemFields[index].Vahed?.ToString().Trim() ?? ""
                    : "";
            }

            sb.Append($"<div id=\"{gridId}\">");

            sb.Append("<div class=\"row styleHeaderTable\">");

            sb.Append($@"
    <div class=""col-md-1 spanStyleMitraSmall"">
        <span id=""spanFieldShomarehName"">
            {(blnIsEzafeBahaAddedItems ? "انتخاب" : "ردیف")}
        </span>
    </div>

    <div class=""col-md-2 spanStyleMitraSmall"">
        شرح
    </div>

    <div class=""col-md-1 spanStyleMitraSmall"">
        <div style=""padding-bottom:3px;border-bottom:1px solid #f2eaea"">
            تعداد
        </div>
        <div class=""VahedStyle"">
            {GetVahed(0)}
        </div>
    </div>

    <div class=""col-md-1 spanStyleMitraSmall"">
        <div style=""padding-bottom:3px;border-bottom:1px solid #f2eaea"">
            طول
        </div>
        <div class=""VahedStyle"">
            {GetVahed(1)}
        </div>
    </div>

    <div class=""col-md-1 spanStyleMitraSmall"">
        <div style=""padding-bottom:3px;border-bottom:1px solid #f2eaea"">
            عرض
        </div>
        <div class=""VahedStyle"">
            {GetVahed(2)}
        </div>
    </div>

    <div class=""col-md-1 spanStyleMitraSmall"">
        <div style=""padding-bottom:3px;border-bottom:1px solid #f2eaea"">
            ارتفاع
        </div>
        <div class=""VahedStyle"">
            {GetVahed(3)}
        </div>
    </div>

    <div class=""col-md-1 spanStyleMitraSmall"">
        <div style=""padding-bottom:3px;border-bottom:1px solid #f2eaea"">
            وزن یا...
        </div>
        <div class=""VahedStyle"">
            {GetVahed(4)}
        </div>
    </div>

    <div class=""col-md-1 spanStyleMitraSmall"">
        <div style=""padding-bottom:3px;border-bottom:1px solid #f2eaea"">
            <span>مقدار جزء</span>
        </div>
        <div class=""VahedStyle"">
            {GetVahed(5)}
        </div>
    </div>

    <div class=""col-md-2 spanStyleMitraSmall"">
        توضیحات
    </div>

    <div class=""col-md-1 spanStyleMitraSmall"">
        <span>ویرایش/حذف</span>
    </div>
");

            sb.Append("</div>");

            sb.Append(@"
    <div class=""row styleFieldTable"">
        <div class=""col-md-12 RMCollectStyle"">
        </div>
    </div>
");

            sb.Append("</div>");

            str += sb.ToString();


            List<int> lst = new List<int>();

            str += "<div id=\"divItemsAddedAndRel" + (int)NoeFBId + "_" + Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim() + "\">";

            str += "</div>";

            if (varRizMetreUsers.Count != 0)
            {
                decimal dSumAll = 0;

                foreach (var RizMetre in varRizMetreUsers)
                {
                    decimal dMeghdarJoz = 0;
                    decimal dTedad = RizMetre.Tedad == null ? 0 : RizMetre.Tedad.Value; //decimal.Parse(DtRizMetreUsers.Rows[i]["Tedad"].ToString() == "" ? "0" : DtRizMetreUsers.Rows[i]["Tedad"].ToString());
                    decimal dTool = RizMetre.Tool == null ? 0 : RizMetre.Tool.Value; //decimal.Parse(DtRizMetreUsers.Rows[i]["Tool"].ToString() == "" ? "0" : DtRizMetreUsers.Rows[i]["Tool"].ToString());
                    decimal dArz = RizMetre.Arz == null ? 0 : RizMetre.Arz.Value; //decimal.Parse(DtRizMetreUsers.Rows[i]["Arz"].ToString() == "" ? "0" : DtRizMetreUsers.Rows[i]["Arz"].ToString());
                    decimal dErtefa = RizMetre.Ertefa == null ? 0 : RizMetre.Ertefa.Value; //decimal.Parse(DtRizMetreUsers.Rows[i]["Ertefa"].ToString() == "" ? "0" : DtRizMetreUsers.Rows[i]["Ertefa"].ToString());
                    decimal dVazn = RizMetre.Vazn == null ? 0 : RizMetre.Vazn.Value; //decimal.Parse(DtRizMetreUsers.Rows[i]["Vazn"].ToString() == "" ? "0" : DtRizMetreUsers.Rows[i]["Vazn"].ToString());

                    if (dTedad == 0 && dTool == 0 && dArz == 0 && dErtefa == 0 && dVazn == 0)
                        dMeghdarJoz = 0;
                    else
                        dMeghdarJoz += (dTedad == 0 ? 1 : dTedad) * (dTool == 0 ? 1 : dTool) *
                        (dArz == 0 ? 1 : dArz) * (dErtefa == 0 ? 1 : dErtefa) * (dVazn == 0 ? 1 : dVazn);


                    DataTable DtItemsHasConditionFiltered = new DataTable();
                    if (DtItemsHasCondition.Rows.Count != 0)
                    {
                        string strItemAdded = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                        DataRow[] DrItemsHasCondition = DtItemsHasCondition.Select("ItemFBShomareh=" + Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim() + " and IsShow=1");
                        if (DrItemsHasCondition.Length != 0)
                        {
                            DtItemsHasConditionFiltered = DrItemsHasCondition.CopyToDataTable();
                        }
                    }

                    string strEzafeBaha = "";
                    List<EzafeBahaForRizMetreUsersDto> lstEBRM = new List<EzafeBahaForRizMetreUsersDto>();

                    for (int m = 0; m < lst.Count; m++)
                    {
                        DataRow[] DrItemsHasConditionWithItemFBShomarehFiltered = DtItemsHasConditionFiltered.Select("ConditionGroupId=" + lst[m]);

                        if (DrItemsHasConditionWithItemFBShomarehFiltered.Length != 0)
                        {
                            if (blnItemHasEzafeBaha)
                            {
                                clsRizMetreUsers rizMetreUser = new clsRizMetreUsers();
                                if (DastyarCommon.CheckLakeGiriIsAddingRizMetreUsers1(Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim(), DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString().Trim(), request.BarAvordUserId, RizMetre.ID))
                                {
                                    strEzafeBaha = "<div class=\"col-md-6\" style=\"padding-right:0px;\"><input checked=\"checked\" id=\"CKEzafeBaha" + RizMetre.ID.ToString() + lst[m] + "\" onclick=\"SelectEzafeBahaOrLakeGiriRecords('" + RizMetre.ID.ToString() + "','" + Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim() + "'," + LevelNumber + ")\" type=\"checkbox\" /></div>";
                                    EzafeBahaForRizMetreUsersDto EBRM = new EzafeBahaForRizMetreUsersDto()
                                    {
                                        RizMetreId = RizMetre.ID.ToString(), //DtRizMetreUsers.Rows[i]["ID"].ToString(),
                                        htmlEzafeBaha = strEzafeBaha
                                    };
                                    lstEBRM.Add(EBRM);
                                }
                                else
                                {
                                    if (blnIsEzafeBahaAddedItems)
                                    {
                                        strEzafeBaha = "<div class=\"col-md-6\" style=\"padding-right:0px;\"><input id=\"CKEzafeBaha" + RizMetre.ID.ToString() + lst[m] + "\" onclick=\"SelectEzafeBahaOrLakeGiriRecords('" + RizMetre.ID.ToString() + "','" + Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim() + "'," + LevelNumber + ")\" type=\"checkbox\" /></div>";
                                        EzafeBahaForRizMetreUsersDto EBRM = new EzafeBahaForRizMetreUsersDto()
                                        {
                                            RizMetreId = RizMetre.ID.ToString(),//DtRizMetreUsers.Rows[i]["ID"].ToString(),
                                            htmlEzafeBaha = strEzafeBaha
                                        };
                                        lstEBRM.Add(EBRM);
                                    }
                                    else
                                    {
                                        strEzafeBaha = "<div class=\"col-md-6\" style=\"padding-right:0px;\"><input class=\"displayNone\" id=\"CKEzafeBaha" + RizMetre.ID.ToString() + lst[m] + "\" onclick=\"SelectEzafeBahaOrLakeGiriRecords('" + RizMetre.ID.ToString() + "','" + Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim() + "'," + LevelNumber + ")\" type=\"checkbox\" /></div>";
                                        EzafeBahaForRizMetreUsersDto EBRM = new EzafeBahaForRizMetreUsersDto()
                                        {
                                            RizMetreId = RizMetre.ID.ToString(),
                                            htmlEzafeBaha = strEzafeBaha
                                        };
                                        lstEBRM.Add(EBRM);
                                    }
                                }
                            }
                        }
                    }

                    dSumAll += dMeghdarJoz;

                }

            }


            var result = new
            {
                result = "OK",
                FBId = FBId,
                ItemsFBShomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString(),
                str = str,
                lstItemsFields = lstItemsFields,
                lstItemsFieldEssentialValue = lstItemsFieldEssentialValue,
                lstDefaultMaxMin = lstDefaultMaxMin
            };

            return new JsonResult(result);//("OK_" + FBId.ToString() + "_" + Dt.Rows[0]["ItemsFBShomareh"].ToString() + "_" + str);
        }
        return new JsonResult("");

    }

    public JsonResult GetRizMetreWithFBId([FromBody] GetRizMetreWithFBIdInputDto request)
    {

        clsOperation_ItemsFB? operation_ItemsFB = _context.Operation_ItemsFBs.Where(x => x.OperationId == request.Operation).FirstOrDefault();
        if (operation_ItemsFB == null)
        {
            return new JsonResult("NOK_");
        }

        DeleteEditButtonManagementDto DeleteEditButtonMng = new DeleteEditButtonManagementDto()
        {
            hasDelButton = operation_ItemsFB.EnableDeleting == null ? true : operation_ItemsFB.EnableDeleting.Value,
            HasEditButton = operation_ItemsFB.EnableEditing == null ? true : operation_ItemsFB.EnableEditing.Value,
        };

        string strItemShomareh = operation_ItemsFB.ItemsFBShomareh.Trim();


        List<clsItemsFields> itemsFields = _context.ItemsFieldses.Where(x => x.ItemShomareh == strItemShomareh.Substring(0, 6) && x.NoeFB == request.NoeFB).OrderBy(x => x.FieldType).ToList();

        var varRizMetreUsers = (from RUsers in _context.RizMetreUserses
                                join fb in _context.FBs on RUsers.FBId equals fb.ID
                                where RUsers.LevelNumber == request.LevelNumber
                                select new GetRizMetreWithFBIdDto
                                {
                                    ID = RUsers.ID,
                                    Shomareh = RUsers.Shomareh,
                                    ShomarehNew = RUsers.ShomarehNew,
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
                                    FBId = RUsers.FBId,
                                    MeghdarJoz = RUsers.MeghdarJoz,
                                    strEzafeBaha = ""
                                })
                                .Where(x => x.FBId == request.FBId)
                                .OrderBy(x => x.Shomareh)
                                .ToList();


        var result = new
        {
            ItemsFields = itemsFields,
            RizMetreUsers = varRizMetreUsers,
            DeleteEditButtonMng
        };

        return new JsonResult(result);
    }

    //public JsonResult GetNOperation_ItemsFB([FromBody] GetNOperation_ItemsFBInputDto request)
    //{
    //    string ItemsFBShomareh = request.ItemsFBShomareh;
    //    long Operation = request.Operation;
    //    Guid BarAvordUserId = request.BarAvordUserId;
    //    int Type = request.Type;
    //    int Year = request.Year;
    //    NoeFehrestBaha NoeFB = request.NoeFB;
    //    int LevelNumber = request.LevelNumber;
    //    DateTime Now = DateTime.Now;

    //    //clsOperation_ItemsFB Operation_ItemsFB = new clsOperation_ItemsFB();
    //    var varBA = _context.BaravordUsers.Where(x => x.ID == BarAvordUserId).ToList();
    //    DataTable DtBA = clsConvert.ToDataTable(varBA);
    //    //DataTable DtBA = clsBaravordUser.ListWithParametr("ID=" + BarAvordId);
    //    var varDt = _context.Operation_ItemsFBs.Where(x => x.OperationId == Operation && x.Year == Year).ToList();
    //    DataTable Dt = clsConvert.ToDataTable(varDt);
    //    //DataTable Dt = clsOperation_ItemsFB.ListWithParameter("OperationId=" + Operation + " and Year=1397");
    //    //DataTable DtFB = clsFehrestBaha.ListWithParameter("Shomareh='" + Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim() + "' and sal=1397 and NoeFb=234");
    //    Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
    //    string strItemShomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
    //    var varFB = _context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strItemShomareh).ToList();
    //    DataTable DtFBUser = clsConvert.ToDataTable(varFB);
    //    //DataTable DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["ID"].ToString() + " and Shomareh='" + Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim() + "'");
    //    var varItemsForGetValues = _context.ItemsForGetValuess.Where(x => x.ItemShomareh == strItemShomareh && x.Year == Year).ToList();
    //    DataTable DtItemsForGetValues = clsConvert.ToDataTable(varItemsForGetValues);
    //    //DataTable DtItemsForGetValues = clsOperation_ItemsFB.ItemsForGetValuesListWithParameter("ItemShomareh='" + Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim() + "' and Year=1397");// and ItemShomarehForGetValue='" + ItemsFBShomareh + "'");

    //    Guid FBId = new Guid();
    //    if (DtFBUser.Rows.Count == 0)
    //    {
    //        clsFB FB = new clsFB();
    //        FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
    //        FB.Shomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
    //        FB.BahayeVahedZarib = 0;
    //        //FB.NoeFBId=noef
    //        _context.FBs.Add(FB);
    //        _context.SaveChanges();
    //        FBId = FB.ID;
    //        //FBId = Operation_ItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["ID"].ToString()), Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim(), 0);
    //    }
    //    else
    //        FBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

    //    var varRizMetreUsersAll = (from clsRizMetreUserss in _context.RizMetreUserses
    //                               join clsFB in _context.FBs on clsRizMetreUserss.FBId equals clsFB.ID
    //                               where clsRizMetreUserss.LevelNumber == LevelNumber
    //                               select new
    //                               {
    //                                   clsRizMetreUserss.ID,
    //                                   clsRizMetreUserss.Shomareh,
    //                                   clsRizMetreUserss.Sharh,
    //                                   clsRizMetreUserss.Tedad,
    //                                   clsRizMetreUserss.Tool,
    //                                   clsRizMetreUserss.Arz,
    //                                   clsRizMetreUserss.Ertefa,
    //                                   clsRizMetreUserss.Vazn,
    //                                   clsRizMetreUserss.Des,
    //                                   clsRizMetreUserss.FBId,
    //                                   clsRizMetreUserss.OperationsOfHamlId,
    //                                   clsRizMetreUserss.ForItem,
    //                                   FBShomareh = clsFB.Shomareh,
    //                                   clsFB.BarAvordId
    //                               }).Where(x => x.BarAvordId == BarAvordUserId).ToList();
    //    DataTable DtRizMetreUsersAll = clsConvert.ToDataTable(varRizMetreUsersAll);

    //    long ShomareNew = 1;
    //    clsRizMetreUsers? RizMetre = _context.RizMetreUserses.Include(x => x.FB).OrderByDescending(x => x.InsertDateTime).ThenByDescending(x => x.Shomareh).FirstOrDefault(x => x.FB.BarAvordId == BarAvordUserId);
    //    if (RizMetre != null)
    //    {
    //        long currentShomareNew = RizMetre.ShomarehNew == null || RizMetre.ShomarehNew.Trim() == "" ? 1 : long.Parse(RizMetre.ShomarehNew);
    //        if (currentShomareNew > RizMetre.Shomareh)
    //        {
    //            ShomareNew = currentShomareNew;
    //        }
    //        else
    //            ShomareNew = RizMetre.Shomareh;
    //    }
    //    //DataTable DtRizMetreUsersAll = clsRizMetreUsers.RizMetreUsersesWithFBListWithParameter("BarAvordId=" + BarAvordId);
    //    bool blnHasItemForGetValue = false;
    //    if (DtItemsForGetValues.Rows.Count != 0)
    //    {
    //        if (ItemsFBShomareh.Trim() != "")
    //        {
    //            DataRow[] DrItemsForGetValues = DtItemsForGetValues.Select("ItemShomarehForGetValue='" + ItemsFBShomareh.Trim() + "'");
    //            DataRow[] Dr = DtRizMetreUsersAll.Select("FBShomareh='" + DrItemsForGetValues[0]["ItemShomarehForGetValue"].ToString().Trim() + "'");
    //            if (Dr.Length != 0)
    //            {
    //                var varItemsFBShomarehValueShomareh = _context.ItemsFBShomarehValueShomarehs.Where(x => x.FBShomareh == ItemsFBShomareh.Trim() && x.BarAvordId == BarAvordUserId && x.Type == Type).ToList();
    //                DataTable DtItemsFBShomarehValueShomareh = clsConvert.ToDataTable(varItemsFBShomarehValueShomareh);

    //                //DataTable DtItemsFBShomarehValueShomareh = clsItemsFBShomarehValueShomareh.ListWithParameter("FBShomareh='" + ItemsFBShomareh.Trim() + "' and BarAvordId=" + BarAvordId + " and Type=" + Type);
    //                //clsItemsFBShomarehValueShomareh ItemsFBShomarehValueShomareh = new clsItemsFBShomarehValueShomareh();
    //                bool blnCheck = false;
    //                if (DtItemsFBShomarehValueShomareh.Rows.Count == 0)
    //                {
    //                    var varItemsForGetValuess = _context.ItemsForGetValuess.Where(x => x.ItemShomarehForGetValue == ItemsFBShomareh.Trim() && x.Year == Year).ToList();
    //                    DataTable DtItemsForGetValuess = clsConvert.ToDataTable(varItemsForGetValuess);

    //                    //DataTable DtItemsForGetValuess = clsOperation_ItemsFB.ItemsForGetValuesListWithParameter("ItemShomarehForGetValue='" + ItemsFBShomareh.Trim() + "' and Year=1397");// and ItemShomarehForGetValue='" + ItemsFBShomareh + "'");
    //                    List<string> strParam = new List<string>();
    //                    if (DtItemsForGetValuess.Rows.Count != 0)
    //                    {
    //                        for (int i = 0; i < DtItemsForGetValuess.Rows.Count; i++)
    //                        {
    //                            strParam.Add(DtItemsForGetValuess.Rows[i]["ItemShomareh"].ToString().Trim());
    //                            //if (strParam == "")
    //                            //    strParam += " ltrim(rtrim(clsFB.shomareh)) in('" + DtItemsForGetValuess.Rows[i]["ItemShomareh"].ToString().Trim() + "'";
    //                            //else
    //                            //    strParam += ",'" + DtItemsForGetValuess.Rows[i]["ItemShomareh"].ToString().Trim() + "'";
    //                        }
    //                        //strParam += ")";
    //                    }


    //                    var clsRizMetreUsers = _context.RizMetreUserses.Where(x => x.FB.BarAvordId == BarAvordUserId && x.ForItem == ItemsFBShomareh && strParam.Contains(x.FB.Shomareh)).ToList();

    //                    if (clsRizMetreUsers.Count != 0)
    //                    {
    //                        _context.RizMetreUserses.RemoveRange(clsRizMetreUsers);
    //                        _context.SaveChanges();
    //                    }
    //                    //clsRizMetreUsers.Delete(strParam + " and BarAvordId=" + BarAvordId + " and ForItem='" + ItemsFBShomareh.Trim() + "'");

    //                    try
    //                    {
    //                        clsItemsFBShomarehValueShomareh ItemsFBShomarehValueShomareh = new clsItemsFBShomarehValueShomareh();
    //                        ItemsFBShomarehValueShomareh.BarAvordId = BarAvordUserId;
    //                        ItemsFBShomarehValueShomareh.FBShomareh = ItemsFBShomareh.Trim();
    //                        ItemsFBShomarehValueShomareh.GetValuesShomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
    //                        ItemsFBShomarehValueShomareh.Type = Type;
    //                        _context.ItemsFBShomarehValueShomarehs.Add(ItemsFBShomarehValueShomareh);
    //                        _context.SaveChanges();
    //                        blnCheck = true;
    //                    }
    //                    catch (Exception)
    //                    {
    //                        blnCheck = false;
    //                        //throw;
    //                    }

    //                    //blnCheck = ItemsFBShomarehValueShomareh.Save();
    //                }
    //                else
    //                {
    //                    string strValuesShomareh = DtItemsFBShomarehValueShomareh.Rows[0]["GetValuesShomareh"].ToString().Trim();

    //                    var clsRizMetreUsers = _context.RizMetreUserses.Where(x => x.FB.Shomareh == strValuesShomareh && x.FB.BarAvordId == BarAvordUserId && x.ForItem == ItemsFBShomareh.Trim() && x.LevelNumber == LevelNumber).ToList();

    //                    if (clsRizMetreUsers.Count != 0)
    //                    {
    //                        _context.RizMetreUserses.RemoveRange(clsRizMetreUsers);
    //                        _context.SaveChanges();
    //                    }

    //                    string strItemsFBShomareh1 = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
    //                    //clsRizMetreUsers.Delete(" clsFB.Shomareh='" + DtItemsFBShomarehValueShomareh.Rows[0]["GetValuesShomareh"].ToString().Trim() + "' and BarAvordId=" + BarAvordId + " and ForItem='" + ItemsFBShomareh.Trim() + "'");
    //                    //try
    //                    //{


    //                    var getValuesShomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();

    //                    var item = _context.ItemsFBShomarehValueShomarehs
    //                        .FirstOrDefault(x =>
    //                            x.BarAvordId == BarAvordUserId &&
    //                            x.FBShomareh == ItemsFBShomareh.Trim() &&
    //                            x.Type == Type);

    //                    if (item != null)
    //                    {
    //                        item.GetValuesShomareh = getValuesShomareh;
    //                        _context.SaveChanges();
    //                        blnCheck = true;
    //                    }


    //                    //clsItemsFBShomarehValueShomareh ItemsFBShomarehValueShomareh = new clsItemsFBShomarehValueShomareh();
    //                    //ItemsFBShomarehValueShomareh.BarAvordId = BarAvordUserId;
    //                    //ItemsFBShomarehValueShomareh.FBShomareh = ItemsFBShomareh.Trim();
    //                    //ItemsFBShomarehValueShomareh.GetValuesShomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
    //                    //ItemsFBShomarehValueShomareh.Type = Type;

    //                    //var paramItemsFBShomareh = new SqlParameter("@FBShomareh", ItemsFBShomareh.Trim());
    //                    //var paramBarAvordUserId = new SqlParameter("@BarAvordUserId", BarAvordUserId);
    //                    //var paramGetValuesShomareh = new SqlParameter("@GetValuesShomareh", Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim());
    //                    //var paramType = new SqlParameter("@Type", Type);

    //                    //var Check = _context.Set<ItemsFBShomarehValueShomarehUpdateProcedureDto>()
    //                    //    .FromSqlRaw("EXEC ItemsFBShomarehValueShomarehUpdate @FBShomareh,@BarAvordUserId,@GetValuesShomareh,@Type", paramItemsFBShomareh, paramBarAvordUserId, paramGetValuesShomareh, paramType)
    //                    //    .AsEnumerable().FirstOrDefault();

    //                    //if (Check != null)
    //                    //{
    //                    //    blnCheck = Check.check;
    //                    //}

    //                    //blnCheck = ItemsFBShomarehValueShomareh.Update();
    //                    //var clsItemsFBShomarehValueShomareh = _context.ItemsFBShomarehValueShomarehs.Where(f => f.BarAvordId == BarAvordUserId
    //                    //&& f.FBShomareh == ItemsFBShomareh.Trim() && f.GetValuesShomareh == strItemsFBShomareh1 && f.Type == Type).ToList();
    //                    //if (clsItemsFBShomarehValueShomareh.Count!=0)
    //                    //{
    //                    //    _context.Entry(clsItemsFBShomarehValueShomareh).CurrentValues.SetValues(ItemsFBShomarehValueShomareh);
    //                    //    _context.SaveChanges();
    //                    //}
    //                    //blnCheck = true;
    //                    //}
    //                    //catch (Exception)
    //                    //{
    //                    //    blnCheck = false;
    //                    //    //throw;
    //                    //}

    //                    // blnCheck = ItemsFBShomarehValueShomareh.Update();
    //                }

    //                if (DtItemsForGetValues.Rows[0]["RizMetreFieldsRequire"].ToString() != "")
    //                {
    //                    if (blnCheck)
    //                    {
    //                        DataTable DtRizMetreUsersForInserted = Dr.CopyToDataTable();
    //                        blnHasItemForGetValue = true;
    //                        string strItemsFBShomareh1 = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
    //                        var varRizMetreUsersForGetValues = (from RUsers in _context.RizMetreUserses
    //                                                            join fb in _context.FBs on RUsers.FBId equals fb.ID
    //                                                            where RUsers.LevelNumber == LevelNumber
    //                                                            select new
    //                                                            {
    //                                                                Shomareh = RUsers.Shomareh,
    //                                                                Sharh = RUsers.Sharh,
    //                                                                Tedad = RUsers.Tedad,
    //                                                                Tool = RUsers.Tool,
    //                                                                Arz = RUsers.Arz,
    //                                                                Ertefa = RUsers.Ertefa,
    //                                                                Vazn = RUsers.Vazn,
    //                                                                Des = RUsers.Des,
    //                                                                ForItem = RUsers.ForItem,
    //                                                                Type = RUsers.Type,
    //                                                                UseItem = RUsers.UseItem,
    //                                                                BarAvordUserId = fb.BarAvordId,
    //                                                                FBId = RUsers.FBId
    //                                                            }).Where(x => x.FBId == FBId && x.ForItem == strItemsFBShomareh1).ToList();
    //                        DataTable DtRizMetreUsersForGetValues = clsConvert.ToDataTable(varRizMetreUsersForGetValues);

    //                        //DataTable DtRizMetreUsersForGetValues = clsRizMetreUsers.RizMetreUsersesListWithParameter("FBId=" + FBId + " and ForItem='" + Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim() + "'");
    //                        if (DtRizMetreUsersForGetValues.Rows.Count == 0)
    //                        {
    //                            string[] strRizMetreFieldsRequire = DtItemsForGetValues.Rows[0]["RizMetreFieldsRequire"].ToString().Split(',');
    //                            List<string> lst = new List<string>();
    //                            for (int j = 0; j < strRizMetreFieldsRequire.Length; j++)
    //                            {
    //                                lst.Add(strRizMetreFieldsRequire[j]);
    //                            }

    //                            for (int i = 0; i < DtRizMetreUsersForInserted.Rows.Count; i++)
    //                            {
    //                                clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
    //                                var strCal = lst.Where(a => a.Substring(0, 1) == "1").ToList();
    //                                if (strCal.Count != 0)
    //                                {
    //                                    string[] s = strCal[0].ToString().Split('+');
    //                                    decimal? dTedad = DtRizMetreUsersForInserted.Rows[i]["Tedad"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsersForInserted.Rows[i]["Tedad"].ToString().Trim());
    //                                    if (s.Length > 1)
    //                                        RizMetreUsers.Tedad = (dTedad == null ? 1 : dTedad) * decimal.Parse(s[1]) + dTedad == null ? 0 : dTedad;
    //                                    else
    //                                        RizMetreUsers.Tedad = dTedad;// decimal.Parse(DtRizMetreUsersForInserted.Rows[i]["Tedad"].ToString().Trim());
    //                                }
    //                                strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
    //                                if (strCal.Count != 0)
    //                                {
    //                                    string[] s = strCal[0].ToString().Split('+');
    //                                    decimal? dTool = DtRizMetreUsersForInserted.Rows[i]["Tool"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsersForInserted.Rows[i]["Tool"].ToString().Trim());
    //                                    if (s.Length > 1)
    //                                        RizMetreUsers.Tool = (dTool == null ? 1 : dTool) * decimal.Parse(s[1]) + dTool == null ? 0 : dTool;
    //                                    else
    //                                        RizMetreUsers.Tool = dTool;// decimal.Parse(DtRizMetreUsersForInserted.Rows[i]["Tool"].ToString().Trim());
    //                                }
    //                                strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
    //                                if (strCal.Count != 0)
    //                                {
    //                                    string[] s = strCal[0].ToString().Split('+');
    //                                    decimal? dArz = DtRizMetreUsersForInserted.Rows[i]["Arz"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsersForInserted.Rows[i]["Arz"].ToString().Trim());
    //                                    if (s.Length > 1)
    //                                        RizMetreUsers.Arz = (dArz == null ? 1 : dArz) * decimal.Parse(s[1]) + dArz == null ? 0 : dArz;
    //                                    else
    //                                        RizMetreUsers.Arz = dArz;// decimal.Parse(DtRizMetreUsersForInserted.Rows[i]["Arz"].ToString().Trim());
    //                                }
    //                                strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
    //                                if (strCal.Count != 0)
    //                                {
    //                                    string[] s = strCal[0].ToString().Split('+');
    //                                    decimal? dErtefa = DtRizMetreUsersForInserted.Rows[i]["Ertefa"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsersForInserted.Rows[i]["Ertefa"].ToString().Trim());
    //                                    if (s.Length > 1)
    //                                        RizMetreUsers.Ertefa = (dErtefa == null ? 1 : dErtefa) * decimal.Parse(s[1]) + dErtefa == null ? 0 : dErtefa;
    //                                    else
    //                                        RizMetreUsers.Ertefa = dErtefa; //decimal.Parse(DtRizMetreUsersForInserted.Rows[i]["Ertefa"].ToString().Trim());
    //                                }
    //                                strCal = lst.Where(a => a.Substring(0, 1) == "5").ToList();
    //                                if (strCal.Count != 0)
    //                                {
    //                                    string[] s = strCal[0].ToString().Split('+');
    //                                    decimal? dVazn = DtRizMetreUsersForInserted.Rows[i]["Vazn"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsersForInserted.Rows[i]["Vazn"].ToString().Trim());
    //                                    if (s.Length > 1)
    //                                        RizMetreUsers.Vazn = (dVazn == null ? 1 : dVazn) * decimal.Parse(s[1]) + dVazn == null ? 0 : dVazn;
    //                                    else
    //                                        RizMetreUsers.Vazn = dVazn;// decimal.Parse(DtRizMetreUsersForInserted.Rows[i]["Vazn"].ToString().Trim());
    //                                }
    //                                RizMetreUsers.FBId = FBId;
    //                                RizMetreUsers.Des = DtRizMetreUsersForInserted.Rows[i]["Des"].ToString().Trim();// "به آیتم شماره " + ItemsFBShomareh.Trim();
    //                                RizMetreUsers.Shomareh = long.Parse(DtRizMetreUsersForInserted.Rows[i]["Shomareh"].ToString().Trim());
    //                                ShomareNew++;
    //                                RizMetreUsers.ShomarehNew = ShomareNew.ToString();

    //                                RizMetreUsers.Sharh = DtRizMetreUsersForInserted.Rows[i]["Sharh"].ToString().Trim();
    //                                RizMetreUsers.ForItem = ItemsFBShomareh.Trim();
    //                                RizMetreUsers.UseItem = "";
    //                                RizMetreUsers.OperationsOfHamlId = 1;
    //                                RizMetreUsers.Type = "4";
    //                                RizMetreUsers.LevelNumber = LevelNumber;
    //                                RizMetreUsers.InsertDateTime = Now;

    //                                decimal dMeghdarJoz = 0;
    //                                if (RizMetreUsers.Tedad == null && RizMetreUsers.Tool == null && RizMetreUsers.Arz == null && RizMetreUsers.Ertefa == null && RizMetreUsers.Vazn == null)
    //                                    dMeghdarJoz = 0;
    //                                else
    //                                    dMeghdarJoz += (RizMetreUsers.Tedad == null ? 1 : RizMetreUsers.Tedad.Value) * (RizMetreUsers.Tool == null ? 1 : RizMetreUsers.Tool.Value) *
    //                                    (RizMetreUsers.Arz == null ? 1 : RizMetreUsers.Arz.Value) * (RizMetreUsers.Ertefa == null ? 1 : RizMetreUsers.Ertefa.Value)
    //                                    * (RizMetreUsers.Vazn == null ? 1 : RizMetreUsers.Vazn.Value);

    //                                RizMetreUsers.MeghdarJoz = dMeghdarJoz;

    //                                _context.RizMetreUserses.Add(RizMetreUsers);
    //                                _context.SaveChanges();
    //                                //RizMetreUsers.Save();
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    //////////////////
    //    var varItemsFields = (from ItemF in _context.ItemsFieldses
    //                          join OpItemFB in _context.Operation_ItemsFBs
    //                          on ItemF.ItemShomareh equals OpItemFB.ItemsFBShomareh
    //                          select new
    //                          {
    //                              ItemShomareh = ItemF.ItemShomareh,
    //                              NoeFB = ItemF.NoeFB,
    //                              IsEnteringValue = ItemF.IsEnteringValue,
    //                              Vahed = ItemF.Vahed,
    //                              FieldType = ItemF.FieldType,
    //                              OperationId = OpItemFB.OperationId
    //                          }).Where(x => x.OperationId == Operation && x.NoeFB == NoeFB).Distinct().OrderBy(x => x.FieldType).ToList();
    //    DataTable DtItemsFields = clsConvert.ToDataTable(varItemsFields);
    //    //DataTable DtItemsFields = clsItemsFields.ItemsFieldsListWithParameter("OperationId='" + Operation + "' and NoeFB=234");
    //    var varFB1 = _context.FBs.Where(x => x.ID == FBId).ToList();
    //    DataTable DtFB = clsConvert.ToDataTable(varFB1);
    //    //DataTable DtFB = clsOperation_ItemsFB.FBListWithParameter("ID=" + FBId);
    //    var varItemsHasConditionAddedToFB = (from tblItemsHasConditionAddedToFB in _context.ItemsHasConditionAddedToFBs
    //                                         join tblItemsHasCondition_ConditionContext in _context.ItemsHasCondition_ConditionContexts
    //                                         on tblItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId equals tblItemsHasCondition_ConditionContext.Id
    //                                         join tblItemsHasCondition in _context.ItemsHasConditions on tblItemsHasCondition_ConditionContext.ItemsHasConditionId equals tblItemsHasCondition.Id
    //                                         select new
    //                                         {
    //                                             tblItemsHasConditionAddedToFB.ID,
    //                                             tblItemsHasConditionAddedToFB.FBShomareh,
    //                                             tblItemsHasConditionAddedToFB.BarAvordId,
    //                                             tblItemsHasConditionAddedToFB.Meghdar,
    //                                             tblItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId,
    //                                             tblItemsHasConditionAddedToFB.ConditionGroupId,
    //                                             ItemFBShomareh = tblItemsHasCondition.ItemFBShomareh
    //                                         }).Where(x => x.FBShomareh == strItemShomareh && x.BarAvordId == BarAvordUserId && x.ItemFBShomareh == ItemsFBShomareh).ToList();
    //    DataTable DtItemsHasConditionAddedToFB = clsConvert.ToDataTable(varItemsHasConditionAddedToFB);
    //    //DataTable DtItemsHasConditionAddedToFB = clsItemsHasConditionAddedToFB.ListWithParameterSimple("BarAvordId=" + BarAvordId + " and FBShomareh='" + Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim() + "' and ItemFBShomareh='" + ItemsFBShomareh + "'");
    //    //DtItemsHasConditionAddedToFB = clsOperation_ItemsFB.ItemsHasConditionAddedToFB("FBShomareh='" + Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim() + "'");


    //    //DataTable DtItemsFBShomarehValueShomareh = clsItemsFBShomarehValueShomareh.ListWithParameter("FBShomareh='" + DtFB.Rows[0]["Shomareh"].ToString().Trim() + "' and BarAvordId=" + BarAvordId);
    //    if (DtItemsHasConditionAddedToFB.Rows.Count != 0)
    //    {
    //        //string strItemsHasCondition_ConditionContext = "";
    //        //if (DtItemsHasConditionAddedToFB.Rows.Count != 0)
    //        //{
    //        //    strItemsHasCondition_ConditionContext += "ItemsHasCondition_ConditionContextId in(";
    //        //    for (int i = 0; i < DtItemsHasConditionAddedToFB.Rows.Count; i++)
    //        //    {
    //        //        if ((i + 1) < DtItemsHasConditionAddedToFB.Rows.Count)
    //        //            strItemsHasCondition_ConditionContext += DtItemsHasConditionAddedToFB.Rows[i]["ItemsHasCondition_ConditionContextId"].ToString() + ",";
    //        //        else
    //        //            strItemsHasCondition_ConditionContext += DtItemsHasConditionAddedToFB.Rows[i]["ItemsHasCondition_ConditionContextId"].ToString();
    //        //    }
    //        //    strItemsHasCondition_ConditionContext += ")";
    //        //}
    //        long[] strItemsHasCondition_ConditionContext = new long[] { };
    //        if (DtItemsHasConditionAddedToFB.Rows.Count != 0)
    //        {
    //            for (int i = 0; i < DtItemsHasConditionAddedToFB.Rows.Count; i++)
    //            {
    //                strItemsHasCondition_ConditionContext[i] = long.Parse(DtItemsHasConditionAddedToFB.Rows[i]["ID"].ToString());
    //            }
    //        }

    //        var varItemsAddingToFB = _context.ItemsAddingToFBs.Where(x => strItemsHasCondition_ConditionContext.Contains(x.ItemsHasCondition_ConditionContextId)).ToList();
    //        DataTable DtItemsAddingToFB = clsConvert.ToDataTable(varItemsAddingToFB);

    //        //DataTable DtItemsAddingToFB = clsItemsAddingToFB.ListWithParameter(strItemsHasCondition_ConditionContext);
    //        for (int Counter = 0; Counter < DtItemsHasConditionAddedToFB.Rows.Count; Counter++)
    //        {
    //            decimal Meghdar = decimal.Parse(DtItemsHasConditionAddedToFB.Rows[Counter]["Meghdar"].ToString());
    //            string RBCode = DtItemsHasConditionAddedToFB.Rows[Counter]["ItemsHasCondition_ConditionContextId"].ToString().Trim();
    //            DataRow[] Dr = DtItemsAddingToFB.Select("ItemsHasCondition_ConditionContextId=" + RBCode);
    //            if (Dr.Length != 0)
    //            {
    //                for (int idr = 0; idr < Dr.Length; idr++)
    //                {
    //                    switch (Dr[idr]["ConditionType"].ToString())
    //                    {
    //                        case "1":
    //                            {
    //                                string strCharacterPlus = Dr[idr]["CharacterPlus"].ToString().Trim();
    //                                string strCondition = Dr[idr]["Condition"].ToString().Trim();
    //                                string strFinalWorking = Dr[idr]["FinalWorking"].ToString();
    //                                string strConditionOp = strCondition.Replace("x", Meghdar.ToString().Trim());
    //                                StringToFormula StringToFormula = new StringToFormula();
    //                                bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
    //                                if (blnCheck)
    //                                {
    //                                    strFinalWorking = strFinalWorking.Replace("x", Meghdar.ToString().Trim());
    //                                    decimal dPercent = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());
    //                                    //clsOperation_ItemsFB Operation_ItemsFB = new clsOperation_ItemsFB();
    //                                    //DataTable DtBA = clsOperation_ItemsFB.BarAvordListWithParameter("");

    //                                    guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
    //                                    string strAddedItems = Dr[idr]["AddedItems"].ToString().Trim();
    //                                    var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems + strCharacterPlus).ToList();
    //                                    DtFBUser = clsConvert.ToDataTable(varFBUsersAdded);

    //                                    //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["ID"].ToString() + " and Shomareh='" + Dr[idr]["AddedItems"].ToString().Trim() + "A'");
    //                                    Guid intFBId = new Guid();
    //                                    if (DtFBUser.Rows.Count == 0)
    //                                    {
    //                                        clsFB FBSave = new clsFB();
    //                                        FBSave.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
    //                                        FBSave.Shomareh = Dr[idr]["AddedItems"].ToString().Trim() + strCharacterPlus; //"A";
    //                                        FBSave.BahayeVahedZarib = dPercent;
    //                                        _context.FBs.Add(FBSave);
    //                                        _context.SaveChanges();
    //                                        intFBId = FBSave.ID;
    //                                        //intFBId = Operation_ItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["ID"].ToString()), Dr[idr]["AddedItems"].ToString().Trim() + "A", dPercent);
    //                                    }
    //                                    else
    //                                        intFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

    //                                    string strShomareh1 = DtFB.Rows[0]["Shomareh"].ToString().Trim();
    //                                    Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
    //                                    var varRizMetreUsers = (from RizMetreUsers in _context.RizMetreUserses
    //                                                            join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
    //                                                            where RizMetreUsers.LevelNumber == LevelNumber
    //                                                            select new
    //                                                            {
    //                                                                RizMetreUsers.ID,
    //                                                                RizMetreUsers.Shomareh,
    //                                                                RizMetreUsers.Tedad,
    //                                                                RizMetreUsers.Tool,
    //                                                                RizMetreUsers.Arz,
    //                                                                RizMetreUsers.Ertefa,
    //                                                                RizMetreUsers.Vazn,
    //                                                                RizMetreUsers.Sharh,
    //                                                                RizMetreUsers.Des,
    //                                                                RizMetreUsers.FBId,
    //                                                                RizMetreUsers.OperationsOfHamlId,
    //                                                                RizMetreUsers.ForItem,
    //                                                                RizMetreUsers.Type,
    //                                                                RizMetreUsers.UseItem,
    //                                                                FB.BarAvordId
    //                                                            }).Where(x => x.FBId == guFBId && x.Type == "1").OrderBy(x => x.Shomareh).ToList();
    //                                    DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);
    //                                    //DataTable DtRizMetreUsers = clsRizMetreUsers.RizMetreUsersesListWithParameter("FBId=" + DtFB.Rows[0]["ID"].ToString() + " and Type=1");
    //                                    var varRizMetreUsersCurrent = (from RizMetreUsers in _context.RizMetreUserses
    //                                                                   join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
    //                                                                   where RizMetreUsers.LevelNumber == LevelNumber
    //                                                                   select new
    //                                                                   {
    //                                                                       RizMetreUsers.ID,
    //                                                                       RizMetreUsers.Shomareh,
    //                                                                       RizMetreUsers.Tedad,
    //                                                                       RizMetreUsers.Tool,
    //                                                                       RizMetreUsers.Arz,
    //                                                                       RizMetreUsers.Ertefa,
    //                                                                       RizMetreUsers.Vazn,
    //                                                                       RizMetreUsers.Sharh,
    //                                                                       RizMetreUsers.Des,
    //                                                                       RizMetreUsers.FBId,
    //                                                                       RizMetreUsers.OperationsOfHamlId,
    //                                                                       RizMetreUsers.ForItem,
    //                                                                       RizMetreUsers.Type,
    //                                                                       RizMetreUsers.UseItem,
    //                                                                       FB.BarAvordId
    //                                                                   }).Where(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2").OrderBy(x => x.Shomareh).ToList();
    //                                    DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsers);
    //                                    //DataTable DtRizMetreUsersCurrent = clsRizMetreUsers.RizMetreUsersesListWithParameter("FBId=" + intFBId + " and ForItem='" + DtFB.Rows[0]["Shomareh"].ToString().Trim() + "' and Type=2");
    //                                    for (int i = 0; i < DtRizMetreUsers.Rows.Count; i++)
    //                                    {
    //                                        DataRow[] DrRizMetreUsersCurrent = DtRizMetreUsersCurrent.Select("shomareh=" + int.Parse(DtRizMetreUsers.Rows[i]["Shomareh"].ToString()));
    //                                        if (DrRizMetreUsersCurrent.Length == 0)
    //                                        {
    //                                            clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
    //                                            RizMetreUsers.Shomareh = long.Parse(DtRizMetreUsers.Rows[i]["Shomareh"].ToString());
    //                                            ShomareNew++;
    //                                            RizMetreUsers.ShomarehNew = ShomareNew.ToString();

    //                                            RizMetreUsers.Sharh = DtRizMetreUsers.Rows[i]["Sharh"].ToString().Trim();

    //                                            if (DtItemsFields.Rows[0]["IsEnteringValue"].ToString() == "True")
    //                                                RizMetreUsers.Tedad = DtRizMetreUsers.Rows[i]["Tedad"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Tedad"].ToString());
    //                                            else
    //                                                RizMetreUsers.Tedad = null;

    //                                            if (DtItemsFields.Rows[1]["IsEnteringValue"].ToString() == "True")
    //                                                RizMetreUsers.Tool = DtRizMetreUsers.Rows[i]["Tool"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Tool"].ToString());
    //                                            else
    //                                                RizMetreUsers.Tool = null;

    //                                            if (DtItemsFields.Rows[2]["IsEnteringValue"].ToString() == "True")
    //                                                RizMetreUsers.Arz = DtRizMetreUsers.Rows[i]["Arz"].ToString() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Arz"].ToString());
    //                                            else
    //                                                RizMetreUsers.Arz = null;

    //                                            if (DtItemsFields.Rows[3]["IsEnteringValue"].ToString() == "True")
    //                                                RizMetreUsers.Ertefa = DtRizMetreUsers.Rows[i]["Ertefa"].ToString() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Ertefa"].ToString());
    //                                            else
    //                                                RizMetreUsers.Ertefa = null;

    //                                            if (DtItemsFields.Rows[4]["IsEnteringValue"].ToString() == "True")
    //                                                RizMetreUsers.Vazn = DtRizMetreUsers.Rows[i]["Vazn"].ToString() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Vazn"].ToString());
    //                                            else
    //                                                RizMetreUsers.Vazn = null;

    //                                            RizMetreUsers.Des = Dr[idr]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + DtFB.Rows[0]["Shomareh"].ToString().Trim();
    //                                            RizMetreUsers.FBId = intFBId;
    //                                            RizMetreUsers.OperationsOfHamlId = 1;
    //                                            RizMetreUsers.Type = "2";
    //                                            RizMetreUsers.ForItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
    //                                            RizMetreUsers.UseItem = "";
    //                                            RizMetreUsers.LevelNumber = LevelNumber;
    //                                            RizMetreUsers.InsertDateTime = Now;


    //                                            decimal dMeghdarJoz = 0;
    //                                            if (RizMetreUsers.Tedad == null && RizMetreUsers.Tool == null && RizMetreUsers.Arz == null && RizMetreUsers.Ertefa == null && RizMetreUsers.Vazn == null)
    //                                                dMeghdarJoz = 0;
    //                                            else
    //                                                dMeghdarJoz += (RizMetreUsers.Tedad == null ? 1 : RizMetreUsers.Tedad.Value) * (RizMetreUsers.Tool == null ? 1 : RizMetreUsers.Tool.Value) *
    //                                                (RizMetreUsers.Arz == null ? 1 : RizMetreUsers.Arz.Value) * (RizMetreUsers.Ertefa == null ? 1 : RizMetreUsers.Ertefa.Value)
    //                                                * (RizMetreUsers.Vazn == null ? 1 : RizMetreUsers.Vazn.Value);
    //                                            RizMetreUsers.MeghdarJoz = dMeghdarJoz;

    //                                            _context.RizMetreUserses.Add(RizMetreUsers);
    //                                            _context.SaveChanges();
    //                                            //RizMetreUsers.Save();
    //                                        }
    //                                    }
    //                                }
    //                                break;
    //                            }
    //                        case "2":
    //                            {
    //                                //clsOperation_ItemsFB Operation_ItemsFB = new clsOperation_ItemsFB();
    //                                //DataTable DtBA = clsOperation_ItemsFB.BarAvordListWithParameter("");
    //                                string strShomarehAdd = Dr[0]["AddedItems"].ToString().Trim();
    //                                var varFBUser = _context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strShomarehAdd).ToList();
    //                                DtFBUser = clsConvert.ToDataTable(varFBUser);

    //                                //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["ID"].ToString() + " and Shomareh='" + Dr[0]["AddedItems"].ToString().Trim() + "'");
    //                                Guid intFBId = new Guid();
    //                                if (DtFBUser.Rows.Count == 0)
    //                                {
    //                                    clsFB Fb = new clsFB();
    //                                    Fb.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
    //                                    Fb.Shomareh = Dr[0]["AddedItems"].ToString().Trim();
    //                                    Fb.BahayeVahedZarib = 0;
    //                                    _context.FBs.Add(Fb);
    //                                    _context.SaveChanges();
    //                                    //intFBId = Operation_ItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["ID"].ToString()), Dr[0]["AddedItems"].ToString().Trim(), 0);
    //                                }
    //                                else
    //                                    intFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

    //                                Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
    //                                var varRizMetreUsers = (from RizMetreUsers in _context.RizMetreUserses
    //                                                        join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
    //                                                        where RizMetreUsers.LevelNumber == LevelNumber
    //                                                        select new
    //                                                        {
    //                                                            RizMetreUsers.ID,
    //                                                            RizMetreUsers.Shomareh,
    //                                                            RizMetreUsers.Tedad,
    //                                                            RizMetreUsers.Tool,
    //                                                            RizMetreUsers.Arz,
    //                                                            RizMetreUsers.Ertefa,
    //                                                            RizMetreUsers.Vazn,
    //                                                            RizMetreUsers.Sharh,
    //                                                            RizMetreUsers.Des,
    //                                                            RizMetreUsers.FBId,
    //                                                            RizMetreUsers.OperationsOfHamlId,
    //                                                            RizMetreUsers.ForItem,
    //                                                            RizMetreUsers.Type,
    //                                                            RizMetreUsers.UseItem,
    //                                                            FB.BarAvordId
    //                                                        }).Where(x => x.FBId == guFBId && x.Type == "1").OrderBy(x => x.Shomareh).ToList();
    //                                DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);
    //                                string strShomareh1 = DtFB.Rows[0]["Shomareh"].ToString().Trim();
    //                                var varRizMetreUsersCurrent = (from RizMetreUsers in _context.RizMetreUserses
    //                                                               join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
    //                                                               where RizMetreUsers.LevelNumber == LevelNumber
    //                                                               select new
    //                                                               {
    //                                                                   RizMetreUsers.ID,
    //                                                                   RizMetreUsers.Shomareh,
    //                                                                   RizMetreUsers.Tedad,
    //                                                                   RizMetreUsers.Tool,
    //                                                                   RizMetreUsers.Arz,
    //                                                                   RizMetreUsers.Ertefa,
    //                                                                   RizMetreUsers.Vazn,
    //                                                                   RizMetreUsers.Sharh,
    //                                                                   RizMetreUsers.Des,
    //                                                                   RizMetreUsers.FBId,
    //                                                                   RizMetreUsers.OperationsOfHamlId,
    //                                                                   RizMetreUsers.ForItem,
    //                                                                   RizMetreUsers.Type,
    //                                                                   RizMetreUsers.UseItem,
    //                                                                   FB.BarAvordId
    //                                                               }).Where(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2").OrderBy(x => x.Shomareh).ToList();
    //                                DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsers);

    //                                //DataTable DtRizMetreUsers = clsRizMetreUsers.RizMetreUsersesListWithParameter("FBId=" + DtFB.Rows[0]["ID"].ToString() + " and Type=1");
    //                                //DataTable DtRizMetreUsersCurrent = clsRizMetreUsers.RizMetreUsersesListWithParameter("FBId=" + intFBId + " and ForItem='" + DtFB.Rows[0]["Shomareh"].ToString().Trim() + "' and Type=2");
    //                                for (int i = 0; i < DtRizMetreUsers.Rows.Count; i++)
    //                                {
    //                                    DataRow[] DrRizMetreUsersCurrent = DtRizMetreUsersCurrent.Select("shomareh=" + long.Parse(DtRizMetreUsers.Rows[i]["Shomareh"].ToString()));
    //                                    if (DrRizMetreUsersCurrent.Length == 0)
    //                                    {
    //                                        clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
    //                                        RizMetreUsers.Shomareh = long.Parse(DtRizMetreUsers.Rows[i]["Shomareh"].ToString());
    //                                        ShomareNew++;
    //                                        RizMetreUsers.ShomarehNew = ShomareNew.ToString();

    //                                        RizMetreUsers.Sharh = DtRizMetreUsers.Rows[i]["Sharh"].ToString().Trim();

    //                                        if (DtItemsFields.Rows[0]["IsEnteringValue"].ToString() == "True")
    //                                            RizMetreUsers.Tedad = DtRizMetreUsers.Rows[i]["Tedad"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Tedad"].ToString());
    //                                        else
    //                                            RizMetreUsers.Tedad = null;

    //                                        if (DtItemsFields.Rows[1]["IsEnteringValue"].ToString() == "True")
    //                                            RizMetreUsers.Tool = DtRizMetreUsers.Rows[i]["Tool"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Tool"].ToString());
    //                                        else
    //                                            RizMetreUsers.Tool = null;

    //                                        if (DtItemsFields.Rows[2]["IsEnteringValue"].ToString() == "True")
    //                                            RizMetreUsers.Arz = DtRizMetreUsers.Rows[i]["Arz"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Arz"].ToString());
    //                                        else
    //                                            RizMetreUsers.Arz = null;

    //                                        if (DtItemsFields.Rows[3]["IsEnteringValue"].ToString() == "True")
    //                                            RizMetreUsers.Ertefa = DtRizMetreUsers.Rows[i]["Ertefa"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Ertefa"].ToString());
    //                                        else
    //                                            RizMetreUsers.Ertefa = null;

    //                                        if (DtItemsFields.Rows[4]["IsEnteringValue"].ToString() == "True")
    //                                            RizMetreUsers.Vazn = DtRizMetreUsers.Rows[i]["Vazn"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Vazn"].ToString());
    //                                        else
    //                                            RizMetreUsers.Vazn = null;

    //                                        RizMetreUsers.Des = Dr[0]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + DtFB.Rows[0]["Shomareh"].ToString().Trim();
    //                                        RizMetreUsers.FBId = intFBId;
    //                                        RizMetreUsers.OperationsOfHamlId = 1;
    //                                        RizMetreUsers.Type = "2";
    //                                        RizMetreUsers.ForItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
    //                                        RizMetreUsers.UseItem = "";
    //                                        RizMetreUsers.LevelNumber = LevelNumber;
    //                                        RizMetreUsers.InsertDateTime = Now;


    //                                        decimal dMeghdarJoz = 0;
    //                                        if (RizMetreUsers.Tedad == null && RizMetreUsers.Tool == null && RizMetreUsers.Arz == null && RizMetreUsers.Ertefa == null && RizMetreUsers.Vazn == null)
    //                                            dMeghdarJoz = 0;
    //                                        else
    //                                            dMeghdarJoz += (RizMetreUsers.Tedad == null ? 1 : RizMetreUsers.Tedad.Value) * (RizMetreUsers.Tool == null ? 1 : RizMetreUsers.Tool.Value) *
    //                                            (RizMetreUsers.Arz == null ? 1 : RizMetreUsers.Arz.Value) * (RizMetreUsers.Ertefa == null ? 1 : RizMetreUsers.Ertefa.Value)
    //                                            * (RizMetreUsers.Vazn == null ? 1 : RizMetreUsers.Vazn.Value);

    //                                        RizMetreUsers.MeghdarJoz = dMeghdarJoz;


    //                                        _context.RizMetreUserses.Add(RizMetreUsers);
    //                                        _context.SaveChanges();
    //                                        //RizMetreUsers.Save();
    //                                    }
    //                                }
    //                                break;
    //                            }
    //                        case "3":
    //                            {
    //                                decimal dPercent = decimal.Parse(Dr[0]["FinalWorking"].ToString());
    //                                //decimal CharacterPlus = decimal.Parse(Dr[0]["CharacterPlus"].ToString());
    //                                //clsOperation_ItemsFB Operation_ItemsFB = new clsOperation_ItemsFB();
    //                                //DataTable DtBA = clsOperation_ItemsFB.BarAvordListWithParameter("");
    //                                string strAddedItems = Dr[0]["AddedItems"].ToString().Trim();
    //                                string strStatus = dPercent > 0 ? "B" : "e";
    //                                var varFBUser = _context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems + strStatus).ToList();
    //                                DtFBUser = clsConvert.ToDataTable(varFBUser);
    //                                //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["ID"].ToString() + " and Shomareh='" + Dr[0]["AddedItems"].ToString().Trim() + strStatus + "'");
    //                                Guid intFBId = new Guid();
    //                                if (DtFBUser.Rows.Count == 0)
    //                                {
    //                                    clsFB Fb = new clsFB();
    //                                    Fb.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
    //                                    Fb.Shomareh = Dr[0]["AddedItems"].ToString().Trim() + strStatus;
    //                                    Fb.BahayeVahedZarib = dPercent;
    //                                    _context.FBs.Add(Fb);
    //                                    _context.SaveChanges();
    //                                    intFBId = Fb.ID;
    //                                    //intFBId = Operation_ItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["ID"].ToString()), Dr[0]["AddedItems"].ToString().Trim() + strStatus, dPercent);
    //                                }
    //                                else
    //                                    intFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

    //                                Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
    //                                var varRizMetreUsers = (from RizMetreUsers in _context.RizMetreUserses
    //                                                        join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
    //                                                        where RizMetreUsers.LevelNumber == LevelNumber
    //                                                        select new
    //                                                        {
    //                                                            RizMetreUsers.ID,
    //                                                            RizMetreUsers.Shomareh,
    //                                                            RizMetreUsers.Tedad,
    //                                                            RizMetreUsers.Tool,
    //                                                            RizMetreUsers.Arz,
    //                                                            RizMetreUsers.Ertefa,
    //                                                            RizMetreUsers.Vazn,
    //                                                            RizMetreUsers.Sharh,
    //                                                            RizMetreUsers.Des,
    //                                                            RizMetreUsers.FBId,
    //                                                            RizMetreUsers.OperationsOfHamlId,
    //                                                            RizMetreUsers.ForItem,
    //                                                            RizMetreUsers.Type,
    //                                                            RizMetreUsers.UseItem,
    //                                                            FB.BarAvordId
    //                                                        }).Where(x => x.FBId == guFBId && x.Type == "1").OrderBy(x => x.Shomareh).ToList();
    //                                DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);
    //                                string strShomareh1 = DtFB.Rows[0]["Shomareh"].ToString().Trim();
    //                                var varRizMetreUsersCurrent = (from RizMetreUsers in _context.RizMetreUserses
    //                                                               join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
    //                                                               where RizMetreUsers.LevelNumber == LevelNumber
    //                                                               select new
    //                                                               {
    //                                                                   RizMetreUsers.ID,
    //                                                                   RizMetreUsers.Shomareh,
    //                                                                   RizMetreUsers.Tedad,
    //                                                                   RizMetreUsers.Tool,
    //                                                                   RizMetreUsers.Arz,
    //                                                                   RizMetreUsers.Ertefa,
    //                                                                   RizMetreUsers.Vazn,
    //                                                                   RizMetreUsers.Sharh,
    //                                                                   RizMetreUsers.Des,
    //                                                                   RizMetreUsers.FBId,
    //                                                                   RizMetreUsers.OperationsOfHamlId,
    //                                                                   RizMetreUsers.ForItem,
    //                                                                   RizMetreUsers.Type,
    //                                                                   RizMetreUsers.UseItem,
    //                                                                   FB.BarAvordId
    //                                                               }).Where(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2").OrderBy(x => x.Shomareh).ToList();
    //                                DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsers);
    //                                //DataTable DtRizMetreUsers = clsRizMetreUsers.RizMetreUsersesListWithParameter("FBId=" + DtFB.Rows[0]["ID"].ToString() + " and Type=1");
    //                                //DataTable DtRizMetreUsersCurrent = clsRizMetreUsers.RizMetreUsersesListWithParameter("FBId=" + intFBId + " and ForItem='" + DtFB.Rows[0]["Shomareh"].ToString().Trim() + "' and Type=2");
    //                                for (int i = 0; i < DtRizMetreUsers.Rows.Count; i++)
    //                                {
    //                                    DataRow[] DrRizMetreUsersCurrent = DtRizMetreUsersCurrent.Select("shomareh=" + int.Parse(DtRizMetreUsers.Rows[i]["Shomareh"].ToString()));
    //                                    if (DrRizMetreUsersCurrent.Length == 0)
    //                                    {
    //                                        clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
    //                                        RizMetreUsers.Shomareh = long.Parse(DtRizMetreUsers.Rows[i]["Shomareh"].ToString());
    //                                        ShomareNew++;
    //                                        RizMetreUsers.ShomarehNew = ShomareNew.ToString();

    //                                        RizMetreUsers.Sharh = DtRizMetreUsers.Rows[i]["Sharh"].ToString().Trim();

    //                                        if (DtItemsFields.Rows[0]["IsEnteringValue"].ToString() == "True")
    //                                            RizMetreUsers.Tedad = DtRizMetreUsers.Rows[i]["Tedad"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Tedad"].ToString());
    //                                        else
    //                                            RizMetreUsers.Tedad = null;

    //                                        if (DtItemsFields.Rows[1]["IsEnteringValue"].ToString() == "True")
    //                                            RizMetreUsers.Tool = DtRizMetreUsers.Rows[i]["Tool"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Tool"].ToString());
    //                                        else
    //                                            RizMetreUsers.Tool = null;

    //                                        if (DtItemsFields.Rows[2]["IsEnteringValue"].ToString() == "True")
    //                                            RizMetreUsers.Arz = DtRizMetreUsers.Rows[i]["Arz"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Arz"].ToString());
    //                                        else
    //                                            RizMetreUsers.Arz = null;

    //                                        if (DtItemsFields.Rows[3]["IsEnteringValue"].ToString() == "True")
    //                                            RizMetreUsers.Ertefa = DtRizMetreUsers.Rows[i]["Ertefa"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Ertefa"].ToString());
    //                                        else
    //                                            RizMetreUsers.Ertefa = null;

    //                                        if (DtItemsFields.Rows[4]["IsEnteringValue"].ToString() == "True")
    //                                            RizMetreUsers.Vazn = DtRizMetreUsers.Rows[i]["Vazn"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Vazn"].ToString());
    //                                        else
    //                                            RizMetreUsers.Vazn = null;

    //                                        RizMetreUsers.Des = Dr[0]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + DtFB.Rows[0]["Shomareh"].ToString().Trim();
    //                                        RizMetreUsers.FBId = intFBId;
    //                                        RizMetreUsers.OperationsOfHamlId = 1;
    //                                        RizMetreUsers.Type = "2";
    //                                        RizMetreUsers.ForItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
    //                                        RizMetreUsers.UseItem = "";
    //                                        RizMetreUsers.LevelNumber = LevelNumber;
    //                                        RizMetreUsers.InsertDateTime = Now;


    //                                        decimal dMeghdarJoz = 0;
    //                                        if (RizMetreUsers.Tedad == 0 && RizMetreUsers.Tool == 0 && RizMetreUsers.Arz == 0 && RizMetreUsers.Ertefa == 0 && RizMetreUsers.Vazn == 0)
    //                                            dMeghdarJoz = 0;
    //                                        else
    //                                            dMeghdarJoz += (RizMetreUsers.Tedad == null ? 1 : RizMetreUsers.Tedad.Value) * (RizMetreUsers.Tool == null ? 1 : RizMetreUsers.Tool.Value) *
    //                                            (RizMetreUsers.Arz == null ? 1 : RizMetreUsers.Arz.Value) * (RizMetreUsers.Ertefa == null ? 1 : RizMetreUsers.Ertefa.Value)
    //                                            * (RizMetreUsers.Vazn == null ? 1 : RizMetreUsers.Vazn.Value);
    //                                        RizMetreUsers.MeghdarJoz = dMeghdarJoz;


    //                                        _context.RizMetreUserses.Add(RizMetreUsers);
    //                                        _context.SaveChanges();
    //                                        //RizMetreUsers.Save();
    //                                    }
    //                                }
    //                                break;
    //                            }
    //                        case "4":
    //                            {
    //                                string strCondition = Dr[idr]["Condition"].ToString().Trim();
    //                                string strFinalWorking = Dr[idr]["FinalWorking"].ToString();
    //                                string strConditionOp = strCondition.Replace("x", Meghdar.ToString().Trim());
    //                                StringToFormula StringToFormula = new StringToFormula();
    //                                bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
    //                                if (blnCheck)
    //                                {
    //                                    strFinalWorking = strFinalWorking.Replace("x", Meghdar.ToString().Trim());
    //                                    decimal dMultiple = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());
    //                                    //clsOperation_ItemsFB Operation_ItemsFB = new clsOperation_ItemsFB();
    //                                    //DataTable DtBA = clsOperation_ItemsFB.BarAvordListWithParameter("");
    //                                    string strShomarehAdd = Dr[idr]["AddedItems"].ToString().Trim();
    //                                    var varFBUser = _context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strShomarehAdd).ToList();
    //                                    DtFBUser = clsConvert.ToDataTable(varFBUser);
    //                                    //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["ID"].ToString() + " and Shomareh='" + Dr[idr]["AddedItems"].ToString().Trim() + "'");
    //                                    Guid intFBId = new Guid();
    //                                    if (DtFBUser.Rows.Count == 0)
    //                                    {
    //                                        clsFB Fb = new clsFB();
    //                                        Fb.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
    //                                        Fb.Shomareh = Dr[0]["AddedItems"].ToString().Trim();
    //                                        Fb.BahayeVahedZarib = 0;
    //                                        _context.FBs.Add(Fb);
    //                                        _context.SaveChanges();
    //                                        intFBId = Fb.ID;
    //                                        //intFBId = Operation_ItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["ID"].ToString()), Dr[idr]["AddedItems"].ToString().Trim(), 0);
    //                                    }
    //                                    else
    //                                        intFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

    //                                    DataTable DtRizMetreUsers = new DataTable();
    //                                    string strForItem = "";
    //                                    string strUseItem = "";
    //                                    string strItemFBShomareh = ItemsFBShomareh;// DtFB.Rows[0]["Shomareh"].ToString().Trim();

    //                                    if (Dr[idr]["UseItemForAdd"].ToString().Trim() == "")
    //                                    {
    //                                        strForItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
    //                                        Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
    //                                        var varRizMetreUsers = (from RizMetreUsers in _context.RizMetreUserses
    //                                                                join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
    //                                                                where RizMetreUsers.LevelNumber == LevelNumber
    //                                                                select new
    //                                                                {
    //                                                                    RizMetreUsers.ID,
    //                                                                    RizMetreUsers.Shomareh,
    //                                                                    RizMetreUsers.Tedad,
    //                                                                    RizMetreUsers.Tool,
    //                                                                    RizMetreUsers.Arz,
    //                                                                    RizMetreUsers.Ertefa,
    //                                                                    RizMetreUsers.Vazn,
    //                                                                    RizMetreUsers.Sharh,
    //                                                                    RizMetreUsers.Des,
    //                                                                    RizMetreUsers.FBId,
    //                                                                    RizMetreUsers.OperationsOfHamlId,
    //                                                                    RizMetreUsers.ForItem,
    //                                                                    RizMetreUsers.Type,
    //                                                                    RizMetreUsers.UseItem,
    //                                                                    FB.BarAvordId
    //                                                                }).Where(x => x.FBId == guFBId && x.Type == "1").OrderBy(x => x.Shomareh).ToList();
    //                                        DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);
    //                                        //DtRizMetreUsers = clsRizMetreUsers.RizMetreUsersesListWithParameter("FBId=" + DtFB.Rows[0]["ID"].ToString() + " and Type=1");
    //                                    }
    //                                    else
    //                                    {
    //                                        strUseItem = ItemsFBShomareh;// DtFB.Rows[0]["Shomareh"].ToString().Trim();
    //                                        strForItem = Dr[idr]["UseItemForAdd"].ToString().Trim();
    //                                        varFB = _context.FBs.Where(x => x.BarAvordId == BarAvordUserId && x.Shomareh == strForItem).ToList();
    //                                        DtFB = clsConvert.ToDataTable(varFBUser);
    //                                        //DtFB = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordId + " and Shomareh='" + strForItem + "'");
    //                                        Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
    //                                        var varRizMetreUsers = (from RizMetreUsers in _context.RizMetreUserses
    //                                                                join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
    //                                                                where RizMetreUsers.LevelNumber == LevelNumber
    //                                                                select new
    //                                                                {
    //                                                                    RizMetreUsers.ID,
    //                                                                    RizMetreUsers.Shomareh,
    //                                                                    RizMetreUsers.Tedad,
    //                                                                    RizMetreUsers.Tool,
    //                                                                    RizMetreUsers.Arz,
    //                                                                    RizMetreUsers.Ertefa,
    //                                                                    RizMetreUsers.Vazn,
    //                                                                    RizMetreUsers.Sharh,
    //                                                                    RizMetreUsers.Des,
    //                                                                    RizMetreUsers.FBId,
    //                                                                    RizMetreUsers.OperationsOfHamlId,
    //                                                                    RizMetreUsers.ForItem,
    //                                                                    RizMetreUsers.Type,
    //                                                                    RizMetreUsers.UseItem,
    //                                                                    FB.BarAvordId
    //                                                                }).Where(x => x.FBId == guFBId && x.ForItem == strItemFBShomareh && x.Type == "4").OrderBy(x => x.Shomareh).ToList();
    //                                        DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);
    //                                        //DtRizMetreUsers = clsRizMetreUsers.RizMetreUsersesListWithParameter("FBId=" + DtFB.Rows[0]["ID"].ToString() + " and ForItem='" + strItemFBShomareh + "' and Type=4");
    //                                    }
    //                                    var varRizMetreUsersCurrent = (from RizMetreUsers in _context.RizMetreUserses
    //                                                                   join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
    //                                                                   where RizMetreUsers.LevelNumber == LevelNumber
    //                                                                   select new
    //                                                                   {
    //                                                                       ID = RizMetreUsers.ID,
    //                                                                       Shomareh = RizMetreUsers.Shomareh,
    //                                                                       Tedad = RizMetreUsers.Tedad,
    //                                                                       Tool = RizMetreUsers.Tool,
    //                                                                       Arz = RizMetreUsers.Arz,
    //                                                                       Ertefa = RizMetreUsers.Ertefa,
    //                                                                       Vazn = RizMetreUsers.Vazn,
    //                                                                       Des = RizMetreUsers.Des,
    //                                                                       FBId = RizMetreUsers.FBId,
    //                                                                       OperationsOfHamlId = RizMetreUsers.OperationsOfHamlId,
    //                                                                       ForItem = RizMetreUsers.ForItem,
    //                                                                       Type = RizMetreUsers.Type,
    //                                                                       UseItem = RizMetreUsers.UseItem,
    //                                                                       BaravordUserId = FB.BarAvordId
    //                                                                   }).Where(x => x.FBId == intFBId && x.ForItem == strForItem && x.UseItem == strUseItem && x.Type == "2").ToList();
    //                                    DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsersCurrent);

    //                                    //DataTable DtRizMetreUsersCurrent = clsRizMetreUsers.RizMetreUsersesListWithParameter("FBId=" + intFBId + " and ForItem='" + strForItem + "' and UseItem='" + strUseItem + "' and Type=2");
    //                                    for (int i = 0; i < DtRizMetreUsers.Rows.Count; i++)
    //                                    {
    //                                        DataRow[] DrRizMetreUsersCurrent = DtRizMetreUsersCurrent.Select("shomareh=" + int.Parse(DtRizMetreUsers.Rows[i]["Shomareh"].ToString()));
    //                                        if (DrRizMetreUsersCurrent.Length == 0)
    //                                        {
    //                                            clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
    //                                            RizMetreUsers.Shomareh = long.Parse(DtRizMetreUsers.Rows[i]["Shomareh"].ToString());
    //                                            ShomareNew++;
    //                                            RizMetreUsers.ShomarehNew = ShomareNew.ToString();

    //                                            RizMetreUsers.Sharh = DtRizMetreUsers.Rows[i]["Sharh"].ToString().Trim();

    //                                            if (DtItemsFields.Rows[0]["IsEnteringValue"].ToString() == "True")
    //                                                RizMetreUsers.Tedad = (DtRizMetreUsers.Rows[i]["Tedad"].ToString() == "" ? 1 : decimal.Parse(DtRizMetreUsers.Rows[i]["Tedad"].ToString())) * dMultiple;
    //                                            else
    //                                                RizMetreUsers.Tedad = null;

    //                                            if (DtItemsFields.Rows[1]["IsEnteringValue"].ToString() == "True")
    //                                                RizMetreUsers.Tool = DtRizMetreUsers.Rows[i]["Tool"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Tool"].ToString());
    //                                            else
    //                                                RizMetreUsers.Tool = null;

    //                                            if (DtItemsFields.Rows[2]["IsEnteringValue"].ToString() == "True")
    //                                                RizMetreUsers.Arz = DtRizMetreUsers.Rows[i]["Arz"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Arz"].ToString());
    //                                            else
    //                                                RizMetreUsers.Arz = null;

    //                                            if (DtItemsFields.Rows[3]["IsEnteringValue"].ToString() == "True")
    //                                                RizMetreUsers.Ertefa = decimal.Parse(DtRizMetreUsers.Rows[i]["Ertefa"].ToString());
    //                                            else
    //                                                RizMetreUsers.Ertefa = null;

    //                                            if (DtItemsFields.Rows[4]["IsEnteringValue"].ToString() == "True")
    //                                                RizMetreUsers.Vazn = DtRizMetreUsers.Rows[i]["Vazn"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Vazn"].ToString());
    //                                            else
    //                                                RizMetreUsers.Vazn = null;

    //                                            RizMetreUsers.Des = Dr[idr]["Des"].ToString().Trim();// Dr[idr]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + strForItem;
    //                                            RizMetreUsers.FBId = intFBId;
    //                                            RizMetreUsers.OperationsOfHamlId = 1;
    //                                            RizMetreUsers.Type = "2";
    //                                            RizMetreUsers.ForItem = strForItem;
    //                                            RizMetreUsers.UseItem = strUseItem;
    //                                            RizMetreUsers.LevelNumber = LevelNumber;
    //                                            RizMetreUsers.InsertDateTime = Now;


    //                                            decimal dMeghdarJoz = 0;
    //                                            if (RizMetreUsers.Tedad == null && RizMetreUsers.Tool == null && RizMetreUsers.Arz == null && RizMetreUsers.Ertefa == null && RizMetreUsers.Vazn == null)
    //                                                dMeghdarJoz = 0;
    //                                            else
    //                                                dMeghdarJoz += (RizMetreUsers.Tedad == null ? 1 : RizMetreUsers.Tedad.Value) * (RizMetreUsers.Tool == null ? 1 : RizMetreUsers.Tool.Value) *
    //                                                (RizMetreUsers.Arz == null ? 1 : RizMetreUsers.Arz.Value) * (RizMetreUsers.Ertefa == null ? 1 : RizMetreUsers.Ertefa.Value)
    //                                                * (RizMetreUsers.Vazn == null ? 1 : RizMetreUsers.Vazn.Value);
    //                                            RizMetreUsers.MeghdarJoz = dMeghdarJoz;


    //                                            _context.RizMetreUserses.Add(RizMetreUsers);
    //                                            _context.SaveChanges();
    //                                            //RizMetreUsers.Save();
    //                                        }
    //                                    }
    //                                }
    //                                break;
    //                            }
    //                        case "5":
    //                            {
    //                                string strCondition = Dr[idr]["Condition"].ToString().Trim();
    //                                string strConditionOp = strCondition.Replace("x", Meghdar.ToString().Trim());
    //                                StringToFormula StringToFormula = new StringToFormula();
    //                                bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
    //                                if (blnCheck)
    //                                {
    //                                    //clsOperation_ItemsFB Operation_ItemsFB = new clsOperation_ItemsFB();
    //                                    //DataTable DtBA = clsOperation_ItemsFB.BarAvordListWithParameter("");
    //                                    string strAddedItems = Dr[idr]["AddedItems"].ToString().Trim();
    //                                    var varFBUser = _context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems).ToList();
    //                                    DtFBUser = clsConvert.ToDataTable(varFBUser);
    //                                    //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["ID"].ToString() + " and Shomareh='" + Dr[idr]["AddedItems"].ToString().Trim() + "'");
    //                                    Guid intFBId = new Guid();
    //                                    if (DtFBUser.Rows.Count == 0)
    //                                    {
    //                                        clsFB FB = new clsFB();
    //                                        FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
    //                                        FB.Shomareh = Dr[0]["AddedItems"].ToString().Trim();
    //                                        FB.BahayeVahedZarib = 0;
    //                                        _context.FBs.Add(FB);
    //                                        _context.SaveChanges();
    //                                        intFBId = FB.ID;
    //                                        //intFBId = Operation_ItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["ID"].ToString()), Dr[idr]["AddedItems"].ToString().Trim(), 0);
    //                                    }
    //                                    else
    //                                        intFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());


    //                                    Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
    //                                    var varRizMetreUsers = (from RizMetreUsers in _context.RizMetreUserses
    //                                                            join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
    //                                                            where RizMetreUsers.LevelNumber == LevelNumber
    //                                                            select new
    //                                                            {
    //                                                                RizMetreUsers.ID,
    //                                                                RizMetreUsers.Shomareh,
    //                                                                RizMetreUsers.Tedad,
    //                                                                RizMetreUsers.Tool,
    //                                                                RizMetreUsers.Arz,
    //                                                                RizMetreUsers.Ertefa,
    //                                                                RizMetreUsers.Vazn,
    //                                                                RizMetreUsers.Sharh,
    //                                                                RizMetreUsers.Des,
    //                                                                RizMetreUsers.FBId,
    //                                                                RizMetreUsers.OperationsOfHamlId,
    //                                                                RizMetreUsers.ForItem,
    //                                                                RizMetreUsers.Type,
    //                                                                RizMetreUsers.UseItem,
    //                                                                FB.BarAvordId
    //                                                            }).Where(x => x.FBId == guFBId && x.Type == "1").OrderBy(x => x.Shomareh).ToList();
    //                                    DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);
    //                                    string strShomareh1 = DtFB.Rows[0]["Shomareh"].ToString().Trim();
    //                                    var varRizMetreUsersCurrent = (from RizMetreUsers in _context.RizMetreUserses
    //                                                                   join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
    //                                                                   where RizMetreUsers.LevelNumber == LevelNumber
    //                                                                   select new
    //                                                                   {
    //                                                                       ID = RizMetreUsers.ID,
    //                                                                       Shomareh = RizMetreUsers.Shomareh,
    //                                                                       Tedad = RizMetreUsers.Tedad,
    //                                                                       Tool = RizMetreUsers.Tool,
    //                                                                       Arz = RizMetreUsers.Arz,
    //                                                                       Ertefa = RizMetreUsers.Ertefa,
    //                                                                       Vazn = RizMetreUsers.Vazn,
    //                                                                       Des = RizMetreUsers.Des,
    //                                                                       FBId = RizMetreUsers.FBId,
    //                                                                       OperationsOfHamlId = RizMetreUsers.OperationsOfHamlId,
    //                                                                       ForItem = RizMetreUsers.ForItem,
    //                                                                       Type = RizMetreUsers.Type,
    //                                                                       UseItem = RizMetreUsers.UseItem,
    //                                                                       BaravordUserId = FB.BarAvordId
    //                                                                   }).Where(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
    //                                    DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsersCurrent);
    //                                    //DataTable DtRizMetreUsers = clsRizMetreUsers.RizMetreUsersesListWithParameter("FBId=" + DtFB.Rows[0]["ID"].ToString() + " and Type=1");
    //                                    //DataTable DtRizMetreUsersCurrent = clsRizMetreUsers.RizMetreUsersesListWithParameter("FBId=" + intFBId + " and ForItem='" + DtFB.Rows[0]["Shomareh"].ToString().Trim() + "' and Type=2");
    //                                    for (int i = 0; i < DtRizMetreUsers.Rows.Count; i++)
    //                                    {
    //                                        DataRow[] DrRizMetreUsersCurrent = DtRizMetreUsersCurrent.Select("shomareh=" + long.Parse(DtRizMetreUsers.Rows[i]["Shomareh"].ToString()));
    //                                        if (DrRizMetreUsersCurrent.Length == 0)
    //                                        {
    //                                            clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
    //                                            RizMetreUsers.Shomareh = long.Parse(DtRizMetreUsers.Rows[i]["Shomareh"].ToString());
    //                                            ShomareNew++;
    //                                            RizMetreUsers.ShomarehNew = ShomareNew.ToString();

    //                                            RizMetreUsers.Sharh = DtRizMetreUsers.Rows[i]["Sharh"].ToString().Trim();

    //                                            if (DtItemsFields.Rows[0]["IsEnteringValue"].ToString() == "True")
    //                                                RizMetreUsers.Tedad = DtRizMetreUsers.Rows[i]["Tedad"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Tedad"].ToString());
    //                                            else
    //                                                RizMetreUsers.Tedad = null;

    //                                            if (DtItemsFields.Rows[1]["IsEnteringValue"].ToString() == "True")
    //                                                RizMetreUsers.Tool = DtRizMetreUsers.Rows[i]["Tool"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Tool"].ToString());
    //                                            else
    //                                                RizMetreUsers.Tool = null;

    //                                            if (DtItemsFields.Rows[2]["IsEnteringValue"].ToString() == "True")
    //                                                RizMetreUsers.Arz = DtRizMetreUsers.Rows[i]["Arz"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Arz"].ToString());
    //                                            else
    //                                                RizMetreUsers.Arz = null;

    //                                            if (DtItemsFields.Rows[3]["IsEnteringValue"].ToString() == "True")
    //                                                RizMetreUsers.Ertefa = DtRizMetreUsers.Rows[i]["Ertefa"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Ertefa"].ToString());
    //                                            else
    //                                                RizMetreUsers.Ertefa = null;

    //                                            if (DtItemsFields.Rows[4]["IsEnteringValue"].ToString() == "True")
    //                                                RizMetreUsers.Vazn = DtRizMetreUsers.Rows[i]["Vazn"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Vazn"].ToString());
    //                                            else
    //                                                RizMetreUsers.Vazn = null;

    //                                            RizMetreUsers.Des = Dr[idr]["Des"].ToString().Trim();// Dr[idr]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + DtFB.Rows[0]["Shomareh"].ToString().Trim();
    //                                            RizMetreUsers.FBId = intFBId;
    //                                            RizMetreUsers.OperationsOfHamlId = 1;
    //                                            RizMetreUsers.Type = "2";
    //                                            RizMetreUsers.ForItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
    //                                            RizMetreUsers.UseItem = "";
    //                                            RizMetreUsers.LevelNumber = LevelNumber;
    //                                            RizMetreUsers.InsertDateTime = Now;

    //                                            decimal dMeghdarJoz = 0;
    //                                            if (RizMetreUsers.Tedad == null && RizMetreUsers.Tool == null && RizMetreUsers.Arz == null && RizMetreUsers.Ertefa == null && RizMetreUsers.Vazn == null)
    //                                                dMeghdarJoz = 0;
    //                                            else
    //                                                dMeghdarJoz += (RizMetreUsers.Tedad == null ? 1 : RizMetreUsers.Tedad.Value) * (RizMetreUsers.Tool == null ? 1 : RizMetreUsers.Tool.Value) *
    //                                                (RizMetreUsers.Arz == null ? 1 : RizMetreUsers.Arz.Value) * (RizMetreUsers.Ertefa == null ? 1 : RizMetreUsers.Ertefa.Value)
    //                                                * (RizMetreUsers.Vazn == null ? 1 : RizMetreUsers.Vazn.Value);

    //                                            RizMetreUsers.MeghdarJoz = dMeghdarJoz;


    //                                            _context.RizMetreUserses.Add(RizMetreUsers);
    //                                            _context.SaveChanges();
    //                                            //RizMetreUsers.Save();
    //                                        }
    //                                    }
    //                                }
    //                                break;
    //                            }
    //                        case "6":
    //                            {
    //                                string strCondition = Dr[idr]["Condition"].ToString().Trim();
    //                                StringToFormula StringToFormula = new StringToFormula();
    //                                string strAddedItems = Dr[idr]["AddedItems"].ToString().Trim();
    //                                var varFBUser = _context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems).ToList();
    //                                DtFBUser = clsConvert.ToDataTable(varFBUser);

    //                                //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["ID"].ToString() + " and Shomareh='" + Dr[idr]["AddedItems"].ToString().Trim() + "'");
    //                                Guid intFBId = new Guid();
    //                                if (DtFBUser.Rows.Count == 0)
    //                                {
    //                                    clsFB FB = new clsFB();
    //                                    FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
    //                                    FB.Shomareh = Dr[0]["AddedItems"].ToString().Trim();
    //                                    FB.BahayeVahedZarib = 0;
    //                                    _context.FBs.Add(FB);
    //                                    _context.SaveChanges();
    //                                    intFBId = FB.ID;
    //                                    //intFBId = Operation_ItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["ID"].ToString()), Dr[idr]["AddedItems"].ToString().Trim(), 0);
    //                                }
    //                                else
    //                                    intFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

    //                                Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
    //                                var varRizMetreUsers = (from RizMetreUsers in _context.RizMetreUserses
    //                                                        join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
    //                                                        where RizMetreUsers.LevelNumber == LevelNumber
    //                                                        select new
    //                                                        {
    //                                                            RizMetreUsers.ID,
    //                                                            RizMetreUsers.Shomareh,
    //                                                            RizMetreUsers.Tedad,
    //                                                            RizMetreUsers.Tool,
    //                                                            RizMetreUsers.Arz,
    //                                                            RizMetreUsers.Ertefa,
    //                                                            RizMetreUsers.Vazn,
    //                                                            RizMetreUsers.Sharh,
    //                                                            RizMetreUsers.Des,
    //                                                            RizMetreUsers.FBId,
    //                                                            RizMetreUsers.OperationsOfHamlId,
    //                                                            RizMetreUsers.ForItem,
    //                                                            RizMetreUsers.Type,
    //                                                            RizMetreUsers.UseItem,
    //                                                            FB.BarAvordId
    //                                                        }).Where(x => x.FBId == guFBId && x.Type == "1").OrderBy(x => x.Shomareh).ToList();
    //                                DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);

    //                                string strShomareh1 = DtFB.Rows[0]["Shomareh"].ToString().Trim();
    //                                var varRizMetreUsersCurrent = (from RizMetreUsers in _context.RizMetreUserses
    //                                                               join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
    //                                                               where RizMetreUsers.LevelNumber == LevelNumber
    //                                                               select new
    //                                                               {
    //                                                                   ID = RizMetreUsers.ID,
    //                                                                   Shomareh = RizMetreUsers.Shomareh,
    //                                                                   Tedad = RizMetreUsers.Tedad,
    //                                                                   Tool = RizMetreUsers.Tool,
    //                                                                   Arz = RizMetreUsers.Arz,
    //                                                                   Ertefa = RizMetreUsers.Ertefa,
    //                                                                   Vazn = RizMetreUsers.Vazn,
    //                                                                   Des = RizMetreUsers.Des,
    //                                                                   FBId = RizMetreUsers.FBId,
    //                                                                   OperationsOfHamlId = RizMetreUsers.OperationsOfHamlId,
    //                                                                   ForItem = RizMetreUsers.ForItem,
    //                                                                   Type = RizMetreUsers.Type,
    //                                                                   UseItem = RizMetreUsers.UseItem,
    //                                                                   BaravordUserId = FB.BarAvordId
    //                                                               }).Where(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
    //                                DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsersCurrent);

    //                                //DataTable DtRizMetreUsers = clsRizMetreUsers.RizMetreUsersesListWithParameter("FBId=" + DtFB.Rows[0]["ID"].ToString() + " and Type=1");
    //                                // DataTable DtRizMetreUsersCurrent = clsRizMetreUsers.RizMetreUsersesListWithParameter("FBId=" + FBId + " and ForItem='" + DtFB.Rows[0]["Shomareh"].ToString().Trim() + "' and Type=2");
    //                                for (int i = 0; i < DtRizMetreUsers.Rows.Count; i++)
    //                                {
    //                                    string strConditionOp = strCondition.Replace("x", DtRizMetreUsers.Rows[i]["Ertefa"].ToString().Trim());
    //                                    bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
    //                                    if (blnCheck)
    //                                    {
    //                                        DataRow[] DrRizMetreUsersCurrent = DtRizMetreUsersCurrent.Select("shomareh=" + int.Parse(DtRizMetreUsers.Rows[i]["Shomareh"].ToString()));
    //                                        if (DrRizMetreUsersCurrent.Length == 0)
    //                                        {
    //                                            clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
    //                                            RizMetreUsers.Shomareh = long.Parse(DtRizMetreUsers.Rows[i]["Shomareh"].ToString());
    //                                            ShomareNew++;
    //                                            RizMetreUsers.ShomarehNew = ShomareNew.ToString();

    //                                            RizMetreUsers.Sharh = DtRizMetreUsers.Rows[i]["Sharh"].ToString().Trim();
    //                                            RizMetreUsers.Tedad = DtRizMetreUsers.Rows[i]["Tedad"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Tedad"].ToString());
    //                                            RizMetreUsers.Tool = DtRizMetreUsers.Rows[i]["Tool"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Tool"].ToString());
    //                                            RizMetreUsers.Arz = DtRizMetreUsers.Rows[i]["Arz"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Arz"].ToString());
    //                                            RizMetreUsers.Ertefa = DtRizMetreUsers.Rows[i]["Ertefa"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Ertefa"].ToString());
    //                                            RizMetreUsers.Vazn = DtRizMetreUsers.Rows[i]["Vazn"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Vazn"].ToString());
    //                                            RizMetreUsers.Des = Dr[idr]["Des"].ToString().Trim();// Dr[idr]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + DtFB.Rows[0]["Shomareh"].ToString().Trim();
    //                                            RizMetreUsers.FBId = FBId;
    //                                            RizMetreUsers.OperationsOfHamlId = 1;
    //                                            RizMetreUsers.Type = "2";
    //                                            RizMetreUsers.ForItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
    //                                            RizMetreUsers.UseItem = "";
    //                                            RizMetreUsers.LevelNumber = LevelNumber;
    //                                            RizMetreUsers.InsertDateTime = Now;


    //                                            decimal dMeghdarJoz = 0;
    //                                            if (RizMetreUsers.Tedad == null && RizMetreUsers.Tool == null && RizMetreUsers.Arz == null && RizMetreUsers.Ertefa == null && RizMetreUsers.Vazn == null)
    //                                                dMeghdarJoz = 0;
    //                                            else
    //                                                dMeghdarJoz += (RizMetreUsers.Tedad == null ? 1 : RizMetreUsers.Tedad.Value) * (RizMetreUsers.Tool == null ? 1 : RizMetreUsers.Tool.Value) *
    //                                                (RizMetreUsers.Arz == null ? 1 : RizMetreUsers.Arz.Value) * (RizMetreUsers.Ertefa == null ? 1 : RizMetreUsers.Ertefa.Value) * (RizMetreUsers.Vazn == null ? 1 : RizMetreUsers.Vazn.Value);
    //                                            RizMetreUsers.MeghdarJoz = dMeghdarJoz;



    //                                            _context.RizMetreUserses.Add(RizMetreUsers);
    //                                            _context.SaveChanges();
    //                                            //RizMetreUsers.Save();
    //                                        }
    //                                    }
    //                                }
    //                                break;
    //                            }
    //                        case "8":
    //                            {
    //                                string strCondition = Dr[idr]["Condition"].ToString().Trim();
    //                                StringToFormula StringToFormula = new StringToFormula();
    //                                string strAddedItems = Dr[idr]["AddedItems"].ToString().Trim();
    //                                var varFBUser = _context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems).ToList();
    //                                DtFBUser = clsConvert.ToDataTable(varFBUser);

    //                                //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["ID"].ToString() + " and Shomareh='" + Dr[idr]["AddedItems"].ToString().Trim() + "'");
    //                                Guid intFBId = new Guid();
    //                                if (DtFBUser.Rows.Count == 0)
    //                                {
    //                                    clsFB FB = new clsFB();
    //                                    FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
    //                                    FB.Shomareh = Dr[idr]["AddedItems"].ToString().Trim();
    //                                    FB.BahayeVahedZarib = 0;
    //                                    _context.FBs.Add(FB);
    //                                    _context.SaveChanges();
    //                                    intFBId = FB.ID;
    //                                    //intFBId = Operation_ItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["ID"].ToString()), Dr[idr]["AddedItems"].ToString().Trim(), 0);
    //                                }
    //                                else
    //                                    intFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());




    //                                Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
    //                                var varRizMetreUsers = (from RizMetreUsers in _context.RizMetreUserses
    //                                                        join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
    //                                                        where RizMetreUsers.LevelNumber == LevelNumber
    //                                                        select new
    //                                                        {
    //                                                            ID = RizMetreUsers.ID,
    //                                                            Shomareh = RizMetreUsers.Shomareh,
    //                                                            Tedad = RizMetreUsers.Tedad,
    //                                                            Tool = RizMetreUsers.Tool,
    //                                                            Arz = RizMetreUsers.Arz,
    //                                                            Ertefa = RizMetreUsers.Ertefa,
    //                                                            Vazn = RizMetreUsers.Vazn,
    //                                                            Des = RizMetreUsers.Des,
    //                                                            FBId = RizMetreUsers.FBId,
    //                                                            OperationsOfHamlId = RizMetreUsers.OperationsOfHamlId,
    //                                                            ForItem = RizMetreUsers.ForItem,
    //                                                            Type = RizMetreUsers.Type,
    //                                                            UseItem = RizMetreUsers.UseItem,
    //                                                            BaravordUserId = FB.BarAvordId
    //                                                        }).Where(x => x.FBId == guFBId && x.Type == "1").OrderBy(x => x.Shomareh).ToList();
    //                                DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);
    //                                string strShomareh1 = DtFB.Rows[0]["Shomareh"].ToString().Trim();
    //                                var varRizMetreUsersCurrent = (from RizMetreUsers in _context.RizMetreUserses
    //                                                               join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
    //                                                               where RizMetreUsers.LevelNumber == LevelNumber
    //                                                               select new
    //                                                               {
    //                                                                   ID = RizMetreUsers.ID,
    //                                                                   Shomareh = RizMetreUsers.Shomareh,
    //                                                                   Tedad = RizMetreUsers.Tedad,
    //                                                                   Tool = RizMetreUsers.Tool,
    //                                                                   Arz = RizMetreUsers.Arz,
    //                                                                   Ertefa = RizMetreUsers.Ertefa,
    //                                                                   Vazn = RizMetreUsers.Vazn,
    //                                                                   Des = RizMetreUsers.Des,
    //                                                                   FBId = RizMetreUsers.FBId,
    //                                                                   OperationsOfHamlId = RizMetreUsers.OperationsOfHamlId,
    //                                                                   ForItem = RizMetreUsers.ForItem,
    //                                                                   Type = RizMetreUsers.Type,
    //                                                                   UseItem = RizMetreUsers.UseItem,
    //                                                                   BaravordUserId = FB.BarAvordId
    //                                                               }).Where(x => x.FBId == FBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
    //                                DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsersCurrent);

    //                                //DataTable DtRizMetreUsers = clsRizMetreUsers.RizMetreUsersesListWithParameter("FBId=" + DtFB.Rows[0]["ID"].ToString() + " and Type=1");
    //                                //DataTable DtRizMetreUsersCurrent = clsRizMetreUsers.RizMetreUsersesListWithParameter("FBId=" + FBId + " and ForItem='" + DtFB.Rows[0]["Shomareh"].ToString().Trim() + "' and Type=2");
    //                                for (int i = 0; i < DtRizMetreUsers.Rows.Count; i++)
    //                                {
    //                                    string strConditionOp = strCondition.Replace("x", DtRizMetreUsers.Rows[i]["Arz"].ToString().Trim());
    //                                    bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
    //                                    if (blnCheck)
    //                                    {
    //                                        DataRow[] DrRizMetreUsersCurrent = DtRizMetreUsersCurrent.Select("shomareh=" + int.Parse(DtRizMetreUsers.Rows[i]["Shomareh"].ToString()));
    //                                        if (DrRizMetreUsersCurrent.Length == 0)
    //                                        {
    //                                            decimal ArzEzafi = 1;
    //                                            string strFinalWorking = DtItemsAddingToFB.Rows[i]["FinalWorking"].ToString();
    //                                            if (strFinalWorking != "")
    //                                            {
    //                                                strFinalWorking = strFinalWorking.Replace("x", DtRizMetreUsers.Rows[i]["Arz"].ToString().Trim());
    //                                                ArzEzafi = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());
    //                                            }

    //                                            clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
    //                                            RizMetreUsers.Shomareh = long.Parse(DtRizMetreUsers.Rows[i]["Shomareh"].ToString());
    //                                            ShomareNew++;
    //                                            RizMetreUsers.ShomarehNew = ShomareNew.ToString();

    //                                            RizMetreUsers.Sharh = DtRizMetreUsers.Rows[i]["Sharh"].ToString().Trim();
    //                                            RizMetreUsers.Tedad = (DtRizMetreUsers.Rows[i]["Tedad"].ToString().Trim() == "" ? 1 : decimal.Parse(DtRizMetreUsers.Rows[i]["Tedad"].ToString())) * ArzEzafi;
    //                                            RizMetreUsers.Tool = DtRizMetreUsers.Rows[i]["Tool"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Tool"].ToString());
    //                                            RizMetreUsers.Arz = DtRizMetreUsers.Rows[i]["Arz"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Arz"].ToString());
    //                                            RizMetreUsers.Ertefa = DtRizMetreUsers.Rows[i]["Ertefa"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Ertefa"].ToString());
    //                                            RizMetreUsers.Vazn = DtRizMetreUsers.Rows[i]["Vazn"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Vazn"].ToString());
    //                                            RizMetreUsers.Des = Dr[idr]["Des"].ToString().Trim();// Dr[idr]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + DtFB.Rows[0]["Shomareh"].ToString().Trim();
    //                                            RizMetreUsers.FBId = FBId;
    //                                            RizMetreUsers.OperationsOfHamlId = 1;
    //                                            RizMetreUsers.Type = "2";
    //                                            RizMetreUsers.ForItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
    //                                            RizMetreUsers.UseItem = "";
    //                                            RizMetreUsers.LevelNumber = LevelNumber;
    //                                            RizMetreUsers.InsertDateTime = Now;


    //                                            decimal dMeghdarJoz = 0;
    //                                            if (RizMetreUsers.Tedad == null && RizMetreUsers.Tool == null && RizMetreUsers.Arz == null && RizMetreUsers.Ertefa == null && RizMetreUsers.Vazn == null)
    //                                                dMeghdarJoz = 0;
    //                                            else
    //                                                dMeghdarJoz += (RizMetreUsers.Tedad == null ? 1 : RizMetreUsers.Tedad.Value) * (RizMetreUsers.Tool == null ? 1 : RizMetreUsers.Tool.Value) *
    //                                                (RizMetreUsers.Arz == null ? 1 : RizMetreUsers.Arz.Value) * (RizMetreUsers.Ertefa == null ? 1 : RizMetreUsers.Ertefa.Value)
    //                                                * (RizMetreUsers.Vazn == null ? 1 : RizMetreUsers.Vazn.Value);

    //                                            RizMetreUsers.MeghdarJoz = dMeghdarJoz;


    //                                            _context.RizMetreUserses.Add(RizMetreUsers);
    //                                            _context.SaveChanges();
    //                                            //RizMetreUsers.Save();
    //                                        }
    //                                    }
    //                                }
    //                                break;
    //                            }
    //                        default:
    //                            break;
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    /////////////////
    //    /////////////
    //    /////////////
    //    /////////////
    //    string strShomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
    //    var varOpItems = _context.Operation_ItemsFBs.Where(x => x.ItemsFBShomareh == strShomareh && x.Year == Year && x.OperationId == request.Operation).ToList();
    //    DataTable DtOpItems = clsConvert.ToDataTable(varOpItems);

    //    string strItemsFBShomareh = DtOpItems.Rows[0]["ItemsFBShomareh"].ToString().Trim();
    //    var varItemsFields1 = (
    //                        from ItemsFields in _context.ItemsFieldses
    //                        join Operation_ItemsFB in _context.Operation_ItemsFBs on
    //                        ItemsFields.ItemShomareh equals Operation_ItemsFB.ItemsFBShomareh
    //                        where Operation_ItemsFB.Year == Year && Operation_ItemsFB.OperationId == request.Operation
    //                        select new
    //                        {
    //                            ID = ItemsFields.Id,
    //                            ItemShomareh = ItemsFields.ItemShomareh,
    //                            FieldType = ItemsFields.FieldType,
    //                            Vahed = ItemsFields.Vahed,
    //                            IsEnteringValue = ItemsFields.IsEnteringValue,
    //                            DefaultValue = ItemsFields.DefaultValue,
    //                            NoeFB = ItemsFields.NoeFB,
    //                            OperationId = Operation_ItemsFB.OperationId,
    //                            Year = Operation_ItemsFB.Year
    //                        }).Where(x => x.ItemShomareh == strItemsFBShomareh && x.NoeFB == NoeFB).Distinct().OrderBy(x => x.FieldType).ToList();
    //    DtItemsFields = clsConvert.ToDataTable(varItemsFields1);

    //    //DataTable DtOpItems = clsOperation_ItemsFB.ListWithParameter("ItemsFBShomareh=" + Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim());
    //    //DtItemsFields = clsItemsFields.ItemsFieldsListWithParameter("ItemShomareh='" + DtOpItems.Rows[0]["ItemsFBShomareh"].ToString().Trim() + "' and NoeFB=234");
    //    string lstItemsFields = "";
    //    for (int i = 0; i < DtItemsFields.Rows.Count; i++)
    //    {
    //        lstItemsFields += DtItemsFields.Rows[i]["IsEnteringValue"].ToString().Trim() + ",";
    //    }

    //    if (FBId != Guid.NewGuid())
    //    {
    //        Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
    //        var varRizMetreUsers = (from RizMetreUsers in _context.RizMetreUserses
    //                                join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
    //                                where RizMetreUsers.LevelNumber == LevelNumber
    //                                select new
    //                                {
    //                                    RizMetreUsers.ID,
    //                                    RizMetreUsers.Shomareh,
    //                                    RizMetreUsers.Tedad,
    //                                    RizMetreUsers.Tool,
    //                                    RizMetreUsers.Arz,
    //                                    RizMetreUsers.Ertefa,
    //                                    RizMetreUsers.Vazn,
    //                                    RizMetreUsers.Sharh,
    //                                    RizMetreUsers.Des,
    //                                    RizMetreUsers.FBId,
    //                                    RizMetreUsers.OperationsOfHamlId,
    //                                    RizMetreUsers.ForItem,
    //                                    RizMetreUsers.Type,
    //                                    RizMetreUsers.UseItem,
    //                                    FB.BarAvordId
    //                                }).Where(x => x.FBId == FBId && x.ForItem == ItemsFBShomareh).OrderBy(x => x.Shomareh).ToList();
    //        DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);
    //        //DataTable DtRizMetreUsers = clsRizMetreUsers.RizMetreUsersesListWithParameter("FBId=" + FBId);
    //        string str = "";
    //        strShomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
    //        var varItemsHasCondition = (from tblItemsHasCondition in _context.ItemsHasConditions
    //                                    join tblItemsHasCondition_ConditionContext in _context.ItemsHasCondition_ConditionContexts
    //                                    on tblItemsHasCondition.Id equals tblItemsHasCondition_ConditionContext.ItemsHasConditionId
    //                                    join tblConditionContext in _context.ConditionContexts on
    //                                    tblItemsHasCondition_ConditionContext.ConditionContextId equals tblConditionContext.Id
    //                                    join tblConditionGroup in _context.ConditionGroups on
    //                                    tblConditionContext.ConditionGroupId equals tblConditionGroup.Id
    //                                    select new
    //                                    {
    //                                        tblItemsHasCondition_ConditionContext.Id,
    //                                        ItemsHasConditionId = tblItemsHasCondition.Id,
    //                                        ItemFBShomareh = tblItemsHasCondition.ItemFBShomareh,
    //                                        tblItemsHasCondition_ConditionContext.HasEnteringValue,
    //                                        tblConditionContext.Context,
    //                                        tblItemsHasCondition_ConditionContext.Des,
    //                                        tblConditionGroup.ConditionGroupName,
    //                                        ConditionGroupId = tblConditionGroup.Id,
    //                                        tblItemsHasCondition_ConditionContext.DefaultValue,
    //                                        tblItemsHasCondition_ConditionContext.IsShow,
    //                                        tblItemsHasCondition_ConditionContext.ParentId,
    //                                        tblItemsHasCondition_ConditionContext.MoveToRel,
    //                                        tblItemsHasCondition_ConditionContext.ViewCheckAllRecords,
    //                                    }).Where(x => x.ItemFBShomareh == strItemShomareh).ToList();
    //        DataTable DtItemsHasCondition = clsConvert.ToDataTable(varItemsHasCondition);

    //        // str += "<script type=\"text/javascript\">AddRizMetreUsersN('" + DtOpItems.Rows[0]["OperationId"].ToString().Trim() + "','" + FBId + "','" + lstItemsFields + "'," + LevelNumber + ")</script>";
    //        str += "<div class=\"row styleHeaderTable\"><div class=\"col-md-1 spanStyleMitraSmall\">شماره</div><div class=\"col-md-2 spanStyleMitraSmall\">شرح</div>";
    //        str += "<div class=\"col-md-1 spanStyleMitraSmall\"><div style=\"padding-bottom:3px;border-bottom:1px solid #84d4e6\">تعداد</div><div class=\"VahedStyle\">" + DtItemsFields.Rows[0]["Vahed"].ToString().Trim() + " </div></div><div class=\"col-md-1 spanStyleMitraSmall\"><div style=\"padding-bottom:3px;border-bottom:1px solid #84d4e6\">طول</div><div class=\"VahedStyle\">" + DtItemsFields.Rows[1]["Vahed"].ToString().Trim() + "</div></div>";
    //        str += "<div class=\"col-md-1 spanStyleMitraSmall\"><div style=\"padding-bottom:3px;border-bottom:1px solid #84d4e6\">عرض</div><div class=\"VahedStyle\">" + DtItemsFields.Rows[2]["Vahed"].ToString().Trim() + "</div></div><div class=\"col-md-1 spanStyleMitraSmall\"><div style=\"padding-bottom:3px;border-bottom:1px solid #84d4e6\">ارتفاع</div><div class=\"VahedStyle\">" + DtItemsFields.Rows[3]["Vahed"].ToString().Trim() + "</div></div>";
    //        str += "<div class=\"col-md-1 spanStyleMitraSmall\"><div style=\"padding-bottom:3px;border-bottom: 1px solid #84d4e6;\">وزن</div><div class=\"VahedStyle\">" + DtItemsFields.Rows[4]["Vahed"].ToString().Trim() + "</div></div><div class=\"col-md-1 spanStyleMitraSmall\"><div style=\"padding-bottom:3px;border-bottom:1px solid #84d4e6\"><span>مقدار جزء</span></div><div class=\"VahedStyle\">" + DtItemsFields.Rows[5]["Vahed"].ToString().Trim() + "</div></div><div class=\"col-md-2 spanStyleMitraSmall\">توضیحات</div>";
    //        str += "<div class=\"col-md-1 spanStyleMitraSmall\"><span>ویرایش/حذف</span></div></div>";

    //        if (DtRizMetreUsers.Rows.Count != 0)
    //        {
    //            decimal? dSumAll = 0;
    //            str += "<div class=\"row styleFieldTable\">";
    //            str += "<div class=\"col-md-12 RMCollectStyle\">";

    //            for (int i = 0; i < DtRizMetreUsers.Rows.Count; i++)
    //            {
    //                decimal? dMeghdarJoz = null;
    //                decimal? dTedad = DtRizMetreUsers.Rows[i]["Tedad"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Tedad"].ToString());
    //                decimal? dTool = DtRizMetreUsers.Rows[i]["Tool"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Tool"].ToString());
    //                decimal? dArz = DtRizMetreUsers.Rows[i]["Arz"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Arz"].ToString());
    //                decimal? dErtefa = DtRizMetreUsers.Rows[i]["Ertefa"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Ertefa"].ToString());
    //                decimal? dVazn = DtRizMetreUsers.Rows[i]["Vazn"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[i]["Vazn"].ToString());

    //                if (dTedad == null && dTool == null && dArz == null && dErtefa == null && dVazn == null)
    //                    dMeghdarJoz = 0;
    //                else
    //                    dMeghdarJoz += (dTedad == null ? 1 : dTedad) * (dTool == null ? 1 : dTool) *
    //                    (dArz == null ? 1 : dArz) * (dErtefa == null ? 1 : dErtefa) * (dVazn == null ? 1 : dVazn);
    //                dSumAll += dMeghdarJoz == null ? 0 : dMeghdarJoz;

    //                string strTedad = DtRizMetreUsers.Rows[i]["Tedad"].ToString() == "" ? "" : decimal.Parse(DtRizMetreUsers.Rows[i]["Tedad"].ToString()).ToString("G29");
    //                string strTool = DtRizMetreUsers.Rows[i]["Tool"].ToString() == "" ? "" : decimal.Parse(DtRizMetreUsers.Rows[i]["Tool"].ToString()).ToString("G29");
    //                string strArz = DtRizMetreUsers.Rows[i]["Arz"].ToString() == "" ? "" : decimal.Parse(DtRizMetreUsers.Rows[i]["Arz"].ToString()).ToString("G29");
    //                string strErtefa = DtRizMetreUsers.Rows[i]["Ertefa"].ToString() == "" ? "" : decimal.Parse(DtRizMetreUsers.Rows[i]["Ertefa"].ToString()).ToString("G29");
    //                string strVazn = DtRizMetreUsers.Rows[i]["Vazn"].ToString() == "" ? "" : decimal.Parse(DtRizMetreUsers.Rows[i]["Vazn"].ToString()).ToString("G29");
    //                str += "<div class=\"row styleRowTable\" onclick=\"RizMetreSelectClick('" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "')\"><div class=\"col-md-1\">" + DtRizMetreUsers.Rows[i]["Shomareh"].ToString() + "</div>";
    //                str += "<div class=\"col-md-2\"><input type=\"text\" class=\"form-control TextEdit spanStyleMitraSmall\" id=\"txtSharh" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "\" value=\"" + DtRizMetreUsers.Rows[i]["Sharh"].ToString() + "\"/></div>";
    //                str += "<div class=\"col-md-1\"><input type=\"text\" class=\"form-control TextEdit spanStyleMitraSmall\" id=\"txtTedad" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "\" value=\"" + strTedad + "\"/></div>";
    //                str += " <div class=\"col-md-1\"><input type=\"text\" class=\"form-control TextEdit spanStyleMitraSmall\" id=\"txtTool" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "\" value=\"" + strTool + "\"/></div>";
    //                str += "<div class=\"col-md-1\"><input type=\"text\" class=\"form-control TextEdit spanStyleMitraSmall\" id=\"txtArz" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "\" value=\"" + strArz + "\"/></div>";
    //                str += "<div class=\"col-md-1\"><input type=\"text\" class=\"form-control TextEdit spanStyleMitraSmall\" id=\"txtErtefa" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "\" value=\"" + strErtefa + "\"/></div>";
    //                str += "<div class=\"col-md-1\"><input type=\"text\" class=\"form-control TextEdit spanStyleMitraSmall\" id=\"txtVazn" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "\" value=\"" + strVazn + "\"/></div>";
    //                str += "<div class=\"col-md-1 RMMJozStyle\">" + (dMeghdarJoz == 0 ? "" : Math.Round(dMeghdarJoz == null ? 0 : dMeghdarJoz.Value, 2).ToString("G29")) + "</div>";
    //                str += "<div class=\"col-md-2\"><input type=\"text\" class=\"form-control spanStyleMitraSmall\" id=\"txtDes" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "\" value=\"" + DtRizMetreUsers.Rows[i]["Des"].ToString() + "\"/></div>";
    //                str += "<div class=\"col-md-1\"><i class=\"fa fa-edit EditRMUStyle displayNone\" id=\"iEdit" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "\" onclick=\"EditNRMUClick('" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "','" + DtOpItems.Rows[0]["OperationId"].ToString().Trim() + "','" + FBId + "','" + lstItemsFields + "')\"></i><i id=\"iUpdate" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "\" class=\"fa fa-save SaveRMUStyle\" onclick=\"UpdateNRMUClick('" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "','" + DtOpItems.Rows[0]["OperationId"].ToString() + "','" + FBId + "')\"></i><i class=\"fa fa-trash DelRMUStyle\"  onclick=\"DeleteRMUNClick('" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "','" + FBId + "')\"></i></div></div>";
    //            }
    //            str += "</div>";
    //            str += "</div>";
    //        }
    //        //else
    //        //{
    //        //    str += "<div class=\"row NoRizMetre\"><div class=\"col-md-12\" style=\"padding:15px;\"><span class=\"spanStyleMitraMedium\">هیچ ریزمتره ای برای این آیتم درج نگردیده است، لطفا از دکمه <span class=\"RizMetreUsersAddStyle spanStyleMitraSmall\" onclick=\"AddRizMetreUsersN('" + DtOpItems.Rows[0]["OperationId"].ToString().Trim() + "','" + FBId + "')\">افزودن ریزمتره</span> استفاده نمایید</span></div></div>";
    //        //}
    //        return new JsonResult("OK_" + FBId.ToString() + "_" + str + "_" + blnHasItemForGetValue.ToString());

    //        //return "OK_" + FBId.ToString() + "" + str + "" + blnHasItemForGetValue.ToString();
    //    }
    //    return new JsonResult("");
    //}

    List<long> GetAllDescendantIds(List<GetAllDescendantIdsDto> allOps, long parentId)
    {
        var result = new List<long> { parentId };
        var children = allOps.Where(x => x.ParentId == parentId).ToList();
        foreach (var child in children)
        {
            result.AddRange(GetAllDescendantIds(allOps, child.ID));
        }
        return result;
    }


    public JsonResult CheckRutin([FromBody] CheckRutinInputDto request)
    {
        long AddedOperationId = request.AddedOperationId;
        string ItemsFBShomareh = request.ItemsFBShomareh;
        Guid BarAvordUserId = request.BarAvordUserId;
        int Type = request.Type;
        int Year = request.Year;
        NoeFehrestBaha NoeFB = request.NoeFB;
        int LevelNumber = request.LevelNumber;

        if (Type == 5)
        {
            var ShomarehItems = (from op_Item in _context.Operation_ItemsFBs
                                 join FB in _context.FehrestBahas
                                 on op_Item.ItemsFBShomareh equals FB.Shomareh
                                 select new
                                 {
                                     ItemsFBShomareh = op_Item.ItemsFBShomareh,
                                     OperationId = op_Item.OperationId,
                                     Sharh = FB.Sharh,
                                     Year = FB.Sal,
                                     NoeFB = FB.NoeFB
                                 }).Where(x => x.Year == Year && x.NoeFB == NoeFB);

            var operation = (from op in _context.Operations
                             join Sh_Items in ShomarehItems
                             on op.Id equals Sh_Items.OperationId
                             into joinTable
                             from jT in joinTable.DefaultIfEmpty()
                             where op.Year == request.Year
                             select new GetAllDescendantIdsDto
                             {
                                 ID = op.Id,
                                 Order = op.Order,
                                 ParentId = op.ParentId,
                                 OperationName = op.OperationName,
                                 FunctionCall = op.FunctionCall == null ? "" : op.FunctionCall,
                                 Sharh = jT.Sharh,
                                 ItemsFBShomareh = jT.ItemsFBShomareh == null ? "" : jT.ItemsFBShomareh,
                                 Year = op.Year
                             })
                             .OrderBy(x => x.Order)
                             .ThenBy(x => x.ID)
                             .ToList();



            long targetId = 1404004; // آیدی مورد نظر شما
            long targetId2 = 1404007; // آیدی مورد نظر شما
            var ids1 = GetAllDescendantIds(operation, targetId);
            var ids2 = GetAllDescendantIds(operation, targetId2);

            var mergedIds = ids1.Union(ids2).ToList();

            var filteredOperations = operation
                .Where(x => mergedIds.Contains(x.ID))
                .OrderBy(x => x.Order)
                .ThenBy(x => x.ID)
                .ToList();


            return new JsonResult(filteredOperations);

        }

        string strOp = "";
        var varoperation = (from op in _context.Operations
                            join Operation_ItemsFB in _context.Operation_ItemsFBs
                            on op.Id equals Operation_ItemsFB.OperationId
                            into joinTable
                            from jT in joinTable.DefaultIfEmpty()
                            where op.Year == Year
                            select new
                            {
                                ID = op.Id,
                                ParentId = op.ParentId,
                                OperationName = op.OperationName,
                                FunctionCall = op.FunctionCall == null ? "" : op.FunctionCall,
                                ItemsFBShomareh = jT.ItemsFBShomareh == null ? "" : jT.ItemsFBShomareh
                            }).Where(x => x.ParentId == AddedOperationId).ToList();
        DataTable DtOperation = clsConvert.ToDataTable(varoperation);
        if (DtOperation.Rows.Count == 0)
        {
            var varoperation1 = (from op in _context.Operations
                                 join Operation_ItemsFB in _context.Operation_ItemsFBs
                                 on op.Id equals Operation_ItemsFB.OperationId
                                 into joinTable
                                 from jT in joinTable.DefaultIfEmpty()
                                 where op.Year == Year
                                 select new
                                 {
                                     ID = op.Id,
                                     ParentId = op.ParentId,
                                     OperationName = op.OperationName,
                                     FunctionCall = op.FunctionCall == null ? "" : op.FunctionCall,
                                     ItemsFBShomareh = jT.ItemsFBShomareh == null ? "" : jT.ItemsFBShomareh
                                 }).Where(x => x.ID == AddedOperationId).ToList();
            DtOperation = clsConvert.ToDataTable(varoperation1);
        }

        var varItemsFBShomarehValueShomareh = _context.ItemsFBShomarehValueShomarehs.Where(x => x.FBShomareh == ItemsFBShomareh.Trim() && x.BarAvordId == BarAvordUserId && x.Type == Type).ToList();
        DataTable DtItemsFBShomarehValueShomareh = clsConvert.ToDataTable(varItemsFBShomarehValueShomareh);

        var varOperationItemsFB = _context.Operation_ItemsFBs.Where(x => x.Year == Year).ToList();
        DataTable DtOperationItemsFB = clsConvert.ToDataTable(varOperationItemsFB);

        for (int i = 0; i < DtOperation.Rows.Count; i++)
        {
            string strItemsFBShomareh = DtOperation.Rows[i]["ItemsFBShomareh"].ToString().Trim();
            var varItemsFields = (from ItemsFields in _context.ItemsFieldses
                                  join Operation_ItemsFB in _context.Operation_ItemsFBs
                                  on ItemsFields.ItemShomareh equals Operation_ItemsFB.ItemsFBShomareh
                                  where Operation_ItemsFB.Year == Year
                                  select new
                                  {
                                      ID = ItemsFields.Id,
                                      ItemShomareh = ItemsFields.ItemShomareh,
                                      FieldType = ItemsFields.FieldType,
                                      Vahed = ItemsFields.Vahed,
                                      IsEnteringValue = ItemsFields.IsEnteringValue,
                                      DefaultValue = ItemsFields.DefaultValue,
                                      NoeFB = ItemsFields.NoeFB,
                                      OperationId = Operation_ItemsFB.OperationId
                                  }).Where(x => x.ItemShomareh == strItemsFBShomareh && x.NoeFB == NoeFB).OrderBy(x => x.FieldType).ToList();
            DataTable DtItemsFields = clsConvert.ToDataTable(varItemsFields);
            string lstItemsFields = "";
            for (int ii = 0; ii < DtItemsFields.Rows.Count; ii++)
            {
                lstItemsFields += DtItemsFields.Rows[ii]["IsEnteringValue"].ToString().Trim() + ",";
            }

            DataRow[] Dr = DtOperationItemsFB.Select("OperationId=" + DtOperation.Rows[i]["ID"].ToString());
            if (DtItemsFBShomarehValueShomareh.Rows.Count != 0)
            {
                if (Dr[0]["ItemsFBShomareh"].ToString().Trim() == DtItemsFBShomarehValueShomareh.Rows[0]["GetValuesShomareh"].ToString().Trim())
                {
                    string strItemsFBShomareh1 = Dr[0]["ItemsFBShomareh"].ToString().Trim();
                    var varItemsKholasehFosul = (from FehrestBahas in _context.FehrestBahas
                                                 select new
                                                 {
                                                     FehrestBahaShomareh = FehrestBahas.Shomareh,
                                                     FehrestBahas.Sharh,
                                                     FehrestBahas.Vahed,
                                                     FehrestBahas.BahayeVahed,
                                                     FehrestBahas.Sal,
                                                     FehrestBahas.Id,
                                                     FehrestBahas.NoeFB,
                                                     Type = "",
                                                     TypeID = 0,
                                                     ProjectID = 0,
                                                     Meghdar = 0
                                                 }).Where(x => x.Sal == Year && x.NoeFB == NoeFB && x.FehrestBahaShomareh == strItemsFBShomareh1).ToList();
                    DataTable DtItemsKholasehFosul = clsConvert.ToDataTable(varItemsKholasehFosul);
                    strOp += "<div class=\"col-md-12\"><input id=\"na" + DtOperation.Rows[i]["ID"].ToString().Trim() + "\" name=\"Group\" checked=\"checked\" type=\"radio\" onclick=\"NOperationClick('" + DtOperation.Rows[i]["ID"].ToString().Trim() + "'," + LevelNumber + ")\"/><span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"spanAddedItemsClick('" + DtOperation.Rows[i]["ID"].ToString().Trim() + "')\">" + Dr[0]["ItemsFBShomareh"].ToString().Trim() + "-" + DtItemsKholasehFosul.Rows[0]["Sharh"].ToString().Trim() + "</span>";
                    strOp += "<div id=\"nulna" + DtOperation.Rows[i]["ID"].ToString().Trim() + "\" class=\"row\" style=\"display: block; margin: 10px\">";
                    strOp += "<div id=\"nuldivna" + DtOperation.Rows[i]["ID"].ToString().Trim() + "\" class=\"col-md-12\" style=\"border: 1px solid #79c7ea; text-align: center;\">";
                    var varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordUserId && x.Shomareh == strItemsFBShomareh1).ToList();
                    DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

                    Guid FBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
                    if (FBId != Guid.NewGuid())
                    {
                        var varRizMetreUsers = (from RizMetreUsers in _context.RizMetreUserses
                                                join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
                                                where RizMetreUsers.LevelNumber == LevelNumber
                                                select new
                                                {
                                                    RizMetreUsers.ID,
                                                    RizMetreUsers.Shomareh,
                                                    RizMetreUsers.Tedad,
                                                    RizMetreUsers.Tool,
                                                    RizMetreUsers.Arz,
                                                    RizMetreUsers.Ertefa,
                                                    RizMetreUsers.Vazn,
                                                    RizMetreUsers.Sharh,
                                                    RizMetreUsers.Des,
                                                    RizMetreUsers.FBId,
                                                    OperationsOfHamlId = RizMetreUsers.OperationsOfHamlId,
                                                    ForItem = RizMetreUsers.ForItem,
                                                    Type = RizMetreUsers.Type,
                                                    UseItem = RizMetreUsers.UseItem,
                                                    BaravordUserId = FB.BarAvordId
                                                }).Where(x => x.FBId == FBId && x.ForItem.Trim() == ItemsFBShomareh.Trim()).OrderBy(x => x.Shomareh).ToList();
                        DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);
                        //strOp += "<script type=\"text/javascript\">AddRizMetreUsersN('" + DtOperation.Rows[i]["ID"].ToString() + "','" + FBId + "','" + lstItemsFields + "'," + LevelNumber + ")</script>";
                        strOp += "<div class=\"row styleHeaderTable\"><div class=\"col-md-1 spanStyleMitraSmall\">شماره</div><div class=\"col-md-2 spanStyleMitraSmall\">شرح</div>";
                        strOp += "<div class=\"col-md-1 spanStyleMitraSmall\"><div style=\"padding-bottom:3px;border-bottom:1px solid #fff\">تعداد</div><div class=\"VahedStyle\">" + DtItemsFields.Rows[0]["Vahed"].ToString().Trim() + " </div></div><div class=\"col-md-1 spanStyleMitraSmall\"><div style=\"padding-bottom:3px;border-bottom:1px solid #fff\">طول</div><div class=\"VahedStyle\">" + DtItemsFields.Rows[1]["Vahed"].ToString().Trim() + "</div></div>";
                        strOp += "<div class=\"col-md-1 spanStyleMitraSmall\"><div style=\"padding-bottom:3px;border-bottom:1px solid #fff\">عرض</div><div class=\"VahedStyle\">" + DtItemsFields.Rows[2]["Vahed"].ToString().Trim() + "</div></div><div class=\"col-md-1 spanStyleMitraSmall\"><div style=\"padding-bottom:3px;border-bottom:1px solid #fff\">ارتفاع</div><div class=\"VahedStyle\">" + DtItemsFields.Rows[3]["Vahed"].ToString().Trim() + "</div></div>";
                        strOp += "<div class=\"col-md-1 spanStyleMitraSmall\"><div style=\"padding-bottom:3px;border-bottom: 1px solid #fff;\">وزن یا...</div><div class=\"VahedStyle\">" + DtItemsFields.Rows[4]["Vahed"].ToString().Trim() + "</div></div><div class=\"col-md-1 spanStyleMitraSmall\"><div style=\"padding-bottom:3px;border-bottom:1px solid #fff\"><span>مقدار جزء</span></div><div class=\"VahedStyle\">" + DtItemsFields.Rows[5]["Vahed"].ToString().Trim() + "</div></div><div class=\"col-md-2 spanStyleMitraSmall\">توضیحات</div>";
                        strOp += "<div class=\"col-md-1 spanStyleMitraSmall\"><span>ویرایش/حذف</span></div></div>";
                        if (DtRizMetreUsers.Rows.Count != 0)
                        {
                            decimal dSumAll = 0;
                            strOp += "<div class=\"row styleFieldTable\">";
                            strOp += "<div class=\"col-md-12 RMCollectStyle\">";
                            for (int m = 0; m < DtRizMetreUsers.Rows.Count; m++)
                            {
                                decimal? dMeghdarJoz = null;
                                decimal? dTedad = DtRizMetreUsers.Rows[m]["Tedad"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[m]["Tedad"].ToString().Trim());
                                decimal? dTool = DtRizMetreUsers.Rows[m]["Tool"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[m]["Tool"].ToString());
                                decimal? dArz = DtRizMetreUsers.Rows[m]["Arz"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[m]["Arz"].ToString());
                                decimal? dErtefa = DtRizMetreUsers.Rows[m]["Ertefa"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[m]["Ertefa"].ToString());
                                decimal? dVazn = DtRizMetreUsers.Rows[m]["Vazn"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsers.Rows[m]["Vazn"].ToString());
                                if (dTedad == null && dTool == null && dArz == null && dErtefa == null && dVazn == null)
                                    dMeghdarJoz = null;
                                else
                                    dMeghdarJoz += (dTedad == null ? 1 : dTedad) * (dTool == null ? 1 : dTool) *
                                        (dArz == null ? 1 : dArz) * (dErtefa == null ? 1 : dErtefa) * (dVazn == null ? 1 : dVazn);
                                dSumAll += dMeghdarJoz == null ? 0 : dMeghdarJoz.Value;

                                string strTedad = DtRizMetreUsers.Rows[m]["Tedad"].ToString().Trim() == "" ? "" : decimal.Parse(DtRizMetreUsers.Rows[m]["Tedad"].ToString()).ToString("G29");
                                string strTool = DtRizMetreUsers.Rows[m]["Tool"].ToString().Trim() == "" ? "" : decimal.Parse(DtRizMetreUsers.Rows[m]["Tool"].ToString()).ToString("G29");
                                string strArz = DtRizMetreUsers.Rows[m]["Arz"].ToString().Trim() == "" ? "" : decimal.Parse(DtRizMetreUsers.Rows[m]["Arz"].ToString()).ToString("G29");
                                string strErtefa = DtRizMetreUsers.Rows[m]["Ertefa"].ToString().Trim() == "" ? "" : decimal.Parse(DtRizMetreUsers.Rows[m]["Ertefa"].ToString()).ToString("G29");
                                string strVazn = DtRizMetreUsers.Rows[m]["Vazn"].ToString().Trim() == "" ? "" : decimal.Parse(DtRizMetreUsers.Rows[m]["Vazn"].ToString()).ToString("G29");
                                strOp += "<div class=\"row styleRowTable\" onclick=\"RizMetreSelectClick('" + DtRizMetreUsers.Rows[m]["ID"].ToString() + "')\"><div class=\"col-md-1\"><span class=\"RMRowStyle\">"
                                    + DtRizMetreUsers.Rows[m]["Shomareh"].ToString() + "</span></div>";
                                strOp += "<div class=\"col-md-2\"><input type=\"text\" class=\"form-control TextEdit spanStyleMitraSmall\" id=\"txtSharh" + DtRizMetreUsers.Rows[m]["ID"].ToString() + "\" value=\"" + DtRizMetreUsers.Rows[m]["Sharh"].ToString() + "\"/></div>";
                                strOp += "<div class=\"col-md-1\"><input type=\"text\" class=\"form-control TextEdit spanStyleMitraSmall\" id=\"txtTedad" + DtRizMetreUsers.Rows[m]["ID"].ToString() + "\" value=\"" + strTedad + "\"/></div>";
                                strOp += " <div class=\"col-md-1\"><input type=\"text\" class=\"form-control TextEdit spanStyleMitraSmall\" id=\"txtTool" + DtRizMetreUsers.Rows[m]["ID"].ToString() + "\" value=\"" + strTool + "\"/></div>";
                                strOp += "<div class=\"col-md-1\"><input type=\"text\" class=\"form-control TextEdit spanStyleMitraSmall\" id=\"txtArz" + DtRizMetreUsers.Rows[m]["ID"].ToString() + "\" value=\"" + strArz + "\"/></div>";
                                strOp += "<div class=\"col-md-1\"><input type=\"text\" class=\"form-control TextEdit spanStyleMitraSmall\" id=\"txtErtefa" + DtRizMetreUsers.Rows[m]["ID"].ToString() + "\" value=\"" + strErtefa + "\"/></div>";
                                strOp += "<div class=\"col-md-1\"><input type=\"text\" class=\"form-control TextEdit spanStyleMitraSmall\" id=\"txtVazn" + DtRizMetreUsers.Rows[m]["ID"].ToString() + "\" value=\"" + strVazn + "\"/></div>";
                                strOp += "<div class=\"col-md-1\"><label class=\"RMMJozStyle\">" + ((dMeghdarJoz == 0) ? "" : Math.Round(dMeghdarJoz == null ? 0 : dMeghdarJoz.Value, 2).ToString()) + "</label></div>";
                                strOp += "<div class=\"col-md-2\"><input type=\"text\" class=\"form-control TextEdit spanStyleMitraSmall\" id=\"txtDes" + DtRizMetreUsers.Rows[m]["ID"].ToString() + "\" value=\"" + DtRizMetreUsers.Rows[m]["Des"].ToString() + "\"/></div>";
                                strOp += "<div class=\"col-md-1\"><i class=\"fa fa-edit EditRMUStyle displayNone\" id=\"iEdit" + DtRizMetreUsers.Rows[m]["ID"].ToString() + "\" onclick=\"EditNRMUClick('" + DtRizMetreUsers.Rows[m]["ID"].ToString() + "','" + DtOperation.Rows[i]["ID"].ToString() + "','" + FBId + "','" + lstItemsFields + "')\"></i><i id=\"iUpdate" + DtRizMetreUsers.Rows[m]["ID"].ToString() + "\" class=\"fa fa-save SaveRMUStyle\" onclick=\"UpdateNRMUClick('" + DtRizMetreUsers.Rows[m]["ID"].ToString() + "','" + DtOperation.Rows[i]["ID"].ToString() + "','" + FBId + "')\"></i><i class=\"fa fa-trash DelRMUStyle\" onclick=\"DeleteRMUNClick('" + DtRizMetreUsers.Rows[m]["ID"].ToString() + "','" + FBId + "')\"></i></div></div>";
                            }
                            strOp += "</div>";
                            strOp += "</div>";
                        }
                    }
                    strOp += "</div></div></div>";
                }
                else
                {
                    string strItemsFBShomareh1 = Dr[0]["ItemsFBShomareh"].ToString().Trim();
                    var varItemsKholasehFosul = (from FehrestBahas in _context.FehrestBahas
                                                 select new
                                                 {
                                                     FehrestBahaShomareh = FehrestBahas.Shomareh,
                                                     FehrestBahas.Sharh,
                                                     FehrestBahas.Vahed,
                                                     FehrestBahas.BahayeVahed,
                                                     FehrestBahas.Sal,
                                                     FehrestBahas.Id,
                                                     FehrestBahas.NoeFB,
                                                     Type = "",
                                                     TypeID = 0,
                                                     ProjectID = 0,
                                                     Meghdar = 0
                                                 }).Where(x => x.Sal == Year && x.NoeFB == NoeFB && x.FehrestBahaShomareh == strItemsFBShomareh1).ToList();
                    DataTable DtItemsKholasehFosul = clsConvert.ToDataTable(varItemsKholasehFosul);


                    strOp += "<div class=\"col-md-12\"><input id=\"na" + DtOperation.Rows[i]["ID"].ToString().Trim() + "\" name=\"Group\" type=\"radio\" onclick=\"NOperationClick('" + DtOperation.Rows[i]["ID"].ToString().Trim() + "'," + LevelNumber + ")\"/><span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"spanAddedItemsClick('" + DtOperation.Rows[i]["ID"].ToString().Trim() + "')\">" + Dr[0]["ItemsFBShomareh"].ToString().Trim() + "-" + DtItemsKholasehFosul.Rows[0]["Sharh"].ToString().Trim() + "</span>";
                    strOp += "<div id=\"nulna" + DtOperation.Rows[i]["ID"].ToString().Trim() + "\" class=\"row\" style=\"display:none;margin:10px\">";
                    strOp += "<div id=\"nuldivna" + DtOperation.Rows[i]["ID"].ToString().Trim() + "\" class=\"col-md-12\" style=\"border:1px solid #79c7ea;text-align:center;\">";
                    strOp += "</div></div></div>";
                }
            }
            else
            {
                string strItemsFBShomareh1 = Dr[0]["ItemsFBShomareh"].ToString().Trim();
                var varItemsKholasehFosul = (from FehrestBahas in _context.FehrestBahas
                                             select new
                                             {
                                                 FehrestBahaShomareh = FehrestBahas.Shomareh,
                                                 FehrestBahas.Sharh,
                                                 FehrestBahas.Vahed,
                                                 FehrestBahas.BahayeVahed,
                                                 FehrestBahas.Sal,
                                                 FehrestBahas.Id,
                                                 FehrestBahas.NoeFB,
                                                 Type = "",
                                                 TypeID = 0,
                                                 ProjectID = 0,
                                                 Meghdar = 0
                                             }).Where(x => x.Sal == Year && x.NoeFB == NoeFB && x.FehrestBahaShomareh == strItemsFBShomareh1).ToList();
                DataTable DtItemsKholasehFosul = clsConvert.ToDataTable(varItemsKholasehFosul);

                strOp += "<div class=\"col-md-12\"><input id=\"na" + DtOperation.Rows[i]["ID"].ToString().Trim() + "\" name=\"Group\" type=\"radio\" onclick=\"NOperationClick('" + DtOperation.Rows[i]["ID"].ToString().Trim() + "'," + LevelNumber + ")\"/><span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"spanAddedItemsClick('" + DtOperation.Rows[i]["ID"].ToString().Trim() + "')\">" + Dr[0]["ItemsFBShomareh"].ToString().Trim() + "-" + DtItemsKholasehFosul.Rows[0]["Sharh"].ToString().Trim() + "</span>";
                strOp += "<div id=\"nulna" + DtOperation.Rows[i]["ID"].ToString().Trim() + "\" class=\"row\" style=\"display:none;margin: 10px\">";
                strOp += "<div id=\"nuldivna" + DtOperation.Rows[i]["ID"].ToString().Trim() + "\" class=\"col-md-12\" style=\"border:1px solid #79c7ea;text-align:center;\">";
                strOp += "</div></div></div>";
            }
        }
        return new JsonResult("OK_" + strOp);
    }

}


public class OperationRequestDto
{
    public long Operation { get; set; }
    public Guid BarAvordUserId { get; set; }
    public NoeFehrestBaha NoeFBId { get; set; }
    public int Year { get; set; }
    /// <summary>
    /// جهت بررسی اینکه ریز متره از کدام سطح فراخوانی شده است
    /// سطح دوم سطوحیست که به آیتم های مربوط میشود که خودشان اضافه بهای سطح اول هستند
    /// </summary>
    public int LevelNumber { get; set; }
}




