using MiniProjet.Net.Models;
using System.Linq;
namespace MiniProjet.Net.Models.Repositories;

	public class ContenuPanierRepository : IContenuPanierRepository
	{
		readonly ApplicationDbContext context;

		public ContenuPanierRepository(ApplicationDbContext context)
		{
			this.context = context;
		}


		public void AddContenuPanier(ContenuPanier contenuPanier)
		{
			context.Add(contenuPanier);
			context.SaveChanges();
		}
    public IList<ContenuPanier> GetByPanier(int x)
    {
        return context.ContenuPanier.Where(n => n.PanierId == x).ToList();
    }



    public ContenuPanier GetHotArticle()
    {
        var hotArticleGroup = context.ContenuPanier
                                .GroupBy(cp => cp.ProduitId)
                                .Select(group => new {
                                    ProduitId = group.Key,
                                    TotalQuantite = group.Sum(cp => cp.Quantite)
                                })
                                .OrderByDescending(result => result.TotalQuantite)
                                .FirstOrDefault(); // Get the product with the highest total quantity

        if (hotArticleGroup == null)
        {
            return null;
        }

        var hotContenuPanier = context.ContenuPanier
                                    .FirstOrDefault(cp => cp.ProduitId == hotArticleGroup.ProduitId);

        return hotContenuPanier;
    }
    public ContenuPanier GetHotClient()
    {
        var hotArticleGroup = context.ContenuPanier
                                 .GroupBy(cp => new { cp.PanierId, cp.ProduitId }) // Group by both PanierId and ProduitId
                                 .Select(group => new {
                                     PanierId = group.Key.PanierId,
                                     ProduitId = group.Key.ProduitId,
                                     TotalQuantite = group.Sum(cp => cp.Quantite)
                                 })
                                 .OrderByDescending(result => result.TotalQuantite)
                                 .FirstOrDefault(); // Get the group with the highest total quantity

        if (hotArticleGroup == null)
        {
            // Handle the case where no hot articles are found
            return null;
        }

        // Retrieve the ContenuPanier object for the hottest article
        var hotContenuPanier = context.ContenuPanier
                                    .FirstOrDefault(cp => cp.PanierId == hotArticleGroup.PanierId &&
                                                           cp.ProduitId == hotArticleGroup.ProduitId);

        return hotContenuPanier;
    }




}
