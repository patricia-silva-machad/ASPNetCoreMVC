using System.ComponentModel.DataAnnotations.Schema;

namespace Lanches.Models {
    public class PedidoDetalhe 
    {

        public int PedidoDetalheId { get; set; }
        public int PedidoId { get; set; }
        public int LancheId { get; set; }
        public int Quantidade { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Preco { get; set; }

        //define um relacionamento 1:N e gera chaves estrangeiras: PedidoId(not null) LancheId(not null)
        public virtual Lanche Lanche { get; set; }
        public virtual Pedido Pedido { get; set; }

    }
}
