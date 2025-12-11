namespace Basket_Exam.Models.ViewModels
{
    public class ShowCartViewModel
    {
        /// <summary>
        /// نام تصویر محصول.
        /// </summary>
        public string ImageName { get; set; }

        /// <summary>
        /// عنوان محصول.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// تعداد محصول در سبد خرید.
        /// </summary>
        public int Count { get; set; }
    }
}
