using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trabalho_api.Entities;

namespace trabalho_api.Interfaces
{
    public interface IInstituicaoRepository
    {
        Task<IEnumerable<Instituicao>> ObterTodos();
        Task<Instituicao> ObterPorId(Guid id);
        Task<IEnumerable<Instituicao>> ObterPorNome(string nome);
        void Adicionar(Instituicao instituicao);
        void Atualizar(Instituicao instituicao);
        void Remover(Instituicao instituicao);
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