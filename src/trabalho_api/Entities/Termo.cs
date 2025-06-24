using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trabalho_api.Entities
{
    public class Termo
    {
        protected Termo() { }

        public Guid Id { get; private set; }
        public int Numero { get; private set; }
        public Guid CursoId { get; private set; }
        public Curso Curso { get; private set; }
        public ICollection<Disciplina> Disciplinas { get; private set; }


        public Termo(int numero, Guid cursoId)
        {
            Id = Guid.NewGuid();
            Numero = numero;
            CursoId = cursoId;
        }
        
        public void AtualizarTermos(int numero, Guid cursoId)
        {
            Numero = numero;
            CursoId = cursoId;
        }
    }
}