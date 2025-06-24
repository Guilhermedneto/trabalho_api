using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trabalho_api.DTOs.CursoDTO
{
    public class CriarFuncionarioDTO
    {
        public string Nome { get; set; }
        public Guid InstituicaoId { get; set; }

        public CriarFuncionarioDTO(string nome, Guid instituicaoId)
        {
            Nome = nome;
            InstituicaoId = instituicaoId;
        }
    }
}