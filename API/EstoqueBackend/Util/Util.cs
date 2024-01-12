using EstoqueBackend.Data;
using EstoqueBackend.Data.DTOs;

namespace EstoqueBackend.Util
{
    public static class Util
    {
        public static int RetornaIdComNomeTamanho(ProdutoDTO produtoDTO, string nome, int tamanho)
        {
            using (var contexto = new EstoqueContext())
            {
                var produto = contexto.Produtos
                .Where(p => p.Nome == produtoDTO.Nome && p.Tamanho == produtoDTO.Tamanho)
                .Select(p => p.Id)
                    .FirstOrDefault();
                return produto;
            }
        }
        public static int RetornaIdComNomeTamanho(PedidoDTO pedido, string nome, int tamanho)
        {
            using (var contexto = new EstoqueContext())
            {
                var produto = contexto.Produtos
                .Where(p => p.Nome == pedido.produtoNome && p.Tamanho == pedido.produtoTamanho)
                .Select(p => p.Id)
                    .FirstOrDefault();
                return produto;
            }
        }
    }
}
