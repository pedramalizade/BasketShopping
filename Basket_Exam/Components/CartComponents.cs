using Basket_Exam.Data;
using Basket_Exam.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Basket_Exam.Components
{
    public class CartComponents : ViewComponent
    {
        private readonly MyContext _myContext;
        public CartComponents(MyContext myContext)
        {
            _myContext = myContext; 
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<ShowCartViewModel> _list = new List<ShowCartViewModel>();
            if (User.Identity.IsAuthenticated)
            {
                string CurrentUserID = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var order = _myContext.Orders.SingleOrDefault(o => o.UserId == CurrentUserID && !o.IsFinally);
                if (order != null)
                {
                  var details = _myContext.OrderDetails.Where(d => d.OrderId == order.OrderId).ToList();
                    foreach (var item in details)
                    {
                        var product = _myContext.Products.Find(item.ProductId);
                        _list.Add(new ShowCartViewModel()
                        {
                            Count = item.Count,
                            Title = product.Title,
                            ImageName = product.ImageName
                        });
                    }
                }
            }
            return View("/Views/Shared/_ShowCart.cshtml", _list);
        }
    }
}
