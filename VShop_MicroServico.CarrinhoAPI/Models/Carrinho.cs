namespace VShop_MicroServico.CarrinhoAPI.Models
{
    public class Carrinho
    {
        public CarrinhoCabec CarrinhoCabec { get; set; } = new CarrinhoCabec();
        public IEnumerable<CarrinhoItem> CarrinhoItems { get; set; } = Enumerable.Empty<CarrinhoItem>();
    }
}
