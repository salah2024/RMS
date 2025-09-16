using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Models.Dto.TreeDto;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Dastyar
{
    public class DastyarController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<IActionResult> Index()
        {
            var operations = await GetOperations(); // متد برای گرفتن عملیات‌ها
            var itemsKholaseh = await GetItemsKholaseh(); // متد برای گرفتن خلاصه‌ها

            ViewBag.Operations = operations;
            ViewBag.ItemsKholaseh = itemsKholaseh;

            return View();
        }

        private async Task<List<OperationForTreeDto>> GetOperations()
        {

            List<OperationForTreeDto> lstOperationForTree = await _context.Operations.Include(x => x.Operation_ItemsFBs).Select(x => new OperationForTreeDto
            {
                Id = x.Id,
                ParentId = x.ParentId,
                OperationName = x.OperationName,
                FunctionCall = x.FunctionCall == null ? "" : x.FunctionCall,
                ItemsFBShomareh = x.Operation_ItemsFBs != null ? x.Operation_ItemsFBs.ItemsFBShomareh : "",
            }).ToListAsync();
            return lstOperationForTree;
            // شبیه‌سازی داده‌ها
            //return new List<OperationForTreeDto>
            //{
            //    new OperationForTreeDto { Id = "1", ParentId = "0", OperationName = "Operation 1", FunctionCall = "function1", ItemsFBShomareh = "" },
            //    new OperationForTreeDto { Id = "2", ParentId = "0", OperationName = "Operation 2", FunctionCall = "", ItemsFBShomareh = "" },
            //    new OperationForTreeDto { Id = "3", ParentId = "0", OperationName = "Operation 3", FunctionCall = "", ItemsFBShomareh = "FB123" },
            //    new OperationForTreeDto { Id = "4", ParentId = "2", OperationName = "Operation 4", FunctionCall = "", ItemsFBShomareh = "" },
            //};
        }

        private async Task<List<ItemKholasehFosulForTreeDto>> GetItemsKholaseh()
        {
            List<ItemKholasehFosulForTreeDto> ItemKholasehFosulForTree =
                await _context.FehrestBahas.Where(x => x.NoeFB == NoeFehrestBaha.RahoBand && x.Sal == 1397).Select(x=> new ItemKholasehFosulForTreeDto
                {
                    Sharh=x.Sharh,
                    Shomareh=x.Shomareh
                }).ToListAsync();
            return ItemKholasehFosulForTree;
            // شبیه‌سازی داده‌ها
            //return new List<ItemKholasehFosulForTreeDto>
            //{
            //    new ItemKholasehFosulForTreeDto { Shomareh = "FB123", Sharh = "Description for FB123" }
            // };
        }
    }
}
