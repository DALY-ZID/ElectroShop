using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniProjet.Net.Models;
using MiniProjet.Net.Repositories.CategorieRepositories;
using MiniProjet.Net.Repositories.OrderRepositories;
using MiniProjet.Net.Repositories.ProductRepositories;
using MiniProjet.Net.ViewModels;

namespace MiniProjet.Net.Controllers
{
   
    public class ProductController : Controller
    {
        readonly IProductRepository productRepository;
        readonly ICategorieRepository categorieRepository;
        private readonly IWebHostEnvironment hostingEnvironment;

        public ProductController(IProductRepository productRepository, ICategorieRepository categorieRepository, IWebHostEnvironment hostingEnvironment)
        {
            this.productRepository = productRepository;
            this.categorieRepository = categorieRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: ProductController
        public ActionResult Index()
        {
            var products = productRepository.GetAll();
            ViewBag.CategoryId = new SelectList(categorieRepository.GetAll(), "CategoryId ", "CategoryName");
            return View(products);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            var products = productRepository.GetById(id);
            return View(products);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(categorieRepository.GetAll(), "CategoryId", "CategoryName");
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.ImageFile != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.ImageFile.CopyTo(new FileStream(filePath, FileMode.Create));

                }

                Product newProduct = new Product
                {
                    ProductName = model.ProductName,
                    Description = model.Description,
                    Price = model.Price,
                    ImageUrl = uniqueFileName,
                    CategoryId = model.CategoryId,
                    Brand = model.Brand,
                    Stock = model.Stock
                };

                productRepository.Add(newProduct);
                return RedirectToAction("details", new { id = newProduct.ProductId });
            }

            ViewBag.CategoryId = new SelectList(categorieRepository.GetAll(), "CategoryId", "CategoryName");
            return View();
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            Product product = productRepository.GetById(id);
            EditViewModel productEditViewModel = new EditViewModel
            {
                Id = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                Brand = product.Brand,
                ExistingImagePath = product.ImageUrl,
                CategoryId = product.CategoryId
            };
            ViewBag.CategoryId = new SelectList(categorieRepository.GetAll(), "CategoryId", "CategoryName");

            return View(productEditViewModel);
        }


        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Product product = productRepository.GetById(model.Id);

                product.ProductName = model.ProductName;
                product.Description = model.Description;
                product.Price = model.Price;
                product.Stock = model.Stock;
                product.Brand = model.Brand;
                product.CategoryId = model.CategoryId;

                if (model.ImageFile != null)
                {
                    
                    if (model.ExistingImagePath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingImagePath);
                        System.IO.File.Delete(filePath);
                    }
                    
                    product.ImageUrl = ProcessUploadedFile(model);
                }

                Product updatedProduct = productRepository.Edit(product);
                if (updatedProduct != null)
                    return RedirectToAction("Index");
                else
                    return NotFound();
            }
            ViewBag.CategoryId = new SelectList(categorieRepository.GetAll(), "CategoryId", "CategoryName");
            return View(model);
        }

        [NonAction]
        private string ProcessUploadedFile(EditViewModel model)
        {
            string uniqueFileName = null;
            if (model.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }


        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            Product product = productRepository.GetById(id);
            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Product p)
        {
            try
            {
                productRepository.Delete(p);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }

        public ActionResult Search(string name, int? categoryId)
        {
            IList<Product> result;

            if (!string.IsNullOrEmpty(name) && categoryId.HasValue && categoryId > 0)
            {
                result = productRepository.FindByNameAndCategory(name, categoryId.Value);
            }
            else if (categoryId.HasValue && categoryId > 0)
            {
                result = productRepository.GetProductsByCategoryId(categoryId.Value);
            }
            else
            {
                result = productRepository.GetAll();
            }

            ViewBag.CategoryId = new SelectList(categorieRepository.GetAll(), "CategoryId", "CategoryName");
            return View("Index", result);
        }


        public ActionResult BestSellingProducts()
        {
            var bestSellingProducts = productRepository.GetBestSellingProducts(10); // Affiche les 10 articles les plus vendus
            return View(bestSellingProducts);
        }
        public ActionResult SearchN(string name, decimal? minPrice, decimal? maxPrice)
        {
            var result = productRepository.GetAll();
            if (!string.IsNullOrEmpty(name))
                result = productRepository.GetProductByName(name);
            if (minPrice.HasValue || maxPrice.HasValue)
            {
                result = result.Where(p => (!minPrice.HasValue || p.Price >= minPrice) && (!maxPrice.HasValue || p.Price <= maxPrice)).ToList();

            }


            ViewBag.SchoolID = new SelectList(productRepository.GetAll(), "ProductID", "Produit");
            return View("Index", result);
        }


    }
}
