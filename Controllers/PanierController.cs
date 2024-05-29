using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProjet.Net.Models;
using MiniProjet.Net.Models.Repositories;
using MiniProjet.Net.Models.Help;
using MiniProjet.Net.Repositories.ProductRepositories;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic; // Add this for List<T>

namespace mini_projet.Controllers
{
    public class PanierController : Controller
    {
        private readonly IProductRepository produitRepository;
		
		private readonly ICommandeRepository _commandeRepository;

        private readonly IPanierRepository _panierRepository;

		private readonly IContenuPanierRepository _contenuPanierRepository;

		public PanierController(IProductRepository _produitRepository, ICommandeRepository commandeRepository,IPanierRepository panierRepository,IContenuPanierRepository contenuPanierRepository)
        {
            produitRepository = _produitRepository;
            _commandeRepository = commandeRepository;
			_panierRepository = panierRepository;
            _contenuPanierRepository = contenuPanierRepository;


		}

        public ActionResult Index()
        {
            ViewBag.Liste = ListeCart.Instance.Items;
            ViewBag.total = ListeCart.Instance.GetSubTotal();
            return View();
        }

        public ActionResult AjouterProduit(int id)
        {
            Product pp = produitRepository.GetById(id);
            ListeCart.Instance.AddItem(pp);
            ViewBag.Liste = ListeCart.Instance.Items;
            ViewBag.total = ListeCart.Instance.GetSubTotal();
            return View();
        }

        [HttpPost]
        public ActionResult PlusProduit(int id)
        {
            Product pp = produitRepository.GetById(id);
            ListeCart.Instance.AddItem(pp);
            Item trouve = null;
            foreach (Item a in ListeCart.Instance.Items)
            {
                if (a.Prod.ProductId == pp.ProductId)
                    trouve = a;
            }
            var results = new
            {
                ct = 1,
                Total = ListeCart.Instance.GetSubTotal(),
                Quatite = trouve.quantite,
                TotalRow = trouve.TotalPrice
            };
            return Json(results);
        }

        public ActionResult CheckOut()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckOut(IFormCollection collection)
        {

			var ContenuPaniers = new List<ContenuPanier>();

			// Create a new Panier
			Panier panier = new Panier();

			// Save the Panier object to get its generated PanierId
			_panierRepository.AddPanier(panier);
            decimal totalPanier = 0;
            foreach (var item in ListeCart.Instance.Items)
			{
                decimal itemTotal = item.Prod.Price * item.quantite;
                totalPanier += itemTotal;
                ContenuPanier contenuPanier = new ContenuPanier
				{
					// Set the PanierId to the generated PanierId
					PanierId = panier.PanierId,
					ProduitId = item.Prod.ProductId,
					Quantite = item.quantite
				};

				ContenuPaniers.Add(contenuPanier);
			}

			// Save ContenuPaniers to the database
			foreach (var contenuPanier in ContenuPaniers)
			{
				_contenuPanierRepository.AddContenuPanier(contenuPanier);
			}
		
            
			Commande newCommande = new Commande
            {
                Panier = panier,
                UserName = User.Identity.Name,
                TotalAmount = totalPanier
            };

			
			_commandeRepository.Add(newCommande);
          

			ListeCart.Instance.Items.Clear();
            ViewBag.Message = "Commande effectuée avec succès";
            return View();
        }

        [HttpPost]
        public ActionResult SupprimerProduit(int id)
        {
            Product pp = produitRepository.GetById(id);
            ListeCart.Instance.RemoveItem(pp);
            var results = new
            {
                Total = ListeCart.Instance.GetSubTotal(),
            };
            return Json(results);
        }
    }
}
