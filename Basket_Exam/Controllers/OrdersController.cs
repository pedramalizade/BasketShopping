namespace Basket_Exam.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderService;
        public OrdersController(IOrderRepository orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// افزودن یک محصول به سبد خرید کاربر.
        /// </summary>
        /// <param name="id">شناسه محصول</param>
        public async Task<IActionResult> AddToCart(int id)
        {
            string currentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _orderService.AddToCart(id, currentUserID);

            return Redirect("/");
        }

        /// <summary>
        /// نمایش سبد خرید و لیست آیتم‌های سفارش.
        /// </summary>
        public async Task<IActionResult> ShowOrder()
        {
            string currentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orderList = await _orderService.GetUserOrderAsync(currentUserID);

            return View(orderList);
        }

        /// <summary>
        /// حذف یک آیتم از سفارش فعلی.
        /// </summary>
        /// <param name="id">شناسه جزئیات سفارش</param>
        public async Task<IActionResult> Delete(int id)
        {
            await _orderService.DeleteOrderDetailAsync(id);
            return RedirectToAction("ShowOrder");
        }
        /// <summary>
        /// اجرای دستور روی آیتم سفارش (افزایش یا کاهش تعداد).
        /// </summary>
        /// <param name="id">شناسه جزئیات سفارش</param>
        /// <param name="command">دستور: up یا down</param>
        public async Task<IActionResult> Command(int id, string command)
        {
            await _orderService.UpdateOrderDetailCommandAsync(id, command);
            return RedirectToAction("ShowOrder");
        }

        /// <summary>
        /// به‌روزرسانی مجموع سفارش بر اساس آیتم‌های آن.
        /// </summary>
        /// <param name="orderId">شناسه سفارش</param>
        [HttpPost]
        public async Task<IActionResult> UpdateSumOrder(int orderId)
        {
            await _orderService.UpdateSumOrder(orderId);

            return RedirectToAction("ShowOrder");
        }

        /// <summary>
        /// ایجاد درخواست پرداخت و انتقال کاربر به صفحه پرداخت.
        /// </summary>

        public async Task<IActionResult> Payment()
        {
            string currentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var redirectUrl = await _orderService.PaymentRequestAsync(currentUserID);

            if (redirectUrl == null)
            {
                return BadRequest();
            }

            return Redirect(redirectUrl);
        }
    }
}
