namespace VShop_MicroServico.ProdutoWEB.Models
{
    public class CarrinhoViewModel
    {
        public CarrinhoCabecViewModel CarrinhoCabec { get; set; } = new CarrinhoCabecViewModel();
        public IEnumerable<CarrinhoItemViewModel> CarrinhoItems { get; set; } = Enumerable.Empty<CarrinhoItemViewModel>();
    }
}
