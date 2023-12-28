using EstoqueBackend.Data;
using EstoqueBackend.Data.DTOs;
using EstoqueBackend.Model;
using Microsoft.AspNetCore.Mvc;

namespace EstoqueBackend.Controllers
{
    [Route("/Cliente")]
    public class ClienteController : Controller
    {
        [Route("/AddCliente")]
        [HttpPost]
        public IActionResult AddCliente([FromBody] ClienteDTO clienteDTO)
        {
            using (var contexto = new EstoqueContext())
            {
                var cliente = new Cliente(clienteDTO);
                contexto.Clientes.Add(cliente);
                contexto.SaveChanges();
                return Ok(new { Message = "Cliente adicionado com sucesso.", ClienteId = cliente.Id });

            }

        }
    }
}
