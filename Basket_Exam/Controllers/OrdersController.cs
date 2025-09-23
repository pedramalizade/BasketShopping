namespace Basket_Exam.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            string currentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _orderService.AddToCart(id, currentUserID);

            return Redirect("/");
        }

        public async Task<IActionResult> ShowOrder()
        {
            string currentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orderList = await _orderService.GetUserOrderAsync(currentUserID);

            return View(orderList);
        }


        public async Task<IActionResult> Delete(int id)
        {
            await _orderService.DeleteOrderDetailAsync(id);
            return RedirectToAction("ShowOrder");
        }

        public async Task<IActionResult> Command(int id, string command)
        {
            await _orderService.UpdateOrderDetailCommandAsync(id, command);
            return RedirectToAction("ShowOrder");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSumOrder(int orderId)
        {
            await _orderService.UpdateSumOrder(orderId);

            return RedirectToAction("ShowOrder");
        }

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
