using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEnd.Controllers
{
    public class ProductController : Controller
    {
        IProductHelper _productHelper;
        ICategoryHelper _categoryHelper;

        public ProductController(IProductHelper productHelper, ICategoryHelper categoryHelper)
        {
            _productHelper = productHelper;
            _categoryHelper = categoryHelper;
        }

        public IActionResult Index()
        {
            List<ProductViewModel> products = _productHelper.GetAllProducts();
            return View(products);
        }

        public IActionResult Details(int id)
        {
            ProductViewModel product = _productHelper.GetProductById(id);
            var category = _categoryHelper.GetCategoryById(product.CategoryId);
            product.CategoryName = category.Name;

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public IActionResult Create()
        {
            var categories = _categoryHelper.GetAllCategories();
            ViewData["CategoryList"] = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductViewModel product, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                var createdProduct = _productHelper.AddProduct(product, photo);
                if (createdProduct != null)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Failed to create product.");
            }

            return View(product);
        }

        public IActionResult Edit(int id)
        {
            var product = _productHelper.GetProductById(id);
            var categories = _categoryHelper.GetAllCategories();
            ViewData["CategoryList"] = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductViewModel product, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                var updatedProduct = _productHelper.UpdateProduct(product, photo);
                if (updatedProduct != null)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Failed to update service.");
            }

            return View(product);
        }

        public IActionResult Delete(int id)
        {
            var product = _productHelper.GetProductById(id);
            var category = _categoryHelper.GetCategoryById(product.CategoryId);
            product.CategoryName = category.Name; 
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ProductViewModel product)
        {
            try
            {
                _productHelper.DeleteProduct(product.Id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
