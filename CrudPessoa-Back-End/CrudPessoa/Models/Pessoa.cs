using System;

namespace CrudPessoa.Models
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public string Rg { get; set; }
        public string Cpf { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public DateTime DataRegistro { get; set; }
        public DateTime DataAtualização { get; set; }
    }
}
