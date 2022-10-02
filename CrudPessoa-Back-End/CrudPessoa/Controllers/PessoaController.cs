using CrudPessoa.Repositories;
using CrudPessoa.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CrudPessoa.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly PessoaRepository _pessoaRepository = new PessoaRepository();
        [HttpGet]
        public IActionResult ListarTodas()
        {
            var resultado = _pessoaRepository.ListarTodas();

            if (resultado == null || !resultado.Any())
                return Ok(new { sucesso = true, resultado, mensagem = "Não foram encontrados registros." });
            return Ok(new { sucesso = true, resultado });
        }

        [HttpGet]
        public IActionResult ListarPorCriterio(string criterio)
        {
            var resultado = _pessoaRepository.ListarPorCriterio(criterio);

            if (resultado == null || !resultado.Any())
                return Ok(new { sucesso = true, resultado, mensagem = "Não foram encontrados registros." });
            return Ok(new { sucesso = true, resultado });
        }

        [HttpPost]
        public IActionResult Salvar(SalvarPessoaViewModel salvarViewModel)
        {
            var resultado = _pessoaRepository.Salvar(salvarViewModel.Pessoa);

            if (resultado == null)
                return BadRequest(new { sucesso = false, resultado, mensagem = "Não foi possível inserir a pessoa. Contate o Administrador." });
            return Ok(new { sucesso = true, resultado, mensagem = "Registro salvo com sucesso." });
        }

        [HttpPut]
        public IActionResult Alterar(AtualizarPessoaViewModel atualizarPessoaViewModel)
        {
            var resultado = _pessoaRepository.Alterar(atualizarPessoaViewModel.Pessoa);

            if (resultado == null)
                return BadRequest(new { sucesso = false, resultado, mensagem = "Não foi possível atualizar a pessoa. Contate o Administrador." });
            return Ok(new { sucesso = true, resultado, mensagem = "Registro atualizado com sucesso." });
        }

        [HttpDelete]
        public IActionResult Remover(int id)
        {
            var resultado = _pessoaRepository.Remover(id);

            if (!resultado)
                return BadRequest(new { sucesso = resultado, mensagem = "Não foi possível remover a pessoa. Contate o Administrador." });
            return Ok(new { sucesso = true, mensagem = "Registro removido com sucesso." });
        }
    }
}
