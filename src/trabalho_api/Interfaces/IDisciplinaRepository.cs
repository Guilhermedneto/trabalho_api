using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trabalho_api.Entities;

namespace trabalho_api.Interfaces
{
    public interface IDisciplinaRepository
    {
        Task<IEnumerable<Disciplina>> ObterTodas();
        Task<Disciplina> ObterPorId(Guid id);
        Task<IEnumerable<Disciplina>> ObterPorNome(string nome);
        Task<IEnumerable<Disciplina>> ObterTodasComTermoECurso();
        void Adicionar(Disciplina disciplina);
        void Atualizar(Disciplina disciplina);
        void Remover(Disciplina disciplina);
        Task<bool> SalvarAlteracoes();

    }
}


//   Task<IEnumerable<Curso>> ObterTodos();
// Task<Curso> ObterPorId(Guid id);
// Task<IEnumerable<Curso>> ObterPorNome(string nome);
// void Adicionar(Curso curso);
// void Atualizar(Curso curso);
// void Remover(Curso curso);
// Task<bool> SalvarAlteracoes();