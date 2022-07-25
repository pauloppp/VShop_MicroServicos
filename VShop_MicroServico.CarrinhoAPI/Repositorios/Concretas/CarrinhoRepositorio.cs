using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VShop_MicroServico.CarrinhoAPI.Contexto;
using VShop_MicroServico.CarrinhoAPI.DTOs;
using VShop_MicroServico.CarrinhoAPI.Models;
using VShop_MicroServico.CarrinhoAPI.Repositorios.Interfaces;

namespace VShop_MicroServico.CarrinhoAPI.Repositorios.Concretas
{
    public class CarrinhoRepositorio : ICarrinhoRepositorio
    {
        private readonly CarrinhoAppDbContext _context;
        private readonly IMapper _mapper;

        public CarrinhoRepositorio(CarrinhoAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CarrinhoDTO> GetCarrinhoByUserIdAsync(string userId)
        {
            Carrinho carrinho = new Carrinho();

            // Obter o cabec pelo userId
            carrinho.CarrinhoCabec = await _context.CarrinhoCabecs.FirstOrDefaultAsync(c => c.UserId == userId);

            // Obter os ítens
            carrinho.CarrinhoItems = _context.CarrinhoItems.Where(c => c.CarrinhoCabecId == carrinho.CarrinhoCabec.Id).Include(c => c.Produto);

            return _mapper.Map<CarrinhoDTO>(carrinho);
        }

        public async Task<bool> ExcluirItemCarrinhoAsync(int carrinhoItemId)
        {
            try
            {
                CarrinhoItem carrinhoItem = await _context.CarrinhoItems.FirstOrDefaultAsync(c => c.Id == carrinhoItemId);

                // Obter qtde total de ítens do carrinho
                int qtdeItensCarrinho = _context.CarrinhoItems.Where(c => c.CarrinhoCabecId == carrinhoItem.CarrinhoCabecId).Count();

                _context.CarrinhoItems.Remove(carrinhoItem);

                if (qtdeItensCarrinho == 1)
                {
                    // Se quantidade = 1, então remove cabec pois, não existem mais ítens após a exclusão do único ítem.
                    var carrinhoCabecRemove = await _context.CarrinhoCabecs.FirstOrDefaultAsync(c => c.Id == carrinhoItem.CarrinhoCabecId);
                    _context.CarrinhoCabecs.Remove(carrinhoCabecRemove);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> LimparCarrinhoAsync(string userId)
        {
            var carrinhoCabec = await _context.CarrinhoCabecs.FirstOrDefaultAsync(c => c.UserId == userId);

            if (carrinhoCabec is not null)
            {
                _context.CarrinhoItems.RemoveRange(_context.CarrinhoItems.Where(c => c.CarrinhoCabecId == carrinhoCabec.Id));

                _context.CarrinhoCabecs.Remove(carrinhoCabec);

                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CarrinhoDTO> AtualizarCarrinhoAsync(CarrinhoDTO carrinhoDTO)
        {
            Carrinho carrinho = _mapper.Map<Carrinho>(carrinhoDTO);

            // Salva o produto no BancoDeDados se ele não existir 
            await SalvarProduto_InDataBase(carrinhoDTO, carrinho);

            //Verifica se o CarrinhoCabec é null
            var carrinhoCabec = await _context.CarrinhoCabecs.AsNoTracking().FirstOrDefaultAsync(c => c.UserId == carrinho.CarrinhoCabec.UserId);
            if (carrinhoCabec is null)
            {
                // Criar o Cabec e os itens
                await CriarCarrinhoCabec_e_Items(carrinho);
            }
            else
            {
                // Atualiza a quantidade e os itens
                await AtualizarQuantidade_e_Items(carrinhoDTO, carrinho, carrinhoCabec);
            }
            return _mapper.Map<CarrinhoDTO>(carrinho);
        }

        private async Task AtualizarQuantidade_e_Items(CarrinhoDTO carrinhoDTO, Carrinho carrinho, CarrinhoCabec carrinhoCabec)
        {
            // Se CarrinhoCabec não é null, verifica se CarrinhoItems possui o mesmo produto
            var carrinhoDetalhe = await _context.CarrinhoItems.AsNoTracking().FirstOrDefaultAsync(
                                   p => p.ProdutoId == carrinhoDTO.CarrinhoItems.FirstOrDefault()
                                   .ProdutoId && p.CarrinhoCabecId == carrinhoCabec.Id);

            if (carrinhoDetalhe is null)
            {
                // Cria o CarrinhoItems
                carrinho.CarrinhoItems.FirstOrDefault().CarrinhoCabecId = carrinhoCabec.Id;
                carrinho.CarrinhoItems.FirstOrDefault().Produto = null;
                _context.CarrinhoItems.Add(carrinho.CarrinhoItems.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
            else
            {
                // Atualiza a quantidade e o CarrinhoItems
                carrinho.CarrinhoItems.FirstOrDefault().Produto = null;
                carrinho.CarrinhoItems.FirstOrDefault().Quantity += carrinhoDetalhe.Quantity;
                carrinho.CarrinhoItems.FirstOrDefault().Id = carrinhoDetalhe.Id;
                carrinho.CarrinhoItems.FirstOrDefault().CarrinhoCabecId = carrinhoDetalhe.CarrinhoCabecId;
                _context.CarrinhoItems.Update(carrinho.CarrinhoItems.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
        }

        private async Task CriarCarrinhoCabec_e_Items(Carrinho carrinho)
        {
            //Cria o CarrinhoCabec e o CarrinhoItems
            _context.CarrinhoCabecs.Add(carrinho.CarrinhoCabec);
            await _context.SaveChangesAsync();

            carrinho.CarrinhoItems.FirstOrDefault().CarrinhoCabecId = carrinho.CarrinhoCabec.Id;
            carrinho.CarrinhoItems.FirstOrDefault().Produto = null;

            _context.CarrinhoItems.Add(carrinho.CarrinhoItems.FirstOrDefault());

            await _context.SaveChangesAsync(); 
        }

        private async Task SalvarProduto_InDataBase(CarrinhoDTO carrinhoDTO, Carrinho carrinho)
        {
            //Verifica se o produto ja foi salvo, senão salva
            var product = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == carrinhoDTO.CarrinhoItems.FirstOrDefault().ProdutoId);

            if (product is null)
            {
                _context.Produtos.Add(carrinho.CarrinhoItems.FirstOrDefault().Produto);
                await _context.SaveChangesAsync();
            }
        }

        public Task<bool> AplicarCouponAsync(string userId, string couponCode)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExcluirCouponAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
