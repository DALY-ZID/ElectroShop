using MiniProjet.Net.Models;
namespace MiniProjet.Net.Repositories.CategorieRepositories

{
    public interface ICategorieRepository
    {
        IList<Category> GetAll();
        Category GetById(int id);
        void Add(Category s);
        void Edit(Category s);
        void Delete(Category s);
       
    }
}
