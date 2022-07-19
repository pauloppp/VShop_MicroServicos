namespace VShop_MicroServico.CarrinhoAPI.DTOs
{
    public class CarrinhoDTO
    {
        public CarrinhoCabecDTO CarrinhoCabec { get; set; } = new CarrinhoCabecDTO();
        public IEnumerable<CarrinhoItemDTO> CarrinhoItems { get; set; } = Enumerable.Empty<CarrinhoItemDTO>();
    }
}
