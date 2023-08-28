using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlacesTodo.Context;
using PlacesTodo.Models;

namespace PlacesTodo.Controllers
{
    [Authorize]
    public class TasksController : GenericController<Item>
    {
        public TasksController(AppDBContext context, UserManager<User> userManager) : base(context, userManager) {}

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int parentId)
        {
            ViewBag.ParentName = _context.Folders.Where(v => v.Id == parentId).FirstOrDefault().Name;

            return View(new Item { ParentId = parentId });
        }
    }
}
