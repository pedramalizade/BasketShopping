namespace Basket_Exam.Models.ViewModels
{
    public class ShowOrderViewModel
    {
        /// <summary>
        /// شناسه جزئیات سفارش.
        /// </summary>
        public int OrderDetailId { get; set; }

        /// <summary>
        /// نام تصویر محصول.
        /// </summary>
        public string ImageName { get; set; }

        /// <summary>
        /// عنوان محصول.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// تعداد محصول در این آیتم سفارش.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// قیمت واحد محصول.
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// مبلغ کل (قیمت × تعداد).
        /// </summary>
        public int Sum { get; set; }
    }
}
