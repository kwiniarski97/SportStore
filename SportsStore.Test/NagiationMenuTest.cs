using System;
using System.Collections.Generic;
using System.Text;

namespace SportsStore.Test
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using Microsoft.AspNetCore.Routing;

    using Moq;

    using SportsStore.Components;
    using SportsStore.Models;

    using Xunit;

    public class NagiationMenuTest
    {
        [Fact]
        public void Can_Select_Categories()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(
                new[]
                    {
                        new Product { ProductId = 1, Name = "P1", Category = "Apples" },
                        new Product { ProductId = 2, Name = "P2", Category = "Apples" },
                        new Product { ProductId = 3, Name = "P3", Category = "Plums" },
                        new Product { ProductId = 4, Name = "P4", Category = "Oranges" },
                    }.AsQueryable<Product>());

            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);

            // Act
            var results = ((IEnumerable<string>)(target.Invoke() as ViewComponentResult).ViewData.Model).ToArray();

            // Assert 
            Assert.True(Enumerable.SequenceEqual(new[] { "Apples, Oranges, Plums" }, results));
        }

        [Fact]
        public void Indicates_Selected_Category()
        {
            // assign
            string selectedCategory = "Apples";
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(repo => repo.Products).Returns(
                (new[]
                     {
                         new Product { ProductId = 1, Name = "P1", Category = "Apples" },
                         new Product { ProductId = 4, Name = "P2", Category = "Oranges" },
                     }).AsQueryable<Product>());

            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);

            target.ViewComponentContext = new ViewComponentContext
            {
                ViewContext = new ViewContext
                {
                    RouteData = new RouteData()
                }
            };

            target.RouteData.Values["category"] = selectedCategory;

            string result = (string)(target.Invoke() as ViewComponentResult).ViewData["SelectedCategory"];

            Assert.Equal(selectedCategory, result);
        }
    }
}