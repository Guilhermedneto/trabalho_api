using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trabalho_api.DTOs.CursoDTO
{
    public class ExibirInstituicaoDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string Endereco { get; set; }
        public int Numero { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }  
        public ICollection<ExibirFuncionarioDTO> Funcionarios { get; set; }
        public ICollection<ExibirCursoDTO> Cursos { get; set; }
        
    }
}