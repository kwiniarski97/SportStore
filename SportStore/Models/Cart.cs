using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public virtual void AddItem(Product product, int quantity)
        {
            CartLine line = this.lineCollection.FirstOrDefault(p => p.Product.ProductId == product.ProductId);

            if (line == null)
            {
                this.lineCollection.Add(new CartLine { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Product prod)
        {
            this.lineCollection.RemoveAll(l => l.Product.ProductId == prod.ProductId);
        }

        public virtual decimal ComputeTotalValue() => this.lineCollection.Sum(e => e.Product.Price * e.Quantity);

        public virtual IEnumerable<CartLine> Lines => lineCollection;

        public virtual void Clear() => this.lineCollection.Clear();
    }

    public class CartLine
    {
        public int CartLineID { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}