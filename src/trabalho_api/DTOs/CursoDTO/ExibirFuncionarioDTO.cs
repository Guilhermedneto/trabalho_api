using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trabalho_api.DTOs.CursoDTO
{
    public class ExibirFuncionarioDTO
    {
        public Guid Id { get;  set; }
        public Guid InstituicaoId { get;  set; }
        public string Nome { get;  set; } 
    }
}