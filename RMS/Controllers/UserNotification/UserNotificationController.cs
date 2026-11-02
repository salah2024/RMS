using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.UserNotification.Dto;

namespace RMS.Controllers.UserNotification;

public class UserNotificationController(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : Controller
{
    private readonly ApplicationDbContext _context = context;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    [HttpPost]
    public async Task<JsonResult> GetUserNotification([FromBody] GetUserNotificationDto request)
    {
        string BaravordId = request.BarAvordId;
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
            return Json(new List<object>());

        Guid userId = Guid.Parse(user.Id);

        Guid? gBaravordId = null;
        if (BaravordId!="")
        {
            gBaravordId = Guid.Parse(BaravordId);
        }

        var notifications = await _context.UserNotifications
            .Where(x => x.UserId == userId && (x.BarAvordId == null || x.BarAvordId == gBaravordId))
            .OrderByDescending(x => x.InsertDateTime)
            .Select(x => new
            {
                x.ID,
                x.Title,
                x.ShortDes,
                x.Description,
                x.RelateLink,
                x.IsRead,
            }).ToListAsync();

        if (notifications.Count != 0)
        {
            return Json(notifications);

        }
        else
            return Json(new List<object>());
    }

    [HttpPost]
    public async Task<JsonResult> GetUnreadCount([FromBody] GetUserNotificationDto request)
    {
        string BaravordId = request.BarAvordId;

        var user = await _userManager.GetUserAsync(User);

        Guid? gBaravordId = null;
        if (BaravordId != "")
        {
            gBaravordId = Guid.Parse(BaravordId);
        }

        if (user == null)
            return Json(new List<object>());

        Guid userId = Guid.Parse(user.Id);

        var notifications = await _context.UserNotifications
            .CountAsync(x => x.UserId == userId && (x.BarAvordId == null || x.BarAvordId == gBaravordId) && x.IsRead == false);
        if (notifications != 0)
        {
            return Json(notifications);

        }
        else
            return Json(0);
    }
}
