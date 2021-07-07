namespace BestCodify.Models
{
    public class StriperPaymentDto
    {
        public string ProductName { get; set; }
        public decimal Amount { get; set; }
        public string ImageUrl { get; set; }
        public string ReturnUrl { get; set; }
    }
}
