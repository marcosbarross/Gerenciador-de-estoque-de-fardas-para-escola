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

        [Route("/GetPedidos")]
        [HttpGet]
        public IActionResult GetPedidos()
        {
            try
            {
                using (var contexto = new EstoqueContext())
                {
                    var pedidos = contexto.Pedidos.ToList();

                    return Ok(pedidos);
                }
            }
            catch (Exception ex)
            {
                // Log do erro
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [Route("/EditarPedidos")]
        [HttpPut]
        public IActionResult EditarPedidos([FromBody] PedidoDTO pedidoDTO)
        {
            try
            {
                using (var contexto = new EstoqueContext())
                {
                    var pedidoExistente = contexto.Pedidos.Find(pedidoDTO.Id);

                    if (pedidoExistente == null)
                    {
                        return BadRequest("Pedido não encontrado");
                    }

                    var produtoExistente = contexto.Produtos.Find(Util.Util.RetornaIdComNomeTamanho(pedidoDTO, pedidoDTO.produtoNome, pedidoDTO.produtoTamanho));

                    if (produtoExistente == null)
                    {
                        return BadRequest("Produto não encontrado");
                    }

                    ItemController.ComprarItem(produtoExistente.Id, pedidoDTO.Quantidade);

                    pedidoExistente.Quantidade = pedidoDTO.Quantidade;
                    pedidoExistente.ProdutoId = produtoExistente.Id;

                    contexto.SaveChanges();

                    return Ok("Pedido editado com sucesso");
                }
            }
            catch (Exception ex)
            {
                // Log do erro
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [Route("/DeletarPedidos")]
        [HttpDelete]
        public IActionResult DeletarPedidos([FromBody] PedidoDTO pedidoDTO)
        {
            try
            {
                using (var contexto = new EstoqueContext())
                {
                    var pedidoExistente = contexto.Pedidos.Find(pedidoDTO.Id);

                    if (pedidoExistente.Id == null)
                    {
                        return BadRequest("Pedido não encontrado");
                    }
                    contexto.Pedidos.Remove(pedidoExistente);

                    contexto.SaveChanges();

                    return Ok("Pedido deletado com sucesso");
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