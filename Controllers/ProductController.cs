using LemonHiveEcommerce.DTOs;
using LemonHiveEcommerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LemonHiveEcommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartItemService _cartItemService;

        public ProductController(IProductService service, ICartItemService cartItemService)
        {
            _productService = service;
            _cartItemService = cartItemService;
        }

        public async Task<IActionResult> Index(string search, int page = 1)
        {
            int pageSize = 5;
            var result = await _productService.GetPagedProductsAsync(search, page, pageSize);

            ViewData["Search"] = search;
            ViewData["CurrentPage"] = result.PageIndex;
            ViewData["TotalPages"] = (int)Math.Ceiling((double)result.TotalCount / pageSize);

            var cartItems = await _cartItemService.GetAllAsync();
            int cartItemCount = cartItems?.Sum(ci => ci.Qty) ?? 0;

            ViewData["CartItemCount"] = cartItemCount;

            return View(result.Items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDto product)
        {
            if (!ModelState.IsValid)
                return View(product);

            var result = await _productService.AddAsync(product);
            if (result)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Failed to create product.");
            return View(product);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductDto product)
        {
            if (id != product.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(product);

            var result = await _productService.UpdateAsync(product);
            if (result)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Failed to update product.");
            return View(product);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var result = await _productService.DeleteAsync(id);
            if (result)
                return RedirectToAction(nameof(Index));

            TempData["errorMessage"] = "Failed to delete product.";
            return RedirectToAction(nameof(Delete), new { id });
        }

        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }
    }
}
