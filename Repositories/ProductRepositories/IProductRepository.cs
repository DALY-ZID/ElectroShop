using MiniProjet.Net.Models;

namespace MiniProjet.Net.Repositories.ProductRepositories
{
    public interface IProductRepository
    {
        IList<Product> GetAll();
        Product GetById(int id);
        void Add(Product p);
        Product Edit(Product p);
        void Delete(Product p);
        IList<Product> GetProductsByCategoryId(int? categoryId);
        IList<Product> FindByNameAndCategory(string name, int? categoryId);
        IList<Product> GetBestSellingProducts(int count);
        IList<Product> GetProductByPrice(decimal price);
        IList<Product> GetProductByName(String s);
        Product GetProductByid(int id);
    }
}
