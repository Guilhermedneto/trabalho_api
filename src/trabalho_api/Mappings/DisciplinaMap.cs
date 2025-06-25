using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using trabalho_api.Entities;

namespace trabalho_api.Mappings
{
    public class DisciplinaMap : IEntityTypeConfiguration<Disciplina>
    {
        public void Configure(EntityTypeBuilder<Disciplina> builder)
        {
            builder.ToTable("Disciplinas");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                .ValueGeneratedNever();

            builder.Property(d => d.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(d => d.Termo)
                .WithMany(t => t.Disciplinas)
                .HasForeignKey(d => d.TermoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    

   
}


//   public Guid Id { get; private set; }
//         public string Nome { get; private set; }
//         public Guid TermoId { get; private set; }
//         public Termo Termo { get; private set; }