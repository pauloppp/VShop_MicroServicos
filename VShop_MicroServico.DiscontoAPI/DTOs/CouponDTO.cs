using System.ComponentModel.DataAnnotations;

namespace VShop_MicroServico.DiscontoAPI.DTOs
{
    public class CouponDTO
    {
        public int CouponId { get; set; }

        [Required]
        public string? CouponCode { get; set; }

        [Required]
        public decimal Discount { get; set; }
    }
}
