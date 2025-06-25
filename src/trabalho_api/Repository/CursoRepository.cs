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
    public class CursoRepository : ICursoRepository
    {
        private readonly ApplicationDbContext _context;

        public CursoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Adicionar(Curso curso)
        {
            _context.Cursos.Add(curso); 
        }

        public void Atualizar(Curso curso)
        {
            _context.Cursos.Update(curso);
        }

        public async Task<Curso> ObterPorId(Guid id)
        {
            var curso = await _context.Cursos.Where(c => c.Id == id).FirstOrDefaultAsync();
                     
            return curso!;
           
        }

        public async Task<IEnumerable<Curso>> ObterPorNome(string? nome)
        {
            var filtro = nome?.ToUpper();

            IEnumerable<Curso> cursos;
            if (filtro is null)
            {
                cursos = await _context.Cursos.ToListAsync();
            }
            else
            {
                cursos = await _context.Cursos
                    .Where(c => c.Nome.ToUpper().Contains(filtro))
                    .ToListAsync();
            }
            
            return cursos;
            
        }

        public async Task<IEnumerable<Curso>> ObterTodos()
        {
            return await _context.Cursos.ToListAsync();
        }

        public void Remover(Curso curso)
        {
            _context.Cursos.Remove(curso);
        }

        public async Task<bool> SalvarAlteracoes()
        {
            var linhasAfetadas = await _context.SaveChangesAsync();
            return linhasAfetadas > 0;
        }

       
    }
}