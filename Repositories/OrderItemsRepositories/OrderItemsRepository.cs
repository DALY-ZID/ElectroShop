using MiniProjet.Net.Models;
using Microsoft.EntityFrameworkCore;

namespace MiniProjet.Net.Repositories.OrderItemsRepositories
{
    public class OrderItemsRepository : IOrderItemsRepository
    {

        readonly ApplicationDbContext context;
        public OrderItemsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IList<OrderItem> GetAll()
        {
            return context.OrderItems.OrderBy(x => x.OrderItemId).Include(x
            => x.Order).ToList();
        }
        public OrderItem GetById(int id)
        {
            return context.OrderItems.Where(x => x.OrderItemId == id).Include(x => x.Order).SingleOrDefault();
        }
        public void Add(OrderItem s)
        {
            context.OrderItems.Add(s);
            context.SaveChanges();
        }
        public void Edit(OrderItem s)
        {
            OrderItem s1 = context.OrderItems.Find(s.OrderItemId);
            if (s1 != null)
            {
                s1.Price = s.Price;
                s1.Quantity = s.Quantity;
                context.SaveChanges();
            }
        }
        public void Delete(OrderItem s)
        {
            OrderItem s1 = context.OrderItems.Find(s.OrderItemId);
            if (s1 != null)
            {
                context.OrderItems.Remove(s1);
                context.SaveChanges();
            }
        }
    }
}
