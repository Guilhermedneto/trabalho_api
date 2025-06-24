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
    public class DisciplinaRepository : IDisciplinaRepository
    {
        private readonly ApplicationDbContext _context;

        public DisciplinaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Adicionar(Disciplina disciplina)
        {
            _context.Disciplinas.Add(disciplina);
        }

        public void Atualizar(Disciplina disciplina)
        {
            _context.Disciplinas.Update(disciplina);
        }

        public async Task<Disciplina?> ObterPorId(Guid id)
        {
            var disciplina = await _context.Disciplinas
                .Where(d => d.Id == id)
                .Include(d => d.Termo)
                    .ThenInclude(t => t.Curso)
                    .ThenInclude(c => c.Instituicao)
                .FirstOrDefaultAsync();

            return disciplina;
        }

        public async Task<IEnumerable<Disciplina>> ObterPorNome(string nome)
        {
            var filtro = nome?.ToUpper();
            IEnumerable<Disciplina> disciplinas;
            if (filtro is null)
            {
                disciplinas = await _context.Disciplinas
                    .Include(d => d.Termo)
                        .ThenInclude(t => t.Curso)
                        .ThenInclude(c => c.Instituicao)
                    .ToListAsync();
            }
            else
            {
                disciplinas = await _context.Disciplinas
                    .Where(d => d.Nome.ToUpper().Contains(filtro))
                    .Include(d => d.Termo)
                        .ThenInclude(t => t.Curso)
                        .ThenInclude(c => c.Instituicao)
                    .ToListAsync();
            }
            return disciplinas;
        }

        public async Task<IEnumerable<Disciplina>> ObterTodas()
        {
            var disciplinas = await _context.Disciplinas
                .Include(d => d.Termo)
                .ToListAsync();
            return disciplinas;
        }

        public async Task<IEnumerable<Disciplina>> ObterTodasComTermoECurso()
        {
            var disciplinas = await _context.Disciplinas
                .Include(d => d.Termo)
                    .ThenInclude(t => t.Curso)
                    .ThenInclude(c => c.Instituicao)
                .ToListAsync();
            return disciplinas;
        }

        public void Remover(Disciplina disciplina)
        {
            _context.Disciplinas.Remove(disciplina);
        }

        public async Task<bool> SalvarAlteracoes()
        {
            var resultado = await _context.SaveChangesAsync();
            return resultado > 0;
        }
    }
}