using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trabalho_api.DTOs.CursoDTO
{
    public class EditarFuncionarioDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Guid InstituicaoId { get; set; }

        public EditarFuncionarioDTO(string nome, Guid instituicaoId)
        {
            Nome = nome;
            InstituicaoId = instituicaoId;
        }
    }
}