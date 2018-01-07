namespace SportsStore.Models.ViewModels
{
    using System.Collections.Generic;

    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public string CurrentCategory { get; set; }
    }
}