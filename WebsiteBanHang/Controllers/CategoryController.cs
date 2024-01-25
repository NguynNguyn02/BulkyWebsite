using Microsoft.AspNetCore.Mvc;
using WebsiteBanHang.Data;
using WebsiteBanHang.Models;

namespace WebsiteBanHang.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> items= _db.Categories.OrderBy(x=>x.DisplayOrder).ToList();
            return View(items);
        }
        public IActionResult Create() { 
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category model)
        {
            if (model.Name == model.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name cannot similar DisplayOrder");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(model);
                _db.SaveChanges();
                TempData["Success"] = "Success Create";

                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            Category? item = _db.Categories.Find(id);
            if (item == null)   
            {
                return NotFound();
            }
            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(Category model)
        {
            if(ModelState.IsValid)
            {
                _db.Categories.Update(model);
                _db.SaveChanges();
                TempData["Success"] = "Success Edit";

                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? item = _db.Categories.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            
            Category? item = _db.Categories.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(item);
            _db.SaveChanges();
            TempData["Success"] = "Success Delete";
            return RedirectToAction("Index");
            
            
        }
    }
}
