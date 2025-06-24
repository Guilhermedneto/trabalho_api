using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trabalho_api.DTOs.CursoDTO
{
    public class EditarTermoDTO
    {
        public Guid Id { get; set; }
        public int Numero { get; set; }
        public Guid CursoId { get; set; }

        public EditarTermoDTO(int numero, Guid cursoId)
        {
            Numero = numero;
            CursoId = cursoId;
        }
    }
}