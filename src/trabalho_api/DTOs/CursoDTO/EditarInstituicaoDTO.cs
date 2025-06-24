using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trabalho_api.DTOs.CursoDTO
{
    public class EditarInstituicaoDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string Endereco { get; set; }
        public int Numero { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public EditarInstituicaoDTO(Guid id,
                                 string nome,
                                 string apelido,
                                 string endereco,
                                 int numero,
                                 string cep,
                                 string bairro,
                                 string cidade,
                                 string estado)
        {
            Id = id;
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
