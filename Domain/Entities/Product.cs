namespace Basket_Exam.Models
{
    public class Product
    {
        /// <summary>
        /// شناسه یکتای محصول.
        /// </summary>
        [Key]
        public int ProductId { get; set; }

        /// <summary>
        /// عنوان محصول.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        /// <summary>
        /// توضیحات کامل محصول.
        /// </summary>
        [Required]
        public string Text { get; set; }

        /// <summary>
        /// تعداد موجودی محصول.
        /// </summary>
        [Required]
        public int Count { get; set; }

        /// <summary>
        /// قیمت واحد محصول.
        /// </summary>
        [Required]
        public int Price { get; set; }

        /// <summary>
        /// نام فایل تصویر محصول.
        /// </summary>
        [Required]
        public string ImageName { get; set; }

        /// <summary>
        /// لیست جزئیات سفارش‌هایی که این محصول در آن‌ها ثبت شده است.
        /// </summary>
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
