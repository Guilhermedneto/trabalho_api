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
    public class InstituicaoMap : IEntityTypeConfiguration<Instituicao>
    {
        public void Configure(EntityTypeBuilder<Instituicao> builder)
        {
            builder.ToTable("Instituicoes");

            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id)
                .ValueGeneratedNever();
            builder.Property(i => i.Nome)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(i => i.Apelido)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(i => i.Endereco)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(i => i.Numero)
                .IsRequired();
            builder.Property(i => i.Cep)
                .IsRequired()
                .HasMaxLength(10);
            builder.Property(i => i.Bairro)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(i => i.Cidade)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(i => i.Estado)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(i => i.Cursos)
                .WithOne(c => c.Instituicao)
                .HasForeignKey(c => c.InstituicaoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(i => i.Funcionarios)
                .WithOne(f => f.Instituicao)
                .HasForeignKey(f => f.InstituicaoId)
                .OnDelete(DeleteBehavior.Cascade);

                
        }

    }
}


    //  public Guid Id { get; private set; }
    //     public string Nome { get; private set; }
    //     public string Apelido { get; private set; }
    //     public string Endereco { get; private set; }
    //     public int Numero { get; private set; }
    //     public string Cep { get; private set; }
    //     public string Bairro { get; private set; }
    //     public string Cidade { get; private set; }
    //     public string Estado { get; private set; }