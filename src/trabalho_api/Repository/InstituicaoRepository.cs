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
    public class InstituicaoRepository : IInstituicaoRepository
    {
        private readonly ApplicationDbContext _context;

        public InstituicaoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Adicionar(Instituicao instituicao)
        {
            _context.Instituicoes.Add(instituicao);
        }

        public void Atualizar(Instituicao instituicao)
        {
            _context.Instituicoes.Update(instituicao);
        }

        public async Task<Instituicao> ObterPorId(Guid id)
        {
            var instituicao = await _context.Instituicoes
                .Where(i => i.Id == id)
                .Include(i => i.Cursos)
                .Include(i => i.Funcionarios)
                .FirstOrDefaultAsync();

            return instituicao!;
        }

        public async Task<IEnumerable<Instituicao>> ObterPorNome(string nome)
        {
            var filtro = nome?.ToUpper();
            IEnumerable<Instituicao> instituicoes;
            if (filtro is null)
            {
                instituicoes = await _context.Instituicoes
                    .Include(i => i.Cursos)
                    .Include(i => i.Funcionarios)
                    .ToListAsync();
            }
            else
            {
                instituicoes = await _context.Instituicoes
                    .Include(i => i.Cursos)
                    .Include(i => i.Funcionarios)
                    .Where(i => i.Nome.ToUpper().Contains(filtro))
                    .ToListAsync();
            }
            return instituicoes;
        }

        public async Task<IEnumerable<Instituicao>> ObterTodos()
        {
            var instituicoes = await _context.Instituicoes
                .Include(i => i.Cursos)
                .Include(i => i.Funcionarios)
                .ToListAsync();

            return instituicoes;
        }

        public void Remover(Instituicao instituicao)
        {
            _context.Instituicoes.Remove(instituicao);
        }

        public async Task<bool> SalvarAlteracoes()
        {
            var resultado = await _context.SaveChangesAsync();
            return resultado > 0;
        }
    }
}