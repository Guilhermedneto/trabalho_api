using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trabalho_api.Entities
{
    public class Disciplina
    {
        protected Disciplina() { }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public Guid TermoId { get; private set; }
        public Termo Termo { get; private set; }

        public Disciplina(string nome, Guid termoId)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            TermoId = termoId;
        }

        public void AtualizarDisciplina(string nome, Guid termoId)
        {
            Nome = nome;
            TermoId = termoId;
        }
    }
}