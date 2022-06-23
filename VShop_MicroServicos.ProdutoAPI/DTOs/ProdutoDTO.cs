using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using VShop_MicroServicos.ProdutoAPI.Models;

namespace VShop_MicroServicos.ProdutoAPI.DTOs
{
    public class ProdutoDTO // Ou ViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [MinLength(3)]
        [MaxLength(100)]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo Preco é obrigatório")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "O campo Descricao é obrigatório")]
        [MinLength(3)]
        [MaxLength(255)]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "O campo Estoque é obrigatório")]
        [Range(1, 9999)]
        public long Estoque { get; set; }

        public string? ImagemURL { get; set; }
        
        public string? CategoriaNome { get; set; }

        [JsonIgnore] // Elimina esse campo no retorno da API como null.
        public Categoria? Categoria { get; set; }

        public int CategoriaId { get; set; }
    }
}
