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
    [Route("funcionario")]
    public class FuncionarioController : Controller
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IInstituicaoRepository _instituicaoRepository;

        public FuncionarioController(IFuncionarioRepository funcionarioRepository, IInstituicaoRepository instituicaoRepository)
        {
            _funcionarioRepository = funcionarioRepository;
            _instituicaoRepository = instituicaoRepository;
        }

        [HttpGet("obter-todos")]
        public async Task<IActionResult> ObterTodos()
        {
            var funcionarios = await _funcionarioRepository.ObterTodos();
            var funcionarioDTOs = funcionarios.Select(d => new ExibirFuncionarioDTO
            {
                Id = d.Id,
                Nome = d.Nome,
                InstituicaoId = d.InstituicaoId,
            });
            if (funcionarioDTOs == null || !funcionarioDTOs.Any())
            {
                return NotFound("Nenhum funcionário encontrado.");
            }
            return Ok(funcionarioDTOs);
        }

        [HttpGet("obter-por-nome/{nome}")]
        public async Task<IActionResult> ObterPorNome([FromRoute] string? nome)
        {
            var funcionarios = await _funcionarioRepository.ObterPorNome(nome);
            var funcionariosDTO = funcionarios.Select(f => new ExibirFuncionarioDTO
            {
                Id = f.Id,
                InstituicaoId = f.InstituicaoId,
                Nome = f.Nome,
            });
            if (funcionariosDTO == null || !funcionariosDTO.Any())
            {
                return NotFound($"Nenhum funcionário encontrado com o nome {nome}.");
            }
            return Ok(funcionariosDTO);
        }

        [HttpGet("obter-por-id/{id}")]
        public async Task<IActionResult> ObterPorId([FromRoute] Guid id)
        {
            var funcionario = await _funcionarioRepository.ObterPorId(id);
            var funcionarioDTO = new ExibirFuncionarioDTO
            {
                Id = funcionario.Id,
                InstituicaoId = funcionario.InstituicaoId,
                Nome = funcionario.Nome,
            };
            if (funcionarioDTO == null)
            {
                return NotFound($"Funcionário com ID {id} não encontrado.");
            }
            return Ok(funcionarioDTO);
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> Adicionar([FromBody] CriarFuncionarioDTO funcionarioDTO)
        {
            var funcionarioExistente = await _funcionarioRepository.ObterPorNome(funcionarioDTO.Nome);
            if (funcionarioExistente != null && funcionarioExistente.Any())
            {
                return BadRequest($"Funcionário com o nome {funcionarioDTO.Nome} já existe.");
            }

            var funcionario = new Funcionario(funcionarioDTO.InstituicaoId,
                                              funcionarioDTO.Nome);

            _funcionarioRepository.Adicionar(funcionario);

            if (await _funcionarioRepository.SalvarAlteracoes())
            {
                return CreatedAtAction(nameof(ObterPorId), new { id = funcionario.Id }, funcionario);
            }

            return BadRequest("Erro ao adicionar funcionário.");
        }

        [HttpPut("atualizar")]
        public async Task<IActionResult> Atualizar([FromBody] EditarFuncionarioDTO funcionarioDTO)
        {
            var funcionarioExistente = await _funcionarioRepository.ObterPorId(funcionarioDTO.Id);
            if (funcionarioExistente == null)
            {
                return NotFound($"Funcionário com ID {funcionarioDTO.Id} não encontrado.");
            }

            // Verifica se a instituição existe
            var instituicaoExiste = await _instituicaoRepository.ObterPorId(funcionarioDTO.InstituicaoId);
            if (instituicaoExiste == null)
            {
                return NotFound($"Instituição com ID {funcionarioDTO.InstituicaoId} não encontrada.");
            }

            funcionarioExistente.AtualizarFuncionario(funcionarioDTO.Nome, funcionarioDTO.InstituicaoId);

            _funcionarioRepository.Atualizar(funcionarioExistente);
            if (await _funcionarioRepository.SalvarAlteracoes())
            {
                // Retorne um DTO simples, sem navegação
                var funcionarioDTOResult = new ExibirFuncionarioDTO
                {
                    Id = funcionarioExistente.Id,
                    InstituicaoId = funcionarioExistente.InstituicaoId,
                    Nome = funcionarioExistente.Nome
                };
                return Ok(funcionarioDTOResult);
            }
            return BadRequest("Erro ao atualizar funcionário.");
        }

        [HttpDelete("remover/{id}")]
        public async Task<IActionResult> Remover([FromRoute] Guid id)

        {
            var funcionarioExistente = await _funcionarioRepository.ObterPorId(id);
            if (funcionarioExistente == null)
            {
                return NotFound($"Funcionário com ID {id} não encontrado.");
            }

            _funcionarioRepository.Remover(funcionarioExistente);
            if (await _funcionarioRepository.SalvarAlteracoes())
            {
                return NoContent();
            }
            return BadRequest("Erro ao remover funcionário.");
        }
    }
}