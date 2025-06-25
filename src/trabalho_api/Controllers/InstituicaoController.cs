using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using trabalho_api.DTOs.CursoDTO;
using trabalho_api.Entities;
using trabalho_api.Interfaces;

namespace trabalho_api.Controllers
{
    [ApiController]
    [Route("instituicao")]
    public class InstituicaoController : Controller
    {
        private readonly IInstituicaoRepository _instituicaoRepository;

        public InstituicaoController(IInstituicaoRepository instituicaoRepository)
        {
            _instituicaoRepository = instituicaoRepository;
        }



        [HttpGet("obter-todos")]
        public async Task<IActionResult> ObterTodos()
        {
            var instituicoes = await _instituicaoRepository.ObterTodos();
            var instituicoesDTO = instituicoes.Select(i => new ExibirInstituicaoDTO
            {
                Id = i.Id,
                Nome = i.Nome,
                Apelido = i.Apelido,
                Endereco = i.Endereco,
                Numero = i.Numero,
                Cep = i.Cep,
                Bairro = i.Bairro,
                Cidade = i.Cidade,
                Estado = i.Estado,
                Funcionarios = (i.Funcionarios ?? new List<Funcionario>()).Select(f => new ExibirFuncionarioDTO
                {
                    Id = f.Id,
                    InstituicaoId = f.InstituicaoId,
                    Nome = f.Nome,
                }).ToList(),
                Cursos = (i.Cursos ?? new List<Curso>()).Select(c => new ExibirCursoDTO
                {
                    Nome = c.Nome,
                    Duracao = c.Duracao,
                    Apelido = c.Apelido,
                }).ToList()

            });
            if (instituicoesDTO == null || !instituicoesDTO.Any())
            {
                return NotFound("Nenhuma instituição encontrada.");
            }
            return Ok(instituicoesDTO);
        }

        [HttpGet("obter-por-nome/{nome}")]
        public async Task<IActionResult> ObterPorNome([FromRoute] string? nome)
        {
            var instituicoes = await _instituicaoRepository.ObterPorNome(nome);
            var instituicoesDTO = instituicoes.Select(i => new ExibirInstituicaoDTO
            {
                Id = i.Id,
                Nome = i.Nome,
                Apelido = i.Apelido,
                Endereco = i.Endereco,
                Numero = i.Numero,
                Cep = i.Cep,
                Bairro = i.Bairro,
                Cidade = i.Cidade,
                Estado = i.Estado,
                Funcionarios = (i.Funcionarios ?? new List<Funcionario>()).Select(f => new ExibirFuncionarioDTO
                {
                    Id = f.Id,
                    InstituicaoId = f.InstituicaoId,
                    Nome = f.Nome,
                }).ToList(),
                Cursos = (i.Cursos ?? new List<Curso>()).Select(c => new ExibirCursoDTO
                {
                    Nome = c.Nome,
                    Duracao = c.Duracao,
                    Apelido = c.Apelido,
                }).ToList()
            });
            if (instituicoesDTO == null || !instituicoesDTO.Any())
            {
                return NotFound($"Nenhuma instituição encontrada com o nome {nome}.");
            }
            return Ok(instituicoesDTO);
        }

        [HttpGet("obter-por-id/{id}")]
        public async Task<IActionResult> ObterPorId([FromRoute] Guid id)
        {
            var instituicao = await _instituicaoRepository.ObterPorId(id);
            var instituicaoDTO = new ExibirInstituicaoDTO
            {
                Id = instituicao.Id,
                Nome = instituicao.Nome,
                Apelido = instituicao.Apelido,
                Endereco = instituicao.Endereco,
                Numero = instituicao.Numero,
                Cep = instituicao.Cep,
                Bairro = instituicao.Bairro,
                Cidade = instituicao.Cidade,
                Estado = instituicao.Estado,
                Funcionarios = instituicao.Funcionarios.Select(f => new ExibirFuncionarioDTO
                {
                    Id = f.Id,
                    InstituicaoId = f.InstituicaoId,
                    Nome = f.Nome,
                }).ToList(),
                Cursos = instituicao.Cursos.Select(c => new ExibirCursoDTO
                {
                    Nome = c.Nome,
                    Duracao = c.Duracao,
                    Apelido = c.Apelido,
                }).ToList()
            };
            if (instituicaoDTO == null)
            {
                return NotFound($"Instituição com ID {id} não encontrada.");
            }
            return Ok(instituicaoDTO);
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> Adicionar([FromBody] CriarInstituicaoDTO instituicaoDTO)
        {
            var instituicaoExistente = await _instituicaoRepository.ObterPorNome(instituicaoDTO.Nome);
            if (instituicaoExistente != null && instituicaoExistente.Any())
            {
                return BadRequest($"Instituição com o nome {instituicaoDTO.Nome} já existe.");
            }

            var instituicao = new Instituicao(instituicaoDTO.Nome,
                                                instituicaoDTO.Apelido,
                                                instituicaoDTO.Endereco,
                                                instituicaoDTO.Numero,
                                                instituicaoDTO.Cep,
                                                instituicaoDTO.Bairro,
                                                instituicaoDTO.Cidade,
                                                instituicaoDTO.Estado
                                              );

            _instituicaoRepository.Adicionar(instituicao);
            if (await _instituicaoRepository.SalvarAlteracoes())
            {
                return CreatedAtAction(nameof(ObterPorId), new { id = instituicao.Id }, instituicao);
            }

            return BadRequest("Erro ao adicionar a instituição.");
        }

        [HttpPut ("atualizar")]
        public async Task<IActionResult> Atualizar([FromBody] EditarInstituicaoDTO editarInstituicaoDTO)
        {
            var instituicaoExistente = await _instituicaoRepository.ObterPorId(editarInstituicaoDTO.Id);
            if (instituicaoExistente == null)
            {
                return NotFound($"Instituição com ID {editarInstituicaoDTO.Id} não encontrada.");
            }

            if (editarInstituicaoDTO.Nome != instituicaoExistente.Nome)
            {
                var instituicaoComMesmoNome = await _instituicaoRepository.ObterPorNome(editarInstituicaoDTO.Nome);
                if (instituicaoComMesmoNome != null && instituicaoComMesmoNome.Any())
                {
                    return BadRequest($"Já existe uma instituição com o nome {editarInstituicaoDTO.Nome}.");
                }
            }

            instituicaoExistente.AtualizarInstituicao(editarInstituicaoDTO.Nome,
                                                      editarInstituicaoDTO.Apelido,
                                                      editarInstituicaoDTO.Endereco,
                                                      editarInstituicaoDTO.Numero,
                                                      editarInstituicaoDTO.Cep,
                                                      editarInstituicaoDTO.Bairro,
                                                      editarInstituicaoDTO.Cidade,
                                                      editarInstituicaoDTO.Estado);

            _instituicaoRepository.Atualizar(instituicaoExistente);

            if (await _instituicaoRepository.SalvarAlteracoes())
            {
                return Ok(instituicaoExistente);
            }

            return BadRequest("Erro ao atualizar a instituição.");
        }

        [HttpDelete("remover/{id}")]
        public async Task<IActionResult> Remover([FromRoute] Guid id)
        {
            var instituicaoExistente = await _instituicaoRepository.ObterPorId(id);
            if (instituicaoExistente == null)
            {
                return NotFound($"Instituição com ID {id} não encontrada.");
            }

            _instituicaoRepository.Remover(instituicaoExistente);
            if (await _instituicaoRepository.SalvarAlteracoes())
            {
                return NoContent();
            }

            return BadRequest("Erro ao remover a instituição.");
        }
    }
}