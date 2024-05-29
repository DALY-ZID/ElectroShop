using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProjet.Net.Models;
using MiniProjet.Net.Repositories.CategorieRepositories;

namespace MiniProjet.Net.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        readonly ICategorieRepository categorieRepository;

        public CategoryController(ICategorieRepository categorieRepository)
        {
            this.categorieRepository = categorieRepository;
        }

        // GET: Category
        public ActionResult Index()
        {
            var categories = categorieRepository.GetAll();
            return View(categories);
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            var category = categorieRepository.GetById(id);
            return View(category);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            try
            {
                categorieRepository.Add(category);
                return RedirectToAction(nameof(Index));     
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            var category = categorieRepository.GetById(id);
            return View(category);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            try
            {
                categorieRepository.Edit(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            Category category = categorieRepository.GetById(id);
            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Category c)
        {
            try
            {
                categorieRepository.Delete(c);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
