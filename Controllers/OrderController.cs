using Microsoft.AspNetCore.Mvc;
using MiniProjet.Net.Repositories.OrderRepositories;
using System.Security.Claims;

namespace MiniProjet.Net.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        // GET: OrderController
        public ActionResult Index()
        {
            var orders = orderRepository.GetAll();
            return View(orders);
        }

        public IActionResult Orders()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                var orders = orderRepository.GetOrdersByUserId(userId);

                return View(orders);
            }

            return RedirectToAction("Error");
        }
    


    public async Task<IActionResult> TopCustomers()
        {
            var topCustomers = await orderRepository.GetTopCustomersByTotalAmountAsync();

            // Log or debug here
            Console.WriteLine("Top Customers in Controller:");
            foreach (var customer in topCustomers)
            {
                Console.WriteLine($"UserId: {customer.UserId}, UserName: {customer.UserName}, TotalAmount: {customer.TotalAmount}");
            }

            return View(topCustomers);
        }


        public IActionResult CustomerOrders(string userId)
        {
            var orders = orderRepository.GetOrdersByUserId(userId);
            return View(orders);
        }
    }
}
