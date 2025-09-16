using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.Board.Dto;
using RMS.Controllers.BoardStand.Dto;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Entity;
using static RMS.Models.Common.BoardEnum;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.BoardStand
{
    public class BoardStandController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        public JsonResult ReturnBoardStandItems([FromBody] ReturnBoardStandItemsDto request)
        {
            var BoardStandList = typeof(BoardStandType)
                    .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                    .Select(f => f.GetValue(null)?.ToString())
                    .ToList();

            Guid BaravordId = request.BaravordId;
            List<BarAvordAddedBoardStandDto> lstBarAvordAddedBoardStand =
                _context.BarAvordAddedBoardStands.Include(x => x.boardStandItems)
                .Where(x => x.BarAvordId == BaravordId).Select(x => new BarAvordAddedBoardStandDto
                {
                    BoardStandType = x.boardStandItems.BoardStandType,
                    Tedad = x.Tedad
                }).ToList();

            var result = new
            {
                BoardStandList,
                lstBarAvordAddedBoardStand
            };

            return new JsonResult(result);
        }

        public JsonResult SaveBoardStand([FromBody] BoardStandDto request)
        {
            Guid BaravordId = request.BaravordId;
            int Tedad = request.Tedad;
            int BoardStandType = request.BoardStandType;
            int Year = request.Year;


            ///حذف قبلی ها
            List<Guid> lstBoardStandItemIds = _context.BoardStandItemses.Where(x => x.BoardStandType == BoardStandType).Select(x => x.ID).ToList();

            List<clsBarAvordAddedBoardStand> lstBarAvordAddedBoardStand = _context.BarAvordAddedBoardStands.Where(x => x.BarAvordId == BaravordId && lstBoardStandItemIds.Contains(x.BoardStandItemId)).ToList();

            List<clsRizMetreForBarAvordAddedBoardStand> RizMetreForBarAvordAddedBoardStands = _context.RizMetreForBarAvordAddedBoardStands.Include(x => x.BarAvordAddedBoardStand)
                .Where(x => x.BarAvordAddedBoardStand.BarAvordId == BaravordId && lstBoardStandItemIds.Contains(x.BarAvordAddedBoardStand.BoardStandItemId)).ToList();

            _context.RizMetreForBarAvordAddedBoardStands.RemoveRange(RizMetreForBarAvordAddedBoardStands);

            List<Guid> lstRizMetreForBarAvordAddedBoardStandIds = RizMetreForBarAvordAddedBoardStands.Select(x => x.RizMetreId).ToList();

            var entities = lstRizMetreForBarAvordAddedBoardStandIds.Select(id => new clsRizMetreUsers { ID = id }).ToList();

            _context.RizMetreUserses.RemoveRange(entities);
            _context.BarAvordAddedBoardStands.RemoveRange(lstBarAvordAddedBoardStand);

            _context.SaveChanges();


            //درج جدید
            long Shomareh = 0;
            clsRizMetreUsers? RM = _context.RizMetreUserses.Include(x => x.FB).OrderByDescending(x => x.Shomareh).FirstOrDefault(x => x.FB.BarAvordId == BaravordId);
            if (RM != null)
            {
                Shomareh = RM.Shomareh;
            }
            Shomareh++;

            List<clsBoardStandItems> lstBoardStandItems = _context.BoardStandItemses.Where(x => x.Year == Year && x.BoardStandType == BoardStandType).ToList();
            if (lstBoardStandItems.Count != 0)
            {
                foreach (var item in lstBoardStandItems)
                {
                    string? strAddedItem = item.AddedFBShomareh;
                    if (strAddedItem != null)
                    {
                        clsBarAvordAddedBoardStand barAvordAddedBoardStand = new clsBarAvordAddedBoardStand
                        {
                            BarAvordId = BaravordId,
                            BoardStandItemId = item.ID,
                            Tedad = Tedad
                        };
                        _context.BarAvordAddedBoardStands.Add(barAvordAddedBoardStand);

                        clsFB? FBUsers = _context.FBs.FirstOrDefault(x => x.BarAvordId == BaravordId && x.Shomareh == strAddedItem);
                        Guid FBId = new Guid();
                        if (FBUsers == null)
                        {
                            clsFB FBSave = new clsFB();
                            FBSave.BarAvordId = BaravordId;
                            FBSave.Shomareh = strAddedItem;
                            FBSave.BahayeVahedZarib = 0;
                            _context.FBs.Add(FBSave);
                            _context.SaveChanges();
                            FBId = FBSave.ID;
                        }
                        else
                            FBId = FBUsers.ID;

                        clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
                        RizMetreUsers.Shomareh = Shomareh;
                        Shomareh++;
                        RizMetreUsers.Sharh = item.Sharh;

                        RizMetreUsers.Tedad = item.Tedad * Tedad;

                        RizMetreUsers.Tool = item.Tool;

                        RizMetreUsers.Arz = item.Arz;

                        RizMetreUsers.Ertefa = item.Ertefa;

                        RizMetreUsers.Vazn = item.Zarib;

                        RizMetreUsers.Des = "";

                        RizMetreUsers.FBId = FBId;
                        RizMetreUsers.OperationsOfHamlId = 1;
                        RizMetreUsers.Type = "1";
                        RizMetreUsers.ForItem = "";
                        RizMetreUsers.UseItem = "";
                        RizMetreUsers.LevelNumber = 1;


                        decimal dMeghdarJoz = 0;
                        if (RizMetreUsers.Tedad == null && RizMetreUsers.Tool == null && RizMetreUsers.Arz == null && RizMetreUsers.Ertefa == null && RizMetreUsers.Vazn == null)
                            dMeghdarJoz = 0;
                        else
                            dMeghdarJoz += (RizMetreUsers.Tedad == null ? 1 : RizMetreUsers.Tedad.Value) * (RizMetreUsers.Tool == null ? 1 : RizMetreUsers.Tool.Value) *
                            (RizMetreUsers.Arz == null ? 1 : RizMetreUsers.Arz.Value) * (RizMetreUsers.Ertefa == null ? 1 : RizMetreUsers.Ertefa.Value)
                            * (RizMetreUsers.Vazn == null ? 1 : RizMetreUsers.Vazn.Value);
                        RizMetreUsers.MeghdarJoz = dMeghdarJoz;

                        _context.RizMetreUserses.Add(RizMetreUsers);

                        clsRizMetreForBarAvordAddedBoardStand rizMetreForBarAvordAddedBoardStand = new clsRizMetreForBarAvordAddedBoardStand
                        {
                            BarAvordAddedBoardStandId = barAvordAddedBoardStand.ID,
                            RizMetreId = RizMetreUsers.ID,
                        };
                        _context.RizMetreForBarAvordAddedBoardStands.Add(rizMetreForBarAvordAddedBoardStand);
                    }
                }
            }
            _context.SaveChanges();

            switch (BoardStandType)
            {
                case 1:
                    {


                        //clsFB? FBUsers = _context.FBs.FirstOrDefault(x => x.BarAvordId == BaravordId && x.Shomareh == strAddedItem);
                        //Guid FBId = new Guid();
                        //if (FBUsers == null)
                        //{
                        //    clsFB FBSave = new clsFB();
                        //    FBSave.BarAvordId = BaravordId;
                        //    FBSave.Shomareh = strAddedItem;
                        //    FBSave.BahayeVahedZarib = 0;
                        //    _context.FBs.Add(FBSave);
                        //    _context.SaveChanges();
                        //    FBId = FBSave.ID;
                        //}
                        //else
                        //    FBId = FBUsers.ID;

                        break;
                    }
                default:
                    break;
            }

            return new JsonResult("OK");
        }

        public JsonResult DeleteBoardStand([FromBody] BoardStandForDeleteDto request)
        {
            try
            {
                Guid BaravordId = request.BaravordId;
                int BoardStandType = request.BoardStandType;

                List<Guid> lstBoardStandItemIds = _context.BoardStandItemses.Where(x => x.BoardStandType == BoardStandType).Select(x => x.ID).ToList();

                List<clsBarAvordAddedBoardStand> lstBarAvordAddedBoardStand= _context.BarAvordAddedBoardStands.Where(x => x.BarAvordId == BaravordId && lstBoardStandItemIds.Contains(x.BoardStandItemId)).ToList();
                
                List<clsRizMetreForBarAvordAddedBoardStand> RizMetreForBarAvordAddedBoardStands = _context.RizMetreForBarAvordAddedBoardStands.Include(x => x.BarAvordAddedBoardStand)
                    .Where(x => x.BarAvordAddedBoardStand.BarAvordId == BaravordId && lstBoardStandItemIds.Contains(x.BarAvordAddedBoardStand.BoardStandItemId)).ToList();

                _context.RizMetreForBarAvordAddedBoardStands.RemoveRange(RizMetreForBarAvordAddedBoardStands);

                List<Guid> lstRizMetreForBarAvordAddedBoardStandIds = RizMetreForBarAvordAddedBoardStands.Select(x => x.RizMetreId).ToList();

                var entities = lstRizMetreForBarAvordAddedBoardStandIds.Select(id => new clsRizMetreUsers { ID = id }).ToList();

                _context.RizMetreUserses.RemoveRange(entities);
                _context.BarAvordAddedBoardStands.RemoveRange(lstBarAvordAddedBoardStand);

                _context.SaveChanges();

                return new JsonResult("OK");
            }
            catch (Exception)
            {
                return new JsonResult("NOK");

            }
        }

        public JsonResult GetRizMetreForBoardStand([FromBody] GetRizMetreForBoardStandDto request)
        {
            Guid BaravordId = request.BaravordId;
            NoeFehrestBaha NoeFB = request.NoeFB;
            int Year = request.Year;
            List<int> lstBoardStandType = request.lstBoardStandType;

            List<ItemFBShomarehForGetAndShowAddItemsFieldsDto> lstItemFields = _context.ItemsFieldses.Where(x => x.NoeFB == NoeFB).Select(x => new ItemFBShomarehForGetAndShowAddItemsFieldsDto
            {
                Shomareh = x.ItemShomareh,
                FieldType = x.FieldType,
                Vahed = x.Vahed,
                IsEnteringValue = x.IsEnteringValue
            }).ToList();

            List<RizMetreForBarAvordBoardStandDto> lst = new List<RizMetreForBarAvordBoardStandDto>();

            foreach (var BoardStandType in lstBoardStandType)
            {
                List<Guid> lstBoardStandItemIds = _context.BoardStandItemses.Where(x => x.Year == Year && x.BoardStandType == BoardStandType).Select(x => x.ID).ToList();
                List<clsBarAvordAddedBoardStand> lstbarAvordAddedBoardStand = _context.BarAvordAddedBoardStands.Where(x => x.BarAvordId == BaravordId && lstBoardStandItemIds.Contains(x.BoardStandItemId)).ToList();
                if (lstbarAvordAddedBoardStand.Count != 0)
                {
                    foreach (var barAvordAddedBoardStand in lstbarAvordAddedBoardStand)
                    {
                        List<RizMetreForBarAvordBoardStandDto> lstRizMetreForBarAvordBoard =
                            _context.RizMetreForBarAvordAddedBoardStands.Include(x => x.RizMetreUsers).ThenInclude(x => x.FB)
                            .Where(x => x.BarAvordAddedBoardStandId == barAvordAddedBoardStand.ID)
                            .Select(x => new RizMetreForBarAvordBoardStandDto
                            {
                                Id = x.ID,
                                RizMetreId = x.RizMetreUsers.ID,
                                BarAvordAddedBoardStandId = x.BarAvordAddedBoardStandId,
                                Tedad = x.RizMetreUsers.Tedad,
                                Tool = x.RizMetreUsers.Tool,
                                Arz = x.RizMetreUsers.Arz,
                                Ertefa = x.RizMetreUsers.Ertefa,
                                Vazn = x.RizMetreUsers.Vazn,
                                MeghdarJoz = x.RizMetreUsers.MeghdarJoz,
                                ItemFBShomareh = x.RizMetreUsers.FB.Shomareh,
                                Shomareh = x.RizMetreUsers.Shomareh,
                                Sharh = x.RizMetreUsers.Sharh,
                                Des = x.RizMetreUsers.Des,
                            }).ToList();

                        lst.AddRange(lstRizMetreForBarAvordBoard);
                    }
                }
            }

            List<string> lstItemFBShomareh = lst.Select(x => x.ItemFBShomareh).Distinct().ToList();

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

            lstItemFBShomarehForGet = lstItemFBShomarehForGet
                .OrderBy(x => x.ItemFBShomareh)
                .ToList();

            return new JsonResult(new
            {
                lst,
                lstItemFBShomarehForGet
            });
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
