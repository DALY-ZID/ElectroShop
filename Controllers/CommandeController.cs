using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting.Internal;
using MiniProjet.Net.Models;
using MiniProjet.Net.Models.Repositories;
using MiniProjet.Net.Repositories.ProductRepositories;
using MiniProjet.Net.ViewModels;
using NuGet.Protocol.Core.Types;

namespace mini_projet.Controllers
{
    public class CommandeController : Controller
    {
        private readonly ICommandeRepository _commandeRepository;
        private readonly IContenuPanierRepository _contenuPanierRepository;
        private IProductRepository _produitRepository;

        public CommandeController(ICommandeRepository commandeRepository, IContenuPanierRepository contenuPanierRepository, IProductRepository produitRepository)
        {
            _commandeRepository = commandeRepository;
            _contenuPanierRepository = contenuPanierRepository;
            _produitRepository = produitRepository;
        }


        // GET: CommandeController
        public ActionResult Index()
        {
            var c = _commandeRepository.GetAllforUser(User.Identity.Name.ToString());
          
            return View(c);
        }

        public ActionResult Voir(int id)
        {
            var contenuPanier = _contenuPanierRepository.GetByPanier(id);
            var produits = _produitRepository.GetAll();
            var viewModel = new Tuple<IEnumerable<ContenuPanier>, IEnumerable<Product>>(contenuPanier, produits);
            return View(viewModel);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult HotArticle()
        {
            var contenuPanier = _contenuPanierRepository.GetHotArticle();
            var produit = _produitRepository.GetProductByid(contenuPanier.ProduitId);
          
            
            return View(produit);
        }

            [Authorize(Roles = "Admin")]
            public ActionResult HotClient()
            {
               
                var client = _commandeRepository.GetHotClient();


                return View(client);
            }

		public ActionResult ListCommandes()
		{
			var commaneds = _commandeRepository.GetAll();
		


			return View(commaneds);
		}


		// GET: CommandeController/Details/5
		public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CommandeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommandeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CommandeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CommandeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CommandeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CommandeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProduitController/Edit/5
        public ActionResult Action(int id)
        {
            Commande commande = _commandeRepository.GetById(id);
            if (commande == null)
            {
                return NotFound();
            }
            return View(commande);
        }
        // POST: ProduitController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Action(Commande updatedCommande)
        {
            try
            {
              
                    var existingCommande = _commandeRepository.GetById(updatedCommande.CommandeId);
                    if (existingCommande == null)
                    {
                        return NotFound();
                    }

                    existingCommande.status = updatedCommande.status;
                    existingCommande.TotalAmount = updatedCommande.TotalAmount;

                    _commandeRepository.Ex(existingCommande);

                    return RedirectToAction("ListCommandes");
                
                return View(updatedCommande); // Return to the edit view to display validation errors
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return View(updatedCommande); // Return to the edit view with the submitted data
            }
        }


        public ActionResult SearchN(string name, int? status)
        {
            var result = _commandeRepository.GetAll();

            if (!string.IsNullOrEmpty(name))
            {
                result = _commandeRepository.GetCommandeByMail(name);
            }

            if (status.HasValue)
            {
                result = result.Where(p => p.status == status).ToList();
            }

            return View("ListCommandes", result);
        }


    }
}
