using MiniProjet.Net.Models;

namespace MiniProjet.Net.Repositories.CategorieRepositories
{
    public class CategorieRepository : ICategorieRepository
    {
        readonly ApplicationDbContext context;
        public CategorieRepository (ApplicationDbContext context)
        {
            this.context = context;
        }
        public IList<Category> GetAll()
        {
            return context.Categories.OrderBy(x => x.CategoryName).ToList();
        }
        public Category GetById(int id)
        {
            return context.Categories.Find(id);
        }
        public void Add(Category s)
        {
            context.Categories.Add(s);
            context.SaveChanges();
        }
        public void Edit(Category c)
        {
            Category c1 = context.Categories.Find(c.CategoryId);
            if (c1 != null)
            {
                c1.CategoryName = c.CategoryName;
               
                context.SaveChanges();
            }
        }
        public void Delete(Category c)
        {
            Category c1 = context.Categories.Find(c.CategoryId);
            if (c1 != null)
            {
                context.Categories.Remove(c1);
                context.SaveChanges();
            }
        }
       
    }
}
