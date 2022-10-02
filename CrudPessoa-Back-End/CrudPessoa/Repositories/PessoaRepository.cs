using CrudPessoa.Dtos;
using CrudPessoa.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CrudPessoa.Repositories
{
    public class PessoaRepository
    {
        private readonly string _connection = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=CrudPessoa;Integrated Security=True;Pooling=False";
        public PessoaDto Salvar(Pessoa pessoa)
        {
            PessoaDto pessoaDto = new PessoaDto();
            try
            {
                var query = @"INSERT INTO Pessoa 
                              (Nome, Cpf, Rg, DataDeNascimento) 
                              OUTPUT Inserted.Id
                              VALUES (@nome,@cpf,@rg,@dataNascimento)";
                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        nome = pessoa.Nome,
                        cpf = pessoa.Cpf,
                        rg = pessoa.Rg,
                        dataNascimento = pessoa.DataDeNascimento,
                        dataAtualizacao = DateTime.Now
                    };
                    pessoaDto.Id = (int)connection.ExecuteScalar(query, parametros);
                    pessoaDto.Nome = pessoa.Nome;
                    pessoaDto.Cpf = pessoa.Cpf;
                    pessoaDto.Rg = pessoa.Rg;
                    pessoaDto.DataDeNascimento = pessoa.DataDeNascimento;
                    pessoaDto.DataDeAtualização = pessoa.DataAtualização;
                    pessoaDto.DataDeRegistro = pessoa.DataRegistro;
                }

                return pessoaDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
        public PessoaDto Alterar(PessoaDto pessoa)
        {
            try
            {
                var query = @"UPDATE Pessoa SET Nome = @nome, Cpf = @cpf, Rg = @rg, DataDeNascimento = @dataNascimento, 
                                     DataDeAtualizacao = @dataAtualizacao WHERE Id = @id";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        id = pessoa.Id,
                        nome = pessoa.Nome,
                        cpf = pessoa.Cpf,
                        rg = pessoa.Rg,
                        dataNascimento = pessoa.DataDeNascimento,
                        dataAtualizacao = DateTime.Now
                    };
                    connection.Execute(query, parametros);
                }

                return pessoa;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
        public List<PessoaDto> ListarTodas()
        {
            List<PessoaDto> pessoasEncontradas;
            try
            {
                var query = @"SELECT Id, Nome, Cpf, Rg, DataDeRegistro, DataDeAtualizacao, DataDeNascimento, DataDeRegistro FROM Pessoa                                      ";

                using (var connection = new SqlConnection(_connection))
                {
                    pessoasEncontradas = connection.Query<PessoaDto>(query).ToList();
                }
                return pessoasEncontradas;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
        public List<PessoaDto> ListarPorCriterio(string criterio)
        {
            List<PessoaDto> pessoasEncontradas;
            try
            {
                var query = @"SELECT Id, Nome, Cpf, DataDeAtualizacao,
                                    DataDeNascimento, DataDeRegistro FROM Pessoa
                                      WHERE Nome like CONCAT('%',@criterio,'%')";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        criterio
                    };
                    pessoasEncontradas = connection.Query<PessoaDto>(query, parametros).ToList();
                }
                return pessoasEncontradas;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
        public bool Remover(int id)
        {
            try
            {
                var query = @"DELETE Pessoa WHERE Id = @id";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        id
                    };
                    connection.Query<PessoaDto>(query, parametros);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return false;
            }
        }
    }
}
