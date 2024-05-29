using MiniProjet.Net.Models;

namespace MiniProjet.Net.Repositories.OrderRepositories
{
    public interface IOrderRepository
    {
        IList<Order> GetAll();
        Order GetById(int id);
        void Add(Order s);
        void Edit(Order s);
        void Delete(Order s);
        Task<IList<TopCustomer>> GetTopCustomersByTotalAmountAsync();
        IList<Order> GetOrdersByUserId(string userId);
    }
}
