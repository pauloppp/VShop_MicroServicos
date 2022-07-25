namespace VShop_MicroServico.ProdutoWEB.Models
{
    public class CarrinhoItemViewModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; } = 1;
        public int ProdutoId { get; set; }
        public int CarrinhoCabecDTOId { get; set; }
        public ProdutoViewModel Produto { get; set; } = new ProdutoViewModel();
    }
}
