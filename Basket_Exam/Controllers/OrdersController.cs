using Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ZarinpalSandbox;

namespace Basket_Exam.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            string currentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _orderRepository.AddToCart(id, currentUserID);

            return Redirect("/");
        }

        public async Task<IActionResult> ShowOrder()
        {
            string currentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orderList = await _orderRepository.GetUserOrderAsync(currentUserID);

            return View(orderList);
        }


        public async Task<IActionResult> Delete(int id)
        {
            await _orderRepository.DeleteOrderDetailAsync(id);
            return RedirectToAction("ShowOrder");
        }

        public async Task<IActionResult> Command(int id, string command)
        {
            await _orderRepository.UpdateOrderDetailCommandAsync(id, command);
            return RedirectToAction("ShowOrder");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSumOrder(int orderId)
        {
            await _orderRepository.UpdateSumOrder(orderId);

            return RedirectToAction("ShowOrder");
        }

        public async Task<IActionResult> Payment()
        {
            string currentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var redirectUrl = await _orderRepository.PaymentRequestAsync(currentUserID);

            if (redirectUrl == null)
            {
                return BadRequest();
            }

            return Redirect(redirectUrl);
        }
    }
}
