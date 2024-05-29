using MiniProjet.Net.Models;
using Microsoft.EntityFrameworkCore;


namespace MiniProjet.Net.Repositories.ProductRepositories
{
    public class ProductRepository : IProductRepository
    {
        readonly ApplicationDbContext context;
        public ProductRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IList<Product> GetAll()
        {
            return context.Products.OrderBy(x => x.ProductName).Include(x => x.Category).ToList();
        }
        public Product GetById(int id)
        {
            return context.Products.Where(x => x.ProductId == id).Include(x => x.Category).SingleOrDefault();
        }
        public void Add(Product p)
        {
            context.Products.Add(p);
            context.SaveChanges();
        }
        public Product Edit(Product p)
        {
            var Product = context.Products.Attach(p);
            Product.State = EntityState.Modified;
            context.SaveChanges();
            return p;

        }
       
        public IList<Product> GetProductByPrice(decimal price)
        {
            return context.Products.Where(p => p.Price == price).ToList();
        }
        public Product GetProductByid(int id)
        {
            return context.Products.FirstOrDefault(p => p.ProductId == id);
        }

        public IList<Product> GetProductByName(String s)
        {
            return context.Products.Where(n => n.ProductName.Contains(s)).ToList();
        }
        public IList<Product> GetProductByCat(int s)
        {
            return context.Products.Where(n => n.Category.CategoryId == s).ToList();
        }
        public void Delete(Product p)
        {
            Product p1 = context.Products.Find(p.ProductId);
            if (p1 != null)
            {
                context.Products.Remove(p1);
                context.SaveChanges();
            }
        }

        public IList<Product> GetProductsByCategoryId(int? categoryId)
        {
            return context.Products
                .Where(p => p.CategoryId == categoryId)
                .Include(p => p.Category)
                .ToList();
        }

        public IList<Product> FindByNameAndCategory(string name, int? categoryId)
        {
            return context.Products
                .Where(p => p.ProductName.Contains(name) && p.CategoryId == categoryId)
                .Include(p => p.Category)
                .ToList();
        }

        public IList<Product> GetBestSellingProducts(int count)
        {
            var bestSellingProducts = context.ContenuPanier
                .GroupBy(oi => oi.ProduitId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalQuantity = g.Sum(oi => oi.Quantite)
                })
                .OrderByDescending(x => x.TotalQuantity)
                .Take(count)
                .Join(context.Products,
                      oi => oi.ProductId,
                      p => p.ProductId,
                      (oi, p) => p)
                .ToList();

            return bestSellingProducts;
        }
    }
}
