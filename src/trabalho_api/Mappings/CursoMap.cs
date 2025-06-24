using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using trabalho_api.Entities;

namespace trabalho_api.Mappings
{
    public class CursoMap : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Duracao)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.Apelido)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.HasOne(c => c.Instituicao)
                .WithMany(i => i.Cursos)
                .HasForeignKey(c => c.InstituicaoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Cursos");
            
            builder.HasIndex(c => c.Nome).IsUnique();
        }
    }
}


//  public Guid Id { get; private set; }
//         public string Nome { get; private set; }
//         public string Duracao { get; private set; }
//         public string Apelido { get; private set; }
//         public Guid InstituicaoId { get; private set; }
//         public Instituicao Instituicao { get; private set; }