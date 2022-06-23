namespace VShop_MicroServicos.ProdutoAPI.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string? Nome { get; set; }

        // Relacionamento 1:N -- Propriedades de Navegação
        public ICollection<Produto>? Produtos { get; set; }
    }
}
