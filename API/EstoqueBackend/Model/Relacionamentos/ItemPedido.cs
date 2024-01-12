using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EstoqueBackend.Model.Relacionamentos
{
    public class ItemPedido
    {
        [Key]
        public int Id { get; set; }

        // Relacionamento com o Pedido
        [ForeignKey("PedidoId")]
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }

        // Relacionamento com o Produto
        [ForeignKey("ProdutoId")]
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }

        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        [Required]
        public int Quantidade { get; set; }
    }
}
