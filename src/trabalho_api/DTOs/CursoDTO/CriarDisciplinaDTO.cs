using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trabalho_api.DTOs.CursoDTO
{
    public class CriarDisciplinaDTO
    {
        public string Nome { get; set; }
        public Guid TermoId { get; set; }
        public CriarDisciplinaDTO(string nome, Guid termoId)
        {
            Nome = nome;
            TermoId = termoId;
        }
    }
}