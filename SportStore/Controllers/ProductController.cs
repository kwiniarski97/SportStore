namespace SportsStore.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using SportsStore.Models;
    using SportsStore.Models.ViewModels;

    public class ProductController : Controller
    {
        private IProductRepository repository;

        public int PageSize = 4;

        public ProductController(IProductRepository repo)
        {
            this.repository = repo;
        }

        public ViewResult List(string category, int productPage = 1) => this.View(
            new ProductListViewModel
            {
                PagingInfo =
                        new PagingInfo
                        {
                            CurrentPage = productPage,
                            ItemsPerPage = this.PageSize,
                            TotalItems =
                                    category == null
                                        ? this.repository.Products.Count()
                                        : this.repository.Products.Count(p => p.Category == category)
                        },
                Products = this.repository.Products
                        .Where(p => p.Category == category || category == null)
                        .OrderBy(p => p.ProductId).Skip((productPage - 1) * this.PageSize)
                        .Take(this.PageSize),
                CurrentCategory = category
            });
    }
}