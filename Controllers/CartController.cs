//using LemonHiveEcommerce.Models;
//using LemonHiveEcommerce.Repositories.Interfaces;
//using Microsoft.AspNetCore.Mvc;

//namespace LemonHiveEcommerce.Controllers
//{
//    public class CartController : Controller
//    {
//        private readonly IUnitOfWork _unitOfWork;

//        public CartController(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        public async Task<IActionResult> Index()
//        {
//            try
//            {
//                var cartItems = await _unitOfWork.CartItems.Gets();
//                if (cartItems == null || cartItems == null || !cartItems.Any())
//                {
//                    TempData["errorMessage"] = "Cart is empty";
//                    return View(new List<Product>());
//                }

//                var productIds = cartItems.Select(ci => ci.ProductId).ToList();
//                var products = await _unitOfWork.Products.GetByIds(productIds);

//                return View(products);
//            }
//            catch (Exception ex)
//            {
//                TempData["errorMessage"] = ex.Message;
//                return View(new List<Product>());
//            }
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> AddToCart(Guid productId, int qty)
//        {
//            try
//            {
//                if (productId == Guid.Empty || qty <= 0)
//                {
//                    TempData["errorMessage"] = "Invalid product or quantity.";
//                    return RedirectToAction("Index", "Product");
//                }

//                var product = await _unitOfWork.Products.GetById(productId);
//                if (product == null)
//                {
//                    TempData["errorMessage"] = "Product not found.";
//                    return RedirectToAction("Index", "Product");
//                }


//                    var cartItem = new CartItem
//                    {
//                        Id = Guid.NewGuid(),
//                        ProductId = productId,
//                        Qty = qty,
//                        CreatedDate = DateTime.Now,
//                        UpdatedDate = DateTime.Now,
//                    };
//                    await _unitOfWork.CartItems.Add(cartItem);

//                await _unitOfWork.SaveASync();
//                TempData["successMessage"] = "Product added to cartItems.";
//                return RedirectToAction("Index", "Product");
//            }
//            catch (Exception ex)
//            {
//                TempData["errorMessage"] = $"Error: {ex.Message}";
//                return RedirectToAction("Index", "Product");
//            }
//        }

//        public async Task<IActionResult> MyCart()
//        {
//            try
//            {
//                var items = await _unitOfWork.CartItems.Gets();

//                return View(items);
//            }
//            catch (Exception ex)
//            {
//                TempData["errorMessage"] = $"Error loading cartItems: {ex.Message}";
//                return View(new List<CartItem>());
//            }
//        }
//    }
//}






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

