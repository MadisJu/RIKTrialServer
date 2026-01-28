using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RIKTrialServer.Domain.Models;

namespace RIKTrialServer.Infra.Persistance.Configs
{
    public sealed class PersonConfiguration
    : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons");

            builder.Property(x => x.FirstName)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(x => x.LastName)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(x => x.IdNumber)
                   .HasMaxLength(11)
                   .IsRequired();

            builder.HasIndex(x => x.IdNumber)
                   .IsUnique();

            builder.Property(x => x.AdditionalInfo)
                .HasMaxLength(1500);
        }
    }
}
