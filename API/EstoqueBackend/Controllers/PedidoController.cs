using Microsoft.AspNetCore.Mvc;
using EstoqueBackend.Data;
using EstoqueBackend.Data.DTOs;
using EstoqueBackend.Model;
using EstoqueBackend.Util;

namespace EstoqueBackend.Controllers
{
    [Route("/Pedidos")]
    public class PedidoController : Controller
    {

        [Route("/AddPedido")]
        [HttpPost]
        public IActionResult AddPedido([FromBody] PedidoDTO pedidoDTO)
        {
            try
            {
                using (var contexto = new EstoqueContext())
                {
                    var produtoExistente = contexto.Produtos.Find(Util.Util.RetornaIdComNomeTamanho(pedidoDTO, pedidoDTO.produtoNome, pedidoDTO.produtoTamanho));

                    if (produtoExistente == null)
                    {
                        return BadRequest("Produto não encontrado");
                    }
                    var novoPedido = new Pedido(pedidoDTO);
                    
                    ItemController.ComprarItem(produtoExistente.Id, pedidoDTO.Quantidade);
                    
                    novoPedido.ProdutoId = produtoExistente.Id;

                    contexto.Pedidos.Add(novoPedido);

                    produtoExistente.QuantidadeRestante -= pedidoDTO.Quantidade;

                    contexto.SaveChanges();

                    return Ok("Pedido adicionado com sucesso");
                }
            }
            catch (Exception ex)
            {
                // Log do erro
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}
