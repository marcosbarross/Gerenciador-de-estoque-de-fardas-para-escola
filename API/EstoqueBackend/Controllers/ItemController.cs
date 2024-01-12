using System.Net;
using EstoqueBackend.Data;
using EstoqueBackend.Data.DTOs;
using EstoqueBackend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EstoqueBackend.Controllers
{
    [Route("/item")]
    public class ItemController : Controller
    {
        [Route("/AddItem")]
        [HttpPost]
        public IActionResult AddItem([FromBody] ProdutoDTO produtoDTO)
        {
            using (var contexto = new EstoqueContext())
            {
                var produto = new Produto(produtoDTO);
                contexto.Produtos.Add(produto);
                contexto.SaveChanges();
                return Ok();
            }
        }

        [Route("/ComprarItem")]
        [HttpPut]
        public IActionResult ComprarItem([FromBody] ProdutoDTO produtoDTO)
        {
            using (var contexto = new EstoqueContext())
            {
                var produtoId = Util.Util.RetornaIdComNomeTamanho(produtoDTO, produtoDTO.Nome, produtoDTO.Tamanho);
                if (produtoId != 0)
                {
                    // Produto encontrado, agora você pode usar o ID
                    var produtoExistente = contexto.Produtos.Find(produtoId);

                    //PedidoController.AddPedido(produtoExistente.Id, produtoDTO.QuantidadeComprada);

                    produtoExistente.QuantidadeRestante -= produtoDTO.QuantidadeComprada;
                    contexto.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest("Produto não existe");
                }
            }
        }

        public static void ComprarItem(int produtoId, int quantidadeComprada)
        {
            using (var contexto = new EstoqueContext())
            {
                var produtoExistente = contexto.Produtos.Find(produtoId);
                produtoExistente.QuantidadeRestante -= quantidadeComprada;
                contexto.SaveChanges();
            }
        }

        [Route("/EditarItem")]
        [HttpPut]
        public IActionResult EditarItem([FromBody] ProdutoDTO produtoDTO)
        {
            using (var contexto = new EstoqueContext()) {
                var produtoId = Util.Util.RetornaIdComNomeTamanho(produtoDTO, produtoDTO.Nome, produtoDTO.Tamanho); ;
                if (produtoId != 0)
                {
                    // Produto encontrado, agora você pode usar o ID
                    var produtoExistente = contexto.Produtos.Find(produtoId);
                    produtoExistente.Nome = produtoDTO.Nome;
                    produtoExistente.Tamanho = produtoDTO.Tamanho;
                    produtoExistente.Preco = produtoDTO.Preco;
                    produtoExistente.QuantidadeRestante = produtoDTO.QuantidadeRestante;
                    contexto.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest("Produto não existe");
                }
            }
        }
     


        [Route("/RemoverItem")]
        [HttpDelete]
        public IActionResult RemoveItem([FromBody] ProdutoDTO produtoDTO)
        {
            using (var contexto = new EstoqueContext())
            {
                var produtoId = Util.Util.RetornaIdComNomeTamanho(produtoDTO, produtoDTO.Nome, produtoDTO.Tamanho);
                if (produtoId != 0)
                {
                    var produtoExistente = contexto.Produtos.Find(produtoId);
                    contexto.Produtos.Remove(produtoExistente);
                    contexto.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest("Produto não existe");
                }
            }
        }

        [Route("/ListarItens")]
        [HttpGet]
        public IActionResult ListarItens()
        {
            using (var contexto = new EstoqueContext())
            {
                var produtos = contexto.Produtos
                    .GroupBy(p => p.Nome)
                    .Select(group => new
                    {
                        nome = group.Key, // Chave do grupo é o nome
                        tamanho = group.First().Tamanho,
                        preco = group.First().Preco,
                        quantidadeRestante = group.First().QuantidadeRestante,
                    })
                    .ToList();

                return Ok(produtos);
            }
        }
    }
}
