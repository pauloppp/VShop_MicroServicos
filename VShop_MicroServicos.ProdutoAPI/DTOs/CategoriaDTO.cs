using System.ComponentModel.DataAnnotations;
using VShop_MicroServicos.ProdutoAPI.Models;

namespace VShop_MicroServicos.ProdutoAPI.DTOs
{
    public class CategoriaDTO // Ou ViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [MinLength(3)]
        [MaxLength(100)]
        public string? Nome { get; set; }


        public ICollection<Produto>? Produtos { get; set; }
    }
}
