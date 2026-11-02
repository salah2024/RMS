using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Models.Entity;
using System.Security;

namespace RMS.Controllers.Permissions
{
    public class PermissionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PermissionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Permissions?q=
        public async Task<IActionResult> Index(string? q)
        {
            var query = _context.Permissions.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(q))
            {
                q = q.Trim();
                query = query.Where(p =>
                    p.Key.Contains(q) ||
                    p.Title.Contains(q) ||
                    (p.Group != null && p.Group.Contains(q)));
            }

            var items = await query
                .OrderBy(p => p.Group)
                .ThenBy(p => p.Key)
                .ToListAsync();

            ViewBag.Q = q;

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("Index", items);

            return View(items);
        }

        // GET: /Permissions/Create
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string baseKey, string title, string? group)
        {
            baseKey = (baseKey ?? "").Trim();

            if (string.IsNullOrWhiteSpace(baseKey))
                ModelState.AddModelError("baseKey", "Key الزامی است.");

            if (!ModelState.IsValid)
                return View();

            var keys = new[]
            {
        $"{baseKey}.View",
        //$"{baseKey}.Create",
        //$"{baseKey}.Edit",
        //$"{baseKey}.Delete"
    };

            var exists = await _context.Permissions
                .AnyAsync(x => keys.Contains(x.Key));

            if (exists)
            {
                ModelState.AddModelError("baseKey", "برای این Key قبلاً Permission ثبت شده است.");
                return View();
            }

            foreach (var key in keys)
            {
                _context.Permissions.Add(new clsPermission
                {
                    Key = key,
                    Group = group,
                    Title = key switch
                    {
                        var k when k.EndsWith(".View") => $"مشاهده {title}",
                        //var k when k.EndsWith(".Create") => $"ایجاد {title}",
                        //var k when k.EndsWith(".Edit") => $"ویرایش {title}",
                        //var k when k.EndsWith(".Delete") => $"حذف {title}",
                        _ => title
                    },
                    IsActive = true
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Permissions/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _context.Permissions.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return NotFound();
            return View(entity);
        }

        // POST: /Permissions/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(clsPermission input)
        {
            input.Key = (input.Key ?? "").Trim();
            input.Title = (input.Title ?? "").Trim();
            input.Group = string.IsNullOrWhiteSpace(input.Group) ? null : input.Group.Trim();

            if (string.IsNullOrWhiteSpace(input.Key))
                ModelState.AddModelError(nameof(clsPermission.Key), "Key الزامی است.");

            if (string.IsNullOrWhiteSpace(input.Title))
                ModelState.AddModelError(nameof(clsPermission.Title), "Title الزامی است.");

            if (!ModelState.IsValid)
                return View(input);

            var entity = await _context.Permissions.FirstOrDefaultAsync(x => x.Id == input.Id);
            if (entity == null) return NotFound();

            var keyExists = await _context.Permissions.AnyAsync(x => x.Key == input.Key && x.Id != input.Id);
            if (keyExists)
            {
                ModelState.AddModelError(nameof(clsPermission.Key), "این Key قبلاً ثبت شده است.");
                return View(input);
            }

            entity.Key = input.Key;
            entity.Title = input.Title;
            entity.Group = input.Group;
            entity.IsActive = input.IsActive;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Permissions/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Permissions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return NotFound();
            return View(entity);
        }

        // POST: /Permissions/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entity = await _context.Permissions.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return NotFound();

            _context.Permissions.Remove(entity);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
