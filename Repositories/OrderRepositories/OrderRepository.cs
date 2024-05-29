using MiniProjet.Net.Models;
using Microsoft.EntityFrameworkCore;

namespace MiniProjet.Net.Repositories.OrderRepositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IList<Order> GetAll()
        {
            return context.Orders.ToList();
        }

        public Order GetById(int id)
        {
            return context.Orders.Where(x => x.OrderId == id)
                                 .Include(x => x.User)
                                 .SingleOrDefault();
        }

        public void Add(Order s)
        {
            context.Orders.Add(s);
            context.SaveChanges();
        }

        public void Edit(Order s)
        {
            Order s1 = context.Orders.Find(s.OrderId);
            if (s1 != null)
            {
                s1.IsPaid = s.IsPaid;
                s1.OrderDate = s.OrderDate;
                s1.TotalAmount = s.TotalAmount;
                context.SaveChanges();
            }
        }

        public void Delete(Order s)
        {
            Order s1 = context.Orders.Find(s.OrderId);
            if (s1 != null)
            {
                context.Orders.Remove(s1);
                context.SaveChanges();
            }
        }

        public async Task<IList<TopCustomer>> GetTopCustomersByTotalAmountAsync()
        {
            var orderTotals = context.OrderItems
                .GroupBy(oi => oi.OrderId)
                .Select(g => new
                {
                    OrderId = g.Key,
                    TotalAmount = g.Sum(oi => oi.Price * oi.Quantity)
                });

            var topCustomers = await context.Orders
                .Join(orderTotals,
                    o => o.OrderId,
                    ot => ot.OrderId,
                    (o, ot) => new { o.UserId, o.User.UserName, ot.TotalAmount })
                .GroupBy(o => new { o.UserId, o.UserName })
                .Select(g => new TopCustomer
                {
                    UserId = g.Key.UserId,
                    UserName = g.Key.UserName,
                    TotalAmount = g.Sum(x => x.TotalAmount)
                })
                .OrderByDescending(tc => tc.TotalAmount)
                .ToListAsync();

            // Log or debug here
            Console.WriteLine("Top Customers:");
            foreach (var customer in topCustomers)
            {
                Console.WriteLine($"UserId: {customer.UserId}, UserName: {customer.UserName}, TotalAmount: {customer.TotalAmount}");
            }

            return topCustomers;
        }



        public IList<Order> GetOrdersByUserId(string userId)
        {
            return context.Orders
                          .Where(o => o.UserId == userId)
                          .Include(o => o.User)
                          .Include(o => o.OrderItems)
                              .ThenInclude(oi => oi.Product)
                          .ToList();
        }
    }
}
