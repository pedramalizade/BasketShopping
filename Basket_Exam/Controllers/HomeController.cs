namespace Basket_Exam.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyContext _myContext;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, MyContext myContext)
        {
            _myContext = myContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(_myContext.Products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult OnlinePayment(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"].ToString();
                var order = _myContext.Orders.Find(id);
                var payment = new Payment(order.Sum);
                var res = payment.Verification(authority).Result;
                if(res.Status == 100)
                {
                    order.IsFinally = true;
                    _myContext.Orders.Update(order);
                    _myContext.SaveChanges();
                    ViewBag.Code = res.RefId;
                    return View();
                }

            }
            return NotFound();
        }
    }
}
