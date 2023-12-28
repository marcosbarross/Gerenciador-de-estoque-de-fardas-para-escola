using EstoqueBackend.Model;

namespace EstoqueBackend.Data.DTOs
{
    public class ProdutoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Tamanho { get; set; }
        public double Preco { get; set; }
        public int QuantidadeRestante { get; set; }
        public int QuantidadeComprada { get; set; }
    }
}
