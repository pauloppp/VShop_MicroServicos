using System.ComponentModel.DataAnnotations;

namespace VShop_MicroServico.ProdutoWEB.Models
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }

        [Required]
        public string? Nome { get; set; }

        [Required]
        //[DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N2}")]
        //[Range(0, 999.99)]
        public decimal Preco { get; set; }

        [Required]
        public string? Descricao { get; set; }

        [Required]
        public long Estoque { get; set; }

        [Required]
        public string? ImagemURL { get; set; }

        public string? CategoriaNome { get; set; }

        [Display(Name = "Categorias")]
        public int CategoriaId { get; set; }
    }
}
