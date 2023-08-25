using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlacesTodo.Context;
using PlacesTodo.Models;

namespace PlacesTodo.Controllers
{
    public class GenericController<T> : Controller where T : class
    {
        protected readonly AppDBContext _context;
        protected readonly UserManager<User> _userManager;
        public GenericController(AppDBContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //public ActionResult Create(int parentId, Func<T, bool> predicate)
        //{}
        public virtual ActionResult Details(int id)
        {
            T item = _context.Set<T>().Find(id);
            return View(item);
        }


        // POST: FoldersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Create(T t)
        {
            if (ModelState.IsValid)
            {
                _context.Set<T>().Add(t);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Folders");
            }
            return View(t);
        }

        //     GET: FoldersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(GetItem(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Edit(T t)
        {
            if (ModelState.IsValid)
            {
                _context.Update(t);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Folders");
            }
            return View(t);
        }

        public ActionResult Delete(int id)
        {
            return View(GetItem(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Delete(int id, bool diff = false)
        {
            T t = _context.Set<T>().Find(id);
            _context.Set<T>().Remove(t);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Folders");
        }

        private T GetItem(int id)
        {
            return _context.Set<T>().Find(id);
        }
    }
}
