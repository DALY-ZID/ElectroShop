using MiniProjet.Net.Models;

namespace MiniProjet.Net.Repositories.OrderItemsRepositories
{
    public interface IOrderItemsRepository
    {
        IList<OrderItem> GetAll();
        OrderItem GetById(int id);
        void Add(OrderItem s);
        void Edit(OrderItem s);
        void Delete(OrderItem s);
    }
}
