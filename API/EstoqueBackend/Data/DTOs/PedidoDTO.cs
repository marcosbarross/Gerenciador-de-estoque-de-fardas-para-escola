using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EstoqueBackend.Data.DTOs
{
    public class PedidoDTO
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public int ProdutoId { get; set; }
        public int produtoTamanho { get; set; }
        public String produtoNome { get; set; }
        public int Quantidade { get; set; }
    }
}
