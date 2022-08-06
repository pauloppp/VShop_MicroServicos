namespace VShop_MicroServico.ProdutoWEB.Models
{
    public class CarrinhoCabecViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string CouponCode { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; } = 0.00m;
    }
}
