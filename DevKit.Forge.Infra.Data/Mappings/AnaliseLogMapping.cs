using DevKit.Forge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevKit.Forge.Infra.Data.Mappings;

public class AnaliseLogMapping : IEntityTypeConfiguration<AnaliseLog>
{
    public void Configure(EntityTypeBuilder<AnaliseLog> builder)
    {
        builder.ToTable("AnalisesDeLog");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.NomeArquivo)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.DataProcessamento)
            .IsRequired();

        builder.Property(x => x.Sucesso)
            .IsRequired();

        builder.Property(x => x.QtdErros)
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(x => x.QtdAvisos)
            .HasDefaultValue(0)
            .IsRequired();
    }
}
