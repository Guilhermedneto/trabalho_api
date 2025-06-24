using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trabalho_api.Entities
{
    public class Curso
    {
        protected Curso() { }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Duracao { get; private set; }
        public string Apelido { get; private set; }
        public Guid InstituicaoId { get; private set; }
        public Instituicao Instituicao { get; private set; }
        public ICollection<Termo> Termos { get; private set; }

        public Curso(string nome,
                     string duracao,
                     string apelido,
                     Guid instituicaoId)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Duracao = duracao;
            Apelido = apelido;
            InstituicaoId = instituicaoId;
        }
        

        public void AtualizarNome(string nome,
                                  string duracao,
                                  string apelido,
                                  Guid instituicaoId)
        {
            Nome = nome;
            Duracao = duracao;
            Apelido = apelido;
            InstituicaoId = instituicaoId;
        } 
    }
}