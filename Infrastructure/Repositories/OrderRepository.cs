namespace Infrastructure.Repositories
{
    public class OrderRepository : IOrderService
    {
        private readonly MyContext _context;
        public OrderRepository(MyContext context)
        {
            _context = context;
        }
        public async Task AddToCart(int productId, string userId)
        {
            var order = await _context.Orders
                .SingleOrDefaultAsync(o => o.UserId == userId && !o.IsFinally);

            if (order == null)
            {
                order = new Order
                {
                    UserId = userId,
                    CreateDate = DateTime.Now,
                    IsFinally = false,
                    Sum = 0
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                _context.OrderDetails.Add(new OrderDetail
                {
                    OrderId = order.OrderId,
                    Count = 5,
                    Price = (await _context.Products.FindAsync(productId)).Price,
                    ProductId = productId
                });

                await _context.SaveChangesAsync();
            }
            else
            {
                var details = await _context.OrderDetails
                    .SingleOrDefaultAsync(d => d.OrderId == order.OrderId && d.ProductId == productId);

                if (details == null)
                {
                    _context.OrderDetails.Add(new OrderDetail
                    {
                        OrderId = order.OrderId,
                        Count = 5,
                        Price = (await _context.Products.FindAsync(productId)).Price,
                        ProductId = productId
                    });
                }
                else
                {
                    details.Count += 1;
                    _context.Update(details);
                }

                await _context.SaveChangesAsync();
            }

            await UpdateSumOrder(order.OrderId);
        }

        public async Task DeleteOrderDetailAsync(int orderDetailId)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(orderDetailId);
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
                await _context.SaveChangesAsync();

                await UpdateSumOrder(orderDetail.OrderId);
            }
        }

        public async Task UpdateOrderDetailCommandAsync(int orderDetailId, string command)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(orderDetailId);

            if (orderDetail == null) return;

            switch (command)
            {
                case "up":
                    orderDetail.Count += 1;
                    _context.OrderDetails.Update(orderDetail);
                    break;

                case "down":
                    orderDetail.Count -= 1;
                    if (orderDetail.Count <= 0)
                    {
                        _context.OrderDetails.Remove(orderDetail);
                    }
                    else
                    {
                        _context.OrderDetails.Update(orderDetail);
                    }
                    break;
            }

            await _context.SaveChangesAsync();

            await UpdateSumOrder(orderDetail.OrderId);
        }

        public async Task<string?> PaymentRequestAsync(string userId)
        {
            var order = await _context.Orders
                .SingleOrDefaultAsync(o => o.UserId == userId && !o.IsFinally);

            if (order == null) return null;

            var payment = new Payment(order.Sum);
            var res = await payment.PaymentRequest(
                $"پرداخت فاکتور شماره {order.OrderId}",
                $"https://localhost:7162/Home/OnlinePayment/{order.OrderId}",
                "pedramalizade03@gmail.com",
                "09912214603");

            if (res.Status == 100)
            {
                return $"https://sandbox.zarinpal.com/pg/StartPay/{res.Authority}";
            }

            return null; 
        }

        public async Task<List<ShowOrderViewModel>> GetUserOrderAsync(string userId)
        {
            var order = await _context.Orders
                .SingleOrDefaultAsync(o => o.UserId == userId && !o.IsFinally);

            if (order == null)
            {
                return new List<ShowOrderViewModel>();
            }

            var query = from d in _context.OrderDetails
                        join p in _context.Products on d.ProductId equals p.ProductId
                        where d.OrderId == order.OrderId
                        select new ShowOrderViewModel
                        {
                            OrderDetailId = d.OrderDetailId,
                            ImageName = p.ImageName,
                            Title = p.Title,
                            Count = d.Count,
                            Price = d.Price,
                            Sum = d.Count * d.Price
                        };

            return await query.ToListAsync();
        }

        public async Task UpdateSumOrder(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.Sum = await _context.OrderDetails
                    .Where(o => o.OrderId == order.OrderId)
                    .Select(d => d.Count * d.Price)
                    .SumAsync();

                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
