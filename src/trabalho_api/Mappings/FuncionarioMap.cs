using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using trabalho_api.Entities;

namespace trabalho_api.Mappings
{
    public class FuncionarioMap : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.ToTable("Funcionarios");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(f => f.Instituicao)
                .WithMany(i => i.Funcionarios)
                .HasForeignKey(f => f.InstituicaoId);
        }
    }
}


//  public Guid Id { get; private set; }
//         public Guid InstituicaoId { get; private set; }
//         public Instituicao Instituicao { get; private set; }
//         public string Nome { get; private set; }