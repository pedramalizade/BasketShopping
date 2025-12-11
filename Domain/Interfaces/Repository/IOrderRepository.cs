namespace Domain.Interfaces.Repository
{
    public interface IOrderRepository
    {
        /// <summary>
        /// افزودن محصول به سبد خرید کاربر. 
        /// اگر سفارشی باز وجود نداشته باشد، ایجاد می‌شود.
        /// در صورت وجود، تعداد محصول افزایش یا رکورد جدید اضافه می‌شود.
        /// </summary>
        /// <param name="productId">شناسه محصول</param>
        /// <param name="userId">شناسه کاربر</param>
        Task AddToCart(int productId, string userId);
        /// <summary>
        /// دریافت لیست آیتم‌های سبد خرید کاربر.
        /// اگر سفارشی باز وجود نداشته باشد، لیست خالی برمی‌گردد.
        /// </summary>
        /// <param name="userId">شناسه کاربر</param>
        /// <returns>لیست آیتم‌های سفارش</returns>
        Task<List<ShowOrderViewModel>> GetUserOrderAsync(string userId);
        /// <summary>
        /// حذف یک آیتم از جزئیات سفارش و به‌روزرسانی مجموع کل.
        /// </summary>
        /// <param name="orderDetailId">شناسه جزئیات سفارش</param>
        Task DeleteOrderDetailAsync(int orderDetailId);
        /// <summary>
        /// تغییر تعداد یک آیتم در سفارش بر اساس دستور (افزایش یا کاهش).
        /// اگر تعداد به صفر برسد، آیتم حذف می‌شود.
        /// </summary>
        /// <param name="orderDetailId">شناسه جزئیات سفارش</param>
        /// <param name="command">دستور: "up" یا "down"</param>
        Task UpdateOrderDetailCommandAsync(int orderDetailId, string command);
        /// <summary>
        /// محاسبه و به‌روزرسانی مجموع مبلغ سفارش بر اساس جزئیات آن.
        /// </summary>
        /// <param name="orderId">شناسه سفارش</param>
        Task UpdateSumOrder(int orderId);
        /// <summary>
        /// ایجاد درخواست پرداخت برای سفارش باز کاربر.
        /// در صورت موفقیت، لینک پرداخت بازگردانده می‌شود.
        /// </summary>
        /// <param name="userId">شناسه کاربر</param>
        /// <returns>لینک پرداخت یا null</returns>
        Task<string?> PaymentRequestAsync(string userId);
    }
}
