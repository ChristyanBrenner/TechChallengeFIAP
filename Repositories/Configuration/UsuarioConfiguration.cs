using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Configuration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(p => p.DataCriacao).HasColumnType("DATETIME").IsRequired();
            builder.Property(p => p.Nome).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(p => p.Email).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(p => p.SenhaHash).HasColumnType("VARCHAR(MAX)").IsRequired();
        }
    }
}
