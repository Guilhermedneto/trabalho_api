using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trabalho_api.Entities
{
    public class Funcionario
    {
        protected Funcionario() { }
        public Guid Id { get; private set; }
        public Guid InstituicaoId { get; private set; }
        public Instituicao Instituicao { get; private set; }
        public string Nome { get; private set; }

        public Funcionario(Guid instituicaoId, string nome)
        {
            Id = Guid.NewGuid();
            InstituicaoId = instituicaoId;
            Nome = nome;
        }

        public void AtualizarFuncionario(string nome, Guid instituicaoId)
        {
            InstituicaoId = instituicaoId;
            Nome = nome;

        }
    }
}