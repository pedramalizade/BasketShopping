namespace Basket_Exam.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string ImageName { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
