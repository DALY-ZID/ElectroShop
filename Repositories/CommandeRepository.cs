using Microsoft.EntityFrameworkCore;
using MiniProjet.Net.Models;

namespace MiniProjet.Net.Models.Repositories
{

    public class CommandeRepository : ICommandeRepository
    {
        private readonly ApplicationDbContext _context;

        public CommandeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Commande commande)
        {
            _context.Commandes.Add(commande);
            _context.SaveChanges();
        }

        public IEnumerable<Commande> GetAll()
        {
            return _context.Commandes.ToList();
        }
        public IEnumerable<Commande> GetAllforUser(string u)
        {

            return _context.Commandes.Where(n => n.UserName == u).ToList();
        }


        public Commande GetById(int id)
        {
            return _context.Commandes.Find(id);
        }
        public IEnumerable<Commande> GetHotClient()
        {
            var hotClientGroup = _context.Commandes
                                    .GroupBy(cp => cp.UserName)
                                    .Select(group => new Commande
                                    {
                                        UserName = group.Key,
                                        TotalAmount = group.Sum(cp => cp.TotalAmount)
                                        
                                    })
                                    .OrderByDescending(commande => commande.TotalAmount);
                                    
            return hotClientGroup;
        }
        public void Ex(Commande c)
        {
            _context.Entry(c).State = EntityState.Modified;
            _context.SaveChanges();
            
        }
        public IList<Commande> GetCommandeByMail(String s)
        {
            return _context.Commandes.Where(n => n.UserName.Contains(s)).ToList();
        }


        // Implement other methods if needed
    }
}
