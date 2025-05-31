using LemonHiveEcommerce.DTOs;
using LemonHiveEcommerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LemonHiveEcommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartItemService _cartItemService;

        public CartController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var cartItems = await _cartItemService.GetAllWithProductAsync();

                if (cartItems == null || cartItems.Count() == 0)
                {
                    TempData["errorMessage"] = "Cart is empty.";
                    return View(new List<CartItemDto>());
                }

                return View(cartItems);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = $"Error loading cart: {ex.Message}";
                return View(new List<CartItemDto>());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(Guid productId, int qty)
        {
            try
            {
                if (productId == Guid.Empty || qty <= 0)
                {
                    TempData["errorMessage"] = "Invalid product or quantity.";
                    return RedirectToAction("Index", "Product");
                }

                var result = await _cartItemService.AddToCartAsync(productId, qty);

                if (!result)
                {
                    TempData["errorMessage"] = "Failed to add product to cart.";
                    return RedirectToAction("Index", "Product");
                }

                TempData["successMessage"] = "Product added to cart successfully.";
                return RedirectToAction("Index", "Product");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = $"Error: {ex.Message}";
                return RedirectToAction("Index", "Product");
            }
        }

        public async Task<IActionResult> MyCart()
        {
            try
            {
                var cartItems = await _cartItemService.GetAllAsync();
                return View(cartItems);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = $"Error loading cart: {ex.Message}";
                return View(new List<CartItemDto>());
            }
        }
    }
}

