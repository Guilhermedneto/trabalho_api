using Microsoft.EntityFrameworkCore;
using trabalho_api.Entities;
using trabalho_api.Mappings;

namespace trabalho_api.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Instituicao> Instituicoes { get; set; }
        public DbSet<Termo> Termos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }
            
            modelBuilder.ApplyConfiguration(new CursoMap());
            modelBuilder.ApplyConfiguration(new FuncionarioMap());
            modelBuilder.ApplyConfiguration(new InstituicaoMap());
            modelBuilder.ApplyConfiguration(new DisciplinaMap());
            modelBuilder.ApplyConfiguration(new TermoMap());
            
            base.OnModelCreating(modelBuilder);
        }
    }
   
}