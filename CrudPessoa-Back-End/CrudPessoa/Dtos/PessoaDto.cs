using System;

namespace CrudPessoa.Dtos
{
    public class PessoaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Rg { get; set; }
        public string Cpf { get; set; }
        public string DataDeNascimento { get; set; }
        public string DataDeRegistro { get; set; }
        public string DataDeAtualizacao { get; set; }
    }
}
