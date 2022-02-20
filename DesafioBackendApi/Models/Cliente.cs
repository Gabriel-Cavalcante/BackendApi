using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.ComponentModel.DataAnnotations;

namespace DesafioBackendApi.Models
{
    public class Cliente
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Email é obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Senha é obrigatória")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "CPF é obrigatório")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "Data de nascimento é obrigatória")]
        public DateTime DataNascimento { get; set; }

        public int Id { get; set; }

        public Cliente() { }

        public Cliente(string nome, string email, string senha, string cpf, DateTime dataNascimento)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Cpf = cpf;
            DataNascimento = dataNascimento;
        }
    }
}
