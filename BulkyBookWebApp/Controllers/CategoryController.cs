using BulkyBookWebApp.Data;
using BulkyBookWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            this._db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = this._db.Categories;
            return View(objCategoryList);
        }

        // Get
        public IActionResult Create()
        {
            return View();
        }

        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayError cannot exactly match the 'Name'");
            }

            if (ModelState.IsValid)
            {
                this._db.Categories.Add(obj);
                await this._db.SaveChangesAsync();

                TempData["success"] = "Category created successfully!";
                return RedirectToAction("Index");
            }

            return View(obj);
            
        }

        // Get
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = this._db.Categories.Find(id);
            //var categoryFromDbFirst = this._db.Categories.FirstOrDefault(u => u.Id == id);
            //var categoryFromDbSingle = this._db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayError cannot exactly match the 'Name'");
            }

            if (ModelState.IsValid)
            {
                this._db.Categories.Update(obj);
                this._db.SaveChanges();

                TempData["success"] = "Category updated successfully!";
                return RedirectToAction("Index");
            }

            return View(obj);

        }

        // Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = this._db.Categories.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        // Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = this._db.Categories.Find(id);

            if (obj == null)
            {
                return NotFound();

            }
            
            this._db.Categories.Remove(obj);
            this._db.SaveChanges();

            TempData["success"] = "Category deleted successfully!";
            return RedirectToAction("Index");

        }
    }
}
