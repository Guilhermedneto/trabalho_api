using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trabalho_api.Entities;

namespace trabalho_api.Interfaces
{
    public interface IFuncionarioRepository
    {
        Task<IEnumerable<Funcionario>> ObterTodos();
        Task<Funcionario> ObterPorId(Guid id);
        Task<IEnumerable<Funcionario>> ObterPorNome(string nome);
        void Adicionar(Funcionario funcionario);
        void Atualizar(Funcionario funcionario);
        void Remover(Funcionario funcionario);
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