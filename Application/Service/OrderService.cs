namespace Application.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderService _orderService;
        public OrderService(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task AddToCart(int productId, string userId)
        {
            await _orderService.AddToCart(productId, userId);
        }

        public async Task DeleteOrderDetailAsync(int orderDetailId)
        {
            await _orderService.DeleteOrderDetailAsync(orderDetailId);
        }

        public async Task<List<ShowOrderViewModel>> GetUserOrderAsync(string userId)
        {
            var result = await _orderService.GetUserOrderAsync(userId);
            return result;
        }

        public async Task<string?> PaymentRequestAsync(string userId)
        {
            var result = await _orderService.PaymentRequestAsync(userId);
            return result;
        }

        public async Task UpdateOrderDetailCommandAsync(int orderDetailId, string command)
        {
            await _orderService.UpdateOrderDetailCommandAsync(orderDetailId, command);
        }

        public async Task UpdateSumOrder(int orderId)
        {
            await _orderService.UpdateSumOrder(orderId);    
        }
    }
}
