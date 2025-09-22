namespace Basket_Exam.Jobs
{
    [DisallowConcurrentExecution]
    public class RemoveCartJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var options = new DbContextOptionsBuilder<MyContext>();
            options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BasketDB;Trusted_Connection=True;MultipleActiveResultSets=true");

            using (MyContext _context = new MyContext(options.Options))
            {
                var orders = _context.Orders.Where(o => !o.IsFinally && o.CreateDate < DateTime.Now.AddHours(-24)).ToList();
                foreach (var order in orders)
                {
                    var orderDetail = _context.OrderDetails.Where(od => od.OrderId == order.OrderId).ToList();
                    foreach (var detail in orderDetail)
                    {
                        _context.Remove(detail);
                    }
                    _context.Remove(order);
                }
                _context.SaveChanges();
            }
                return Task.CompletedTask;  
        }
    }
}
