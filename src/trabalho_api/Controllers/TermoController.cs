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
    [Route("[controller]")]
    public class TermoController : Controller
    {
        private readonly ITermoRepository _termoRepository;

        public TermoController(ITermoRepository termoRepository)
        {
            _termoRepository = termoRepository;
        }

        [HttpGet("obter-todos")]
        public async Task<IActionResult> ObterTodos()
        {
            var termos = await _termoRepository.ObterTodos();
            var termosDTO = termos.Select(t => new ExibirTermosDTO
            {
                Id = t.Id,
                Numero = t.Numero,
                CursoId = t.CursoId
            });
            if (termosDTO == null || !termosDTO.Any())
            {
                return NotFound("Nenhum termo encontrado.");
            }
            return Ok(termosDTO);
        }
        [HttpGet("obter-por-nome/{nome}")]
        public async Task<IActionResult> ObterPorNome([FromRoute] int numero)
        {
            var termos = await _termoRepository.ObterPorNome(numero);
            var termosDTO = termos.Select(t => new ExibirTermosDTO
            {
                Id = t.Id,
                Numero = t.Numero,
                CursoId = t.CursoId
            });
            if (termosDTO == null || !termosDTO.Any())
            {
                return NotFound($"Nenhum termo encontrado com o número {numero}.");
            }
            return Ok(termosDTO);
        }

        [HttpGet("obter-por-id/{id}")]
        public async Task<IActionResult> ObterPorId([FromRoute] Guid id)
        {
            var termo = await _termoRepository.ObterPorId(id);
            var termoDTO = new ExibirTermosDTO
            {
                Id = termo.Id,
                Numero = termo.Numero,
                CursoId = termo.CursoId
            };

            if (termoDTO == null)
            {
                return NotFound($"Termo com ID {id} não encontrado.");
            }
            return Ok(termoDTO);
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> Adicionar([FromBody] CriarTermoDTO termoNumero)
        {
            var termosExistentes = await _termoRepository.ObterPorNome(termoNumero.Numero);
            // Verifica se já existe termo com o mesmo número no mesmo curso
            if (termosExistentes != null && termosExistentes.Any(t => t.CursoId == termoNumero.CursoId))
            {
                return BadRequest($"Já existe um termo com o número {termoNumero.Numero} neste curso.");
            }

            var termo = new Termo(termoNumero.Numero, termoNumero.CursoId);

            _termoRepository.Adicionar(termo);
            if (await _termoRepository.SalvarAlteracoes())
            {
                return CreatedAtAction(nameof(ObterPorId), new { id = termo.Id }, termo);
            }
            else
            {
                return BadRequest("Erro ao adicionar o termo.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] EditarTermoDTO termo)
        {
            var termoExistente = await _termoRepository.ObterPorId(termo.Id);
            if (termoExistente == null)
            {
                return NotFound($"Termo com ID {termo.Id} não encontrado.");
            }

            termoExistente.AtualizarTermos(termo.Numero,
                                            termo.CursoId);

            _termoRepository.Atualizar(termoExistente);

            if (await _termoRepository.SalvarAlteracoes())
            {
                return Ok(termoExistente);
            }
            else
            {
                return BadRequest("Erro ao atualizar o termo.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover([FromRoute] Guid id)
        {
            var termoExistente = await _termoRepository.ObterPorId(id);
            if (termoExistente == null)
            {
                return NotFound($"Termo com ID {id} não encontrado.");
            }

            _termoRepository.Remover(termoExistente);
            if (await _termoRepository.SalvarAlteracoes())
            {
                return Ok(termoExistente);
            }
            else
            {
                return BadRequest("Erro ao remover o termo.");
            }
        }
    }
}