using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlacesTodo.Context;
using PlacesTodo.Models;

namespace PlacesTodo.Controllers
{
    [Authorize]
    public class FoldersController : GenericController<Folder>
    {
        public FoldersController(AppDBContext context, UserManager<User> userManager) : base(context, userManager) { }

        // GET: FoldersController
        public async Task<IActionResult> Index()
        {
            var folders = _context.Folders.Include(f => f.Children).Include(f => f.Tasks).ToList();
            var user = await _userManager.GetUserAsync(HttpContext.User);

            return View(folders.Where(v => v.ParentId == null && v.Id == user.FolderId).FirstOrDefault());
        }

        // GET: FoldersController/Create
        public ActionResult Create(int parentId)
        {
            ViewBag.ParentName = _context.Folders.Where(v => v.Id == parentId).FirstOrDefault().Name;
            
            return View(new Folder { ParentId = parentId });
        }

        //POST: FoldersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Delete(int id, bool diff = false)
        {
            DeleteWithChildren(id);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void DeleteWithChildren(int id)
        {
            Folder folder = _context.Folders.FirstOrDefault(v => v.Id == id);

            if (folder == null) return;

            List<Folder> children = _context.Folders.Where(v => v.ParentId == id).ToList();
            if (children != null && children.Count > 0)
            {
                foreach (Folder child in children)
                {
                    DeleteWithChildren(child.Id);
                }
            }
            _context.Remove(folder);
        }
    }
}
