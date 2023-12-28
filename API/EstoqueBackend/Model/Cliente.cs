using System.ComponentModel.DataAnnotations;
using EstoqueBackend.Data.DTOs;

namespace EstoqueBackend.Model
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Aluno { get; set; }
        [Required] 
        public string Nome { get; set; }

        Cliente() { }

        public Cliente(ClienteDTO clienteDTO)
        {
            Aluno = clienteDTO.Aluno;
            Nome = clienteDTO.nome;
        }
    }
}
