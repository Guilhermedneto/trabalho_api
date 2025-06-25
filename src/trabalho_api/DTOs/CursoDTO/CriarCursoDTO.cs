using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trabalho_api.DTOs.CursoDTO
{
    public class CriarCursoDTO
    {
        public string Nome { get; set; }
        public string Duracao { get; set; }
        public string Apelido { get; set; }
        public Guid InstituicaoId { get; set; }


        public CriarCursoDTO(string nome,
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