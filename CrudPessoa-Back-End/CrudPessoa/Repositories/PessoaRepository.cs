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
                    pessoaDto.DataDeNascimento = pessoa.DataDeNascimento.ToString("dd/MM/yyyy");
                    pessoaDto.DataDeAtualizacao = pessoa.DataAtualização.ToString("dd/MM/yyyy");
                    pessoaDto.DataDeRegistro = pessoa.DataRegistro.ToString("dd/MM/yyyy");
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
                        dataNascimento = Convert.ToDateTime(pessoa.DataDeNascimento),
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
                var query = @"SELECT Id, Nome, Cpf, Rg,
                                     FORMAT(p.DataDeRegistro, 'dd/MM/yyyy') as DataDeRegistro, 
                                     FORMAT(p.DataDeAtualizacao, 'dd/MM/yyyy') as DataDeAtualizacao, 
                                     FORMAT(p.DataDeNascimento, 'dd/MM/yyyy') as DataDeNascimento
                              FROM Pessoa p";

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
                var query = @"SELECT Id, Nome, Cpf, Rg,
                                     FORMAT(p.DataDeRegistro, 'dd/MM/yyyy') as DataDeRegistro, 
                                     FORMAT(p.DataDeAtualizacao, 'dd/MM/yyyy') as DataDeAtualizacao, 
                                     FORMAT(p.DataDeNascimento, 'dd/MM/yyyy') as DataDeNascimento 
                              FROM Pessoa p
                              WHERE CONCAT(Id,Nome,Cpf,Rg,DataDeRegistro,DataDeAtualizacao,DataDeNascimento) like CONCAT('%',@criterio,'%')";

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
        public PessoaDto EncontrarPorId(int id) 
        {
            PessoaDto pessoaEncontrada;
            try
            {
                var query = @"SELECT Id, Nome, Cpf, Rg,
                                     FORMAT(p.DataDeRegistro, 'dd/MM/yyyy') as DataDeRegistro, 
                                     FORMAT(p.DataDeAtualizacao, 'dd/MM/yyyy') as DataDeAtualizacao, 
                                     FORMAT(p.DataDeNascimento, 'dd/MM/yyyy') as DataDeNascimento                                    
                              FROM Pessoa p
                              WHERE Id = @id";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        id
                    };
                    pessoaEncontrada = connection.QueryFirstOrDefault<PessoaDto>(query, parametros);
                }
                return pessoaEncontrada;
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
