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
    [Route("disciplina")]
    public class DisciplinaController : ControllerBase
    {
        private readonly IDisciplinaRepository _disciplinaRepository;
        private readonly ITermoRepository _termoRepository;

        public DisciplinaController(IDisciplinaRepository disciplinaRepository, ITermoRepository termoRepository)
        {
            _disciplinaRepository = disciplinaRepository;
            _termoRepository = termoRepository;
        }

        [HttpGet("obter-todas")]
        public async Task<IActionResult> ObterTodas()
        {
            var disciplinas = await _disciplinaRepository.ObterTodasComTermoECurso();
            var disciplinasDTO = disciplinas.Select(d => new ExibirDisciplinaDTO
            {
                Id = d.Id,
                Nome = d.Nome,
                TermoId = d.TermoId,
                TermoNumero = d.Termo?.Numero.ToString(),
                CursoId = d.Termo?.CursoId ?? Guid.Empty,
                CursoNome = d.Termo?.Curso?.Nome,
                InstituicaoId = d.Termo?.Curso?.InstituicaoId ?? Guid.Empty,
                InstituicaoNome = d.Termo?.Curso?.Instituicao?.Nome
            });
            return Ok(disciplinasDTO);
        }

        [HttpGet("obter-por-nome/{nome}")]
        public async Task<IActionResult> ObterPorNome([FromRoute] string? nome)
        {
            var disciplinas = await _disciplinaRepository.ObterPorNome(nome);
            var disciplinasDTO = disciplinas.Select(d => new ExibirDisciplinaDTO
            {
                Id = d.Id,
                Nome = d.Nome,
                TermoId = d.TermoId,
                TermoNumero = d.Termo != null ? d.Termo.Numero.ToString() : string.Empty,
                CursoId = d.Termo != null ? d.Termo.CursoId : Guid.Empty,
                CursoNome = d.Termo != null && d.Termo.Curso != null ? d.Termo.Curso.Nome : string.Empty,
                InstituicaoId = d.Termo != null && d.Termo.Curso != null ? d.Termo.Curso.InstituicaoId : Guid.Empty,
                InstituicaoNome = d.Termo != null && d.Termo.Curso != null && d.Termo.Curso.Instituicao != null ? d.Termo.Curso.Instituicao.Nome : string.Empty
            });
            if (disciplinasDTO == null || !disciplinasDTO.Any())
            {
                return NotFound($"Nenhuma disciplina encontrada com o nome {nome}.");
            }
            return Ok(disciplinasDTO);
        }
        [HttpGet("obter-por-id/{id}")]
        public async Task<IActionResult> ObterPorId([FromRoute] Guid id)
        {
            var disciplina = await _disciplinaRepository.ObterPorId(id);

            if (disciplina == null)
            {
                return NotFound($"Disciplina com ID {id} não encontrada.");
            }

            var disciplinaDTO = new ExibirDisciplinaDTO
            {
                Id = disciplina.Id,
                Nome = disciplina.Nome,
                TermoId = disciplina.TermoId,
                TermoNumero = disciplina.Termo?.Numero.ToString(),
                CursoId = disciplina.Termo?.CursoId ?? Guid.Empty,
                CursoNome = disciplina.Termo?.Curso?.Nome,
                InstituicaoId = disciplina.Termo?.Curso?.InstituicaoId ?? Guid.Empty,
                InstituicaoNome = disciplina.Termo?.Curso?.Instituicao?.Nome
            };

            return Ok(disciplinaDTO);
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> Adicionar([FromBody] CriarDisciplinaDTO disciplinaDTO)
        {
            var disciplinaExistente = await _disciplinaRepository.ObterPorNome(disciplinaDTO.Nome);
            if (disciplinaExistente != null && disciplinaExistente.Any())
            {
                return BadRequest($"Disciplina com o nome {disciplinaDTO.Nome} já existe.");
            }

            var disciplina = new Disciplina(disciplinaDTO.Nome,
                                            disciplinaDTO.TermoId);

            _disciplinaRepository.Adicionar(disciplina);

            var salvarAlteracoes = await _disciplinaRepository.SalvarAlteracoes();
            if (!salvarAlteracoes)
            {
                return BadRequest("Erro ao adicionar a disciplina.");
            }
            return Ok(disciplina);
        }

        [HttpPut("atualizar")]
        public async Task<IActionResult> Atualizar([FromBody] EditarDisciplinaDTO disciplinaDTO)
        {
            var disciplinaExistente = await _disciplinaRepository.ObterPorId(disciplinaDTO.Id);
            if (disciplinaExistente == null)
            {
                return NotFound($"Disciplina com ID {disciplinaDTO.Id} não encontrada.");
            }

            // Verifica se o termo existe antes de atualizar
            var termoExiste = await _termoRepository.ObterPorId(disciplinaDTO.TermoId);
            if (termoExiste == null)
            {
                return NotFound($"Termo com ID {disciplinaDTO.TermoId} não encontrado.");
            }

            if (disciplinaDTO.Nome != disciplinaExistente.Nome)
            {
                var disciplinaComMesmoNome = await _disciplinaRepository.ObterPorNome(disciplinaDTO.Nome);
                if (disciplinaComMesmoNome != null && disciplinaComMesmoNome.Any())
                {
                    return BadRequest($"Já existe uma disciplina com o nome {disciplinaDTO.Nome}.");
                }
            }

            disciplinaExistente.AtualizarDisciplina(disciplinaDTO.Nome, disciplinaDTO.TermoId);

            _disciplinaRepository.Atualizar(disciplinaExistente);

            var salvarAlteracoes = await _disciplinaRepository.SalvarAlteracoes();
            if (!salvarAlteracoes)
            {
                return BadRequest("Erro ao atualizar a disciplina.");
            }

            // Retorne um DTO simples para evitar ciclos
            var disciplinaAtualizadaDTO = new ExibirDisciplinaDTO
            {
                Id = disciplinaExistente.Id,
                Nome = disciplinaExistente.Nome,
                TermoId = disciplinaExistente.TermoId,
                TermoNumero = termoExiste.Numero.ToString(),
                CursoId = termoExiste.CursoId,
                CursoNome = termoExiste.Curso?.Nome ?? string.Empty,
                InstituicaoId = termoExiste.Curso?.InstituicaoId ?? Guid.Empty,
                InstituicaoNome = termoExiste.Curso?.Instituicao?.Nome ?? string.Empty
            };

            return Ok(disciplinaAtualizadaDTO);
        }

        [HttpDelete("remover/{id}")]
        public async Task<IActionResult> Remover([FromRoute] Guid id)
        {
            var disciplinaExistente = await _disciplinaRepository.ObterPorId(id);
            if (disciplinaExistente == null)
            {
                return NotFound($"Disciplina com ID {id} não encontrada.");
            }

            _disciplinaRepository.Remover(disciplinaExistente);
            var salvarAlteracoes = await _disciplinaRepository.SalvarAlteracoes();
            if (!salvarAlteracoes)
            {
                return BadRequest("Erro ao remover a disciplina.");
            }
            return Ok($"Disciplina com ID {id} removida com sucesso.");
        }
    }
}

