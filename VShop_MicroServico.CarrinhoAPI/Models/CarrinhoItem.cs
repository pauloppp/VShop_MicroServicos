namespace VShop_MicroServico.CarrinhoAPI.Models
{
    public class CarrinhoItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; } = 1;
        public int ProdutoId { get; set; }
        public int CarrinhoCabecId { get; set; }
        public Produto Produto { get; set; } = new Produto();
        public CarrinhoCabec CarrinhoCabec { get; set; } = new CarrinhoCabec();
    }
}
