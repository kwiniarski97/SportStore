namespace SportsStore.Components
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using SportsStore.Models;

    public class NavigationMenuViewComponent : ViewComponent
    {
        private IProductRepository repository;

        public NavigationMenuViewComponent(IProductRepository repository)
        {
            this.repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            this.ViewBag.SelectedCategory = this.RouteData?.Values["category"];
            return View(this.repository.Products.Select(x => x.Category).Distinct().OrderBy(x => x));
        }
    }
}