using Basket_Exam.Data;
using Basket_Exam.Models;
using Basket_Exam.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ZarinpalSandbox;

namespace Basket_Exam.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly MyContext _context;
        public OrdersController(MyContext context)
        {
            _context = context;
        }
        public IActionResult AddToCart(int id)
        {
            string CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Order order = _context.Orders.SingleOrDefault(o => o.UserId == CurrentUserID && !o.IsFinally);
            if (order == null)
            {
                order = new Order()
                {
                    UserId = CurrentUserID,
                    CreateDate = DateTime.Now,
                    IsFinally = false,
                    Sum = 0
                };
                _context.Orders.Add(order);
                _context.SaveChanges();
                _context.OrderDetails.Add(new OrderDetail()
                {
                    OrderId = order.OrderId,
                    Count = 5,
                    Price = _context.Products.Find(id).Price,
                    ProductId = id
                });
                _context.SaveChanges();
            }
            else
            {
                var details = _context.OrderDetails.SingleOrDefault(d => d.OrderId == order.OrderId && d.ProductId == id);
                if (details == null)
                {
                    _context.OrderDetails.Add(new OrderDetail()
                    {
                        OrderId = order.OrderId,
                        Count = 5,
                        Price = _context.Products.Find(id).Price,
                        ProductId = id
                    });
                }
                else
                {
                    details.Count += 1;
                    _context.Update(details);
                }
                _context.SaveChanges();
            }
            UpdateSumOrder(order.OrderId);
            return Redirect("/");
        }

        public IActionResult ShowOrder()
        {
            string CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Order order = _context.Orders.SingleOrDefault(o => o.UserId == CurrentUserID && !o.IsFinally);
            List<ShowOrderViewModel> _list = new List<ShowOrderViewModel>();
            if (order != null)
            {
                var details = _context.OrderDetails.Where(d => d.OrderId == order.OrderId).ToList();
                foreach (var item in details)
                {
                    var product = _context.Products.Find(item.ProductId);
                    _list.Add(new ShowOrderViewModel()
                    {
                        Count = item.Count,
                        ImageName = product.ImageName,
                        OrderDetailId = item.OrderDetailId,
                        Price = item.Price,
                        Sum = item.Count * item.Price,
                        Title = product.Title
                    });
                }
            }
            return View(_list);
        }

        public IActionResult Delete(int id)
        {
            var orderDetail = _context.OrderDetails.Find(id);
            _context.Remove(orderDetail);
            _context.SaveChanges();
            return RedirectToAction("ShowOrder");
        }
        public IActionResult Command(int id, string command)
        {
            var orderDetail = _context.OrderDetails.Find(id);
            switch (command)
            {
                case "up":
                    {
                        orderDetail.Count += 1;
                        break;
                    }
                case "down":
                    {
                        orderDetail.Count -= 1;
                        if(orderDetail.Count == 0)
                        {
                            _context.OrderDetails.Remove(orderDetail);
                        }
                        else
                        {
                            _context.Update(orderDetail);
                        }
                        break;
                    }
            }


            _context.SaveChanges();
            return RedirectToAction("ShowOrder");
        }
        public void UpdateSumOrder(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            order.Sum = _context.OrderDetails.Where(o => o.OrderId == order.OrderId).Select(d => d.Count * d.Price).Sum();

            _context.Update(order);
            _context.SaveChanges();
        }

        public IActionResult Payment()
        {
            var order = _context.Orders.SingleOrDefault(o => !o.IsFinally);
            if (order == null)
            {
                return NotFound();
            }
            var payment = new Payment(order.Sum);
            var res = payment.PaymentRequest($"پرداخت فاکتور شماره {order.OrderId}",
                "https://localhost:7162/Home/OnlinePayment/" + order.OrderId, "pedramalizade03@gmail.com", "09912214603");

            if(res.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            }
            else
            {
                return BadRequest();
            }
            return null;
        }
    }
}
