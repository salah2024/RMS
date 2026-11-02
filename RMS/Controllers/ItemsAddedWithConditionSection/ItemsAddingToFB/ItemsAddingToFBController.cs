using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Models.Entity;

namespace RMS.Controllers.ItemsAddedWithConditionSection.ItemsAddingToFB;

public class ItemsAddingToFBController : Controller
{
    private readonly ApplicationDbContext _context;


    public ItemsAddingToFBController(
        ApplicationDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll(long itemsHasConditionConditionContextId)
    {

        var list = await (

            from a in _context.ItemsAddingToFBs

            where a.ItemsHasCondition_ConditionContextId ==
                  itemsHasConditionConditionContextId

            orderby a.Id

            select new
            {
                a.Id,

                a.ItemsHasCondition_ConditionContextId,

                a.AddedItems,

                a.Condition,

                a.FinalWorking,

                a.ConditionType,

                a.DesOfAddingItems,

                a.UseItemForAdd,

                a.FieldsAdding,

                a.CharacterPlus
            }

            ).ToListAsync();

        return Json(list);

    }



    [HttpGet]
    public async Task<IActionResult> Get(long id)
    {

        var data =
            await _context.ItemsAddingToFBs

            .FirstOrDefaultAsync(x => x.Id == id);

        if (data == null)
            return NotFound();

        return Json(new
        {

            data.Id,

            data.ItemsHasCondition_ConditionContextId,

            data.AddedItems,

            data.Condition,

            data.FinalWorking,

            data.ConditionType,

            data.DesOfAddingItems,

            data.UseItemForAdd,

            data.FieldsAdding,

            data.CharacterPlus

        });

    }


    [HttpPost]
    public async Task<IActionResult> Save(
        [FromBody] clsItemsAddingToFB model)
    {
        try
        {

        model.Id = 0;

        model.Year = model.Year;

        model.NoeFB = model.NoeFB;

        _context.ItemsAddingToFBs.Add(model);

        await _context.SaveChangesAsync();

        return Ok();
        }
        catch (Exception)
        {

            throw;
        }

    }

    [HttpPost]
    public async Task<IActionResult> Update(
            [FromBody] clsItemsAddingToFB model)
    {

        var entity =
            await _context.ItemsAddingToFBs

            .FirstOrDefaultAsync(x => x.Id == model.Id);

        if (entity == null)
            return NotFound();

        entity.AddedItems = model.AddedItems;

        entity.Condition = model.Condition;

        entity.FinalWorking = model.FinalWorking;

        entity.ConditionType = model.ConditionType;

        entity.DesOfAddingItems = model.DesOfAddingItems;

        entity.UseItemForAdd = model.UseItemForAdd;

        entity.FieldsAdding = model.FieldsAdding;

        entity.CharacterPlus = model.CharacterPlus;

        await _context.SaveChangesAsync();

        return Ok();

    }



    [HttpPost]
    public async Task<IActionResult> Delete(long id)
    {

        var entity =
            await _context.ItemsAddingToFBs

            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
            return NotFound();

        _context.ItemsAddingToFBs.Remove(entity);

        await _context.SaveChangesAsync();

        return Ok();

    }






}
