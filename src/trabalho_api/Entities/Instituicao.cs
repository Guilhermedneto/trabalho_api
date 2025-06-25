using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trabalho_api.Entities
{
    public class Instituicao
    {
        protected Instituicao() { }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Apelido { get; private set; }
        public string Endereco { get; private set; }
        public int Numero { get; private set; }
        public string Cep { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        
        public ICollection<Curso> Cursos { get; set; }
        public ICollection<Funcionario> Funcionarios { get; set; }

    
    public Instituicao(string nome,
                                    string apelido,
                                    string endereco,
                                    int numero,
                                    string cep,
                                    string bairro,
                                    string cidade,
                                    string estado)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Apelido = apelido;
            Endereco = endereco;
            Numero = numero;
            Cep = cep;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
        }

        public void AtualizarInstituicao(string nome,
                                            string apelido,
                                            string endereco,
                                            int numero,
                                            string cep,
                                            string bairro,
                                            string cidade,
                                            string estado)
        {
            Nome = nome;
            Apelido = apelido;
            Endereco = endereco;
            Numero = numero;
            Cep = cep;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
        }
    }
}