using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trabalho_api.Entities;

namespace trabalho_api.Interfaces
{
    public interface ITermoRepository
    {
        Task<IEnumerable<Termo>> ObterTodos();
        Task<Termo> ObterPorId(Guid id);
        Task<IEnumerable<Termo>> ObterPorNome(int numero);
        void Adicionar(Termo termo);
        void Atualizar(Termo termo);
        void Remover(Termo termo);
        Task<bool> SalvarAlteracoes();

    }
}

//   Task<IEnumerable<Curso>> ObterTodos();
//         Task<Curso> ObterPorId(Guid id);
//         Task<IEnumerable<Curso>> ObterPorNome(string nome);
//         void Adicionar(Curso curso);
//         void Atualizar(Curso curso);
//         void Remover(Curso curso);
//         Task<bool> SalvarAlteracoes();