namespace MiniProjet.Net.Models.Repositories
{
	public interface IContenuPanierRepository
	{
		void AddContenuPanier(ContenuPanier contenuPanier);
		IList<ContenuPanier> GetByPanier(int x);
        ContenuPanier GetHotArticle();
        ContenuPanier GetHotClient();
    }
}
