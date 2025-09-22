namespace Domain.Interfaces.Repository
{
    public interface IOrderRepository
    {
        Task AddToCart(int productId, string userId);
        Task<List<ShowOrderViewModel>> GetUserOrderAsync(string userId);
        Task DeleteOrderDetailAsync(int orderDetailId);
        Task UpdateOrderDetailCommandAsync(int orderDetailId, string command);
        Task UpdateSumOrder(int orderId);
        Task<string?> PaymentRequestAsync(string userId);
    }
}
