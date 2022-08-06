namespace VShop_MicroServico.CarrinhoAPI.DTOs
{
    public class ProdutoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public long Estoque { get; set; }
        public string ImagemURL { get; set; } = string.Empty;
        public string CategoriaNome { get; set; } = string.Empty;
    }
}
