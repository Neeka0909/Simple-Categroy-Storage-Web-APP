using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using TestAppWeb.Data;
using TestAppWeb.Models;

namespace TestAppWeb.Controllers
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
            IEnumerable<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] // prevent cross script attack,  validate token
        public IActionResult Create (Category obj)
        {
            //custom Validations
            if (obj.Name== obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The Display Order cannot exactly match the name"); //CustomError - error key if you want to display error insode the name property change CustomError to name
               // ModelState.AddModelError("name", "The Display Order cannot exactly match the name");
            }

            //asp-validation
            if(ModelState.IsValid)
            {
                _db.Categories.Add(obj); //send data to data base
                _db.SaveChanges(); //save database data
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //EDIT
        //GET
        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = _db.Categories.Find(id);
            // var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
            // var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if(categoryFromDb== null)
            {
                return NotFound();
            }
            return View(categoryFromDb);


        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] // prevent cross script attack,  validate token
        public IActionResult Edit(Category obj)
        {
            //custom Validations
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The Display Order cannot exactly match the name"); //CustomError - error key if you want to display error insode the name property change CustomError to name
                                                                                                            // ModelState.AddModelError("name", "The Display Order cannot exactly match the name");
            }

            //asp-validation
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj); //send data to data base
                _db.SaveChanges(); //save database data
                TempData["success"] = "Category Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Delete 
        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = _db.Categories.Find(id);
            // var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
            // var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);


        }
        
        //POST
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken] // prevent cross script attack,  validate token
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.Find(id);
            if (obj == null) {
                return NotFound();
            }
             _db.Categories.Remove(obj); //send data to data base
             _db.SaveChanges(); //save database data
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");

            
        }

    }
}
