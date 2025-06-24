using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using trabalho_api.Entities;

namespace trabalho_api.Mappings
{
    public class TermoMap : IEntityTypeConfiguration<Termo>
    {
        public void Configure(EntityTypeBuilder<Termo> builder)
        {
            builder.ToTable("Termos");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Numero)
                .IsRequired();

            builder.Property(t => t.CursoId)
                .IsRequired();

            builder.HasOne(t => t.Curso)
                .WithMany(c => c.Termos)
                .HasForeignKey(t => t.CursoId);
        }
        

    }
}

//  public Guid Id { get; private set; }
//         public int Numero { get; private set; }
//         public Guid CursoId { get; private set; }
//         public Curso Curso { get; private set; }