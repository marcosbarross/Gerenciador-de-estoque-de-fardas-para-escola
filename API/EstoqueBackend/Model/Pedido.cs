using EstoqueBackend.Data.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EstoqueBackend.Model
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NomeCliente { get; set; }
        [Required]
        [ForeignKey("Produto")]
        public int ProdutoId { get; set; }
        [Required]
        public int Quantidade { get; set; }
        public int produtoTamanho { get; set; }
        public String produtoNome { get; set; }

        public Pedido() { }
        public Pedido(PedidoDTO pedidoDTO)
        {
            Id = pedidoDTO.Id;
            NomeCliente = pedidoDTO.NomeCliente;
            ProdutoId = pedidoDTO.ProdutoId;
            produtoTamanho = pedidoDTO.produtoTamanho;
            produtoNome = pedidoDTO.produtoNome;
            Quantidade = pedidoDTO.Quantidade;
        }
    }
}
