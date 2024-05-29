using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniProjet.Net.Repositories.CategorieRepositories;
using MiniProjet.Net.Repositories.ProductRepositories;
namespace MiniProjet.Net.Controllers
{
   

    public class ProductsUserController : Controller
    {
        readonly IProductRepository productRepository;
        // GET: SchoolController



        // GET: StudentController
        public ProductsUserController(IProductRepository pRepository)
        {

            this.productRepository = pRepository;


        }
        public IActionResult Index()
        {
            var products = productRepository.GetAll();
            return View(products);
        }
    }
}
