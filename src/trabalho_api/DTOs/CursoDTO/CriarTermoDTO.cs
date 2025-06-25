using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trabalho_api.DTOs.CursoDTO
{
    public class CriarTermoDTO
    {
        public int Numero { get; set; }
        public Guid CursoId { get; set; }

        public CriarTermoDTO(int numero, Guid cursoId)
        {
            Numero = numero;
            CursoId = cursoId;
        }
    }
}