using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RIKTrialServer.Domain.Models;

namespace RIKTrialServer.Infra.Persistance.Configs
{
    public sealed class CompanyConfiguration
    : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies");

            builder.Property(x => x.Name)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(x => x.CompanyCode)
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(x => x.ParticipantAmount)
                   .IsRequired();

            builder.Property(x => x.AdditionalInfo)
                .HasMaxLength(5000);
        }
    }
}
