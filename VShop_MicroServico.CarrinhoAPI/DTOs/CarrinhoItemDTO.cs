namespace VShop_MicroServico.CarrinhoAPI.DTOs
{
    public class CarrinhoItemDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; } = 1;
        public int ProdutoId { get; set; }
        public int CarrinhoCabecDTOId { get; set; }
        public ProdutoDTO Produto { get; set; } = new ProdutoDTO();
        public CarrinhoCabecDTO CarrinhoCabec { get; set; } = new CarrinhoCabecDTO();
    }
}
