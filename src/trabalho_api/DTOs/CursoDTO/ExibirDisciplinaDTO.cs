using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trabalho_api.DTOs.CursoDTO
{
    public class ExibirDisciplinaDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Guid TermoId { get; set; }
        public string TermoNumero { get; set; }
        public Guid CursoId { get; set; }
        public string CursoNome { get; set; }
        public Guid InstituicaoId { get; set; }
        public string InstituicaoNome { get; set; }
    }
}