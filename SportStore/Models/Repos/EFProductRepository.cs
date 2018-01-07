namespace SportsStore.Models
{
    using System.Linq;

    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext context;

        public EFProductRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Product> Products => this.context.Products;
    }
}