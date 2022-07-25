using System.ComponentModel.DataAnnotations;

namespace VShop_MicroServico.ProdutoWEB.Models
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }

        [Required]
        public string? Nome { get; set; }

        [Required]
        [Range(0.01, 999999.99)]
        public decimal Preco { get; set; }

        [Required]
        public string? Descricao { get; set; }

        [Required]
        public long Estoque { get; set; }

        [Required]
        public string? ImagemURL { get; set; }

        public string? CategoriaNome { get; set; }

        public int Quantidade { get; set; } = 1;

        [Display(Name = "Categorias")]
        public int CategoriaId { get; set; }
    }
}
