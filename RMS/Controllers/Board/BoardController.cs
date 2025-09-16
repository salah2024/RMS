using System;
using System.Drawing;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.Board.Dto;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Entity;
using static RMS.Models.Common.BoardEnum;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Board
{
    public class BoardController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        public JsonResult ReturnBoardItems([FromBody] GetBoardItemDto request)
        {
            string BoardName = "octagon";
            long OperationId = request.OperationId;
            clsOperation? operation = _context.Operations.FirstOrDefault(x => x.Id == OperationId);
            if (operation != null)
            {
                BoardName = operation.LatinName;

                List<int> sizeList = new List<int>();
                List<string> MaterialList = new List<string>();
                string SizeName = "";
                switch (BoardName.ToLower())
                {
                    case "octagon":
                        {
                            SizeName = "قطر 8 ضلعی";
                            sizeList = typeof(SizeOctagon)
                                .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                                .Select(f => (int)f.GetValue(null))
                                .ToList();
                            break;
                        }
                    case "circle":
                        {
                            SizeName = "قطر دایره";

                            sizeList = typeof(SizeCircle)
                                .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                                .Select(f => (int)f.GetValue(null))
                                .ToList();
                            break;
                        }
                    case "triangle":
                        {
                            SizeName = "ارتفاع مثلث";
                            sizeList = typeof(SizeTriangle)
                            .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                            .Select(f => (int)f.GetValue(null))
                            .ToList();
                            break;
                        }
                    default:
                        break;
                }

                var materialList = typeof(Material)
                        .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                        .Select(f => f.GetValue(null)?.ToString())
                        .ToList();


                var boardTypeList = typeof(BoardTypes)
                    .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                    .Select(f => f.GetValue(null)?.ToString())
                    .ToList();

                var printTypeList = typeof(PrintTypes)
                        .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                        .Select(f => f.GetValue(null)?.ToString())
                        .ToList();

                var thiknessList = typeof(Thikness)
                        .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                        .Select(f => f.GetValue(null)?.ToString())
                        .ToList();


                var result = new
                {
                    SizeName,
                    OperationId,
                    sizeList,
                    thiknessList,
                    materialList,
                    boardTypeList,
                    printTypeList
                };

                return new JsonResult(result);
            }

            return new JsonResult(null);
        }

        public JsonResult ReturnBoardInfoItems([FromBody] GetBoardItemDto request)
        {
            long OperationId = request.OperationId;

            List<string> MaterialList = new List<string>();

            var materialInfoList = typeof(MaterialInfo)
                    .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                    .Select(f => f.GetValue(null)?.ToString())
                    .ToList();

            var zakhamatVaraqInfoList = typeof(ZakhamatVaraqInfo)
                .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                .Select(f => f.GetValue(null)?.ToString())
                .ToList();

            var printTypeInfoList = typeof(PrintTypeInfo)
                    .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                    .Select(f => f.GetValue(null)?.ToString())
                    .ToList();


            var result = new
            {
                OperationId,
                materialInfoList,
                zakhamatVaraqInfoList,
                printTypeInfoList
            };

            return new JsonResult(result);
        }

        public JsonResult SaveBoard([FromBody] BoardDataForSaveDto request)
        {
            try
            {
                Guid BaravordId = request.BaravordId;

                //clsOperation? operation = _context.Operations.FirstOrDefault(x => x.Id == request.OperationId);
                //int Shape = 1;
                string strMainSharh = "";
                int Shape = request.Shape;
                //if (operation != null)
                //{
                switch (Shape)
                {
                    case 1:
                        {
                            strMainSharh = " شکل 8 ضلعی، ";
                            break;
                        }
                    case 2:
                        {
                            strMainSharh = " شکل دایره، ";
                            break;
                        }
                    case 3:
                        {
                            strMainSharh = " شکل مثلث، ";
                            break;
                        }
                    default:
                        break;
                }
                //}

                string strSize = request.Items.First(x => x.Name.ToLower() == "size").Value == null ? "0" : request.Items.First(x => x.Name.ToLower() == "size").Value;

                int size = int.Parse(strSize);
                int Material = int.Parse(request.Items.First(x => x.Name.ToLower() == "material").Value);
                int Tip = int.Parse(request.Items.First(x => x.Name.ToLower() == "tip").Value);
                int print = int.Parse(request.Items.First(x => x.Name.ToLower() == "print").Value);
                decimal thikness = decimal.Parse(request.Items.First(x => x.Name.ToLower() == "thikness").Value);
                string Sharh = request.Sharh;
                decimal Arz = request.Arz / 1000;
                decimal Ertefa = request.Ertefa / 1000;
                long Tedad = request.Tedad;
                bool UsePOP = request.UsePOP;
                decimal? PercentPrintPOP = request.PercentPrintPOP / 100;

                long Shomareh = 0;
                clsRizMetreUsers? RM = _context.RizMetreUserses.Include(x => x.FB).OrderByDescending(x => x.Shomareh).FirstOrDefault(x => x.FB.BarAvordId == BaravordId);
                if (RM != null)
                {
                    Shomareh = RM.Shomareh;
                }
                Shomareh++;

                if (Material == 1)
                {
                    strMainSharh += "ورق روغنی، ";
                }
                else if (Material == 1)
                {
                    strMainSharh += "ورق گالوانیزه، ";

                }

                strMainSharh += " اندازه " + size + " ،";

                //clsBarAvordAddedBoard? barAvordAddedBoard = _context.BarAvordAddedBoards.FirstOrDefault(x => x.BarAvordId == BaravordId && x.Size == size
                //                                                    && x.Material == Material && x.BoardTip == Tip && x.PrintType == print && x.Thickness == thikness);

                Guid barAvordAddedBoardId = new Guid();
                //if (barAvordAddedBoard == null)
                //{
                clsBarAvordAddedBoard barAvordAddedBoard1 = new clsBarAvordAddedBoard
                {
                    Size = size,
                    Material = Material,
                    BoardTip = Tip,
                    PrintType = print,
                    Thickness = thikness,
                    BarAvordId = BaravordId,
                    BoardType = Shape,
                    Tedad = Tedad,
                    Sharh = Sharh,
                    Arz = Arz,
                    Ertefa = Ertefa,
                    UsePOP = UsePOP,
                    PercentPrintPOP = PercentPrintPOP
                };
                _context.BarAvordAddedBoards.Add(barAvordAddedBoard1);
                _context.SaveChanges();

                barAvordAddedBoardId = barAvordAddedBoard1.ID;
                //}
                //else
                //    barAvordAddedBoardId = barAvordAddedBoard.ID;

                decimal Area = 0;

                if (Shape != 4)
                {
                    clsBoardInfo? boardInfo = _context.BoardInfos.FirstOrDefault(x => x.BoardType == Shape && x.BoardSize == size);
                    if (boardInfo != null)
                    {
                        Area = boardInfo.Area;
                    }
                }

                ////در صورتی که تیپ برابر با 3 باشد یعنی رخ دار
                ///یا ضخامت برابر با 1.25 نیاشد در این صورت
                /////بایستی ابتدا آیتم را در بیس پیدا نموده 
                /// سپس آیتم را در اضافه ها نیز پیدا نمود و آخر سر هر چند آیتم را درج نمود
                /// 

                ///ابتدا آیتم را بیس را پیدا میکنیم
                ///

                //تیپ ساده و لبه دار و ضخامت 1.25

                clsBoardItems? boardItems = new clsBoardItems();
                if (Tip == 1 || Tip == 2)
                {
                    boardItems = _context.BoardItemses.FirstOrDefault(x => x.Shape != "5" && x.Tip == Tip && x.Material == Material && x.PrintType == print && x.Thikness == 1.25m && x.AddedItemType == true);
                }
                else
                    boardItems = _context.BoardItemses.FirstOrDefault(x => x.Shape != "5" && x.Tip == 1 && x.Material == Material && x.PrintType == print && x.Thikness == 1.25m && x.AddedItemType == true);

                string strMainAddedItem = "";
                if (boardItems != null)
                {
                    string strSharh = "";
                    if (Tip == 1)
                    {
                        strSharh += "تیپ ساده، ";
                    }
                    else if (Tip == 2)
                    {
                        strSharh += "تیپ لبه دار، ";
                    }

                    strSharh += " ضخامت 1.25 ";

                    strMainSharh += " شبرنگ " + print + " ،";

                    strMainAddedItem = boardItems.AddedItem;

                    clsFB? FBUsers = _context.FBs.FirstOrDefault(x => x.BarAvordId == BaravordId && x.Shomareh == strMainAddedItem);
                    Guid FBId = new Guid();
                    if (FBUsers == null)
                    {
                        clsFB FBSave = new clsFB();
                        FBSave.BarAvordId = BaravordId;
                        FBSave.Shomareh = strMainAddedItem;
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
                    RizMetreUsers.Sharh = Sharh;

                    RizMetreUsers.Tedad = Tedad;

                    RizMetreUsers.Tool = null;
                    if (Shape != 4)
                    {
                        RizMetreUsers.Arz = null;

                        RizMetreUsers.Ertefa = null;

                        RizMetreUsers.Vazn = Area;
                    }
                    else
                    {
                        RizMetreUsers.Arz = Arz;

                        RizMetreUsers.Ertefa = Ertefa;

                        RizMetreUsers.Vazn = null;
                    }


                    RizMetreUsers.Des = strMainSharh + strSharh;
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

                    ///ریزمتره درج شده به ریزمتره های درج شده در تابلو اضافه میشود
                    clsRizMetreForBarAvordAddedBoard RizMetreForBarAvordAddedBoard = new clsRizMetreForBarAvordAddedBoard
                    {
                        BarAvordAddedBoardId = barAvordAddedBoardId,
                        InsertDateTime = DateTime.Now,
                        RizMetreId = RizMetreUsers.ID,
                        IsDeleted = false
                    };
                    _context.RizMetreForBarAvordAddedBoards.Add(RizMetreForBarAvordAddedBoard);

                    _context.SaveChanges();
                }


                //clsBoardItems? boardItems = _context.BoardItemses.FirstOrDefault(x => x.Shape != "5" && (x.Tip == 1 || x.Tip == 2) && x.Material == Material && x.PrintType == print && x.Thikness == 1.25m && x.AddedItemType == true);
                //string strMainAddedItem = "";
                //if (boardItems != null)
                //{
                //    string strSharh = "";
                //    if (Tip == 1)
                //    {
                //        strSharh += "تیپ ساده، ";
                //    }
                //    else if (Tip == 1)
                //    {
                //        strSharh += "تیپ لبه دار، ";
                //    }

                //    strSharh += " ضخامت 1.25 ";

                //    strMainSharh += " شبرنگ " + print + " ،";

                //    strMainAddedItem = boardItems.AddedItem;

                //    clsFB? FBUsers = _context.FBs.FirstOrDefault(x => x.BarAvordId == BaravordId && x.Shomareh == strMainAddedItem);
                //    Guid FBId = new Guid();
                //    if (FBUsers == null)
                //    {
                //        clsFB FBSave = new clsFB();
                //        FBSave.BarAvordId = BaravordId;
                //        FBSave.Shomareh = strMainAddedItem;
                //        FBSave.BahayeVahedZarib = 0;
                //        _context.FBs.Add(FBSave);
                //        _context.SaveChanges();
                //        FBId = FBSave.ID;
                //    }
                //    else
                //        FBId = FBUsers.ID;




                //    clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
                //    RizMetreUsers.Shomareh = Shomareh;
                //    Shomareh++;
                //    RizMetreUsers.Sharh = Sharh;

                //    RizMetreUsers.Tedad = Tedad;

                //    RizMetreUsers.Tool = null;

                //    if (Shape != 4)
                //    {
                //        RizMetreUsers.Arz = null;

                //        RizMetreUsers.Ertefa = null;

                //        RizMetreUsers.Vazn = Area;
                //    }
                //    else
                //    {
                //        RizMetreUsers.Arz = Arz;

                //        RizMetreUsers.Ertefa = Ertefa;

                //        RizMetreUsers.Vazn = null;
                //    }


                //    RizMetreUsers.Des = strMainSharh + strSharh;
                //    RizMetreUsers.FBId = FBId;
                //    RizMetreUsers.OperationsOfHamlId = 1;
                //    RizMetreUsers.Type = "1";
                //    RizMetreUsers.ForItem = "";
                //    RizMetreUsers.UseItem = "";
                //    RizMetreUsers.LevelNumber = 1;


                //    decimal dMeghdarJoz = 0;
                //    if (RizMetreUsers.Tedad == null && RizMetreUsers.Tool == null && RizMetreUsers.Arz == null && RizMetreUsers.Ertefa == null && RizMetreUsers.Vazn == null)
                //        dMeghdarJoz = 0;
                //    else
                //        dMeghdarJoz += (RizMetreUsers.Tedad == null ? 1 : RizMetreUsers.Tedad.Value) * (RizMetreUsers.Tool == null ? 1 : RizMetreUsers.Tool.Value) *
                //        (RizMetreUsers.Arz == null ? 1 : RizMetreUsers.Arz.Value) * (RizMetreUsers.Ertefa == null ? 1 : RizMetreUsers.Ertefa.Value)
                //        * (RizMetreUsers.Vazn == null ? 1 : RizMetreUsers.Vazn.Value);
                //    RizMetreUsers.MeghdarJoz = dMeghdarJoz;

                //    _context.RizMetreUserses.Add(RizMetreUsers);

                //    ///ریزمتره درج شده به ریزمتره های درج شده در تابلو اضافه میشود
                //    clsRizMetreForBarAvordAddedBoard RizMetreForBarAvordAddedBoard = new clsRizMetreForBarAvordAddedBoard
                //    {
                //        BarAvordAddedBoardId = barAvordAddedBoardId,
                //        InsertDateTime = DateTime.Now,
                //        RizMetreId = RizMetreUsers.ID,
                //        IsDeleted = false
                //    };
                //    _context.RizMetreForBarAvordAddedBoards.Add(RizMetreForBarAvordAddedBoard);

                //    _context.SaveChanges();
                //}

                if (thikness != 1.25m)
                {
                    List<clsBoardItems> lstboardItems = _context.BoardItemses.Where(x => (x.Shape != "5" && x.Material == Material && x.Thikness == thikness && x.AddedItemType == false)).ToList();
                    if (lstboardItems.Count != 0)
                    {
                        string strSharh = "";
                        strMainSharh += " شبرنگ " + print + " ،";
                        foreach (var BI in lstboardItems)
                        {

                            string strAddedItem = "";
                            strAddedItem = BI.AddedItem;

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
                            RizMetreUsers.Sharh = Sharh;

                            RizMetreUsers.Tedad = Tedad;

                            RizMetreUsers.Tool = null;

                            if (Shape != 4)
                            {
                                RizMetreUsers.Arz = null;

                                RizMetreUsers.Ertefa = null;

                                RizMetreUsers.Vazn = Area;
                            }
                            else
                            {
                                RizMetreUsers.Arz = Arz;

                                RizMetreUsers.Ertefa = Ertefa;

                                RizMetreUsers.Vazn = null;
                            }


                            RizMetreUsers.Des = strMainSharh + strSharh + " ضخامت " + BI.Thikness + " میلیمتر ";
                            RizMetreUsers.FBId = FBId;
                            RizMetreUsers.OperationsOfHamlId = 1;
                            RizMetreUsers.Type = "2";
                            RizMetreUsers.ForItem = strMainAddedItem;
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

                            ///ریزمتره درج شده به ریزمتره های درج شده در تابلو اضافه میشود
                            clsRizMetreForBarAvordAddedBoard RizMetreForBarAvordAddedBoard = new clsRizMetreForBarAvordAddedBoard
                            {
                                BarAvordAddedBoardId = barAvordAddedBoardId,
                                InsertDateTime = DateTime.Now,
                                RizMetreId = RizMetreUsers.ID,
                                IsDeleted = false
                            };
                            _context.RizMetreForBarAvordAddedBoards.Add(RizMetreForBarAvordAddedBoard);

                            _context.SaveChanges();
                        }
                        //}
                    }
                }
                if (Tip == 3)
                {
                    List<clsBoardItems> lstboardItems = _context.BoardItemses.Where(x => (x.Shape != "5" && x.Tip == 3 && x.AddedItemType == false)).ToList();
                    if (lstboardItems.Count != 0)
                    {
                        string strSharh = " تیپ رخ دار، ";
                        strMainSharh += " شبرنگ " + print + " ،";
                        foreach (var BI in lstboardItems)
                        {

                            string strAddedItem = "";
                            strAddedItem = BI.AddedItem;

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
                            RizMetreUsers.Sharh = Sharh;

                            RizMetreUsers.Tedad = Tedad;

                            RizMetreUsers.Tool = null;

                            if (Shape != 4)
                            {
                                RizMetreUsers.Arz = null;

                                RizMetreUsers.Ertefa = null;

                                RizMetreUsers.Vazn = Area;
                            }
                            else
                            {
                                RizMetreUsers.Arz = Arz;

                                RizMetreUsers.Ertefa = Ertefa;

                                RizMetreUsers.Vazn = null;
                            }


                            RizMetreUsers.Des = strMainSharh + strSharh + " ضخامت " + BI.Thikness + " میلیمتر ";
                            RizMetreUsers.FBId = FBId;
                            RizMetreUsers.OperationsOfHamlId = 1;
                            RizMetreUsers.Type = "2";
                            RizMetreUsers.ForItem = strMainAddedItem;
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

                            ///ریزمتره درج شده به ریزمتره های درج شده در تابلو اضافه میشود
                            clsRizMetreForBarAvordAddedBoard RizMetreForBarAvordAddedBoard = new clsRizMetreForBarAvordAddedBoard
                            {
                                BarAvordAddedBoardId = barAvordAddedBoardId,
                                InsertDateTime = DateTime.Now,
                                RizMetreId = RizMetreUsers.ID,
                                IsDeleted = false
                            };
                            _context.RizMetreForBarAvordAddedBoards.Add(RizMetreForBarAvordAddedBoard);

                            _context.SaveChanges();
                        }
                        //}
                    }
                }



                //در صورتی که تیپ رخ دار باشد  
                //if (Tip == 3)
                //{
                //    List<clsBoardItems> lstboardItems = _context.BoardItemses.Where(x => x.Shape != "5" && (x.Tip == Tip) || (x.Material == Material && x.Thikness != 1.25m && x.AddedItemType == false)).ToList();
                //    if (lstboardItems.Count != 0)
                //    {
                //        string strSharh = " تیپ رخ دار، ";
                //        strMainSharh += " شبرنگ " + print + " ،";
                //        foreach (var BI in lstboardItems)
                //        {

                //            string strAddedItem = "";
                //            strAddedItem = BI.AddedItem;

                //            clsFB? FBUsers = _context.FBs.FirstOrDefault(x => x.BarAvordId == BaravordId && x.Shomareh == strAddedItem);
                //            Guid FBId = new Guid();
                //            if (FBUsers == null)
                //            {
                //                clsFB FBSave = new clsFB();
                //                FBSave.BarAvordId = BaravordId;
                //                FBSave.Shomareh = strAddedItem;
                //                FBSave.BahayeVahedZarib = 0;
                //                _context.FBs.Add(FBSave);
                //                _context.SaveChanges();
                //                FBId = FBSave.ID;
                //            }
                //            else
                //                FBId = FBUsers.ID;



                //            clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
                //            RizMetreUsers.Shomareh = Shomareh;
                //            Shomareh++;

                //            RizMetreUsers.Sharh = Sharh;

                //            RizMetreUsers.Tedad = Tedad;

                //            RizMetreUsers.Tool = null;

                //            if (Shape != 4)
                //            {
                //                RizMetreUsers.Arz = null;

                //                RizMetreUsers.Ertefa = null;

                //                RizMetreUsers.Vazn = Area;
                //            }
                //            else
                //            {
                //                RizMetreUsers.Arz = Arz;

                //                RizMetreUsers.Ertefa = Ertefa;

                //                RizMetreUsers.Vazn = null;
                //            }


                //            RizMetreUsers.Des = strMainSharh + strSharh + " ضخامت " + BI.Thikness + " میلیمتر ";
                //            RizMetreUsers.FBId = FBId;
                //            RizMetreUsers.OperationsOfHamlId = 1;
                //            RizMetreUsers.Type = "2";
                //            RizMetreUsers.ForItem = strMainAddedItem;
                //            RizMetreUsers.UseItem = "";
                //            RizMetreUsers.LevelNumber = 1;


                //            decimal dMeghdarJoz = 0;
                //            if (RizMetreUsers.Tedad == null && RizMetreUsers.Tool == null && RizMetreUsers.Arz == null && RizMetreUsers.Ertefa == null && RizMetreUsers.Vazn == null)
                //                dMeghdarJoz = 0;
                //            else
                //                dMeghdarJoz += (RizMetreUsers.Tedad == null ? 1 : RizMetreUsers.Tedad.Value) * (RizMetreUsers.Tool == null ? 1 : RizMetreUsers.Tool.Value) *
                //                (RizMetreUsers.Arz == null ? 1 : RizMetreUsers.Arz.Value) * (RizMetreUsers.Ertefa == null ? 1 : RizMetreUsers.Ertefa.Value)
                //                * (RizMetreUsers.Vazn == null ? 1 : RizMetreUsers.Vazn.Value);
                //            RizMetreUsers.MeghdarJoz = dMeghdarJoz;

                //            _context.RizMetreUserses.Add(RizMetreUsers);

                //            ///ریزمتره درج شده به ریزمتره های درج شده در تابلو اضافه میشود
                //            clsRizMetreForBarAvordAddedBoard RizMetreForBarAvordAddedBoard = new clsRizMetreForBarAvordAddedBoard
                //            {
                //                BarAvordAddedBoardId = barAvordAddedBoardId,
                //                InsertDateTime = DateTime.Now,
                //                RizMetreId = RizMetreUsers.ID,
                //                IsDeleted = false
                //            };
                //            _context.RizMetreForBarAvordAddedBoards.Add(RizMetreForBarAvordAddedBoard);

                //            _context.SaveChanges();
                //        }
                //    }

                //}
                //در صورتی که از
                //POP
                //استفاده شده باشد
                if (UsePOP)
                {
                    clsBoardItems? boardItems1 = _context.BoardItemses.FirstOrDefault(x => x.Shape != "5" && x.PrintType == 4 && x.PrintTypeBase == print && x.AddedItemType == false);
                    strMainSharh += " استفاده از شبرنگ POP بجای " + print + " ،";

                    if (boardItems != null)
                    {
                        string strAddedItem = "";
                        strAddedItem = boardItems1.AddedItem;

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
                        RizMetreUsers.Sharh = Sharh;

                        RizMetreUsers.Tedad = Tedad;

                        RizMetreUsers.Tool = null;

                        if (Shape != 4)
                        {
                            RizMetreUsers.Arz = null;

                            RizMetreUsers.Ertefa = PercentPrintPOP;

                            RizMetreUsers.Vazn = Area;
                        }
                        else
                        {
                            RizMetreUsers.Arz = Arz;

                            RizMetreUsers.Ertefa = Ertefa;

                            RizMetreUsers.Vazn = PercentPrintPOP;
                        }

                        RizMetreUsers.Des = strMainSharh + strMainSharh;
                        RizMetreUsers.FBId = FBId;
                        RizMetreUsers.OperationsOfHamlId = 1;
                        RizMetreUsers.Type = "2";
                        RizMetreUsers.ForItem = strMainAddedItem;
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

                        ///ریزمتره درج شده به ریزمتره های درج شده در تابلو اضافه میشود
                        clsRizMetreForBarAvordAddedBoard RizMetreForBarAvordAddedBoard = new clsRizMetreForBarAvordAddedBoard
                        {
                            BarAvordAddedBoardId = barAvordAddedBoardId,
                            InsertDateTime = DateTime.Now,
                            RizMetreId = RizMetreUsers.ID,
                            IsDeleted = false
                        };
                        _context.RizMetreForBarAvordAddedBoards.Add(RizMetreForBarAvordAddedBoard);

                        _context.SaveChanges();
                    }
                }
                return new JsonResult("OK");

            }
            catch (Exception)
            {
                return new JsonResult("NOK");
            }
        }

        public JsonResult UpdateBoard([FromBody] BoardDataDto request)
        {

            try
            {
                Guid BaravordId = request.BaravordId;
                Guid barAvordAddedBoardId = request.barAvordAddedBoardId;

                string strMainSharh = "";

                int Shape = request.Shape;
                switch (Shape)
                {
                    case 1:
                        {
                            strMainSharh = " شکل 8 ضلعی، ";
                            break;
                        }
                    case 2:
                        {
                            strMainSharh = " شکل دایره، ";
                            break;
                        }
                    case 3:
                        {
                            strMainSharh = " شکل مثلث، ";
                            break;
                        }
                    default:
                        break;
                }

                int size = int.Parse(request.Items.First(x => x.Name.ToLower() == "size").Value);
                int Material = int.Parse(request.Items.First(x => x.Name.ToLower() == "material").Value);
                int Tip = int.Parse(request.Items.First(x => x.Name.ToLower() == "tip").Value);
                int print = int.Parse(request.Items.First(x => x.Name.ToLower() == "print").Value);
                decimal thikness = decimal.Parse(request.Items.First(x => x.Name.ToLower() == "thikness").Value);

                decimal Arz = request.Arz / 1000;
                decimal Ertefa = request.Ertefa / 1000;

                string Sharh = request.Sharh;
                long Tedad = request.Tedad;
                bool UsePOP = request.UsePOP;
                decimal? PercentPrintPOP = request.PercentPrintPOP / 100;

                if (Material == 1)
                {
                    strMainSharh += "ورق روغنی، ";
                }
                else if (Material == 1)
                {
                    strMainSharh += "ورق گالوانیزه، ";

                }

                strMainSharh += " اندازه " + size + " ،";

                clsBarAvordAddedBoard? barAvordAddedBoard = _context.BarAvordAddedBoards.FirstOrDefault(x => x.ID == barAvordAddedBoardId);
                if (barAvordAddedBoard != null)
                {
                    barAvordAddedBoard.Size = size;
                    barAvordAddedBoard.Material = Material;
                    barAvordAddedBoard.BoardTip = Tip;
                    barAvordAddedBoard.PrintType = print;
                    barAvordAddedBoard.Thickness = thikness;
                    barAvordAddedBoard.BoardType = Shape;
                    barAvordAddedBoard.Tedad = Tedad;
                    barAvordAddedBoard.Sharh = Sharh;

                    barAvordAddedBoard.Arz = Arz;
                    barAvordAddedBoard.Ertefa = Ertefa;

                    barAvordAddedBoard.UsePOP = UsePOP;
                    barAvordAddedBoard.PercentPrintPOP = PercentPrintPOP;

                    List<clsRizMetreForBarAvordAddedBoard> lstRizMetreForBarAvordAddedBoard =
                        _context.RizMetreForBarAvordAddedBoards.Where(x => x.BarAvordAddedBoardId == barAvordAddedBoard.ID).ToList();

                    List<Guid> lstRizMetreIds = lstRizMetreForBarAvordAddedBoard.Select(x => x.RizMetreId).ToList();
                    List<clsRizMetreUsers> lstRizMetre = _context.RizMetreUserses.Where(x => lstRizMetreIds.Contains(x.ID)).ToList();

                    _context.RizMetreUserses.RemoveRange(lstRizMetre);
                    _context.SaveChanges();
                }
                else
                {
                    return new JsonResult("NOK");
                }

                long Shomareh = 0;
                clsRizMetreUsers? RM = _context.RizMetreUserses.Include(x => x.FB).OrderByDescending(x => x.Shomareh).FirstOrDefault(x => x.FB.BarAvordId == BaravordId);
                if (RM != null)
                {
                    Shomareh = RM.Shomareh;
                }
                Shomareh++;

                decimal Area = 0;
                if (Shape != 4)
                {
                    clsBoardInfo? boardInfo = _context.BoardInfos.FirstOrDefault(x => x.BoardType == Shape && x.BoardSize == size);
                    if (boardInfo != null)
                    {
                        Area = boardInfo.Area;
                    }
                }
                //else
                //{
                //    Area = Arz * Ertefa;

                //}

                ////در صورتی که تیپ برابر با 3 باشد یعنی رخ دار
                ///یا ضخامت برابر با 1.25 نیاشد در این صورت
                /////بایستی ابتدا آیتم را در بیس پیدا نموده 
                /// سپس آیتم را در اضافه ها نیز پیدا نمود و آخر سر هر چند آیتم را درج نمود
                /// 

                ///ابتدا آیتم را بیس را پیدا میکنیم
                ///

                //تیپ ساده و لبه دار و ضخامت 1.25

                clsBoardItems? boardItems = new clsBoardItems();
                if (Tip==1 || Tip==2)
                {
                    boardItems = _context.BoardItemses.FirstOrDefault(x => x.Shape != "5" && x.Tip == Tip && x.Material == Material && x.PrintType == print && x.Thikness == 1.25m && x.AddedItemType == true);
                }
                else
                    boardItems = _context.BoardItemses.FirstOrDefault(x => x.Shape != "5" && x.Tip == 1 && x.Material == Material && x.PrintType == print && x.Thikness == 1.25m && x.AddedItemType == true);

                string strMainAddedItem = "";
                if (boardItems != null)
                {
                    string strSharh = "";
                    if (Tip == 1)
                    {
                        strSharh += "تیپ ساده، ";
                    }
                    else if (Tip == 2)
                    {
                        strSharh += "تیپ لبه دار، ";
                    }

                    strSharh += " ضخامت 1.25 ";

                    strMainSharh += " شبرنگ " + print + " ،";

                    strMainAddedItem = boardItems.AddedItem;

                    clsFB? FBUsers = _context.FBs.FirstOrDefault(x => x.BarAvordId == BaravordId && x.Shomareh == strMainAddedItem);
                    Guid FBId = new Guid();
                    if (FBUsers == null)
                    {
                        clsFB FBSave = new clsFB();
                        FBSave.BarAvordId = BaravordId;
                        FBSave.Shomareh = strMainAddedItem;
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
                    RizMetreUsers.Sharh = Sharh;

                    RizMetreUsers.Tedad = Tedad;

                    RizMetreUsers.Tool = null;
                    if (Shape != 4)
                    {
                        RizMetreUsers.Arz = null;

                        RizMetreUsers.Ertefa = null;

                        RizMetreUsers.Vazn = Area;
                    }
                    else
                    {
                        RizMetreUsers.Arz = Arz;

                        RizMetreUsers.Ertefa = Ertefa;

                        RizMetreUsers.Vazn = null;
                    }


                    RizMetreUsers.Des = strMainSharh + strSharh;
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

                    ///ریزمتره درج شده به ریزمتره های درج شده در تابلو اضافه میشود
                    clsRizMetreForBarAvordAddedBoard RizMetreForBarAvordAddedBoard = new clsRizMetreForBarAvordAddedBoard
                    {
                        BarAvordAddedBoardId = barAvordAddedBoardId,
                        InsertDateTime = DateTime.Now,
                        RizMetreId = RizMetreUsers.ID,
                        IsDeleted = false
                    };
                    _context.RizMetreForBarAvordAddedBoards.Add(RizMetreForBarAvordAddedBoard);

                    _context.SaveChanges();
                }
                //در صورتی که تیپ رخ دار باشد  
                //if (Tip == 3)
                //{
                if (thikness != 1.25m)
                {
                    List<clsBoardItems> lstboardItems = _context.BoardItemses.Where(x => (x.Shape != "5" && x.Material == Material && x.Thikness == thikness && x.AddedItemType == false)).ToList();
                    if (lstboardItems.Count != 0)
                    {
                        string strSharh = "";
                        strMainSharh += " شبرنگ " + print + " ،";
                        foreach (var BI in lstboardItems)
                        {

                            string strAddedItem = "";
                            strAddedItem = BI.AddedItem;

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
                            RizMetreUsers.Sharh = Sharh;

                            RizMetreUsers.Tedad = Tedad;

                            RizMetreUsers.Tool = null;

                            if (Shape != 4)
                            {
                                RizMetreUsers.Arz = null;

                                RizMetreUsers.Ertefa = null;

                                RizMetreUsers.Vazn = Area;
                            }
                            else
                            {
                                RizMetreUsers.Arz = Arz;

                                RizMetreUsers.Ertefa = Ertefa;

                                RizMetreUsers.Vazn = null;
                            }


                            RizMetreUsers.Des = strMainSharh + strSharh + " ضخامت " + BI.Thikness + " میلیمتر ";
                            RizMetreUsers.FBId = FBId;
                            RizMetreUsers.OperationsOfHamlId = 1;
                            RizMetreUsers.Type = "2";
                            RizMetreUsers.ForItem = strMainAddedItem;
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

                            ///ریزمتره درج شده به ریزمتره های درج شده در تابلو اضافه میشود
                            clsRizMetreForBarAvordAddedBoard RizMetreForBarAvordAddedBoard = new clsRizMetreForBarAvordAddedBoard
                            {
                                BarAvordAddedBoardId = barAvordAddedBoardId,
                                InsertDateTime = DateTime.Now,
                                RizMetreId = RizMetreUsers.ID,
                                IsDeleted = false
                            };
                            _context.RizMetreForBarAvordAddedBoards.Add(RizMetreForBarAvordAddedBoard);

                            _context.SaveChanges();
                        }
                        //}
                    }
                }
                if (Tip == 3)
                {
                    List<clsBoardItems> lstboardItems = _context.BoardItemses.Where(x => (x.Shape != "5" && x.Tip == 3 && x.AddedItemType == false)).ToList();
                    if (lstboardItems.Count != 0)
                    {
                        string strSharh = " تیپ رخ دار، ";
                        strMainSharh += " شبرنگ " + print + " ،";
                        foreach (var BI in lstboardItems)
                        {

                            string strAddedItem = "";
                            strAddedItem = BI.AddedItem;

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
                            RizMetreUsers.Sharh = Sharh;

                            RizMetreUsers.Tedad = Tedad;

                            RizMetreUsers.Tool = null;

                            if (Shape != 4)
                            {
                                RizMetreUsers.Arz = null;

                                RizMetreUsers.Ertefa = null;

                                RizMetreUsers.Vazn = Area;
                            }
                            else
                            {
                                RizMetreUsers.Arz = Arz;

                                RizMetreUsers.Ertefa = Ertefa;

                                RizMetreUsers.Vazn = null;
                            }


                            RizMetreUsers.Des = strMainSharh + strSharh + " ضخامت " + BI.Thikness + " میلیمتر ";
                            RizMetreUsers.FBId = FBId;
                            RizMetreUsers.OperationsOfHamlId = 1;
                            RizMetreUsers.Type = "2";
                            RizMetreUsers.ForItem = strMainAddedItem;
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

                            ///ریزمتره درج شده به ریزمتره های درج شده در تابلو اضافه میشود
                            clsRizMetreForBarAvordAddedBoard RizMetreForBarAvordAddedBoard = new clsRizMetreForBarAvordAddedBoard
                            {
                                BarAvordAddedBoardId = barAvordAddedBoardId,
                                InsertDateTime = DateTime.Now,
                                RizMetreId = RizMetreUsers.ID,
                                IsDeleted = false
                            };
                            _context.RizMetreForBarAvordAddedBoards.Add(RizMetreForBarAvordAddedBoard);

                            _context.SaveChanges();
                        }
                        //}
                    }
                }
                //در صورتی که از
                //POP
                //استفاده شده باشد
                if (UsePOP)
                {
                    clsBoardItems? boardItems1 = _context.BoardItemses.FirstOrDefault(x => x.Shape != "5" && x.PrintType == 4 && x.PrintTypeBase == print && x.AddedItemType == false);
                    strMainSharh += " استفاده از شبرنگ POP بجای " + print + " ،";

                    if (boardItems != null)
                    {
                        string strAddedItem = "";
                        strAddedItem = boardItems1.AddedItem;

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
                        RizMetreUsers.Sharh = Sharh;

                        RizMetreUsers.Tedad = Tedad;

                        RizMetreUsers.Tool = null;

                        if (Shape != 4)
                        {
                            RizMetreUsers.Arz = null;

                            RizMetreUsers.Ertefa = PercentPrintPOP;

                            RizMetreUsers.Vazn = Area;
                        }
                        else
                        {
                            RizMetreUsers.Arz = Arz;

                            RizMetreUsers.Ertefa = Ertefa;

                            RizMetreUsers.Vazn = PercentPrintPOP;
                        }


                        RizMetreUsers.Des = strMainSharh + strMainSharh;
                        RizMetreUsers.FBId = FBId;
                        RizMetreUsers.OperationsOfHamlId = 1;
                        RizMetreUsers.Type = "2";
                        RizMetreUsers.ForItem = strMainAddedItem;
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

                        ///ریزمتره درج شده به ریزمتره های درج شده در تابلو اضافه میشود
                        clsRizMetreForBarAvordAddedBoard RizMetreForBarAvordAddedBoard = new clsRizMetreForBarAvordAddedBoard
                        {
                            BarAvordAddedBoardId = barAvordAddedBoardId,
                            InsertDateTime = DateTime.Now,
                            RizMetreId = RizMetreUsers.ID,
                            IsDeleted = false
                        };
                        _context.RizMetreForBarAvordAddedBoards.Add(RizMetreForBarAvordAddedBoard);

                        _context.SaveChanges();
                    }
                }
                return new JsonResult("OK");

            }
            catch (Exception)
            {
                return new JsonResult("NOK");
            }
        }

        public JsonResult DeleteBoard([FromBody] BoardDataForDeleteDto request)
        {
            try
            {
                Guid barAvordAddedBoardId = request.barAvordAddedBoardId;

                clsBarAvordAddedBoard? barAvordAddedBoard = _context.BarAvordAddedBoards.FirstOrDefault(x => x.ID == barAvordAddedBoardId);
                if (barAvordAddedBoard != null)
                {
                    List<clsRizMetreForBarAvordAddedBoard> lstRizMetreForBarAvordAddedBoard =
                        _context.RizMetreForBarAvordAddedBoards.Where(x => x.BarAvordAddedBoardId == barAvordAddedBoardId).ToList();

                    List<Guid> lstRizMetreIds = lstRizMetreForBarAvordAddedBoard.Select(x => x.RizMetreId).ToList();
                    List<clsRizMetreUsers> lstRizMetre = _context.RizMetreUserses.Where(x => lstRizMetreIds.Contains(x.ID)).ToList();

                    _context.RizMetreUserses.RemoveRange(lstRizMetre);
                    _context.BarAvordAddedBoards.Remove(barAvordAddedBoard);

                    _context.SaveChanges();

                    return new JsonResult("OK");
                }
                else
                {
                    return new JsonResult("NOK");
                }
            }
            catch (Exception)
            {
                return new JsonResult("NOK");
            }
        }

        public JsonResult GetRizMetreForBoard([FromBody] GetRizMetreForBoardDto request)
        {
            Guid BaravordId = request.BaravordId;
            NoeFehrestBaha NoeFB = request.NoeFB;
            int Year = request.Year;

            List<int> lstBoardType = request.lstBoardType;

            List<ItemFBShomarehForGetAndShowAddItemsFieldsDto> lstItemFields = _context.ItemsFieldses.Where(x => x.NoeFB == NoeFB).Select(x => new ItemFBShomarehForGetAndShowAddItemsFieldsDto
            {
                Shomareh = x.ItemShomareh,
                FieldType = x.FieldType,
                Vahed = x.Vahed,
                IsEnteringValue = x.IsEnteringValue
            }).ToList();

            long OperationId = request.OperationId;
            clsOperation? operation = _context.Operations.FirstOrDefault(x => x.Id == OperationId);

            //int Shape = request.Shape;
            //if (operation != null)
            //{
            //    switch (operation.LatinName.ToLower())
            //    {
            //        case "octagon":
            //            {
            //                Shape = 1;
            //                break;
            //            }
            //        case "circle":
            //            {
            //                Shape = 2;
            //                break;
            //            }
            //        case "triangle":
            //            {
            //                Shape = 3;
            //                break;
            //            }
            //        default:
            //            break;
            //    }
            //}

            List<clsBarAvordAddedBoard> lstbarAvordAddedBoard = _context.BarAvordAddedBoards.Where(x => x.BarAvordId == BaravordId && lstBoardType.Contains(x.BoardType)).ToList();
            if (lstbarAvordAddedBoard.Count != 0)
            {
                List<RizMetreForBarAvordBoardDto> lst = new List<RizMetreForBarAvordBoardDto>();
                foreach (var barAvordAddedBoard in lstbarAvordAddedBoard)
                {
                    List<RizMetreForBarAvordBoardDto> lstRizMetreForBarAvordBoard =
                        _context.RizMetreForBarAvordAddedBoards.Include(x => x.RizMetreUsers).ThenInclude(x => x.FB)
                        .Where(x => x.BarAvordAddedBoardId == barAvordAddedBoard.ID)
                        .Select(x => new RizMetreForBarAvordBoardDto
                        {
                            Id = x.ID,
                            RizMetreId = x.RizMetreUsers.ID,
                            BarAvordAddedBoardId = x.BarAvordAddedBoardId,
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

                return new JsonResult(new
                {
                    lst,
                    lstItemFBShomarehForGet
                });
            }


            return new JsonResult("OK");
        }

        public JsonResult GetBarAvordAddedBoardWithId([FromBody] GetBarAvordAddedBoardWithIdDto request)
        {
            Guid BarAvordAddedBoardId = request.BarAvordAddedBoardId;
            clsBarAvordAddedBoard? barAvordAddedBoard = _context.BarAvordAddedBoards.FirstOrDefault(x => x.ID == BarAvordAddedBoardId);
            if (barAvordAddedBoard != null)
                return new JsonResult(barAvordAddedBoard);
            else
                return new JsonResult("NOK");

        }

        public JsonResult UpdateRizMetre([FromBody] BoardUpdateRizMetreDto request)
        {

            Guid RizMetreId = request.RizMetreId;
            decimal? Tedad = request.Tedad;

            clsRizMetreUsers? rizMetreUsers = _context.RizMetreUserses.FirstOrDefault(x => x.ID == RizMetreId);
            if (rizMetreUsers != null)
            {
                rizMetreUsers.Tedad = Tedad;


                decimal? Tool = rizMetreUsers.Tool;
                decimal? Arz = rizMetreUsers.Arz;
                decimal? Ertefa = rizMetreUsers.Ertefa;
                decimal? Vazn = rizMetreUsers.Vazn;

                decimal dMeghdarJoz = 0;
                if (Tedad == null && Tool == null && Arz == null && Ertefa == null && Vazn == null)
                    dMeghdarJoz = 0;
                else
                    dMeghdarJoz += (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                    (Arz == null ? 1 : Arz.Value) * (Ertefa == null ? 1 : Ertefa.Value) * (Vazn == null ? 1 : Vazn.Value);

                rizMetreUsers.MeghdarJoz = dMeghdarJoz;
                _context.SaveChanges();

                return new JsonResult(dMeghdarJoz);
            }

            return new JsonResult("NOK");
        }


        public JsonResult SaveInfoBoard([FromBody] BoardDataDto request)
        {
            try
            {
                Guid BaravordId = request.BaravordId;

                string strMainSharh = "";
                int Shape = request.Shape;

                strMainSharh = " تابلو اطلاعاتی، ";

                int Material = int.Parse(request.Items.First(x => x.Name.ToLower() == "materialinfoboard").Value);
                int print = int.Parse(request.Items.First(x => x.Name.ToLower() == "printinfoboard").Value);
                decimal thikness = decimal.Parse(request.Items.First(x => x.Name.ToLower() == "zakhamatinfoboard").Value);
                string Sharh = request.Sharh;
                decimal Arz = request.Arz / 1000;
                decimal Ertefa = request.Ertefa / 1000;
                long Tedad = request.Tedad;
                bool UsePOP = request.UsePOP;
                decimal? PercentPrintPOP = request.PercentPrintPOP / 100;

                if (Material == 1)
                {
                    strMainSharh += "ورق روغنی، ";
                }
                else if (Material == 2)
                {
                    strMainSharh += "ورق گالوانیزه، ";

                }


                Guid barAvordAddedBoardId = new Guid();
                clsBarAvordAddedBoard barAvordAddedBoard1 = new clsBarAvordAddedBoard
                {
                    Material = Material,
                    PrintType = print,
                    Thickness = thikness,
                    BarAvordId = BaravordId,
                    BoardType = Shape,
                    Tedad = Tedad,
                    Sharh = Sharh,
                    Arz = Arz,
                    Ertefa = Ertefa,
                    UsePOP = UsePOP,
                    PercentPrintPOP = PercentPrintPOP
                };
                _context.BarAvordAddedBoards.Add(barAvordAddedBoard1);
                _context.SaveChanges();

                barAvordAddedBoardId = barAvordAddedBoard1.ID;

                //decimal Area = 0;
                //Area = (Arz * Ertefa) / 1000000;

                long Shomareh = 0;
                clsRizMetreUsers? RM = _context.RizMetreUserses.Include(x => x.FB).OrderByDescending(x => x.Shomareh).FirstOrDefault(x => x.FB.BarAvordId == BaravordId);
                if (RM != null)
                {
                    Shomareh = RM.Shomareh;
                }
                Shomareh++;

                ///ابتدا آیتم را بیس را پیدا میکنیم
                ///
                clsBoardItems? boardItems = _context.BoardItemses.FirstOrDefault(x => x.Shape == "5" && x.Material == Material && x.PrintType == print && x.Thikness == thikness && x.AddedItemType == true);
                string strMainAddedItem = "";
                if (boardItems != null)
                {
                    string strSharh = "";

                    strSharh += " ضخامت " + thikness;

                    strMainSharh += " شبرنگ " + print + " ،";

                    strMainAddedItem = boardItems.AddedItem;

                    clsFB? FBUsers = _context.FBs.FirstOrDefault(x => x.BarAvordId == BaravordId && x.Shomareh == strMainAddedItem);
                    Guid FBId = new Guid();
                    if (FBUsers == null)
                    {
                        clsFB FBSave = new clsFB();
                        FBSave.BarAvordId = BaravordId;
                        FBSave.Shomareh = strMainAddedItem;
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
                    RizMetreUsers.Sharh = Sharh;

                    RizMetreUsers.Tedad = Tedad;

                    RizMetreUsers.Tool = null;

                    RizMetreUsers.Arz = Arz;

                    RizMetreUsers.Ertefa = Ertefa;

                    RizMetreUsers.Vazn = null;

                    RizMetreUsers.Des = strMainSharh + strSharh;
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

                    ///ریزمتره درج شده به ریزمتره های درج شده در تابلو اضافه میشود
                    clsRizMetreForBarAvordAddedBoard RizMetreForBarAvordAddedBoard = new clsRizMetreForBarAvordAddedBoard
                    {
                        BarAvordAddedBoardId = barAvordAddedBoardId,
                        InsertDateTime = DateTime.Now,
                        RizMetreId = RizMetreUsers.ID,
                        IsDeleted = false
                    };
                    _context.RizMetreForBarAvordAddedBoards.Add(RizMetreForBarAvordAddedBoard);

                    _context.SaveChanges();
                }
                else
                {
                    return new JsonResult("NoItem");
                }

                //long Shomareh = 1;

                ///اضافه بها
                ///
                clsBoardItems? boardItemsAdded = _context.BoardItemses.FirstOrDefault(x => x.Shape == "5" && x.Material == Material && x.PrintType == print && x.Thikness == thikness && x.AddedItemType == false);
                if (boardItemsAdded != null)
                {
                    string strSharh = "";

                    strSharh += " ضخامت " + thikness;

                    strMainSharh += " شبرنگ " + print + " ،";

                    strMainAddedItem = boardItemsAdded.AddedItem;

                    clsFB? FBUsers = _context.FBs.FirstOrDefault(x => x.BarAvordId == BaravordId && x.Shomareh == strMainAddedItem);
                    Guid FBId = new Guid();
                    if (FBUsers == null)
                    {
                        clsFB FBSave = new clsFB();
                        FBSave.BarAvordId = BaravordId;
                        FBSave.Shomareh = strMainAddedItem;
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
                    RizMetreUsers.Sharh = Sharh;

                    RizMetreUsers.Tedad = Tedad;

                    RizMetreUsers.Tool = null;

                    RizMetreUsers.Arz = Arz;

                    RizMetreUsers.Ertefa = Ertefa;

                    RizMetreUsers.Vazn = null;

                    RizMetreUsers.Des = strMainSharh + strSharh;
                    RizMetreUsers.FBId = FBId;
                    RizMetreUsers.OperationsOfHamlId = 1;
                    RizMetreUsers.Type = "2";
                    RizMetreUsers.ForItem = strMainAddedItem;
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

                    ///ریزمتره درج شده به ریزمتره های درج شده در تابلو اضافه میشود
                    clsRizMetreForBarAvordAddedBoard RizMetreForBarAvordAddedBoard = new clsRizMetreForBarAvordAddedBoard
                    {
                        BarAvordAddedBoardId = barAvordAddedBoardId,
                        InsertDateTime = DateTime.Now,
                        RizMetreId = RizMetreUsers.ID,
                        IsDeleted = false
                    };
                    _context.RizMetreForBarAvordAddedBoards.Add(RizMetreForBarAvordAddedBoard);

                    _context.SaveChanges();
                }

                //در صورتی که از
                //POP
                //استفاده شده باشد
                if (UsePOP)
                {
                    clsBoardItems? boardItems1 = _context.BoardItemses.FirstOrDefault(x => x.Shape == "5" && x.PrintType == 4 && x.PrintTypeBase == print && x.AddedItemType == false);
                    strMainSharh += " استفاده از شبرنگ POP بجای " + print + " ،";

                    if (boardItems1 != null)
                    {
                        string strAddedItem = "";
                        strAddedItem = boardItems1.AddedItem;

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
                        RizMetreUsers.Sharh = Sharh;

                        RizMetreUsers.Tedad = Tedad;

                        RizMetreUsers.Tool = null;

                        RizMetreUsers.Arz = Arz;

                        RizMetreUsers.Ertefa = Ertefa;

                        RizMetreUsers.Vazn = PercentPrintPOP;

                        RizMetreUsers.Des = strMainSharh + strMainSharh;
                        RizMetreUsers.FBId = FBId;
                        RizMetreUsers.OperationsOfHamlId = 1;
                        RizMetreUsers.Type = "2";
                        RizMetreUsers.ForItem = strMainAddedItem;
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

                        ///ریزمتره درج شده به ریزمتره های درج شده در تابلو اضافه میشود
                        clsRizMetreForBarAvordAddedBoard RizMetreForBarAvordAddedBoard = new clsRizMetreForBarAvordAddedBoard
                        {
                            BarAvordAddedBoardId = barAvordAddedBoardId,
                            InsertDateTime = DateTime.Now,
                            RizMetreId = RizMetreUsers.ID,
                            IsDeleted = false
                        };
                        _context.RizMetreForBarAvordAddedBoards.Add(RizMetreForBarAvordAddedBoard);

                        _context.SaveChanges();
                    }
                }
                return new JsonResult("OK");

            }
            catch (Exception)
            {
                return new JsonResult("NOK");
            }
        }

        public JsonResult UpdateInfoBoard([FromBody] BoardDataDto request)
        {

            try
            {
                Guid BaravordId = request.BaravordId;
                Guid barAvordAddedBoardId = request.barAvordAddedBoardId;

                string strMainSharh = "";

                int Shape = request.Shape;

                strMainSharh = " تابلو اطلاعاتی، ";


                int Material = int.Parse(request.Items.First(x => x.Name.ToLower() == "materialinfoboard").Value);
                int print = int.Parse(request.Items.First(x => x.Name.ToLower() == "printinfoboard").Value);
                decimal thikness = decimal.Parse(request.Items.First(x => x.Name.ToLower() == "zakhamatinfoboard").Value);

                decimal Arz = request.Arz / 1000;
                decimal Ertefa = request.Ertefa / 1000;

                string Sharh = request.Sharh;
                long Tedad = request.Tedad;
                bool UsePOP = request.UsePOP;
                decimal? PercentPrintPOP = request.PercentPrintPOP / 100;

                if (Material == 1)
                {
                    strMainSharh += "ورق روغنی، ";
                }
                else if (Material == 2)
                {
                    strMainSharh += "ورق گالوانیزه، ";

                }

                long Shomareh = 0;
                clsRizMetreUsers? RM = _context.RizMetreUserses.Include(x => x.FB).OrderByDescending(x => x.Shomareh).FirstOrDefault(x => x.FB.BarAvordId == BaravordId);
                if (RM != null)
                {
                    Shomareh = RM.Shomareh;
                }
                Shomareh++;

                clsBarAvordAddedBoard? barAvordAddedBoard = _context.BarAvordAddedBoards.FirstOrDefault(x => x.ID == barAvordAddedBoardId);
                if (barAvordAddedBoard != null)
                {
                    barAvordAddedBoard.Material = Material;
                    barAvordAddedBoard.PrintType = print;
                    barAvordAddedBoard.Thickness = thikness;
                    barAvordAddedBoard.BoardType = Shape;
                    barAvordAddedBoard.Tedad = Tedad;
                    barAvordAddedBoard.Sharh = Sharh;

                    barAvordAddedBoard.Arz = Arz;
                    barAvordAddedBoard.Ertefa = Ertefa;

                    barAvordAddedBoard.UsePOP = UsePOP;
                    barAvordAddedBoard.PercentPrintPOP = PercentPrintPOP;

                    List<clsRizMetreForBarAvordAddedBoard> lstRizMetreForBarAvordAddedBoard =
                        _context.RizMetreForBarAvordAddedBoards.Where(x => x.BarAvordAddedBoardId == barAvordAddedBoard.ID).ToList();

                    List<Guid> lstRizMetreIds = lstRizMetreForBarAvordAddedBoard.Select(x => x.RizMetreId).ToList();
                    List<clsRizMetreUsers> lstRizMetre = _context.RizMetreUserses.Where(x => lstRizMetreIds.Contains(x.ID)).ToList();

                    _context.RizMetreUserses.RemoveRange(lstRizMetre);
                    _context.SaveChanges();
                }
                else
                {
                    return new JsonResult("NOK");
                }

                //decimal Area = 0;
                //Area = (Arz * Ertefa)/1000000;

                ///ابتدا آیتم را بیس را پیدا میکنیم
                ///

                clsBoardItems? boardItems = _context.BoardItemses.FirstOrDefault(x => x.Shape == "5" && x.Material == Material && x.PrintType == print && x.Thikness == thikness && x.AddedItemType == true);
                string strMainAddedItem = "";
                if (boardItems != null)
                {
                    string strSharh = "";

                    strSharh += " ضخامت " + thikness;

                    strMainSharh += " شبرنگ " + print + " ،";

                    strMainAddedItem = boardItems.AddedItem;

                    clsFB? FBUsers = _context.FBs.FirstOrDefault(x => x.BarAvordId == BaravordId && x.Shomareh == strMainAddedItem);
                    Guid FBId = new Guid();
                    if (FBUsers == null)
                    {
                        clsFB FBSave = new clsFB();
                        FBSave.BarAvordId = BaravordId;
                        FBSave.Shomareh = strMainAddedItem;
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
                    RizMetreUsers.Sharh = Sharh;

                    RizMetreUsers.Tedad = Tedad;

                    RizMetreUsers.Tool = null;

                    RizMetreUsers.Arz = Arz;

                    RizMetreUsers.Ertefa = Ertefa;

                    RizMetreUsers.Vazn = null;

                    RizMetreUsers.Des = strMainSharh + strSharh;
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

                    ///ریزمتره درج شده به ریزمتره های درج شده در تابلو اضافه میشود
                    clsRizMetreForBarAvordAddedBoard RizMetreForBarAvordAddedBoard = new clsRizMetreForBarAvordAddedBoard
                    {
                        BarAvordAddedBoardId = barAvordAddedBoardId,
                        InsertDateTime = DateTime.Now,
                        RizMetreId = RizMetreUsers.ID,
                        IsDeleted = false
                    };
                    _context.RizMetreForBarAvordAddedBoards.Add(RizMetreForBarAvordAddedBoard);

                    _context.SaveChanges();
                }


                ///اضافه بها
                ///
                clsBoardItems? boardItemsAdded = _context.BoardItemses.FirstOrDefault(x => x.Shape == "5" && x.Material == Material && x.PrintType == print && x.Thikness == thikness && x.AddedItemType == false);
                if (boardItemsAdded != null)
                {
                    string strSharh = "";

                    strSharh += " ضخامت " + thikness;

                    strMainSharh += " شبرنگ " + print + " ،";

                    strMainAddedItem = boardItemsAdded.AddedItem;

                    clsFB? FBUsers = _context.FBs.FirstOrDefault(x => x.BarAvordId == BaravordId && x.Shomareh == strMainAddedItem);
                    Guid FBId = new Guid();
                    if (FBUsers == null)
                    {
                        clsFB FBSave = new clsFB();
                        FBSave.BarAvordId = BaravordId;
                        FBSave.Shomareh = strMainAddedItem;
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
                    RizMetreUsers.Sharh = Sharh;

                    RizMetreUsers.Tedad = Tedad;

                    RizMetreUsers.Tool = null;

                    RizMetreUsers.Arz = Arz;

                    RizMetreUsers.Ertefa = Ertefa;

                    RizMetreUsers.Vazn = null;

                    RizMetreUsers.Des = strMainSharh + strSharh;
                    RizMetreUsers.FBId = FBId;
                    RizMetreUsers.OperationsOfHamlId = 1;
                    RizMetreUsers.Type = "2";
                    RizMetreUsers.ForItem = strMainAddedItem;
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

                    ///ریزمتره درج شده به ریزمتره های درج شده در تابلو اضافه میشود
                    clsRizMetreForBarAvordAddedBoard RizMetreForBarAvordAddedBoard = new clsRizMetreForBarAvordAddedBoard
                    {
                        BarAvordAddedBoardId = barAvordAddedBoardId,
                        InsertDateTime = DateTime.Now,
                        RizMetreId = RizMetreUsers.ID,
                        IsDeleted = false
                    };
                    _context.RizMetreForBarAvordAddedBoards.Add(RizMetreForBarAvordAddedBoard);

                    _context.SaveChanges();
                }

                //در صورتی که از
                //POP
                //استفاده شده باشد
                if (UsePOP)
                {
                    clsBoardItems? boardItems1 = _context.BoardItemses.FirstOrDefault(x => x.Shape == "5" && x.PrintType == 4 && x.PrintTypeBase == print && x.AddedItemType == false);
                    strMainSharh += " استفاده از شبرنگ POP بجای " + print + " ،";

                    if (boardItems1 != null)
                    {
                        string strAddedItem = "";
                        strAddedItem = boardItems1.AddedItem;

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
                        RizMetreUsers.Sharh = Sharh;

                        RizMetreUsers.Tedad = Tedad;

                        RizMetreUsers.Tool = null;

                        RizMetreUsers.Arz = Arz;

                        RizMetreUsers.Ertefa = Ertefa;

                        RizMetreUsers.Vazn = PercentPrintPOP;

                        RizMetreUsers.Des = strMainSharh + strMainSharh;
                        RizMetreUsers.FBId = FBId;
                        RizMetreUsers.OperationsOfHamlId = 1;
                        RizMetreUsers.Type = "2";
                        RizMetreUsers.ForItem = strMainAddedItem;
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

                        ///ریزمتره درج شده به ریزمتره های درج شده در تابلو اضافه میشود
                        clsRizMetreForBarAvordAddedBoard RizMetreForBarAvordAddedBoard = new clsRizMetreForBarAvordAddedBoard
                        {
                            BarAvordAddedBoardId = barAvordAddedBoardId,
                            InsertDateTime = DateTime.Now,
                            RizMetreId = RizMetreUsers.ID,
                            IsDeleted = false
                        };
                        _context.RizMetreForBarAvordAddedBoards.Add(RizMetreForBarAvordAddedBoard);

                        _context.SaveChanges();
                    }
                }
                return new JsonResult("OK");

            }
            catch (Exception)
            {
                return new JsonResult("NOK");
            }
        }

    }
}
