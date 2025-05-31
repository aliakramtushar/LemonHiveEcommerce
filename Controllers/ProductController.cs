using LemonHiveEcommerce.DTOs;
using LemonHiveEcommerce.Models;
using LemonHiveEcommerce.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace LemonHiveEcommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartItemService _cartItemService;
        private IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductService service, ICartItemService cartItemService, IWebHostEnvironment webHostEnvironment)
        {
            _productService = service;
            _cartItemService = cartItemService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(string search, int page = 1)
        {
            try
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
            catch (Exception ex)
            {
                TempData["errorMessage"] = $"Error loading cart: {ex.Message}";
                throw;
            }
        }

        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = $"Error loading cart: {ex.Message}";
                throw;
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDto product)
        {
            try
            {
                if (!ModelState.IsValid) return View(product);

                if (product.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(product.ImageFile.FileName);
                    string path = Path.Combine(wwwRootPath + "/assets/images", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(fileStream);
                    }

                    product.ImagePath = "/assets/" + fileName;
                }

                var result = await _productService.AddAsync(product);

                TempData["successMessage"] = "Product created successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = $"Error loading cart: {ex.Message}";

            }
            return View(product);
        }


        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest();

                var product = await _productService.GetByIdAsync(id);
                if (product == null)
                    return NotFound();

                return View(product);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = $"Error loading cart: {ex.Message}";
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductDto product)
        {
            try
            {
                if (id != product.Id)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return View(product);

                var result = await _productService.UpdateAsync(product);
                if (result)
                {
                    TempData["successMessage"] = "Product updated successfully.";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Failed to update product.");
                return View(product);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = $"Error loading cart: {ex.Message}";
                throw;
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest();

                var product = await _productService.GetByIdAsync(id);
                if (product == null)
                    return NotFound();

                return View(product);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = $"Error loading cart: {ex.Message}";
                return NotFound();
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                var result = await _productService.DeleteAsync(id);
                if (result)
                {
                    TempData["successMessage"] = "Product updated successfully.";
                    return RedirectToAction(nameof(Index));
                }

                TempData["errorMessage"] = "Failed to delete product.";
                return RedirectToAction(nameof(Delete), new { id });
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = $"Error loading cart: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest();
                }

                var product = await _productService.GetByIdAsync(id);
                if (product == null)
                {
                    TempData["errorMessage"] = "No Product found";
                    return NotFound();
                }

                return View(product);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = $"Error loading cart: {ex.Message}";
                return View();
            }
        }
    }
}
