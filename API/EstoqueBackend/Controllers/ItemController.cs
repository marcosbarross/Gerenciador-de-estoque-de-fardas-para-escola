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
                // Buscar o ID do produto pelo nome e tamanho
                var produtoId = contexto.Produtos
                    .Where(p => p.Nome == produtoDTO.Nome && p.Tamanho == produtoDTO.Tamanho)
                    .Select(p => p.Id)
                    .FirstOrDefault();

                if (produtoId != 0)
                {
                    // Produto encontrado, agora você pode usar o ID
                    var produtoExistente = contexto.Produtos.Find(produtoId);
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


        [Route("/RemoverItem")]
        [HttpDelete]
        public IActionResult RemoveItem([FromBody] ProdutoDTO produtoDTO)
        {
            using (var contexto = new EstoqueContext())
            {
                var produtoExistente = contexto.Produtos.FirstOrDefault(p => p.Nome == produtoDTO.Nome || p.Id == produtoDTO.Id);

                if (produtoExistente != null)
                {
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
                // Projeção anônima para selecionar apenas as propriedades desejadas
                var produtos = contexto.Produtos
                    .Select(p => new
                    {
                        nome = p.Nome,
                        tamanho = p.Tamanho,
                        preco = p.Preco,
                        quantidadeRestante = p.QuantidadeRestante,
                    })
                    .ToList();

                return Ok(produtos);
            }
        }
    }
}
