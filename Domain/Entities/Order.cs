namespace Basket_Exam.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public int Sum { get; set; }
        public bool IsFinally { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
