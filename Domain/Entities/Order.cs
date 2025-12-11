namespace Basket_Exam.Models
{
    public class Order
    {
        /// <summary>
        /// شناسه یکتا برای هر سفارش.
        /// </summary>
        [Key]
        public int OrderId { get; set; }
        /// <summary>
        /// شناسه کاربری صاحب سفارش.
        /// </summary>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// تاریخ و زمان ایجاد سفارش.
        /// </summary>
        [Required]
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// مجموع مبلغ سفارش (جمع اقلام).
        /// </summary>
        [Required]
        public int Sum { get; set; }

        /// <summary>
        /// تعیین می‌کند که سفارش نهایی (پرداخت‌شده) هست یا نه.
        /// </summary>
        public bool IsFinally { get; set; }

        /// <summary>
        /// لیست جزئیات اقلام موجود در سفارش.
        /// </summary>
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
