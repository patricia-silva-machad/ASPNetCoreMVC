using Lanches.Context;
using Microsoft.EntityFrameworkCore;

namespace Lanches.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;
        public CarrinhoCompra(AppDbContext context)
        {
            _context = context;
        }
        public string CarrinhoCompraId{get; set;}
        public List<CarrinhoCompraItem> CarrinhoCompraItems {get; set;}
        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            //define uma sessão
            ISession session =
                services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            //obtem um serviço do tipo do nosso contexto
            var context = services.GetService<AppDbContext>();
            //obtem ou gera o ID do carrinho da session
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();
            //se for nulo ?? atribui o id do carrinho na sessão
            session.SetString("CarrinhoId", carrinhoId);
            //retorna o carrinho com o contexto e o Id atribuido ou obtido
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId
            };
        }
        public void AdicionarAoCarrinho(Lanche lanche)
        {   
            //verificando se o lanche existe na tabela Carrinho compra itens
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                s => s.Lanche.LancheId == lanche.LancheId && 
                s.CarrinhoCompraId == CarrinhoCompraId);
            if(carrinhoCompraItem == null) 
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade = 1
                };
                _context.CarrinhoCompraItens.Add(carrinhoCompraItem);
            } 
            else
            {
                carrinhoCompraItem.Quantidade++;
            }
            _context.SaveChanges();
        }

        public int RemoverDoCarrinho(Lanche lanche)
        {
            //verificando se o lanche existe na tabela Carrinho compra itens
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                s => s.Lanche.LancheId == lanche.LancheId && 
                s.CarrinhoCompraId == CarrinhoCompraId);
            var quantidadeLocal = 0;
            if(carrinhoCompraItem != null)
            {
                if(carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--;
                    quantidadeLocal = carrinhoCompraItem.Quantidade;
                }
                else
                {
                    _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
                }
            }
            _context.SaveChanges();
            return quantidadeLocal;
        }

        // retorna uma instancia dos itens do carrinho de compras se nao for null, se for nula vai obter todos os carrinhos com seus itens na tabela de carrinhoCompras.
        public List<CarrinhoCompraItem> GetCarrinhoCompraItems()
        {
            return CarrinhoCompraItems ??
                (CarrinhoCompraItems =
                    _context.CarrinhoCompraItens
                    .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                    .Include(s => s.Lanche)
                    .ToList());
        }
        // localiza no carrinho de compras todos os carrinhos com id expecifico e usa o metodo removeRange, removendo todos os itens de um carrinho de compra com um id expecifico. 
        public void LimparCarrinho()
        {
            var carrinhoItens = _context.CarrinhoCompraItens
                    .Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);
            
            _context.CarrinhoCompraItens.RemoveRange(carrinhoItens);
            _context.SaveChanges();
        }

        // retorna um decimal do total de todos os itens de um carrinho de compras, usa a instancia de contexto, filtrando o carrinho com o id, seleciona do carrinho o preço e a quantidade e exibe o total do carrinho.
        
        public void GetCarrinhoCompraTotal()
        {
            var total = _context.CarrinhoCompraItens
                .Where(c => c.CarrinhoCompraId ==CarrinhoCompraId)
                .Select(c => c.Lanche.Preco * c.Quantidade).Sum();
        }
    }
}