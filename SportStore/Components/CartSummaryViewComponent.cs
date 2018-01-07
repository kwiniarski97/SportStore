using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Components
{
    using Microsoft.AspNetCore.Mvc;

    using SportsStore.Models;

    public class CartSummaryViewComponent : ViewComponent
    {
        private Cart cart;

        public CartSummaryViewComponent(Cart cartService)
        {
            this.cart = cartService;
        }

        public IViewComponentResult Invoke()
        {
            return this.View(cart);
        }
    }

}
