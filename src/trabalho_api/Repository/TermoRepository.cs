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
    public class TermoRepository : ITermoRepository
    {
        private readonly ApplicationDbContext _context;

        public TermoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Adicionar(Termo termo)
        {
            _context.Termos.Add(termo);
        }

        public void Atualizar(Termo termo)
        {
            _context.Termos.Update(termo);
        }

        public async Task<Termo> ObterPorId(Guid id)
        {
            var termo = await _context.Termos
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();
            return termo!;
        }

        public async Task<IEnumerable<Termo>> ObterPorNome(int numero)
        {
            var filtro = numero.ToString();
            IEnumerable<Termo> termos;
            if (string.IsNullOrEmpty(filtro))
            {
                termos = await _context.Termos.ToListAsync();
            }
            else
            {
                termos = await _context.Termos
                    .Where(t => t.Numero.ToString().Contains(filtro))
                    .ToListAsync();
            }       
            return termos;
        }

        public async Task<IEnumerable<Termo>> ObterTodos()
        {
            var termos = await _context.Termos
                .Include(t => t.Curso)
                .ToListAsync();
            return termos;

        }

        public void Remover(Termo termo)
        {
            _context.Termos.Remove(termo);
        }

        public async Task<bool> SalvarAlteracoes()
        {
            var resultado = await _context.SaveChangesAsync();
            return resultado > 0;
        }
    }
}