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
    [Route("curso")]
    public class CursoController : Controller
    {
        private readonly ICursoRepository _cursoRepository;

        public CursoController(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        [HttpGet("obter-todos")]
        public async Task<IActionResult> ObterTodos()
        {
            var cursos = await _cursoRepository.ObterTodos();
            var cursosDTO = cursos.Select(c => new ExibirCursoDTO
            {
                Nome = c.Nome,
                Duracao = c.Duracao,
                Apelido = c.Apelido,
            });
            if (cursosDTO == null || !cursosDTO.Any())
            {
                return NotFound("Nenhum curso encontrado.");
            }
            return Ok(cursosDTO);
        }

        [HttpGet("obter-por-nome/{nome}")]

        public async Task<IActionResult> ObterPorNome([FromRoute] string? nome)
        {
            var cursos = await _cursoRepository.ObterPorNome(nome);
            var cursosDTO = cursos.Select(c => new ExibirCursoDTO
            {
                Nome = c.Nome,
                Duracao = c.Duracao,
                Apelido = c.Apelido,
            });
            if (cursosDTO == null || !cursosDTO.Any())
            {
                return NotFound($"Nenhum curso encontrado com o nome {nome}.");
            }
            return Ok(cursosDTO);
        }

        [HttpGet("obter-por-id/{id}")]
        public async Task<IActionResult> ObterPorId([FromRoute] Guid id)
        {
            var curso = await _cursoRepository.ObterPorId(id);
            var cursoDTO = new ExibirCursoDTO
            {
                Nome = curso.Nome,
                Duracao = curso.Duracao,
                Apelido = curso.Apelido,
            };
            if (cursoDTO == null)
            {
                return NotFound($"Curso com ID {id} não encontrado.");
            }
            return Ok(cursoDTO);
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> Adicionar([FromBody] CriarCursoDTO cursoDTO)
        {
            var cursoExistente = await _cursoRepository.ObterPorNome(cursoDTO.Nome);
            if (cursoExistente != null && cursoExistente.Any(c => c.InstituicaoId == cursoDTO.InstituicaoId))
            {
                return BadRequest($"Já existe um curso com o nome {cursoDTO.Nome} nesta instituição.");
            }

            var curso = new Curso(
                cursoDTO.Nome,
                cursoDTO.Duracao,
                cursoDTO.Apelido,
                cursoDTO.InstituicaoId
            );

            _cursoRepository.Adicionar(curso);

            var SalvarAlteracoes = await _cursoRepository.SalvarAlteracoes();
            if (!SalvarAlteracoes)
            {
                return BadRequest("Erro ao adicionar o curso.");
            }
            return Ok(curso);
        }

        [HttpPut("atualizar")]
        public async Task<IActionResult> Atualizar([FromBody] EditarCursoDTO cursoDTO)
        {
            var cursoExistente = await _cursoRepository.ObterPorId(cursoDTO.Id);
            if (cursoExistente == null)
            {
                return NotFound($"Curso com ID {cursoDTO.Id} não encontrado.");
            }

            if (cursoDTO.Nome != cursoExistente.Nome)
            {
                var cursoComMesmoNome = await _cursoRepository.ObterPorNome(cursoDTO.Nome);
                if (cursoComMesmoNome != null && cursoComMesmoNome.Any())
                {
                    return BadRequest($"Já existe um curso com o nome {cursoDTO.Nome}.");
                }
            }

            cursoExistente.AtualizarNome(cursoDTO.Nome,
                                        cursoDTO.Duracao,
                                        cursoDTO.Apelido,
                                        cursoDTO.InstituicaoId  );
            _cursoRepository.Atualizar(cursoExistente);

            var SalvarAlteracoes = await _cursoRepository.SalvarAlteracoes();
            if (!SalvarAlteracoes)
            {
                return BadRequest("Erro ao atualizar o curso.");
            }
            return Ok(cursoExistente);
        }

        [HttpDelete("remover/{id}")]
        public async Task<IActionResult> Remover([FromRoute] Guid id)
        {
            var cursoExistente = await _cursoRepository.ObterPorId(id);
            if (cursoExistente == null)
            {
                return NotFound($"Curso com ID {id} não encontrado.");
            }

            _cursoRepository.Remover(cursoExistente);
            var SalvarAlteracoes = await _cursoRepository.SalvarAlteracoes();
            if (!SalvarAlteracoes)
            {
                return BadRequest("Erro ao remover o curso.");
            }
            return Ok($"Curso com ID {id} removido com sucesso.");
    }
    }
}
