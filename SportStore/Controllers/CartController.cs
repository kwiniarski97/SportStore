using System.Linq;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SportsStore.Infrastructure;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using SportsStore.Models;
    using SportsStore.Models.ViewModels;

    public class CartController : Controller
    {
        private IProductRepository repository;

        private Cart cart;

        public CartController(IProductRepository repository, Cart cartService)
        {
            this.repository = repository;
            this.cart = cartService;
        }

        public ViewResult Index(string resultsUrl)
        {
            return View(new CartIndexViewModel { Cart = this.cart, returnUrl = resultsUrl });
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = this.repository.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                cart.AddItem(product, 1);

            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = this.repository.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                this.cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}