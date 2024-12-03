using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
	public class ProductController : Controller
	{
		IProductHelper _productHelper;

		public ProductController(IProductHelper productHelper)
		{
			_productHelper = productHelper;
		}

		// GET: Product
		public IActionResult Index()
		{
			List<ProductViewModel> products = _productHelper.GetAllProducts();
			return View(products);
		}

		// GET: Product/Details/5
		public IActionResult Details(int id)
		{
			ProductViewModel product = _productHelper.GetProductById(id);
			if (product == null)
			{
				return NotFound();
			}
			return View(product);
		}

		// GET: Product/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Product/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(ProductViewModel product)
		{
			if (ModelState.IsValid)
			{
				var createdProduct = _productHelper.AddProduct(product);
				if (createdProduct != null)
				{
					return RedirectToAction(nameof(Index));
				}
				ModelState.AddModelError("", "Failed to create product.");
			}
			return View(product);
		}

		// GET: Product/Edit/5
		public IActionResult Edit(int id)
		{
			ProductViewModel product = _productHelper.GetProductById(id);
			if (product == null)
			{
				return NotFound();
			}
			return View(product);
		}

		// POST: Product/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(ProductViewModel product)
		{
			if (ModelState.IsValid)
			{
				var updatedProduct = _productHelper.UpdateProduct(product);
				if (updatedProduct != null)
				{
					return RedirectToAction(nameof(Index));
				}
				ModelState.AddModelError("", "Failed to update product.");
			}
			return View(product);
		}

		// GET: Product/Delete/5
		public IActionResult Delete(int id)
		{
			ProductViewModel product = _productHelper.GetProductById(id);
			if (product == null)
			{
				return NotFound();
			}
			return View(product);
		}

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
