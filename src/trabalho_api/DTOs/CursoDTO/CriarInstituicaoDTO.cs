using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trabalho_api.DTOs.CursoDTO
{
    public class CriarInstituicaoDTO
    {
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string Endereco { get; set; }
        public int Numero { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public CriarInstituicaoDTO(string nome,
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



