using MiniProjet.Net.Models;
using System.Collections.Generic;
using System.Linq;

namespace MiniProjet.Net.Models
{
	public class PanierRepository : IPanierRepository
	{
		readonly ApplicationDbContext context;
		
		public PanierRepository(ApplicationDbContext context)
		{
			this.context = context;
		}
		

		public void AddPanier(Panier panier)
		{
			context.Add(panier);
			context.SaveChanges();
		}

	
	}
}
