namespace Application.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderService _orderService;
        public OrderService(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// افزودن محصول به سبد خرید کاربر. 
        /// اگر سفارشی باز وجود نداشته باشد، ایجاد می‌شود.
        /// در صورت وجود، تعداد محصول افزایش یا رکورد جدید اضافه می‌شود.
        /// </summary>
        /// <param name="productId">شناسه محصول</param>
        /// <param name="userId">شناسه کاربر</param>
        public async Task AddToCart(int productId, string userId)
        {
            await _orderService.AddToCart(productId, userId);
        }

        /// <summary>
        /// حذف یک آیتم از جزئیات سفارش و به‌روزرسانی مجموع کل.
        /// </summary>
        /// <param name="orderDetailId">شناسه جزئیات سفارش</param>
        public async Task DeleteOrderDetailAsync(int orderDetailId)
        {
            await _orderService.DeleteOrderDetailAsync(orderDetailId);
        }

        /// <summary>
        /// دریافت لیست آیتم‌های سبد خرید کاربر.
        /// اگر سفارشی باز وجود نداشته باشد، لیست خالی برمی‌گردد.
        /// </summary>
        /// <param name="userId">شناسه کاربر</param>
        /// <returns>لیست آیتم‌های سفارش</returns>
        public async Task<List<ShowOrderViewModel>> GetUserOrderAsync(string userId)
        {
            var result = await _orderService.GetUserOrderAsync(userId);
            return result;
        }

        /// <summary>
        /// ایجاد درخواست پرداخت برای سفارش باز کاربر.
        /// در صورت موفقیت، لینک پرداخت بازگردانده می‌شود.
        /// </summary>
        /// <param name="userId">شناسه کاربر</param>
        /// <returns>لینک پرداخت یا null</returns>
        public async Task<string?> PaymentRequestAsync(string userId)
        {
            var result = await _orderService.PaymentRequestAsync(userId);
            return result;
        }

        /// <summary>
        /// تغییر تعداد یک آیتم در سفارش بر اساس دستور (افزایش یا کاهش).
        /// اگر تعداد به صفر برسد، آیتم حذف می‌شود.
        /// </summary>
        /// <param name="orderDetailId">شناسه جزئیات سفارش</param>
        /// <param name="command">دستور: "up" یا "down"</param>
        public async Task UpdateOrderDetailCommandAsync(int orderDetailId, string command)
        {
            await _orderService.UpdateOrderDetailCommandAsync(orderDetailId, command);
        }


        /// <summary>
        /// محاسبه و به‌روزرسانی مجموع مبلغ سفارش بر اساس جزئیات آن.
        /// </summary>
        /// <param name="orderId">شناسه سفارش</param>
        public async Task UpdateSumOrder(int orderId)
        {
            await _orderService.UpdateSumOrder(orderId);    
        }
    }
}
