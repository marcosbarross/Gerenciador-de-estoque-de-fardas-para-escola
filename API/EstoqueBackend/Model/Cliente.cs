using EstoqueBackend.Data.DTOs;
using EstoqueBackend.Model.Relacionamentos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EstoqueBackend.Model
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NomeCliente { get; set; }

        // Lista de itens do pedido
        public List<ItemPedido> Itens { get; set; }
        public Cliente() { }

        // Construtor que aceita o nome do cliente como argumento
        public Cliente(ClienteDTO clienteDTO)
        {
            NomeCliente = clienteDTO.NomeCliente;
        }
    }
}
