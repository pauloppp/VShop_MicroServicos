using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VShop_MicroServicos.ProdutoAPI.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }

        [DataType(DataType.Currency)]
        public decimal Preco { get; set; }

        public string? Descricao { get; set; }
        public long Estoque { get; set; }
        public string? ImagemURL { get; set; }

        // Relacionamento N:1 -- Propriedades de Navegação
        public Categoria? Categoria { get; set; }
        public int CategoriaId { get; set; }
    }
}
