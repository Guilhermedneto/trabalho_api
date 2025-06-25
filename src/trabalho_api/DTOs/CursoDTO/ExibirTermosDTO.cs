using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trabalho_api.DTOs.CursoDTO
{
    public class ExibirTermosDTO
    {
        public Guid Id { get;  set; }
        public int Numero { get;  set; }
        public Guid CursoId { get;  set; }

    }
}