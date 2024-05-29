namespace MiniProjet.Net.Models.Repositories
{
    public interface ICommandeRepository
    {
        void Add(Commande commande);
        IEnumerable<Commande> GetAll();
        IEnumerable<Commande> GetAllforUser(string u);
        void Ex(Commande c);
        Commande GetById(int id);
        IEnumerable<Commande> GetHotClient();
        IList<Commande> GetCommandeByMail(String s);
        // Other methods if needed
    }
}
