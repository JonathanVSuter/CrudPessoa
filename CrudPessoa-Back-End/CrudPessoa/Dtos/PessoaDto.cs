using System;

namespace CrudPessoa.Dtos
{
    public class PessoaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Rg { get; set; }
        public string Cpf { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public DateTime DataDeRegistro { get; set; }
        public DateTime DataDeAtualizacao { get; set; }
    }
}
