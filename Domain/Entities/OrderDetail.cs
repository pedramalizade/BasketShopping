namespace Basket_Exam.Models
{
    public class OrderDetail
    {
        /// <summary>
        /// شناسه یکتای جزئیات سفارش.
        /// </summary>
        [Key]
        public int OrderDetailId { get; set; }

        /// <summary>
        /// شناسه سفارش مرتبط.
        /// </summary>
        [Required]
        public int OrderId { get; set; }

        /// <summary>
        /// شناسه محصول موجود در سفارش.
        /// </summary>
        [Required]
        public int ProductId { get; set; }

        /// <summary>
        /// تعداد محصول سفارش‌داده‌شده.
        /// </summary>
        [Required]
        public int Count { get; set; }

        /// <summary>
        /// قیمت واحد محصول در لحظه ثبت سفارش.
        /// </summary>
        [Required]
        public int Price { get; set; }

        /// <summary>
        /// اطلاعات محصول مرتبط.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// اطلاعات سفارش مرتبط.
        /// </summary>
        public Order Order { get; set; }
    }
}
