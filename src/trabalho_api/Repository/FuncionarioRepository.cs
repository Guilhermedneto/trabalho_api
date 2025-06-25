using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using trabalho_api.Context;
using trabalho_api.Entities;
using trabalho_api.Interfaces;

namespace trabalho_api.Repository
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly ApplicationDbContext _context;

        public FuncionarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Adicionar(Funcionario funcionario)
        {
            _context.Funcionarios.Add(funcionario);
        }

        public void Atualizar(Funcionario funcionario)
        {
            _context.Funcionarios.Update(funcionario);
        }

        public async Task<Funcionario> ObterPorId(Guid id)
        {
            var funcionario = await _context.Funcionarios
                .Where(f => f.Id == id)
                .FirstOrDefaultAsync();
            return funcionario!;
        }

        public async Task<IEnumerable<Funcionario>> ObterPorNome(string nome)
        {
            var filtro = nome?.ToUpper();
            IEnumerable<Funcionario> funcionarios;
            if (filtro is null)
            {
                funcionarios = await _context.Funcionarios.ToListAsync();
            }
            else
            {
                funcionarios = await _context.Funcionarios
                    .Where(f => f.Nome.ToUpper().Contains(filtro))
                    .ToListAsync();
            }
            return funcionarios;

        }

        public async Task<IEnumerable<Funcionario>> ObterTodos()
        {
            var funcionarios = await _context.Funcionarios
                .Include(f => f.Instituicao)
                .ToListAsync();
            return funcionarios;

        }

        public void Remover(Funcionario funcionario)
        {
            _context.Funcionarios.Remove(funcionario);
        }

        public async Task<bool> SalvarAlteracoes()
        {
            var resultado = await _context.SaveChangesAsync();
            return resultado > 0;
        }
    }
}